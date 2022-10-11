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
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.InteropServices;
using SLCRegister;
using common;

public partial class Login_Login : System.Web.UI.Page
{
    const string StartAdminUrl = "~/UserInfo/UserList.aspx";
    //const string StartUserUrl = "~/Report/AvailableReport.aspx";
    //pupeng 2014-5-30
    const string StartUserUrl = "~/UserInfo/UserInfoEdit.aspx?UserId=" ;
    //chen 2019 add
    const string StartLogview = "~/LogView/LogView.aspx";

    string ReturnUrl = string.Empty;

    //product key element
    // Add By Weichangye 2012.03.13
    public int CheckResult = 0;
    public int StartResult = 0;

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //2011.03.21 Delete By SES Jijianxiong ST
        // Bug Management Sheet_SimpleEA_110321.xls No.23
        //Helper.ConfigFileInfo infolice = new Helper.ConfigFileInfo();
        //string filelice = infolice.ExistsLicenseFile();
        //if (string.IsNullOrEmpty(filelice))
        //{
        //    Page.Response.Redirect("../LicenseErrorPage.htm");
        //    return;
        //}
        //2011.03.21 Delete By SES Jijianxiong ED

        btnLogin.Click +=new EventHandler(btnLogin_Click);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocusInFirstItemTxt", "<script>SetFocusInFirstItemTxt();</script>");
        ReturnUrl = Request.Params["returnUrl"];
        // 2010.12.27 Update By SES Zhou Miao Ver.1.1 Update ST
        this.txtPassword.Attributes.Add("onkeydown", "Login_In()");
        // 2010.12.27 Update By SES Zhou Miao Ver.1.1 Update ED
    }
    #endregion

    //#region "LoginEA_LoggedIn"
    ///// <summary>
    ///// LoginEA_LoggedIn
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Date>2010.06.14</Date>
    ///// <Author>SES Ji JianXiong</Author>
    ///// <Version>0.01</Version>
    //protected void LoginEA_LoggedIn(object sender, EventArgs e)
    //{
    //    if (LoginEA.UserName.Equals(UtilConst.CON_USER_LONINNAME))
    //    {
    //        if (ReturnUrl != null)
    //        {
    //            LoginEA.DestinationPageUrl = ReturnUrl;
    //        }
    //        else
    //        {
    //            LoginEA.DestinationPageUrl = StartAdminUrl;
    //        }
    //    }
    //    else
    //    {
    //        LoginEA.DestinationPageUrl = StartUserUrl;
    //    }

    //}
    //#endregion

    //#region "LoginEA_OnAuthenticate"
    ///// <summary>
    ///// LoginEA_OnAuthenticate
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Date>2010.06.14</Date>
    ///// <Author>SES Ji JianXiong</Author>
    ///// <Version>0.01</Version>
    //protected void LoginEA_OnAuthenticate(object sender, AuthenticateEventArgs e)
    //{
    //    LoginEA.UserName = LoginEA.UserName.Trim();

    //    // user check
    //    if (SiteSpecificAuthenticationMethod(LoginEA.UserName ,LoginEA.Password))
    //    {
    //        e.Authenticated = true;
    //    }
    //}

    //#endregion

    #region "SiteSpecificAuthenticationMethod"
    /// <summary>
    /// SiteSpecificAuthenticationMethod
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="Password"></param>
    /// <returns></returns>
    /// <Date>2010.06.14</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private bool SiteSpecificAuthenticationMethod(string UserName, string Password)
    {
        int iCount = 0;
        // Maxlength is 10
        if (UserName.Length > 30)
        {
            return false;
        }
        bool isAuthenticationed = false;

        // Insert code that implements a site-specific custom 
        // authentication method here.
        
        //dtUserInfoTableAdapters.UserInfoTableAdapter userInfo = new dtUserInfoTableAdapters.UserInfoTableAdapter();
        //iCount = (int)userInfo.Authentication(UserName, Password);
        dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
        int Login_auth_method = settingDispRow.Login_Auth_method;
        if (Login_auth_method == UtilConst.USER_LDAP && !UserName.Trim().ToLower().Equals("admin") && !UserName.Trim().ToLower().Equals("secuadmin"))
        {
            LDAPHandler ldapAuth = new LDAPHandler();
            string ret = ldapAuth.IDAuthentication(UserName, Password);
            if (ret.Equals(""))
            {
                isAuthenticationed = true;
            }
            else
            {
                isAuthenticationed = false;
            }

        }
        else
        {
            dtUserInfoTableAdapters.UserInfoTableAdapter userInfo = new dtUserInfoTableAdapters.UserInfoTableAdapter();
            iCount = (int)userInfo.Authentication(UserName, Password);
            if (iCount != 1)
            {
                return false;
            }
            // 2011.01.26 Update By SES Jijianxiong ST
            //else
            //{
            //    return true;
            //}

            dtUserInfo.UserInfoDataTable UserResult = userInfo.GetDataByLoginName(UserName);
            try
            {
                string checkusername = UserResult[0].LoginName;
                if (checkusername.Equals(UserName))
                {
                    isAuthenticationed = true;
                }
            }
            catch
            {

                ;
            }
        }



     

        return isAuthenticationed;

        // 2011.01.26 Update By SES Jijianxiong ED
    }
    #endregion


    #region "btnLogin_Click"
    /// <summary>
    /// btnLogin_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.08.18</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        txtLoginID.Text = txtLoginID.Text.Trim();
        Session.Add("txtLoginID", txtLoginID.Text);
        //test
        //String LDAPLoginName = "Administrator";
        //String LDAPPassword = "Cuiqingqing111";
        //LDAPHandler ldap = new LDAPHandler();
        //Boolean ret = ldap.IDAuthentication(LDAPLoginName, LDAPPassword);
        //test
        if (SiteSpecificAuthenticationMethod(txtLoginID.Text, txtPassword.Text))
        {
            // 2012.06.08 delete by Wei Changye 
            //if (Verify(txtLoginID.Text))
            //LoginEA_LoggedIn();

            //end

            // 2012.06.08 Add by Wei Changye 

            //chen update for test
            //check License
            //if (IsOutTrail())
            //    Response.Redirect("~/ProductKeyRegister/RegisterTipsPage.aspx", false);
            //else
            LoginEA_LoggedIn();
            //LoginEA_LoggedIn();
            //end
        }
        else
        {
            showError("Login_ErrorShow");
        }
    }
    #endregion

    #region "LoginEA_LoggedIn"
    /// <summary>
    /// LoginEA_LoggedIn
    /// </summary>
    /// <Date>2010.08.18</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void LoginEA_LoggedIn()
    {
        string URL = string.Empty;

        FormsAuthenticationTicket authenticationTicket;

        authenticationTicket = new FormsAuthenticationTicket(1, txtLoginID.Text, DateTime.Now, DateTime.Now.AddMinutes(UtilConst.CON_FORM_TIMEOUT), false, txtLoginID.Text);

        // Encrypt the ticket.
        string encTicket = FormsAuthentication.Encrypt(authenticationTicket);

        // Create the cookie.
        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));


        if (txtLoginID.Text.Equals(UtilConst.CON_USER_LONINNAME))
        {
            if (ReturnUrl != null)
            {
                URL = ReturnUrl;
            }
            else
            {
                URL = StartAdminUrl;
            }
        }
        else if (txtLoginID.Text.Equals(UtilConst.CON_USER_SECUADMIN))
        {
            URL = StartLogview;
        }
        else
        {

            // Get User's UserID.
            dtUserInfoTableAdapters.UserInfoTableAdapter adapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
            dtUserInfo.UserInfoDataTable dt = adapter.GetDataByLoginName(txtLoginID.Text);
            URL = StartUserUrl + ((dtUserInfo.UserInfoRow)dt.Rows[0]).ID.ToString();
        }

        Page.Response.Redirect(URL, false);

    }
    #endregion


    #region "Show Error Message."
    /// <summary>
    /// Show Error Message. (Display with new UI)
    /// </summary>
    /// <param name="jsfun"></param>
    /// <Date>2010.08.18</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void showError(string jsfun)
    {
        string strScript = "";
        strScript = "    <script language='javascript' type='text/javascript'>" + "\r\n";
        if (jsfun.IndexOf("(") > 0)
        {
            strScript += "     " + jsfun + ";" + "\r\n";
        }
        else
        {
            strScript += "     " + jsfun + "(); " + "\r\n";
        }
        strScript += "    </script>" + "\r\n";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "showError", strScript);
    }
    #endregion


    // product key element
    // Add By Weichangye 2012.03.13

    #region "Verify if Product Key Exist"
    /// <summary>
    /// Verify if Product Key Exist
    /// </summary>
    /// <param name="jsfun"></param>
    /// <Date>2011.10.26/Date>
    /// <Author>SLC lijing</Author>
    /// <Version>0.01</Version>
    public bool Verify(string username)
    {

        //初始化注册模块为“SimpleEA”；   
        LKCclass.LKC_Initiate('Y');
        CheckResult = LKCclass.LKC_Check();
        StartResult = LKCclass.LKC_Start();

        Global.Log("Login Verify checkresult ：" + CheckResult.ToString());
        Global.Log("Login Verify startresult ：" + StartResult.ToString());

        if (CheckResult == Convert.ToInt32(LKCclass.ResultCode.LKC_S_OK))
        {
            //1.正式版本已注册！   
            return true;
        }
        else if (StartResult == Convert.ToInt32(LKCclass.ErrorCode.LKC_E_NO_REGISTERED_KEY))
        {   //2.系统未注册！        
            RegisterPK("系统未注册，请联系您的产品经销商，申请Simple EA注册码！", true);
            return false;
        }
        else if (StartResult == Convert.ToInt32(LKCclass.ErrorCode.LKC_E_ALREADY_IN_EVALUATION))
        {
            //3.曾经注册过试用版本，开始判断注册码是否过期
            if (CheckResult == Convert.ToInt32(LKCclass.ResultCode.LKC_S_EXPIRED))
            {
                //4.已过期;
                if (username == UtilConst.CON_USER_LONINNAME)
                {
                    RegisterPK("试用期已到，为避免影响您的使用，请联系您的产品经销商，申请Simple EA注册码！", true);
                    //直接进入已过期提示，输入注册码注册界面    ！     
                    return false;
                }
                else
                {   //仅提示过期！                   
                    RegisterPK("试用期已到，为避免影响您的使用，请联系管理员进行注册！", false);
                    return false;
                }

            }
            else if (CheckResult >= 1 && CheckResult <= LKCclass.ExpDays)
            {
                // 5.试用版还未过期，进入剩余天数显示！，注册码输入界面！
                if (username == UtilConst.CON_USER_LONINNAME)
                {
                    // add by slc lijing 2011.10.27 st
                    FormsAuthenticationTicket authenticationTicket;
                    authenticationTicket = new FormsAuthenticationTicket(1, txtLoginID.Text, DateTime.Now, DateTime.Now.AddMinutes(UtilConst.CON_FORM_TIMEOUT), false, txtLoginID.Text);
                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(authenticationTicket);
                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    //add by slc lijing 2011.10.27 ed                    
                    RegisterPK("若要继续使用Simple EA，请联系您的产品经销商，申请Simple EA注册码！", true);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                //出现其他错误！
                //this.labeltip.Visible = true;
                //this.labeltip.Text = "注册有误，请联系管理员！";
                Global.Log("Login Verify ：error!!");
                return false;
            }
        }
        return true;
    }
    #endregion

    #region "Into Register Page"
    /// <summary>
    /// Into Register Page
    /// </summary>
    /// <Date>2011.10.26</Date>
    /// <Author>SLC lijing</Author>
    /// <Version>0.01</Version>

    protected void RegisterPK(string MessageTips, bool isadmin)
    {
        string URL = string.Empty;
        URL = "~/ProductKeyRegister/RegisterTipsPage.aspx" + "?CheckResult=" + CheckResult.ToString() + "&StartResult=" + StartResult.ToString() + "&MessageTips=" + MessageTips + "&IsAdmin=" + isadmin;
        Page.Response.Redirect(URL, false);
        //return true;
    }

    #endregion

    //product key end

    // 2012.06.07 add by Wei Changye License

    private bool IsOutTrail()
    {
        RegisterHandler.Initiate("A");
        KeyStatus ks = RegisterHandler.Check();
        int leftDays = RegisterHandler.GetDeadline();
        Session["KeyStatus"] = ks;
        Session["leftDays"] = leftDays;

        if (ks.Equals(KeyStatus.inTrial) && leftDays < 30)
            return true;
        else
            if (ks.Equals(KeyStatus.NotRegister) || ks.Equals(KeyStatus.outTrial))
                return true;
            else
                return false;
    }

    //end
}
