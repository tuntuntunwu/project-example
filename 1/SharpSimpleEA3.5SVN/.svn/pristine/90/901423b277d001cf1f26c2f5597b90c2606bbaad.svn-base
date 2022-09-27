#region Copyright SHARP Corporation
//	Copyright (c) 2010 SHARP CORPORATION. All rights reserved.
//
//	SHARP Simple EA Application
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.DirectoryServices;
using SimpleEACommon;
using System.Net;
using System.Diagnostics;
using System.Xml;
namespace CustomAction
{
    [RunInstaller(true)]
    public partial class CustomAction : Installer
    {
        // Class Name.
        internal const string ClassName = "CustomAction";
        // IIs Virtual Dir
        internal const string IIsVirtualDir = "IIsWebVirtualDir";
        // The directry folder for the Web File.
        internal const string WebFolder = "SimpleEAWeb";
        // The Default AppFriendlyName Value for the Web Site.
        internal const string DefaultVirDir = "SimpleEA";
        // The DB Name for the Simple EA.
        internal const string DBName = "SimpleEA";
        // App_Data
        internal const string App_Data = "App_Data";
        // add by Wei Changye 2012.03.19
        // Follow ME
        internal const string SimpleEAFollowME = "SimpleEAFollowME";

        internal const string SimpleEAMFPCheck = "SimpleEAMFPCheck";
        
        //end 
        internal const string SimpleEAPrintCopy = "PrintCopySys";

        //add by Wei Changye 2012.05.23
        // web config cotrol
        internal const string webConfigPath = "web.config";
        //end

        // Update Message.
        internal const string CON_UPDATE_MSG = "The Database named 'SimpleEA' is exist.\nCompletely remove it, please press Yes.\nSave the data and update it, please press No.\nStop the install process, please press Cancel.";
        // License File Lost.
        internal const string CON_FILELOST_MSG = "The license file is lost.\nPlease check it on the {0}.";
        // Key Name
        internal const string KEY_FILE = "OSA.DIRECT.LIC";
        // The Default Server Name
        internal const string DefaultServerName = "(local)\\SQLEXPRESS";
        // Server Name
        public static string ServerName = string.Empty;

