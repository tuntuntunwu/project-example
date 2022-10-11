using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace PrintCopyService
{
    public partial class PrintCopyService : ServiceBase
    {
        private Thread copyThread;
        RunCopyPrint copyPrint = new RunCopyPrint();

        public PrintCopyService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            copyThread = new Thread(new ThreadStart(copyPrint.TaskStart));
            copyThread.Start();

        }

        protected override void OnStop()
        {
            if (copyThread != null)
            {
                copyThread.Abort();
            }

        }
    }
}
