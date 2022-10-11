
#region Copyright SHARP Corporation
//	Copyright (c) 2010-2014 SHARP CORPORATION. All rights reserved.
//
//	Extended Sharp OSA SDK
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
using System.Text;
using CommonOSA;

public partial class GetResourcesString : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = string.Empty;
        if (Request.ContentLength > 0)
        {
            code = Request.Form[Request.Form.Keys[0]];
        }

        string msg = string.Empty;
        try
        {
            msg = GetGlobalResourceObject("OSAStrings", "ERR_" + code).ToString();
        }
        catch (NullReferenceException)
        {
            msg = Request.Params["code"].ToString();
        }
        Response.Write(msg);

    }
}
