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

namespace CopyPringPDFService
{
    class RunCopyPrint
    {
      

        public void TaskStart()
        {
            //------86400000 millisecond = 1 day
            //------3600000 millisecond = 1 hour
            //------600000 millisecond = 1 minitue
            System.Timers.Timer myTimer = new System.Timers.Timer(Convert.ToDouble(ServiceCommon.GetAppSettingString("RunCycle")));
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(RunCopyPrintPDF2);
            myTimer.Enabled = true;
        }
        public void RunCopyPrintPDF2(object source, System.Timers.ElapsedEventArgs e)
        {
        }
        //public string Send()
        public void RunCopyPrintPDF(object source, System.Timers.ElapsedEventArgs e)
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
            string dstPath = ServiceCommon.GetAppSettingString("CopyPDFFilepath");
            string dstfile = dstPath + bean.CopyFile + ".pdf";
            Boolean flg = FileShare.netCopy2(ip, origfile,  dstfile);
            return flg;
        }

    }
}
