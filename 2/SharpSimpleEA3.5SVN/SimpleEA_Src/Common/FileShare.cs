using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SimpleEACommon
{
    public class FileShare
    {

        public FileShare() { }



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
    }
}
