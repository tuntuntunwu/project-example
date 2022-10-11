using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
//using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace MFPCheckService
{
    public partial class MFPCheckService : ServiceBase
    {
        // socket listening
        private Thread checkThread;
        RunCheck check = new RunCheck();

        public MFPCheckService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            checkThread = new Thread(new ThreadStart(check.TaskStart));
            checkThread.Start();

        }

        protected override void OnStop()
        {
            if (checkThread != null)
                checkThread.Abort();
        }
    }
}
