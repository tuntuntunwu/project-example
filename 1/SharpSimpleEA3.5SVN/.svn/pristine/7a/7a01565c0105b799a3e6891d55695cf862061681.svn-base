using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml;
using System.ServiceProcess;
using System.Net;
using dtServerIPSettingTableAdapters;
using System.IO;
public partial class Settings_ServerIPSetting : MainPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_SERVERIP_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_SERVERIP_SET, "ServerIPSetting.aspx", true);
        this.Master.JobReportTitle();
        // Must Check 
        //CusIPRequire.ErrorMessage = "请输入MFP IP 地址";
        CusIPRequire.ErrorMessage = "IP地址未填写或重复,请核对";
        // Special Code
        IpReqular.ErrorMessage = "Ip 地址格式不正确，请重新输入";
        // Check Access Role
        CheckUser();

        if (!IsPostBack)
        {
            InitialPage();
        }
      
    }


    #region "InitialPage"
    /// <summary>
    /// InitialPage
    /// </summary>
    /// <Date>2014.05.16</Date>
    /// <Author>SLC  chen yg</Author>
    /// <Version>1.2</Version>
    private void InitialPage()
    {
        this.txtIP1.Text = "";
        this.txtIP2.Text = "";
        this.txtIP3.Text = "";
        this.txtIP4.Text = "";

        dtServerIPSettingTableAdapters.SettinServerIPTableAdapter adpter = new SettinServerIPTableAdapter();
        dtServerIPSetting.SettinServerIPDataTable dt = adpter.GetData();

        if (dt!= null && dt.Count > 0)
        {
            string strIp = dt[0].IPAddress;
            if (!string.IsNullOrEmpty(strIp))
            {
                string[] ipList = strIp.Split('.');
                if (ipList.Length > 3)
                {
                    this.txtIP1.Text = ipList[0];
                    this.txtIP2.Text = ipList[1];
                    this.txtIP3.Text = ipList[2];
                    this.txtIP4.Text = ipList[3];
                }
            }
            string TongxinType = dt[0].TongxinType.Trim();
            string TongxinAddr = dt[0].TongxinAddr.Trim();
            if ("0".Equals(TongxinType))
            {
                rdoEmail.Checked = true;
                txtEmail.Text = TongxinAddr;
                txtWeixin.Text = "";
            }
            else
            {
                rdoWC.Checked = true;
                txtWeixin.Text = TongxinAddr;
                txtEmail.Text = "";
            }

            //string loginType = dt[0].LoginType;
            //if ("0".Equals(loginType))
            //{
            //    rdoUserName.Checked = true;
            //}
            //else
            //{
            //    rdoPinCode.Checked = true;
            //}

             string loginType = dt[0].LoginType;
            if (loginType.Equals("1"))
            {
                this.rdoUserPwdLogin.Checked = false;
                this.rdoPinCodeLogin.Checked = true;
            }
            else
            {
                this.rdoUserPwdLogin.Checked = true;
                this.rdoPinCodeLogin.Checked = false;
            }

            if (dt[0].ColLimit != null)
            {
                string colLimit = dt[0].ColLimit.ToString();
                this.txtColThreshold.Text = colLimit;
            }

            if (dt[0].PaperLimit != null)
            {
                string paperLimit = dt[0].PaperLimit.ToString();
                this.txtPaperThreshold.Text = paperLimit;
            }

            if (dt[0].EmailAddress != null)
            {
                this.txtEmailAddress.Text = dt[0].EmailAddress.ToString();
            }
            if (dt[0].ServerIP != null)
            {
                this.txtEmailServerIP.Text = dt[0].ServerIP.ToString();
            }
            if (dt[0].EmailUserName != null)
            {
                this.txtEmailUserName.Text = dt[0].EmailUserName.ToString();
            }
            if (dt[0].EmailPassword != null)
            {
                this.txtEmailPassword.Text = dt[0].EmailPassword.ToString();
            }
            if (dt[0].CorpID != null)
            {
                this.txtCorpID.Text = dt[0].CorpID.ToString();
            }
            if (dt[0].Secret != null)
            {
                this.txtSecret.Text = dt[0].Secret.ToString();
            }
            if (dt[0].AgentID != null)
            {
                this.txtAgentID.Text = dt[0].AgentID.ToString();
            }
            if (dt[0].CompanyInfo != null)
            {
                this.txtCompany.Text = dt[0].CompanyInfo.ToString();
            }

        }
        if (this.txtIP1.Text.Trim() == ""
            || this.txtIP2.Text.Trim() == ""
            || this.txtIP3.Text.Trim() == ""
            || this.txtIP4.Text.Trim() == ""
        )
        {
            IPHostEntry ipHostInfo = Dns.Resolve(Request.Url.Host);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            if (ipAddress.ToString().Equals("127.0.0.1"))
            {
                ipHostInfo = Dns.Resolve(Dns.GetHostName());
                ipAddress = ipHostInfo.AddressList[0];
            }
            string[] ipList = ipAddress.ToString().Split('.');
            if (ipList.Length > 3)
            {
                this.txtIP1.Text = ipList[0];
                this.txtIP2.Text = ipList[1];
                this.txtIP3.Text = ipList[2];
                this.txtIP4.Text = ipList[3];
            }

        }
        //txtPort.Text = GetFollowME("RawRecvPort");
        //txtFileLocation.Text = GetFollowME("FileLocation");
        //txtSPLPeriod.Text = GetFollowME("SplPeriod");
        //txtUserOper.Text = GetFollowME("OpPeriod");
        //string flag = GetFollowME("RawRecvLog");

        //if (flag == "true")
        //    ddlDebug.SelectedIndex = ddlDebug.Items.IndexOf(ddlDebug.Items.FindByValue("1"));
        //else
        //    ddlDebug.SelectedIndex = ddlDebug.Items.IndexOf(ddlDebug.Items.FindByValue("0"));
    }
    #endregion
    #region "btnReset_Click"
    /// <summary>
    /// btnReset_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2014.05.16</Date>
    /// <Author>SLC  chen youguang</Author>
    /// <Version>1.2</Version>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        InitialPage();
    }
    #endregion

    #region "btnApply_Click"
    /// <summary>
    /// btnApply_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2014.05.16</Date>
    /// <Author>SLC  chen youguang</Author>
    /// <Version>1.2</Version>
    protected void btnApply_Click(object sender, EventArgs e)
    {
        
        //ModifyFollowME("FileLocation", txtFileLocation.Text.Trim());
        //ModifyFollowME("SplPeriod", txtSPLPeriod.Text.Trim());
        //ModifyFollowME("OpPeriod", txtUserOper.Text.Trim());

        //if (txtPort.Text.Trim() != GetFollowME("RawRecvPort"))
        //{
        //    ModifyFollowME("RawRecvPort", txtPort.Text.Trim());
        //    btnRestartIIS_Click(new object(), new EventArgs());
        //}

        //if (ddlDebug.SelectedIndex == 0)
        //{
        //    ModifyFollowME("RawRecvLog", "false");
        //    UtilCommon.ModifyWebConfig("AppLog", "false");
        //}
        //else
        //{
        //    ModifyFollowME("RawRecvLog", "true");
        //    UtilCommon.ModifyWebConfig("AppLog", "true");
        //}
        if (!Page.IsValid)
        {
            return;
        }
        using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                string strSql = "";

                strSql = "   DELETE FROM SettinServerIP   " + Environment.NewLine;
                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    cmd.ExecuteNonQuery();
                }

                strSql = "";
                //1. Set MFP Group Information
                strSql = "   INSERT INTO [SettinServerIP]    " + Environment.NewLine;
                strSql += "             ([IPAddress]      " + Environment.NewLine;
                strSql += "              ,[TongxinType]      " + Environment.NewLine;
                strSql += "              ,[TongxinAddr]      " + Environment.NewLine;
                strSql += "              ,[LoginType]      " + Environment.NewLine;
                strSql += "              ,[ColLimit]      " + Environment.NewLine;
                strSql += "              ,[PaperLimit]      " + Environment.NewLine;
                strSql += "              ,[ServerIP]      " + Environment.NewLine;
                strSql += "              ,[EmailAddress]      " + Environment.NewLine;
                strSql += "              ,[EmailUserName]      " + Environment.NewLine;
                strSql += "              ,[EmailPassword]      " + Environment.NewLine;
                strSql += "              ,[CorpID]      " + Environment.NewLine;
                strSql += "              ,[Secret]      " + Environment.NewLine;
                strSql += "              ,[AgentID]      " + Environment.NewLine;
                strSql += "              ,[CompanyInfo])      " + Environment.NewLine;
                strSql += "       VALUES                     " + Environment.NewLine;
                strSql += "             ( {0}                 " + Environment.NewLine;
                strSql += "              ,{1}                 " + Environment.NewLine;
                strSql += "              ,{2}                 " + Environment.NewLine;
                strSql += "              ,{3}                 " + Environment.NewLine;
                strSql += "              ,{4}                 " + Environment.NewLine;
                strSql += "              ,{5}                 " + Environment.NewLine;
                strSql += "              ,{6}                 " + Environment.NewLine;
                strSql += "              ,{7}                 " + Environment.NewLine;
                strSql += "              ,{8}                 " + Environment.NewLine;
                strSql += "              ,{9}                 " + Environment.NewLine;
                strSql += "              ,{10}                 " + Environment.NewLine;
                strSql += "              ,{11}                 " + Environment.NewLine;
                strSql += "              ,{12}                 " + Environment.NewLine;
                strSql += "              ,{13})                 " + Environment.NewLine;
                string[] paramslist = new string[14];
                paramslist[0] = ConvertStringToSQL(this.txtIP1.Text + "." + this.txtIP2.Text + "." + this.txtIP3.Text + "." + this.txtIP4.Text);

                if (rdoEmail.Checked)
                {
                    paramslist[1] = ConvertStringToSQL(UtilConst.CON_TONGXIN_EMAIL.ToString());
                    paramslist[2] = ConvertStringToSQL(txtEmail.Text.Trim());
                }
                else 
                {
                    paramslist[1] = ConvertStringToSQL(UtilConst.CON_TONGXIN_WC.ToString());
                    paramslist[2] = ConvertStringToSQL(txtWeixin.Text.Trim());
                }
                if (rdoUserPwdLogin.Checked)
                {
                    paramslist[3] = ConvertStringToSQL("0");
                }
                else
                {
                    paramslist[3] = ConvertStringToSQL("1");
                }

                if (txtColThreshold.Text.Trim().Equals(""))
                {
                    paramslist[4] = ConvertStringToSQL("0");
                }
                else
                {
                    paramslist[4] = ConvertStringToSQL(txtColThreshold.Text.Trim());
                }
                if (txtPaperThreshold.Text.Trim().Equals(""))
                {
                    paramslist[5] = ConvertStringToSQL("0");
                }
                else
                {
                    paramslist[5] = ConvertStringToSQL(txtPaperThreshold.Text.Trim());
                }
                paramslist[6] = ConvertStringToSQL(txtEmailServerIP.Text.Trim());
                paramslist[7] = ConvertStringToSQL(txtEmailAddress.Text.Trim());
                paramslist[8] = ConvertStringToSQL(txtEmailUserName.Text.Trim());
                paramslist[9] = ConvertStringToSQL(txtEmailPassword.Text.Trim());
                paramslist[10] = ConvertStringToSQL(txtCorpID.Text.Trim());
                paramslist[11] = ConvertStringToSQL(txtSecret.Text.Trim());
                paramslist[12] = ConvertStringToSQL(txtAgentID.Text.Trim());
                paramslist[13] = ConvertStringToSQL(txtCompany.Text.Trim());


                strSql = string.Format(strSql, paramslist);

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
    }
    #endregion

    #region "btnRestartIIS_Click"
    /// <summary>
    /// restart iis
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2014.05.16</Date>
    /// <Author>SLC  chen yg</Author>
    /// <Version>1.2</Version>
    protected void btnRestartIIS_Click(object sender, EventArgs e)
    {
        //string strIP = this.txtIP1.Text.Trim() + "."
        //             + this.txtIP2.Text.Trim() + "."
        //             + this.txtIP3.Text.Trim() + "."
        //             + this.txtIP4.Text.Trim();
        //try
        //{
        //    FollowMEHandler handler = new FollowMEHandler();
        //    handler.SendMsg("restart", strIP.ToString());
        //    ModifyFollowME("OldRawRecvPort", GetFollowME("RawRecvPort"));
        //}
        //catch (Exception exc)
        //{
        //    Global.Log(exc.ToString());
        //    ErrorAlert("系统正忙，请稍候操作！");
        //}
        //System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
        //string dirBinLocation = Path.GetDirectoryName(asm.Location);

     //   string vpath = @"..\Bin";
     //   string dirBinLocation = HttpContext.Current.Server.MapPath(vpath); 

     ////   string SimpleEAFollowME = "SimpleEAFollowME";

     ////   string SimpleEAFollowMEPath = Path.Combine(dirBinLocation, SimpleEAFollowME);

     //   Process proc = null;
     //   proc = new Process();
     //   string servicePath = dirBinLocation + @"\InstallFollowME.bat";
     //   //string servicePath = SimpleEAFollowMEPath + @"\InstallFollowME.bat";
     //   proc.StartInfo.FileName = servicePath;
     //   //proc.StartInfo.CreateNoWindow = true;
     //   proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
     //   proc.Start();

        //ServiceController service = new ServiceController("SimpleEA Follow ME print");
        // 20150831  SimpleEA Follow ME print
        ServiceController service = new ServiceController("SimpleEAFollowME");
        //ServiceController service = new ServiceController("SimpleEA Follow ME print");
        try
        {
            if (service.Status == ServiceControllerStatus.Running)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running);

        }
        catch (Exception ex)
        {
            ErrorAlert(ex.Message);
        }
       

        //string strIP = this.txtIP1.Text.Trim() + "."
        //             + this.txtIP2.Text.Trim() + "."
        //             + this.txtIP3.Text.Trim() + "."
        //             + this.txtIP4.Text.Trim();

        //IPHostEntry ipHostInfo = Dns.Resolve(strIP);
        //IPAddress ipAddress = ipHostInfo.AddressList[0];
        //if (ipAddress.ToString().Equals("127.0.0.1"))
        //{
        //    ipHostInfo = Dns.Resolve(Dns.GetHostName());
        //    ipAddress = ipHostInfo.AddressList[0];
        //}

        //try
        //{
        //    FollowMEHandler handler = new FollowMEHandler();
        //    handler.SendMsg("restart", ipAddress.ToString());
        //    ModifyFollowME("OldRawRecvPort", GetFollowME("RawRecvPort"));
        //}
        //catch (Exception exc)
        //{
        //    Global.Log(exc.ToString());
        //    ErrorAlert("系统正忙，请稍候操作！");
        //}


    }
    #endregion

    #region "ModifyFollowME"
    ///<summary>
    /// 修改web.config文件appSettings配置节中的Add里的value属性
    ///</summary>
    ///<remarks>
    /// 注意，调用该函数后，会使整个WebApplication重启，导致当前所有的会话丢失
    ///</remarks>
    ///<paramname="key">要修改的键key</param>
    ///<paramname="strValue">修改后的value</param>
    ///<exceptioncref="">找不到相关的键</exception>
    ///<exceptioncref="">权限不够，无法保存到web.config文件中</exception>
    public void ModifyFollowME(string key, string strValue)
    {
        try
        {
            UtilCommon.ModifyFollowME(key, strValue);
        }
        catch (Exception ex)
        {
            ErrorAlert("WebConfig 中的配置出现错误，请及时联系管理员！");
        }
    }
    #endregion

    #region "Get Follow ME param"
    /// <summary>
    /// Get Follow ME param from config file
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetFollowME(string key)
    {
        try
        {
            return UtilCommon.GetFollowME(key);
        }
        catch (Exception ex)
        {
            ErrorAlert("WebConfig 中的配置出现错误，请及时联系管理员！");
            return "";
        }
    }

    protected void CusIPRequire_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string strIP = txtIP1.Text + "." + txtIP2.Text + "." + txtIP3.Text + "." + txtIP4.Text;


        dtMFPInformationTableAdapters.MFPInformationTableAdapter MfpInfoAdapter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();

        object o = MfpInfoAdapter.GetByIPAddress(strIP);
        if (o != null && (int)o > 0)
        {
            args.IsValid = false;

        }
        else
        {
            args.IsValid = true;

        }
    }
    #endregion

    protected void rdoEmail_CheckedChanged(object sender, EventArgs e)
    {
        txtEmail.Enabled = true;
        txtWeixin.Enabled = false;
    }
    protected void rdoWc_CheckedChanged(object sender, EventArgs e)
    {
        txtEmail.Enabled = false;
        txtWeixin.Enabled = true;
    }


}