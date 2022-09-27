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

public partial class Settings_FollowMESetting : MainPage
{

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.01</Date>
    /// <Author>SLC  Wei Changye</Author>
    /// <Version>1.2</Version>
    protected void Page_Load(object sender, EventArgs e)
    {

        this.Master.Title = UtilConst.CON_PAGE_FOLLOWME_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_FOLLOWME_SET, "FollowMESetting.aspx", true);
        this.Master.JobReportTitle();

        // Check Access Role
        CheckUser();

        if (!IsPostBack)
        {
            InitialPage();
        }
    }
    #endregion

    #region "InitialPage"
    /// <summary>
    /// InitialPage
    /// </summary>
    /// <Date>2012.03.01</Date>
    /// <Author>SLC  Wei Changye</Author>
    /// <Version>1.2</Version>
    private void InitialPage()
    {
        //txtPort.Text = UtilCommon.GetAppSettingString("RawRecvPort");
        //txtFileLocation.Text = UtilCommon.GetAppSettingString("FileLocation");
        //txtSPLPeriod.Text = UtilCommon.GetAppSettingString("SplPeriod");
        //txtUserOper.Text = UtilCommon.GetAppSettingString("OpPeriod");
        //string flag = UtilCommon.GetAppSettingString("AppLog");

        //if (flag == "true")
        //    ddlDebug.SelectedIndex = ddlDebug.Items.IndexOf(ddlDebug.Items.FindByValue("1"));
        //else
        //    ddlDebug.SelectedIndex = ddlDebug.Items.IndexOf(ddlDebug.Items.FindByValue("0"));


        txtPort.Text = GetFollowME("RawRecvPort");
        txtFileLocation.Text = GetFollowME("FileLocation");
        txtSPLPeriod.Text = GetFollowME("SplPeriod");
        txtUserOper.Text = GetFollowME("OpPeriod");
        string flag = GetFollowME("RawRecvLog");

        if (flag == "true")
            ddlDebug.SelectedIndex = ddlDebug.Items.IndexOf(ddlDebug.Items.FindByValue("1"));
        else
            ddlDebug.SelectedIndex = ddlDebug.Items.IndexOf(ddlDebug.Items.FindByValue("0"));
    }
    #endregion

    #region "btnReset_Click"
    /// <summary>
    /// btnReset_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.01</Date>
    /// <Author>SLC  Wei Changye</Author>
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
    /// <Date>2012.03.01</Date>
    /// <Author>SLC  Wei Changye</Author>
    /// <Version>1.2</Version>
    protected void btnApply_Click(object sender, EventArgs e)
    {
        //Modify("FileLocation", txtFileLocation.Text.Trim());
        //Modify("SplPeriod", txtSPLPeriod.Text.Trim());
        //Modify("OpPeriod", txtSPLPeriod.Text.Trim());
        //Modify("RawRecvPort", txtPort.Text.Trim());

        //if (ddlDebug.SelectedIndex == 0)
        //{
        //    Modify("AppLog", "false");
        //    Modify("HttpLog", "false");
        //    Modify("RawRecvLog", "false");
        //}
        //else
        //{
        //    Modify("AppLog", "true");
        //    Modify("HttpLog", "true");
        //    Modify("RawRecvLog", "true");
        //}
        
        ModifyFollowME("FileLocation", txtFileLocation.Text.Trim());
        ModifyFollowME("SplPeriod", txtSPLPeriod.Text.Trim());
        ModifyFollowME("OpPeriod", txtUserOper.Text.Trim());

        if (txtPort.Text.Trim() != GetFollowME("RawRecvPort"))
        {
            ModifyFollowME("RawRecvPort", txtPort.Text.Trim());
            btnRestartIIS_Click(new object(), new EventArgs());
        }

        if (ddlDebug.SelectedIndex == 0)
        {
            ModifyFollowME("RawRecvLog", "false");
            UtilCommon.ModifyWebConfig("AppLog", "false");
        }
        else
        {
            ModifyFollowME("RawRecvLog", "true");
            UtilCommon.ModifyWebConfig("AppLog", "true");
        }

    }
    #endregion

    #region "btnRestartIIS_Click"
    /// <summary>
    /// restart iis
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.07</Date>
    /// <Author>SLC  Wei Changye</Author>
    /// <Version>1.2</Version>
    protected void btnRestartIIS_Click(object sender, EventArgs e)
    {
        //System.Diagnostics.Process.Start(HttpRuntime.AppDomainAppPath + "SimpleEAFollowME\\InstallFollowME.bat");
        
        // delete by Wei Changye 2012.03.31
        //try
        //{
        //    System.ServiceProcess.ServiceController control = new ServiceController("SimpleFollowME");
        //    if (!control.Status.ToString().Equals("Stopped"))
        //    {
        //        control.Stop();
        //        control.WaitForStatus(ServiceControllerStatus.Stopped);
        //    }
        //    control.Start();
        //    control.WaitForStatus(ServiceControllerStatus.Running);
        //}
        //catch (System.UnauthorizedAccessException ex)
        //{
        //    Global.Log(ex.ToString());
        //    ErrorAlert("您的账户权限无法执行此操作！");
        //}
        //catch (Exception exc)
        //{
        //    Global.Log(exc.ToString());
        //    ErrorAlert("您的账户权限无法执行此操作！");
        //}

        // add by Wei Changye 2012.03.31
        IPHostEntry ipHostInfo = Dns.Resolve(Request.Url.Host);
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        if (ipAddress.ToString().Equals("127.0.0.1"))
        {
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
        }

        try
        {
            FollowMEHandler handler = new FollowMEHandler();
            handler.SendMsg("restart", ipAddress.ToString());
            ModifyFollowME("OldRawRecvPort", GetFollowME("RawRecvPort"));
        }
        catch (Exception exc)
        {
            Global.Log(exc.ToString());
            ErrorAlert("系统正忙，请稍候操作！");
        }

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
    #endregion
}