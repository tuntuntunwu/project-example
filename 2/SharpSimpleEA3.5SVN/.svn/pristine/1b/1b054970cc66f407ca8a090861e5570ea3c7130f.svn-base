
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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using CommonOSA;

//---------------------------------------------------------
// DeviceSession
[Serializable()]
public class DeviceSession : BaseDeviceSession
{
    // --- 选纸 ---
    public string originalInputTrayLabel = string.Empty;
    // --- 双面复印 ---
    public string originalSingleDoubleLabel = string.Empty;
    // --- 倍率 ---
    public string originalRatioLabel = string.Empty;
    //复印浓度
    public string originalExposureLabel = string.Empty;
    public string originalExposureLVLLabel = string.Empty;

    //

    //--- Original size's parameter value ---
    public string originalSizeLabel = string.Empty;

    //--- Original SaddleStitchBinding parameter value ---
    public string originalSaddleStitchBinding = string.Empty;

    //--- OSA version
    public float OSAVersion;

    //--- OSA version
    public Boolean SaddleStitchFinisherExist;
    
    //--- Login User Name
    public string UserName;
    
    //--- URL to call GetDeviceSettings ---
    public string GetDeviceSettingsURL(string callBack)
    {
        return this.req.GetAppWebBaseUrl() + "mfpcommon/GetDeviceSettings/v1?res=jsonp&callback=" + callBack + "&vkey=" + this.vKey;
    }

    //--- URL to call GetJobSettableElements ---
    public string GetJobSettableElementsURL(string callBack)
    {
        return this.req.GetAppWebUrl() + "/GetJobSettableElements/v1?res=jsonp&callback=" + callBack + "&vkey=" + this.vKey;
    }

    //--- URL to call ExecuteJob ---
    public string ExecuteJobURL(string callBack, string key)
    {
        string paramsurl = HttpUtility.UrlEncode(this.req.GetAppBaseUrl() + "/GetJobParams.aspx?key=" + HttpUtility.UrlEncode(key));
        return this.ExecuteJobURL(callBack) + "&paramsurl=" + paramsurl;
    }

    //--- URL to call ExecuteJob ---
    public string ExecuteJobURL(string callBack)
    {
        return this.req.GetAppWebUrl() + "/ExecuteJob/v1?res=jsonp&callback=" + callBack + "&vkey=" + this.vKey;
    }

    //--- URL to call GetJobStatus ---
    public string GetJobStatusURL(string callBack, string jobId)
    {
        return this.req.GetAppWebUrl() + "/GetJobStatus/v1?res=jsonp&callback=" + callBack + "&vkey=" + this.vKey + "&jobid=" + jobId;
    }

    //--- URL to call CancelJob ---
    public string CancelJobURL(string callBack, string jobId)
    {
        return this.req.GetAppWebUrl() + "/CancelJob/v1?res=jsonp&callback=" + callBack + "&vkey=" + this.vKey + "&jobId=" + jobId;
    }

    //--- URL to call CheckParams ---
    public string CheckParamsURL(string callBack)
    {
        return this.req.GetAppWebUrl() + "/CheckParams/v1?res=jsonp&callback=" + callBack + "&vkey=" + this.vKey + GetRequestParams();
    }

    //--- URL to call GetLoginUser ---
    public string GetLoginUserURL(string callBack)
    {
        return this.req.GetAppWebBaseUrl() + "mfpcommon/GetLoginUser/v1?res=jsonp&callback=" + callBack + "&vkey=" + this.vKey;
    }
    
    private string GetRequestParams()
    {
        // Convert to job parameter for paramsurl
        SetJob.SetJobElements doc = Helper.ConvertToSetJobElements(this);
        StringBuilder requestParams = new StringBuilder();
        // create request parameters
        foreach (SetJob.Property prop in doc.complex[0].property)
        {
            requestParams.Append("&" + prop.sysname + "=" + prop.Value);
        }
        return requestParams.ToString();
    }
}