        public CustomAction()
        {
            InitializeComponent();
            Common.installflg = true;
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {



            // Execute inherited Install
            base.Install(stateSaver);
            string FunctionName = "Install";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            try
            {
                // ServerName"
                ServerName = Context.Parameters["ServerName"];
                CustomLog.RecordLog("1", CustomLog.Level.DEBUG);
                // add by  Wei Changye 2012.03.30
                ServerName = ServerName.Replace("\\\\", "\\");
                CustomLog.RecordLog("2", CustomLog.Level.DEBUG);
                if (string.IsNullOrEmpty(ServerName))
                {
                    // 2011.03.22 Update By SES Jijianxiong ST
                    // Bug Management Sheet_SimpleEA_110321.xls No.4
                    // ServerName = DefaultServerName;
                    object objServerName = stateSaver["ServerName"];
                    CustomLog.RecordLog("3", CustomLog.Level.DEBUG);

                    if (objServerName == null)
                    {
                        ServerName = DefaultServerName;
                        CustomLog.RecordLog("4", CustomLog.Level.DEBUG);
                    }
                    else
                    {
                        ServerName = objServerName.ToString();
                        CustomLog.RecordLog("5", CustomLog.Level.DEBUG);
                    }
                    // 2011.03.22 Update By SES Jijianxiong ST
                }
                else
                {
                    stateSaver.Add("ServerName", ServerName);
                    CustomLog.RecordLog("6", CustomLog.Level.DEBUG);
                }
                SqlCommon.ConnectionString = ServerName;
                // --> Log
                CustomLog.RecordLog(string.Format("The [ServerName] is {0}.", ServerName), CustomLog.Level.DEBUG);

                // User Dir
                // 2011.03.23 Update By SES Jijianxiong ST
                //string VirDirName = this.Context.Parameters["VirDirName"];
                //if (string.IsNullOrEmpty(VirDirName))
                //{
                //    // 2011.03.22 Update By SES Jijianxiong ST
                //    // Bug Management Sheet_SimpleEA_110321.xls No.4
                //    // VirDirName = DefaultVirDir;
                //    object objVirtualDir = stateSaver["VirtualDir"];

                //    // --> Log
                //    CustomLog.RecordLog(string.Format("Get the [Virtual Directory] From stateSaver: {0}.", VirDirName), CustomLog.Level.DEBUG);

                //    if (objVirtualDir == null)
                //    {
                //        VirDirName = DefaultVirDir;
                //    }
                //    else
                //    {
                //        VirDirName = objVirtualDir.ToString();
                //    }
                //    // 2011.03.22 Update By SES Jijianxiong ST

                //}
                //else
                //{
                //    stateSaver.Add("VirtualDir", VirDirName);
                //    // --> Log
                //    CustomLog.RecordLog(string.Format("Save the [Virtual Directory]: {0}.", VirDirName), CustomLog.Level.DEBUG);

                //}

                string VirDirName = DefaultVirDir;
                // 2011.03.23 Update By SES Jijianxiong ED

                // --> Log
                CustomLog.RecordLog(string.Format("The [Virtual Directory] is {0}.", VirDirName), CustomLog.Level.DEBUG);

                // Get Assembly
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

                // Dll Location
                string dirBinLocation = Path.GetDirectoryName(asm.Location);

                // Location
                string dirLocation = dirBinLocation.Substring(0, (dirBinLocation.IndexOf("\\bin")));
                // --> Log
                CustomLog.RecordLog(string.Format("Get the [Install DirectoryName] is {0}.", dirLocation), CustomLog.Level.DEBUG);

                //Get UserInfo.xml Path.
                string WebPath = Path.Combine(dirLocation, WebFolder);
                string UserInfoXMLPath = Path.Combine(WebPath, App_Data);
                //add By WeiChangye 2012.03.19
                string SimpleEAFollowMEPath = Path.Combine(dirBinLocation, SimpleEAFollowME);
                //end 

                string SimpleEAMFPCheckPath = Path.Combine(dirBinLocation, SimpleEAMFPCheck);

                //add By WeiChangye 2012.03.19
                string SimpleEAPrintCopyPath = Path.Combine(dirLocation, SimpleEAPrintCopy);
                //end 

                // add By WeiChangye 2012.05.23
                string strWebConfigPath = Path.Combine(WebPath, webConfigPath);
                //end

                // Check IIS
                if (!IsIISExist())
                {
                    throw new Exception("The IIS Exist Check is failed.");
                }

                // --> Log
                CustomLog.RecordLog("The IIS Exist Check is OK.", CustomLog.Level.DEBUG);

                // Check SQL
                if (!SqlCommon.InitConnection())
                {
                    throw new Exception("The Sql Server Exist Check is failed.");
                }
                // --> Log
                CustomLog.RecordLog("The Sql Server Exist Check is OK.", CustomLog.Level.DEBUG);

                bool IsUpdate = false;
                // Check Old Simple EA DB.
                if (SqlCommon.IsSpecailDbExist(DBName))
                {
                    // --> Log
                    CustomLog.RecordLog("The Old Version is Exist.", CustomLog.Level.DEBUG);
                    DialogResult retVal = Common.Confirm(CON_UPDATE_MSG);
                    if (retVal == DialogResult.No)
                    {
                        IsUpdate = true;
                    }
                    else if (retVal == DialogResult.Cancel)
                    {
                        throw new Exception("User canceled the install process on the DB Install.");
                    }
                }

                if (IsUpdate)
                {
                    // --> Log
                    CustomLog.RecordLog("Update UserInfo.xml File.", CustomLog.Level.DEBUG);
                    try
                    {
                        // Update userInfo.xml File.
                        SqlCommon.UpdateUserXml(WebPath);
                    }
                    catch (Exception)
                    {
                        
                        throw new System.UnauthorizedAccessException("XML File Update Error.");
                    }
                    // --> Log
                    CustomLog.RecordLog("Update UserInfo.xml File is finished.", CustomLog.Level.DEBUG);
                }
                else
                {
                    // --> Log
                    CustomLog.RecordLog("Install the Data Base for the Ver.1.0.", CustomLog.Level.DEBUG);
                    // Install the Data Base for the Ver.1.0;
                    SqlCommon.InstallDatabase();
                    // --> Log
                    CustomLog.RecordLog("Install the Data Base for the Ver.1.0 is finished.", CustomLog.Level.DEBUG);
                }

                // --> Log
                CustomLog.RecordLog("Install the Data Base for the Ver.1.1.", CustomLog.Level.DEBUG);
                // Update the Data Base for the Ver.1.1;
                SqlCommon.UpdateDatabase();
                // --> Log
                CustomLog.RecordLog("Install the Data Base for the Ver.1.1 is finished.", CustomLog.Level.DEBUG);

                // Specifies security descriptor to the UserXml.
                CustomFileSecurity.DirAccessControl(UserInfoXMLPath,
                    FileSystemRights.FullControl,
                    AccessControlType.Allow);

                // add by WeiChangye 2012.03.19
                // add Follow ME authorize
                CustomFileSecurity.DirAccessControl(SimpleEAFollowMEPath,
                    FileSystemRights.FullControl,
                    AccessControlType.Allow);
                //end

                // add by WeiChangye 2012.05.23
                CustomFileSecurity.FileAccessControl(strWebConfigPath,
                    FileSystemRights.FullControl,
                    AccessControlType.Allow);
                //end
                
                // 2011.03.23 Delete By SES Jijianxiong ST
                //// Specifies security descriptor to the LIC.
                //string fullfilepath = Path.Combine(WebPath, KEY_FILE);
                //if (File.Exists(fullfilepath))
                //{
                //    CustomFileSecurity.FileAccessControl(fullfilepath,
                //        FileSystemRights.FullControl,
                //        AccessControlType.Allow);
                //}
                //else
                //{
                //    Common.Alert(string.Format(CON_FILELOST_MSG, fullfilepath));

                //}
                // 2011.03.23 Delete By SES Jijianxiong ED

                try
                {
                    // Config Web Config
                    string SimpleEAConnectionString = ConfigWeb(WebPath);

                    //chen add
                    //ConfigWeb(WebPath)
                    ModifyFollowMEDBConfig(SimpleEAFollowMEPath, SimpleEAConnectionString);
                    ModifyMFPCheckDBConfig(SimpleEAMFPCheckPath, SimpleEAConnectionString);
                    ModifyPrintCopyDBConfig(SimpleEAPrintCopyPath, SimpleEAConnectionString);
                    //

                    // Config config.ini file
                    ConfigINI(dirBinLocation, WebPath);

                    // CreateWebSite
                    CreateWebSite(VirDirName, dirLocation);
                    //create SageCopy WebSize
                    string copyVirDirName = "SafeCopy";
                    this.CreateSafeCopyWebSite(copyVirDirName, dirLocation);
                    // 2011.03.23 Update By SES Jijianxiong ED
                    CreateSafeCopyWebSite(copyVirDirName, dirLocation);
                    // Modify the Shortcut
                    string ServerBindings = GetServerBindings();
                    CreateShortcutFile(ServerBindings, VirDirName, dirBinLocation);

                }
                catch (Exception ex)
                {
                    Common.Error(Common.MSG_ACCESS);
                    CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                }
                //stateSaver.Add("VirtualDir", VirDirName);
                //// 2011.03.22 Add By SES Jijianxiong ST
                //// Bug Management Sheet_SimpleEA_110321.xls No.4
                //stateSaver.Add("ServerName", ServerName);
                //// 2011.03.22 Add By SES Jijianxiong ED

                //setup windows service
                Process proc = null;
                proc = new Process();
                string servicePath = SimpleEAFollowMEPath + @"\InstallFollowME.bat";
                proc.StartInfo.FileName = servicePath;
                //proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();

                //2012.07.09 add by Wei Changye open txt
                string strTxtPath = Path.Combine(dirBinLocation, "ReadMe.txt");
                System.Diagnostics.Process.Start("notepad.exe", strTxtPath);
                //end add



                CustomLog.RecordLog("Dat file has been installed : "+servicePath, CustomLog.Level.DEBUG);
            }
            catch (System.UnauthorizedAccessException ex)
            {

                Common.Error(Common.MSG_ACCESS);
                CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                throw ex;
            }
            catch (Exception ex)
            {
                Common.Error(ex.Message);

                CustomLog.RecordLog(ex.GetType().ToString() + ":" + ex.ToString(), CustomLog.Level.ERROR);
                CustomLog.RecordLog(ex.GetType().ToString() + ":" + ex.Message, CustomLog.Level.ERROR);
                throw ex;
            }

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

        }


        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);

