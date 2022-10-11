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
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO; 
using Osa.Util;
using SesMiddleware;
using Osa.MfpWebService;
using DAL;
using common;
public partial class Main : MainOSA
{
    // Control for error message display.
    protected ErrorDisp div_errordisp;
    protected void Page_Load(object sender, EventArgs e)
    {


        // 2010.12.09 Update By SES Jijianxiong Ver.1.1 Update ST
        //Helper.DeviceSession dev = GetDeviceSession(Request.Params["DeviceId"]);

        //div_errordisp = new ErrorDisp(span_msg);

        //// err_msg which is displayed in the Default page.
        //string err_msg = string.Empty;
        //if (null != dev)
        //{
        //    err_msg = dev.ErrMsg;
        //}

        //if (!string.IsNullOrEmpty(err_msg))
        //{
        //    div_errordisp.InnerText = err_msg;
        //}
        //else
        //{
        //    div_errordisp.InnerText = "";
        //}
        
        
       div_errordisp = new ErrorDisp(span_msg);
       ////chen add start
       //if ("iccardlogin" == Request.Params["op"])
       //{
       //    string iccardid = Request.Params["iccard_id"];
       //    Login_Card(iccardid);
       //    //return;
       //}
       ////chen add end

        if (string.IsNullOrEmpty(div_error))
        {
            div_errordisp.InnerText = "";
        }
        else
        {
            div_errordisp.InnerText = div_error;
            return;
        }
        // 2010.12.09 Update By SES Jijianxiong Ver.1.1 Update ED


        // For Ic Card loginIn In Simple EA version 1.2.2

        if (status.Value == "Login_Click")
        {

            Login_Click(sender, e);
            //status.Value = "";
        }
        ////20140618 chen add start
        //else
        //if ("iccardlogin" == Request.Params["op"])
        //{
        //    string iccardid = Request.Params["iccard_id"];
        //   try
        //   {
        //       iccard_Login(iccardid, E_EA_OSA_TYPE.OSAMAIN);
        //       if (!string.IsNullOrEmpty(div_error))
        //       {
        //           div_errordisp.InnerText = div_error;
        //       }
        //   }
        //   catch
        //   {

        //       div_errordisp.InnerText = "System Error! Please contect to Administrator.";
        //   }
        //}
        ////20140618 chen add end


        status.Value = "";

        if (!IsPostBack)
        {
            // When the set in web.config is use the IC Card
            if (UtilCommon.GetICCardFlg)
            {
                // Password and login name input textbox is unvisible.
                //Input_disp.Visible = true;
                div_ICCard.Visible = true;
                div_Input.Visible = false;

                // 2012.08.22 Update By Wei CHangye
                //btnInput.Visible = true;
                if (UtilCommon.GetNormalLoginFlg)
                {
                    btnInput.Visible = true;
                    Input_disp.Visible = true;
                }
                else
                {
                    Input_disp.Visible = false;
                    btnInput.Visible = false;
                }
                // end Update

                // Update By SES.JiJianxiong 2010.09.03 ST
                // btnInput.Text = UtilConst.CON_ITEM_MFP_STATUS_INPUT;
                btnInput.ImageUrl = "Images_mfp/DirectLogin.png";
                // Update By SES.JiJianxiong 2010.09.03 ED
            }
            else
            {
                div_ICCard.Visible = false;
                div_Input.Visible = true;
                btnInput.Visible = false;
                Input_disp.Visible = false;
            }
            string iccardid = "";
            if ("iccardlogin" == Request.Params["op"])
            {
                iccardid = Request.Params["iccard_id"];
            }
            if (iccardid.Trim() != "")
            {
                try
                {
                    iccard_Login(iccardid, E_EA_OSA_TYPE.OSAMAIN);
                    if (!string.IsNullOrEmpty(div_error))
                    {
                        div_errordisp.InnerText = div_error;
                    }
                }
                catch
                {

                    div_errordisp.InnerText = "System Error! Please contect to Administrator.";
                }
            }
        }
    }


