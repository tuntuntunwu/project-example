#region Copyright SHARP Corporation
//	Copyright (c) 2011 SHARP CORPORATION. All rights reserved.
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SesMiddleware;
using System.Xml;
using SimpleEACommon;
using System.Data.SqlClient;

namespace UserDataBackUp
{
    public partial class Main : Form
    {
        private INIClass ini;
        private const string MSG_MIDDLEWARE_ERROR = "请确认SimpleEA安装目录下的App_Data文件夹asp.net用户是否有写操作权限。";
        private const string CON_MESSAGE_ININOTEXIST = "配置文件不存在，请确认配置文件是否在程序所在目录。";
        private const string CON_MESSAGE_INIERROR = "配置文件内容不正确，请联系管理员。";

        private const string CON_MESSAGE_FINISH = "操作正常终了。";
        //2011.01.24 Add By SES zhoudmiao  Update ST
        private const string CON_MESSAGE_FINISH_INPUT = "操作正常终了。\r\n请到web端重新确认用户信息！";
        private const string CON_MESSAGE_INPUTDATA = "导入文件内容不正确，请确认。";
        //2011.01.24 Add By SES zhoumiao  Update ED

        private const string CON_MESSAGE_CANCEL = "用户取消了操作。";
        //2011.01.20 Update By SES zhoumiao  Update ST
        //private const string CON_MESSAGE_DELETE_CONFIRM = "是否覆盖数据库中已存在的用户信息？\r\n 请注意操作不可逆。";
        private const string CON_MESSAGE_DELETE_CONFIRM = "是否覆盖数据库中已存在的用户信息？\r\n"
            + "（是：覆盖，否：不覆盖，取消：不做处理）\r\n 请注意操作不可逆。";
        //2011.01.20 Update By SES zhoumiao  Update ED
        private const string CON_MESSAGE_SETUP = "应用程序正在运行中，请不要重复启动！";
        private const string CON_MESSAGE_AGAIN = "必须选择SimpleEA安装目录下的App_Data文件夹下的UserInfo.XML文件";

        private string DBConnectionStrings = "";
        // 2011.01.21 Add By SES zhoumiao Update ST
        //SimpleEA Install Path
        private string SimpleEAInstallPath = "";
        //initial UserInfo Path
        private string newUserInfoFilePath = "";
        private const string CON_MESSAGE_XMLNOTEXIST = "UserInfo.XML文件不存在，请确认UserInfo.XML文件是否在程序所在目录。";
        // 2011.01.21 Add By SES zhoumiao Update ED
        private string SafeFileName = "";
        // App_Data
        internal const string App_Data = "App_Data";
        // UserInfo File Name
        internal const string newFileName = "UserInfo.XML";
        //2011.01.25 Add By SES zhoumiao  Update ST
        // Class Name.
        internal const string ClassName = "UserDataBackUp";
        //2011.01.25 Add By SES zhoumiao  Update ED

        public Main()
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "UserDataBackUp Load";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED

