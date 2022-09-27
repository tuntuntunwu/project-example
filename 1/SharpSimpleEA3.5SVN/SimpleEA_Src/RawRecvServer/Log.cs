using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace FollowMEService
{
    public class Log
    {
        public static void log(string msg)
        {
            try
            {
                lock (ServiceCommon.logQueue)
                {
                    ServiceCommon.logQueue.Enqueue(msg);
                }
            }
            catch (Exception e)
            {
                Thread.Sleep(1000);
            }
            finally
            {
            }
        }
    }

    public class LogThread
    {
        private string logPath = ServiceCommon.RawRecvLogPath + "RawRecvLog.txt";

        public void Log()
        {
            while (true)
            {
                if (ServiceCommon.logQueue.Count > 0)
                {
                    lock (ServiceCommon.logQueue)
                    {
                        string logPath = ServiceCommon.RawRecvLogPath + "RawRecvLog.txt";
                        string strLog = ServiceCommon.logQueue.Dequeue();

                        Write(strLog, logPath);
                    }
                }
                else
                    Thread.Sleep(1000);
            }
        }

        public void Write(string msg, string path)
        {
            TextWriter w = null;
            try
            {
                if (ServiceCommon.InitRawRecvLog())
                {

                    if (!File.Exists(path))
                    {
                        // Create a file to write to.
                        w = File.CreateText(logPath);
                        w.WriteLine("Application Log for Raw Recv file");
                        w.WriteLine("----------------------------");
                    }
                    else
                    {
                        // Append text to the existing file.
                        w = File.AppendText(path);
                    }

                    // Write log text with time-date stamped
                    w.Write("\r\nLog Entry : ");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", msg);
                    w.WriteLine("-------------------------------");
                }
            }
            catch (Exception e)
            {
                Thread.Sleep(100);
            }
            finally
            {
                if (w != null)
                {
                    // Update the underlying file.
                    w.Flush();
                    w.Close();
                    w.Dispose();
                }
            }
        }
    }
}