            try
            {
                // Add by Wei Changye 2012.03.23 uninstall windows service
                // Get Assembly
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                // Dll Location
                string dirBinLocation = Path.GetDirectoryName(asm.Location);
                // Location
                string dirLocation = dirBinLocation.Substring(0, (dirBinLocation.IndexOf("\\bin")));
                // Web Location
                string WebPath = Path.Combine(dirLocation, WebFolder);
                // FollowME folder
                string SimpleEAFollowMEPath = Path.Combine(dirBinLocation, SimpleEAFollowME);
                Process proc = null;
                proc = new Process();
                string servicePath = SimpleEAFollowMEPath + @"\UninstallFollowME.bat";
                proc.StartInfo.FileName = servicePath;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                CustomLog.RecordLog("Dat file has been Uninstalled: " + servicePath, CustomLog.Level.DEBUG);


                string FunctionName = "Uninstall";

                // --> Log
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

                try
                {
                    string VirDirName = DefaultVirDir;
                    DirectoryEntry rootfolder = new DirectoryEntry("IIS://localhost/W3SVC/1/ROOT");

                    DirectoryEntry entry = FindSubEntry(VirDirName, rootfolder);
                    if (entry != null)
                    {
                        rootfolder.Children.Remove(entry);
                        rootfolder.CommitChanges();
                    }
                }
                catch (Exception ex)
                {
                    Common.Error(ex.Message);

                    CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                }
                // --> Log
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

            }
            catch (Exception)
            {
                ;                
            }
        }

