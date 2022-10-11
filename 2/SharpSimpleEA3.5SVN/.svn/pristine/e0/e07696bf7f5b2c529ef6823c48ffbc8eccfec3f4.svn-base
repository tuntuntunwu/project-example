using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace FollowMEService
{
    public partial class SimpleEAService : ServiceBase
    {
        public SocketRecv rawRecv;
        // socket listening
        private Thread listenThread;
        // clear program
        private Thread clearThread;
        // log program
        private Thread logThread;

        public SimpleEAService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // socket listening program
            Log.log("start");
            rawRecv = new SocketRecv();
            listenThread = new Thread(new ThreadStart(rawRecv.CreatSocket));
            listenThread.Start();

            // clear program
            clearThread = new Thread(new ThreadStart(new CycleSweep().TaskStart));
            clearThread.Start();

            // log
            logThread = new Thread(new ThreadStart(new LogThread().Log));
            logThread.Start();
        }

        protected override void OnStop()
        {
            if (rawRecv != null)
                rawRecv.StopSocket();
            listenThread.Abort();
            Log.log("End Application Listening !");
            clearThread.Abort();
            Log.log("End Delete task service !");
        }

    }
}
