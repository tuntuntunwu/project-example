using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;


namespace FollowMEService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            //this.Committed += new InstallEventHandler(ProjectInstaller_Committed);
            // start up after setup
            this.AfterInstall += new InstallEventHandler(ProjectInstaller_Committed);
        }


        private void ProjectInstaller_Committed(object sender, InstallEventArgs e)
        {
            System.ServiceProcess.ServiceController controller = new System.ServiceProcess.ServiceController("SimpleEAFollowME"); //参数为服务的名字
            controller.Start();
        }  
    }
}