        #region Function
        #region "ConfigINI"
        /// <summary>
        /// ConfigINI
        /// </summary>
        /// <param name="dirBinLocation"></param>
        /// <Date>2011.01.20</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private void ConfigINI(string dirBinLocation, string WebPath)
        {
            string FunctionName = "ConfigWeb";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            try
            {
                string configpath = Path.Combine(dirBinLocation, "config.ini");
                // --> Log
                CustomLog.RecordLog(string.Format("INI Config File Path is {0}", configpath), CustomLog.Level.DEBUG);

                INIClass ini = new INIClass(configpath);

                if (!ini.isExist())
                {
                    throw new Exception("INI Config File is not exist.");
                }

                // Set Data Source
                string strValue = string.Format("Data Source={0};Initial Catalog=SimpleEA;Persist Security Info=True;User ID=admin;Password=admin;pooling=false;",
                            ServerName);

                ini.SetValue("SimpleEA", "SimpleEAConnectionString", strValue);

                // Set the Install Path
                ini.SetValue("SimpleEA", "InstallPath", WebPath);


            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        
        }
        #endregion


        #region "Config Web Config"
        /// <summary>
        /// ConfigWeb
        /// </summary>
        /// <param name="path"></param>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        internal string ConfigWeb(string path)
        {
            string SimpleEAConnectionString = "";

            string FunctionName = "ConfigWeb";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);


            string configpath = Path.Combine(path, "web.config");
            // --> Log
            CustomLog.RecordLog(string.Format("Web Config File Path is {0}", configpath), CustomLog.Level.DEBUG);

            FileInfo webconfig = new FileInfo(configpath);

            System.Xml.XmlDocument xmlwebconfig = new System.Xml.XmlDocument();
            xmlwebconfig.Load(webconfig.FullName);
            bool existflg = false;
            foreach (System.Xml.XmlNode node in xmlwebconfig["configuration"]["connectionStrings"])
            {
                if (node.Name == "add")
                {
                    if (node.Attributes.GetNamedItem("name").Value == "SimpleEAConnectionString")
                    {
                        string strValue = string.Format("Data Source={0};Initial Catalog=SimpleEA;Persist Security Info=True;User ID=admin;Password=admin;pooling=false;",
            ServerName);
                        SimpleEAConnectionString = strValue;
                        node.Attributes.GetNamedItem("connectionString").Value = strValue;
                        existflg = true;
                        break;
                    }
                }

            }

            if (!existflg)
            {
                throw new Exception("SimpleEAConnectionString is not exist in the web.Config file.");
            }

            xmlwebconfig.Save(webconfig.FullName);

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

            return SimpleEAConnectionString;
        }


