
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
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CommonOSA
{
    //---------------------------------------------------------
    // BaseDeviceSession
    [Serializable()]
    public abstract class BaseDeviceSession
    {

        //--- MFP information ---
        public GetJob.Xmldocout xmlDocDsc = null;
        //--- MFP execution parameter set on screen ---
        public Dictionary<string, List<string>> currentData = null;
        //--- CSS file name ---
        public string cssFile = string.Empty;
        public OsaRequestInfo req = null;
        //--- Vendor key ---
        public string vKey = string.Empty;
        //--- Execution job ID ---
        public string jid = null;
        
        private Dictionary<string, Dictionary<string, string>> controlValue = new Dictionary<string, Dictionary<string, string>>();

        //--- Jsonp to display initial screen ---
        public string ShowScreenURL(string callBack)
        {
            return this.req.GetAppWebBaseUrl() + "mfpcommon/ShowScreen/v1?type=mfp&res=jsonp&callback=" + callBack + "&vkey=" + this.vKey;
        }

        //--- The property value corresponding to control ID is set ---
        public void SetControlValue(string key, List<WebControl> targetControl)
        {
            Dictionary<string, string> addData = new Dictionary<string, string>();
            foreach (WebControl ctl in targetControl)
            {
                addData.Add(ctl.ClientID, ctl.ToolTip);
            }

            if (controlValue.ContainsKey(key))
            {
                controlValue[key]= addData;
            }
            else
            {
                controlValue.Add(key, addData);
            }

        }

        //--- The control setting value array that corresponds to the key is gotten ---
        public Dictionary<string, string> GetControlValue(string key)
        {
            return controlValue[key];
        }

    }
}
