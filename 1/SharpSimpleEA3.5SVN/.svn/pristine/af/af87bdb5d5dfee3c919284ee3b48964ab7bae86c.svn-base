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

public partial class OsaMain3 : MainOSA
{
   
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
            if (!string.IsNullOrEmpty(div_error))
            {
                return;
            }


            if ("validate" == Request.Params["status"])
            {
                if (Request.Params["id_ok"] != null)
                {
                    string id_Login = "";
                    string id_Password = "";
                    // Login Name
                    id_Login = Request.Params["id_Login"];
                    // password
                    id_Password = Request.Params["id_password"];
                    // Login
                    direct_Login(id_Login, id_Password, E_EA_OSA_TYPE.OSA30);
                }
            }
        }
        catch
        {
            div_error = "System Error! Please contect to Administrator.";
        }



        //20140618 chen add start
        if (!IsPostBack)
        {
            string iccardid = "";
            if ("iccardlogin" == Request.Params["op"])
            {
                iccardid = Request.Params["iccard_id"];
            }
            if (iccardid.Trim() != "")
            {
                try
                {
                    iccard_Login(iccardid, E_EA_OSA_TYPE.OSA30);
                    if (!string.IsNullOrEmpty(div_error))
                    {
                        ;
                    }
                }
                catch
                {
                    ;
                }
            }

        }
        //20140618 chen add end                   
    }
    #endregion
}
