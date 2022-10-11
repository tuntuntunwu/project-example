using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Data.SqlClient;
using System.ServiceProcess;

namespace FollowMEService
{
    /// <summary>
    ///SocketRecv 用于监听服务器端指定端口，接收SPL数据或者相关命令。
    /// </summary>
    public class SocketRecv
    {
        
        private Socket s;
        // reflect paramter
        private AbstractExtract extract;
        private IImport import;
        private static readonly string SpaceName = "FollowMEService";
        private static readonly string AssemblyName = "FollowMEService";

        /// <summary>
        /// Init the AbstractExtract & IImport 
        /// </summary>
        public SocketRecv()
        {
            try
            {
                string className = SpaceName + "." + ServiceCommon.AnalyseMethod;
                extract = (AbstractExtract)Assembly.Load(AssemblyName).CreateInstance(className);

                string importClassName = SpaceName + "." + ServiceCommon.ImportMethod;
                import = (IImport)Assembly.Load(AssemblyName).CreateInstance(importClassName);
            }
            catch (Exception e)
            {
                Log.log(e.ToString());
                extract = new ExtractUserName();
                import = new ImportToPCName();
            }
        }

        /// <summary>
        /// start listening the request, each request will create a thread to handle it. 
        /// </summary>
        public void CreatSocket()
        {
            try
            {
                int port = Convert.ToInt32(ServiceCommon.RawRecvPort);

                IPAddress ipAddress;
                //get local ip address
                if (ServiceCommon.GetAppSettingString("IPAddress").Trim() == "")
                {
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    ipAddress = ipHostInfo.AddressList[0];
                }
                else
                {
                    ipAddress = IPAddress.Parse(ServiceCommon.GetAppSettingString("IPAddress").Trim());
                }

                //创建终结点（EndPoint）
                IPEndPoint ipe = new IPEndPoint(ipAddress, port);

                //创建socket并开始监听
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Bind(ipe);
                s.Listen(0);
                Log.log("start application...");
                Log.log("waiting for connection...");

                
                //接受到client连接，为此连接建立新的socket，并接受信息

                while (true)//定义循环，以便可以建立N次连接
                {
                    //为新建连接创建新的socket
                    Socket temp = s.Accept();
                    Log.log("set up connection!");
                    Thread handlThread = new Thread(new ParameterizedThreadStart(HandlerSocket));
                    handlThread.Start(temp);
                 
                }
            }
            catch (Exception ex)
            {
                Log.log("Error Message:" + ex.ToString());
                Log.log(ex.Message);
                if (s != null)
                {
                    s.Close();
                }
            }
        }

        /// <summary>
        /// try to stop socket
        /// </summary>
        public void StopSocket()
        {
            if (s != null)
                s.Close();
        }

        /// <summary>
        /// synchro call back method, handle transfer and get param
        /// </summary>
        /// <param name="o"></param>
        public void HandlerSocket(object o)
        {
            try
            {
                if (o == null)
                    return;
                Socket temp = (Socket)o;
                byte[] recvBytes = new byte[2048];
                int bytesCount = 0;
                bool firstPack = true;
                int taskID = -1;

                string filepath = "";
                // file save path
                FileStream fs = null;
                while (true)
                {
                    bytesCount = temp.Receive(recvBytes, recvBytes.Length, 0);
                    //读取完成后退出循环  
                    if (bytesCount <= 0)
                    {
                        if (fs != null)
                        {
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();

                        }
                        recvBytes = null;

                        //active DB
                        //user can't see the print task until transfer finished
                        if (taskID != -1)
                        {
                            ActiveDBRecord(taskID);
                        }
                        break;
                    }

                    // first read,inital file name & path
                    if (firstPack)
                    {
                        firstPack = false;

                        // if only the info of Command then break
                        // The empty login name will no longer break any more 2014.2.11
                        //if (IsOnlyFirstPack(recvBytes, bytesCount, out fs, out taskID))
                        if (IsOnlyFirstPack(recvBytes, bytesCount, out fs, out taskID, out filepath))
                        {
                            break;
                        }
 
                        recvBytes = new byte[1024];
                        continue;
                    }
                    fs.Write(recvBytes, 0, bytesCount);
                    recvBytes = new byte[1024];
                }
                if (temp != null)
                {
                    temp.Close();
                    Log.log("connection has been closed!");
                }
            }
            catch (Exception e)
            {
                Log.log("Error Message:" + e.ToString());
            }
        }

