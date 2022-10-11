using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Data;
using System.Threading;

namespace FollowMEService
{
    class SocketSend
    {
        //public Mutex m_mutex = new Mutex();

        public SocketSend()
        {
        }

        private string GetFilePath(string key)
        {
            string sql = string.Format("select * from MFPPrintTask where MFPPrintTaskID = {0}", ServiceCommon.ConvertIntToSQL(key));
            DataTable table = ServiceCommon.ExecuteDataTable(sql);
            
            foreach (DataRow dr in table.Rows)
            {
                return dr["FileLocation"].ToString() + dr["DiskFileName"].ToString();
            }
            return "";
        }

        /// <summary>
        /// 
        /// start send thread
        /// </summary>
        /// <param name="key"></param>
        /// <param name="remoteIP"></param>
        public void Send(string key, string remoteIP)
        {
            IPAddress ip = IPAddress.Parse(remoteIP);

            //socket
            //Thread sendThread = new Thread(new ParameterizedThreadStart(StartConn));
            //sendThread.Start(new IpAndKeyModel(ip, key));

            //ftp
            Thread ftpThread = new Thread(new ParameterizedThreadStart(UploadFile));
            ftpThread.Start(new IpAndKeyModel(ip, key));

        }
        /// <summary>
        /// 
        /// start send thread
        /// </summary>
        /// <param name="key"></param>
        /// <param name="remoteIP"></param>
        public void SendIDS(string idstrs, string remoteIP)
        {
            IPAddress ip = IPAddress.Parse(remoteIP);

            Thread ftpThread = new Thread(new ParameterizedThreadStart(UploadFile2));
            ftpThread.Start(new IpAndKeyModel(ip, idstrs));

        }
        //public void SendArray(string keys, string remoteIP)
        //{
        //    IPAddress ip = IPAddress.Parse(remoteIP);

        //    Thread ftpThread = new Thread(new ParameterizedThreadStart(UploadFileS));
        //    ftpThread.Start(new IpAndKeyModel(ip, key));

        //}

        /// <summary>
        /// socket send method; Second choice, not stable
        /// </summary>
        /// <param name="o"></param>
        private void StartConn(Object o)
        {
            Log.log("Socekt Print");
            IpAndKeyModel model = (IpAndKeyModel)o;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.BeginConnect(new IPEndPoint(model.ip, 9100), new AsyncCallback(EndConn), new SocketAndKeyModel(socket, model));
        }

        /// <summary>
        /// socket call back method
        /// </summary>
        /// <param name="AR"></param>
        private void EndConn(IAsyncResult AR)
        {
            SocketAndKeyModel model = (SocketAndKeyModel)AR.AsyncState;
            IpAndKeyModel ipModel =model .model ;

            try
            {
                Log.log("Socket Print---EndConn");

                Socket socket = model.soc;
                socket.EndConnect(AR);

                string filename = GetFilePath(ipModel.key);
                Log.log("uploadfile " + filename + "start");

                //if (!File.Exists(GetFilePath(ipModel.key)))
                if (!File.Exists(filename))
                {
                    return;
                }

                // read file and send
                //using (FileStream fileSteam = new FileStream(GetFilePath(ipModel.key), FileMode.Open, FileAccess.Read))
                using (FileStream fileSteam = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    int readBytes = 0;
                    while (true)
                    {
                        // read
                        Byte[] fsSize = new Byte[1024];
                        readBytes = fileSteam.Read(fsSize, 0, fsSize.Length);
                        // if read end
                        if (readBytes <= 0)
                        {
                            fileSteam.Flush();
                            fileSteam.Close();
                            break;
                        }
                        //send
                        socket.Send(fsSize, readBytes, 0); //发送消息
                    }
                }
                // release
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
                model = null;

                Log.log("uploadfile " + filename + "end");
            }
            catch (Exception e)
            {
                Log.log(e.ToString());
            }
        }

        /// <summary>
        /// ftp upload file to MFP, first choice
        /// </summary>
        /// <param name="o"></param>
        public void UploadFile(Object o)
        {

            //m_mutex.WaitOne();

            Log.log("Ftp Print");

            //get para
            IpAndKeyModel model = (IpAndKeyModel)o;

            FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://" + model.ip.ToString() + "/" + DateTime.Now.ToString("yyMMddHHmmss") + ".spl");
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.UseBinary = true;
            req.Timeout = 10 * 1000;
            try
            {
                string filename = GetFilePath(model.key);
                Log.log("Socekt Print");

                Log.log("upload file" + filename + "start");
                //using (FileStream fs = new FileStream(GetFilePath(model.key), FileMode.Open, FileAccess.Read))
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (Stream stream = req.GetRequestStream())
                    {
                        int BufferLength = 2048;
                        byte[] b = new byte[BufferLength];
                        int i;
                        while ((i = fs.Read(b, 0, BufferLength)) > 0)
                        {
                            stream.Write(b, 0, i);
                        }
                    }
                }
                Log.log("upload file" + filename + "end");

            }
            catch (Exception ex)
            {
                Log.log("FTP Error:" + ex.ToString());
                Thread sendThread = new Thread(new ParameterizedThreadStart(StartConn));
                sendThread.Start(model);
            }
            finally
            {
                
            }
            //m_mutex.ReleaseMutex();
        }

        /// <summary>
        /// ftp upload file to MFP, first choice
        /// </summary>
        /// <param name="o"></param>
        public void UploadFile2(Object o)
        {

            //m_mutex.WaitOne();

            Log.log("Ftp Print");

            //get para
            IpAndKeyModel model = (IpAndKeyModel)o;

            //FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://" + model.ip.ToString() + "/" + DateTime.Now.ToString("yyMMddHHmmss") + ".spl");
            //req.Method = WebRequestMethods.Ftp.UploadFile;
            //req.UseBinary = true;
            //req.Timeout = 10 * 1000;



            string[] idArray = model.key.Split(',');

            foreach (string key in idArray)
            {
                FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://" + model.ip.ToString() + "/" + DateTime.Now.ToString("yyMMddHHmmss") + ".spl");
                req.Method = WebRequestMethods.Ftp.UploadFile;
                req.UseBinary = true;
                req.Timeout = 10 * 1000;

                try
                {
                    //string filename = GetFilePath(model.key);
                    string filename = GetFilePath(key);
                    Log.log("Socekt Print");

                    Log.log("upload file" + filename + "start");
                    //using (FileStream fs = new FileStream(GetFilePath(model.key), FileMode.Open, FileAccess.Read))
                    using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        using (Stream stream = req.GetRequestStream())
                        {
                            int BufferLength = 2048;
                            byte[] b = new byte[BufferLength];
                            int i;
                            while ((i = fs.Read(b, 0, BufferLength)) > 0)
                            {
                                stream.Write(b, 0, i);
                            }
                        }
                    }
                    Log.log("upload file" + filename + "end");


                }
                catch (Exception ex)
                {
                    Log.log("FTP Error:" + ex.ToString());
                    Thread sendThread = new Thread(new ParameterizedThreadStart(StartConn));
                    IpAndKeyModel model2 = new IpAndKeyModel(model.ip, key);
                    sendThread.Start(model);
                }
                finally
                {

                }

                System.Threading.Thread.Sleep(10);
            }
            //m_mutex.ReleaseMutex();
        }

    }
}