    #region "The Button Change the Status between Input Status and IC Card Status."
    /// <summary>
    /// The Button Change the Status between Input Status and IC Card Status.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnInput_Click(object sender, EventArgs e)
    {
        // Update By SES.JiJianxiong 2010.09.03 ST
        // if (btnInput.Text.Equals(UtilConst.CON_ITEM_MFP_STATUS_INPUT))
        if (btnInput.ImageUrl.Equals("Images_mfp/DirectLogin.png"))
        // Update By SES.JiJianxiong 2010.09.03 ED
        {
            div_ICCard.Visible = false;
            div_Input.Visible = true;
            // Update By SES.JiJianxiong 2010.09.03 ST
            //btnInput.Text = UtilConst.CON_ITEM_MFP_STATUS_IC;
            btnInput.ImageUrl = "Images_mfp/IC_cardLogin.png";
            // Update By SES.JiJianxiong 2010.09.03 ED
            txtLoginName.Text = "";
            txtPassword.Text = "";
            InputID.Text = "";

        }
        else
        {
            div_ICCard.Visible = true;
            div_Input.Visible = false;
            // Update By SES.JiJianxiong 2010.09.03 ST
            // btnInput.Text = UtilConst.CON_ITEM_MFP_STATUS_INPUT;
            btnInput.ImageUrl = "Images_mfp/DirectLogin.png";
            // Update By SES.JiJianxiong 2010.09.03 ED
            txtLoginName.Text = "";
            txtPassword.Text = "";
            InputID.Text = "";
        }
    }
    #endregion

    #region "btnLogIn_Click by input login name and password"
    /// <summary>
    /// btnLogIn_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.20</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        // 2010.12.09 Update By SES Jijianxiog ST
        //// Login Name is Must.
       //if (string.IsNullOrEmpty(txtLoginName.Text))
       //{
       //    div_errordisp.InnerText = UtilConst.MSG_LOGIN_NAMEMUST;
       //    return;
       //}

       //// Passwrod is Must.
       //if (string.IsNullOrEmpty(txtPassword.Text))
       //{
       //    div_errordisp.InnerText = UtilConst.MSG_PASSWORD_NAMEMUST;
       //}

       //Helper.SimpleEAUser user = new Helper.SimpleEAUser(txtLoginName.Text, txtPassword.Text);
       //if (user.isAuthorized)
       //{
       //    LoginMFP(user.accountid);
       //    return;
       //}

       //div_errordisp.InnerText = UtilConst.MSG_MFP_LOGIN_ERROR;
       //return;

