#region Copyright SHARP Corporation
//
//	SHARP OSA SDK
//	DirectOsaPrint Application
//
//	Copyright (c) 2005-2011 SHARP CORPORATION. All rights reserved.
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER "AS IS" AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
//
// Global.asax.cs
//
// Contains global definitions and processings for DirectOsaPrint sample
// application.
//
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Web.Services.Protocols;
using System.Net;
using Osa.MfpWebService;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


    /// <summary>
    /// The following changes are made to the default processing.
    /// 
    /// 1. Error log mechanism that will create error log file
    /// is added.  By default, it is set to off, but can be turned on
    /// by changing value of key="AppLog" to "true".
    /// 
    /// 2. Application_PreRequestHandlerExecute() method is added to
    /// attach stylesheet as appropriate for every pages so that
    /// in simulated (desktop browser) access, pages will be converted
    /// from original XML to correct HTML for display.
    /// 
    /// 3. MFP access, depending on Accept-Charset value, 
    /// the same information is returned in response encoding.
    /// Application_Error() method is modified to display generic
    /// error page when error is caught in this highest leve.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        // add by zhengwei 2015
        protected FollowMEHandler handler;

       
        // END
        //TODO: Check the path C:\Documents and Settings\<User>\Local Settings\Temp
        // log file names to be generated
        private static string SimpleEAPullPrintLogfileName =
            UtilCommon.AppLogPath + "SimpleEASystemLog.txt";

        public const long TIMEOUT = 5000;
        public static string OSA_LICENSE_KEY = "";
        public static string g_WSDLGeneric = "1.0.0.23";
        public static string UISessionId = string.Empty;

        //chen add start
        //Required for ICCard support
        public static string[] acceptCard = new string[10];
        public static int acceptCardNum = 0;
        //chen add end

        //Invalid chars for file name
        public static char[] g_InvalidChars = new char[] { '<', '>', '!', '~', '|', '$', '%', '+', '\'', '(', ')', ',', ';', ':', ',', '/', '\\', '?', '=', '^', '@', '.', '&', '"', '*', '}', '{', '[', ']', '`' };

        public static bool UseDefaults()
        {
            return (String.Compare(UtilCommon.GetAppSettingString("UseDefaults").ToString(), "true", true) == 0);
        }
        public Global()
        {
            InitializeComponent();
        }

        #region DeleteFile
        /// <summary>
        /// DeleteFile
        /// </summary>
        /// <param name="lst"></param>
        /// <Date>2012.03.12</Date>
        /// <Author>SLC Wei Changye</Author>
        /// <Version>1.2</Version>
        private static bool DeleteFile(List<string> lst)
        {
            if (lst.Count > 0)
            {
                foreach (string item in lst)
                {

                    dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();

                    dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(Convert.ToInt32(item));
                    if (table == null)
                        return false;

                    //get path
                    string filePath = table[0].FileLocation + table[0].DiskFileName;

                    //try to delete
                    if (!File.Exists(filePath))
                        continue;
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                return true;
            }
            else
                return false;
        }

        #endregion

        #region CheckCycle
        /// <summary>
        /// DeleteFile
        /// </summary>
        /// <param name="lst"></param>
        /// <Date>2012.03.12</Date>
        /// <Author>SLC Wei Changye</Author>
        /// <Version>1.2</Version>
        public static void CheckCycle(DateTime now)
        {

            int splPeriod = Convert.ToInt32(UtilCommon.GetAppSettingString("SplPeriod"));
            int userOper = Convert.ToInt32(UtilCommon.GetAppSettingString("OpPeriod"));


            List<string> lstSplOverdue = new List<string>();
            DateTime splDeadLine = now.AddDays(-splPeriod);
            DateTime userOperDeadLine = now.AddDays(-userOper);

            dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
            dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByDeadLine(splDeadLine);

            // Initial OverDue
            if (table.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    lstSplOverdue.Add ( dr["MFPPrintTaskID"].ToString());
                }
            }

            try
            {
                using (SqlConnection con = new SqlConnection(UtilCommon. DBConnectionStrings))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();

                    // 1. delete file
                    DeleteFile(lstSplOverdue);

                    // 2. delete Data in DB
                    try
                    {
                        string deleteSqlstring = "DELETE FROM [MFPPrintTask] WHERE CreateTime < ({0})";
                        deleteSqlstring = string.Format(deleteSqlstring, UtilCommon.ConvertDateToSQL(splDeadLine));
                        using (SqlCommand cmd = new SqlCommand(deleteSqlstring, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        string updateSqlString = string.Format(
                            "UPDATE MFPPrintTask set State = '0' where CreateTime < ({0})", UtilCommon.ConvertDateToSQL(userOperDeadLine));
                        using (SqlCommand cmd = new SqlCommand(updateSqlString, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (tran.Connection != null)
                        {
                            tran.Rollback();
                        }
                        throw ex;
                    }
                    finally
                    {
                        tran.Dispose();
                        tran = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log("SPL Delete & Update Exception:" + DateTime.Now + "\n");
                Log(ex.ToString());
                throw (ex);
            }
        }

        #endregion

        // Write log to event log file from arbitrary
        // location in the code only if AppLog flag is on
        public static void Log(string logMessage)
        {
            if (UtilCommon. InitAppLog())
            {
                TextWriter w;
                if (!File.Exists(SimpleEAPullPrintLogfileName))
                {
                    // Create a file to write to.
                    w = File.CreateText(SimpleEAPullPrintLogfileName);
                    w.WriteLine("Application Log for Web UI");
                    w.WriteLine("----------------------------");
                }
                else
                {
                    // Append text to the existing file.
                    w = File.AppendText(SimpleEAPullPrintLogfileName);
                }

                // Write log text with time-date stamped
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :");
                w.WriteLine("  :{0}", logMessage);
                w.WriteLine("-------------------------------");

                // Update the underlying file.
                w.Flush();
                w.Close();
            }
        }

        // set up request and response filters so that
        // all communication between MFP (or browser) and server
        // is recorded to log file
        private void StartHttpLog()
        {
            //if (UtilCommon.InitHttpLog())
            //{
            //    Request.Filter =
            //        new OSARequestFilter(Request, SimpleEAPullPrintHttpLogfileName);

            //    Response.Filter =
            //        new OSAResponseFilter(Response, SimpleEAPullPrintHttpLogfileName);
            //}
        }


        protected void Application_Start(Object sender, EventArgs e)
        {
            // start log if corresponding flag is true in web.config file
            UtilCommon.InitAppLog();
            StartHttpLog();
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            // start logging of http request and response
            // of this page using filter mechanism
            StartHttpLog();
            Request.ContentEncoding = System.Text.Encoding.GetEncoding(GetAcceptCharset());
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {

        }

        // this method is top-level, catch-all error handler
        // whenever error is unhandled in page level, this handler
        // is called.
        protected void Application_Error(Object sender, EventArgs e)
        {
            // since error is caught in top-level, simply go to generic error
            // page and with user acknowledgment, terminate the session
            string s1 = "ERROR OCCURRED IN THE APPLICATION";
            string s2 = e.ToString();
            Response.Redirect(GetStatusURL(s1, s2));
        }

        public static string GetEncodedStatusURL(string s1, string s2)
        {
            string url = @"OSAPrintStatus.aspx";
            string sParamas = "";
            sParamas = "?param1=" + s1;
            url += sParamas;
            url = HttpUtility.UrlEncode(url);
            return url;
        }

        public static string GetStatusURL(string s1, string s2)
        {
            string sLine1 = HttpUtility.UrlEncode(s1);
            string sLine2 = HttpUtility.UrlEncode(s2);
            string sAllParams = String.Format("param1={0}&param2={1}", sLine1, sLine2);
            string sURL = "OSAPrintStatus.aspx?" + sAllParams;
            return sURL;
        }

        public static string GetStatusURL(string s1, string s2, string sOP)
        {
            string sLine1 = HttpUtility.UrlEncode(s1);
            string sLine2 = HttpUtility.UrlEncode(s2);
            string sAllParams = "";
            if (sOP.Length > 0)
            {
                sAllParams = String.Format("op={0}&param1={1}&param2={2}", sOP, sLine1, sLine2);
            }
            else
            {
                sAllParams = String.Format("param1={0}&param2={1}", sLine1, sLine2);
            }
            string sURL = "OSAPrintStatus.aspx?" + sAllParams;
            return sURL;
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            OSAMetadata Metadata = (OSAMetadata)Session["Metadata"];
            if (null != Metadata)
            {
                Metadata.Dispose();

               
            }
        }

        protected void Application_End(Object sender, EventArgs e)
        {
          
        }

        // this method is called for every page access, and if accessed
        // from web browser, it attaches stylesheet so that page will be
        // converted into HTML.
        protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            string strRequest = Request.Url.AbsoluteUri.ToString();
            if (strRequest.IndexOf(@".asmx") == -1)
            {
                Response.AppendHeader("Content-type", "text/xml");

                // globalization readiness code
                string strCharset = "utf-8";
                if (null != Request.UserAgent)
                {
                    if (Request.UserAgent.IndexOf("OpenSystems") >= 0)
                    {
                        // if accessed from MFP, return back Accept-Charset passed
                        strCharset = GetAcceptCharset();
                    }
                }
                Response.ContentEncoding = System.Text.Encoding.GetEncoding(strCharset);
            }
        }

        // retrieve Accpet-Charset passed from MFP browser in
        // http request headers
        private string GetAcceptCharset()
        {
            string strAcceptCharset = "utf-8";

            for (int i = 0; i < Request.Headers.AllKeys.GetLength(0); i++)
            {
                if (Request.Headers.AllKeys[i] == "Accept-Charset")
                {
                    // the first entry is the value we want
                    // (if multiple values are present, they are delimited
                    // by ',' or ';' characters
                    char[] delims = { ',', ';' };
                    string[] strValues = Request.Headers.Get(i).Split(delims);

                    if (strValues.Length > 0)
                    {
                        strAcceptCharset = strValues[0];
                    }
                    break;
                }
            }

            return strAcceptCharset;
        }




        // examine to see if time out occured
        public static bool IsTimeOutOccured(string strNow)
        {
            bool bRet = false;

            // from timeout point of view
            long lNowSeconds = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            long lLastJBSeconds = long.Parse(strNow);
            long lTimeSpan = (lNowSeconds - lLastJBSeconds);
            long lTimeout = Global.TIMEOUT;

            if (lTimeSpan > lTimeout)
            {
                bRet = true;
            }

            return bRet;
        }

        // display top-level screen of the MFP
        public static void ShowTopLevelScreen(MFPCoreWS webService, string sessionID)
        {
            try
            {
                if (sessionID.Length > 0)
                {
                    SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                    E_MFP_SHOWSCREEN_TYPE mfpTLS;
                    mfpTLS = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                    screen_addr.Item = mfpTLS;

                    webService.ShowScreen(sessionID, screen_addr, ref Global.g_WSDLGeneric);
                }
            }
            catch (Exception ex)
            {
                ShowStatusScreen(webService, GetStatusURL("ShowTopLevelScreen Failed.", "The exception is" + ex.Message.ToString()), sessionID);
            }

        }

        // display error screen
        public static void ShowStatusScreen(MFPCoreWS webService, string sUrl, string sessionID)
        {
            try
            {
                if (sessionID.Length > 0)
                {
                    SHOWSCREEN_TYPE app_screen = new SHOWSCREEN_TYPE();
                    app_screen.Item = @sUrl;
                    webService.ShowScreen(sessionID, app_screen, ref Global.g_WSDLGeneric);
                }
            }
            catch (Exception ex)
            {
                Global.Log("Exception in Showstatusscreen():" + ex.Message.ToString());
            }
        }

        // Close current job, then display top level  message
        public static void CloseCurJobAndShowTopLevelScreen(MFPCoreWS webService, string sessionID, OSA_JOB_ID_TYPE jID)
        {
            try
            {
                if (jID != null)
                {

                    webService.CloseJob(jID, ref Global.g_WSDLGeneric);
                    Global.Log("Job with jobID = " + jID.uid.ToString() + " Closed");

                }
            }
            catch (Exception ex)
            {
                // no process here needed
                Global.Log("CloseJob Threw Exception. The exception is = " + ex.Message.ToString());
            }
            finally
            {
                // show error screen regardless of whether
                // exception occurs or not
                ShowTopLevelScreen(webService, sessionID);
            }
        }



        // close current job, then display error message
        public static void CloseCurJobAndShowErrorScreen(MFPCoreWS webService, string sURL, string sessionID, OSA_JOB_ID_TYPE jID)
        {
            try
            {
                if (jID != null)
                {
                    webService.CloseJob(jID, ref Global.g_WSDLGeneric);
                    Global.Log("Job with jobID = " + jID.uid.ToString() + " Closed");
                }
            }
            catch (Exception ex)
            {
                // no process here needed
                Global.Log("CloseJob Threw Exception. The exception is = " + ex.Message.ToString());
            }
            finally
            {
                // show error screen regardless of whether
                // exception occurs or not
                ShowStatusScreen(webService, sURL, sessionID);
            }
        }

        public static MFPCoreWS CreateWS(string sMFPIP)
        {
            string sURL = Global.GetMFPURL(sMFPIP);
            MFPCoreWS m_webService = new MFPCoreWS();
            m_webService.Url = sURL;
            Global.Log("In CreateWS: sURL is : " + sURL);
            //set the security headers	
            SECURITY_SOAPHEADER_TYPE sec = new SECURITY_SOAPHEADER_TYPE();
            sec.licensekey = Global.OSA_LICENSE_KEY;
            m_webService.Security = sec;

            return m_webService;

        }


        public static string GetMFPURL(string sIP)
        {
            string sURL = @"http://" + sIP + @"/MfpWebServices/MFPCoreWS.asmx";
            //if (String.Compare(UtilCommon.UseSSL, "true", true) == 0)
            //{
            //    sURL = @"https://" + sIP + @"/MfpWebServices/MFPCoreWS.asmx";
            //    Set the SSL policy
            //     surpress warning due to self-signed certificate
            //    ServicePointManager.CertificatePolicy = new OSAPrintOverrideSSLPolicy();

            //}
            return sURL;

        }

        //XML_DOC_SET_TYPE to XML_DOC_DSC_TYPE conversion 
        public static XML_DOC_SET_TYPE ConvertDSCToSETType(XML_DOC_DSC_TYPE DSCsettings)
        {
            if (DSCsettings == null || DSCsettings.complex.Length < 1)
                return null;

            XML_DOC_SET_TYPE xmlSetDoc = new XML_DOC_SET_TYPE();
            COMPLEX_SET_TYPE[] complexField = xmlSetDoc.complex;
            PopulateComplexSetType(DSCsettings.complex, ref complexField);
            xmlSetDoc.complex = complexField;
            return xmlSetDoc;
        }
        //sets the complex array recursively into XML_DOC_SET_TYPE complex
        private static void PopulateComplexSetType(COMPLEX_DSC_TYPE[] settings, ref COMPLEX_SET_TYPE[] complexSET)
        {
            if (settings != null && settings.Length > 0)
            {
                int count = 0;
                complexSET = new COMPLEX_SET_TYPE[settings.Length];
                foreach (COMPLEX_DSC_TYPE complex in settings)
                {
                    if (complex != null && (complex.sysname != null && complex.sysname.Length > 0))
                    {
                        complexSET[count] = new COMPLEX_SET_TYPE();
                        complexSET[count].sysname = complex.sysname;//Printer
                        PROPERTY_SET_TYPE[] complexField = complexSET[count].property;
                        COMPLEX_SET_TYPE[] complexField1 = complexSET[count].complex;
                        PopulatePropertySetType(complex.property, ref complexField);
                        complexSET[count].property = complexField;
                        if (complex.complex != null)
                        {
                            PopulateComplexSetType(complex.complex, ref complexField1);
                        }
                        complexSET[count].complex=complexField1;
                        count++;
                    }
                }
            }
        }
        //sets the prop array recursively into XML_DOC_SET_TYPE complex property
        private static void PopulatePropertySetType(PROPERTY_DSC_TYPE[] propertyDSCList, ref PROPERTY_SET_TYPE[] propertySET)
        {
            if (propertyDSCList != null && propertyDSCList.Length > 0)
            {
                propertySET = new PROPERTY_SET_TYPE[propertyDSCList.Length];
                for (int i = 0; i < propertyDSCList.Length; i++)
                {
                    propertySET[i] = new PROPERTY_SET_TYPE();
                    propertySET[i].sysname = propertyDSCList[i].sysname;
                    propertySET[i].Value = propertyDSCList[i].value;
                }
            }
        }
        public static string GetFileExtension(XML_DOC_SET_TYPE setXMLDOC)
        {
            string sFileType = "file-format";//should match XML_DOC description
            string sExt = "";
            sExt = GetThePropValueFromXMLDOCSETType(setXMLDOC, sFileType);
            //check for TIFF and TIF, MFP appends the TIF extension for TIFF format
            if (String.Compare(sExt, "TIFF", true) == 0)
            {
                sExt = "TIF";
            }
            //check for JPEG, MFP appends the JPG extension for JPEG format
            else if (String.Compare(sExt, "JPEG", true) == 0)
            {
                sExt = "JPG";
            }
            if (String.Compare(sExt, "ENCRYPT_PDF", true) == 0)
            {
                sExt = "PDF";
            }
            return sExt;
        }

        //To retrieve the property value in the XML_DOC_SET_TYPE
        //gets the given property value in the XML_DOC_SET_TYPE
        public static string GetThePropValueFromXMLDOCSETType(XML_DOC_SET_TYPE xmlDocSET, string name)
        {
            string str_ret = "";
            if (xmlDocSET != null && name.Length > 0)
            {
                foreach (COMPLEX_SET_TYPE complex in xmlDocSET.complex)
                {
                    if (complex != null && (complex.sysname != null && complex.sysname.Length > 0))
                    {
                        str_ret = GetThePropValueRecursively(complex, name);
                    }
                }
            }
            return str_ret;
        }

        //Gets the prop array recursively from the complex
        private static string GetThePropValueRecursively(COMPLEX_SET_TYPE complexSET, string name)
        {
            string str_ret = "";
            if (complexSET != null)
            {
                if (complexSET.property != null && complexSET.property.Length > 0)
                {
                    foreach (PROPERTY_SET_TYPE propType in complexSET.property)
                    {
                        if (propType.sysname != null && propType.sysname.Length > 0)
                        {
                            if (propType.sysname == name)
                            {
                                str_ret = propType.Value;
                                
                                break;
                            }
                        }
                    }

                }
                if (str_ret.Length == 0)
                {
                    if (complexSET.complex != null && complexSET.complex.Length > 0)
                    {
                        foreach (COMPLEX_SET_TYPE complexcomplex in complexSET.complex)
                        {
                            if (complexcomplex != null && (complexcomplex.sysname != null && complexcomplex.sysname.Length > 0))
                            {
                                str_ret = GetThePropValueRecursively(complexcomplex, name);
                                if (str_ret.Length != 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return str_ret;
        }
        //sets the specified property value in XML_DOC_SET_TYPE
        public static bool SetThePropValueInXMLDOCSETType(ref XML_DOC_SET_TYPE xmlDocSET, string name, string new_value)
        {
            bool bRet = false;
            if (xmlDocSET != null && name.Length > 0)
            {
                for (int k = 0; k < xmlDocSET.complex.Length; k++)
                {
                    if (xmlDocSET.complex[k] != null && (xmlDocSET.complex[k].sysname != null && xmlDocSET.complex[k].sysname.Length > 0))
                    {
                        bRet = SetThePropValueRecursively(ref xmlDocSET.complex[k], name, new_value);
                    }
                }
            }
            return bRet;
        }
        //sets the specified property value in XML_DOC_SET_TYPE, called by the SetThePropValueInXMLDOCSETType
        private static bool SetThePropValueRecursively(ref COMPLEX_SET_TYPE complexSET, string name, string new_value)
        {
            bool bRet = false;
            if (complexSET != null)
            {
                if (complexSET.property != null && complexSET.property.Length > 0)
                {
                    for (int i = 0; i < complexSET.property.Length; i++)
                    {
                        if (complexSET.property[i].sysname != null && complexSET.property[i].sysname.Length > 0)
                        {
                            if (complexSET.property[i].sysname == name)
                            {
                                if (complexSET.property[i].Value != new_value)
                                {
                                    Global.Log("The existing value for " + name + " = " + complexSET.property[i].Value.ToString());
                                    complexSET.property[i].Value = new_value;
                                    Global.Log("The New value = " + new_value);
                                    bRet = true;
                                }
                                break;
                            }
                        }
                    }

                }
                if (!bRet)
                {
                    if (complexSET.complex != null && complexSET.complex.Length > 0)
                    {
                        for (int j = 0; j < complexSET.complex.Length; j++)
                        {
                            if (complexSET.complex[j] != null && (complexSET.complex[j].sysname != null && complexSET.complex[j].sysname.Length > 0))
                            {
                                bRet = SetThePropValueRecursively(ref complexSET.complex[j], name, new_value);
                                if (bRet)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return bRet;
        }
        

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
        #endregion
    }