        /// <summary>
        /// build file path
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string BuildDisk(PrintParaModel model)
        {
            //string folder = ServiceCommon.StringToByte(model.LoginName);
            //string disk = ServiceCommon.SplFileLocation + folder + "\\";

            string folder = DateTime.Now.ToString("yyyy-MM-dd");
            string disk = ServiceCommon.SplFileLocation + folder + "\\";

            if (!Directory.Exists(disk))
            {
                Directory.CreateDirectory(disk);
            }
            return disk;
        }

        /// <summary>
        /// save info to DB
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileLocation"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private int SaveToDB(string fileName, string fileLocation, PrintParaModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ServiceCommon.DBConnectionStrings))
                {
                    object ID;
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {
                        string strSql;
                        //1. Set User Group Information
                        strSql = "   INSERT INTO [MFPPrintTask]          " + Environment.NewLine;
                        strSql += "             ([LoginName]                " + Environment.NewLine;
                        strSql += "             ,[DiskFileName]          " + Environment.NewLine;
                        strSql += "             ,[FileLocation]         " + Environment.NewLine;
                        strSql += "             ,[PrintFileName]          " + Environment.NewLine;
                        strSql += "             ,[CreateTime]          " + Environment.NewLine;
                        strSql += "             ,[State])           " + Environment.NewLine;
                        strSql += "       VALUES                     " + Environment.NewLine;
                        strSql += "             ({0}                 " + Environment.NewLine;
                        strSql += "             ,{1}                 " + Environment.NewLine;
                        strSql += "             ,{2}                 " + Environment.NewLine;
                        strSql += "             ,{3}                 " + Environment.NewLine;
                        strSql += "             ,getdate()           " + Environment.NewLine;
                        strSql += "             ,{4});               " + Environment.NewLine;
                        strSql += "             select @@IDENTITY    " + Environment.NewLine;

                        string[] paramslist = new string[5];
                        // User Id
                        paramslist[0] = ServiceCommon.ConvertStringToSQL(model.LoginName);
                        // UserName
                        paramslist[1] = ServiceCommon.ConvertStringToSQL(fileName);
                        // LoginName
                        paramslist[2] = ServiceCommon.ConvertStringToSQL(fileLocation);
                        // Password
                        //if (model.FileName == null || model.FileName.Trim() == "")
                        //{
                        //    paramslist[3] = ServiceCommon.ConvertStringToSQL(model.FileName);
                        //}
                        //else
                        //{
                        //    paramslist[3] = ServiceCommon.ConvertStringToSQL(model.FileName);
                        //}
                        paramslist[3] = ServiceCommon.ConvertStringToSQL(model.FileName);
                        // ICCardID
                        paramslist[4] = ServiceCommon.ConvertStringToSQL("0");

                        strSql = string.Format(strSql, paramslist);

                        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        {
                            ID = cmd.ExecuteScalar();
                        }

                        tran.Commit();

                        return Convert.ToInt32(ID);
                    }
                    catch (Exception ex)
                    {
                        if (tran.Connection != null)
                        {
                            tran.Rollback();
                        }
                        throw ex;
                    }
                    finally
                    {
                        tran.Dispose();
                        tran = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// if msg is a command or a SPL file
        /// </summary>
        /// <param name="recvBytes"></param>
        /// <param name="bytesCount"></param>
        /// <param name="fs"></param>
        /// <param name="taskID"></param>
        /// <returns></returns>
        private bool IsOnlyFirstPack(byte[] recvBytes, int bytesCount, out FileStream fs, out int taskID, out string filepath)
        {
            string strRecv = Encoding.Default.GetString(recvBytes, 0, 1023);
            fs = null;
            taskID = -1;
            filepath = "";
            // msg action
            if (IsMsg(strRecv))
                return true;

            //get user para
            PrintParaModel paraModel = extract.ExtractInfo(recvBytes);

            // Edited by Le Ning 2014.2.11 
            if (IsLoginNameEmpty(paraModel))
            {
                //Log.log("出现了一个没有用户名密码的文档");
            }

            string tempStr = paraModel.LoginName;
            if (tempStr.Length > 30)
            {
                tempStr = tempStr.Substring(0,30);

                paraModel.LoginName = tempStr;
                Log.log("Login Name length exceed 30! ");
            }
            //End


            //save to DB
            string disk = BuildDisk(paraModel);
            //string fileName = DateTime.Now.ToString("yyMMddHHmmss") + ".spl";
             DateTime dt = DateTime.Now;
            string fileName = dt.ToString("yyMMddHHmmss") +dt.Millisecond.ToString() +".spl";

            taskID = SaveToDB(fileName, disk, paraModel);

            string path = disk + fileName;
            filepath = path;
            fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(recvBytes, 0, bytesCount);

            //

            return false;
        }

        /// <summary>
        /// if data just a msg, handle it
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool IsMsg(string msg)
        {
            string[] array = msg.Split('@');
            // restart or not
            if (array[0].ToLower().Contains("restart"))
            {
                Log.log("Invoke restart thread!");
                Thread restartThread = new Thread(new ThreadStart(RestartService));
                restartThread.Start();
                return true;
            }
            else
                // send file
                if (array[0].Contains("sendData"))
                {
                    Log.log("Send Print Data! \r\n" + "MFPTaskID:" + array[1] + " \r\n" + "MFP IP Address:" + array[2] + " \r\n");

                    //string[] idArray = array[1].Split(',');
                    SocketSend sendInstance = new SocketSend();

                    //chen 20180611 for sequence print
                    //foreach (string str in idArray)
                    //{
                    //    sendInstance.Send(str, array[2]);
                    //}

                    sendInstance.SendIDS(array[1], array[2]);

                    return true;
                }

            //20190620 add for delete pdf
                //else
                //    // send file
                //    if (array[0].Contains("DelCopyPDF"))
                //    {
                //        Log.log("Delete Copy Pdf  \r\n" + "filename:" + array[1] + " \r\n");
                //        string location = array[1];
                //        try
                //        {
                //            if (System.IO.File.Exists(location))
                //            {
                //                    System.IO.File.Delete(location);
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            ;
                //        }
                //        return true;
                //    }
            //
            return false;
        }

        /// <summary>
        /// if login name is empty, cancel the transfer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool IsLoginNameEmpty(PrintParaModel model)
        {
            //Edited by Le Ning 2014.2.11
            if (model.IsLoginNameEmpty)
            {
                //Log.log("File Login Name is empty,transfer cancel!");
                Log.log("Find a printing document without login name!");
            }
            return model.IsLoginNameEmpty;
        }

        /// <summary>
        /// restart Windows Service
        /// </summary>
        private void RestartService()
        {
            try
            {
                System.ServiceProcess.ServiceController control = new ServiceController("SimpleEAFollowME");
                if (!control.Status.ToString().Equals("Stopped"))
                {
                    control.Stop();
                    control.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                control.Start();
                control.WaitForStatus(ServiceControllerStatus.Running);
                Log.log("Restart Service!");
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Log.log(ex.ToString());
            }
            catch (Exception exc)
            {
                Log.log(exc.ToString());
            }
        }

        /// <summary>
        /// not use now
        /// </summary>
        /// <param name="taskID"></param>
        private void ActiveDBRecord(int taskID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ServiceCommon.DBConnectionStrings))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {
                        string strSql;
                        //1. active
                        strSql = "UPDATE MFPPrintTask SET [State] ={0}";
                        strSql += "WHERE MFPPrintTaskID={1}";

                        string[] paramslist = new string[2];
                        // state active
                        paramslist[0] = ServiceCommon.ConvertIntToSQL("1");
                        // taskid
                        paramslist[1] = ServiceCommon.ConvertIntToSQL(taskID.ToString());

                        strSql = string.Format(strSql, paramslist);

                        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (tran.Connection != null)
                        {
                            tran.Rollback();
                        }
                        throw ex;
                    }
                    finally
                    {
                        tran.Dispose();
                        tran = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}