        try
        {
            string iccardid = InputID.Text;
            InputID.Text = "";
            //direct_Login(txtLoginName.Text, txtPassword.Text, E_EA_OSA_TYPE.OSAMAIN);
            dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
            int Login_auth_method = settingDispRow.Login_Auth_method;
            if (Login_auth_method == UtilConst.USER_SYS)
            {
                direct_Login(txtLoginName.Text, txtPassword.Text, E_EA_OSA_TYPE.OSA40);

                if (!string.IsNullOrEmpty(div_error))
                {
                    div_errordisp.InnerText = div_error;
                }

            }
            else if (Login_auth_method == UtilConst.USER_LDAP)
            {
                LDAPHandler ldapAuth = new LDAPHandler();
                string ret = ldapAuth.IDAuthentication(txtLoginName.Text, txtPassword.Text);
                if (ret.Equals(""))
                {
                    LDAP_Login(txtLoginName.Text, txtPassword.Text, E_EA_OSA_TYPE.OSA40);
                    if (!string.IsNullOrEmpty(div_error))
                    {
                        div_errordisp.InnerText = div_error;
                    }
                }
                else
                {
                    div_errordisp.InnerText = "用户名和密码错误，请重新输入！";

                }

            }
            else
            {

            }



            if (!string.IsNullOrEmpty(div_error))
            {
                div_errordisp.InnerText = div_error;
            }
        }
        catch
        {

            div_errordisp.InnerText = "System Error! Please contect to Administrator.";
        }
        // 2010.12.09 Update By SES Jijianxiog ED

    }
    #endregion

    #region "Login_Click by IcCard"
    /// <summary>
    /// btnLogIn_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.20</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Login_Click(object sender, EventArgs e)
    
    {
        // 2010.12.09 Update By SES Jijianxiog ST
        //string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
       //string iccardid = InputID.Text;
       //InputID.Text = "";
       //string UserName = "";
       //string Password = "";

       //int nPos;
       //string UserInfo = ICCardData.GetUserInfo(iccardid, serverPath);

       //if (UserInfo.Length > 0)
       //{
       //    nPos = UserInfo.IndexOf(",");
       //    UserName = UserInfo.Substring(0, nPos);
       //    Password = UserInfo.Substring(nPos + 1);

       //    Helper.SimpleEAUser user = new Helper.SimpleEAUser(UserName, Password);
       //    if (user.isAuthorized)
       //    {
       //        LoginMFP(user.accountid);
       //        return;
       //    }
       //    else
       //    {
       //        // Login Error.
       //        div_errordisp.InnerText = UtilConst.MSG_MFP_LOGIN_ERROR;
       //    }
       //}
       //else
       //{
       //    // Card ID not exist in the UserInfo.XML file.
       //    div_errordisp.InnerText =string.Format(UtilConst.MSG_LOGIN_CARD_NOTEXIST , iccardid);
       //}
       //return;
        try
        {
            string iccardid = InputID.Text;
            InputID.Text = "";
            iccard_Login(iccardid, E_EA_OSA_TYPE.OSAMAIN);
            if (!string.IsNullOrEmpty(div_error))
            {
                div_errordisp.InnerText = div_error;
            }
        }
        catch 
        {

            div_errordisp.InnerText = "System Error! Please contect to Administrator.";
        }
        // 2010.12.09 Update By SES Jijianxiog ED
    }
    #endregion

    ////chen add start
    //#region "Login_Card by IcCard"
    ///// <summary>
    ///// btnLogIn_Click
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Date>2010.07.20</Date>
    ///// <Author>SES chen youguang</Author>
    ///// <Version>0.01</Version>
    //protected void Login_Card(string iccardid)
    //{
    //    try
    //    {
    //        iccard_Login(iccardid, E_EA_OSA_TYPE.OSAMAIN);
    //        if (!string.IsNullOrEmpty(div_error))
    //        {
    //            div_errordisp.InnerText = div_error;
    //        }
    //    }
    //    catch
    //    {

    //        div_errordisp.InnerText = "System Error! Please contect to Administrator.";
    //    }
    //}
    //#endregion

    ////chen add end
    // 2010.12.09 Delete By SES Jijianxiog ST
    //#region "GetDeviceSession"
    ///// <summary>
    ///// GetDeviceSession
    ///// </summary>
    ///// <param name="devid"></param>
    ///// <returns></returns>
    ///// <Date>2010.07.20</Date>
    ///// <Author>SES Ji JianXiong</Author>
    ///// <Version>0.01</Version>
    //private Helper.DeviceSession GetDeviceSession(string devid)
    //{
    //   string[] strid = devid.Split(',');
    //   string str_uisid = string.Empty;
    //   if (1 < strid.Length)
    //   {
    //       str_uisid = strid[1].ToString();
    //   }
    //   else
    //   {
    //       str_uisid = devid;
    //   }
    //   return Helper.DeviceSession.Get(str_uisid);
    //}
    //#endregion


    //#region "Login in to the MFP"
    ///// <summary>
    ///// Login in to the MFP
    ///// </summary>
    ///// <param name="accid"></param>
    ///// <Date>2010.07.20</Date>
    ///// <Author>SES Ji JianXiong</Author>
    ///// <Version>0.01</Version>
    //private void LoginMFP(string accid)
    //{

    //    // Get DeviceID
    //    string devid = Request.Params["DeviceId"];

    //    string[] strid = devid.Split(',');
    //    string str_uisid = string.Empty;

    //    if (1 < strid.Length)
    //    {
    //        str_uisid = strid[1].ToString();
    //    }
    //    else
    //    {
    //        str_uisid = devid;
    //    }

    //    Helper.DeviceSession dev = null;
    //    try
    //    {
    //        dev = Helper.DeviceSession.Get(str_uisid);
    //        if (null != dev)
    //        {
    //            dev.ErrMsg = string.Empty;
    //            dev.LogUserIn(accid);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        div_errordisp.InnerText = ex.Message;
    //        return;
    //    }


    //}
    //#endregion
    // 2010.12.09 Delete By SES Jijianxiog ED

    #region "Error Display"
    /// <summary>
    /// Error Display
    /// </summary>
    /// <Date>2010.09.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public class ErrorDisp
    {
        TextBox span;

        public ErrorDisp(TextBox _span)
        {
            span = _span;
        }

        public string  InnerText {
            set {
                span.Text = value.Trim();
            }
        }
    }

    #endregion
}
