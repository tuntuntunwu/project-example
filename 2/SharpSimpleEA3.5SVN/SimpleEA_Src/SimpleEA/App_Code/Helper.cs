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
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.Web.Configuration;
using System.Xml;
using System.Configuration;
using Osa.MfpWebService;
using Osa.Util;
using System.Web;
using System.Security;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Data.SqlClient;
using SLCRegister;
using BLL;
using Model;
using DAL;
public class Helper
{
    //20140619 chen add
    public class LogDataInfo
    {
        //20170614 add
        public int ID = 0;
        public DateTime Time = new DateTime();
        public int UserId = 0;
        public string UserName = "";
        public string LoginName = "";
        public string GroupId = "";
        public string GroupName = "";
        public string Jobuid = "";
        public string Serialnumber = "";
        public string MFPName = "";
        public string MFPModel = "";
        public string MFPIp = "";
        public int Duplex = 0;
        public int JOBId = 0;
        public string FileName = "";
        public string PageID = "";
        public string Status = "";
        public string ErrorCode = "";
        public string MFPPrintTaskID = "";

        public string PCName = "";

        //计算价格和money用
        public int PriceDetailID1=0;
        public string JobName1="";
        public int JobID1 = 0;
        public int FunctionId1 = 0;
        public string colorType1 = "" ;
        public uint sheetCount1 = 0;
        public uint papeCount1 = 0;
        public uint copyCount1 = 0;
        public decimal spendMoney1 = 0;
        
        public int DspSheetCount1 = 0;
        public int DspPapeCount1 = 0;

        public int PriceDetailID2 = 0;
        public string JobName2;
        public int JobID2 = 0;
        public int FunctionId2 = 0;
        public string colorType2 = "";
        public uint sheetCount2 = 0;
        public uint papeCount2 = 0;
        public uint copyCount2 = 0;
        public decimal spendMoney2 = 0;
        public int DspSheetCount2 = 0;
        public int DspPapeCount2 = 0;

    }
    //20140619 chen end

    //---------------------------------------------------------
    // ConfigFileInfo
    /// <summary>
    /// Get required information from appSettings in Web.config
    /// </summary>
    public class ConfigFileInfo
    {
        // Get a valur of the node by specified key from <appSettings>
        public static string GetAppSettingString(string key)
        {
            string value = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                return value;
            }

            value = ConfigurationManager.AppSettings.Get(key).ToString();

            return value;
        }

        //2011.03.21 Delete By SES Jijianxiong ST
        // Bug Management Sheet_SimpleEA_110321.xls No.23
        //// Get a value of "LicFile" key from <appSettings> in Web.config, and 
        //// returns the file path when the file is exist.
        //public string ExistsLicenseFile()
        //{
        //    string licFileName = GetAppSettingString("LicFile");
        //    if (string.IsNullOrEmpty(licFileName))
        //    {
        //        return licFileName;
        //    }
        //    string path = HttpContext.Current.Request.PhysicalApplicationPath + licFileName;
        //    if (!File.Exists(path))
        //    {
        //        // File does not exist
        //        path = string.Empty;
        //    }
        //    return path;
        //}
        //2011.03.21 Delete By SES Jijianxiong ED

        // Returns a value of "UseSSL" key value from <appSettings> in Web.config.
        public bool UseSSL()
        {
            bool ret = false;
            string ssl = GetAppSettingString("UseSSL");
            if (string.IsNullOrEmpty(ssl))
            {
                // No UseSSL key is found.
                return ret;
            }
            if (ssl == "true")
            {
                ret = true;
            }
            return ret;
        }

