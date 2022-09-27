using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
//using System.Text;
using System.Xml;
using System.IO;
using System.Timers;
using System.Collections;
using System.Threading;
namespace PrintCopyService
{
    class RunCopyPrint
    {
      

        public void TaskStart()
        {
            //------86400000 millisecond = 1 day
            //------3600000 millisecond = 1 hour
            //------600000 millisecond = 1 minitue
            //System.Timers.Timer myTimer = new System.Timers.Timer(Convert.ToDouble(ServiceCommon.GetAppSettingString("RunCycle")));
            //myTimer.Elapsed += new System.Timers.ElapsedEventHandler(RunCopyPrintPDF);
            //myTimer.Enabled = true;
            int timesped = Convert.ToInt32(ServiceCommon.GetAppSettingString("RunCycle"));
            while (true)
            {
                RunCopyPrintPDF();
                Thread.Sleep(timesped);
            }
        }
     
        //public string Send()
//        public void RunCopyPrintPDF(object source, System.Timers.ElapsedEventArgs e)
        public void RunCopyPrintPDF()
        {

            DalPrintCopy  dal = new DalPrintCopy();

            List<PrintCopyEntry> beanlist = dal.SearchRecord();

            foreach (PrintCopyEntry bean in beanlist)
            {
                if (bean.Finished == 0 && bean.CopyTimes < 5)
                {
                    Boolean flg = copyPrintPdfFile(bean);
                    if (flg)
                    {
                        bean.Finished = 1;
                    }
                    bean.CopyTimes = bean.CopyTimes + 1;
                    dal.Update(bean);
                }
            }
        }
        public Boolean copyPrintPdfFile(PrintCopyEntry bean)
        {

            string ip = bean.IpAddress;
            string origfile = bean.OrigFile + ".pdf"; ;
            //string dstPath = ServiceCommon.GetAppSettingString("CopyPDFFilepath");
            DalCopyConfig dalCopy = new DalCopyConfig();
            CopyConfigEntry copyEntry = dalCopy.GetCopyConfigInfo();
            string dstPath = copyEntry.CopyFileLocation;
            if( dstPath.Substring(dstPath.Length -1) != @"\" )
            {
                dstPath = dstPath + @"\";
            }
            string dstfile = dstPath + bean.CopyFile + ".pdf";
            if (!FileProcess.FolderExist(dstPath))
            {
                FileProcess.FolderCreate(dstPath);
            }

            if (File.Exists(dstfile))
            {
                return true;
            }
            Boolean flg = NetFileProcess.netCopy2(ip, origfile, dstfile);
            return flg;
        }

    }
}
