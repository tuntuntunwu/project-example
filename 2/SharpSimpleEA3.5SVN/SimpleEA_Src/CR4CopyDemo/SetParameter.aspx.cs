
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

public partial class SetParameter : BasePage
{
    protected DeviceSession currentSession = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (null == prop)
        {
            // No session information is found. Try to load from a file.
            Helper.Load(Request);
            currentSession = Helper.GetSession(Request);
            if (null == currentSession)
            {
                return; // Unrecoverable
            }
            prop = new JobSettableProp(currentSession);
        }

        foreach (string key in Request.Form.AllKeys)
        {
            prop.SetPropValue(key, Request.Form[key]);
            //--- Save session information to file ---
            Helper.Save(Request);
        }
    }
}