        public void ModifyFollowMEDBConfig(string folder, string DBConnectionStrings)
        {
            //ModifyFollowME("SimpleEAConnectionString", DBConnectionStrings);
            string key = "SimpleEAConnectionString";
            string strValue = DBConnectionStrings;
            try
            {
                // if is run in vs
                string fileLocation = Path.Combine(folder, "FollowMEService.exe.config");

                string XPath = "/configuration/appSettings/add[@key='?']";

                XmlDocument domWebConfig = new XmlDocument();

                domWebConfig.Load((fileLocation));
                XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
                if (addKey != null)
                {
                    addKey.Attributes["value"].InnerText = strValue;
                    domWebConfig.Save((fileLocation));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ModifyMFPCheckDBConfig(string folder, string DBConnectionStrings)
        {
            //ModifyFollowME("SimpleEAConnectionString", DBConnectionStrings);
            string key = "SimpleEAConnectionString";
            string strValue = DBConnectionStrings;
            try
            {
                // if is run in vs
                string fileLocation = Path.Combine(folder, "MFPCheckService.exe.config");

                string XPath = "/configuration/appSettings/add[@key='?']";

                XmlDocument domWebConfig = new XmlDocument();

                domWebConfig.Load((fileLocation));
                XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
                if (addKey != null)
                {
                    addKey.Attributes["value"].InnerText = strValue;
                    domWebConfig.Save((fileLocation));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModifyPrintCopyDBConfig(string folder, string DBConnectionStrings)
        {
            string key = "SimpleEAConnectionString";
            string strValue = DBConnectionStrings;

            try
            {
                string fileLocation = Path.Combine(folder, "PrintCopySys.exe.config");

                string XPath = "/configuration/appSettings/add[@key='?']";

                XmlDocument domWebConfig = new XmlDocument();

                domWebConfig.Load((fileLocation));
                XmlNode addKey = domWebConfig.SelectSingleNode((XPath.Replace("?", key)));
                if (addKey != null)
                {
                    addKey.Attributes["value"].InnerText = strValue;
                    domWebConfig.Save((fileLocation));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region "Find Sub Entry."
        /// <summary>
        /// Find Sub Entry
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rootentry"></param>
        /// <returns></returns>
        /// <Date>2011.01.13</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        protected DirectoryEntry FindSubEntry(string name, DirectoryEntry rootentry)
        {
            string FunctionName = "FindSubEntry";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            DirectoryEntry ret = null;
            foreach (DirectoryEntry entry in rootentry.Children)
            {
                if (entry.SchemaClassName == IIsVirtualDir && entry.Name.ToLower() == name.ToLower())
                {
                    ret = entry;
                }
            }

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            return ret;
        }
        #endregion

        #region "Is IIS Exist in localhost PC"
        /// <summary>
        /// Is IIS Exist in localhost PC
        /// </summary>
        /// <returns></returns>
        /// <Date>2011.01.13</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        protected bool IsIISExist()
        {
            string FunctionName = "IsIISExist";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            bool IsExist = true;

            int intIISVersion;
            IsExist = IIServicesVersion.GetIISServerType(out intIISVersion);
            // --> Log
            CustomLog.RecordLog(string.Format("Internet Information Services Version is {0}.", intIISVersion), CustomLog.Level.DEBUG);

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

            return IsExist;
        }
        #endregion

        #region "Create WebSites on the IIS"
        /// <summary>
        /// Create WebSites on the IIS
        /// </summary>
        /// <param name="_VirDirName"></param>
        /// <Date>2011.01.13</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        protected void CreateWebSite(string _VirDirName, string _dirLocation)
        {
            string FunctionName = "CreateWebSite";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            // Create IIS Virtual Dir 
            using (DirectoryEntry rootfolder = new DirectoryEntry("IIS://localhost/W3SVC/1/ROOT"))
            {


                using (DirectoryEntry entry = FindSubEntry(_VirDirName, rootfolder))
                {
                    if (entry != null)
                    {
                        // --> Log
                        CustomLog.RecordLog("Entry is not null", CustomLog.Level.DEBUG);
                        rootfolder.Children.Remove(entry);
                        rootfolder.CommitChanges();
                        // --> Log
                        CustomLog.RecordLog(string.Format("Deleted the Exist [Virtual Directory]:{0};", _VirDirName), CustomLog.Level.DEBUG);
                    }
                }

                // --> Log
                CustomLog.RecordLog("Create the new Virtual", CustomLog.Level.DEBUG);
                using (DirectoryEntry newVirDir = rootfolder.Children.Add(_VirDirName, IIsVirtualDir))
                {
                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual Begin", CustomLog.Level.DEBUG);
                    // newVirDir.Path = dirLocation;
                    newVirDir.Properties["DefaultDoc"][0] = "Default.aspx";

                    // Configuring Access Permissions.
                    newVirDir.Properties["AccessRead"][0] = true;
                    newVirDir.Properties["AccessScript"][0] = true;
                    newVirDir.Properties["AccessWrite"][0] = false;

                    newVirDir.Properties["AuthNTLM"][0] = false;
                    newVirDir.Properties["AuthAnonymous"][0] = true;
                    newVirDir.Properties["AuthBasic"][0] = false;
                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual AppCreate", CustomLog.Level.DEBUG);
                    newVirDir.Invoke("AppCreate", true);

                    string path = System.IO.Path.Combine(_dirLocation, WebFolder);
                    newVirDir.Properties["Path"][0] = path;
                    newVirDir.Properties["AppFriendlyName"][0] = _VirDirName;

                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual AppFriendlyName", CustomLog.Level.DEBUG);
                    newVirDir.CommitChanges();
                    rootfolder.CommitChanges();
                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual CommitChanges", CustomLog.Level.DEBUG);
                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual End", CustomLog.Level.DEBUG);
                }
            }

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        }
        #endregion


        #region "Create SafeCopy WebSites on the IIS"
        /// <summary>
        /// Create SageCopy WebSites on the IIS
        /// </summary>
        /// <param name="_VirDirName"></param>
        /// <Date>2018.02.07</Date>
        /// <Author>chen</Author>
        /// <Version>0.01</Version>
        protected void CreateSafeCopyWebSite(string _VirDirName, string _dirLocation)
        {
            string FunctionName = "CreateWebSite";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            // Create IIS Virtual Dir 
            using (DirectoryEntry rootfolder = new DirectoryEntry("IIS://localhost/W3SVC/1/ROOT"))
            {


                using (DirectoryEntry entry = FindSubEntry(_VirDirName, rootfolder))
                {
                    if (entry != null)
                    {
                        // --> Log
                        CustomLog.RecordLog("Entry is not null", CustomLog.Level.DEBUG);
                        rootfolder.Children.Remove(entry);
                        rootfolder.CommitChanges();
                        // --> Log
                        CustomLog.RecordLog(string.Format("Deleted the Exist [Virtual Directory]:{0};", _VirDirName), CustomLog.Level.DEBUG);
                    }
                }

                // --> Log
                CustomLog.RecordLog("Create the new Virtual", CustomLog.Level.DEBUG);
                using (DirectoryEntry newVirDir = rootfolder.Children.Add(_VirDirName, IIsVirtualDir))
                {
                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual Begin", CustomLog.Level.DEBUG);
                    // newVirDir.Path = dirLocation;
                    newVirDir.Properties["DefaultDoc"][0] = "Default.aspx";

                    // Configuring Access Permissions.
                    newVirDir.Properties["AccessRead"][0] = true;
                    newVirDir.Properties["AccessScript"][0] = true;
                    newVirDir.Properties["AccessWrite"][0] = false;

                    newVirDir.Properties["AuthNTLM"][0] = false;
                    newVirDir.Properties["AuthAnonymous"][0] = true;
                    newVirDir.Properties["AuthBasic"][0] = false;
                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual AppCreate", CustomLog.Level.DEBUG);
                    newVirDir.Invoke("AppCreate", true);

                    //string path = System.IO.Path.Combine(_dirLocation, WebFolder);
                    string copyWebFolder = "SafeCopy";
                    string path = System.IO.Path.Combine(_dirLocation, copyWebFolder);
                    newVirDir.Properties["Path"][0] = path;
                    newVirDir.Properties["AppFriendlyName"][0] = _VirDirName;

                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual AppFriendlyName", CustomLog.Level.DEBUG);
                    newVirDir.CommitChanges();
                    rootfolder.CommitChanges();
                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual CommitChanges", CustomLog.Level.DEBUG);
                    // --> Log
                    CustomLog.RecordLog("Create the new Virtual End", CustomLog.Level.DEBUG);
                }
            }

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        }
        #endregion


        #region GetServerBindings
        /// <summary>
        /// GetServerBindings
        /// </summary>
        /// <returns></returns>
        private static string  GetServerBindings() {
            string FunctionName = "GetServerBindings";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            string strServerBindings = string.Empty;

            // Create IIS Virtual Dir 
            using (DirectoryEntry rootfolder = new DirectoryEntry("IIS://localhost/W3SVC/1/ROOT"))
            {


                object objServerBindings = rootfolder.Properties["ServerBindings"].Value;

                if (objServerBindings == null)
                {
                    strServerBindings = "localhost";
                }
                else
                {
                    strServerBindings = objServerBindings.ToString();
                }



            }

            // --> Log
            CustomLog.RecordLog(string.Format("the ServerBindings is {0}", strServerBindings), CustomLog.Level.DEBUG);

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            return strServerBindings;

        }
        #endregion

        #region "Is SQL Exist in localhost PC."
        /// <summary>
        /// Is SQL Exist in localhost PC.
        /// </summary>
        /// <returns></returns>
        /// <Date>2011.01.13</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        protected bool IsSQLExist()
        {
            string FunctionName = "IsIISExist";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            bool IsExist = true;

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

            return IsExist;
        }
        #endregion

        #region CreateShortcutFile
        /// <summary>
        /// CreateShortcutFile
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="URL"></param>
        /// <param name="SpecialFolder"></param>
        public static void CreateShortcutFile(string IISIP, string ServerName, string SpecialFolder)
        {
            SpecialFolder = Path.Combine(SpecialFolder, "Simple EA_V2.0.htm");
            string oldfile = string.Empty;
            using (System.IO.StreamReader read = new System.IO.StreamReader(SpecialFolder))
            {
                oldfile = read.ReadToEnd();
            }

            oldfile.Replace("localhost", IISIP);
            oldfile.Replace("SimpleEA", ServerName);
            // Create shortcut file, based on Title
            using (System.IO.StreamWriter objWriter = new System.IO.StreamWriter(SpecialFolder, false))
            {
                objWriter.Write(oldfile);
            }
        }

        #endregion

        #endregion

    }
}