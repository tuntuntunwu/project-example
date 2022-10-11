
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
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonOSA;

public partial class _Default : BasePage
{
    protected string requestGetDeviceSettingsURL;
    protected string requestGetJobSettableElementsURL;
    private const string MAINPAGE = "Main.aspx";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            //--- Get session information ---
            DeviceSession s = Helper.GetSession(Request);
            //--- Deserialize JobSettableElements's results ---
            Helper.JsonDeSerialize(this.JobSettableElements.Value, s);

            s.OSAVersion = float.Parse(this.OSAVersion.Value);
            if (null == prop)
            {
                prop = new JobSettableProp(s);
            }

            s.SaddleStitchFinisherExist = false;
            GetJob.Property propStaple = (GetJob.Property)prop.GetProperty(OSACopy.PropName.STAPLE.value);
            if (propStaple != null)
            {
                int index = Array.IndexOf(propStaple.allowedValueList, OSACopy.PropValue.STAPLE.SADDLE_STITCH);
                if (index != -1)
                {
                    s.SaddleStitchFinisherExist = true;
                }
            }
            if ((s.SaddleStitchFinisherExist == true) && (s.OSAVersion >= 4.5))
            {
                Helper.SaddleStitchInit(s, prop);
            }

            //--- Set session information ---
            Helper.SetSession(Request, s);

            //--- Save session information to file ---
            Helper.Save(Request);

            Response.Redirect(MAINPAGE, false);
        }
        else
        {
            OsaRequestInfo reqinfo;
            reqinfo = new OsaRequestInfo(Request);

            // You should check whether the remote user is an OpenSystems enabled MFP.
            // Or an exception is thrown because they has not DeviceID.
            // You can use a utility method OsaRequestInfo.IsOpenSystems() for this check.

            //Create a MfpDeviceSession object for this DeviceID
            string errPage = string.Empty;
            DeviceSession s = null;

            if (!Helper.Create(reqinfo, Request.PhysicalApplicationPath, ref errPage, out s))
            {
                Response.Redirect(errPage);
                return;

            }

            //--- Get css file name ---
            s.cssFile = s.req.GetBC_Resolution();
            //--- Set session information ---
            Helper.SetSession(Request, s);
            requestGetDeviceSettingsURL = s.GetDeviceSettingsURL("GetDeviceSettingsCallback");
            requestGetJobSettableElementsURL = s.GetJobSettableElementsURL("GetJobSettableElementsCallback");
        }
    }
}
