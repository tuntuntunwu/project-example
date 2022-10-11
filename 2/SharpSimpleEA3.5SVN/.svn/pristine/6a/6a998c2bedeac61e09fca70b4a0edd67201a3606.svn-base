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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using SimpleEACommon;

namespace JobOutput
{
    public partial class Main : Form
    {
        // Class Name.
        internal const string ClassName = "Main";
        private INIClass ini;
        private const string CON_MESSAGE_ININOTEXIST = "配置文件不存在，请确认配置文件是否在程序所在目录。";
        private const string CON_MESSAGE_INIERROR = "配置文件内容不正确，请联系管理员。";
        private const string CON_MESSAGE_FINISH = "操作正常终了。";
        private const string CON_MESSAGE_CANCEL = "用户取消了操作。";
        private const string CON_MESSAGE_DELETE_CONFIRM = "将删除指定时间内的数据，是否继续。\r\n 请注意操作不可逆。";
        private const string CON_MESSAGE_SETUP = "应用程序正在运行中，请不要重复启动！";
        private string DBConnectionStrings = "";

        public Main()
        {
            InitializeComponent();
            string FunctionName = "Main";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);


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
                // 2011.01.25 Update By SES Jijianxiong ST
                // throw new Exception(CON_MESSAGE_SETUP);
                CustomLog.RecordLog(CON_MESSAGE_SETUP, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES Jijianxiong ED
            }


            this.Load += new EventHandler(Main_Load);

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
                // 2011.01.25 Update By SES Jijianxiong ST
                // throw new Exception(CON_MESSAGE_ININOTEXIST);
                CustomLog.RecordLog(CON_MESSAGE_ININOTEXIST, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES Jijianxiong ED
            }

            // Get Connection String
            DBConnectionStrings = ini.GetValue("SimpleEA", "SimpleEAConnectionString");
            if (string.IsNullOrEmpty(DBConnectionStrings))
            {
                // 2011.01.21 Update By SES Jijianxiong ST
                // MessageBox.Show(CON_MESSAGE_INIERROR, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.Error(CON_MESSAGE_INIERROR);
                // 2011.01.21 Update By SES Jijianxiong ED
                // 2011.01.25 Update By SES Jijianxiong ST
                // throw new Exception(CON_MESSAGE_INIERROR);
                CustomLog.RecordLog(CON_MESSAGE_INIERROR, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES Jijianxiong ED

            }

            // Connection to DB
            string strSql = "SELECT '1' AS Name FROM [JobInformation]";
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

                // 2011.01.25 Update By SES Jijianxiong ST
                //// 2011.01.21 Update By SES Jijianxiong ST
                //// MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Common.Error(ex.Message);
                //// 2011.01.21 Update By SES Jijianxiong ED
                //throw ex;

                Common.Error(Common.MSG_SYSTEMERROR);
                CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES Jijianxiong ED

            }
            finally
            {
                // --> Log
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            }

        }

        protected void Main_Load(Object sender, EventArgs e)
        {
            string FunctionName = "Main_Load";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            // Set Time Format.
            this.StartTime.Format = DateTimePickerFormat.Custom;
            this.StartTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";

            this.EndTime.Format = DateTimePickerFormat.Custom;
            this.EndTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

        }

        #region "btnClose_Click"
        /// <summary>
        /// Close Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Date>2010.08.06</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private void btnClose_Click(object sender, EventArgs e)
        {
            string FunctionName = "btnClose_Click";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            this.Close();
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        }
        #endregion

        #region "btnOutput_Click"
        /// <summary>
        /// btnOutput_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <Date>2010.08.06</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private void btnOutput_Click(object sender, EventArgs e)
        {
            string FunctionName = "btnOutput_Click";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            btnClose.Enabled = false;
            btnOutput.Enabled = false;

            this.Enabled = false;
            // Save File.
            try
            {
                if (savefile())
                {
                    // 2011.01.21 Update By SES Jijianxiong ST
                    // MessageBox.Show(CON_MESSAGE_FINISH, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Common.Alert(CON_MESSAGE_FINISH);
                    // 2011.01.21 Update By SES Jijianxiong ED
                    
                }
                else
                {
                    // 2011.01.21 Update By SES Jijianxiong ST
                    // MessageBox.Show(CON_MESSAGE_CANCEL, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Common.Alert(CON_MESSAGE_FINISH);
                    // 2011.01.21 Update By SES Jijianxiong ED
                }
            }
            catch (Exception ex)
            {

                // 2011.01.25 Update By SES Jijianxiong ST
                //// 2011.01.21 Update By SES Jijianxiong ST
                //// MessageBox.Show(ex.Message.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Common.Error(ex.Message);
                //// 2011.01.21 Update By SES Jijianxiong ED
                Common.Error(Common.MSG_SYSTEMERROR);
                CustomLog.RecordLog(ex.Message, CustomLog.Level.ERROR);
                Application.Exit();
                // 2011.01.25 Update By SES Jijianxiong ED
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnClose.Enabled = true;
                btnOutput.Enabled = true;
                this.Enabled = true;
                // --> Log
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            }

        }
        #endregion

