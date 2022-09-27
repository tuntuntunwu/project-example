
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
using CommonOSA;

public partial class FatalErrorForm : BasePage
{
    protected string detail = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (null != Request.Params["msg"])
        {
            detail = Request.Params["msg"];
        }
        else
        {
            if (null != Request.Params["code"])
            {
                try
                {
                    detail = GetGlobalResourceObject("OSAStrings", "ERR_" + Request.Params["code"]).ToString();
                }
                catch (NullReferenceException)
                {
                    detail = Request.Params["code"].ToString();
                }
            }
        }

    }
}
