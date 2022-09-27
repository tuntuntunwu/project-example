#region Copyright SHARP Corporation
//	copyright (c) 2010 SHARP CORPORATION. All rights reserved.
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
using System.Xml;
using System.Security;
using System.Diagnostics;
using Osa.MfpWebService;
using Osa.Util;

/// <summary>
/// Summary of LoginManager 
/// </summary>
public class LoginManager
{
    public LoginManager()
	{
	}

    public SCREEN_INFO_TYPE GetScreenInfoType(string accid, string devid, LoginScreenType p_tyLoginSc)
    {
        SCREEN_INFO_TYPE tyScInfo = new SCREEN_INFO_TYPE();
        //Default set
        p_tyLoginSc.MainMode = "HOME";
        tyScInfo.mainmode = p_tyLoginSc.MainMode;
        if (!p_tyLoginSc.SubMode.Equals("DEFAULT"))
            tyScInfo.submode = p_tyLoginSc.SubMode;
        else
            tyScInfo.submode = string.Empty;
        Helper.ConfigFileInfo configInfo = new Helper.ConfigFileInfo();
        if (!configInfo.UseLoginScreen())
        {
            return tyScInfo;
        }
        if (string.IsNullOrEmpty(accid))
        {
            return tyScInfo;
        }

        if (p_tyLoginSc.FixedScreen)
        {            
            return tyScInfo;
        }

        return tyScInfo;
    }

}
