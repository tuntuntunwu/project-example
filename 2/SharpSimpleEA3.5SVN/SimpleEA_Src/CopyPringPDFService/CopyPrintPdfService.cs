using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace CopyPringPDFService
{
    public partial class CopyPrintPdfService : ServiceBase
    {

        private Thread checkThread;
        RunCopyPrint copyPrint = new RunCopyPrint();

        public CopyPrintPdfService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("aaaa");
            checkThread = new Thread(new ThreadStart(copyPrint.TaskStart));
            checkThread.Start();
            Console.WriteLine("bbbbb");
        }

        protected override void OnStop()
        {
        }
    }
}