        // Returns a value of "UseLoginScreen" key value from <appSettings> in Web.config.
        public bool UseLoginScreen()
        {
            bool ret = true;
            return ret;
        }
        // Begin auto logout setting
        // Get a value of "logout-mode" key from <appSettings> in Web.config. 
        public string GetLogoutMode()
        {
            return GetAppSettingString("logout-mode");
        }
        // End auto logout setting
    }

	//---------------------------------------------------------
	// DeviceSession
    [Serializable()]
	public class DeviceSession
	{
        //---------------------------------------------------------------------
        // static members
        private static Dictionary<string, DeviceSession> sessionData = new Dictionary<string, DeviceSession>();
        public DEVICE_INFO_TYPE deviceinfo = null;
        public ACL_DOC_TYPE xmldocacl = null;
        public LIMITS_TYPE[] xmldoclimits = null;
        public string Generic = null;
        public string ErrMsg = null;
        protected const string VENDOR_KEY = "eCGfMQyA5INvjQCWucGwQr5uXRByQgQq2tcALSruE2KtFtfn7KXnNfmmZQGDyZMRmKjvLTlUI/agRn5vkpSJLg==";

        [NonSerialized]
		public MFP_WEBSERVICE_TYPE[] mfpwebservices = null;
        [NonSerialized]
        public CREDENTIALS_TYPE Credentials = null;
       private static string save_session_file = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data\\session.data");

        public DeviceSession()
        {

        }

        private string GetVendorKey(string path)
        {
            string key = string.Empty;
            // 2011.03.21 Update By SES jijianixong ST
            // Bug Management Sheet_SimpleEA_110321.xls No.23
            //if (string.IsNullOrEmpty(path))
            //{
            //    return key;
            //}
            //if (!File.Exists(path))  // Check the file existence
            //{
            //    return key;
            //}
            //XmlDocument licXmlDoc = new XmlDocument();
            //try
            //{
            //    licXmlDoc.Load(path);
            //    XmlNodeList elemList = licXmlDoc.GetElementsByTagName("VendorKey");
            //    if (0 < elemList.Count)
            //    {
            //        key = elemList[0].InnerXml;
            //    }
            //}
            //catch (XmlException ex)
            //{
            //    ErrMsg = ex.Message;
            //}
            key = VENDOR_KEY;
            // 2011.03.21 Update By SES jijianixong ED
            return key;
        }
 		public MFPCoreWSEx GetConfiguredMfpCoreWS()
		{
            string protocol = "http://";
            string ws_port = ":80";
            string licFile = string.Empty;
            string key = VENDOR_KEY;
            Helper.ConfigFileInfo configInfo = new Helper.ConfigFileInfo();

            //2011.03.21 Delete By SES Jijianxiong ST
            // Bug Management Sheet_SimpleEA_110321.xls No.23

            //licFile = configInfo.ExistsLicenseFile();
            //if (!string.IsNullOrEmpty(licFile))
            //{
            //    // Read the vendor key from the license file.
            //    key = GetVendorKey(licFile);
            //}
            //2011.03.21 Delete By SES Jijianxiong ED
            if (configInfo.UseSSL())
            {
                // SSL
                protocol = "https://";
                ws_port = ":443";
            }

			return new MFPCoreWSEx(
				protocol + deviceinfo.network_address + ws_port/*":80"*/,
                key, Credentials, null, Generic);
		}
		
		public void InitializeMfp(string myUrl)
		{
			E_EVENT_ID_TYPE event_type = E_EVENT_ID_TYPE.ON_JOB_COMPLETED;


			ACCESS_POINT_TYPE sink_url = new ACCESS_POINT_TYPE();
			sink_url.URLType = E_ADDRESSPOINT_TYPE.SOAP;
			sink_url.Value = myUrl;

			MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();

            try
            {
                mfpWS.Subscribe(null, event_type, sink_url, true);
            }
            catch (SoapException ex)
            {
                ErrMsg = ex.Message;
            }


            // 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ST
            // For the Log Record.
            //E_EVENT_ID_TYPE event_type1 = E_EVENT_ID_TYPE.ON_JOB_CREATE;
            //try
            //{
            //    mfpWS.Subscribe(null, event_type1, sink_url, true);
            //}
            //catch (SoapException ex)
            //{
            //    ErrMsg = ex.Message;
            //}

            E_EVENT_ID_TYPE event_start = E_EVENT_ID_TYPE.ON_JOB_STARTED;
            try
            {
                mfpWS.Subscribe(null, event_start, sink_url, true);
            }
            catch (SoapException ex)
            {
                ErrMsg = ex.Message;
            }
            // 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ED

        }

        //public void LogUserIn(string accid)
        //{
        //    UserManager usrman = new UserManager();
        //    ACL_DOC_TYPE acl = null;
        //    LIMITS_TYPE[] limits = null;
        //    LoginScreenType LoginSc = usrman.GetLoginScreen(accid);

        //    // Get UserACl
        //    acl = usrman.GetUserACL(accid , this.xmldocacl);


        //    // Get Userlimits
        //    //20140605 chen update
        //    //limits = usrman.GetUserAbleLimits(accid, this.xmldoclimits);
        //    limits = usrman.GetUserAbleLimitsByFunct(accid, this.xmldoclimits);
        //    //

        //    LoginManager loginman = new LoginManager();
        //    SCREEN_INFO_TYPE screen_info = loginman.GetScreenInfoType(accid, deviceinfo.uuid, LoginSc);

        //    MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
        //    try
        //    {
        //        mfpWS.EnableDevice(acl, limits);
        //        mfpWS.ShowScreen(screen_info);
        //    }
        //    catch (SoapException )
        //    {
        //        SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
        //        screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
        //        mfpWS.ShowScreen(screen_addr);
        //    }
        //}

        //2018-01-17 chen add
        public void LogUserIn(string sn, string accid)
        {
            UserManager usrman = new UserManager();
            ACL_DOC_TYPE acl = null;
            LIMITS_TYPE[] limits = null;
            LoginScreenType LoginSc = usrman.GetLoginScreen(accid);

            //获取MFP序列号
            string serialnumber = DeviceSession.GetSerialnumberByDevid(sn);

            // Get UserACl
            acl = usrman.GetUserACL(serialnumber, accid, this.xmldocacl);


            //limits = usrman.GetUserAbleLimitsByFunct(accid, this.xmldoclimits);
            limits = usrman.GetUserAbleLimitsByFunct(serialnumber, accid, this.xmldoclimits);
            //

            LoginManager loginman = new LoginManager();
            SCREEN_INFO_TYPE screen_info = loginman.GetScreenInfoType(accid, deviceinfo.uuid, LoginSc);

            MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
            try
            {
                mfpWS.EnableDevice(acl, limits);
                mfpWS.ShowScreen(screen_info);
            }
            catch (SoapException)
            {
                SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
                screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
                mfpWS.ShowScreen(screen_addr);
            }
        }
        // 2012.04.24 Delete by Wei Changye ST
        ///// <summary>
        ///// Add by Wei Changye 2012.01.10
        ///// </summary>
        ///// <param name="accid"></param>
        //public void LogUserInShowTask()
        //{
        //    MFPCoreWSEx mfpWS = GetConfiguredMfpCoreWS();
        //    //SHOWSCREEN_TYPE s = new SHOWSCREEN_TYPE();
        //    try
        //    {
        //        //s .Item="MFPScreen/UserPrintDetails.aspx";
        //        //mfpWS.ShowScreen(deviceinfo.uuid, s, ref Global.g_WSDLGeneric);
        //        mfpWS.ShowScreen("../MFPScreen/UserPrintDetails.aspx");
        //    }
        //    catch (SoapException)
        //    {
        //        SHOWSCREEN_TYPE screen_addr = new SHOWSCREEN_TYPE();
        //        screen_addr.Item = E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN;
        //        mfpWS.ShowScreen(screen_addr);
        //    }
        //}
        // 2012.04.24 Delete by Wei Changye ED


        #region "Create sessionData"
        /// <summary>
        /// Create sessionData
        /// </summary>
        /// <param name="deviceinfo"></param>
        /// <param name="mfpwebservices"></param>
        /// <param name="xmldocacl"></param>
        /// <param name="Credentials"></param>
        /// <param name="xmldoclimits"></param>
        /// <param name="generic"></param>
        /// <Date>2010.11.15</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static void Create(DEVICE_INFO_TYPE deviceinfo, MFP_WEBSERVICE_TYPE[] mfpwebservices,
									ACL_DOC_TYPE xmldocacl, CREDENTIALS_TYPE Credentials,
									LIMITS_TYPE[] xmldoclimits, string generic)
		{
            if (null == deviceinfo)
            {
                return;
            }
			DeviceSession s = new DeviceSession();
			s.deviceinfo = deviceinfo;
			s.mfpwebservices = mfpwebservices;
			s.xmldocacl = xmldocacl;
			s.Credentials = Credentials;
			s.xmldoclimits = xmldoclimits;
            s.Generic = generic;

            // 2010.11.15 Update By SES.JiJianxiong ST
            // Object Save Solution
            //if (sessionData.ContainsKey(deviceinfo.uuid))
            //{
            //    // This is a known session ID.
            //    sessionData[deviceinfo.uuid] = s;
            //}
            //else
            //{
            //    // This is unknown session.
            //    try
            //    {
            //        sessionData.Add(deviceinfo.uuid, s);
            //    }
            //    catch (ArgumentException ex)
            //    {
            //        throw ex;
            //    }
            //}

            // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ST
            string serialnumber = deviceinfo.serialnumber;
            if (string.IsNullOrEmpty(serialnumber))
            {
                serialnumber = DeviceSession.GetSerialnumberByDevid(deviceinfo.uuid);
            }
            // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ED


            if (sessionData.ContainsKey(serialnumber))
            {
                // This is a known session ID.
                sessionData[serialnumber] = s;
            }
            else
            {
                try
                {
                    sessionData.Add(serialnumber, s);
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
            }


            // Get the DeviceSession
            string dstContents = "";
            try
            {
                // Entity Class To StringBuilder
                XmlSerializer mySerializer = new XmlSerializer(typeof(DeviceSession));

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                using (System.IO.StringWriter sw = new System.IO.StringWriter(sb))
                {
                    mySerializer.Serialize(sw, s);
                }

                // Change the encoding
                dstContents = sb.ToString();
                // Replace the Encode 
                if (dstContents.IndexOf(" encoding=\"utf-16\"") > -1)
                {
                    dstContents = dstContents.Replace(" encoding=\"utf-16\"", "");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Save Entity Class To DB
            using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    // Is the serialnumber existed in the DB
                    dtDeviceSessionTableAdapters.DeviceSessionTableAdapter query = new dtDeviceSessionTableAdapters.DeviceSessionTableAdapter();
                    int rowcount = (int)query.IsExist(serialnumber);

                    string strSql;
                    if (rowcount > 0)
                    {
                        // Update Information
                        strSql = "   UPDATE [DeviceSession]          " + Environment.NewLine;
                        strSql += "  SET                        " + Environment.NewLine;
                        strSql += "       [DeviceSession] = {1}      " + Environment.NewLine;
                        strSql += "      ,[UpdateDate] = getdate() " + Environment.NewLine;
                        strSql += "WHERE [SerialNumber] = {0}   " + Environment.NewLine;
                    }
                    else
                    {
                        // Add Information
                        strSql = "   INSERT INTO [DeviceSession]          " + Environment.NewLine;
                        strSql += "             ([SerialNumber]                " + Environment.NewLine;
                        strSql += "             ,[DeviceSession]          " + Environment.NewLine;
                        strSql += "             ,[CreateDate]        " + Environment.NewLine;
                        strSql += "             ,[UpdateDate])       " + Environment.NewLine;
                        strSql += "       VALUES                     " + Environment.NewLine;
                        strSql += "             ({0}                 " + Environment.NewLine;
                        strSql += "             ,{1}                 " + Environment.NewLine;
                        strSql += "             ,getdate() , getdate())  " + Environment.NewLine;
                    }

                    string[] paramslist = new string[2];
                    // SerialNumber
                    paramslist[0] = UtilCommon.ConvertStringToSQL(serialnumber);
                    // DeviceSession
                    paramslist[1] = UtilCommon.ConvertStringToSQL(dstContents);

                    strSql = string.Format(strSql, paramslist);

                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
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
            // 2010.11.15 Update By SES.JiJianxiong Static Update ED
        }
        #endregion

        public static int GetDevSessionCount()
        {
            if (null == sessionData)
            {
                return -1;
            }
            return sessionData.Count;
        }

        #region "Get the Session Data."
        /// <summary>
        /// Get the Session Data.
        /// </summary>
        /// <param name="strIn">devid or serialnumber</param>
        /// <returns></returns>
        /// <Date>2010.11.15</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static DeviceSession Get(string strIn)
		{
            // 2010.11.15 Update By SES.JiJianxiong Static Update ST
            //if (string.IsNullOrEmpty(devid))
            //{
            //    return null;
            //}
            //if (!sessionData.ContainsKey(devid))
            //{
            //    return null;    // unknown devid
            //}
            //return sessionData[devid];

            // Get the serialnumber By param strIn.
            string serialnumber = "";
            if (strIn.Length > 10)
            {
                // Get the serialnumber by devid
                serialnumber = GetSerialnumberByDevid(strIn);
            }
            else
            {
                serialnumber = strIn;
            }

            // Is the serialnumber is Contains in the SessionData.
            if (!sessionData.ContainsKey(serialnumber))
            {
                // Get the information from DB.
                try
                {
                    dtDeviceSessionTableAdapters.DeviceSessionTableAdapter query = new dtDeviceSessionTableAdapters.DeviceSessionTableAdapter();
                    dtDeviceSession.DeviceSessionDataTable result = query.GetDataBySerialNumber(serialnumber);
                    string strDbdevicesession = result[0].DeviceSession;
                    String strDeviceSession = result[0].DeviceSession;

                    DeviceSession item;

                    XmlSerializer mySerializer = new XmlSerializer(typeof(DeviceSession));

                    using (System.IO.StringReader sw = new System.IO.StringReader(strDeviceSession))
                    {
                        item = (DeviceSession)mySerializer.Deserialize(sw);
                    }

                    sessionData.Add(serialnumber, item);
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            }

            return sessionData[serialnumber];
            // 2010.11.15 Update By SES.JiJianxiong Static Update ED

        }
        #endregion

        #region "Get Serialnumber By Devid."
        /// <summary>
        /// Get Serialnumber By Devid.
        /// </summary>
        /// <param name="devid"></param>
        /// <returns></returns>
        /// <Date>2010.11.15</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static string GetSerialnumberByDevid(string devid)
        {
            string serialnumber = devid;
            // device-uuid's format:SNXXXXXXXXXXMNSHARP MX-XXXX
            // XXXXXXXXXX is serialnumber
            if (devid.Length > 10)
            {
                serialnumber = devid.Substring(2, 10);

            }
            return serialnumber;
        }
        #endregion
    }


    //---------------------------------------------------------
    // The Custom Accountant

    public class MyAccountant : AccountantBase
    {
        public override CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo)
        {
            string accid = userinfo.accountid;

            CREDENTIALS_BASE_TYPE ret = new CREDENTIALS_BASE_TYPE();
            ret.accountid = accid;
            return ret;
        }


        #region "Save Job Information into SimpleEA's DB."
        /// <summary>
        /// Save Job Information into SimpleEA's DB.
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="sheetCount"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <param name="results"></param>
        /// <Date>2010.07.27</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
      //chen update 20140424 start
        //protected override void RecordSheetCount(string accid, uint sheetCount, string propName, string propValue, JOB_RESULTS_BASE_TYPE results)
        protected override void RecordSheetCount(string accid, uint sheetCount, uint papeCount, uint copyCount, string propName, string propValue, JOB_RESULTS_BASE_TYPE results)
      //chen update 20140424 end
        {
            if (0 < sheetCount)
            {
                // UserId
                int userid = int.Parse(accid);
                // JobName
                string jobName = MapJobModeToLimits( results.jobmode);
                // Color-Type
                string ColorType =  GetColorType( propName , propValue);
                // Page-Type
                string pageType =GetPageType(results);
                // Page-Num
                int pageNum = (int)sheetCount;
                // SerialNumber
                string serialnumber = results.deviceinfo.serialnumber;
                // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ST
                if (string.IsNullOrEmpty(serialnumber))
                {
                    serialnumber = DeviceSession.GetSerialnumberByDevid(results.deviceinfo.uuid);
                }
                // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ED

                // Delete By SES JiJianXiong ST
                // Test in the SESC. MFP time is older than the server time.
                // MFP time is year 2003.
                //// Time ( Start Time)
                //DateTime time = results.timestamp[0].Value;
                // Delete By SES JiJianXiong ED

                UtilCommon.MFPJob mfpjob = GetMFPJob(jobName, ColorType);
                // Update By SES JiJianXiong ST
                // Test in the SESC. MFP time is older than the server time.
                // MFP time is year 2003.
                // UtilCommon.SaveJobRecrod(userid, mfpjob, pageType, pageNum, serialNumber, time);
                
                //chen add 20140424 start
                //单双面
                 E_EA_DUPLEX_TYPE eDuplex = E_EA_DUPLEX_TYPE.Unknown;
                // Get the File Information for the REMOTE-JOB.
                foreach (PROPERTY_SET_TYPE property in results.properties)
                {
                    switch (property.sysname)
                    {
                        case "computer-name":
                            break;
                        case "filename":
                            break;
                        case "duplex-mode":
                            eDuplex = GetDuplex(property.Value);
                            break;
                        default:
                            break;
                    }
                }
                //UtilCommon.SaveJobRecrod(userid, mfpjob, pageType, pageNum, serialnumber);
                UtilCommon.SaveJobRecrod(userid, mfpjob, pageType, pageNum, (int)papeCount, (int)copyCount, (int)eDuplex, serialnumber);
                //chen add 20140424 end
                // Update By SES JiJianXiong ED
            }
        }
        #endregion

        #region "Get Duplex"
        /// <summary>
        /// Get Duplex
        /// </summary>
        /// <param name="strname"></param>
        /// <returns></returns>
        /// <Date>2010.12.08</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>1.10</Version>
        private E_EA_DUPLEX_TYPE GetDuplex(string strname)
        {
            switch (strname.ToUpper())
            {
                case "1SIDED":
                    return E_EA_DUPLEX_TYPE.SIDED1;
                case "2SIDED":
                    return E_EA_DUPLEX_TYPE.SIDED2;
                case "2SIDED_BOOKLET":
                    return E_EA_DUPLEX_TYPE.SIDED_BOOKLET2;
                case "2SIDED_TABLET":
                    return E_EA_DUPLEX_TYPE.SIDED_TABLET2;
                case "PAMPHLET":
                    return E_EA_DUPLEX_TYPE.PAMPHLET;
                default:
                    return E_EA_DUPLEX_TYPE.Unknown;
            }
        }
        #endregion

        #region "Get Page Type."
        /// <summary>
        /// GetPageType
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <Date>2010.07.27</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private string GetPageType(JOB_RESULTS_BASE_TYPE result)
        {

            RESOURCE_PAPER_TYPE[] paperout;
            if (result is JOB_RESULTS_COPY_TYPE)
			{
                paperout = ((JOB_RESULTS_COPY_TYPE)result).resources.paperout;
			}
            else if (result is JOB_RESULTS_PRINT_TYPE)
			{
                paperout = ((JOB_RESULTS_PRINT_TYPE)result).resources.paperout;
			}
            else if (result is JOB_RESULTS_SCAN_TYPE)
			{
                paperout = ((JOB_RESULTS_SCAN_TYPE)result).resources.paperout;
			}
            else if (result is JOB_RESULTS_DOCFILING_TYPE)
            {
                paperout = ((JOB_RESULTS_DOCFILING_TYPE)result).resources.paperout;
            }
            else
            {
                //foreach (PROPERTY_SET_TYPE property in result.properties)
                //{
                //    if (property.sysname.Equals("original-size"))
                //    {
                //        return property.Value;
                //    }
                //}

                //  unused pager.
                return "";
            }

            foreach (RESOURCE_PAPER_TYPE papertype in paperout)
            {
                if (papertype.property == null)
                {
                    continue;
                }
                foreach (PROPERTY_SET_TYPE property in papertype.property)
                {
                    if ( property.sysname.Equals("papersize")) {
                        return property.Value;
                    }
                }
            }


            return "";
        }
        #endregion
 
        #region "Get Color Type."
        /// <summary>
        /// Get Color Type.
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        /// <Date>2010.07.27</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private string GetColorType(string propName, string propValue)
        {
            if (propName.Equals("color-mode"))
            {

                return propValue;
            }
            else
            {
                return "";
            }

        }
        #endregion

    }

    #region "Custom Accountant for the Log Record"
    /// <summary>
    /// LogAccountant
    /// </summary>
    /// <Date>2010.11.17</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.10</Version>
    public class LogAccountant : AccountantBase
    {
        private UtilCommon.MFPJob mfpjob;
        private Nullable<int> PageID = null;
        private Nullable<int> Number = null;
        //chen add 20140424 for money start
        private Nullable<int> papeCount = null;
        private Nullable<int> copyCount = null;
        //chen add 20140424 for money end
        private string PCName = null;
        private string FileName = null;
        private E_EA_DUPLEX_TYPE eDuplex = E_EA_DUPLEX_TYPE.Unknown;

        public override CREDENTIALS_BASE_TYPE ValidateCredential(CREDENTIALS_TYPE userinfo)
        {
            string accid = userinfo.accountid;

            CREDENTIALS_BASE_TYPE ret = new CREDENTIALS_BASE_TYPE();
            ret.accountid = accid;
            return ret;
        }

        #region "Save Job Information into SimpleEA's DB."
        /// <summary>
        /// Save Job Information into SimpleEA's DB.
        /// </summary>
        /// <param name="accid"></param>
        /// <param name="sheetCount"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <param name="results"></param>
        /// <Date>2010.11.17</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>1.10</Version>
        /// 
//chen update 20140424 start
        //protected override void RecordSheetCount(string accid, uint sheetCount, string propName, string propValue, JOB_RESULTS_BASE_TYPE results)
        protected override void RecordSheetCount(string accid, uint sheetCount, uint _papeCount, uint _copyCount, string propName, string propValue, JOB_RESULTS_BASE_TYPE results)
//chen update 20140424 end
        {
            if (0 < sheetCount)
            {
                // UserId
                int userid = int.Parse(accid);
                // JobName
                string jobName = MapJobModeToLimits(results.jobmode);
                // Color-Type
                string ColorType = GetColorType(propName, propValue);
                mfpjob = GetMFPJob(jobName, ColorType);

                // Page-Type
                string pageType = GetPageType(results);

                // Get PageSize ID.
                PageID = UtilConst.CON_DATE_OTHER_PAGE;
                if (pageType == "")
                {
                    // Page is Unused.
                    PageID = UtilConst.CON_DATE_UNUSED_PAGE;
                }
                else
                {
                    dtPaperSizeInformationTableAdapters.PaperSizeInformationTableAdapter adapter = new dtPaperSizeInformationTableAdapters.PaperSizeInformationTableAdapter();
                    dtPaperSizeInformation.PaperSizeInformationDataTable dt = adapter.GetDataByPageSize(pageType);
                    if (dt.Rows.Count == 0)
                    {
                        // The Page Type is unknow for Simple EA.
                        PageID = UtilConst.CON_DATE_OTHER_PAGE;
                    }
                    else
                    {
                        PageID = ((dtPaperSizeInformation.PaperSizeInformationRow)dt.Rows[0]).ID;
                    }
                }
                // Page-Num
                Number = (int)sheetCount;

                //chen add 20140424 for money start
                papeCount = (int)_papeCount;
                copyCount = (int)_copyCount;
                //chen add 20140424 for money end

                // Get the File Information for the REMOTE-JOB.
                foreach (PROPERTY_SET_TYPE property in results.properties)
                {
                    switch (property.sysname)
                    {
                        case "computer-name":
                            PCName = property.Value;
                            break;
                        case "filename":
                            FileName = property.Value;
                            break;
                        case "duplex-mode":
                            eDuplex = GetDuplex(property.Value);
                            break;
                        default:
                            break;
                    }
                }

            }
        }
        #endregion

        #region "Record the log"
        /// <summary>
        /// Record the log information into DB
        /// </summary>
        /// <param name="eventdata"></param>
        /// <Date>2010.11.17</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>1.10</Version>
        public void RecordLog(EVENT_DATA_TYPE eventdata)
        {
            try
            {

                // Get the Device Information.
                DEVICE_INFO_TYPE deviceinfo = eventdata.deviceinfo;

                // 2011.01.19 Update By SES Jijianxiong ST
                // BUG: Print with unknow user, record cancel job. In this time, accountid is "Other2"
                // Get the User ID
                //int intUserId = Convert.ToInt32(eventdata.userinfo.accountid);
                int intUserId = -1;
                try
                {
                     intUserId = Convert.ToInt32(eventdata.userinfo.accountid);
                }
                catch 
                {

                    intUserId = -1;
                }

                // 2011.01.19 Update By SES Jijianxiong ED

                // Check DETAILS ABSTRACT TYPE.
                if (eventdata.details is DETAILS_ON_JOB_STARTED_TYPE)
                {
                    DETAILS_ON_JOB_STARTED_TYPE starttype = eventdata.details as DETAILS_ON_JOB_STARTED_TYPE;
                    // Get details on JobId
                    OSA_JOB_ID_TYPE osajobid = starttype.mfpjobid.Item as OSA_JOB_ID_TYPE;
                    // Get Details on jobmode
                    JOB_MODE_TYPE jobmode = starttype.jobmode;



                    RecordStartLog(intUserId, deviceinfo, jobmode, osajobid);
                }
                else if (eventdata.details is DETAILS_ON_JOB_COMPLETED_TYPE)
                {
                    DETAILS_ON_JOB_COMPLETED_TYPE detalCompleted = (DETAILS_ON_JOB_COMPLETED_TYPE)eventdata.details;
                    JOB_RESULTS_BASE_TYPE result = (JOB_RESULTS_BASE_TYPE)detalCompleted.JobResults;


                    RecordEndLog(intUserId, eventdata);
                }
                else
                {
                    ;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion

        #region " Record the log for the End Log."
        /// <summary>
        /// Record the log for the End Log.
        /// </summary>
        /// <param name="intUserId"></param>
        /// <param name="eventdata"></param>
        /// <Date>2010.11.17</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>1.10</Version>
//        private void RecordEndLog(int intUserId,EVENT_DATA_TYPE eventdata)
//        {
//            string serialnumber = string.Empty;
//            string Jobuid;
//            string Status;
//            string ErrorCode = null;

//            DETAILS_ON_JOB_COMPLETED_TYPE detalCompleted = (DETAILS_ON_JOB_COMPLETED_TYPE)eventdata.details;
//            JOB_RESULTS_BASE_TYPE result = (JOB_RESULTS_BASE_TYPE)detalCompleted.JobResults;
//;
//            // Get the uid for this job.
//            Jobuid = (result.mfpjobid.Item as OSA_JOB_ID_TYPE).uid;
//            // serialnumber
//            serialnumber = eventdata.deviceinfo.serialnumber;

//            // 2010.12.09 Add By SES Jijianxiong ST
//            // Get JobID and ColorType
//            // JobName
//            string jobName = MapJobModeToLimits(result.jobmode);
//            // Color-Type
//            string ColorType = "";
//            foreach (PROPERTY_SET_TYPE propertie in result.properties)
//            {
//                if (propertie.sysname.Equals("color-mode"))
//                {
//                    ColorType = propertie.Value;
//                    break;
//                }
//            }
//            mfpjob = GetMFPJob(jobName, ColorType);
//            // 2010.12.09 Add By SES Jijianxiong ED


//            // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ST
//            if (string.IsNullOrEmpty(serialnumber))
//            {
//                serialnumber = DeviceSession.GetSerialnumberByDevid(eventdata.deviceinfo.uuid);
//            }
//            // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ED

//            switch (result.jobstatus.status)
//            {
//                case E_JOB_STATUS_TYPE.CANCELED:
//                    Status = UtilConst.JOB_STATUS_CANCELED;
//                    // Get the Job Type, page Type and page count.
//                    this.RecordClicks(eventdata);

//                    break;
//                case E_JOB_STATUS_TYPE.FINISHED:
//                    Status = UtilConst.JOB_STATUS_FINISHED;
//                    // Get the Job Type, page Type and page count.
//                    this.RecordClicks(eventdata);

//                    break;
//                case E_JOB_STATUS_TYPE.SUSPENDED:
//                    Status = UtilConst.JOB_STATUS_SUSPENDED;
//                    // Get Error Information.
//                    JOB_STATUS_DETAILS_SUSPENDED_TYPE suspendeddetails = (result.jobstatus.details as JOB_STATUS_DETAILS_SUSPENDED_TYPE);

//                    foreach (ERROR_TYPE errortype in suspendeddetails.errorList)
//                    {
//                        if (!string.IsNullOrEmpty(errortype.code.ToString()))
//                        {
//                            ErrorCode = errortype.code.ToString();
//                            break;
//                        }
//                    }

//                    break;
//                case E_JOB_STATUS_TYPE.ERROR:
//                    Status = UtilConst.JOB_STATUS_ERROR;

//                    // Get Error Information.
//                    JOB_STATUS_DETAILS_ERROR_TYPE errordetails = (result.jobstatus.details as JOB_STATUS_DETAILS_ERROR_TYPE);

//                    foreach (ERROR_TYPE errortype in errordetails.errorList)
//                    {
//                        if (!string.IsNullOrEmpty(errortype.code.ToString()))
//                        {
//                            ErrorCode = errortype.code.ToString();
//                            break;
//                        }
//                    }

//                    break;
//                case E_JOB_STATUS_TYPE.QUEUED:
//                case E_JOB_STATUS_TYPE.READY:
//                case E_JOB_STATUS_TYPE.SCANNED:
//                case E_JOB_STATUS_TYPE.STARTED:
//                default:
//                    // Marked by SES Jijianxiong 2010.11.18 ST
//                    // Now I can not get those method by MX-2600N
//                    // Maybe it will happen.
//                    // Marked by SES Jijianxiong 2010.11.18 ED
//                    Status = ((int) result.jobstatus.status).ToString();
//                    break;
//            }


//            // 2011.01.10 Update By SES Jijianxiong ST
//            // Base the Specification_SimpleEA(V1.31).doc

//            //// 2010.12.08 Update By SES Jijianxiong Ver.1.1 Update ST
//            //// Base the Specification_SimpleEA(V1.27)_20101203.doc

//            ////string strsql = "INSERT INTO [LogInformation]	"
//            ////    + "           ([ID]         "
//            ////    + "           ,[UserID]		"
//            ////    + "           ,[SerialNumber]"
//            ////    + "           ,[Jobuid]		"
//            ////    + "           ,[JobID]		"
//            ////    + "           ,[FunctionID]	"
//            ////    + "           ,[PageID]		"
//            ////    + "           ,[Number]		"
//            ////    + "           ,[Time]		"
//            ////    + "           ,[MFPName]	"
//            ////    + "           ,[PCName]		"
//            ////    + "           ,[FileName]	"
//            ////    + "           ,[Status]		"
//            ////    + "           ,[ErrorCode])	"
//            ////    + "     VALUES			    "
//            ////    + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
//            ////    + "           ,{0}		    "
//            ////    + "           ,{1}          "
//            ////    + "           ,{2}          "
//            ////    + "           ,{3}		    "
//            ////    + "           ,{4}		    "
//            ////    + "           ,{5}		    "
//            ////    + "           ,{6}		    "
//            ////    + "           ,getdate()    "
//            ////    + "           ,{7}          "
//            ////    + "           ,{8}		    "
//            ////    + "           ,{9}		    "
//            ////    + "           ,{10}		    "
//            ////    + "           ,{11})		";

//            ////string[] paramslist = new string[12];
//            ////// User ID
//            ////paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());
//            ////// SerialNumber
//            ////paramslist[1] = UtilCommon.ConvertStringToSQL(serialnumber);
//            ////// Jobuid
//            ////paramslist[2] = UtilCommon.ConvertStringToSQL(Jobuid);
//            ////// jobID
//            ////paramslist[3] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
//            ////// FunctionID
//            ////paramslist[4] = UtilCommon.ConvertIntToSQL(mfpjob.FunctionId.ToString());
//            ////// PageID
//            ////paramslist[5] = UtilCommon.ConvertIntToSQL(PageID.ToString());
//            ////// Number
//            ////paramslist[6] = UtilCommon.ConvertIntToSQL(Number.ToString());
//            ////// MFPName
//            ////paramslist[7] = UtilCommon.ConvertStringToSQL(result.deviceinfo.name);
//            ////// PCName
//            ////paramslist[8] = UtilCommon.ConvertStringToSQL(PCName);
//            ////// FileName
//            ////paramslist[9] = UtilCommon.ConvertStringToSQL(FileName);
//            ////// Status
//            ////paramslist[10] = UtilCommon.ConvertIntToSQL(Status);
//            ////// ErrorCode
//            ////paramslist[11] = UtilCommon.ConvertStringToSQL(ErrorCode);

//            //string strsql = "INSERT INTO [LogInformation]	"
//            //    + "           ([ID]             "
//            //    + "           ,[Time]           "
//            //    + "           ,[UserID]         "
//            //    + "           ,[Jobuid]         "
//            //    + "           ,[SerialNumber]   "
//            //    + "           ,[MFPName]        "
//            //    + "           ,[MFPModel]       "
//            //    + "           ,[MFPIPAddress]   "
//            //    + "           ,[Duplex]         "
//            //    + "           ,[JobID]          "
//            //    + "           ,[FunctionID]     "
//            //    + "           ,[FileName]       "
//            //    + "           ,[PageID]         "
//            //    + "           ,[Number]         "
//            //    //+ "           ,[PCName]         "     //2010.12.14 Delete By SES Jijiajianxiong
//            //    + "           ,[Status]         "
//            //    + "           ,[ErrorCode])     "

//            //    + "     VALUES			    "
//            //    + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
//            //    + "           ,getdate()    "
//            //    + "           ,{0}		    "
//            //    + "           ,{1}          "
//            //    + "           ,{2}          "
//            //    + "           ,{3}		    "
//            //    + "           ,{4}		    "
//            //    + "           ,{5}		    "
//            //    + "           ,{6}          "
//            //    + "           ,{7}          "
//            //    + "           ,{8}          "
//            //    + "           ,{9}          "
//            //    + "           ,{10}         "
//            //    + "           ,{11}         "
//            //    // + "           ,{12}         " // 2010.12.14 Delete By SES Jijianxiong
//            //    + "           ,{12}         "
//            //    + "           ,{13})        ";

//            //string[] paramslist = new string[14];
//            //// User ID
//            //paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());
//            //// Jobuid
//            //paramslist[1] = UtilCommon.ConvertStringToSQL(Jobuid);
//            //// SerialNumber
//            //paramslist[2] = UtilCommon.ConvertStringToSQL(serialnumber);

//            //// 2010.12.21 Update By SES Jijianxiong ST
//            //// Some times the deviceinfo.modelname is null.
//            //// Some times: MX-2700N
//            ////// MFPName
//            ////if (result.deviceinfo.name == null)
//            ////{
//            ////    paramslist[3] = UtilCommon.ConvertStringToSQL("");
//            ////}
//            ////else
//            ////{
//            ////    paramslist[3] = UtilCommon.ConvertStringToSQL(result.deviceinfo.name.Trim());
//            ////}
//            ////// MFP Model Name
//            ////paramslist[4] = UtilCommon.ConvertStringToSQL(result.deviceinfo.modelname);
//            ////// MFP Ip Address
//            ////paramslist[5] = UtilCommon.ConvertStringToSQL(result.deviceinfo.network_address);

//            //string strModelname = "";
//            //string strMFPIp = "";
//            //string strMFPName = "";
//            //strModelname = result.deviceinfo.modelname;
//            //strMFPIp = result.deviceinfo.network_address;
//            //strMFPName = result.deviceinfo.name;

//            //string struuid = result.deviceinfo.uuid;
//            //DEVICE_INFO_TYPE device_info = Helper.DeviceSession.Get(struuid).deviceinfo;
//            //if (string.IsNullOrEmpty(strModelname))
//            //{
//            //    strModelname = device_info.modelname;
//            //}

//            //if (string.IsNullOrEmpty(strMFPIp))
//            //{
//            //    strMFPIp = device_info.network_address;
//            //}

//            //if (string.IsNullOrEmpty(strMFPName))
//            //{
//            //    strMFPName = device_info.name;
//            //}

//            //// MFPName
//            //paramslist[3] = UtilCommon.ConvertStringToSQL(strMFPName);

//            //paramslist[4] = UtilCommon.ConvertStringToSQL(strModelname);

//            //// MFP Ip Address
//            //paramslist[5] = UtilCommon.ConvertStringToSQL(strMFPIp);
//            //// 2010.12.21 Update By SES Jijianxiong ST


//            //// Duplex
//            //paramslist[6] = UtilCommon.ConvertIntToSQL(((int)eDuplex).ToString());

//            //// jobID
//            //paramslist[7] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
//            //// FunctionID
//            //paramslist[8] = UtilCommon.ConvertIntToSQL(mfpjob.FunctionId.ToString());
//            //// FileName
//            //paramslist[9] = UtilCommon.ConvertStringToSQL(FileName);
//            //// PageID
//            //paramslist[10] = UtilCommon.ConvertIntToSQL(PageID.ToString());
//            //// Number
//            //paramslist[11] = UtilCommon.ConvertIntToSQL(Number.ToString());

//            ////2010.12.14 Update By SES Jijiajianxiong ST
//            //// PCName
//            //// paramslist[12] = UtilCommon.ConvertStringToSQL(PCName);
//            //// Status
//            //paramslist[12] = UtilCommon.ConvertIntToSQL(Status);
//            //// ErrorCode
//            //paramslist[13] = UtilCommon.ConvertStringToSQL(ErrorCode);
//            ////2010.12.14 Update By SES Jijiajianxiong ED

//            //// 2010.12.08 Update By SES Jijianxiong Ver.1.1 Update ED

//            // Get User Infor
//            LogUserInfor userinfor = new LogUserInfor(intUserId);
//            //chen 20140424 add start
//            int PaperTypeID;
//            Decimal SpendMoney;
//            int PriceDetailID;
//            int priceID;
//           // int priceCalMode;
//            Decimal paperPrice = 0;
//            Decimal grayPrice = 0;
//            Decimal colorPrice = 0;

//            if (PageID != null)
//            {
//                try
//                {
//                    PaperTypeID = UtilCommon.getPapeTypeID((int)PageID);
//                    priceID = UtilCommon.getMFPPriceID(serialnumber);
//                    //priceCalMode = UtilCommon.getPriceCalMode(priceID);
//                    PriceDetailID = UtilCommon.getPriceDetailID(priceID, PaperTypeID, mfpjob.JOBId);
//                    dtPriceDetailTableAdapters.PriceDetailTableAdapter pdAdapter = new dtPriceDetailTableAdapters.PriceDetailTableAdapter();
//                    dtPriceDetail.PriceDetailDataTable pdDt = pdAdapter.GetDataByPriceDetailID(PriceDetailID);
//                    if (pdDt.Rows.Count != 0)
//                    {
//                        paperPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).PaperPrice;
//                        grayPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).GrayPrice;
//                        colorPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).ColorPrice;
//                    }
//                }
//                catch
//                {
//                    PaperTypeID = 0;
//                    priceID = 0;
//                    //priceCalMode = 0;
//                    PriceDetailID = 0;
//                    paperPrice = 0;
//                    grayPrice = 0;
//                    colorPrice = 0;
//                }
//            }
//            else
//            {
//                PaperTypeID = 0;
//                priceID = 0;
//                //priceCalMode = 0;
//                PriceDetailID = 0;
//                paperPrice = 0;
//                grayPrice = 0;
//                colorPrice = 0;
//            }

//            //耗材价格
//            Decimal colPrice = 0;
//            if (mfpjob.FunctionId == 1)
//            {
//                colPrice = grayPrice;
//            }
//            else
//            {
//                colPrice = colorPrice;
//            }
//            int tmpNumber = 0;
//            if (Number == null)
//            {
//                tmpNumber = 0;
//            }
//            else
//            {
//                tmpNumber = (int)Number;
//            }
//            //if (priceCalMode == 0)
//            //{
//            //    //priceCalMode 0:  面数 * 单价
//            //    SpendMoney = (Decimal)tmpNumber * paperPrice;
//            //}
//            //else
//            //{
//            //    //priceCalMode 0:  总张数*纸张价格 + 面数 * 耗材单价
//            //    SpendMoney = (Decimal)papeCount * paperPrice + (int)tmpNumber * colPrice;
//            //}            //chen 20140424 add end
            
//            //priceCalMode 0:  总张数*纸张价格 + 面数 * 耗材单价
//            if (papeCount == null)
//            {
//                SpendMoney = 0;
//            }
//            else
//            {
//                SpendMoney = (Decimal)papeCount * paperPrice + (int)tmpNumber * colPrice;
//            }
//            string strsql = "INSERT INTO [LogInformation]	"
//                + "           ([ID]             "
//                + "           ,[Time]           "
//                + "           ,[UserID]         "

//                + "           ,[UserName]       "
//                + "           ,[LoginName]      "
//                + "           ,[GroupID]        "
//                + "           ,[GroupName]      "

//                + "           ,[Jobuid]         "
//                + "           ,[SerialNumber]   "
//                + "           ,[MFPName]        "
//                + "           ,[MFPModel]       "
//                + "           ,[MFPIPAddress]   "
//                + "           ,[Duplex]         "
//                + "           ,[JobID]          "
//                + "           ,[FunctionID]     "
//                + "           ,[FileName]       "
//                + "           ,[PageID]         "
//                + "           ,[Number]         "
//                //chen add 20140424 for money start
//                + "           ,[PapeCount]      "
//                + "           ,[CopyCount]        "
//                + "           ,[SpendMoney]     "
//                + "           ,[PriceDetailID]  "
//                //chen add 20140424 for money end
//                + "           ,[Status]         "
//                + "           ,[ErrorCode]     "
//                + "           ,[MFPPrintTaskID])     "

//                + "     VALUES			    "
//                + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
//                + "           ,getdate()    "
//                + "           ,{0}		    "

//                + "           ,{14}         "
//                + "           ,{15}         "
//                + "           ,{16}         "
//                + "           ,{17}         "

//                + "           ,{1}          "
//                + "           ,{2}          "
//                + "           ,{3}		    "
//                + "           ,{4}		    "
//                + "           ,{5}		    "
//                + "           ,{6}          "
//                + "           ,{7}          "
//                + "           ,{8}          "
//                + "           ,{9}          "
//                + "           ,{10}         "
//                + "           ,{11}         "
//                //chen add 20140424 for money start
//                + "           ,{19}         "
//                + "           ,{20}         "
//                + "           ,{21}         "
//                + "           ,{22}         "
//                //chen add 20140424 for money end
//                + "           ,{12}         "
//                + "           ,{13}         "
//                + "           ,{18})        ";


//            //chen upd 20140424 for money start
//            //string[] paramslist = new string[19];
//            string[] paramslist = new string[23];
//            //chen upd 20140424 for money end

//            // User ID
//            paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());

//            // UserName
//            paramslist[14] = UtilCommon.ConvertStringToSQL(userinfor.UserName);
//            // LoginName
//            paramslist[15] = UtilCommon.ConvertStringToSQL(userinfor.LoginName);
//            // GroupID
//            paramslist[16] = UtilCommon.ConvertIntToSQL(userinfor.GroupId.ToString());
//            // GroupName
//            paramslist[17] = UtilCommon.ConvertStringToSQL(userinfor.GroupName);

//            // Jobuid
//            paramslist[1] = UtilCommon.ConvertStringToSQL(Jobuid);
//            // SerialNumber
//            paramslist[2] = UtilCommon.ConvertStringToSQL(serialnumber);

            
//            string strModelname = "";
//            string strMFPIp = "";
//            string strMFPName = "";
//            strModelname = result.deviceinfo.modelname;
//            strMFPIp = result.deviceinfo.network_address;
//            strMFPName = result.deviceinfo.name;

//            string struuid = result.deviceinfo.uuid;
//            DEVICE_INFO_TYPE device_info = Helper.DeviceSession.Get(struuid).deviceinfo;
//            if (string.IsNullOrEmpty(strModelname))
//            {
//                strModelname = device_info.modelname;
//            }

//            if (string.IsNullOrEmpty(strMFPIp))
//            {
//                strMFPIp = device_info.network_address;
//            }

//            if (string.IsNullOrEmpty(strMFPName))
//            {
//                strMFPName = device_info.name;
//            }

//            // MFPName
//            paramslist[3] = UtilCommon.ConvertStringToSQL(strMFPName);

//            // MFP Model Name
//            paramslist[4] = UtilCommon.ConvertStringToSQL(strModelname);

//            // MFP Ip Address
//            paramslist[5] = UtilCommon.ConvertStringToSQL(strMFPIp);

//            // Duplex
//            paramslist[6] = UtilCommon.ConvertIntToSQL(((int)eDuplex).ToString());

//            // jobID
//            paramslist[7] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());

//            //2012.07.05 modify by Wei Changye 

//            // follow me task id  
//            // add by Wei Changye 2012.03.05
//            //int MFPTaskID ;
//            //int.TryParse(PCName,out MFPTaskID);
//            //paramslist[18] = UtilCommon.ConvertIntToSQL(MFPTaskID.ToString().Trim());
//            //end add

//            paramslist[18] = UtilCommon.ConvertIntToSQL("0");

//            // end modify

//            // FunctionID
//            paramslist[8] = UtilCommon.ConvertIntToSQL(mfpjob.FunctionId.ToString());
//            // FileName
//            paramslist[9] = UtilCommon.ConvertStringToSQL(FileName);
//            // PageID
//            paramslist[10] = UtilCommon.ConvertIntToSQL(PageID.ToString());
//            // Number
//            paramslist[11] = UtilCommon.ConvertIntToSQL(Number.ToString());

//            // Status
//            paramslist[12] = UtilCommon.ConvertIntToSQL(Status);
//            // ErrorCode
//            paramslist[13] = UtilCommon.ConvertStringToSQL(ErrorCode);
//            // 2011.01.10 Update By SES Jijianxiong ED


//            //chen add 20140424 for money start
//            paramslist[19] = UtilCommon.ConvertIntToSQL(papeCount.ToString());
//            paramslist[20] = UtilCommon.ConvertIntToSQL(copyCount.ToString());
//            paramslist[21] = UtilCommon.ConvertDecimalToSQL(SpendMoney.ToString());
//            paramslist[22] = UtilCommon.ConvertIntToSQL(PriceDetailID.ToString());
//            //chen add 20140424 for money start


//            strsql = string.Format(strsql, paramslist);

//            // Ex SQL.
//            using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
//            {
//                con.Open();
//                using (SqlCommand cmd = new SqlCommand(strsql, con))
//                {
//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }
        private void RecordEndLog(int intUserId, EVENT_DATA_TYPE eventdata)
        {
            string serialnumber = string.Empty;
            //string Jobuid;
            string Status;
            string ErrorCode = null;

            LogDataInfo logInfo = new LogDataInfo();

            DETAILS_ON_JOB_COMPLETED_TYPE detalCompleted = (DETAILS_ON_JOB_COMPLETED_TYPE)eventdata.details;
            JOB_RESULTS_BASE_TYPE result = (JOB_RESULTS_BASE_TYPE)detalCompleted.JobResults;
            ;
            // Get the uid for this job.
            logInfo.Jobuid = (result.mfpjobid.Item as OSA_JOB_ID_TYPE).uid;
            // serialnumber
            serialnumber = eventdata.deviceinfo.serialnumber;

            // Get JobID and ColorType
            // JobName
            string jobName = MapJobModeToLimits(result.jobmode);
            // Color-Type
            string ColorType = "";
            foreach (PROPERTY_SET_TYPE propertie in result.properties)
            {
                if (propertie.sysname.Equals("color-mode"))
                {
                    ColorType = propertie.Value;
                    break;
                }
            }
            mfpjob = GetMFPJob(jobName, ColorType);

            logInfo.JOBId = mfpjob.JOBId;


            if (string.IsNullOrEmpty(serialnumber))
            {
                serialnumber = DeviceSession.GetSerialnumberByDevid(eventdata.deviceinfo.uuid);
            }
            logInfo.Serialnumber = serialnumber;


            // Get User Infor
            LogUserInfor userinfor = new LogUserInfor(intUserId);
            
            logInfo.UserId = intUserId;

            logInfo.UserName = userinfor.UserName;
            logInfo.LoginName = userinfor.LoginName;
            logInfo.GroupId = userinfor.GroupId.ToString();
            logInfo.GroupName = userinfor.GroupName;

            string strModelname = "";
            string strMFPIp = "";
            string strMFPName = "";
            strModelname = result.deviceinfo.modelname;
            strMFPIp = result.deviceinfo.network_address;
            strMFPName = result.deviceinfo.name;

            string struuid = result.deviceinfo.uuid;
            DEVICE_INFO_TYPE device_info = Helper.DeviceSession.Get(struuid).deviceinfo;
            if (string.IsNullOrEmpty(strModelname))
            {
                strModelname = device_info.modelname;
            }

            if (string.IsNullOrEmpty(strMFPIp))
            {
                strMFPIp = device_info.network_address;
            }

            if (string.IsNullOrEmpty(strMFPName))
            {
                strMFPName = device_info.name;
            }

            logInfo.MFPModel = strModelname;
            logInfo.MFPIp = strMFPIp;
            logInfo.MFPName = strMFPName;



            switch (result.jobstatus.status)
            {
                case E_JOB_STATUS_TYPE.CANCELED:
                    Status = UtilConst.JOB_STATUS_CANCELED;
                    // Get the Job Type, page Type and page count.
                    this.RecordDataParse(eventdata, logInfo);

                    break;
                case E_JOB_STATUS_TYPE.FINISHED:
                    Status = UtilConst.JOB_STATUS_FINISHED;
                    // Get the Job Type, page Type and page count.
                    this.RecordDataParse(eventdata, logInfo);

                    break;
                case E_JOB_STATUS_TYPE.SUSPENDED:
                    Status = UtilConst.JOB_STATUS_SUSPENDED;
                    // Get Error Information.
                    JOB_STATUS_DETAILS_SUSPENDED_TYPE suspendeddetails = (result.jobstatus.details as JOB_STATUS_DETAILS_SUSPENDED_TYPE);

                    foreach (ERROR_TYPE errortype in suspendeddetails.errorList)
                    {
                        if (!string.IsNullOrEmpty(errortype.code.ToString()))
                        {
                            ErrorCode = errortype.code.ToString();
                            break;
                        }
                    }

                    break;
                case E_JOB_STATUS_TYPE.ERROR:
                    Status = UtilConst.JOB_STATUS_ERROR;

                    // Get Error Information.
                    JOB_STATUS_DETAILS_ERROR_TYPE errordetails = (result.jobstatus.details as JOB_STATUS_DETAILS_ERROR_TYPE);

                    foreach (ERROR_TYPE errortype in errordetails.errorList)
                    {
                        if (!string.IsNullOrEmpty(errortype.code.ToString()))
                        {
                            ErrorCode = errortype.code.ToString();
                            break;
                        }
                    }

                    break;
                case E_JOB_STATUS_TYPE.QUEUED:
                case E_JOB_STATUS_TYPE.READY:
                case E_JOB_STATUS_TYPE.SCANNED:
                case E_JOB_STATUS_TYPE.STARTED:
                default:
                    // Marked by SES Jijianxiong 2010.11.18 ST
                    // Now I can not get those method by MX-2600N
                    // Maybe it will happen.
                    // Marked by SES Jijianxiong 2010.11.18 ED
                    Status = ((int)result.jobstatus.status).ToString();
                    break;
            }

            logInfo.Status = Status;
            logInfo.ErrorCode = ErrorCode;
            //20170614 add 
            //获得最大的ID
            //int id = 0;
            //string sql = "SELECT MAX(ID) From [LogInformation]";
            //using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
            //{
            //    con.Open();
            //    try
            //    {
            //        using (SqlCommand cmd = new SqlCommand(sql, con))
            //        {
            //            id = (int)cmd.ExecuteScalar();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //    finally
            //    {
            //        ;
            //    }
            //}
            //logInfo.ID = id + 1;  //人工


            logInfo.Time = System.DateTime.Now;

            //pdf filename
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            string hour = DateTime.Now.Hour.ToString();
            string minute = DateTime.Now.Minute.ToString();
            string second = DateTime.Now.Second.ToString();

            string strDateTime = year+"_"+month+"_"+day+"_"+hour+"_"+minute+"_"+second;

            string pdf_filename = logInfo.FileName;
            string db_pdf_filename = logInfo.ID.ToString() + "_" + logInfo.UserName + "_" + strDateTime;

            //20170614 add start for 打印复印
 
            BllMFP bllMFP = new BllMFP();
            MFPEntry mfpEntry = bllMFP.GetMFPInfo(serialnumber);
            int Monitor = 0;
            Boolean monitor_flg = false;
            if (mfpEntry != null)
            {
                Monitor = mfpEntry.Monitor;
            }
            int printMonitor = Monitor % 10;
            int copyMonitor = Monitor / 10 % 10;
            int bw = Monitor / 100;

            //if (Monitor == 1 && logInfo.JOBId == 2)
            if (printMonitor == 1 && logInfo.JOBId == 2)
            {
                //logInfo.FileName = db_pdf_filename;
                //logInfo.FileName = pdf_filename;
                monitor_flg = true;
            }
            //else if (Monitor == 2 && logInfo.JOBId == 1)
            if (copyMonitor == 1 && logInfo.JOBId == 1)
            {
                monitor_flg = true;
            }
            //else if (Monitor == 3 && (logInfo.JOBId == 1 || logInfo.JOBId == 2) )
            //{
            //    monitor_flg = true;
            //}


            if (logInfo.sheetCount1 == 0 && logInfo.sheetCount2 == 0)
            {
                int id = 0;
                string sql = "SELECT MAX(ID) From [LogInformation]";
                using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
                {
                    con.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            id = (int)cmd.ExecuteScalar();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        ;
                    }
                }
                logInfo.ID = id + 1;  //人工
                RecordLoginfoToDB(logInfo, 1);
            }
            else
            {
                if (logInfo.sheetCount1 > 0)
                {
                    int id = 0;
                    string sql = "SELECT MAX(ID) From [LogInformation]";
                    using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
                    {
                        con.Open();
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {
                                id = (int)cmd.ExecuteScalar();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            ;
                        }
                    }
                    logInfo.ID = id + 1;  //人工
                    RecordLoginfoToDB(logInfo, 1);
                }
                if (logInfo.sheetCount2 > 0)
                {
                    int id = 0;
                    string sql = "SELECT MAX(ID) From [LogInformation]";
                    using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
                    {
                        con.Open();
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(sql, con))
                            {
                                id = (int)cmd.ExecuteScalar();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            ;
                        }
                    }
                    logInfo.ID = id + 1;  //人工
                    RecordLoginfoToDB(logInfo, 2);
                }
            }

            string mfp_ip = mfpEntry.IPAddress;


            if (monitor_flg == true)
            {
                BllPrintCopy bll = new BllPrintCopy();
                PrintCopyEntry bean = new PrintCopyEntry();
                bean.ID = logInfo.ID;
                bean.Time = logInfo.Time;
                bean.UserID = logInfo.UserId;
                bean.IpAddress = mfp_ip;
                bean.CopyType = logInfo.JOBId;
                bean.OrigFile = pdf_filename;
                bean.CopyFile = db_pdf_filename;// logInfo.FileName;
                bean.Finished = 0;
                bean.CopyTimes = 0;
                bll.Add(bean);
            }

            
            //RunCopyPrintPDF();

            //copy留底pdf文件到指定文件夹
//            if (pdf_filename != null && pdf_filename != "")

            //if (monitor_flg == true)
            //{
                //BllCopyConfig bllCopycfg = new BllCopyConfig();
                //Model.CopyConfigEntry conf_entry = bllCopycfg.GetCopyConfigInfo();
                //string dst_folder = HttpContext.Current.Server.MapPath(".") + "/CopyFilePDF/";
                //string src_folder = @"\\" + mfp_ip + @"\public\main\";
                //string src_filename = src_folder + pdf_filename + ".pdf";
                //string dst_filename = dst_folder + db_pdf_filename + ".pdf";

                //if (!FileCommon.FileProcess.FolderExist(dst_folder))
                //{
                //    FileCommon.FileProcess.FolderCreate(dst_folder);
                //}
                //Boolean connect_flg = FileCommon.NetFileProcess.NetConnect(mfp_ip, "admin", "admin");
                ////Boolean connect_status = SimpleEACommon.FileShare.connectState(src_folder, "admin", "admin");
                ////if (connect_status == true)
                //long fileSize=0;
                ////留底复制过程时间过长 异常处理
                //DalPrintCopy dpc = new DalPrintCopy();
                //PrintCopyEntry pce = dpc.GetInfoByID(logInfo.ID);
                //int finished = pce.Finished;
                //int copytimes = pce.CopyTimes;

                //if (finished != 1 && copytimes <=5)
                //{
                //    System.Threading.Thread.Sleep(5000);
                //    {

                //        for (int i = 0; i < 500; i++)
                //        {
                //            FileInfo fi = new FileInfo(src_filename);
                //            if (fi != null && fi.Exists)
                //            {
                //                if (fileSize != fi.Length)
                //                {
                //                    fileSize = fi.Length;
                //                }
                //                else
                //                {
                //                    Boolean ret = FileCommon.FileShare.NetCopy(src_filename, dst_filename);
                //                    //成功留底
                //                    finished = 1;
                //                    break;
                //                }

                //            }
                //            System.Threading.Thread.Sleep(1000 * 10);
                //            //Boolean ret = SimpleCommon.FileShare.NetCopy(src_filename, dst_filename);
                //            //if (ret)
                //            //{
                //            //    //SimpleEACommon.FileProcess.FileCopy(src_filename, dst_filename);
                //            //    //SimpleEACommon.FileProcess.NetCopy(mfp_ip, "admin", "admin", src_filename, dst_filename);
                //            //    //SimpleEACommon.FileProcess.DeleteFile(src_filename);
                //            //    break;
                //            //}
                //            //else
                //            //{
                //            //    System.Threading.Thread.Sleep(1000);
                //            //}
                //        }
                //    }
                //}
                //copytimes = copytimes + 1;
                //pce.CopyTimes = copytimes;
                //pce.Finished = finished;
                //dpc.Update(pce);
                
            //}
            //20170614 add end for 打印复印留底 
        }

        //public void RunCopyPrintPDF()
        //{

        //    DalPrintCopy dal = new DalPrintCopy();

        //    List<PrintCopyEntry> beanlist = dal.SearchRecord();

        //    foreach (PrintCopyEntry bean in beanlist)
        //    {
        //        if (bean.Finished == 0 && bean.CopyTimes < 5)
        //        {
        //            Boolean flg = copyPrintPdfFile(bean);
        //            if (flg)
        //            {
        //                bean.Finished = 1;
        //            }
        //            bean.CopyTimes = bean.CopyTimes + 1;
        //            dal.Update(bean);
        //        }
        //    }
        //}
        //public Boolean copyPrintPdfFile(PrintCopyEntry bean)
        //{

        //    string ip = bean.IpAddress;
        //    string origfile = bean.OrigFile + ".pdf"; ;
        //    string dstPath =  HttpContext.Current.Server.MapPath(".") + "/CopyFilePDF/";//ServiceCommon.GetAppSettingString("CopyPDFFilepath");
        //    string dstfile = dstPath + bean.CopyFile + ".pdf";

        //    Boolean flg = FileCommon.NetFileProcess.netCopy3(ip, origfile, dstfile);
        //    return flg;
        //}


        #endregion

        //20140619 chen add for color auto start
        public void RecordDataParse(EVENT_DATA_TYPE evt, LogDataInfo logInfo)
        {
            string accid = evt.userinfo.accountid;

            DETAILS_ON_JOB_COMPLETED_TYPE details = (DETAILS_ON_JOB_COMPLETED_TYPE)evt.details;
            JOB_RESULTS_BASE_TYPE jr = (JOB_RESULTS_BASE_TYPE)details.JobResults;

            RESOURCE_PAPER_TYPE[] paperout;
            if (jr is JOB_RESULTS_COPY_TYPE)
            {
                paperout = ((JOB_RESULTS_COPY_TYPE)jr).resources.paperout;
            }
            else if (jr is JOB_RESULTS_PRINT_TYPE)
            {
                paperout = ((JOB_RESULTS_PRINT_TYPE)jr).resources.paperout;
            }
            else if (jr is JOB_RESULTS_SCAN_TYPE)
            {
                paperout = ((JOB_RESULTS_SCAN_TYPE)jr).resources.paperout;
            }
            else if (jr is JOB_RESULTS_DOCFILING_TYPE)
            {
                paperout = ((JOB_RESULTS_DOCFILING_TYPE)jr).resources.paperout;
            }
            else return;

            if (null != paperout)
            {
                RecordDataCounts(accid, paperout, jr, logInfo);
            }
        }

        private void RecordDataCounts(string accid,
            RESOURCE_PAPER_TYPE[] pout, 
            JOB_RESULTS_BASE_TYPE results,
            LogDataInfo logInfo)
        {

            uint sheetCount1 = 0;
            uint papeCount1 = 0;
            uint copyCount1 = 0;
            string propname1 = "";
            string propvalue1 = "";

            uint sheetCount2 = 0;
            uint papeCount2 = 0;
            uint copyCount2 = 0;
            string propname2 = "";
            string propvalue2 = "";
            //get copy count
            foreach (RESOURCE_PAPER_TYPE pt in pout)
            {
                //份数
                if (pt.copiescount != null)
                {
                    copyCount1 = Convert.ToUInt32(pt.copiescount[0].Value);
                    copyCount2 = Convert.ToUInt32(pt.copiescount[0].Value);
                    break;
                }


            }

            ////get copy count
            //foreach (RESOURCE_PAPER_TYPE pt in pout)
            //{
            //    //if (!IsForCountingClicks(pt)) break;
            //    if (pt.sheetcount == null)
            //    {
            //        continue;
            //    }
            //    foreach (PROPERTY_SET_TYPE property in pt.property)
            //    {
            //        if (property == null)
            //        {
            //            break;
            //        }
            //        if (property.Value == null)
            //        {
            //            break;
            //        }
            //        if (property.Value == "MONOCHROME")
            //        {
            //            propname1 = property.sysname;
            //            propvalue1 = property.Value;
            //            //面数sheetcount
            //            if (pt.sheetcount != null)
            //            {
            //                sheetCount1 = Convert.ToUInt32(pt.sheetcount);
            //            }
            //            //纸张的数量
            //            if (pt.pagecount != null)
            //            {
            //                papeCount1 = Convert.ToUInt32(pt.pagecount[0].Value);
            //            }
            //            break;
            //        }
            //        else
            //        //if (property.Value == "FULL-COLOR")
            //        {
            //            propname2 = property.sysname;
            //            propvalue2 = property.Value;
            //            //面数sheetcount
            //            if (pt.sheetcount != null)
            //            {
            //                sheetCount2 = Convert.ToUInt32(pt.sheetcount);
            //            }
            //            //纸张的数量
            //            if (pt.pagecount != null)
            //            {
            //                papeCount2 = Convert.ToUInt32(pt.pagecount[0].Value);
            //            }
            //            break;
            //        }

            //    }

            //}

            //get copy count
            if (logInfo.JOBId == UtilConst.ITEM_TITLE_Copy_JobId ||
                logInfo.JOBId == UtilConst.ITEM_TITLE_Print_JobId)
            {
                foreach (RESOURCE_PAPER_TYPE pt in pout)
                {
                    //if (!IsForCountingClicks(pt)) break;
                    if (pt.sheetcount == null)
                    {
                        continue;
                    }
                    if (pt.property == null)
                    {
                        continue;
                    }

                    //20141027 add start
                    Boolean flg = false;
                    foreach (PROPERTY_SET_TYPE property in pt.property)
                    {
                        if (property.sysname.Equals("papersize"))
                        {
                            flg = true;
                        }
                    }
                    if (flg == false)
                    {
                        continue;
                    }
                    //end
                    foreach (PROPERTY_SET_TYPE property in pt.property)
                    {
                        if (property == null)
                        {
                            break;
                        }
                        if (property.Value == null)
                        {
                            break;
                        }
                       
                        if (property.Value == "MONOCHROME")
                        {
                            propname1 = property.sysname;
                            propvalue1 = property.Value;
                            //面数sheetcount
                            if (pt.sheetcount != null)
                            {
                                sheetCount1 += Convert.ToUInt32(pt.sheetcount);
                            }
                            //纸张的数量
                            if (pt.pagecount != null)
                            {
                                papeCount1 += Convert.ToUInt32(pt.pagecount[0].Value);
                            }
                            break;
                        }
                        else
                        //if (property.Value == "FULL-COLOR")
                        {
                            propname2 = property.sysname;
                            propvalue2 = property.Value;
                            //面数sheetcount
                            if (pt.sheetcount != null)
                            {
                                sheetCount2 += Convert.ToUInt32(pt.sheetcount);
                            }
                            //纸张的数量
                            if (pt.pagecount != null)
                            {
                                papeCount2 += Convert.ToUInt32(pt.pagecount[0].Value);
                            }
                            break;
                        }

                    }

                }

            }
            else
            {
                foreach (RESOURCE_PAPER_TYPE pt in pout)
                {
                    if (pt.sheetcount == null)
                    {
                        continue;
                    }
                    if (pt.property == null)
                    {
                        continue;
                    }
                    if (pt.property != null)
                    {
                        foreach (PROPERTY_SET_TYPE property in pt.property)
                        {
                            if (property == null)
                            {
                                break;
                            }
                            if (property.Value == null)
                            {
                                break;
                            }
                            propname1 = property.sysname;
                            propvalue1 = property.Value;
                            break;
                        }

                    }

                    //20141027 add start
                    Boolean flg = false;
                    foreach (PROPERTY_SET_TYPE property in pt.property)
                    {
                        if (property.sysname.Equals("papersize"))
                        {
                            flg = true;
                        }
                    }
                    if (flg == false)
                    {
                        continue;
                    }
                    //end
                    //面数sheetcount
                    if (pt.sheetcount != null)
                    {
                        sheetCount1 += Convert.ToUInt32(pt.sheetcount);
                    }
                    //纸张的数量
                    if (pt.pagecount != null)
                    {
                        papeCount1 += Convert.ToUInt32(pt.pagecount[0].Value);
                    }

                    // }

                }

                //scan 
                papeCount1 = sheetCount1;
            }

            if (sheetCount1 > 0)
            {
                logInfo.colorType1 = propvalue1;
                logInfo.sheetCount1 = sheetCount1;
                logInfo.papeCount1 = papeCount1;
                logInfo.copyCount1 = copyCount1;
                
                ////20141027 add
                //if (logInfo.PageID.Equals(UtilConst.CON_PAGE_A3.ToString())
                //    //&& logInfo.JOBId != UtilConst.ITEM_TITLE_Scan_JobId
                //    //&& logInfo.JOBId != UtilConst.ITEM_TITLE_ScanSave_JobId
                //    && ( logInfo.JOBId == UtilConst.ITEM_TITLE_Copy_JobId
                //    || logInfo.JOBId == UtilConst.ITEM_TITLE_Print_JobId
                //    || logInfo.JOBId == UtilConst.ITEM_TITLE_DFPrint_JobId )
                //    )
                //{
                    
                //    int DspSheetCount = 0;
                //    int DspPapeCount = 0;
                //    if (logInfo.Duplex == 1)
                //    {
                //        DspSheetCount = (int)logInfo.sheetCount1 / 2;
                //        DspPapeCount = (int)logInfo.sheetCount1 / 2;
                //        logInfo.papeCount1 = (uint)DspPapeCount * 2;
                //    }
                //    else
                //    {
                //        DspSheetCount = (int)logInfo.sheetCount1 / 2;
                //        DspPapeCount = (int)(logInfo.sheetCount1 / logInfo.copyCount1 + 2) / 4;
                //        DspPapeCount = (int)(DspPapeCount * logInfo.copyCount1);

                //        logInfo.papeCount1 = (uint)DspPapeCount * 2;
                //    }

                //    logInfo.DspPapeCount1 = DspPapeCount;
                //    logInfo.DspSheetCount1 = DspSheetCount;
                //}

                ////
                RecordDataCount(accid, sheetCount1, papeCount1, copyCount1, propname1, propvalue1, results, logInfo, 1);
                RecordMoney(logInfo.UserId, logInfo.Serialnumber, logInfo.JOBId, logInfo.colorType1, logInfo, 1);
            }
            if (sheetCount2 > 0)
            {
                logInfo.colorType2 = propvalue2;
                logInfo.sheetCount2 = sheetCount2;
                logInfo.papeCount2 = papeCount2;
                logInfo.copyCount2 = copyCount2;


                ////20141027 add
                //if (logInfo.PageID.Equals(UtilConst.CON_PAGE_A3.ToString())
                //    //&& logInfo.JOBId != UtilConst.ITEM_TITLE_Scan_JobId
                //    //&& logInfo.JOBId != UtilConst.ITEM_TITLE_ScanSave_JobId
                //    && (logInfo.JOBId == UtilConst.ITEM_TITLE_Copy_JobId
                //    || logInfo.JOBId == UtilConst.ITEM_TITLE_Print_JobId
                //    || logInfo.JOBId == UtilConst.ITEM_TITLE_DFPrint_JobId)
                //    )
                //{

                //    int DspSheetCount = 0;
                //    int DspPapeCount = 0;
                //    if (logInfo.Duplex == 1)
                //    {
                //        DspSheetCount = (int)logInfo.sheetCount2 / 2;
                //        DspPapeCount = (int)logInfo.sheetCount2 / 2;
                //        logInfo.papeCount2 = (uint)DspPapeCount * 2;
                //    }
                //    else
                //    {
                //        DspSheetCount = (int)logInfo.sheetCount2 / 2;
                //        DspPapeCount = (int)(logInfo.sheetCount2 / logInfo.copyCount2 + 2) / 4;
                //        DspPapeCount = (int)(DspPapeCount * logInfo.copyCount2);
                //        logInfo.papeCount2 = (uint)DspPapeCount * 2;
                //    }

                //    logInfo.DspPapeCount2 = DspPapeCount;
                //    logInfo.DspSheetCount2 = DspSheetCount;
                //}

                ////
                RecordDataCount(accid, sheetCount2, papeCount2, copyCount2, propname2, propvalue2, results, logInfo, 2);
                RecordMoney(logInfo.UserId, logInfo.Serialnumber, logInfo.JOBId, logInfo.colorType2, logInfo, 2);
            }                      
        }
        protected void RecordDataCount(string accid, uint sheetCount, uint _papeCount, uint _copyCount, string propName, string propValue, JOB_RESULTS_BASE_TYPE results, LogDataInfo logInfo, int flg)
        {
            if (0 < sheetCount)
            {
                // UserId
                int userid = int.Parse(accid);
                // JobName
                string jobName = MapJobModeToLimits(results.jobmode);
                // Color-Type
                string ColorType = GetColorType(propName, propValue);

                UtilCommon.MFPJob mfpjob = GetMFPJob(jobName, ColorType);

                if (flg == 1)
                {
                    logInfo.JobID1 = mfpjob.JOBId;
                    logInfo.JobName1 = jobName;
                    logInfo.FunctionId1 = (int)mfpjob.FunctionId;
                }
                else
                {
                    logInfo.JobID2 = mfpjob.JOBId;
                    logInfo.JobName2 = jobName;
                    logInfo.FunctionId2 = (int)mfpjob.FunctionId;
                }
                // Page-Type
                string pageType = GetPageType(results);

                // Get PageSize ID.
                PageID = UtilConst.CON_DATE_OTHER_PAGE;


                if (pageType == "")
                {
                    // Page is Unused.
                    PageID = UtilConst.CON_DATE_UNUSED_PAGE;
                }
                else
                {
                    dtPaperSizeInformationTableAdapters.PaperSizeInformationTableAdapter adapter = new dtPaperSizeInformationTableAdapters.PaperSizeInformationTableAdapter();
                    dtPaperSizeInformation.PaperSizeInformationDataTable dt = adapter.GetDataByPageSize(pageType);
                    if (dt.Rows.Count == 0)
                    {
                        // The Page Type is unknow for Simple EA.
                        PageID = UtilConst.CON_DATE_OTHER_PAGE;
                    }
                    else
                    {
                        PageID = ((dtPaperSizeInformation.PaperSizeInformationRow)dt.Rows[0]).ID;
                    }
                }

                logInfo.PageID = PageID.ToString(); ;

                //// Page-Num
                //Number = (int)sheetCount;
                ////chen add 20140424 for money start
                //papeCount = (int)_papeCount;
                //copyCount = (int)_copyCount;
                ////chen add 20140424 for money end

                // Get the File Information for the REMOTE-JOB.
                foreach (PROPERTY_SET_TYPE property in results.properties)
                {
                    switch (property.sysname)
                    {
                        case "computer-name":
                            PCName = property.Value;
                            break;
                        case "filename":
                            FileName = property.Value;
                            break;
                        case "duplex-mode":
                            eDuplex = GetDuplex(property.Value);
                            break;
                        default:
                            break;
                    }
                }


                logInfo.PCName = PCName;
                logInfo.FileName = FileName;
                logInfo.Duplex = (int)eDuplex;


                //DspSheetCount = (int)logInfo.sheetCount1;
                //DspPapeCount = (int)logInfo.papeCount1;


                ////20141027 add
                if (flg == 1)
                {
                    logInfo.DspPapeCount1 = (int)logInfo.papeCount1;
                    logInfo.DspSheetCount1 = (int)logInfo.sheetCount1;

                    if (logInfo.PageID.Equals(UtilConst.CON_PAGE_A3.ToString())
                        //&& logInfo.JOBId != UtilConst.ITEM_TITLE_Scan_JobId
                        //&& logInfo.JOBId != UtilConst.ITEM_TITLE_ScanSave_JobId
                        && (logInfo.JOBId == UtilConst.ITEM_TITLE_Copy_JobId
                        || logInfo.JOBId == UtilConst.ITEM_TITLE_Print_JobId
                        || logInfo.JOBId == UtilConst.ITEM_TITLE_DFPrint_JobId)
                        )
                    {

                        int DspSheetCount = 0;
                        int DspPapeCount = 0;
                        if (logInfo.Duplex == 1)
                        {
                            DspSheetCount = (int)logInfo.sheetCount1 / 2;
                            DspPapeCount = (int)logInfo.sheetCount1 / 2;
                            logInfo.papeCount1 = (uint)DspPapeCount * 2;
                        }
                        else
                        {
                            DspSheetCount = (int)logInfo.sheetCount1 / 2;
                            DspPapeCount = (int)(logInfo.sheetCount1 / logInfo.copyCount1 + 2) / 4;
                            DspPapeCount = (int)(DspPapeCount * logInfo.copyCount1);

                            logInfo.papeCount1 = (uint)DspPapeCount * 2;
                        }

                        logInfo.DspPapeCount1 = DspPapeCount;
                        logInfo.DspSheetCount1 = DspSheetCount;
                    }

                }
                else
                {
                    logInfo.DspPapeCount2 = (int)logInfo.papeCount2;
                    logInfo.DspSheetCount2 = (int)logInfo.sheetCount2;
                    ////20141027 add
                    if (logInfo.PageID.Equals(UtilConst.CON_PAGE_A3.ToString())
                        //&& logInfo.JOBId != UtilConst.ITEM_TITLE_Scan_JobId
                        //&& logInfo.JOBId != UtilConst.ITEM_TITLE_ScanSave_JobId
                        && (logInfo.JOBId == UtilConst.ITEM_TITLE_Copy_JobId
                        || logInfo.JOBId == UtilConst.ITEM_TITLE_Print_JobId
                        || logInfo.JOBId == UtilConst.ITEM_TITLE_DFPrint_JobId)
                        )
                    {

                        int DspSheetCount = 0;
                        int DspPapeCount = 0;
                        if (logInfo.Duplex == 1)
                        {
                            DspSheetCount = (int)logInfo.sheetCount2 / 2;
                            DspPapeCount = (int)logInfo.sheetCount2 / 2;
                            logInfo.papeCount2 = (uint)DspPapeCount * 2;
                        }
                        else
                        {
                            DspSheetCount = (int)logInfo.sheetCount2 / 2;
                            DspPapeCount = (int)(logInfo.sheetCount2 / logInfo.copyCount2 + 2) / 4;
                            DspPapeCount = (int)(DspPapeCount * logInfo.copyCount2);
                            logInfo.papeCount2 = (uint)DspPapeCount * 2;
                        }

                        logInfo.DspPapeCount2 = DspPapeCount;
                        logInfo.DspSheetCount2 = DspSheetCount;
                    }

                }
                //
            }
        }

        private void RecordMoney(int intUserId, string serialnumber, int JOBId, string colorType, LogDataInfo logInfo, int flg)
        {

            // Get User Infor
            LogUserInfor userinfor = new LogUserInfor(intUserId);
            int PaperTypeID;
            Decimal SpendMoney;
            int PriceDetailID;
            int priceID;
            // int priceCalMode;
            Decimal paperPrice = 0;
            Decimal grayPrice = 0;
            Decimal colorPrice = 0;

            dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
            int Dsp_A3_A4 = settingrow.Dis_A3_A4;

            int Dsp_Count_mode = settingrow.Dis_Count_mode;

            if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
            {

                if (PageID != null)
                {
                    try
                    {
                        int tmpPapeID = (int)PageID;
                        if (tmpPapeID == UtilConst.CON_PAGE_A3) //A3
                        {
                            if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A4))
                            {
                                tmpPapeID = UtilConst.CON_PAGE_A4; //A4
                            }
                        }


                        //PaperTypeID = UtilCommon.getPapeTypeID((int)PageID);
                        PaperTypeID = UtilCommon.getPapeTypeID(tmpPapeID);

                        priceID = UtilCommon.getMFPPriceID(serialnumber);

                        //priceCalMode = UtilCommon.getPriceCalMode(priceID);
                        PriceDetailID = UtilCommon.getPriceDetailID(priceID, PaperTypeID, JOBId);
                        dtPriceDetailTableAdapters.PriceDetailTableAdapter pdAdapter = new dtPriceDetailTableAdapters.PriceDetailTableAdapter();
                        dtPriceDetail.PriceDetailDataTable pdDt = pdAdapter.GetDataByPriceDetailID(PriceDetailID);
                        if (pdDt.Rows.Count != 0)
                        {
                            paperPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).PaperPrice;
                            grayPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).GrayPrice;
                            colorPrice = ((dtPriceDetail.PriceDetailRow)pdDt.Rows[0]).ColorPrice;
                        }



                    }
                    catch
                    {
                        PaperTypeID = 0;
                        priceID = 0;
                        //priceCalMode = 0;
                        PriceDetailID = 0;
                        paperPrice = 0;
                        grayPrice = 0;
                        colorPrice = 0;
                    }
                }
                else
                {
                    PaperTypeID = 0;
                    priceID = 0;
                    //priceCalMode = 0;
                    PriceDetailID = 0;
                    paperPrice = 0;
                    grayPrice = 0;
                    colorPrice = 0;
                }



                if (flg == 1)
                {
                    logInfo.PriceDetailID1 = PriceDetailID;
                }
                else
                {
                    logInfo.PriceDetailID2 = PriceDetailID;
                }



                //耗材价格
                Decimal colPrice = 0;

                //if (logInfo.FunctionId1 == 1)
                if (colorType == "MONOCHROME")
                {
                    colPrice = grayPrice;
                }
                else
                {
                    colPrice = colorPrice;
                }
                int tmpSheetCount = 0;
                int tmpPaperCount = 0;

                if (flg == 1)
                {
                    tmpSheetCount = (int)logInfo.sheetCount1;
                    tmpPaperCount = (int)logInfo.papeCount1;
                }
                else
                {
                    tmpSheetCount = (int)logInfo.sheetCount2;
                    tmpPaperCount = (int)logInfo.papeCount2;
                }


                if (((int)PageID).Equals(UtilConst.CON_PAGE_A3) && Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
                {
                    SpendMoney = (Decimal)tmpPaperCount / 2 * paperPrice + (Decimal)tmpSheetCount / 2 * colPrice;
                }
                else
                {
                    SpendMoney = (Decimal)tmpPaperCount * paperPrice + (Decimal)tmpSheetCount * colPrice;
                }

                if (flg == 1)
                {
                    logInfo.spendMoney1 = SpendMoney;
                }
                else
                {
                    logInfo.spendMoney2 = SpendMoney;
                }
            }
            else
            {

                if (flg == 1)
                {
                    logInfo.PriceDetailID1 = 0;
                }
                else
                {
                    logInfo.PriceDetailID2 = 0;
                }



                paperPrice = 0;
                grayPrice = 0;
                colorPrice = 0;
                //耗材价格
                Decimal colPrice = 1;

                int tmpSheetCount = 0;
                int tmpPaperCount = 0;

                if (flg == 1)
                {
                    tmpSheetCount = (int)logInfo.sheetCount1;
                    tmpPaperCount = (int)logInfo.papeCount1;
                }
                else
                {
                    tmpSheetCount = (int)logInfo.sheetCount2;
                    tmpPaperCount = (int)logInfo.papeCount2;
                }


                if (((int)PageID).Equals(UtilConst.CON_PAGE_A3) && Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
                {
                    SpendMoney = (Decimal)tmpPaperCount / 2 * paperPrice + (Decimal)tmpSheetCount / 2 * colPrice;
                }
                else
                {
                    SpendMoney = (Decimal)tmpPaperCount * paperPrice + (Decimal)tmpSheetCount * colPrice;
                }

                if (flg == 1)
                {
                    logInfo.spendMoney1 = SpendMoney;
                }
                else
                {
                    logInfo.spendMoney2 = SpendMoney;
                }
            }
        }
        private void RecordLoginfoToDB(LogDataInfo logInfo, int flg)
        {

            string strsql = "INSERT INTO [LogInformation]	"
                + "           ([ID]             "
                + "           ,[Time]           "
                + "           ,[UserID]         "
                + "           ,[UserName]       "
                + "           ,[LoginName]      "
                + "           ,[GroupID]        "
                + "           ,[GroupName]      "
                + "           ,[Jobuid]         "
                + "           ,[SerialNumber]   "
                + "           ,[MFPName]        "
                + "           ,[MFPModel]       "
                + "           ,[MFPIPAddress]   "
                + "           ,[Duplex]         "
                + "           ,[JobID]          "
                + "           ,[FunctionID]     "
                + "           ,[FileName]       "
                + "           ,[PageID]         "
                + "           ,[Number]         "
                + "           ,[PapeCount]      "
                + "           ,[CopyCount]        "
                + "           ,[SpendMoney]     "
                + "           ,[PriceDetailID]  "
                + "           ,[Status]         "
                + "           ,[ErrorCode]     "
                + "           ,[MFPPrintTaskID]     "
                + "           ,[DspNumber]     "
                + "           ,[DspPapeCount])     "
                + "     VALUES   (   "
//                + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
                + "           {25}		    "
                + "           ,getdate()    "
//                + "           ,{26}		    "
                + "           ,{0}		    "
                + "           ,{14}         "
                + "           ,{15}         "
                + "           ,{16}         "
                + "           ,{17}         "
                + "           ,{1}          "
                + "           ,{2}          "
                + "           ,{3}		    "
                + "           ,{4}		    "
                + "           ,{5}		    "
                + "           ,{6}          "
                + "           ,{7}          "
                + "           ,{8}          "
                + "           ,{9}          "
                + "           ,{10}         "
                + "           ,{11}         "
                + "           ,{19}         "
                + "           ,{20}         "
                + "           ,{21}         "
                + "           ,{22}         "
                + "           ,{12}         "
                + "           ,{13}         "
                + "           ,{18}         "
                + "           ,{23}         "
                + "           ,{24})        ";


            string[] paramslist = new string[26];

            paramslist[25] = UtilCommon.ConvertIntToSQL(logInfo.ID.ToString());
            //paramslist[26] = UtilCommon.ConvertIntToSQL(logInfo.Time.ToString());

            // User ID
            paramslist[0] = UtilCommon.ConvertIntToSQL(logInfo.UserId.ToString());

            // UserName
            paramslist[14] = UtilCommon.ConvertStringToSQL(logInfo.UserName);
            // LoginName
            paramslist[15] = UtilCommon.ConvertStringToSQL(logInfo.LoginName);
            // GroupID
            paramslist[16] = UtilCommon.ConvertIntToSQL(logInfo.GroupId);
            // GroupName
            paramslist[17] = UtilCommon.ConvertStringToSQL(logInfo.GroupName);

            // Jobuid
            paramslist[1] = UtilCommon.ConvertStringToSQL(logInfo.Jobuid);
            // SerialNumber
            paramslist[2] = UtilCommon.ConvertStringToSQL(logInfo.Serialnumber);



            // MFPName
            paramslist[3] = UtilCommon.ConvertStringToSQL(logInfo.MFPName);

            // MFP Model Name
            paramslist[4] = UtilCommon.ConvertStringToSQL(logInfo.MFPModel);

            // MFP Ip Address
            paramslist[5] = UtilCommon.ConvertStringToSQL(logInfo.MFPIp);

            // Duplex
            paramslist[6] = UtilCommon.ConvertIntToSQL(logInfo.Duplex.ToString());

            // jobID
            paramslist[7] = UtilCommon.ConvertIntToSQL(logInfo.JOBId.ToString());



            paramslist[18] = UtilCommon.ConvertIntToSQL("0");


            // FunctionID
            if (flg == 1)
            {
                paramslist[8] = UtilCommon.ConvertIntToSQL(logInfo.FunctionId1.ToString());
            }
            else
            {
                paramslist[8] = UtilCommon.ConvertIntToSQL(logInfo.FunctionId2.ToString());
            }
            // FileName
            paramslist[9] = UtilCommon.ConvertStringToSQL(logInfo.FileName);
            // PageID
            paramslist[10] = UtilCommon.ConvertIntToSQL(logInfo.PageID);
            // 
            //dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
            //int Dsp_A3_A4 = settingrow.Dis_A3_A4;

           // int DspSheetCount = 0;
           // int DspPapeCount = 0;
            if (flg == 1)
            {
                //paramslist[11] = UtilCommon.ConvertIntToSQL(logInfo.sheetCount1.ToString());
                //paramslist[19] = UtilCommon.ConvertIntToSQL(logInfo.papeCount1.ToString());
                //paramslist[20] = UtilCommon.ConvertIntToSQL(logInfo.copyCount1.ToString());
                //paramslist[21] = UtilCommon.ConvertDecimalToSQL(logInfo.spendMoney1.ToString());
                //paramslist[22] = UtilCommon.ConvertIntToSQL(logInfo.PriceDetailID1.ToString());

                //DspSheetCount = (int)logInfo.sheetCount1;
                //DspPapeCount = (int)logInfo.papeCount1;


                //if (logInfo.PageID.Equals(UtilConst.CON_PAGE_A3.ToString())
                //    //&& logInfo.JOBId != UtilConst.ITEM_TITLE_Scan_JobId
                //    //&& logInfo.JOBId != UtilConst.ITEM_TITLE_ScanSave_JobId
                //    && logInfo.JOBId != UtilConst.ITEM_TITLE_Copy_JobId
                //    && logInfo.JOBId != UtilConst.ITEM_TITLE_Print_JobId
                //    && logInfo.JOBId != UtilConst.ITEM_TITLE_DFPrint_JobId
                //    )
                //{
                //    if (logInfo.Duplex == 1)
                //    {
                //        DspSheetCount = (int)logInfo.sheetCount1 / 2;
                //        DspPapeCount = (int)logInfo.sheetCount1 / 2;
                //        logInfo.papeCount1 = (uint)DspPapeCount * 2;
                //    }
                //    else
                //    {
                //        DspSheetCount = (int)logInfo.sheetCount1 / 2;
                //        DspPapeCount = (int)(logInfo.sheetCount1 / logInfo.copyCount1 + 2) / 4;
                //        DspPapeCount = (int)(DspPapeCount * logInfo.copyCount1);
                //        logInfo.papeCount1 = (uint)DspPapeCount * 2;
                //    }
                //}

                paramslist[11] = UtilCommon.ConvertIntToSQL(logInfo.sheetCount1.ToString());
                paramslist[19] = UtilCommon.ConvertIntToSQL(logInfo.papeCount1.ToString());
                paramslist[20] = UtilCommon.ConvertIntToSQL(logInfo.copyCount1.ToString());
                paramslist[21] = UtilCommon.ConvertDecimalToSQL(logInfo.spendMoney1.ToString());
                paramslist[22] = UtilCommon.ConvertIntToSQL(logInfo.PriceDetailID1.ToString());

                //paramslist[23] = UtilCommon.ConvertIntToSQL(DspSheetCount.ToString());
                //paramslist[24] = UtilCommon.ConvertIntToSQL(DspPapeCount.ToString());
                paramslist[23] = UtilCommon.ConvertIntToSQL(logInfo.DspSheetCount1.ToString());
                paramslist[24] = UtilCommon.ConvertIntToSQL(logInfo.DspPapeCount1.ToString());
            }
            else
            {
                //paramslist[11] = UtilCommon.ConvertIntToSQL(logInfo.sheetCount2.ToString());
                //paramslist[19] = UtilCommon.ConvertIntToSQL(logInfo.papeCount2.ToString());
                //paramslist[20] = UtilCommon.ConvertIntToSQL(logInfo.copyCount2.ToString());
                //paramslist[21] = UtilCommon.ConvertDecimalToSQL(logInfo.spendMoney2.ToString());
                //paramslist[22] = UtilCommon.ConvertIntToSQL(logInfo.PriceDetailID2.ToString());
                //paramslist[23] = UtilCommon.ConvertIntToSQL(logInfo.PriceDetailID2.ToString());


                //DspSheetCount = (int)logInfo.sheetCount2;
               // DspPapeCount = (int)logInfo.papeCount2;
                //if (logInfo.PageID.Equals(UtilConst.CON_PAGE_A3.ToString()))
                //{
                //    DspSheetCount = (int)logInfo.sheetCount2 / 2;
                //    if (logInfo.Duplex == 1)
                //    {
                //        DspPapeCount = (int)logInfo.sheetCount2 / 2;
                //        logInfo.papeCount2 = (uint)DspPapeCount * 2;
                //    }
                //    else
                //    {
                //        //DspPapeCount = (int)logInfo.sheetCount2 / 4;
                //        DspPapeCount = (int)(logInfo.sheetCount2 / logInfo.copyCount2 + 2) / 4;
                //        DspPapeCount = (int)(DspPapeCount * logInfo.copyCount2);
                //        logInfo.papeCount2 = (uint)DspPapeCount * 2;
                //    }
                //}

                paramslist[11] = UtilCommon.ConvertIntToSQL(logInfo.sheetCount2.ToString());
                paramslist[19] = UtilCommon.ConvertIntToSQL(logInfo.papeCount2.ToString());
                paramslist[20] = UtilCommon.ConvertIntToSQL(logInfo.copyCount2.ToString());
                paramslist[21] = UtilCommon.ConvertDecimalToSQL(logInfo.spendMoney2.ToString());
                paramslist[22] = UtilCommon.ConvertIntToSQL(logInfo.PriceDetailID2.ToString());
                paramslist[23] = UtilCommon.ConvertIntToSQL(logInfo.PriceDetailID2.ToString());

                paramslist[23] = UtilCommon.ConvertIntToSQL(logInfo.DspSheetCount2.ToString());
                paramslist[24] = UtilCommon.ConvertIntToSQL(logInfo.DspPapeCount2.ToString());
            }
            // Status
            paramslist[12] = UtilCommon.ConvertIntToSQL(logInfo.Status);
            // ErrorCode
            paramslist[13] = UtilCommon.ConvertStringToSQL(logInfo.ErrorCode);




            strsql = string.Format(strsql, paramslist);

            // Ex SQL.
            using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
            {
                con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(strsql, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    //tran.Commit();
                }
                catch (Exception ex)
                {
                    //if (tran.Connection != null)
                    //{
                    //    tran.Rollback();
                    //}
                    throw ex;
                }
                finally
                {
                    //tran.Dispose();
                    //tran = null;
                    ;
                }
            }


        }
        //20140619 chen add end

        #region " Record the log for the Start Log."
        /// <summary>
        /// Record the log for the Start Log.
        /// </summary>
        /// <param name="strUserId">User id</param>
        /// <param name="deviceinfo">DEVICE_INFO_TYPE</param>
        /// <param name="jobid">OSA_JOB_ID_TYPE</param>
        /// <Date>2010.11.17</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>1.10</Version>
        private void RecordStartLog(int intUserId, DEVICE_INFO_TYPE deviceinfo, JOB_MODE_TYPE jobmode, OSA_JOB_ID_TYPE jobid)
        {

            // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ST
            string serialnumber = deviceinfo.serialnumber;
            if (string.IsNullOrEmpty(serialnumber))
            {
                serialnumber = DeviceSession.GetSerialnumberByDevid(deviceinfo.uuid);
            }
            // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ED

            //// 2010.12.21 Update By SES Jijianxiong ST
            //// Check the states is exist.
            //dtLogInformationTableAdapters.LogInformationTableAdapter adapter = new dtLogInformationTableAdapters.LogInformationTableAdapter();
            //int count = Convert.ToInt32(adapter.ScalarQuery(intUserId, serialnumber, jobid.uid, 0));

            //if (count > 0)
            //{
            //    // the same record data for the start information is exist in the DB.
            //    // no need to record it again.
            //    // 2010.12.20 Add By SES Jijianxiong ST
            //    // While the Job was limit by the "OSA_LIMITS_REACHED"
            //    // the next job's uuid is equal to the Limited job.
                

            //    // 2010.12.20 Add By SES Jijianxiong ED
            //    return;
            //}
            // Get the last date from the DB.
            dtLogInformationTableAdapters.LogInformationTableAdapter adapter = new dtLogInformationTableAdapters.LogInformationTableAdapter();
            dtLogInformation.LogInformationDataTable logtable = adapter.GetLatestDate(intUserId, serialnumber, jobid.uid);
            if (logtable.Count > 0)
            {
                dtLogInformation.LogInformationRow logrow = logtable[0];
                if ( logrow.Status == Convert.ToInt32( UtilConst.JOB_STATUS_CREATE) )
                {
                    // the last date is the new start job.
                    return;
                }
            }

            //// 2010.12.21 Update By SES Jijianxiong ED


            // JobId
            if (jobmode != null)
            {
                string jobModeName = MapJobModeToLimits(jobmode);
                // The Color Mode is no used in this function.
                mfpjob = GetMFPJob(jobModeName, "MONOCHROME");
            }
            else
            {
                // The Color Mode is no used in this function.
                mfpjob = GetMFPJobId(jobid.jobtype);
            }

            // 2010.12.08 Update By SES Jijianxiong Ver.1.1 Update ST
            // Base the Specification_SimpleEA(V1.27)_20101203.doc

            // MFP ModelName
            string strmodelname = deviceinfo.modelname;
            // MFP Ip
            string strmfpIp = deviceinfo.network_address;

            // 2011.01.10 Update By SES Jijianixong ST
            ////string strsql = "INSERT INTO [LogInformation]	"
            ////    + "           ([ID]         "
            ////    + "           ,[UserID]		"
            ////    + "           ,[SerialNumber]"
            ////    + "           ,[Jobuid]		"
            ////    + "           ,[JobID]		"
            ////    + "           ,[FunctionID]	"
            ////    + "           ,[PageID]		"
            ////    + "           ,[Number]		"
            ////    + "           ,[Time]		"
            ////    + "           ,[MFPName]	"
            ////    + "           ,[PCName]		"
            ////    + "           ,[FileName]	"
            ////    + "           ,[Status]		"
            ////    + "           ,[ErrorCode])	"
            ////    + "     VALUES			    "
            ////    + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
            ////    + "           ,{0}		    "
            ////    + "           ,{1}          "
            ////    + "           ,{2}          "
            ////    + "           ,{3}		    "
            ////    + "           ,Null		    "
            ////    + "           ,Null		    "
            ////    + "           ,Null		    "
            ////    + "           ,getdate()    "
            ////    + "           ,{4}          "
            ////    + "           ,Null		    "
            ////    + "           ,Null		    "
            ////    + "           ,{5}		    "
            ////    + "           ,Null)		";

            ////string[] paramslist = new string[6];
            ////// User ID
            ////paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());
            ////// SerialNumber
            ////paramslist[1] = UtilCommon.ConvertStringToSQL(serialnumber);
            ////// Jobuid
            ////paramslist[2] = UtilCommon.ConvertStringToSQL(jobid.uid);
            ////// Job ID
            ////paramslist[3] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
            ////// MFPName
            ////if (deviceinfo.name == null)
            ////{
            ////    paramslist[4] = UtilCommon.ConvertStringToSQL("");
            ////}
            ////else
            ////{
            ////    paramslist[4] = UtilCommon.ConvertStringToSQL(deviceinfo.name.Trim());
            ////}
            ////// Status
            ////paramslist[5] = UtilCommon.ConvertIntToSQL(UtilConst.JOB_STATUS_CREATE);

            //string strsql = "INSERT INTO [LogInformation]	"
            //    + "           ([ID]             "
            //    + "           ,[Time]           "
            //    + "           ,[UserID]         "
            //    + "           ,[Jobuid]         "
            //    + "           ,[SerialNumber]   "
            //    + "           ,[MFPName]        "
            //    + "           ,[MFPModel]       "
            //    + "           ,[MFPIPAddress]   "
            //    + "           ,[Duplex]         "
            //    + "           ,[JobID]          "
            //    + "           ,[FunctionID]     "
            //    + "           ,[FileName]       "
            //    + "           ,[PageID]         "
            //    + "           ,[Number]         "
            //    // + "           ,[PCName]         " // 2010.12.14 Delete By SES Jijianxong
            //    + "           ,[Status]         "
            //    + "           ,[ErrorCode])     "

            //    + "     VALUES			    "
            //    + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
            //    + "           ,getdate()    "
            //    + "           ,{0}		    "
            //    + "           ,{1}          "
            //    + "           ,{2}          "
            //    + "           ,{3}		    "
            //    + "           ,{4}		    "
            //    + "           ,{5}		    "
            //    + "           ,Null		    "
            //    + "           ,{6}          "
            //    + "           ,Null		    "
            //    + "           ,Null		    "
            //    + "           ,Null		    "
            //    + "           ,Null		    "
            //    // + "           ,Null		    "  // 2010.12.14 Delete By SES Jijianxong
            //    + "           ,{7}		    "
            //    + "           ,Null)		";
            //string[] paramslist = new string[8];
            //// User ID
            //paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());
            //// Jobuid
            //paramslist[1] = UtilCommon.ConvertStringToSQL(jobid.uid);
            //// SerialNumber
            //paramslist[2] = UtilCommon.ConvertStringToSQL(serialnumber);


            //// 2010.12.21 Update By SES Jijianxiong ST
            //// Some times the deviceinfo.modelname is null.
            //// Some times: MX-2700N

            ////// MFPName
            ////if (deviceinfo.name == null)
            ////{
            ////    paramslist[3] = UtilCommon.ConvertStringToSQL("");
            ////}
            ////else
            ////{
            ////    paramslist[3] = UtilCommon.ConvertStringToSQL(deviceinfo.name.Trim());
            ////}
            ////// MFP Model Name
            ////paramslist[4] = UtilCommon.ConvertStringToSQL(strmodelname);
            ////// MFP Ip Address
            ////paramslist[5] = UtilCommon.ConvertStringToSQL(strmfpIp);

            //string strMFPName = "";
            //strMFPName = deviceinfo.name;
            //string struuid = deviceinfo.uuid;
            //DEVICE_INFO_TYPE device_info = Helper.DeviceSession.Get(struuid).deviceinfo;
            //if (string.IsNullOrEmpty(strmodelname))
            //{
            //    strmodelname = device_info.modelname;
            //}

            //if (string.IsNullOrEmpty(strmfpIp))
            //{
            //    strmfpIp = device_info.network_address;
            //}

            //if (string.IsNullOrEmpty(strMFPName))
            //{
            //    strMFPName = device_info.name;
            //}

            //// MFPName
            //paramslist[3] = UtilCommon.ConvertStringToSQL(strMFPName);

            //paramslist[4] = UtilCommon.ConvertStringToSQL(strmodelname);

            //// MFP Ip Address
            //paramslist[5] = UtilCommon.ConvertStringToSQL(strmfpIp);
            //// 2010.12.21 Update By SES Jijianxiong ST

            //// Job ID
            //paramslist[6] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
            //// Status
            //paramslist[7] = UtilCommon.ConvertIntToSQL(UtilConst.JOB_STATUS_CREATE);

            //// 2010.12.08 Update By SES Jijianxiong Ver.1.1 Update ED
            //string strsql = "INSERT INTO [LogInformation]	"
            //    + "           ([ID]         "
            //    + "           ,[UserID]		"
            //    + "           ,[SerialNumber]"
            //    + "           ,[Jobuid]		"
            //    + "           ,[JobID]		"
            //    + "           ,[FunctionID]	"
            //    + "           ,[PageID]		"
            //    + "           ,[Number]		"
            //    + "           ,[Time]		"
            //    + "           ,[MFPName]	"
            //    + "           ,[PCName]		"
            //    + "           ,[FileName]	"
            //    + "           ,[Status]		"
            //    + "           ,[ErrorCode])	"
            //    + "     VALUES			    "
            //    + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
            //    + "           ,{0}		    "
            //    + "           ,{1}          "
            //    + "           ,{2}          "
            //    + "           ,{3}		    "
            //    + "           ,Null		    "
            //    + "           ,Null		    "
            //    + "           ,Null		    "
            //    + "           ,getdate()    "
            //    + "           ,{4}          "
            //    + "           ,Null		    "
            //    + "           ,Null		    "
            //    + "           ,{5}		    "
            //    + "           ,Null)		";

            //string[] paramslist = new string[6];
            //// User ID
            //paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());
            //// SerialNumber
            //paramslist[1] = UtilCommon.ConvertStringToSQL(serialnumber);
            //// Jobuid
            //paramslist[2] = UtilCommon.ConvertStringToSQL(jobid.uid);
            //// Job ID
            //paramslist[3] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
            //// MFPName
            //if (deviceinfo.name == null)
            //{
            //    paramslist[4] = UtilCommon.ConvertStringToSQL("");
            //}
            //else
            //{
            //    paramslist[4] = UtilCommon.ConvertStringToSQL(deviceinfo.name.Trim());
            //}
            //// Status
            //paramslist[5] = UtilCommon.ConvertIntToSQL(UtilConst.JOB_STATUS_CREATE);

            // Get User Infor
            LogUserInfor userinfor = new LogUserInfor(intUserId);

            string strsql = "INSERT INTO [LogInformation]	"
                + "           ([ID]             "
                + "           ,[Time]           "
                + "           ,[UserID]         "

                + "           ,[UserName]       "
                + "           ,[LoginName]      "
                + "           ,[GroupID]        "
                + "           ,[GroupName]      "

                + "           ,[Jobuid]         "
                + "           ,[SerialNumber]   "
                + "           ,[MFPName]        "
                + "           ,[MFPModel]       "
                + "           ,[MFPIPAddress]   "
                + "           ,[Duplex]         "
                + "           ,[JobID]          "
                + "           ,[FunctionID]     "
                + "           ,[FileName]       "
                + "           ,[PageID]         "
                + "           ,[Number]         "
                //chen add 20140424 for money start
                + "           ,[PapeCount]      "
                + "           ,[CopyCount]      "
                + "           ,[SpendMoney]     "
                + "           ,[PriceDetailID]  "
                //chen add 20140424 for money end
                + "           ,[Status]         "
                + "           ,[ErrorCode]     "
                + "           ,[MFPPrintTaskID]     "
                + "           ,[DspNumber]     "
                + "           ,[DspPapeCount])     "

                + "     VALUES			    "
                + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
                + "           ,getdate()    "
                + "           ,{0}		    "

                + "           ,{8}         "
                + "           ,{9}         "
                + "           ,{10}         "
                + "           ,{11}         "

                + "           ,{1}          "
                + "           ,{2}          "
                + "           ,{3}		    "
                + "           ,{4}		    "
                + "           ,{5}		    "
                + "           ,Null		    "
                + "           ,{6}          "
                + "           ,Null		    "
                + "           ,Null		    "
                + "           ,Null		    "
                + "           ,Null		    "
                //chen add 20140424 for money start
                + "           ,Null		    "
                + "           ,Null		    "
                + "           ,Null		    "
                + "           ,Null		    "
                //chen add 20140424 for money end

                + "           ,{7}		    "
                + "           ,Null		    "
                + "           ,Null		    "
                + "           ,Null		    "
                + "           ,Null)		";
            string[] paramslist = new string[12];
            // User ID
            paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());

            paramslist[8] = UtilCommon.ConvertStringToSQL(userinfor.UserName);
            // LoginName
            paramslist[9] = UtilCommon.ConvertStringToSQL(userinfor.LoginName);
            // GroupID
            paramslist[10] = UtilCommon.ConvertIntToSQL(userinfor.GroupId.ToString());
            // GroupName
            paramslist[11] = UtilCommon.ConvertStringToSQL(userinfor.GroupName);


            // Jobuid
            paramslist[1] = UtilCommon.ConvertStringToSQL(jobid.uid);
            // SerialNumber
            paramslist[2] = UtilCommon.ConvertStringToSQL(serialnumber);

            string strMFPName = "";
            strMFPName = deviceinfo.name;
            string struuid = deviceinfo.uuid;
            DEVICE_INFO_TYPE device_info = Helper.DeviceSession.Get(struuid).deviceinfo;
            if (string.IsNullOrEmpty(strmodelname))
            {
                strmodelname = device_info.modelname;
            }

            if (string.IsNullOrEmpty(strmfpIp))
            {
                strmfpIp = device_info.network_address;
            }

            if (string.IsNullOrEmpty(strMFPName))
            {
                strMFPName = device_info.name;
            }

            // MFPName
            paramslist[3] = UtilCommon.ConvertStringToSQL(strMFPName);

            paramslist[4] = UtilCommon.ConvertStringToSQL(strmodelname);

            // MFP Ip Address
            paramslist[5] = UtilCommon.ConvertStringToSQL(strmfpIp);

            // Job ID
            paramslist[6] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
            // Status
            paramslist[7] = UtilCommon.ConvertIntToSQL(UtilConst.JOB_STATUS_CREATE);

            // 2011.01.10 Update By SES Jijianxiong ED


            strsql = string.Format(strsql, paramslist);

            // Ex SQL.
            using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
            {
                con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(strsql, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    //tran.Commit();
                }
                catch (Exception ex)
                {
                    //if (tran.Connection != null)
                    //{
                    //    tran.Rollback();
                    //}
                    throw ex;
                }
                finally
                {
                    //tran.Dispose();
                    //tran = null;
                    ;
                }
            }




        }
        #endregion

        #region "Get Page Type."
        /// <summary>
        /// GetPageType
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <Date>2010.07.27</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private string GetPageType(JOB_RESULTS_BASE_TYPE result)
        {

            RESOURCE_PAPER_TYPE[] paperout;
            if (result is JOB_RESULTS_COPY_TYPE)
            {
                paperout = ((JOB_RESULTS_COPY_TYPE)result).resources.paperout;
            }
            else if (result is JOB_RESULTS_PRINT_TYPE)
            {
                paperout = ((JOB_RESULTS_PRINT_TYPE)result).resources.paperout;
            }
            else if (result is JOB_RESULTS_SCAN_TYPE)
            {
                paperout = ((JOB_RESULTS_SCAN_TYPE)result).resources.paperout;
            }
            else if (result is JOB_RESULTS_DOCFILING_TYPE)
            {
                paperout = ((JOB_RESULTS_DOCFILING_TYPE)result).resources.paperout;
            }
            else
            {
                //foreach (PROPERTY_SET_TYPE property in result.properties)
                //{
                //    if (property.sysname.Equals("original-size"))
                //    {
                //        return property.Value;
                //    }
                //}

                //  unused pager.
                return "";
            }
            //20141027 chren update start
            
            //foreach (RESOURCE_PAPER_TYPE papertype in paperout)
            //{
            //    if (papertype.property == null)
            //    {
            //        continue;
            //    }
            //    foreach (PROPERTY_SET_TYPE property in papertype.property)
            //    {
            //        if (property.sysname.Equals("papersize"))
            //        {
            //            return property.Value;
            //        }
            //    }
            //}
            string papersize = "";
            Boolean flg = false;
            foreach (RESOURCE_PAPER_TYPE papertype in paperout)
            {
                if (papertype.property == null)
                {
                    continue;
                }
                foreach (PROPERTY_SET_TYPE property in papertype.property)
                {
                    if (property.sysname.Equals("papersize"))
                    {
                        papersize =  property.Value;
                        break;
                    }
                }
                if (!papersize.Equals(""))
                {
                    break;
                }
            }
            foreach (RESOURCE_PAPER_TYPE papertype in paperout)
            {
                if (papertype.property == null)
                {
                    continue;
                }
                foreach (PROPERTY_SET_TYPE property in papertype.property)
                {
                    if (property.sysname.Equals("papersize"))
                    {
                        if ( !papersize.Equals(property.Value) )
                        {
                            flg = true;
                            break;
                        }
                    }
                }
                if (flg)
                {
                    break;
                }
            }
            if (flg)
            {
                return "A4";
            }
            return papersize;
            //end

            //return "";
        }
        #endregion

        #region "Get Color Type."
        /// <summary>
        /// Get Color Type.
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        /// <Date>2010.07.27</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private string GetColorType(string propName, string propValue)
        {
            if (propName.Equals("color-mode"))
            {

                return propValue;
            }
            else
            {
                return "";
            }

        }
        #endregion

        #region " Record the log for the Simple EA Cancel Mode Log."
        /// <summary>
        /// Record the log for the Simple EA Cancel Mode Log.
        /// </summary>
        /// <param name="intUserId"></param>
        /// <param name="eventdata"></param>
        /// <Date>2010.11.17</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>1.10</Version>
        public void RecordUserSetLog(string strUserId, DEVICE_INFO_TYPE deviceinfo, OSA_JOB_ID_TYPE mfpjobid)
        {
            int intUserId;
            try
            {
                intUserId = Convert.ToInt32(strUserId);
            }
            catch
            {

                intUserId = -1;
            }

            string SerialNumber = string.Empty;
            string Jobuid;
            string Status;
            E_EA_MESSAGE_TYPE ErrorCode = E_EA_MESSAGE_TYPE.SIMPLEEA_OTHER;

            // Get the uid for this job.
            Jobuid = mfpjobid.uid;
            // serialnumber
            SerialNumber = deviceinfo.serialnumber;
            if (string.IsNullOrEmpty(SerialNumber))
            {
                SerialNumber = DeviceSession.GetSerialnumberByDevid(deviceinfo.uuid);
            }

            Status = UtilConst.JOB_STATUS_ERROR;

            // Get JobId 
            mfpjob = GetMFPJobId(mfpjobid.jobtype);

            // 2011.01.10 Update By SES Jijianxiong ST
            //// 2010.12.09 Update By SES Jijianxiong ST
            //// Base the Specification_SimpleEA(V1.27)_20101203.doc

            ////string strsql = "INSERT INTO [LogInformation]	"
            ////    + "           ([ID]         "
            ////    + "           ,[UserID]		"
            ////    + "           ,[SerialNumber]"
            ////    + "           ,[Jobuid]		"
            ////    + "           ,[JobID]		"
            ////    + "           ,[FunctionID]	"
            ////    + "           ,[PageID]		"
            ////    + "           ,[Number]		"
            ////    + "           ,[Time]		"
            ////    + "           ,[MFPName]	"
            ////    + "           ,[PCName]		"
            ////    + "           ,[FileName]	"
            ////    + "           ,[Status]		"
            ////    + "           ,[ErrorCode])	"
            ////    + "     VALUES			    "
            ////    + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
            ////    + "           ,{0}		    "
            ////    + "           ,{1}          "
            ////    + "           ,{2}          "
            ////    + "           ,{3}		    "
            ////    + "           ,{4}		    "
            ////    + "           ,{5}		    "
            ////    + "           ,{6}		    "
            ////    + "           ,getdate()    "
            ////    + "           ,{7}          "
            ////    + "           ,{8}		    "
            ////    + "           ,{9}		    "
            ////    + "           ,{10}		    "
            ////    + "           ,{11})		";

            ////string[] paramslist = new string[12];
            ////// User ID
            ////paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());
            ////// SerialNumber
            ////paramslist[1] = UtilCommon.ConvertStringToSQL(SerialNumber);
            ////// Jobuid
            ////paramslist[2] = UtilCommon.ConvertStringToSQL(Jobuid);
            ////// jobID
            ////paramslist[3] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
            ////// FunctionID
            ////paramslist[4] = UtilCommon.ConvertIntToSQL(mfpjob.FunctionId.ToString());
            ////// PageID
            ////paramslist[5] = UtilCommon.ConvertIntToSQL(PageID.ToString());
            ////// Number
            ////paramslist[6] = UtilCommon.ConvertIntToSQL(Number.ToString());
            ////// MFPName
            ////paramslist[7] = UtilCommon.ConvertStringToSQL(deviceinfo.name);
            ////// PCName
            ////paramslist[8] = UtilCommon.ConvertStringToSQL(PCName);
            ////// FileName
            ////paramslist[9] = UtilCommon.ConvertStringToSQL(FileName);
            ////// Status
            ////paramslist[10] = UtilCommon.ConvertIntToSQL(Status);
            ////// ErrorCode
            ////paramslist[11] = UtilCommon.ConvertStringToSQL(ErrorCode.ToString());

            //string strsql = "INSERT INTO [LogInformation]	"
            //    + "           ([ID]             "
            //    + "           ,[Time]           "
            //    + "           ,[UserID]         "
            //    + "           ,[Jobuid]         "
            //    + "           ,[SerialNumber]   "
            //    + "           ,[MFPName]        "
            //    + "           ,[MFPModel]       "
            //    + "           ,[MFPIPAddress]   "
            //    + "           ,[Duplex]         "
            //    + "           ,[JobID]          "
            //    + "           ,[FunctionID]     "
            //    + "           ,[FileName]       "
            //    + "           ,[PageID]         "
            //    + "           ,[Number]         "
            //    // + "           ,[PCName]         " // 2010.12.14 Delete By SES Jijianxong
            //    + "           ,[Status]         "
            //    + "           ,[ErrorCode])     "

            //    + "     VALUES			    "
            //    + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
            //    + "           ,getdate()    "
            //    + "           ,{0}		    "
            //    + "           ,{1}          "
            //    + "           ,{2}          "
            //    + "           ,{3}		    "
            //    + "           ,{4}		    "
            //    + "           ,{5}		    "
            //    + "           ,{6}          "
            //    + "           ,{7}          "
            //    + "           ,{8}          "
            //    + "           ,{9}          "
            //    + "           ,{10}         "
            //    + "           ,{11}         "
            //    // + "           ,{12}         " // 2010.12.14 Delete By SES Jijianxong
            //    + "           ,{12}         "
            //    + "           ,{13})        ";

            //string[] paramslist = new string[14];
            //// User ID
            //paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());
            //// Jobuid
            //paramslist[1] = UtilCommon.ConvertStringToSQL(Jobuid);
            //// SerialNumber
            //paramslist[2] = UtilCommon.ConvertStringToSQL(SerialNumber);
            //// MFP Model Name
            //// 2010.12.20 Update By SES Jijianxiong ST
            //// Some times the deviceinfo.modelname is null.
            //// Some times: MX-2700N
            //string strModelname = "";
            //string strMFPIp = "";
            //string strMFPName = "";
            //strModelname = deviceinfo.modelname;
            //strMFPIp = deviceinfo.network_address;
            //strMFPName = deviceinfo.name;

            //DEVICE_INFO_TYPE device_info = Helper.DeviceSession.Get(deviceinfo.uuid).deviceinfo;
            //if (string.IsNullOrEmpty(strModelname))
            //{
            //    strModelname = device_info.modelname;
            //}

            //if (string.IsNullOrEmpty(strMFPIp))
            //{
            //    strMFPIp = device_info.network_address;
            //}

            //if (string.IsNullOrEmpty(strMFPName))
            //{
            //    strMFPName = device_info.name;
            //}

            //// MFPName
            //paramslist[3] = UtilCommon.ConvertStringToSQL(strMFPName);

            //paramslist[4] = UtilCommon.ConvertStringToSQL(strModelname);

            //// MFP Ip Address
            //paramslist[5] = UtilCommon.ConvertStringToSQL(strMFPIp);
            //// 2010.12.20 Update By SES Jijianxiong ST

            //// Duplex
            //paramslist[6] = UtilCommon.ConvertIntToSQL("");
            //// jobID
            //paramslist[7] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
            //// FunctionID
            //paramslist[8] = UtilCommon.ConvertIntToSQL(mfpjob.FunctionId.ToString());
            //// FileName
            //paramslist[9] = UtilCommon.ConvertStringToSQL(FileName);
            //// PageID
            //paramslist[10] = UtilCommon.ConvertIntToSQL(PageID.ToString());
            //// Number
            //paramslist[11] = UtilCommon.ConvertIntToSQL(Number.ToString());

            //// 2010.12.14 Update By SES Jijianxong ST
            //// PCName Delete
            ////// PCName
            ////paramslist[12] = UtilCommon.ConvertStringToSQL(PCName);
            ////// Status
            ////paramslist[13] = UtilCommon.ConvertIntToSQL(Status);
            ////// ErrorCode
            ////paramslist[14] = UtilCommon.ConvertStringToSQL(ErrorCode.ToString());
            //// Status
            //paramslist[12] = UtilCommon.ConvertIntToSQL(Status);
            //// ErrorCode
            //paramslist[13] = UtilCommon.ConvertStringToSQL(ErrorCode.ToString());
            //// 2010.12.14 Update By SES Jijianxong ED

            //// 2010.12.09 Update By SES Jijianxiong ED
            // 2010.12.09 Update By SES Jijianxiong ST
            // Base the Specification_SimpleEA(V1.27)_20101203.doc

            //string strsql = "INSERT INTO [LogInformation]	"
            //    + "           ([ID]         "
            //    + "           ,[UserID]		"
            //    + "           ,[SerialNumber]"
            //    + "           ,[Jobuid]		"
            //    + "           ,[JobID]		"
            //    + "           ,[FunctionID]	"
            //    + "           ,[PageID]		"
            //    + "           ,[Number]		"
            //    + "           ,[Time]		"
            //    + "           ,[MFPName]	"
            //    + "           ,[PCName]		"
            //    + "           ,[FileName]	"
            //    + "           ,[Status]		"
            //    + "           ,[ErrorCode])	"
            //    + "     VALUES			    "
            //    + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
            //    + "           ,{0}		    "
            //    + "           ,{1}          "
            //    + "           ,{2}          "
            //    + "           ,{3}		    "
            //    + "           ,{4}		    "
            //    + "           ,{5}		    "
            //    + "           ,{6}		    "
            //    + "           ,getdate()    "
            //    + "           ,{7}          "
            //    + "           ,{8}		    "
            //    + "           ,{9}		    "
            //    + "           ,{10}		    "
            //    + "           ,{11})		";

            //string[] paramslist = new string[12];
            //// User ID
            //paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());
            //// SerialNumber
            //paramslist[1] = UtilCommon.ConvertStringToSQL(SerialNumber);
            //// Jobuid
            //paramslist[2] = UtilCommon.ConvertStringToSQL(Jobuid);
            //// jobID
            //paramslist[3] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
            //// FunctionID
            //paramslist[4] = UtilCommon.ConvertIntToSQL(mfpjob.FunctionId.ToString());
            //// PageID
            //paramslist[5] = UtilCommon.ConvertIntToSQL(PageID.ToString());
            //// Number
            //paramslist[6] = UtilCommon.ConvertIntToSQL(Number.ToString());
            //// MFPName
            //paramslist[7] = UtilCommon.ConvertStringToSQL(deviceinfo.name);
            //// PCName
            //paramslist[8] = UtilCommon.ConvertStringToSQL(PCName);
            //// FileName
            //paramslist[9] = UtilCommon.ConvertStringToSQL(FileName);
            //// Status
            //paramslist[10] = UtilCommon.ConvertIntToSQL(Status);
            //// ErrorCode
            //paramslist[11] = UtilCommon.ConvertStringToSQL(ErrorCode.ToString());

            // Get User Infor
            LogUserInfor userinfor = new LogUserInfor(intUserId);

            string strsql = "INSERT INTO [LogInformation]	"
                + "           ([ID]             "
                + "           ,[Time]           "
                + "           ,[UserID]         "

                + "           ,[UserName]       "
                + "           ,[LoginName]      "
                + "           ,[GroupID]        "
                + "           ,[GroupName]      "

                + "           ,[Jobuid]         "
                + "           ,[SerialNumber]   "
                + "           ,[MFPName]        "
                + "           ,[MFPModel]       "
                + "           ,[MFPIPAddress]   "
                + "           ,[Duplex]         "
                + "           ,[JobID]          "
                + "           ,[FunctionID]     "
                + "           ,[FileName]       "
                + "           ,[PageID]         "
                + "           ,[Number]         "
                //chen add 20140424 for money start
                + "           ,[PapeCount]      "
                + "           ,[CopyCount]      "
                + "           ,[SpendMoney]     "
                + "           ,[PriceDetailID]  "
                //chen add 20140424 for money end
                + "           ,[Status]         "
                + "           ,[ErrorCode])     "

                + "     VALUES			    "
                + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
                + "           ,getdate()    "
                + "           ,{0}		    "

                + "           ,{14}         "
                + "           ,{15}         "
                + "           ,{16}         "
                + "           ,{17}         "

                + "           ,{1}          "
                + "           ,{2}          "
                + "           ,{3}		    "
                + "           ,{4}		    "
                + "           ,{5}		    "
                + "           ,{6}          "
                + "           ,{7}          "
                + "           ,{8}          "
                + "           ,{9}          "
                + "           ,{10}         "
                + "           ,{11}         "
                //chen add 20140424 for money start
                + "           ,[18]        "
                + "           ,[19]        "
                + "           ,[20]        "
                + "           ,[21]        "
                //chen add 20140424 for money end

                + "           ,{12}         "
                + "           ,{13})        ";

            //chen add 20140424 for money start
            //string[] paramslist = new string[18];
            string[] paramslist = new string[22];
            //chen add 20140424 for money end
            // User ID
            paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());

            // UserName
            paramslist[14] = UtilCommon.ConvertStringToSQL(userinfor.UserName);
            // LoginName
            paramslist[15] = UtilCommon.ConvertStringToSQL(userinfor.LoginName);
            // GroupID
            paramslist[16] = UtilCommon.ConvertIntToSQL(userinfor.GroupId.ToString());
            // GroupName
            paramslist[17] = UtilCommon.ConvertStringToSQL(userinfor.GroupName);

            // Jobuid
            paramslist[1] = UtilCommon.ConvertStringToSQL(Jobuid);
            // SerialNumber
            paramslist[2] = UtilCommon.ConvertStringToSQL(SerialNumber);
            // MFP Model Name
            string strModelname = "";
            string strMFPIp = "";
            string strMFPName = "";
            strModelname = deviceinfo.modelname;
            strMFPIp = deviceinfo.network_address;
            strMFPName = deviceinfo.name;

            DEVICE_INFO_TYPE device_info = Helper.DeviceSession.Get(deviceinfo.uuid).deviceinfo;
            if (string.IsNullOrEmpty(strModelname))
            {
                strModelname = device_info.modelname;
            }

            if (string.IsNullOrEmpty(strMFPIp))
            {
                strMFPIp = device_info.network_address;
            }

            if (string.IsNullOrEmpty(strMFPName))
            {
                strMFPName = device_info.name;
            }

            // MFPName
            paramslist[3] = UtilCommon.ConvertStringToSQL(strMFPName);

            paramslist[4] = UtilCommon.ConvertStringToSQL(strModelname);

            // MFP Ip Address
            paramslist[5] = UtilCommon.ConvertStringToSQL(strMFPIp);
            // 2010.12.20 Update By SES Jijianxiong ST

            // Duplex
            paramslist[6] = UtilCommon.ConvertIntToSQL("");
            // jobID
            paramslist[7] = UtilCommon.ConvertIntToSQL(mfpjob.JOBId.ToString());
            // FunctionID
            paramslist[8] = UtilCommon.ConvertIntToSQL(mfpjob.FunctionId.ToString());
            // FileName
            paramslist[9] = UtilCommon.ConvertStringToSQL(FileName);
            // PageID
            paramslist[10] = UtilCommon.ConvertIntToSQL(PageID.ToString());
            // Number
            paramslist[11] = UtilCommon.ConvertIntToSQL(Number.ToString());

            // Status
            paramslist[12] = UtilCommon.ConvertIntToSQL(Status);
            // ErrorCode
            paramslist[13] = UtilCommon.ConvertStringToSQL(ErrorCode.ToString());

            // 2011.01.10 Update By SES Jijianxiong ED

            //chen add 20140424 for money start
            paramslist[18] = UtilCommon.ConvertStringToSQL(papeCount.ToString());
            paramslist[19] = UtilCommon.ConvertStringToSQL(copyCount.ToString());
            //get price detail id 
            int PriceDetailID = 0;
            paramslist[20] = UtilCommon.ConvertStringToSQL(PriceDetailID.ToString());

            //根据价格ID获得价格
            int spendMoney = 0;
            paramslist[21] = UtilCommon.ConvertStringToSQL(spendMoney.ToString());
            //chen add 20140424 for money start


            strsql = string.Format(strsql, paramslist);

            // Ex SQL.
            using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(strsql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }

        }
        #endregion

        #region "Get Duplex"
        /// <summary>
        /// Get Duplex
        /// </summary>
        /// <param name="strname"></param>
        /// <returns></returns>
        /// <Date>2010.12.08</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>1.10</Version>
        private E_EA_DUPLEX_TYPE GetDuplex(string strname)
        {
            switch (strname.ToUpper())
            {
                case "1SIDED":
                    return E_EA_DUPLEX_TYPE.SIDED1;
                case "2SIDED":
                    return E_EA_DUPLEX_TYPE.SIDED2 ;
                case "2SIDED_BOOKLET":
                    return E_EA_DUPLEX_TYPE.SIDED_BOOKLET2;
                case "2SIDED_TABLET":
                    return E_EA_DUPLEX_TYPE.SIDED_TABLET2;
                case "PAMPHLET":
                    return E_EA_DUPLEX_TYPE.PAMPHLET;
                default:
                    return E_EA_DUPLEX_TYPE.Unknown;
            }
        }
        #endregion

        #region " Record the log for the Error Log."
        /// <summary>
        /// Record the log for the Error Log.
        /// </summary>
        /// <param name="intUserId"></param>
        /// <param name="deviceinfo"></param>
        /// <Date>2010.12.08</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>1.10</Version>
        public static void RecordErrLog(string accountid, DEVICE_INFO_TYPE deviceinfo, E_EA_MESSAGE_TYPE ErrorCode)
        {
            int intUserId = -1;
            try
            {
                intUserId = Convert.ToInt32(accountid);
            }
            catch 
            {

                intUserId = -1;
            }
            
            // 2011.01.10 Update By SES Jijianxiong ST
            //string strsql = "INSERT INTO [LogInformation]	"
            //    + "           ([ID]             "
            //    + "           ,[Time]           "
            //    + "           ,[UserID]         "
            //    + "           ,[Jobuid]         "
            //    + "           ,[SerialNumber]   "
            //    + "           ,[MFPName]        "
            //    + "           ,[MFPModel]       "
            //    + "           ,[MFPIPAddress]   "
            //    + "           ,[Duplex]         "
            //    + "           ,[JobID]          "
            //    + "           ,[FunctionID]     "
            //    + "           ,[FileName]       "
            //    + "           ,[PageID]         "
            //    + "           ,[Number]         "
            //    // + "           ,[PCName]         " // 2010.12.14 Delete By SES Jijianxiong
            //    + "           ,[Status]         "
            //    + "           ,[ErrorCode])     "
            //    + "     VALUES			    "
            //    + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
            //    + "           ,getdate()    "
            //    + "           ,{0}		    "
            //    + "           ,{1}          "
            //    + "           ,{2}          "
            //    + "           ,{3}		    "
            //    + "           ,{4}		    "
            //    + "           ,{5}		    "
            //    + "           ,NULL         "
            //    + "           ,-1           "
            //    + "           ,NULL         "
            //    + "           ,NULL         "
            //    + "           ,NULL         "
            //    + "           ,NULL         "
            //    // + "           ,NULL         " // 2010.12.14 Delete By SES Jijianxiong
            //    + "           ,{6}          "
            //    + "           ,{7})         ";

            //string[] paramslist = new string[8];
            //// User ID
            //paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());
            //// JobUUid
            //paramslist[1] = UtilCommon.ConvertIntToSQL("0".PadLeft(25, '0'));

            //// SerialNumber
            //paramslist[2] = UtilCommon.ConvertStringToSQL(deviceinfo.serialnumber);

            //// MFPName
            //if (deviceinfo.name == null)
            //{
            //    paramslist[3] = UtilCommon.ConvertStringToSQL("");
            //}
            //else
            //{
            //    paramslist[3] = UtilCommon.ConvertStringToSQL(deviceinfo.name.Trim());
            //}
            //// MFP Model Name
            //paramslist[4] = UtilCommon.ConvertStringToSQL(deviceinfo.modelname);
            //// MFP Ip Address
            //paramslist[5] = UtilCommon.ConvertStringToSQL(deviceinfo.network_address);
            //// Status
            //paramslist[6] = UtilCommon.ConvertIntToSQL("6");
            //// ErrorCode
            //paramslist[7] = UtilCommon.ConvertStringToSQL(ErrorCode.ToString());

            // Get User Infor
            LogUserInfor userinfor = new LogUserInfor(intUserId);

            string strsql = "INSERT INTO [LogInformation]	"
                + "           ([ID]             "
                + "           ,[Time]           "
                + "           ,[UserID]         "
                + "           ,[UserName]       "
                + "           ,[LoginName]      "
                + "           ,[GroupID]        "
                + "           ,[GroupName]      "
                + "           ,[Jobuid]         "
                + "           ,[SerialNumber]   "
                + "           ,[MFPName]        "
                + "           ,[MFPModel]       "
                + "           ,[MFPIPAddress]   "
                + "           ,[Duplex]         "
                + "           ,[JobID]          "
                + "           ,[FunctionID]     "
                + "           ,[FileName]       "
                + "           ,[PageID]         "
                + "           ,[Number]         "
                //chen add 20140424 for money start
                + "           ,[PapeCount]      "
                + "           ,[CopyCount]      "
                + "           ,[SpendMoney]     "
                + "           ,[PriceDetailID]  "
                //chen add 20140424 for money end
                + "           ,[Status]         "
                + "           ,[ErrorCode]     "
                + "           ,[MFPPrintTaskID]     "
                + "           ,[DspNumber]     "
                + "           ,[DspPapeCount])     "
                + "     VALUES			    "
                + "           (ISNULL((SELECT MAX(ID) From [LogInformation]) , 0) + 1"
                + "           ,getdate()    "
                + "           ,{0}		    "
                + "           ,{8}         "
                + "           ,{9}         "
                + "           ,{10}         "
                + "           ,{11}         "
                + "           ,{1}          "
                + "           ,{2}          "
                + "           ,{3}		    "
                + "           ,{4}		    "
                + "           ,{5}		    "
                + "           ,NULL         "
                + "           ,-1           "
                + "           ,NULL         "
                + "           ,NULL         "
                + "           ,NULL         "
                + "           ,NULL         "
                //chen add 20140424 for money start
                + "           ,NULL         "
                + "           ,NULL         "
                + "           ,NULL         "
                + "           ,NULL         "
                //chen add 20140424 for money end

                + "           ,{6}          "
                + "           ,{7}          "
                + "           ,Null		    "
                + "           ,Null		    "
                + "           ,Null)		";
            string[] paramslist = new string[12];
            // User ID
            paramslist[0] = UtilCommon.ConvertIntToSQL(intUserId.ToString());

            // UserName
            paramslist[8] = UtilCommon.ConvertStringToSQL(userinfor.UserName);
            // LoginName
            paramslist[9] = UtilCommon.ConvertStringToSQL(userinfor.LoginName);
            // GroupID
            paramslist[10] = UtilCommon.ConvertIntToSQL(userinfor.GroupId.ToString());
            // GroupName
            paramslist[11] = UtilCommon.ConvertStringToSQL(userinfor.GroupName);

            // JobUUid
            paramslist[1] = UtilCommon.ConvertIntToSQL("0".PadLeft(25, '0'));

            // SerialNumber
            paramslist[2] = UtilCommon.ConvertStringToSQL(deviceinfo.serialnumber);

            // MFPName
            if (deviceinfo.name == null)
            {
                paramslist[3] = UtilCommon.ConvertStringToSQL("");
            }
            else
            {
                paramslist[3] = UtilCommon.ConvertStringToSQL(deviceinfo.name.Trim());
            }
            // MFP Model Name
            paramslist[4] = UtilCommon.ConvertStringToSQL(deviceinfo.modelname);
            // MFP Ip Address
            paramslist[5] = UtilCommon.ConvertStringToSQL(deviceinfo.network_address);
            // Status
            paramslist[6] = UtilCommon.ConvertIntToSQL("6");
            // ErrorCode
            paramslist[7] = UtilCommon.ConvertStringToSQL(ErrorCode.ToString());


            // 2011.01.10 Update By SES Jijianxiong ED

            strsql = string.Format(strsql, paramslist);

            // Ex SQL.
            using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(strsql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion


    }
    #endregion

    #region "SimpleEAUser"
    /// <summary>
    /// SimpleEAUser
    /// </summary>
    /// <Date>2010.07.20</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public class SimpleEAUser
    {
        string userid = "";
        string password = "";
        int accid = -1;

        /// <summary>
        /// SimpleEAUser
        /// </summary>
        /// <param name="_userid">user id</param>
        /// <param name="_password">password</param>
        /// <Date>2010.07.20</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public SimpleEAUser(string _userid, string _password)
        {
            userid = _userid;
            password = _password;
            accid = -1;
        }

        /// <summary>
        /// isAuthorized
        /// </summary>
        /// <Date>2010.07.20</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public Boolean isAuthorized
        {
            get
            {
                if (string.IsNullOrEmpty(userid))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(password))
                {
                    return false;
                }

                // 2011.03.21 Add By SES jijianxiong ST
                // Bug Management Sheet_SimpleEA_110321.xls No.21
                if (userid.ToLower().Equals(UtilConst.CON_USER_LONINNAME.ToLower()) 
                    || userid.ToLower().Equals(UtilConst.CON_USER_SECUADMIN.ToLower()))
                {
                    return false;
                }
                // 2011.03.21 Add By SES jijianxiong ED


                int iCount = 0;
                // Insert code that implements a site-specific custom 
                // authentication method here.
                dtUserInfoTableAdapters.UserInfoTableAdapter userInfo = new dtUserInfoTableAdapters.UserInfoTableAdapter();
                iCount = (int)userInfo.AuthenticationMFP(userid, password);
                if (iCount != 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public string accountid
        {
            get
            {
                if ( accid < 0 ) {
                    // Get Id from user information table
                    dtUserInfoTableAdapters.UserInfoTableAdapter adapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
                    dtUserInfo.UserInfoDataTable userdate = adapter.GetDataByLoginName(userid);
                    if (userdate.Rows.Count > 0)
                    {
                        accid = userdate[0].ID;
                    }
                }
                return accid.ToString();
            }

        }

        /// <summary>
        /// Can user this MFP
        /// </summary>
        /// <param name="IpAddress"></param>
        /// <returns></returns>
        /// // Add by Zheng Wei 2012.03.14
        /// check if the user could use this machine
        internal bool CanUserIt(string IpAddress,string str)
        {
            if (string.IsNullOrEmpty(IpAddress))
            {
                //throw new Exception("The IpAddress is empty.");
                // TTODO
                return true;
            }
            dtMFPUserResTableAdapters.MFPUserResTableAdapter MFPRes = new dtMFPUserResTableAdapters.MFPUserResTableAdapter();
            // Check the IpAddress and User Id is not exist in the MFPRes Table.
            dtMFPUserRes.MFPUserResDataTable MFPResult = MFPRes.GetData(IpAddress, Convert.ToInt32(str));
            if (MFPResult.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

            //            return MFPResult.Rows.Count == 0;

        }
    }
    #endregion

    #region "Get User's Info"
    /// <summary>
    /// Get User's Info
    /// </summary>
    /// <param name="accid"></param>
    /// <returns></returns>
    public static CREDENTIALS_TYPE GetUserInfo(string accid)
    {
        if (string.IsNullOrEmpty(accid))
        {
            return null;
        }

        dtUserInfoTableAdapters.UserInfoTableAdapter adapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();

        dtUserInfo.UserInfoDataTable dt = adapter.GetDataByUserId(int.Parse(accid));

        if (dt.Rows.Count == 0)
        {
            return null;
        }

        dtUserInfo.UserInfoRow row = (dtUserInfo.UserInfoRow)dt.Rows[0];

        CREDENTIALS_TYPE info = null;

        info = new CREDENTIALS_TYPE();
        info.accountid = accid;

        info.metadata = new OPAQUE_DATA_TYPE();
        XmlElement[] elements = new XmlElement[6];

        XmlDocument xmlDoc = new XmlDocument();


        XmlElement element = xmlDoc.CreateElement("property");
        XmlAttribute attrib = xmlDoc.CreateAttribute("sys-name");
        attrib.Value = "display-name";
        element.Attributes.Append(attrib);
        element.InnerText = row.UserName;
        elements[0] = element;

        element = xmlDoc.CreateElement("property");
        attrib = xmlDoc.CreateAttribute("sys-name");
        attrib.Value = "login-name";
        element.Attributes.Append(attrib);
        element.InnerText = row.LoginName;
        elements[1] = element;

        element = xmlDoc.CreateElement("property");
        attrib = xmlDoc.CreateAttribute("sys-name");
        attrib.Value = "iccard-kind";
        element.Attributes.Append(attrib);
        element.InnerText = "hid";
        elements[2] = element;

        element = xmlDoc.CreateElement("property");
        attrib = xmlDoc.CreateAttribute("sys-name");
        attrib.Value = "iccard-id";
        element.Attributes.Append(attrib);
        element.InnerText = row.ICCardID;
        elements[3] = element;

        element = xmlDoc.CreateElement("property");
        attrib = xmlDoc.CreateAttribute("sys-name");
        attrib.Value = "password";
        element.Attributes.Append(attrib);
        element.InnerText = row.Password;
        elements[4] = element;

        //chen add for scan to me start
        element = xmlDoc.CreateElement("property");
        attrib = xmlDoc.CreateAttribute("sys-name");
        attrib.Value = "email-address";
        element.Attributes.Append(attrib);
        element.InnerText = row.Email; 
        elements[5] = element;
        //chen add for scan to me end


        info.metadata.Any = elements;

        return info;
    }

    #endregion

    //chen update 20140513 start
    #region "Add MFP Information  by serial To Simple EA's DB:MFPInformation Table."
    /// <summary>
    /// Add MFP Information by serial To Simple EA's DB:MFPInformation Table.
    /// </summary>
    /// <param name="deviceinfo"></param>
    /// <Date>2010.07.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static void AddMFPInfoBySerial(DEVICE_INFO_TYPE deviceinfo)
    {
        if (deviceinfo == null)
        {
            return;
        }
        // MFP"s SerialNumber
        string serialnumber = deviceinfo.serialnumber;
        // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ST
        if (string.IsNullOrEmpty(serialnumber))
        {
            serialnumber = DeviceSession.GetSerialnumberByDevid(deviceinfo.uuid);
        }

        // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ED
        string ModelName = deviceinfo.modelname;
        string IPAddress = deviceinfo.network_address;
        string Location = deviceinfo.location;

        // 1.Check Exist: MFP Information is In DB
        if (UtilCommon.IsMFPInfoExistInDB(serialnumber))
        {
            // Update MFP information.
            UtilCommon.UpdateMFPInfo(serialnumber, ModelName, IPAddress, Location);
        }
        else
        {
            // Insert MFP information.
            UtilCommon.InsertMFPInfo(serialnumber, ModelName, IPAddress, Location);
        }

    }
    #endregion
    //chen update 20140513 start

    //chen add 20140513 start
    #region "Add MFP Information To Simple EA's DB:MFPInformation Table."
    /// <summary>
    /// Add MFP Information To Simple EA's DB:MFPInformation Table.
    /// </summary>
    /// <param name="deviceinfo"></param>
    /// <Date>2014.05.13</Date>
    /// <Author>SES chen</Author>
    /// <Version>0.01</Version>
    public static void AddMFPInfo(DEVICE_INFO_TYPE deviceinfo)
    {
        if (deviceinfo == null)
        {
            return;
        }
        // MFP"s SerialNumber
        string serialnumber = deviceinfo.serialnumber;
        // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ST
        if (string.IsNullOrEmpty(serialnumber))
        {
            serialnumber = DeviceSession.GetSerialnumberByDevid(deviceinfo.uuid);
        }


        if (UtilCommon.IsMFPInfoExistInDB(serialnumber))
        {
            AddMFPInfoBySerial(deviceinfo);
        }
        else
        {
            AddMFPInfoByIP(deviceinfo);
        }

    }
    #endregion

    #region "Add MFP Information by IP  To Simple EA's DB:MFPInformation Table."
    /// <summary>
    /// Add MFP Information by IP To Simple EA's DB:MFPInformation Table.
    /// </summary>
    /// <param name="deviceinfo"></param>
    /// <Date>2014.05.13</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public static void AddMFPInfoByIP(DEVICE_INFO_TYPE deviceinfo)
    {
        if (deviceinfo == null)
        {
            return;
        }
        // MFP"s SerialNumber
        string serialnumber = deviceinfo.serialnumber;
        // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ST
        if (string.IsNullOrEmpty(serialnumber))
        {
            serialnumber = DeviceSession.GetSerialnumberByDevid(deviceinfo.uuid);
        }

        // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ED
        string ModelName = deviceinfo.modelname;
        string IPAddress = deviceinfo.network_address;
        string Location = deviceinfo.location;

        // 1.Check Exist: MFP Information is In DB
        if (UtilCommon.IsMFPIPExistInDB(IPAddress))
        //end
        {
            // Update MFP information.
            UtilCommon.UpdateMFPInfo(serialnumber, ModelName, IPAddress, Location);
        }
        else
        {
            // Insert MFP information.
            UtilCommon.InsertMFPInfo(serialnumber, ModelName, IPAddress, Location);
        }

    }
    #endregion



    public static bool checkMFPCountOverFlow(DEVICE_INFO_TYPE deviceinfo)
    {

        // MFP"s SerialNumber
        string serialnumber = deviceinfo.serialnumber;
        // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ST
        if (string.IsNullOrEmpty(serialnumber))
        {
            serialnumber = DeviceSession.GetSerialnumberByDevid(deviceinfo.uuid);
        }

        // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ED
        string ModelName = deviceinfo.modelname;
        string IPAddress = deviceinfo.network_address;



        int authorityCount;
        int currentCount;
        bool isMFPInfoExist = true;
        try
        {
            if ( !UtilCommon.IsMFPInfoExistInDB(serialnumber) &&
                !UtilCommon.IsMFPIPExistInDB(IPAddress)  )
            {
                isMFPInfoExist = false;
            }

            if( isMFPInfoExist == false )
            {
                SLCRegister.RegisterHandler.Initiate("A");
                string path;
                authorityCount = SLCRegister.RegisterHandler.GetOperateCount(out path);

                using (dtMFPInformationTableAdapters.MFPInformationTableAdapter adpter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter())
                {
                    currentCount = (int)adpter.GetCount();
                }
                if (currentCount >= authorityCount)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }

        return false;
    }

    //end

    #region "Get MFPJob By JobName and ColorType in MFP."
    /// <summary>
    /// Get MFPJob By JobName and ColorType in MFP.
    /// </summary>
    /// <param name="JobName"></param>
    /// <param name="ColorType"></param>
    /// <returns></returns>
    /// <Date>2010.07.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public static UtilCommon.MFPJob GetMFPJob(string JobName, string ColorType)
    {
        switch (JobName)
        {
            case "COPY":
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1);
                    case "FULL-COLOR":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2);
                    default:
                        //2010.12.09 Update By SES Jijianxiong
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Copy_JobId, null);
                }
            case "PRINT":
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1);
                    case "FULL-COLOR":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2);
                    default:
                        //2010.12.09 Update By SES Jijianxiong
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Print_JobId, null);
                }
            case "LIST-PRINT":
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1);
                    case "FULL-COLOR":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2);
                    default:
                        //2010.12.09 Update By SES Jijianxiong
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_ListPrint_JobId, null);
                }
            case "FAX-SEND":
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1);
                    default:
                        //2010.12.09 Update By SES Jijianxiong
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Fax_JobId, null);
                }
            case "FAX2-SEND":
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1);
                    default:
                        //2010.12.09 Update By SES Jijianxiong
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_FaxC2_JobId, null);
                }
            case "I-FAX-SEND":
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1);
                    default:
                        //2010.12.09 Update By SES Jijianxiong ST
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_IntFax_JobId, null);
                }
            case "SCANNER":
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1);
                    case "FULL-COLOR":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2);

                    case "GRAYSCALE":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2);
                    case "AUTO":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2);
                        
                    default:
                        //2010.12.09 Update By SES Jijianxiong
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_Scan_JobId, null);
                }
            case "DOC-FILING-PRINT":
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1);
                    case "FULL-COLOR":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2);
                    default:
                        //2010.12.09 Update By SES Jijianxiong
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_DFPrint_JobId, null);
                }
            case "SCAN-TO-HDD":
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
                    case "FULL-COLOR":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
                    case "GRAYSCALE":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
                    case "AUTO":
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
                    default:
                        //2010.12.09 Update By SES Jijianxiong
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(UtilConst.ITEM_TITLE_ScanSave_JobId, null);
                }
            default:
                switch (ColorType)
                {
                    case "MONOCHROME":
                        return new UtilCommon.MFPJob(0, 1);
                    case "FULL-COLOR":
                        return new UtilCommon.MFPJob(0, 2);

                    case "GRAYSCALE":
                        return new UtilCommon.MFPJob(0, 2);
                    case "AUTO":
                        return new UtilCommon.MFPJob(0, 2);
                    default:
                        //2010.12.09 Update By SES Jijianxiong
                        // UnKnow ColorType
                        // return new UtilCommon.MFPJob(0, 1);
                        return new UtilCommon.MFPJob(0, null);
                }
        }
    }
    #endregion

    #region "Get MFPJob By E_MFP_JOB_TYPE" 
    /// <summary>
    /// GetMFPJobId
    /// </summary>
    /// <param name="jobtype"></param>
    /// <returns></returns>
    /// <Date>2010.11.17</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private static UtilCommon.MFPJob GetMFPJobId(E_MFP_JOB_TYPE jobtype)
    {
        int intRetVal = -1;
        switch (jobtype)
        {
            case E_MFP_JOB_TYPE.COPY:
                intRetVal = UtilConst.ITEM_TITLE_Copy_JobId;
                break;
            case E_MFP_JOB_TYPE.DOCFILING:
                intRetVal = UtilConst.ITEM_TITLE_DFPrint_JobId;
                break;
            case E_MFP_JOB_TYPE.PRINT:
                intRetVal = UtilConst.ITEM_TITLE_Print_JobId;
                break;
            case E_MFP_JOB_TYPE.SCAN:
                intRetVal = UtilConst.ITEM_TITLE_Scan_JobId;
                break;
            default:
                break;
        }
        // 2010.12.10 Update By SES Jijianxiong ST
        // return new UtilCommon.MFPJob(intRetVal, 0);
        return new UtilCommon.MFPJob(intRetVal, null);
        // 2010.12.10 Update By SES Jijianxiong ED
    }
    #endregion

    /// <summary>
    /// Set Job Info into Application
    /// </summary>
    public struct MFPApplicationItem
    {
        public String serialnumber;
        public String accountid;
        public E_MFP_JOB_TYPE JobType;
        public String jobuuid;
        public String jobSessionId;

        public MFPApplicationItem(CREDENTIALS_TYPE _userinfo, DEVICE_INFO_TYPE _deviceinfo, OSA_JOB_ID_TYPE mfpjobid)
        {
            accountid = _userinfo.accountid;
            // 2010.12.1 Add By SES.JiJianxiong Ver.1.1 Update ST
            serialnumber = _deviceinfo.serialnumber;
            if (string.IsNullOrEmpty(serialnumber))
            {
                serialnumber = DeviceSession.GetSerialnumberByDevid(_deviceinfo.uuid);
            }

            // 2010.12.1 Add By SES.JiJianxiong Ver.1.1 Update ED
            JobType = mfpjobid.jobtype;
            jobuuid = mfpjobid.uid;
            jobSessionId = mfpjobid.UISessionId;

        }
    }

    public class MFPApplication
    {
        public List<MFPApplicationItem> lstMFPApp;
        public const string MFPAPP_NAME = "SimpleEA_MFPApplication";
        public MFPApplication()
        {
            lstMFPApp = new List<MFPApplicationItem>();
        }

        public void AddMFP(MFPApplicationItem item)
        {
            lstMFPApp.Add(item);
        }

        public void Remove(MFPApplicationItem item)
        {
            lstMFPApp.Remove(item);
        }

        public Boolean isExist(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, OSA_JOB_ID_TYPE mfpjobid)
        {
            foreach (MFPApplicationItem item in lstMFPApp)
            {
                // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ST
                string serialnumber = deviceinfo.serialnumber;
                if (string.IsNullOrEmpty(serialnumber))
                {
                    serialnumber = DeviceSession.GetSerialnumberByDevid(deviceinfo.uuid);
                }
                // 2010.12.1 Add By SES Jijianxiong Update Ver.1.1 ED

                // The JobType is same.
                if (item.serialnumber.Equals(serialnumber) &&
                    item.accountid.Equals(userinfo.accountid) &&
                    item.JobType.Equals(mfpjobid.jobtype))
                {
                    // But jobuuid is not the same.
                    if (item.jobuuid.Equals(mfpjobid.uid))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public Boolean isExistJob(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, OSA_JOB_ID_TYPE mfpjobid)
        {
            foreach (MFPApplicationItem item in lstMFPApp)
            {

                // The JobType is same.
                // 2010.09.26 Update By SES.JiJianXiong ST
                // For the Mutil MFPs Process Update.
                //if (item.serialnumber.Equals(deviceinfo.serialnumber) &&
                //    item.accountid.Equals(userinfo.accountid) && 
                //    item.JobType.Equals(mfpjobid.jobtype))
                if (item.accountid.Equals(userinfo.accountid) &&
                    item.JobType.Equals(mfpjobid.jobtype))
                // 2010.09.26 Update By SES.JiJianXiong ED
                {
                    // But jobuuid is not the same.
                    if (!item.jobuuid.Equals(mfpjobid.uid))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }


        #region "Get Account On in Exist Job"
        /// <summary>
        /// Get Account Id On Exist Job
        /// </summary>
        /// <param name="mfpjobid"></param>
        /// <returns></returns>
        /// <Date>2010.11.29</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public int GetAccountIdOnExistJob(OSA_JOB_ID_TYPE mfpjobid)
        {
            int intAccountId = UtilConst.USER_UNKNOW;
            foreach (MFPApplicationItem item in lstMFPApp)
            {
                if (item.jobuuid.Equals(mfpjobid.uid))
                {
                    try
                    {
                        intAccountId = Convert.ToInt32(item.accountid);
                    }
                    catch 
                    {
                        ;                        
                    }
                    return intAccountId;
                }
            }

            return intAccountId;
        }
        #endregion

        // 2010.09.26 Delete By SES.JiJianXiong ST
        //public Nullable<MFPApplicationItem> GetJobuuid(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, OSA_JOB_ID_TYPE mfpjobid)
        //{
        //    foreach (MFPApplicationItem item in lstMFPApp)
        //    {
        //        if (item.serialnumber.Equals(deviceinfo.serialnumber) && 
        //            item.accountid.Equals(userinfo.accountid) && 
        //            item.JobType.Equals(mfpjobid.jobtype))
        //        {
        //            return item;
        //        }
        //    }
        //    return null;
        //}
        // 2010.09.26 Delete By SES.JiJianXiong ST
    }

    public class LogUserInfor
    {
        private string strUserName = "未知";
        public string UserName
        {
            get
            {
                return strUserName;
            }
        }

        private string strLoginName = "未知";
        public string LoginName
        {
            get
            {
                return strLoginName;
            }
        }
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
        //Default Groupid is"-2" while login not Success 
        //private int intGroupID = -1;
        private int intGroupID = -2;
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
        public int GroupId
        {
            get
            {
                return intGroupID;
            }
        }

        private string strGroupName = "未知";
        public string GroupName
        {
            get
            {
                return strGroupName;
            }
        }

        private int intUserId = -1;
        
        


        public LogUserInfor(int _intUserId)
        {
            intUserId = _intUserId;

            if (intUserId < 0)
            {
                return;
            }

            // Get User Information
            try
            {
                if (intUserId >= 0)
                {
                    dtUserInfoTableAdapters.UserInfoTableAdapter adapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
                    dtUserInfo.UserInfoDataTable userinfor = adapter.GetDataByUserId(intUserId);
                    if (userinfor.Count > 0)
                    {
                        dtUserInfo.UserInfoRow row = userinfor[0] as dtUserInfo.UserInfoRow;
                        strUserName = row.UserName;
                        strLoginName = row.LoginName;
                        intGroupID = row.GroupID;
                    }
                }
                //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
                // Get Group Information
                //if (intGroupID >= 0)
                //{
                //    dtGroupInfoTableAdapters.GroupInfoTableAdapter adapter = new dtGroupInfoTableAdapters.GroupInfoTableAdapter();
                //    dtGroupInfo.GroupInfoDataTable groupinfor = adapter.GetGroupInfoDataById(intGroupID);
                //    if (groupinfor.Count > 0)
                //    {
                //        dtGroupInfo.GroupInfoRow row = groupinfor[0] as dtGroupInfo.GroupInfoRow;
                //        strGroupName = row.GroupName;
                //    }
                //}
                if (intGroupID >= -1)
                {
                    dtGroupInfoTableAdapters.GroupInfoTableAdapter adapter = new dtGroupInfoTableAdapters.GroupInfoTableAdapter();
                    dtGroupInfo.GroupInfoDataTable groupinfor = adapter.GetGroupInfoDataById(intGroupID);
                    if (groupinfor.Count > 0)
                    {
                        dtGroupInfo.GroupInfoRow row = groupinfor[0] as dtGroupInfo.GroupInfoRow;
                        strGroupName = row.GroupName;
                    }
                }
                //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    } 
}