        #region "Save File"
        /// <summary>
        /// Save File
        /// </summary>
        /// <param name="strOutput"></param>
        /// <Date>2010.08.06</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private bool savefile()
        {
            string FunctionName = "savefile";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);


            // Set File Name
            string filename = "Job_Information";
            string strNow = DateTime.Now.ToString("yyyyMMddHHmmss");

            filename = filename + strNow;


            // Config the SaveFileDialog
            saveOutputFile.Title = "保存文件";
            saveOutputFile.FileName = filename;
            saveOutputFile.Filter = "CSV文件(*.csv)|*.csv|文本文件(*.txt)|*.txt";
            saveOutputFile.RestoreDirectory = true;

            DialogResult reDelete = DialogResult.Yes;
            // delete date checkbox is true
            if (chkDelete.Checked.Equals(true))
            {
                // 2011.01.21 Update By SES Jijianxiong ST
                // reDelete = MessageBox.Show(CON_MESSAGE_DELETE_CONFIRM, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                reDelete = Common.Confirm(CON_MESSAGE_DELETE_CONFIRM, MessageBoxButtons.YesNo);
                // 2011.01.21 Update By SES Jijianxiong ED

            }

            if (reDelete.Equals(DialogResult.Yes))
            {
                if (saveOutputFile.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string strOutput = GetData();


                    using (StreamWriter streamWriter = new StreamWriter(saveOutputFile.FileName, false, Encoding.Default))
                    {
                        streamWriter.Write(strOutput);
                        streamWriter.Close();
                    }

                    // delete date checkbox is true
                    if (chkDelete.Checked.Equals(true))
                    {
                        DeleteDate();
                    }

                    // --> Log
                    CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
                    return true;
                }
            }

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            return false;

        }
        #endregion

        #region "Get the Job Information Data"
        /// <summary>
        /// Get the Job Information Data
        /// </summary>
        /// <returns></returns>
        /// <Date>2010.08.06</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private string GetData()
        {
            string FunctionName = "GetData";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            string strSql = " SELECT     UserInfo.LoginName,  " +
                " 			GroupInfo.GroupName,  " +
                " 			JobTypeInformation.JobNameDisp,  " +
                " 			FunctionTypeInformation.FunctionNameDisp,  " +
                " 			MFPInformation.ModelName,  " +
                " 			MFPInformation.IPAddress,  " +
                " 			PaperSizeInformation.PaperSize,  " +
                " 			JobInformation.Number,  " +
                "           JobInformation.Time " +
                " FROM         JobInformation  " +
                " 		LEFT OUTER JOIN " +
                "                       MFPInformation  " +
                "                 ON JobInformation.SerialNumber = MFPInformation.SerialNumber  " +
                "         LEFT OUTER JOIN " +
                "                       JobTypeInformation  " +
                "                 ON JobInformation.JobID = JobTypeInformation.ID  " +
                "         LEFT OUTER JOIN " +
                "                       FunctionTypeInformation  " +
                "                 ON JobInformation.JobID = FunctionTypeInformation.JobID  " +
                "                 AND  " +
                "                       JobInformation.FunctionID = FunctionTypeInformation.FunctionID  " +
                "         LEFT OUTER JOIN " +
                "                       GroupInfo  " +
                "                 ON JobInformation.GroupID = GroupInfo.ID  " +
                "         LEFT OUTER JOIN " +
                "                       UserInfo  " +
                "                 ON JobInformation.UserID = UserInfo.ID  " +
                "         LEFT OUTER JOIN " +
                "                       PaperSizeInformation  " +
                "                 ON JobInformation.PageID = PaperSizeInformation.ID " + 
                " WHERE SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= '" + StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= '" + EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            StringBuilder strOutput = new StringBuilder();
            // Title
            strOutput.Append("LoginName,");
            strOutput.Append("GroupName,");
            strOutput.Append("JobName,");
            strOutput.Append("FunctionName,");
            strOutput.Append("ModelName,");
            strOutput.Append("IPAddress,");
            strOutput.Append("PaperSize,");
            strOutput.Append("Number,");
            strOutput.Append("Time");
            using (SqlDataReader reader = Common.ExecuteReader(strSql, this.DBConnectionStrings))
            {
                while (reader.Read())
                {

                    strOutput.Append("\r\n");

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        
                        strOutput.Append(reader[i].ToString().Replace(",","，"));
                        if (i != reader.FieldCount - 1)
                        {
                            strOutput.Append(",");
                        }
                    }
                }
            }

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

            return strOutput.ToString();
        }
        #endregion

        #region "Get the Job Information Data"
        /// <summary>
        /// Get the Job Information Data
        /// </summary>
        /// <returns></returns>
        /// <Date>2010.08.06</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private void DeleteDate()
        {
            string FunctionName = "DeleteDate";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            string strSql = " DELETE " +
                " FROM         JobInformation  " +
                " WHERE SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= '" + StartTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= '" + EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            using (SqlConnection con = new SqlConnection(DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }
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
                }

            }
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        }
        #endregion

    }
}