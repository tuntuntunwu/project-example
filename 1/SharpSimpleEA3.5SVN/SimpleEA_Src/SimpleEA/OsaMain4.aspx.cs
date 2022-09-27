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


public partial class OsaMain4 : MainOSA
{
    // Control for error message display.
    protected ErrorDisp div_errordisp;
    private bool iccardflg = false;
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

        //chen 20140618 add
        //Application.Clear();
        //Session.Clear();
        //string sn  = Application["strserialNumber"].ToString();
        //handler.MFPExit(GetHidSN());
        //Application.Clear();
        //Session.Clear();
        //

        div_errordisp = new ErrorDisp(span_msg);

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
                // Add By SLC.zhoumiao 2011.05.25 ST
                IClogin_height.Visible = false;
                // Update By SLC.zhoumiao 2011.05.25 ED
                               
            }
            else
            {
                div_ICCard.Visible = false;
                div_Input.Visible = true;
                btnInput.Visible = false;
                Input_disp.Visible = false;
                // Add By SLC.zhoumiao 2011.05.25 ST
                IClogin_height.Visible = true;
                // Update By SLC.zhoumiao 2011.05.25 ED
            }

            //20140618 chen add start
            string iccardid = "";
            if ("iccardlogin" == Request.Params["op"])
            {
                iccardid = Request.Params["iccard_id"];
            }
            if (iccardid.Trim() != "")
            {
                try
                {
                    iccard_Login(iccardid, E_EA_OSA_TYPE.OSA40);
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
            //20140618 chen add end                   

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
        //20170905 chen add for ldap auth and db auth
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
                //DBAuthHandler dbAuth = new DBAuthHandler();
                //Boolean ret = dbAuth.DBLogin(txtLoginName.Text, txtPassword.Text);
                //if (ret)
                //{
                //    DB_Login(txtLoginName.Text, txtPassword.Text, E_EA_OSA_TYPE.OSA40);

                //    if (!string.IsNullOrEmpty(div_error))
                //    {
                //        div_errordisp.InnerText = div_error;
                //    }

                //}
                //else
                //{
                //    div_errordisp.InnerText = "用户名和密码错误，请重新输入！";

                //}
            }




            string iccardid = InputID.Text;
            InputID.Text = "";
            
            //用户名，密码，LDAP进行认证
            //LDAPHandler ldap = new LDAPHandler();
            //string loginname = txtLoginName.Text;
            //string pwd = txtPassword.Text;

            //Boolean ret = ldap.IDAuthentication(loginname, pwd);
            //if (ret == false)
            //{
            //    div_errordisp.InnerText = "用户名密码LDAP认证没通过，请重输入.";
            //    return;
            //}

            //direct_Login(txtLoginName.Text, txtPassword.Text, E_EA_OSA_TYPE.OSA40);

            //if (!string.IsNullOrEmpty(div_error))
            //{
            //    div_errordisp.InnerText = div_error;
            //}
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
    /// 
    //ADD BY ZHENGWEI 2013.12.28
    public  string getTableStirng(string iccardid)
       {
           string after = "";
           for (int i = 0; i < iccardid.Length; i++)
           {
               string a = iccardid.Substring(i, 1);
               char b = Convert.ToChar(a);
               if ((b >= 'a' && b <= 'z') || (b >= 'A' && b <= 'Z') || a == "`" ||(b >= '0' && b<='9'))
               {
                   if (a == "`") after += "0";
                   else if (a == "a") after += "1";
                   else if (a == "b") after += "2";
                   else if (a == "c") after += "3";
                   else if (a == "d") after += "4";
                   else if (a == "e") after += "5";
                   else if (a == "f") after += "6";
                   else if (a == "g") after += "7";
                   else if (a == "h") after += "8";
                   else if (a == "i") after += "9";
                   else after += a;
               }
           }
           return after;
       }
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
            string test;
            test = getTableStirng(iccardid);
            iccardid = test;
            //byte[] array = new byte[1];   //定义一组数组array
            //array = System.Text.Encoding.ASCII.GetBytes(string); //string转换的字母
            //int asciicode = (short)(array[0]); 
            //InputID.Text = Convert.ToString(asciicode); //将转换一的ASCII码转换成string型
            //String tempstring = string.Empty;
            //foreach ( char c in iccardid ) 
            //{
            //    if (Char.IsLetter(c))
            //    {
            //        tempstring = tempstring + ((short)c).ToString();
            //    } 
            //    else 
            //    {
            //     tempstring = tempstring + (c).ToString();

            //    }
            // }
            
            //ADD BY ZHENGWEI 2013.12.28



             



            //ADD BY ZHENGWEI 2013.10.15
            if (iccardid.IndexOf("\n") != -1)
            {
                iccardid = iccardid.Replace("\n", "");
            }
            if (iccardid.IndexOf("\r") != -1)
            {
                iccardid = iccardid.Replace("\r", "");
            }
            //END
            InputID.Text = "";
            iccard_Login(iccardid, E_EA_OSA_TYPE.OSA40);
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
