
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
using System.Web;

namespace CommonOSA
{
    [Serializable()]
    public class OsaRequestInfo 
	{
        private string userAgent;
        private string scheme;
        private string localHost;
        private string localPort;
        private string appPath;
        private string bcResolution;

        private string appWebHost;
        protected string appWebPort;
        private string appwebAppPath;
        private string uiSessionId;
        private string alternative;
        private string deviceId;

        //--- Constructor ---
        public OsaRequestInfo(HttpRequest request)
		{
            userAgent = request.UserAgent;
            scheme = request.Url.Scheme;
            localHost = request.Url.Host;
            localPort = request.Url.Port.ToString();
            appPath = request.ApplicationPath;
            bcResolution = request.Headers["X-BC-Resolution"];

            if (null != request.Headers["X-WA-Port"])
            {
                appWebPort = request.Headers["X-WA-Port"].Split(';')[0];
            }
            else
            {
                appWebPort = "0";
            }

            uiSessionId = request.Headers["X-UISessionId"];
            alternative = request.Headers["X-BC-Alternative"];
            
            appWebHost = System.Web.Configuration.WebConfigurationManager.AppSettings["appwebhost"];
            appwebAppPath = System.Web.Configuration.WebConfigurationManager.AppSettings["appwebAppPath"];
            deviceId = request.Headers["X-DeviceId"];

        }

        // Get [scheme]://[host name]:[port number] [application path] [URL]
        public string GetAbsoluteUrl(string relUrl)
        {
            return GetAppBaseUrl() + relUrl;
        }

        //--- Set host name ---
        public void SetLocalHost(string host)
        {
            this.localHost = host;
        }

        //--- Get host name ---
        public string GetAppHostAddress()
        {
            return localHost;
        }

        //--- Get [scheme]://[host name]:[port number] [application path] ---
        public string GetAppBaseUrl()
        {
            return scheme + "://" + localHost + ":" + localPort + appPath;
        }

        //--- Get WebAPI's port number ---
        public string GetAppWebPort()
        {
            return appWebPort;
        }

        //--- Get [scheme]://[AppWeb host name]:[AppWeb port number] ---
        public string GetAppWebBaseUrl()
        {
            return "http://" + appWebHost + ":" + appWebPort + "/";
        }

        //--- Get [scheme]://[AppWeb host name]:[AppWeb port number] [AppWeb application path] ---
        public string GetAppWebUrl()
        {
            return GetAppWebBaseUrl() + appwebAppPath;
        }

        //--- Get user agent information ---
        public string GetUserAgent()
        {
            return userAgent;
        }

        //--- Get user agent information to correspond key ---
        private string GetUserAgentValue(string key)
        {
            string[] data = userAgent.Split(";".ToCharArray(), 256);
            foreach (string datum in data)
            {
                string[] pair = datum.Split("=".ToCharArray(), 2);

                if (2 == pair.Length && pair[0].Trim() == key)
                {
                    string val = pair[1].Trim();
                    return val.Substring(1, val.Length - 2);
                }
            }

            return null;
        }

        //--- Get user agent information to correspond [product-family] ---
        public string GetProductFamily()
        {
            return GetUserAgentValue("product-family");
        }

        //--- Get request parameter to correspond [X-BC-Resolution] ---
        public string GetBC_Resolution()
        {
            if (string.IsNullOrEmpty(bcResolution)) // use classic logic
            {
                string fami = GetProductFamily();
                if ("37" == fami) return "VGA";
                if ("45" == fami) return "VGA";
                if ("57" == fami) return "VGA";
                return "Half-VGA";
            }
            return "Css/" + bcResolution;
        }

        //--- Get request parameter to correspond [X-BC-Alternative] ---
        public string GetAlternative()
        {
            return alternative;
        }

        //--- Get [X-DeviceId] from argument request parameter ---
        public static string GetDeviceId(HttpRequest request)
        {
            return request.Headers["X-DeviceId"];
        }

        //--- Get request parameter to correspond [X-DeviceId] ---
        public string GetDeviceId()
        {
            return deviceId;
        }

        //--- Get request parameter to correspond [X-UISessionId] ---
        public string GetUiSessionId()
        {
            return uiSessionId;
        }
	}
}