            InitializeComponent();
            bool Exist = false;
            System.Diagnostics.Process CIP = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.Process[] CIPR = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process p in CIPR)
            {
                if (p.ProcessName == CIP.ProcessName && p.Id != CIP.Id)
                {
                    Exist = true;
                }
            }
            if (Exist)
            {
                // 2011.01.21 Update By SES Jijianxiong ST
                // MessageBox.Show(CON_MESSAGE_SETUP, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.Error(CON_MESSAGE_SETUP);
                // 2011.01.21 Update By SES Jijianxiong ED                
                // 2011.01.25 Update By SES zhoumiao Update ST
                //throw new Exception(CON_MESSAGE_SETUP);
                // --> Log
                CustomLog.RecordLog(CON_MESSAGE_SETUP, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES zhoumiao Update ED
            }

            // Init Check  Sql Connection.
            // Ini File Path
            string filepath = Path.Combine(Application.StartupPath, "config.ini");
            ini = new INIClass(filepath);
            // Check ini file is exist?
            if (!ini.isExist())
            {
                // 2011.01.21 Update By SES Jijianxiong ST
                // MessageBox.Show(CON_MESSAGE_ININOTEXIST, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.Error(CON_MESSAGE_ININOTEXIST);
                // 2011.01.21 Update By SES Jijianxiong ED                
                // 2011.01.25 Update By SES zhoumiao Update ST
                //throw new Exception(CON_MESSAGE_ININOTEXIST);
                // --> Log
                CustomLog.RecordLog(CON_MESSAGE_ININOTEXIST, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES zhoumiao Update ED
            }
            // 2011.01.25 Add By SES zhoumiao Update ST
            // --> Log
            CustomLog.RecordLog(string.Format("The config.ini Path is {0}.", filepath), CustomLog.Level.DEBUG);
            // 2011.01.25 Add By SES zhoumiao Update ED          

            // Get Connection String
            DBConnectionStrings = ini.GetValue("SimpleEA", "SimpleEAConnectionString");

            if (string.IsNullOrEmpty(DBConnectionStrings))
            {
                // 2011.01.21 Update By SES Jijianxiong ST
                // MessageBox.Show(CON_MESSAGE_INIERROR, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.Error(CON_MESSAGE_INIERROR);
                // 2011.01.21 Update By SES Jijianxiong ED       
                // 2011.01.25 Update By SES zhoumiao Update ST
                //throw new Exception(CON_MESSAGE_INIERROR);
                // --> Log
                CustomLog.RecordLog(CON_MESSAGE_INIERROR, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES zhoumiao Update ED
            }
            // 2011.01.25 Add By SES zhoumiao Update ST
            // --> Log
            CustomLog.RecordLog(string.Format("The SimpleEAConnectionString is {0}.", DBConnectionStrings), CustomLog.Level.DEBUG);
            // 2011.01.25 Add By SES zhoumiao Update ED
            // 2011.01.21 Add By SES zhoumiao Update ST
            // Get SimpleEA Install Path
            SimpleEAInstallPath = ini.GetValue("SimpleEA", "InstallPath");
            string newAppDataPath = Path.Combine(SimpleEAInstallPath, App_Data);
            // Get Initial UserInfo File Path
            newUserInfoFilePath = Path.Combine(newAppDataPath, newFileName);
            if (!File.Exists(newUserInfoFilePath))
            {
                SafeFileName = string.Empty;
            }
            else
            {
                SafeFileName = newFileName;
            }
            // 2011.01.25 Add By SES zhoumiao Update ST
            // --> Log
            CustomLog.RecordLog(string.Format("The  SimpleEA Install Path is {0}.", SimpleEAInstallPath), CustomLog.Level.DEBUG);
            CustomLog.RecordLog(string.Format("The Initial UserInfo.XML File Path is {0}.", newUserInfoFilePath), CustomLog.Level.DEBUG);
            // 2011.01.25 Add By SES zhoumiao Update ED
            //txtInstall.Text = SimpleEAInstallPath;
            //txtFilepath.Text = newUserInfoFilePath;
            // 2011.01.21 Add By SES zhoumiao Update ED   

            //2011.01.20 Add By SES zhoumiao  Update ST
            btnOutput.Select();
            //2011.01.20 Add By SES zhoumiao  Update ED

            // Connection to DB            
            //2011.01.20 Update By SES zhoumiao  Update ST
            //string strSql = "SELECT '1' AS Name FROM [UserInfo]";
            string strSql = "SELECT '1' AS Name FROM [SimpleEA].[dbo].[UserInfo]";
            //2011.01.20 Update By SES zhoumiao  Update ED
            string strName = "";
            try
            {
                using (SqlDataReader reader = Common.ExecuteReader(strSql, this.DBConnectionStrings))
                {
                    while (reader.Read())
                    {

                        strName = (string)reader["Name"];
                    }
                }
            }
            catch (Exception ex)
            {
                // 2011.01.25 Update By SES zhoumiao Update ST
                //// 2011.01.21 Update By SES Jijianxiong ST
                //// MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Common.Error(ex.Message);
                //// 2011.01.21 Update By SES Jijianxiong ED                          
                // throw ex;
                Common.Error(Common.MSG_SYSTEMERROR);
                // --> Log
                CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES zhoumiao Update ED
            }
            // 2011.01.21 Delete By SES zhoumiao Update ST
            //// 2011.01.20 Add By SES Jijianxiong Update ST
            //lblInput.Visible = false;
            //btnInput.Enabled = false;
            //// 2011.01.20 Add By SES Jijianxiong Update ED
            // 2011.01.21 Delete By SES zhoumiao Update ED

            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED

        }

        #region "btnFileInput_Click"
        /// <summary>
        /// btnFileInput_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private void btnFileInput_Click(object sender, EventArgs e)
        {
            btnClose.Enabled = false;
            btnInput.Enabled = false;
            btnOutput.Enabled = false;
            //btnFileInput.Enabled = false;
            this.Enabled = false;
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "btnFileInput_Click";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            // Open File.
            try
            {
                // Config the OpenFileDialog
                //2011.01.20 Add By SES zhoumiao  Update ST
                openInputFile.Reset();
                //2011.01.20 Add By SES zhoumiao  Update ED
                //title
                openInputFile.Title = "打开文件";
                //Type
                openInputFile.Filter = "XML文件(*.XML)|*.XML";
                openInputFile.RestoreDirectory = true;
                if (openInputFile.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //the UserInfo.xml File Path
                    string path = openInputFile.FileName;
                    if (UserInfo_Check(openInputFile.SafeFileName))
                    {
                        //UserInfo.xml name
                        SafeFileName = openInputFile.SafeFileName;
                        //txtFilepath.Text = openInputFile.FileName;
                        //2011.01.20 Add By SES zhoumiao  Update ST
                        //txtInstall.Text = openInputFile.FileName.Replace("\\App_Data\\UserInfo.XML", "");
                        //2011.01.20 Add By SES zhoumiao  Update ED
                        // 2011.01.25 Add By SES zhoumiao Update ST
                        // --> Log
                        CustomLog.RecordLog(string.Format("The Input UserInfo.XML path is {0}.", openInputFile.FileName), CustomLog.Level.DEBUG);
                        // 2011.01.25 Add By SES zhoumiao Update ED
                        // 2011.01.20 Add By SES Jijianxiong Update ST
                        lblInput.Visible = true;
                        btnInput.Enabled = true;
                        // 2011.01.20 Add By SES Jijianxiong Update ED

                        //2011.01.20 Add By SES zhoumiao  Update ST                    
                        btnInput.Select();
                        //2011.01.20 Add By SES zhoumiao  Update ED


                    }
                    else
                    {
                        //2011.01.21 Delete By SES zhoumiao  Update ST  
                        //// 2011.01.20 Add By SES Jijianxiong Update ST
                        //lblInput.Visible = false;
                        //btnInput.Enabled = false;
                        //// 2011.01.20 Add By SES Jijianxiong Update ED
                        //2011.01.21 Delete By SES zhoumiao  Update ED  

                        SafeFileName = openInputFile.SafeFileName;

                        //txtFilepath.Text = string.Empty;
                        //2011.01.20 Add By SES zhoumiao  Update ST
                        //txtInstall.Text = string.Empty;
                        //btnFileInput.Select();
                        //2011.01.20 Add By SES zhoumiao  Update ED
                        // 2011.01.21 Update By SES Jijianxiong ED
                        ////2011.01.20 Update By SES zhoumiao  Update ST
                        ////MessageBox.Show(CON_MESSAGE_AGAIN, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show(CON_MESSAGE_AGAIN, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ////2011.01.20 Update By SES zhoumiao  Update ED
                        Common.Error(CON_MESSAGE_AGAIN);
                        // 2011.01.21 Update By SES Jijianxiong ED

                        // 2011.01.25 Add By SES zhoumiao Update ST                       
                        // --> Log
                        CustomLog.RecordLog(CON_MESSAGE_AGAIN, CustomLog.Level.ERROR);
                        // 2011.01.25 Add By SES zhoumiao Update ED

                    }
                }
            }
            catch (Exception ex)
            {
                // 2011.01.25 Update By SES zhoumiao Update ST
                //// 2011.01.21 Update By SES Jijianxiong ST
                //// MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Common.Error(ex.Message);
                //// 2011.01.21 Update By SES Jijianxiong ED
                // throw ex;
                Common.Error(Common.MSG_SYSTEMERROR);
                // --> Log
                CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES zhoumiao Update ED
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnClose.Enabled = true;
                //2011.01.21 Add By SES zhoumiao  Update ST                    
                btnInput.Enabled = true;
                //2011.01.21 Add By SES zhoumiao  Update ED
                // 2011.01.20 Delete By SES Jijianxiong Update ST
                // btnInput.Enabled = true;
                // 2011.01.20 Delete By SES Jijianxiong Update ED
                btnOutput.Enabled = true;
                //btnFileInput.Enabled = true;
                this.Enabled = true;

            }
            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED
        }
        #endregion

        #region "btnOutput_Click"
        /// <summary>
        /// btnOutput_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private void btnOutput_Click(object sender, EventArgs e)
        {
            btnClose.Enabled = false;
            btnInput.Enabled = false;
            btnOutput.Enabled = false;
            //btnFileInput.Enabled = false;
            this.Enabled = false;
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "btnOutput_Click";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            // Save File.
            try
            {

                if (savefile())
                {
                    // 2011.01.21 Update By SES Jijianxiong ST
                    // MessageBox.Show(CON_MESSAGE_FINISH, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Common.Alert(CON_MESSAGE_FINISH);
                    // 2011.01.21 Update By SES Jijianxiong ED
                    // 2011.01.25 Add By SES zhoumiao Update ST                       
                    // --> Log
                    CustomLog.RecordLog(CON_MESSAGE_FINISH, CustomLog.Level.INFO);
                    // 2011.01.25 Add By SES zhoumiao Update ED

                }
                else
                {
                    // 2011.01.21 Update By SES Jijianxiong ST
                    // MessageBox.Show(CON_MESSAGE_CANCEL, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Common.Alert(CON_MESSAGE_CANCEL);
                    // 2011.01.21 Update By SES Jijianxiong ED
                    // 2011.01.25 Add By SES zhoumiao Update ST                       
                    // --> Log
                    CustomLog.RecordLog(CON_MESSAGE_CANCEL, CustomLog.Level.INFO);
                    // 2011.01.25 Add By SES zhoumiao Update ED
                }
            }
            catch (Exception ex)
            {
                // 2011.01.25 Update  By SES zhoumiao Update ST
                //// 2011.01.21 Update By SES Jijianxiong ST
                //// MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Common.Error(ex.Message);
                //// 2011.01.21 Update By SES Jijianxiong ED
                Common.Error(Common.MSG_SYSTEMERROR);
                // --> Log
                CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update  By SES zhoumiao Update ED
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnClose.Enabled = true;
                // 2011.01.20 Delete By SES Jijianxiong ST
                //btnInput.Enabled = true;
                // 2011.01.20 Delete By SES Jijianxiong ED
                //2011.01.21 Add By SES zhoumiao  Update ST                    
                btnInput.Enabled = true;
                //2011.01.21 Add By SES zhoumiao  Update ED
                btnOutput.Enabled = true;
                //btnFileInput.Enabled = true;
                this.Enabled = true;
                //2011.01.20 Add By SES zhoumiao  Update ST                    
                btnClose.Select();
                //2011.01.20 Add By SES zhoumiao  Update ED                

            }
            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED

        }
        #endregion

        #region "btnClose_Click"
        /// <summary>
        /// Close Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private void btnClose_Click(object sender, EventArgs e)
        {
            //2011.01.25 Update By SES zhoumiao  Update ST
            //this.Close();
            string FunctionName = "btnClose_Click";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            this.Close();
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Update By SES zhoumiao  Update ED
        }

        #endregion

        #region "btnInput_Click"
        /// <summary>
        /// btnInput_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private void btnInput_Click(object sender, EventArgs e)
        {
            btnClose.Enabled = false;
            btnInput.Enabled = false;
            btnOutput.Enabled = false;
            //btnFileInput.Enabled = false;
            this.Enabled = false;
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "btnImport_Click";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            //Check the path C:\Documents and Settings\<User>\Local Settings\Temp
            string UserInfoXMLPath = Environment.GetEnvironmentVariable("TEMP");
            // Open File.
            try
            {

                /** //chen 20160120 update start 
                if (!UserInfo_Check(SafeFileName))
                {
                    //2011.01.20 Add By SES zhoumiao  Update ST                
                    if (string.IsNullOrEmpty(SafeFileName))
                    {
                        // 2011.01.21 Update By SES Jijianxiong ST
                        // MessageBox.Show(CON_MESSAGE_XMLNOTEXIST, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Common.Error(CON_MESSAGE_XMLNOTEXIST);
                        // 2011.01.21 Update By SES Jijianxiong ED
                        // 2011.01.25 Add By SES zhoumiao Update ST                       
                        // --> Log
                        CustomLog.RecordLog(CON_MESSAGE_XMLNOTEXIST, CustomLog.Level.ERROR);
                        // 2011.01.25 Add By SES zhoumiao Update ED
                        btnFileInput.Select();
                        return;
                    }
                    //2011.01.20 Add By SES zhoumiao  Update ED

                    //2011.01.20 Add By SES zhoumiao  Update ST                    
                    btnFileInput.Select();
                    //2011.01.20 Add By SES zhoumiao  Update ED
                    // 2011.01.21 Update By SES Jijianxiong ST
                    ////2011.01.20 Update By SES zhoumiao  Update ST
                    ////MessageBox.Show(CON_MESSAGE_AGAIN, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show(CON_MESSAGE_AGAIN, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ////2011.01.20 Update By SES zhoumiao  Update ED                   
                    Common.Error(CON_MESSAGE_AGAIN);
                    // 2011.01.21 Update By SES Jijianxiong ED
                    // 2011.01.25 Add By SES zhoumiao Update ST                       
                    // --> Log
                    CustomLog.RecordLog(CON_MESSAGE_AGAIN, CustomLog.Level.ERROR);
                    // 2011.01.25 Add By SES zhoumiao Update ED
                }
                else
                 * */
                //chen 20160120 update end 
                {
                    if (openfile())
                    {
                        //Update the UserInfo.xml
                        //chen 20160120 update start
                        //UpdateUserXml(UserInfoXMLPath);
                        //chen 20160120 update end

                        // 2011.01.27 Delete By SES zhoumiao ST
                        //// 2011.01.24 Add By SES zhoumiao ST
                        //GaRes_Check();
                        //// 2011.01.24 Add By SES zhoumiao ED
                        // 2011.01.27 Delete By SES zhoumiao ED
                        // 2011.01.21 Update By SES Jijianxiong ST
                        // MessageBox.Show(CON_MESSAGE_FINISH, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //2011.01.20 Update By SES zhoumiao  Update ST 
                        //Common.Alert(CON_MESSAGE_FINISH);
                        Common.Alert(CON_MESSAGE_FINISH_INPUT);
                        //2011.01.20 Update By SES zhoumiao  Update ED 
                        // 2011.01.21 Update By SES Jijianxiong ED
                        // 2011.01.25 Add By SES zhoumiao Update ST                       
                        // --> Log
                        CustomLog.RecordLog(CON_MESSAGE_FINISH_INPUT, CustomLog.Level.INFO);
                        // 2011.01.25 Add By SES zhoumiao Update ED
                    }
                    else
                    {
                        // 2011.01.21 Update By SES Jijianxiong ST
                        // MessageBox.Show(CON_MESSAGE_CANCEL, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Common.Alert(CON_MESSAGE_CANCEL);
                        // 2011.01.21 Update By SES Jijianxiong ED
                        // 2011.01.25 Add By SES zhoumiao Update ST                       
                        // --> Log
                        CustomLog.RecordLog(CON_MESSAGE_CANCEL, CustomLog.Level.INFO);
                        // 2011.01.25 Add By SES zhoumiao Update ED

                    }
                    //2011.01.20 Add By SES zhoumiao  Update ST                    
                    btnClose.Select();
                    //2011.01.20 Add By SES zhou    miao  Update ED
                    //2011.01.21 Add By SES zhoumiao  Update ST                    
                    //txtFilepath.Text = newUserInfoFilePath;
                    //txtInstall.Text = SimpleEAInstallPath;
                    if (!File.Exists(newUserInfoFilePath))
                    {
                        SafeFileName = string.Empty;
                    }
                    else
                    {
                        SafeFileName = newFileName;
                    }
                    //2011.01.21 Add By SES zhoumiao  Update ED

                }
            }
            // 2011.01.21 Add by SES Jijianxiong ST
            catch (System.UnauthorizedAccessException ex)
            {

                Common.Error(Common.MSG_ACCESS);
                // 2011.01.25 Update By SES zhoumiao Update ST
                // throw ex;
                CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES zhoumiao Update ED
            }
            // 2011.01.21 Add by SES Jijianxiong ED

            catch (Exception ex)
            {
                // 2011.01.25 Update By SES zhoumiao Update ST
                ////2011.01.24 Update By SES zhoumiao  Update ST                 
                ////// 2011.01.21 Update by SES Jijianxiong ST
                ////// MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ////Common.Error(ex.Message);
                ////// 2011.01.21 Update by SES Jijianxiong ED
                ////throw ex;
                //Common.Error(CON_MESSAGE_INPUTDATA);

                //// throw ex;
                ////2011.01.24 Update By SES zhoumiao  Update ED
                Common.Error(Common.MSG_SYSTEMERROR);
                // --> Log
                CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES zhoumiao Update ED

            }
            finally
            {

                btnInput.Focus();
                this.Cursor = Cursors.Default;
                btnClose.Enabled = true;
                //2011.01.20 Update By SES zhoumiao  Update ST                    
                //btnInput.Enabled = true;                
                //2011.01.21 Update By SES zhoumiao  Update ST  
                //btnInput.Enabled = false;
                //txtFilepath.Text = string.Empty;
                //txtInstall.Text = string.Empty;
                btnInput.Enabled = true;
                //2011.01.21 Delete By SES zhoumiao  Update ED 
                //SafeFileName = String.Empty;
                //2011.01.21 Delete By SES zhoumiao  Update ED 
                //2011.01.21 Update By SES zhoumiao  Update ED  
                //2011.01.20 Update By SES zhoumiao  Update ED                
                btnOutput.Enabled = true;
                //btnFileInput.Enabled = true;
                this.Enabled = true;

            }
            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED
        }
        #endregion

        #region "Open File"
        /// <summary>
        /// Open File
        /// </summary>
        /// <returns></returns>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private bool openfile()
        {
            // 2011.01.27 Add by SES zhoumiao ST

            // Add File Security
            //chen 20160120 update start 
            //CustomFileSecurity.FileAccessControl(txtFilepath.Text.Trim(), System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
            //chen 20160120 update end
            // 2011.01.27 Add by SES zhoumiao ED

            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "openfile";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            //1.0 get UserInfo information 
            String strSql = "SELECT ID,UserName,LoginName,Password,ICCardID,PinCode, Email, GroupID,RestrictionID,ComeFrom, CreateTime,UpdateTime"
            + "  FROM  [SimpleEA].[dbo].[UserInfo]";
            DataTable UserInfotable = Common.ExecuteDataTable(strSql, DBConnectionStrings);


            //2014.6.26 Update By pupeng
            //2.0 get PriceMaster information 
            strSql = "SELECT PriceID,PriceNM,PriceCalMode,CreateTime,UpdateTime"
            + "  FROM  [SimpleEA].[dbo].[PriceMaster]";
            DataTable PriceMastertable = Common.ExecuteDataTable(strSql, DBConnectionStrings);
            //2.0 get GroupInfo information 
            strSql = "SELECT PriceDetailID,PriceID,PaperTypeID,JobID,PaperPrice,GrayPrice,ColorPrice,CreateTime,UpdateTime"
            + "  FROM  [SimpleEA].[dbo].[PriceDetail]";
            DataTable PriceDetailtable = Common.ExecuteDataTable(strSql, DBConnectionStrings);
            //2014.6.26 Update By pupeng




            //2011.01.25 Update By SES zhoumiao  Update ED
            //2.0 get GroupInfo information 
            strSql = "SELECT ID,GroupName,RestrictionID,CreateTime,UpdateTime"
            + "  FROM  [SimpleEA].[dbo].[GroupInfo]";
            //2011.01.20 Update By SES zhoumiao  Update ED
            DataTable GroupInfotable = Common.ExecuteDataTable(strSql, DBConnectionStrings);
            //2011.01.25 Update By SES zhoumiao  Update ED
            //3.0 get RestrictionInfo information 
            strSql = "SELECT ID,RestrictionName,AllQuota, ColorQuota, OverLimit, CreateTime,UpdateTime"
                //2011.01.20 Update By SES zhoumiao  Update ST
                // + "  FROM [RestrictionInfo]";
            + "  FROM  [SimpleEA].[dbo].[RestrictionInfo] ";
            //2011.01.20 Update By SES zhoumiao  Update ED
            //3.1 Get Date
            // Detail Date            
            //2011.01.25 Update By SES zhoumiao  Update ST
            //DataTable RestrictionInfotable = ExecuteDataTable(strSql);
            DataTable RestrictionInfotable = Common.ExecuteDataTable(strSql, DBConnectionStrings);
            //2011.01.25 Update By SES zhoumiao  Update ED
            //4.0 get RestrictionInformation information 
            strSql = "SELECT RestrictionID,JobId,FunctionId,Status,LimitNum"
                //2011.01.20 Update By SES zhoumiao  Update ST
                // + "  FROM [RestrictionInformation]";
            + "  FROM  [SimpleEA].[dbo].[RestrictionInformation]";
            //2011.01.20 Update By SES zhoumiao  Update ED
            //4.1 Get Date
            // Detail Date            
            //2011.01.25 Update By SES zhoumiao  Update ST
            //DataTable RestrictionInformationtable = ExecuteDataTable(strSql);
            DataTable RestrictionInformationtable = Common.ExecuteDataTable(strSql, DBConnectionStrings);
            //2011.01.25 Update By SES zhoumiao  Update ED
            // Config the OpenFileDialog




            openInputFile.Reset();
            openInputFile.Title = "打开文件";
            openInputFile.Filter = "CSV文件(*.csv)|*.csv";

            openInputFile.RestoreDirectory = true;

            DialogResult reDelete = DialogResult.Yes;

            // delete date  is true  
            // 2011.01.21 Update By SES jijianxiong ST
            ////2011.01.20 Update By SES zhoumiao  Update ST
            ////reDelete = MessageBox.Show(CON_MESSAGE_DELETE_CONFIRM, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //reDelete = MessageBox.Show(CON_MESSAGE_DELETE_CONFIRM, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            ////2011.01.20 Update By SES zhoumiao  Update ED
            reDelete = Common.Confirm(CON_MESSAGE_DELETE_CONFIRM);
            // 2011.01.21 Update By SES jijianxiong ED

            //2011.01.20 Add By SES zhoumiao  Update ST           
            if (reDelete.Equals(DialogResult.Cancel))
            {
                //2011.01.25 Add By SES zhoumiao  Update ST
                // --> Log
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED
                return false;
            }
            //2011.01.20 Add By SES zhoumiao  Update ED
            if (openInputFile.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                //the select File Name path
                string path = openInputFile.FileName;
                if (!File.Exists(path))
                {
                    //2011.01.25 Add By SES zhoumiao  Update ST
                    // --> Log
                    CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                    //2011.01.25 Add By SES zhoumiao  Update ED
                    return false;
                }
                // delete date  is true
                if (reDelete.Equals(DialogResult.Yes))
                {
                    CoverDate(path, UserInfotable, PriceMastertable, PriceDetailtable,GroupInfotable, RestrictionInfotable, RestrictionInformationtable);
                }
                else
                {
                    NotCoverDate(path, UserInfotable, PriceMastertable,PriceDetailtable, GroupInfotable, RestrictionInfotable, RestrictionInformationtable);
                }
                //2011.01.25 Add By SES zhoumiao  Update ST
                // --> Log
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED
                return true;
            }

            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED
            return false;

        }
        #endregion

        #region "Cover the Users Information Data"
        /// <summary>
        ///  Cover the Users Information Data
        /// </summary>
        /// <param name="path"></param>
        /// <param name="User"></param>
        /// <param name="Group"></param>
        /// <param name="Res"></param>
        /// <param name="Resformation"></param>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private void CoverDate(String path, DataTable User, DataTable PriceM, DataTable PriceD, DataTable Group, DataTable Res, DataTable Resformation)
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "CoverDate";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED

            string sql = "";
            //2011.01.20 Delete By SES zhoumiao  Update ST
            // int MaxId = 0;
            //2011.01.20 Delete By SES zhoumiao  Update ED

            // Add By SES Jijianxiong ST
            ResUpdate res = new ResUpdate(true);
            // Add By SES Jijianxiong ST
            //2011.01.20 Add By SES zhoumiao  Update ST
            StringBuilder strbuilderComm = new StringBuilder("");
            //2011.01.20 Add By SES zhoumiao  Update ED
           
            using (StreamReader streamReader = new StreamReader(path, Encoding.Default, false))
            {
                String tablename = "";
                //string strInsertComm = "";
                
                while (streamReader.Peek() >= 0)
                {
                    string[] Strsql = streamReader.ReadLine().Split(',');
                    //check the table  name is [UserInfo] 
                    if (Strsql[0].Equals("------------[UserInfo]------------"))
                    {
                        tablename = "UserInfo";
                        //2011.01.20 Delete By SES zhoumiao  Update ST                        
                        ////UserInfo Max ID
                        //MaxId = numbleAdd(tablename);
                        //2011.01.20 Delete By SES zhoumiao  Update ED
                        continue;
                    }
                    if (Strsql[0].Equals("------------[PriceMaster]------------"))
                    {
                        tablename = "PriceMaster";
                        continue;
                    }
                    if (Strsql[0].Equals("------------[PriceDetail]------------"))
                    {
                        tablename = "PriceDetail";
                        continue;
                    }
                    //check the table  name is [GroupInfo] 
                    if (Strsql[0].Equals("------------[GroupInfo]------------"))
                    {
                        tablename = "GroupInfo";
                        //2011.01.20 Delete By SES zhoumiao  Update ST                        
                        ////GroupInfo Max ID
                        //MaxId = numbleAdd(tablename);
                        //2011.01.20 Delete By SES zhoumiao  Update ED
                        continue;
                    }
                    //check the table  name is [RestrictionInfo] 
                    if (Strsql[0].Equals("------------[RestrictionInfo]------------"))
                    {
                        tablename = "RestrictionInfo";
                        //2011.01.20 Delete By SES zhoumiao  Update ST                        
                        ////RestrictionInfo Max ID
                        //MaxId = numbleAdd(tablename);
                        //2011.01.20 Delete By SES zhoumiao  Update ED
                        continue;
                    }
                    //check the table  name is [RestrictionInformation] 
                    if (Strsql[0].Equals("------------[RestrictionInformation]------------"))
                    {
                        tablename = "RestrictionInformation";
                        continue;
                    }

                    switch (tablename)
                    {
                        // check each table's information
                        case "UserInfo":
                            {
                                if (Name_Check(User, Strsql[2], "LoginName"))
                                {
                                    //Cover the [UserInfo] table Data
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    // strInsertComm = "UPDATE UserInfo SET UserName = '" + Strsql[1];
                                    //1,chen,chen,1111,0005854227,123456,ygchen@cc.ecnu.edu.cn,2,0,2014-7-14 13:53:40,2014-11-3 22:00:08
                                    strbuilderComm.Append("UPDATE [SimpleEA].[dbo].[UserInfo] SET UserName = '" + Strsql[1]);
                                    //2011.01.20 Update By SES zhoumiao  Update ED                                    
                                    strbuilderComm.Append("',Password = '" + Strsql[3]);
                                    strbuilderComm.Append("',ICCardID = '" + Strsql[4]);
                                    strbuilderComm.Append("',PinCode = '" + Strsql[5]);
                                    strbuilderComm.Append("',Email = '" + Strsql[6]);
                                    strbuilderComm.Append( "',GroupID = " + Strsql[7]);
                                    strbuilderComm.Append( ",RestrictionID = " + Strsql[8]);
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm += "',CreateTime = '" + Strsql[7];
                                    //strInsertComm += "',UpdateTime = '" + Strsql[8];
                                    //strInsertComm += "'  WHERE LoginName= '" + Strsql[2] + "'";
                                    strbuilderComm.Append( ",UpdateTime = getdate() ");
                                    strbuilderComm.Append("  WHERE LoginName= '" + Strsql[2] + "'");
                                    //2011.01.20 Update By SES zhoumiao  Update ED
                                    strbuilderComm.Append("\n");

                                }
                                else
                                {
                                    //Insert the [UserInfo] table Data
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm = "INSERT INTO UserInfo(ID,UserName,LoginName,Password,";
                                    strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[UserInfo](ID,UserName,LoginName,Password,");
                                    //2011.01.20 Update By SES zhoumiao  Update ED
                                    strbuilderComm.Append("ICCardID,PinCode,Email,GroupID,RestrictionID,CreateTime,UpdateTime)VALUES(");
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm += MaxId + "','" + Strsql[1];
                                    strbuilderComm.Append("(ISNULL((SELECT MAX(ID) From [SimpleEA].[dbo].[UserInfo]) , 0) + 1)");
                                    strbuilderComm.Append(",'" + Strsql[1]);
                                    //2011.01.20 Update By SES zhoumiao  Update ED
                                    strbuilderComm.Append( "','" + Strsql[2]);
                                    strbuilderComm.Append("','" + Strsql[3]);
                                    strbuilderComm.Append( "','" + Strsql[4]);
                                    strbuilderComm.Append("','" + Strsql[5]);
                                    strbuilderComm.Append( "','" + Strsql[6]);
                                    strbuilderComm.Append("'," + Strsql[7]);
                                    strbuilderComm.Append("," + Strsql[8]);
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm += "','" + Strsql[7];
                                    //strInsertComm += "','" + Strsql[8] + "')";
                                    strbuilderComm.Append( ",getdate() ");
                                    strbuilderComm.Append(",getdate())");
                                    strbuilderComm.Append("\n");
                                    //2011.01.20 Update By SES zhoumiao  Update ED
                                    //2011.01.20 Delete By SES zhoumiao  Update ST
                                    //MaxId += 1;
                                    //2011.01.20 Delete By SES zhoumiao  Update ED
                                }
                                break;
                            }
                        case "PriceMaster":
                            {
                                //if (Name_Check(PriceM, Strsql[1], "PriceNM"))
                                if (ID_Check(PriceM, Strsql[0], "PriceID"))
                                {

                                    strbuilderComm.Append("UPDATE [SimpleEA].[dbo].[PriceMaster] SET PriceCalMode = '" + Strsql[2]);

                                    strbuilderComm.Append("',UpdateTime = getdate() ");
                                    strbuilderComm.Append("  WHERE PriceNM= '" + Strsql[1] + "'");
                                    strbuilderComm.Append("\n");

                                }
                                else
                                {
                                    strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[PriceMaster](PriceID,PriceNM,PriceCalMode,CreateTime,UpdateTime)VALUES(");
                                    strbuilderComm.Append(Strsql[0]);
                                    strbuilderComm.Append(",'" + Strsql[1]);
                                    strbuilderComm.Append("'," + Strsql[2]);
                                    strbuilderComm.Append(",getdate() ");
                                    strbuilderComm.Append(",getdate())");
                                    strbuilderComm.Append("\n");
                                }
                                break;
                            }
                        case "PriceDetail":
                            {
                                if (PriceDetail_Check(PriceD, int.Parse(Strsql[1]),int.Parse(Strsql[2]),int.Parse(Strsql[3])))
                                {

                                    strbuilderComm.Append("UPDATE [SimpleEA].[dbo].[PriceDetail] SET PaperPrice = " + decimal.Parse(Strsql[4]));
                                    strbuilderComm.Append(",GrayPrice = " + decimal.Parse(Strsql[5]));
                                    strbuilderComm.Append(",ColorPrice = " + decimal.Parse(Strsql[6]));
                                    strbuilderComm.Append(",UpdateTime = getdate() ");
                                    strbuilderComm.Append("  WHERE PriceID= " + int.Parse(Strsql[1]) + " and PaperTypeid=" + int.Parse(Strsql[2]) + " and JobID=" + int.Parse(Strsql[3]));
                                    strbuilderComm.Append("\n");

                                }
                                else
                                {
                                    strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[PriceDetail](PriceID,PaperTypeid,JobID,PaperPrice,GrayPrice,ColorPrice,CreateTime,UpdateTime)VALUES(");
                                    strbuilderComm.Append( + int.Parse(Strsql[1]));
                                    strbuilderComm.Append("," + int.Parse(Strsql[2]));
                                    strbuilderComm.Append("," + int.Parse(Strsql[3]));
                                    strbuilderComm.Append("," + decimal.Parse(Strsql[4]));
                                    strbuilderComm.Append("," + decimal.Parse(Strsql[5]));
                                    strbuilderComm.Append("," + decimal.Parse(Strsql[6]));
                                    strbuilderComm.Append(",getdate() ");
                                    strbuilderComm.Append(",getdate())");
                                    strbuilderComm.Append("\n");
                                }
                                break;
                            }
                        case "GroupInfo":
                            {
                                if (Name_Check(Group, Strsql[1], "GroupName"))
                                {
                                    //Cover the [GroupInfo] table Data
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm = "UPDATE GroupInfo SET RestrictionID = '" + Strsql[2];
                                    strbuilderComm.Append("UPDATE [SimpleEA].[dbo].[GroupInfo] SET RestrictionID = '" + Strsql[2]);
                                    //2011.01.20 Update By SES zhoumiao  Update ED    
                                    //strbuilderComm.Append("UPDATE GroupInfo SET RestrictionID = '" + Strsql[2]);
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm += "',CreateTime = '" + Strsql[3];
                                    //strInsertComm += "',UpdateTime = '" + Strsql[4];
                                    //strInsertComm += "'  WHERE GroupName= '" + Strsql[1] + "'";
                                    strbuilderComm.Append( "',UpdateTime = getdate() ");
                                    strbuilderComm.Append("  WHERE GroupName= '" + Strsql[1] + "'");
                                    strbuilderComm.Append("\n");
                                    //2011.01.20 Update By SES zhoumiao  Update ED                                    

                                }
                                else
                                {
                                    //Insert the [GroupInfo] table Data
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm = "INSERT INTO GroupInfo(ID,GroupName,RestrictionID,";
                                    strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[GroupInfo](ID,GroupName,RestrictionID,");
                                    //2011.01.20 Update By SES zhoumiao  Update ED                                        
                                    strbuilderComm.Append("CreateTime,UpdateTime)VALUES(");
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm += MaxId + "','" + Strsql[1];
                                    //strInsertComm += "','" + Strsql[2];
                                    //strInsertComm += "','" + Strsql[3];
                                    //strInsertComm += "','" + Strsql[4] + "')";
                                    strbuilderComm.Append("(ISNULL((SELECT MAX(ID) FROM [SimpleEA].[dbo].[GroupInfo]) , 0) + 1),'");
                                    strbuilderComm.Append( Strsql[1] + "','" + Strsql[2]);
                                    strbuilderComm.Append( "',getdate() ");
                                    strbuilderComm.Append( ",getdate())");
                                    strbuilderComm.Append("\n");
                                    //2011.01.20 Update By SES zhoumiao  Update ED
                                    //2011.01.20 Delete By SES zhoumiao  Update ST
                                    //MaxId += 1;
                                    //2011.01.20 Delete By SES zhoumiao  Update ED

                                }
                                break;
                            }
                        case "RestrictionInfo":
                            {
                                // Update By SES Jijianxiong ST
                                //if (Name_Check(Res, Strsql[1], "RestrictionName"))
                                //{
                                //    //Cover the [RestrictionInfo] table Data
                                //    //2011.01.20 Update By SES zhoumiao  Update ST
                                //    //strInsertComm = "UPDATE RestrictionInfo SET CreateTime = '" + Strsql[2];
                                //    //strInsertComm += "',UpdateTime = '" + Strsql[3];
                                //    //strInsertComm += "'  WHERE RestrictionName= '" + Strsql[1] + "'";
                                //    strInsertComm = "UPDATE [SimpleEA].[dbo].[RestrictionInfo] SET UpdateTime = getdate()";
                                //    strInsertComm += "  WHERE RestrictionName= '" + Strsql[1] + "'";
                                //    //2011.01.20 Update By SES zhoumiao  Update ED                                        


                                //}
                                //else
                                //{
                                //    //Insert the [RestrictionInfo] table Data
                                //    //2011.01.20 Update By SES zhoumiao  Update ST
                                //    //strInsertComm = "INSERT INTO RestrictionInfo(ID,RestrictionName,";
                                //    strInsertComm = "INSERT INTO [SimpleEA].[dbo].[RestrictionInfo](ID,RestrictionName,";
                                //    //2011.01.20 Update By SES zhoumiao  Update ED
                                //    strInsertComm += "CreateTime,UpdateTime)VALUES(";

                                //    //2011.01.20 Update By SES zhoumiao  Update ST
                                //    //strInsertComm += MaxId + "','" + Strsql[1];  
                                //    //strInsertComm += "','" + Strsql[2];
                                //    //strInsertComm += "','" + Strsql[3] + "')";
                                //    strInsertComm += "(ISNULL((SELECT MAX(ID) FROM [SimpleEA].[dbo].[RestrictionInfo]) , 0) + 1),'";
                                //    strInsertComm += Strsql[1] + "',getdate() ";
                                //    strInsertComm += ",getdate())";
                                //    //2011.01.20 Update By SES zhoumiao  Update ED
                                //    //2011.01.20 Delete By SES zhoumiao  Update ST
                                //    //MaxId += 1;
                                //    //2011.01.20 Delete By SES zhoumiao  Update ED
                                //}
                                res.AddRestrictionInfo(Strsql);
                                // Update By SES Jijianxiong ED
                                break;
                            }
                        case "RestrictionInformation":
                            {
                                // Update By SES Jijianxiong ST
                                //if (Resformation_Check(Resformation, Strsql[0], Strsql[1], Strsql[2]))
                                //{
                                //    //Cover the [RestrictionInformation] table Data
                                //    //2011.01.20 Update By SES zhoumiao  Update ST
                                //    //strInsertComm = "UPDATE RestrictionInformation SET Status = '" + Strsql[3];
                                //    strInsertComm = "UPDATE [SimpleEA].[dbo].[RestrictionInformation] SET Status = '" + Strsql[3];
                                //    //2011.01.20 Update By SES zhoumiao  Update ED 
                                //    strInsertComm += "',LimitNum = '" + Strsql[4];
                                //    strInsertComm += "'  WHERE RestrictionID= '" + Strsql[0];
                                //    strInsertComm += "' AND  JobId = '" + Strsql[1];
                                //    strInsertComm += "' AND  FunctionId = '" + Strsql[2] + "'";
                                //}
                                //else
                                //{
                                //    //Insert the [RestrictionInformation] table Data
                                //    //2011.01.20 Update By SES zhoumiao  Update ST
                                //    //strInsertComm = "INSERT INTO RestrictionInformation(RestrictionID,JobId,FunctionId,";
                                //    strInsertComm = "INSERT INTO [SimpleEA].[dbo].[RestrictionInformation](RestrictionID,JobId,FunctionId,";
                                //    //2011.01.20 Update By SES zhoumiao  Update ED
                                //    strInsertComm += "Status,LimitNum)VALUES('";
                                //    strInsertComm += Strsql[0];
                                //    strInsertComm += "','" + Strsql[1];
                                //    strInsertComm += "','" + Strsql[2];
                                //    strInsertComm += "','" + Strsql[3];
                                //    strInsertComm += "','" + Strsql[4] + "')";

                                //}
                                res.AddRestrictionInformation(Strsql);
                                // Update By SES Jijianxiong ED
                                break;
                            }
                        default:
                            break;
                    }
                    //2011.01.25 Delete By SES zhoumiao  Update ST
                    //if (!string.IsNullOrEmpty(strInsertComm))
                    //{
                    //    sql += strInsertComm + "\n";
                    //}
                    //2011.01.25 Delete By SES zhoumiao  Update ED
                }

            }

            // Add By SES Jijianxiong ST
            res.UpdateRes(DBConnectionStrings);
            // Add By SES Jijianxiong ED
            //2011.01.25 Update By SES zhoumiao  Update ST
            String UpdateResSql = Update_ResSQl(res).Trim();
            if (!string.IsNullOrEmpty(UpdateResSql))
            {
                strbuilderComm.Append(UpdateResSql);                
                
            }
            sql = strbuilderComm.ToString().Trim();

            //2011.01.25 Update By SES zhoumiao  Update ED

            //2011.01.20 Update By SES zhoumiao  Update ST
            // SqlConnection(sql);
            if (!string.IsNullOrEmpty(sql))
            {
                SqlConnection(sql);
            }
            //2011.01.20 Update By SES zhoumiao  Update ED

            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED

        }
        #endregion

        #region "NotCoverDate the Users Information Data"
        /// <summary>
        ///  NotCoverDate the Users Information Data
        /// </summary>
        /// <param name="path"></param>
        /// <param name="User"></param>
        /// <param name="Group"></param>
        /// <param name="Res"></param>
        /// <param name="Resformation"></param>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private void NotCoverDate(String path, DataTable User,DataTable PriceM,DataTable PriceD, DataTable Group, DataTable Res, DataTable Resformation)
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "NotCoverDate";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            string sql = "";
            //2011.01.20 Delete By SES zhoumiao  Update ST
            // int MaxId = 0;
            //2011.01.20 Delete By SES zhoumiao  Update ED

            // Add By SES Jijianxiong ST
            ResUpdate res = new ResUpdate(false);
            // Add By SES Jijianxiong ED
            
            //2011.01.25 Add By SES zhoumiao  Update ST
            StringBuilder strbuilderComm = new StringBuilder("");
            //2011.01.25 Add By SES zhoumiao  Update ED
            using (StreamReader streamReader = new StreamReader(path, Encoding.Default, false))
            {
                String tablename = "";

                while (streamReader.Peek() >= 0)
                {
                    
                   // string strInsertComm = string.Empty;
                    string[] Strsql = streamReader.ReadLine().Split(',');
                    //check the table  name is [UserInfo] 
                    if (Strsql[0].Equals("------------[UserInfo]------------"))
                    {
                        tablename = "UserInfo";
                        //2011.01.20 Delete By SES zhoumiao  Update ST                        
                        ////UserInfo Max ID
                        //MaxId = numbleAdd(tablename);
                        //2011.01.20 Delete By SES zhoumiao  Update ED
                        continue;
                    }
                    if (Strsql[0].Equals("------------[PriceMaster]------------"))
                    {
                        tablename = "PriceMaster";
                        continue;
                    }
                    if (Strsql[0].Equals("------------[PriceDetail]------------"))
                    {
                        tablename = "PriceDetail";
                        continue;
                    }
                    //check the table  name is [GroupInfo] 
                    if (Strsql[0].Equals("------------[GroupInfo]------------"))
                    {
                        tablename = "GroupInfo";
                        //2011.01.20 Delete By SES zhoumiao  Update ST                        
                        ////GroupInfo Max ID
                        //MaxId = numbleAdd(tablename);
                        //2011.01.20 Delete By SES zhoumiao  Update ED
                        continue;
                    }
                    //check the table  name is [RestrictionInfo] 
                    if (Strsql[0].Equals("------------[RestrictionInfo]------------"))
                    {
                        tablename = "RestrictionInfo";
                        //2011.01.20 Delete By SES zhoumiao  Update ST                        
                        ////RestrictionInfo Max ID
                        //MaxId = numbleAdd(tablename);
                        //2011.01.20 Delete By SES zhoumiao  Update ED
                        continue;
                    }
                    //check the table  name is [RestrictionInformation] 
                    if (Strsql[0].Equals("------------[RestrictionInformation]------------"))
                    {
                        tablename = "RestrictionInformation";
                        continue;
                    }

                    switch (tablename)
                    {
                        //when Not Cover the each table Data
                        case "UserInfo":
                            {
                                if (!(Name_Check(User, Strsql[2], "LoginName")))
                                {
                                    //Insert the [UserInfo] table Data
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm = "INSERT INTO UserInfo(ID,UserName,LoginName,Password,";
                                    strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[UserInfo](ID,UserName,LoginName,Password,");
                                    //2011.01.20 Update By SES zhoumiao  Update ED
                                    strbuilderComm.Append("ICCardID,PinCode,Email,GroupID,RestrictionID,CreateTime,UpdateTime)VALUES(");
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm += MaxId + "','" + Strsql[1];
                                    strbuilderComm.Append("(ISNULL((SELECT MAX(ID) From [SimpleEA].[dbo].[UserInfo]) , 0) + 1)");
                                    strbuilderComm.Append(",'" + Strsql[1]);
                                    //2011.01.20 Update By SES zhoumiao  Update ED
                                    strbuilderComm.Append("','" + Strsql[2]);
                                    strbuilderComm.Append( "','" + Strsql[3]);
                                    strbuilderComm.Append("','" + Strsql[4]);
                                    strbuilderComm.Append("','" + Strsql[5]);
                                    strbuilderComm.Append( "','" + Strsql[6]);
                                    strbuilderComm.Append("'," + Strsql[7]);
                                    strbuilderComm.Append("," + Strsql[8]);
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm += "','" + Strsql[7];
                                    //strInsertComm += "','" + Strsql[8] + "')";
                                    strbuilderComm.Append(",getdate() ");
                                    strbuilderComm.Append(",getdate())");
                                    strbuilderComm.Append("\n");
                                    //2011.01.20 Update By SES zhoumiao  Update ED
                                    //2011.01.20 Delete By SES zhoumiao  Update ST
                                    //MaxId += 1;
                                    //2011.01.20 Delete By SES zhoumiao  Update ED
                                }

                                break;
                            }
                        case "PriceMaster":
                            {
                                //if (!Name_Check(PriceM, Strsql[1], "PriceNM"))
                                if (!ID_Check(PriceM, Strsql[0], "PriceID"))
                                {
                                    strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[PriceMaster](PriceID,PriceNM,PriceCalMode,CreateTime,UpdateTime)VALUES(");
                                    strbuilderComm.Append(Strsql[0]);
                                    strbuilderComm.Append(",'" + Strsql[1]);
                                    strbuilderComm.Append("'," + Strsql[2]);
                                    strbuilderComm.Append(",getdate() ");
                                    strbuilderComm.Append(",getdate())");
                                    strbuilderComm.Append("\n");
                                }
                                break;
                            }
                        case "PriceDetail":
                            {
                                if (!PriceDetail_Check(PriceD, int.Parse(Strsql[1]), int.Parse(Strsql[2]), int.Parse(Strsql[3])))
                                {
                                    strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[PriceDetail](PriceID,PaperTypeid,JobID,PaperPrice,GrayPrice,ColorPrice,CreateTime,UpdateTime)VALUES(");
                                    strbuilderComm.Append(+int.Parse(Strsql[1]));
                                    strbuilderComm.Append("," + int.Parse(Strsql[2]));
                                    strbuilderComm.Append("," + int.Parse(Strsql[3]));
                                    strbuilderComm.Append("," + decimal.Parse(Strsql[4]));
                                    strbuilderComm.Append("," + decimal.Parse(Strsql[5]));
                                    strbuilderComm.Append("," + decimal.Parse(Strsql[6]));
                                    strbuilderComm.Append(",getdate() ");
                                    strbuilderComm.Append(",getdate())");
                                    strbuilderComm.Append("\n");
                                }
                                break;
                            }
                        case "GroupInfo":
                            {
                                if (!(Name_Check(Group, Strsql[1], "GroupName")))
                                {
                                    //Insert the [GroupInfo] table Data
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm = "INSERT INTO GroupInfo(ID,GroupName,RestrictionID,";
                                    strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[GroupInfo](ID,GroupName,RestrictionID,");
                                    //2011.01.20 Update By SES zhoumiao  Update ED                                        
                                    strbuilderComm.Append("CreateTime,UpdateTime)VALUES(");
                                    //2011.01.20 Update By SES zhoumiao  Update ST
                                    //strInsertComm += MaxId + "','" + Strsql[1];
                                    //strInsertComm += "','" + Strsql[2];
                                    //strInsertComm += "','" + Strsql[3];
                                    //strInsertComm += "','" + Strsql[4] + "')";
                                    strbuilderComm.Append("(ISNULL((SELECT MAX(ID) FROM [SimpleEA].[dbo].[GroupInfo]) , 0) + 1),'");
                                    strbuilderComm.Append( Strsql[1] + "','" + Strsql[2]);
                                    strbuilderComm.Append("',getdate() ");
                                    strbuilderComm.Append(",getdate())");
                                    strbuilderComm.Append("\n");
                                    //2011.01.20 Update By SES zhoumiao  Update ED
                                    //2011.01.20 Delete By SES zhoumiao  Update ST
                                    //MaxId += 1;
                                    //2011.01.20 Delete By SES zhoumiao  Update ED
                                }
                                break;
                            }
                        case "RestrictionInfo":
                            {
                                // Update By SES Jijianxiong ST

                                //if (!(Name_Check(Res, Strsql[1], "RestrictionName")))
                                //{
                                //    //Insert the [RestrictionInfo] table Data
                                //    //2011.01.20 Update By SES zhoumiao  Update ST
                                //    //strInsertComm = "INSERT INTO RestrictionInfo(ID,RestrictionName,";
                                //    strInsertComm = "INSERT INTO [SimpleEA].[dbo].[RestrictionInfo](ID,RestrictionName,";
                                //    //2011.01.20 Update By SES zhoumiao  Update ED
                                //    strInsertComm += "CreateTime,UpdateTime)VALUES(";

                                //    //2011.01.20 Update By SES zhoumiao  Update ST
                                //    //strInsertComm += MaxId + "','" + Strsql[1];  
                                //    //strInsertComm += "','" + Strsql[2];
                                //    //strInsertComm += "','" + Strsql[3] + "')";
                                //    strInsertComm += "(ISNULL((SELECT MAX(ID) FROM [SimpleEA].[dbo].[RestrictionInfo]) , 0) + 1),'";
                                //    strInsertComm += Strsql[1] + "',getdate() ";
                                //    strInsertComm += ",getdate())";
                                //    //2011.01.20 Update By SES zhoumiao  Update ED
                                //    //2011.01.20 Delete By SES zhoumiao  Update ST
                                //    //MaxId += 1;
                                //    //2011.01.20 Delete By SES zhoumiao  Update ED
                                //}
                                res.AddRestrictionInfo(Strsql);
                                // Update By SES Jijianxiong ED

                                break;
                            }
                        case "RestrictionInformation":
                            {
                                // Update By SES Jijianxiong ST
                                //if (!(Resformation_Check(Resformation, Strsql[0], Strsql[1], Strsql[2])))
                                //{
                                //    //Insert the [RestrictionInformation] table Data
                                //    //2011.01.20 Update By SES zhoumiao  Update ST
                                //    //strInsertComm = "INSERT INTO RestrictionInformation(RestrictionID,JobId,FunctionId,";
                                //    strInsertComm = "INSERT INTO [SimpleEA].[dbo].[RestrictionInformation](RestrictionID,JobId,FunctionId,";
                                //    //2011.01.20 Update By SES zhoumiao  Update ED
                                //    strInsertComm += "Status,LimitNum)values('";
                                //    strInsertComm += Strsql[0];
                                //    strInsertComm += "','" + Strsql[1];
                                //    strInsertComm += "','" + Strsql[2];
                                //    strInsertComm += "','" + Strsql[3];
                                //    strInsertComm += "','" + Strsql[4] + "')";
                                //}
                                res.AddRestrictionInformation(Strsql);
                                // Update By SES Jijianxiong ED

                                break;
                            }
                        default:
                            break;
                    }

                    //2011.01.25 Delete By SES zhoumiao  Update ST
                    //if (!string.IsNullOrEmpty(strInsertComm))
                    //{
                    //    sql += strInsertComm + "\n";
                    //}
                    //2011.01.25 Delete By SES zhoumiao  Update ED
                }

            }

            // Add By SES Jijianxiong ST
            res.UpdateRes(DBConnectionStrings);
            // Add By SES Jijianxiong ED
            //2011.01.25 Update By SES zhoumiao  Update ST
            String UpdateResSql = Update_ResSQl(res).Trim();
            if (!string.IsNullOrEmpty(UpdateResSql))
            {
                strbuilderComm.Append(UpdateResSql);
            }
            sql = strbuilderComm.ToString().Trim();
            //2011.01.25 Update By SES zhoumiao  Update ED

            //2011.01.20 Update By SES zhoumiao  Update ST
            // SqlConnection(sql);
            if (!string.IsNullOrEmpty(sql))
            {
                SqlConnection(sql);
            }
            //2011.01.20 Update By SES zhoumiao  Update ED
            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED

        }
        #endregion

        #region "UpdateResSQl"
        /// <summary>
        /// UpdateResSQl
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        /// <Date>2011.01.25</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>

        protected String Update_ResSQl(ResUpdate res)
        {
            String strInsertComm = "";
            StringBuilder strbuilderComm = new StringBuilder("");

            // Function Name.
            string FunctionName = "ResformationSQL";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);


            if (res.RestrictionInfoCount > 0)
            {
                for (int i = 0; i < res.RestrictionInfoCount; i++)
                {
                    if (ResStatus.Insert.Equals(res.RestrictionInfoRow(i).Status))
                    {
                        strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[RestrictionInfo](ID,RestrictionName,AllQuota,ColorQuota,OverLimit,");
                        strbuilderComm.Append( "CreateTime,UpdateTime)VALUES(");
                        strbuilderComm.Append(res.RestrictionInfoRow(i).ID + ",'");
                        strbuilderComm.Append(res.RestrictionInfoRow(i).RestrictionName + "',");
                        strbuilderComm.Append(res.RestrictionInfoRow(i).AllQuota + ",");
                        strbuilderComm.Append(res.RestrictionInfoRow(i).ColorQuota + ",");
                        strbuilderComm.Append(res.RestrictionInfoRow(i).OverLimit + ",");
                        strbuilderComm.Append("getdate(),");
                        strbuilderComm.Append("getdate()");
                        strbuilderComm.Append(")");
                        strbuilderComm.Append("\n");
                    }
                    else if (ResStatus.Update.Equals(res.RestrictionInfoRow(i).Status))
                    {
                        strbuilderComm.Append("UPDATE [SimpleEA].[dbo].[RestrictionInfo] SET UpdateTime = getdate()");
                        strbuilderComm.Append(",ID='" + res.RestrictionInfoRow(i).ID + "'");
                        strbuilderComm.Append("  WHERE RestrictionName= '" + res.RestrictionInfoRow(i).RestrictionName + "'");
                        strbuilderComm.Append("\n");
                    }


                }
            }
            if (res.RestrictionInformationCount > 0)
            {
                strbuilderComm.Append("DELETE [SimpleEA].[dbo].[RestrictionInformation] WHERE RestrictionID <> 0 ");
                strbuilderComm.Append("\n");
                for (int i = 0; i < res.RestrictionInformationCount; i++)
                {
                    strbuilderComm.Append("INSERT INTO [SimpleEA].[dbo].[RestrictionInformation](RestrictionID,JobId,FunctionId,");
                    strbuilderComm.Append("Status,LimitNum)VALUES(");
                    strbuilderComm.Append(res.RestrictionInformationRow(i).RestrictionID + ",");
                    strbuilderComm.Append(res.RestrictionInformationRow(i).JobId + ",");
                    strbuilderComm.Append(res.RestrictionInformationRow(i).FunctionId + ",");
                    strbuilderComm.Append(res.RestrictionInformationRow(i).Status + ",'");
                    strbuilderComm.Append(res.RestrictionInformationRow(i).LimitNum + "'");
                    strbuilderComm.Append(")");
                    strbuilderComm.Append("\n");
                }

            }

            strInsertComm = strbuilderComm.ToString();
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            return strInsertComm;

        }
        #endregion

        #region "Save File"
        /// <summary>
        /// Save File
        /// </summary>
        /// <returns></returns>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private bool savefile()
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "savefile";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            // Set File Name
            string filename = "UserDataBackUp";
            string strNow = DateTime.Now.ToString("yyyyMMddHHmmss");

            filename = filename + strNow;

            // Config the SaveFileDialog
            //2011.01.20 Add By SES zhoumiao  Update ST
            saveOutputFile.Reset();
            //2011.01.20 Add By SES zhoumiao  Update ED
            saveOutputFile.Title = "保存文件";
            saveOutputFile.FileName = filename;
            saveOutputFile.Filter = "CSV文件(*.csv)|*.csv";
            saveOutputFile.RestoreDirectory = true;

            if (saveOutputFile.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                string strOutput = GetData();

                using (StreamWriter streamWriter = new StreamWriter(saveOutputFile.FileName, false, Encoding.Default))
                {
                    streamWriter.Write(strOutput);
                    streamWriter.Close();
                }
                //2011.01.25 Add By SES zhoumiao  Update ST
                // --> Log
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED

                return true;
            }
            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED
            return false;

        }
        #endregion

        #region "Get the Users Information Data"
        /// <summary>
        /// Get the Users Information Data
        /// </summary>
        /// <returns></returns>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private string GetData()
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "GetData";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            StringBuilder strOutput = new StringBuilder();

           
            //UserInfo Table Name 
            strOutput.Append("------------[UserInfo]------------");
            //UserInfo Table information
            strOutput = GetStringBuilderData("UserInfo", strOutput);
            //PriceMaster Table Name
            strOutput.Append("\r\n------------[PriceMaster]------------");
            //PriceMaster Table information
            strOutput = GetStringBuilderData("PriceMaster", strOutput);
            //PriceDetail Table Name
            strOutput.Append("\r\n------------[PriceDetail]------------");
            //PriceDetail Table information
            strOutput = GetStringBuilderData("PriceDetail", strOutput);
            //GroupInfo Table Name
            strOutput.Append("\r\n------------[GroupInfo]------------");
            //GroupInfo Table information
            strOutput = GetStringBuilderData("GroupInfo", strOutput);
            //RestrictionInfo Table Name
            strOutput.Append("\r\n------------[RestrictionInfo]------------");
            //RestrictionInfo Table information
            strOutput = GetStringBuilderData("RestrictionInfo", strOutput);
            //RestrictionInformation Table Name
            strOutput.Append("\r\n------------[RestrictionInformation]------------");
            //RestrictionInformation Table information
            strOutput = GetStringBuilderData("RestrictionInformation", strOutput);
            //2018.01.16 start
            //strOutput.Append("\r\n------------[DBThirdAuthConfig]------------");
            //strOutput = GetStringBuilderData("DBThirdAuthConfig", strOutput);
            //strOutput.Append("\r\n------------[LDAPSetting]------------");
            //strOutput = GetStringBuilderData("LDAPSetting", strOutput);
            //2018.01.16 end
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED
            return strOutput.ToString();


        }
        private StringBuilder GetStringBuilderData(String TableName, StringBuilder strOutput)
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "GetStringBuilderData";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            //each Table information Add 
            //2011.01.20 Update By SES zhoumiao  Update ST
            //string strSql = "SELECT * FROM " + TableName;
            string strSql = "SELECT * FROM [SimpleEA].[dbo]." + TableName;
            switch (TableName)
            {
                case "UserInfo":
                    //2011.01.24 Update By SES zhoumiao  Update ST
                    //strSql += "  WHERE [UserInfo].ID <> 0";
                    strSql += "  WHERE [UserInfo].ID <> 0 ORDER BY ID";
                    //2011.01.24 Update By SES zhoumiao  Update ST
                    break;
                case "GroupInfo":
                    //2011.01.24 Update By SES zhoumiao  Update ST
                    //strSql += " WHERE NOT [GroupInfo].ID IN(0,-1)"; 
                    strSql += " WHERE NOT [GroupInfo].ID IN(0,-1) ORDER BY ID";
                    //2011.01.24 Update By SES zhoumiao  Update ST

                    break;
                case "RestrictionInfo":
                    //2011.01.24 Update By SES zhoumiao  Update ST
                    //strSql += " WHERE [RestrictionInfo].ID <> 0"; 
                    strSql += " WHERE  NOT [RestrictionInfo].ID IN(0,-1) ORDER BY ID";
                    //2011.01.24 Update By SES zhoumiao  Update ST

                    break;
                case "PriceMaster":
                    //2011.01.24 Update By SES zhoumiao  Update ST
                    //strSql += " WHERE [RestrictionInfo].ID <> 0"; 
                    strSql += " WHERE [PriceMaster].PriceID <> 0 ORDER BY PriceID";
                    //2011.01.24 Update By SES zhoumiao  Update ST

                    break;
                case "PriceDetail":
                    //2011.01.24 Update By SES zhoumiao  Update ST
                    //strSql += " WHERE [RestrictionInfo].ID <> 0"; 
                    strSql += " WHERE [PriceDetail].PriceDetailID <> 0 ORDER BY PriceDetailID";
                    //2011.01.24 Update By SES zhoumiao  Update ST
                    break;
                //case "DBThirdAuthConfig":
                //    break;
                //case "LDAPSetting":
                //    break;

                default:
                    //2011.01.24 Add By SES zhoumiao  Update ST
                    strSql += " WHERE NOT [RestrictionInformation].RestrictionID IN(0,-1) ORDER BY RestrictionID";
                    //2011.01.24 Add By SES zhoumiao  Update ST
                    break;
            }
            //2011.01.20 Update By SES zhoumiao  Update ED            

            using (SqlDataReader reader = Common.ExecuteReader(strSql, this.DBConnectionStrings))
            {
                while (reader.Read())
                {

                    strOutput.Append("\r\n");

                    for (int i = 0; i < reader.FieldCount; i++)
                    {

                        strOutput.Append(reader[i].ToString().Replace(",", "，"));
                        if (i != reader.FieldCount - 1)
                        {
                            strOutput.Append(",");
                        }
                    }
                }
            }
            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED
            return strOutput;

        }


        #endregion

        //2011.01.25 Delete By SES zhoumiao  Update ST
        //#region "ExecuteDataTable"
        ///// <summary>
        ///// ExecuteDataTable
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        ///// <Date>2011.01.19</Date>
        ///// <Author>SES Zhou Miao</Author>
        ///// <Version>1.0</Version>
        //public DataTable ExecuteDataTable(string sql)
        //{
        //    DataTable data = new DataTable();

        //    using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        //    {
        //        con.Open();
        //        using (SqlCommand cmd = new SqlCommand(sql, con))
        //        {
        //            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
        //            adapter.Fill(data);
        //            return data;
        //        }
        //    }
        //}
        //#endregion
        //2011.01.25 Delete By SES zhoumiao  Update ST

        #region "SqlConnection"
        /// <summary>
        /// SqlConnection
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private void SqlConnection(string sql)
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "SqlConnection";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            using (SqlConnection con = new SqlConnection(DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {

                    using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    // 2011.01.27 Add By SES zhoumiao ST                  

                    String GaResSql = GaRes_Check();
                    using (SqlCommand cmd = new SqlCommand(GaResSql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 2011.01.27 Add By SES zhoumiao ED
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    if (tran.Connection != null)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
                finally
                {
                    tran.Dispose();
                    tran = null;
                    //2011.01.25 Add By SES zhoumiao  Update ST
                    // --> Log
                    CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                    //2011.01.25 Add By SES zhoumiao  Update ED
                }

            }
        }
        #endregion

        #region "Check Name In  Table"
        /// <summary>
        /// Check Name In Table.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Id"></param>
        /// <param name="strColumnName"></param>
        /// <returns></returns>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        protected Boolean Name_Check(DataTable dt, string name, string strColumnName)
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "Name_Check";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            string strSql = strColumnName + "='" + name + "'";
            DataRow[] row = dt.Select(strSql);
            if (row == null || row.Length == 0)
            {
                //2011.01.25 Add By SES zhoumiao  Update ST                
                // --> Log
                CustomLog.RecordLog(string.Format("The {1} of {0}  is existed.", strColumnName, name), CustomLog.Level.DEBUG);
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED
                return false;
            }
            else
            {
                //2011.01.25 Add By SES zhoumiao  Update ST
                // --> Log
                CustomLog.RecordLog(string.Format("The {1} of {0}  is not existed.", strColumnName, name), CustomLog.Level.DEBUG);
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED
                return true;
            }
        }
        protected Boolean ID_Check(DataTable dt, string name, string strColumnName)
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "ID_Check";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            string strSql = strColumnName + "='" + name + "'";
            DataRow[] row = dt.Select(strSql);
            if (row == null || row.Length == 0)
            {
                //2011.01.25 Add By SES zhoumiao  Update ST                
                // --> Log
                CustomLog.RecordLog(string.Format("The {1} of {0}  is existed.", strColumnName, name), CustomLog.Level.DEBUG);
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED
                return false;
            }
            else
            {
                //2011.01.25 Add By SES zhoumiao  Update ST
                // --> Log
                CustomLog.RecordLog(string.Format("The {1} of {0}  is not existed.", strColumnName, name), CustomLog.Level.DEBUG);
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED
                return true;
            }
        }
        protected Boolean PriceDetail_Check(DataTable dt, int priceid, int papertypeid,int jobid)
        {
            string FunctionName = "PriceDetail_Check";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2014.04.26 Add By pu peng  Update ED
            string strSql = "priceid=" + priceid + " and papertypeid=" + papertypeid + " and jobid=" + jobid;
            DataRow[] row = dt.Select(strSql);
            if (row == null || row.Length == 0)
            {
                //2014.04.26 Add By pu peng  Update ST                
                // --> Log
                CustomLog.RecordLog(string.Format("The {0},{1},{2} of priceid,papertypeid,jobid  is existed.", priceid.ToString(), papertypeid.ToString(), jobid.ToString()), CustomLog.Level.DEBUG);
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2014.04.26 Add By pu peng  Update ED
                return false;
            }
            else
            {
                //2014.04.26 Add By pu peng  Update ST
                // --> Log
                CustomLog.RecordLog(string.Format("The {0},{1},{2} of priceid,papertypeid,jobid  is not existed.", priceid.ToString(), papertypeid.ToString(), jobid.ToString()), CustomLog.Level.DEBUG);
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2014.04.26 Add By pu peng  Update ED
                return true;
            }
        }
        #endregion

        #region "Check Group and Restriction In UserInfo Table"
        /// <summary>
        /// Check Group and Restriction In  UserInfo Table
        /// </summary>
        /// <returns></returns>
        /// <Date>2011.01.24</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        protected String GaRes_Check()
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "Group and Restriction Check";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            string strSql = "UPDATE [SimpleEA].[dbo].[UserInfo] SET ";
            strSql += "GroupID = '0'";
            strSql += " WHERE GroupID NOT IN(SELECT ID FROM [SimpleEA].[dbo].[GroupInfo]) \n";
            strSql += "UPDATE [SimpleEA].[dbo].[GroupInfo] SET ";
            strSql += "RestrictionID = '0'";
            strSql += " WHERE RestrictionID NOT IN(SELECT ID FROM [SimpleEA].[dbo].[RestrictionInfo])\n";
            strSql += "UPDATE [SimpleEA].[dbo].[UserInfo] SET ";
            strSql += "RestrictionID = '0'";
            strSql += " WHERE RestrictionID NOT IN(SELECT ID FROM [SimpleEA].[dbo].[RestrictionInfo])";
            
            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED
            return strSql;

        }
        #endregion

        //2011.01.25 Delete By SES zhoumiao  Update ST
        //#region "Check RestrictionInformation In  Table"
        ///// <summary>
        ///// Check RestrictionInformation In  Table
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <param name="Id"></param>
        ///// <param name="strColumnName"></param>
        ///// <returns></returns>
        ///// <Date>2011.01.19</Date>
        ///// <Author>SES Zhou Miao</Author>
        ///// <Version>1.0</Version>
        //protected Boolean Resformation_Check(DataTable dt, string RestrictionID, string JobId, string FunctionId)
        //{
        //    //2011.01.25 Add By SES zhoumiao  Update ST
        //    // Function Name.
        //    string FunctionName = "Resformation_Check";
        //    // --> Log
        //    CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
        //    //2011.01.25 Add By SES zhoumiao  Update ED
        //    string strSql = "RestrictionID = {0} and JobId = {1} and FunctionId={2}";
        //    DataRow[] row = dt.Select(string.Format(strSql, RestrictionID, JobId, FunctionId));
        //    if (row == null || row.Length == 0)
        //    {
        //        //2011.01.25 Add By SES zhoumiao  Update ST
        //        // --> Log
        //        CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        //        //2011.01.25 Add By SES zhoumiao  Update ED
        //        return false;
        //    }
        //    else
        //    {
        //        //2011.01.25 Add By SES zhoumiao  Update ST
        //        // --> Log
        //        CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        //        //2011.01.25 Add By SES zhoumiao  Update ED
        //        return true;
        //    }
        //}
        //#endregion
        //2011.01.25 Delete By SES zhoumiao  Update ST


        //2011.01.20 Delete By SES zhoumiao  Update ST
        //#region "Max number In  Table"
        ///// <summary>
        ///// Max number In  Table
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        ///// <Date>2011.01.19</Date>
        ///// <Author>SES Zhou Miao</Author>
        ///// <Version>1.0</Version>
        //protected int numbleAdd(string dtname)
        //{
        //    //2011.01.20 Update By SES zhoumiao  Update ST
        //    // string strSql = "Select MAX(ID) AS MaxNo FROM " + dtname;
        //    string strSql = "Select ISNULL(MAX(ID),-99) AS MaxNo FROM [SimpleEA].[dbo]." + dtname;           
        //    //2011.01.20 Update By SES zhoumiao  Update ED            
        //    // Detail Date
        //    DataTable Idtable = ExecuteDataTable(strSql);
        //    //2011.01.20 Update By SES zhoumiao  Update ST
        //    //if (Idtable.Rows.Count > 0)
        //    //{
        //    //    return Convert.ToInt32(Idtable.Rows[0][0]) + 1;
        //    //}

        //    //else
        //    //{
        //    //    return 1;
        //    //}
        //    if (Convert.ToInt32(Idtable.Rows[0][0]) != -99)
        //    {
        //        return Convert.ToInt32(Idtable.Rows[0][0]) + 1;
        //    }            
        //    return 1;
        //    //2011.01.20 Update By SES zhoumiao  Update ED 
        //}
        //#endregion
        //2011.01.20 Delete By SES zhoumiao  Update ED

        #region "UpdateUserXml"
        /// <summary>
        /// UpdateUserXml
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        private void UpdateUserXml(string filepath)
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "UpdateUserXml";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            //2011.01.24 Add By SES zhoumiao  Update ST 
            // Set File Name
            string filename = "SimpleEAUserXmlUpdate";
            string strNow = DateTime.Now.ToString("yyyyMMddHHmmss");
            filename = filename + strNow;
            filepath = Path.Combine(filepath, filename);
            //2011.01.24 Add By SES zhoumiao  Update ED 
            string newPath = Path.Combine(filepath, App_Data);
            string newFilePath = Path.Combine(newPath, newFileName);
            //Create a new subfolder under the current active folder
            try
            {
                if ((!Directory.Exists(newPath)))
                {
                    Directory.CreateDirectory(newPath);

                }
                else
                {
                    Directory.Delete(newPath, true);
                    Directory.CreateDirectory(newPath);
                }
                //Create a new file name           
                WriteXML(newFilePath);
                string strSql = " SELECT "
                    + "       LoginName"
                    + "      ,Password"
                    + "      ,ICCardID"
                    //2011.01.20 Update By SES zhoumiao  Update ST
                    // + "  FROM  UserInfo";
                + "  FROM  [SimpleEA].[dbo].[UserInfo]";
                //2011.01.20 Update By SES zhoumiao  Update ED
                // IC Card ID
                string strICCardID = string.Empty;
                // Login Name
                string strLoginName = string.Empty;
                // Password
                string strPassword = string.Empty;
                // Detail Date 
                //2011.01.25 Update By SES zhoumiao  Update ST
                //DataTable UserInfotable = ExecuteDataTable(strSql);
                DataTable UserInfotable = Common.ExecuteDataTable(strSql, DBConnectionStrings);
                //2011.01.25 Update By SES zhoumiao  Update ED
                if (UserInfotable.Rows.Count > 0)
                {
                    foreach (DataRow row in UserInfotable.Rows)
                    {
                        // Get the IC Card ID
                        strICCardID = row["ICCardID"].ToString().Trim();
                        // 2011.01.25 Add By SES zhoumiao Update ST
                        // --> Log                    
                        CustomLog.RecordLog(string.Format("The strICCardID is {0}.", strICCardID), CustomLog.Level.DEBUG);
                        // 2011.01.25 Add By SES zhoumiao Update ED
                        // Get the Login Name.
                        strLoginName = row["LoginName"].ToString().Trim();
                        // --> Log                    
                        CustomLog.RecordLog(string.Format("The  Login Name is {0}.", strLoginName), CustomLog.Level.DEBUG);
                        // Get the Password.
                        strPassword = row["Password"].ToString().Trim();
                        // --> Log                    
                        CustomLog.RecordLog(string.Format("The Password is {0}.", strPassword), CustomLog.Level.DEBUG);
                        // While the IC Card ID is null.
                        if ((!string.IsNullOrEmpty(strICCardID)) && (!"admin".Equals(strLoginName)))
                        {
                            int returnVal = ICCardData.AddICCardInfo(strICCardID, strLoginName, strPassword, filepath);
                            if (returnVal != 0)
                            {
                                //2011.01.25 Add By SES zhoumiao  Update ST
                                // --> Log
                                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                                //2011.01.25 Add By SES zhoumiao  Update ED
                                throw new Exception("Error on Add IccardInfor");
                            }
                        }
                    }
                }

                //chen 20160120 update start
                //if (File.Exists(txtFilepath.Text.Trim()))
                //{
                //    File.Delete(txtFilepath.Text.Trim());
                //}
                //File.Move(newFilePath, txtFilepath.Text.Trim());
                //// 2011.01.20 Add by SES jijianxiong ST
                //// Add File Security
                //CustomFileSecurity.FileAccessControl(txtFilepath.Text.Trim(), System.Security.AccessControl.FileSystemRights.FullControl, System.Security.AccessControl.AccessControlType.Allow);
                //// 2011.01.20 Add by SES jijianxiong ED
                //chen 20160120 update end
            }        
            finally
            {
                //2011.01.24 Update By SES zhoumiao  Update ED
                //Directory.Delete(newPath, true);
                Directory.Delete(filepath, true);
                //2011.01.24 Update By SES zhoumiao  Update ED                
                //2011.01.25 Add By SES zhoumiao  Update ST
                // --> Log
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED
            }

        }
        #endregion

        #region "Check UserInfo File Name"
        /// <summary>
        /// Check UserInfo File Name
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Id"></param>
        /// <param name="strColumnName"></param>
        /// <returns></returns>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        protected Boolean UserInfo_Check(String FileName)
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "UserInfo_Check";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            //2011.01.20 Update By SES zhoumiao  Update ST 
            //2011.01.20 Update By SES zhoumiao  Update ST 
            //if ("UserInfo.XML".Equals(FileName))
            //if (("UserInfo.XML".Equals(FileName))||("UserInfo.xml".Equals(FileName)))
            //2011.01.20 Update By SES zhoumiao  Update ED 
            if ("UserInfo.XML".Equals(FileName))
            //2011.01.20 Update By SES zhoumiao  Update ED
            {
                //2011.01.25 Add By SES zhoumiao  Update ST
                // --> Log               
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED
                return true;
            }
            else
            {
                //2011.01.25 Add By SES zhoumiao  Update ST
                // --> Log                
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                //2011.01.25 Add By SES zhoumiao  Update ED
                return false;
            }
        }
        #endregion

        #region "WriteXML"
        /// <summary>
        /// WriteXML
        /// </summary>
        /// <returns></returns>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Zhou Miao</Author>
        /// <Version>1.0</Version>
        public void WriteXML(string newFilePath)
        {
            //2011.01.25 Add By SES zhoumiao  Update ST
            // Function Name.
            string FunctionName = "WriteXML";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            //2011.01.25 Add By SES zhoumiao  Update ED
            StreamWriter sw = null;
            string strUser = string.Empty;

            try
            {
                sw = new StreamWriter(newFilePath, true);
                strUser = "<?xml version=\"1.0\"?>";
                sw.WriteLine(strUser);
                strUser = "<ICCardList>";
                sw.WriteLine(strUser);
                strUser = "</ICCardList>";
                sw.WriteLine(strUser);

            }
            catch
            {
                ;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            //2011.01.25 Add By SES zhoumiao  Update ST
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            //2011.01.25 Add By SES zhoumiao  Update ED


        }
        #endregion

    }
}