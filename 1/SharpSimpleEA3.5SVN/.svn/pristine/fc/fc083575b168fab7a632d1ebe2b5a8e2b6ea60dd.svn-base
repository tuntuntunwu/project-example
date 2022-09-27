using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Net;
namespace FileCommon
{
    public class NetFileProcess
    {

        public NetFileProcess() { }



        public static bool connectState(string path)

        {

            return connectState(path,"","");

        }



        public static bool connectState(string path,string userName,string passWord)

         {

            bool Flag = false;

            Process proc = new Process();

            try

            {

                proc.StartInfo.FileName = "cmd.exe";

                proc.StartInfo.UseShellExecute = false;

                proc.StartInfo.RedirectStandardInput = true;

                proc.StartInfo.RedirectStandardOutput=true;

                proc.StartInfo.RedirectStandardError=true;

                proc.StartInfo.CreateNoWindow=true;

                proc.Start();

                string dosLine = @"net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";

                proc.StandardInput.WriteLine(dosLine);

                proc.StandardInput.WriteLine("exit");

                while (!proc.HasExited)

                {

                    proc.WaitForExit(1000);

                }

                string errormsg = proc.StandardError.ReadToEnd();

                proc.StandardError.Close();

                if (string.IsNullOrEmpty(errormsg))

                {

                    Flag = true;

                }

                else

                {

                    throw new Exception(errormsg);

                }

            }

            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                proc.Close();

                proc.Dispose();

            }

            return Flag;

        }
        //网络拷贝用

        public static bool Ping(string remoteHost)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"ping -n 1 " + remoteHost;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(500);
                }
                string pingResult = proc.StandardOutput.ReadToEnd();
                if (pingResult.IndexOf("0%") != -1)
                {
                    Flag = true;
                }
                proc.StandardOutput.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                try
                {
                    proc.Close();
                    proc.Dispose();
                }
                catch
                {
                }
            }
            return Flag;
        }
        public static bool Connect(string remoteHost, string userName, string passWord)
        {
            if (!Ping(remoteHost))
            {
                return false;
            }
            bool Flag = true;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                //string dosLine = @"ping -n 1 " + remoteHost;
                string dosLine = @"net use \\202.120.87.215\public\main  Z: admin /user:admin";
                //  net use \\\\202.120.87.215\public\main O: admin  /user:admin
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(500);
                }
                string pingResult = proc.StandardOutput.ReadToEnd();
                if (pingResult.IndexOf("0%") != -1)
                {
                    Flag = true;
                }
                proc.StandardOutput.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                try
                {
                    proc.Close();
                    proc.Dispose();
                }
                catch
                {
                }
            }
            return Flag;
            
        }
        public static bool Connect2(string remoteHost, string userName, string passWord)
        {
            if (!Ping(remoteHost))
            {
                return false;
            }
            bool Flag = true;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"net use \\" + remoteHost + " " + passWord + " " + " /user:" + userName + ">NUL";
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                {
                    Flag = false;
                }
                proc.StandardError.Close();
            }
            catch (Exception ex)
            {
                Flag = false;
            }
            finally
            {
                try
                {
                    proc.Close();
                    proc.Dispose();
                }
                catch
                {
                }
            }
            return Flag;
        }

        public static Boolean NetConnect(string ip, string user, string pwd)
        {
            if (Ping(ip))
            {
                if (Connect(ip, user, pwd))
                {
                    return true;
                }
            }
            return false;
        }
        public static Boolean NetCopy(string srcpath, string dstpath)
        {
            if (File.Exists(srcpath))
            {
                File.Copy(srcpath, dstpath, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CreateZ(string remoteHost,string localPath, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                //string dosLine = @"net use \\202.120.87.215\public\main  Z: amin /user:admin";
                string dosLine = @"net use " + localPath + " " + remoteHost +  " " + passWord + " " + " /user:" + userName;

                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(500);
                }
                string pingResult = proc.StandardOutput.ReadToEnd();
                if (pingResult.IndexOf("0%") != -1)
                {
                    Flag = true;
                }
                proc.StandardOutput.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                try
                {
                    proc.Close();
                    proc.Dispose();
                }
                catch
                {
                }
            }
            return Flag;
        }
        public static bool deleteZ(string localPath)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                //string dosLine = @"net use \\202.120.87.215\public\main  Z: amin /user:admin";
                string dosLine = @"net use "  + localPath + " /delete ";

                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (proc.HasExited == false)
                {
                    proc.WaitForExit(500);
                }
                string pingResult = proc.StandardOutput.ReadToEnd();
                if (pingResult.IndexOf("0%") != -1)
                {
                    Flag = true;
                }
                proc.StandardOutput.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                try
                {
                    proc.Close();
                    proc.Dispose();
                }
                catch
                {
                }
            }
            return Flag;
        }


        public static Boolean netCopy2(string ip, string origfile,  string dstfile)
        {

            string localpath = @"Z:";
            string serverPath = @"\\" + ip + @"\public\main" ;//@"\\202.120.87.215\public\main";
            string loginUser = "admin";
            string loginPassword = "admin";

            string src_filename = @"Z:\" + origfile;
            string dst_filename = dstfile;
            Boolean flg = false;
            for (int i = 0; i < 500; i++)
            {

                int status = NetworkConnection.Connect(serverPath, localpath, loginUser, loginPassword);
                flg = false;
                if (status == (int)ERROR_ID.ERROR_SUCCESS)
                {
                    if (File.Exists(src_filename))
                    {
                        File.Copy(src_filename, dst_filename, true);
                        flg = true;
                    }
                }

                // 断开连接
                NetworkConnection.Disconnect(localpath);

                if (flg)
                {
                    break;
                }
                System.Threading.Thread.Sleep(1000);
            }
            return flg;
        }

        public static Boolean netCopy3(string mfp_ip, string origfile, string dstfile)
        {
            Boolean ret = false;
            //string dst_folder = dst_path;
            string src_folder = @"\\" + mfp_ip + @"\public\main\";
            string src_filename = src_folder + origfile ;
            string dst_filename = dstfile;

            long fileSize=0;


            Boolean connect_flg = NetFileProcess.NetConnect(mfp_ip, "admin", "admin");
            //留底复制过程时间过长 异常处理
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    FileInfo fi = new FileInfo(src_filename);
                    if (fi != null && fi.Exists)
                    {
                        if (fileSize != fi.Length)
                        {
                            fileSize = fi.Length;
                        }
                        else
                        {
                            ret = NetFileProcess.NetCopy(src_filename, dst_filename);
                            break;
                        }
                    }
                    System.Threading.Thread.Sleep(1000 * 10);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return ret;
        }
    }
}
