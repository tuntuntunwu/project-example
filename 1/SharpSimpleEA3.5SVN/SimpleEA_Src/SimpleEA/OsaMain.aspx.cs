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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SesMiddleware;
using DAL;
using common;
public partial class OsaMain : MainOSA
{
    //web.config flg for IC Card Login or Name/Password login.
    protected Boolean  ICCardFlg = UtilCommon.GetICCardFlg;
    // In the IC Card Login in web.config
    // flg for IC Card Login screen or Name/Password login screen.
    protected Boolean ICInput = true;

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.27</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if ("validate" == Request.Params["status"])
            {

                if (Request.Params["id_ok"] != null)
                {
                    // If the web.config in IC Card Login.
                    if (!string.IsNullOrEmpty(Request.Params["hidICCardFlg"]))
                    {
                        // Get screen's status.
                        string hidICCardFlg = Request.Params["hidICCardFlg"];
                        if ("true".Equals(hidICCardFlg))
                        {
                            // IC Card Login screen
                            ICCardFlg = true;
                            ICInput = true;
                        }
                        else
                        {
                            // Name/Password login screen.
                            ICCardFlg = false;
                            ICInput = false;
                        }
                    }

                    if (ICCardFlg != true)
                    {
                        string id_Login = "";
                        string id_Password = "";
                        // Login Name
                        id_Login = Request.Params["id_Login"];
                        // password
                        id_Password = Request.Params["id_password"];
                        // Login

                        dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
                        int Login_auth_method = settingDispRow.Login_Auth_method;
                        if (Login_auth_method == UtilConst.USER_SYS)
                        {
                            direct_Login(id_Login, id_Password, E_EA_OSA_TYPE.OSA30);
                        }
                        else if (Login_auth_method == UtilConst.USER_LDAP)
                        {
                            LDAPHandler ldapAuth = new LDAPHandler();
                            string ret = ldapAuth.IDAuthentication(id_Login, id_Password);
                            if (ret.Equals(""))
                            {
                                LDAP_Login(id_Login, id_Password, E_EA_OSA_TYPE.OSA30);
                                if (!string.IsNullOrEmpty(div_error))
                                {
                                    div_error = div_error;
                                }
                            }
                            else
                            {
                                div_error = "用户名和密码错误，请重新输入！";

                            }
                        }
                    }
                    else
                    {
                        string id_card = "";
                        // IC_Card
                        id_card = Request.Params["id_ic"];
                        // Login
                        iccard_Login(id_card,E_EA_OSA_TYPE.OSA30);
                    }
                }
                else if (!string.IsNullOrEmpty(Request.Params["btnInput"]))
                {
                    //Disply Name/Password login screen.
                    ICCardFlg = false;
                    ICInput = false;
                }
                else if (!string.IsNullOrEmpty(Request.Params["btnReturn"]))
                {
                    //Disply IC Card Login screen.
                    ICCardFlg = true;
                    ICInput = true;
                }
            }
        }
        catch
        {
            div_error = "System Error! Please contect to Administrator.";
        }
    }
    #endregion
}
