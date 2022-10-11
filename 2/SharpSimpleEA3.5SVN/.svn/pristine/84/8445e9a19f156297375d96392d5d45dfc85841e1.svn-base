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
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Osa.MfpWebService;
using Osa.Util;
using System.Diagnostics;
using System.Xml;
using System.Text.RegularExpressions;
using DAL;
using common;
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class MfpSink : System.Web.Services.WebService
{
    //=========================================================================================
    // 1) Hello

    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/Hello",
        RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        Use = System.Web.Services.Description.SoapBindingUse.Literal,
        ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayAttribute("set-extensions")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("auth-extension", IsNullable = false)]
    public AUTH_EXTENSION_TYPE[] Hello(
        [System.Xml.Serialization.XmlElementAttribute("device-info")]
		DEVICE_INFO_TYPE deviceinfo,
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
		CREDENTIALS_TYPE Credentials,
        [System.Xml.Serialization.XmlArrayAttribute("osa-reg-applications", IsNullable = true)]
		[System.Xml.Serialization.XmlArrayItemAttribute("osa-reg-app", IsNullable = false)]
		OSA_REG_APPLICATION_TYPE[] osaregapplications,
        [System.Xml.Serialization.XmlArrayAttribute("mfp-web-services", IsNullable = true)]
		[System.Xml.Serialization.XmlArrayItemAttribute("web-service", IsNullable = false)]
		MFP_WEBSERVICE_TYPE[] mfpwebservices,
        [System.Xml.Serialization.XmlElementAttribute("xml-doc-acl", IsNullable = true)]
		ACL_DOC_TYPE xmldocacl,
        [System.Xml.Serialization.XmlArrayAttribute("xml-doc-limits", IsNullable = true)]
		[System.Xml.Serialization.XmlArrayItemAttribute("limits", IsNullable = false)]
		LIMITS_TYPE[] xmldoclimits,
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)] 
		[System.Xml.Serialization.XmlArrayItemAttribute("counter", IsNullable = false)]
		COUNTER_TYPE[] counters,
		[System.Xml.Serialization.XmlArrayItemAttribute("auth-extension", IsNullable = false)]
        AUTH_EXTENSION_TYPE[] extensions,
        [System.Xml.Serialization.XmlAttributeAttribute()]
		ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")]
		string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")]
		string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")]
		string operationversion)
    {

        try
      {
            // 2012.06.12 Add by Wei Changye 
            // 可控台数
            // 如果可控台数太多，则剔除
            //20140513 update by chen
            //if (!UtilCommon.IsMFPInfoExistInDB(deviceinfo.serialnumber) && UtilCommon.IsMFPCountOverFlow(deviceinfo.serialnumber))
            //{
            //    return null;
            //}
          if (Helper.checkMFPCountOverFlow(deviceinfo))
            {
                //return null;
                ;
            }
            //end
            //end

            // Using Helper function, add the MFP identity as a DeviceSession.
            // We will not use 'osaregapplications' and 'counters' in this sample.
            //
            Helper.DeviceSession.Create(deviceinfo, mfpwebservices, xmldocacl, Credentials, xmldoclimits, generic);

            Helper.DeviceSession devSession = Helper.DeviceSession.Get(deviceinfo.uuid);
            devSession.InitializeMfp(HttpContext.Current.Request.Url.AbsoluteUri);

            // Insert MFP Information into SimpleEA DB.
            // Add MFP Information To Simple EA's DB:MFPInformation Table.
            Helper.AddMFPInfo(deviceinfo);


            // 2010.12.21 Add By SES Jijianxong ST
            //Application["loggedinuser"] = UtilConst.USER_UNKNOW;
            string strserialNumber = deviceinfo.uuid;
            //Application["loggedinuser"] = UtilConst.USER_UNKNOW;
            Application[strserialNumber] = UtilConst.USER_UNKNOW;
            // 2010.12.21 Add By SES Jijianxong ED


            //chen add start
            //for ICCard support

            if (null != extensions)
            {
                for (int i = 0; i < extensions.Length; i++)
                {
                    Global.acceptCard[i] = extensions[i].datatype;
                }

                Global.acceptCardNum = extensions.Length;
            }

            //chen add end

            if (extensions != null)
            {
                return extensions;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {

            Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_SYSERROR);
            throw ex;
        }

    }

    //=========================================================================================
    // 2) Authenticate

    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/Authenticate",
        RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        Use = System.Web.Services.Description.SoapBindingUse.Literal,
        ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("xml-doc-acl")]
    public ACL_DOC_TYPE Authenticate(
        [System.Xml.Serialization.XmlElementAttribute("user-info")]
		CREDENTIALS_TYPE userinfo,
        [System.Xml.Serialization.XmlElementAttribute("device-info")]
		DEVICE_INFO_TYPE deviceinfo,
        [System.Xml.Serialization.XmlAttributeAttribute()]
		ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")]
		string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")]
		string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")]
		string operationversion)
    {
        try
        {
            // 2011.03.21 Add By SES jijianxiong ST
            // Bug Management Sheet_SimpleEA_110321.xls No.22
            ACL_DOC_TYPE def_acl = null;
            def_acl = Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl;
            // 2011.03.21 Add By SES jijianxiong ED


            ACL_DOC_TYPE acl = null;

            if (userinfo == null)
            {
                // 2011.03.21 Update By SES jijianxiong ST
                // Bug Management Sheet_SimpleEA_110321.xls No.22
                // throw new SoapException(UtilConst.MSG_MFP_LOGIN_ERROR_BLANK, SoapException.ClientFaultCode);
                Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
                return def_acl;
                // 2011.03.21 Update By SES jijianxiong ED

            }
            //1.Get LoginName
            string loginName = userinfo.accountid;

            if (string.IsNullOrEmpty(loginName))
            {
                // 2011.03.21 Update By SES jijianxiong ST
                // Bug Management Sheet_SimpleEA_110321.xls No.22
                // throw new SoapException(UtilConst.MSG_MFP_LOGIN_ERROR_BLANK, SoapException.ClientFaultCode);
                Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
                return def_acl;
                // 2011.03.21 Update By SES jijianxiong ED
            }

            ////////2018-01-25 
            string password = "";
            dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
            int Login_auth_method = settingDispRow.Login_Auth_method;
            Boolean authPrint = false;
            if (Login_auth_method == UtilConst.USER_LDAP)
            {
                LDAPHandler ldapAuth = new LDAPHandler();
                string ret = ldapAuth.LDAPAuthPrint(loginName);
                if (ret.Equals(""))
                {
                    password = UtilConst.USER_PASSWORD;
                    authPrint = true;
                }
                else
                {
                    authPrint = false;
                    Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
                    return def_acl;

                }

            }

            if (!authPrint)
            {
                ////////////

                //2.Get Password
                if (userinfo.metadata == null)
                {
                    // 2011.03.21 Update By SES jijianxiong ST
                    // Bug Management Sheet_SimpleEA_110321.xls No.22
                    // throw new SoapException(UtilConst.MSG_MFP_LOGIN_ERROR_BLANK, SoapException.ClientFaultCode);
                    Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
                    return def_acl;
                    // 2011.03.21 Update By SES jijianxiong ED
                }
                // 2010.12.01 Add By SES Jijianxiong Ver.1.1 Update ST
                // OSA 2.0 Only: userinfo.metadata.Any == null
                if (userinfo.metadata.Any == null)
                {
                    // 2011.03.21 Update By SES jijianxiong ST
                    // Bug Management Sheet_SimpleEA_110321.xls No.22
                    // throw new SoapException(UtilConst.MSG_MFP_LOGIN_ERROR_BLANK, SoapException.ClientFaultCode);
                    Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
                    return def_acl;
                    // 2011.03.21 Update By SES jijianxiong ED
                }
                // 2010.12.01 Add By SES Jijianxiong Ver.1.1 Update ED

                if (userinfo.metadata.Any.Length == 0)
                {
                    // 2011.03.21 Update By SES jijianxiong ST
                    // Bug Management Sheet_SimpleEA_110321.xls No.22
                    // throw new SoapException(UtilConst.MSG_MFP_LOGIN_ERROR_BLANK, SoapException.ClientFaultCode);
                    Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
                    return def_acl;
                    // 2011.03.21 Update By SES jijianxiong ED
                }

                if (!userinfo.metadata.Any[0].Name.Equals("password"))
                {
                    // 2011.03.21 Update By SES jijianxiong ST
                    // Bug Management Sheet_SimpleEA_110321.xls No.22
                    // throw new SoapException(UtilConst.MSG_MFP_LOGIN_ERROR_BLANK, SoapException.ClientFaultCode);
                    Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
                    return def_acl;
                    // 2011.03.21 Update By SES jijianxiong ED
                }





                password = userinfo.metadata.Any[0].InnerText;
                if (string.IsNullOrEmpty(password))
                {
                    // 2011.03.21 Update By SES jijianxiong ST
                    // Bug Management Sheet_SimpleEA_110321.xls No.22
                    // throw new SoapException(UtilConst.MSG_MFP_LOGIN_ERROR_BLANK, SoapException.ClientFaultCode);
                    Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
                    return def_acl;
                    // 2011.03.21 Update By SES jijianxiong ED
                }
            //////////////
            }
            ////////////////

                //3.CheckUser
                Helper.SimpleEAUser user = new Helper.SimpleEAUser(loginName, password);

                // Add by Zheng Wei 2012.03.14
                // check if the user could use this machine
                String IpAddress = deviceinfo.network_address;
                if (!user.CanUserIt(IpAddress, user.accountid))
                {
                    throw new SoapException(UtilConst.MSG_MFP_USER_NOTA, SoapException.ClientFaultCode);
                }

            if (user.isAuthorized)
            {
                userinfo.accountid = user.accountid;
                // 2010.12.21 Add By SES Jijianxong ST
                //Application["loggedinuser"] = userinfo.accountid;
                string strserialNumber = deviceinfo.uuid;
                Application[strserialNumber] = userinfo.accountid;
                // 2010.12.21 Add By SES Jijianxong ED
                UserManager userman = new UserManager();
                // 2011.03.21 Update By SES jijianxiong ST
                // Bug Management Sheet_SimpleEA_110321.xls No.22
                // acl = userman.GetUserACL(userinfo.accountid, Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl);
                acl = userman.GetUserACL(userinfo.accountid, def_acl);
                // 2011.03.21 Update By SES jijianxiong ED

                if (null == acl)
                {
                    // 2011.05.25 Update By SLC zhoumiao ST
                    // return null;
                    return acl;
                    // 2011.05.25 Update By SLC zhoumiao ED
                }
                // Get the accid and the password from the login name.
                return acl;
            }
            else
            {
                throw new SoapException(UtilConst.MSG_MFP_LOGIN_ERROR, SoapException.ClientFaultCode);
            }
        }
        catch (SoapException se)
        {
            // 2010.12.21 Add By SES Jijianxong ST
            //Application["loggedinuser"] = UtilConst.USER_UNKNOW;
            string strserialNumber = deviceinfo.uuid;
            Application[strserialNumber] = userinfo.accountid;
            // 2010.12.21 Add By SES Jijianxong ED
            Helper.LogAccountant.RecordErrLog("-1", deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);

            // add by Wei Changye 2012.03.23
            Global.Log(se.ToString());

            // 2011.05.25 Update By SLC zhoumiao ST
            //return null;
            ACL_DOC_TYPE def_acl = null;
            def_acl = Helper.DeviceSession.Get(deviceinfo.uuid).xmldocacl;
            return def_acl;
            // 2011.05.25 Update By SLC zhoumiao ED
            
        }
        catch (Exception ex)
        {
            // 2010.12.21 Add By SES Jijianxong ST
            //Application["loggedinuser"] = UtilConst.USER_UNKNOW;
            string strserialNumber = deviceinfo.uuid;
            Application[strserialNumber] = userinfo.accountid;
            // 2010.12.21 Add By SES Jijianxong ED
            Helper.LogAccountant.RecordErrLog(userinfo.accountid, deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_SYSERROR);

            // add by Wei Changye 2012.03.23
            Global.Log(ex.ToString());
            throw ex;
        }
    }

    //=========================================================================================
    // 3) Authorize

    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/Authorize",
        RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        Use = System.Web.Services.Description.SoapBindingUse.Literal,
        ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlArrayAttribute("xml-doc-limits")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("limits", IsNullable = false)]
    public LIMITS_TYPE[] Authorize(
        [System.Xml.Serialization.XmlElementAttribute("mfp-job-id")]
		MFP_JOB_ID_TYPE mfpjobid,
        [System.Xml.Serialization.XmlElementAttribute("user-info")]
		CREDENTIALS_TYPE userinfo,
        [System.Xml.Serialization.XmlElementAttribute("device-info")]
		DEVICE_INFO_TYPE deviceinfo,
        [System.Xml.Serialization.XmlElementAttribute("job-settings")]
		JOBSETTINGS_TYPE jobsettings,
        [System.Xml.Serialization.XmlAttributeAttribute()]
		ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")]
		string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")]
		string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")]
		string operationversion)
    {
        try
        {
            // 2012.06.19  Add by Wei Changye
            // check sn is in DB
            if (!UtilCommon.IsSerialNOExsit(deviceinfo.serialnumber))
            {
                Helper.LogAccountant.RecordErrLog(userinfo.accountid, deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_REGISTER_LIMIT);
                return null;
            }
            //end

            // Using helper function, obtain the limit document filled in with data related to the account
            if (null == userinfo)
            {
                // fatal error
                string ErroString = "Authorize parameter userinfo cannot be null";
                throw new SoapException(ErroString, SoapException.ClientFaultCode);
            }
            if (null == deviceinfo)
            {
                // fatal error
                string ErroString = "Authorize parameter deviceinfo cannot be null";
                throw new SoapException(ErroString, SoapException.ClientFaultCode);
            }

            if (mfpjobid.Item is OSA_JOB_ID_TYPE)
            {
                // 2011.03.22 Delete By SES Jijianxiong ST
                // Delete this process(Continuous operation Check).
                // CheckSameJob(userinfo, deviceinfo, mfpjobid.Item as OSA_JOB_ID_TYPE);
                // 2011.03.22 Delete By SES Jijianxiong ED
                CreateApplication(userinfo, deviceinfo, mfpjobid.Item as OSA_JOB_ID_TYPE);
            }

            try
            {
                LIMITS_TYPE[] limits = null;
                UserManager userman = new UserManager();
                string serialnumber = deviceinfo.serialnumber;
                limits = userman.GetUserAbleLimitsByMoney(userinfo.accountid,serialnumber, Helper.DeviceSession.Get(deviceinfo.uuid).xmldoclimits);
                if (null != limits)
                {
                    return limits;
                }
                throw new SoapException("Invalid Credential", SoapException.ClientFaultCode);
            }
            catch (Exception ex)
            {
                // add by Wei Changye 2012.03.23
                Global.Log(ex.ToString());
                Helper.LogAccountant.RecordErrLog(userinfo.accountid, deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_SYSERROR);
                throw ex;
            }


        }
        catch (SoapException soap)
        {
            // 2010.12.20 Add By SES Ji.Jijianxiong ST
            // Do not throw the SoapException.
            throw soap;
            // 2010.12.20 Add By SES Ji.Jijianxiong ST
        }
    }

    //=========================================================================================
    // 4) Event

    [WebMethod]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/Event",
        RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
        Use = System.Web.Services.Description.SoapBindingUse.Literal,
        ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void Event(
        [System.Xml.Serialization.XmlElementAttribute("event-data")]
		EVENT_DATA_TYPE eventdata,
        [System.Xml.Serialization.XmlAttributeAttribute()]
		ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")]
		string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")]
		string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")]
		string operationversion)
    {
        JOB_RESULTS_BASE_TYPE result = null;
        DETAILS_ON_JOB_COMPLETED_TYPE detalCompleted = null;
        
        try
        {
            // 2011.06.01 Add By SLC zhoumiao Ver.1.1  ST
            if (eventdata.userinfo.accountid.Equals(UtilConst.CON_DATE_SYSTEM_NAME))
            {
                eventdata.userinfo.accountid = "-1";
            }
            // 2011.06.01  Add By SLC zhoumiao Ver.1.1  ED

            //2011.5.25 Add By SLC zhoumiao Ver.1.1 Update ST
            //if (eventdata.userinfo.accountid == null)
            //{
            //    return;

            //}
            //2011.5.25 Add By SLC zhoumiao Ver.1.1 Update ED
            if (eventdata.details is DETAILS_ON_JOB_COMPLETED_TYPE)
            {
                detalCompleted = (DETAILS_ON_JOB_COMPLETED_TYPE)eventdata.details;
                result = (JOB_RESULTS_BASE_TYPE)detalCompleted.JobResults;
            }
            //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ST
            //check user is admin 
            if (eventdata.userinfo.accountid.Equals(UtilConst.CON_DATE_ADMIN_NAME))
            {
                eventdata.userinfo.accountid = "0";
                // 2011.03.28 Delete By SES Jijianxiong ST
                //Application["admin"] = "0";
                // 2011.03.28 Delete By SES Jijianxiong ED
            }

            // 2011.03.28 Delete By SES Jijianxiong ST
            ////set the accountid equals 0 while eventdata.details is DETAILS_ON_JOB_STARTED_TYPE and user name is "admin"
            //if ((!string.IsNullOrEmpty(Application["admin"].ToString()))&&(eventdata.details is DETAILS_ON_JOB_COMPLETED_TYPE))
            //{
            //    eventdata.userinfo.accountid = "0";
            //    Application["admin"] = "";
            //}
            // 2011.03.28 Delete By SES Jijianxiong ED

            

            //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ED


            // 2010.11.29 Add By SES Jijianxiong Ver.1.1 Update ST
            // OSA 2.0 UserInfo is null while eventdata.details is DETAILS_ON_JOB_STARTED_TYPE
            // Check DETAILS ABSTRACT TYPE.
            if (eventdata.details is DETAILS_ON_JOB_STARTED_TYPE)
            {
                DETAILS_ON_JOB_STARTED_TYPE starttype = eventdata.details as DETAILS_ON_JOB_STARTED_TYPE;
                // Get details on JobId
                OSA_JOB_ID_TYPE osajobid = starttype.mfpjobid.Item as OSA_JOB_ID_TYPE;
                // Get Details on jobmode
                JOB_MODE_TYPE jobmode = starttype.jobmode;
                //SetAccountId(eventdata.userinfo, osajobid);
                string uuid = eventdata.deviceinfo.uuid;
                SetAccountId(uuid, eventdata.userinfo, osajobid);
            }

            // 2010.11.29 Add By SES Jijianxiong Ver.1.1 Update ED


            // 2010.11.17 Add By SES Jijianxiong Ver.1.1 Update ST
            // Record the log
            (new Helper.LogAccountant()).RecordLog(eventdata);
            // 2010.11.17 Add By SES Jijianxiong Ver.1.1 Update ED


            // Check for detail type of eventdata. If it is DETAILS_ON_JOB_COMPLETED_TYPE
            // then record the click counts by calling RecordClicks()
            //
            if (eventdata.details is DETAILS_ON_JOB_COMPLETED_TYPE)
            {
               
                detalCompleted = (DETAILS_ON_JOB_COMPLETED_TYPE)eventdata.details;
                result = (JOB_RESULTS_BASE_TYPE)detalCompleted.JobResults;
                OSA_JOB_ID_TYPE jobidtype = result.mfpjobid.Item as OSA_JOB_ID_TYPE;
                switch (result.jobstatus.status)
                {
                    case E_JOB_STATUS_TYPE.FINISHED:
                    case E_JOB_STATUS_TYPE.CANCELED:    // 2010.11.17 Add By SES Jijianxiong Ver.1.1 Update
                        new Helper.MyAccountant().RecordClicks(eventdata);
                        if (result.mfpjobid.Item is OSA_JOB_ID_TYPE)
                        {
                            ReleaseApplication(eventdata.userinfo, eventdata.deviceinfo, jobidtype);
                        }
                        break;
                    // 2010.11.17 Delete By SES Jijianxiong Ver.1.1 Update ST
                    //case E_JOB_STATUS_TYPE.STARTED:
                    //    break;
                    // 2010.11.17 Delete By SES Jijianxiong Ver.1.1 Update ED
                    default:
                        if (result.mfpjobid.Item is OSA_JOB_ID_TYPE)
                        {
                            ReleaseApplication(eventdata.userinfo, eventdata.deviceinfo, result.mfpjobid.Item as OSA_JOB_ID_TYPE);
                        }
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Global.Log(ex.ToString());

            // While System Exception happend
            if ( result != null) {
                ReleaseApplication(eventdata.userinfo, eventdata.deviceinfo, result.mfpjobid.Item as OSA_JOB_ID_TYPE);
            }
            Helper.LogAccountant.RecordErrLog(eventdata.userinfo.accountid, eventdata.deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_SYSERROR);
            throw ex;
        }

    }

    //// 5) NotifyUserCredentials
    //[WebMethod]
    //[System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/NotifyUserCredentials",
    //    RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
    //    ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1",
    //    Use = System.Web.Services.Description.SoapBindingUse.Literal,
    //    ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    //public void NotifyUserCredentials(
    //    [System.Xml.Serialization.XmlElementAttribute("user-info")] CREDENTIALS_TYPE userinfo,
    //    [System.Xml.Serialization.XmlElementAttribute("device-info")] DEVICE_INFO_TYPE deviceinfo,
    //    [System.Xml.Serialization.XmlAttributeAttribute()] ref string generic,
    //    [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")] string productfamily,
    //    [System.Xml.Serialization.XmlAttributeAttribute("product-version")] string productversion,
    //    [System.Xml.Serialization.XmlAttributeAttribute("operation-version")] string operationversion)
    //{
    //    bool supportedCard = true;
    //    string strError = string.Empty;
    //    strError = "The IC card you used is not valid.";
    //    strError += "                                    ";
    //    strError += "Click the OK button and try again.";

    //    if (supportedCard)
    //    {
    //        if ("hid" == userinfo.datatype)
    //        {
    //            string CardDetails = string.Empty;
    //            for (int i = 0; i < userinfo.metadata.Any.Length; i++)
    //            {
    //                XmlElement data = userinfo.metadata.Any[i];
    //                if (data.Name.CompareTo("data") == 0)
    //                {
    //                    CardDetails = data.InnerText;
    //                    break;
    //                }
    //            }

    //            //Added by Le Ning 2013.8.27;
    //            byte[] binaryData;
    //            try
    //            {
    //                binaryData = System.Convert.FromBase64String(CardDetails);
    //                CardDetails = System.Text.Encoding.UTF8.GetString(binaryData);

    //            }
    //            catch (System.Exception)
    //            {
    //                return;
    //            }

    //            ////Read the user mapping file and then assign the account number to the "id_domain"
    //            //AccountDetails acctDet = null;
    //            //XmlSerializer ser = new XmlSerializer(typeof(AccountDetails));
    //            //string filePath = HostingEnvironment.ApplicationPhysicalPath + "data\\MfpExtDetails.xml";
    //            //using (TextReader tr = new StreamReader(filePath))
    //            //{
    //            //    acctDet = (AccountDetails)ser.Deserialize(tr);
    //            //}

    //            //string actNum = string.Empty;
    //            //if (acctDet != null)
    //            //{
    //            //    for (int i = 0; i < acctDet.accounts.Length; i++)
    //            //    {
    //            //        Account acct = acctDet.accounts[i];
    //            //        if (acct.Data == CardDetails)
    //            //        {
    //            //            actNum = acct.Id;
    //            //            break;
    //            //        }
    //            //    }
    //            //}

    //            //if (actNum == string.Empty)
    //            //{
    //            //    Application["ICCardLoginError"] = "true";
    //            //    Application["ErrorString"] = strError;
    //            //    url.Item = @"LogonError.aspx";
    //            //    mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
    //            //    throw new SoapException(strError, SoapException.ClientFaultCode);
    //            //}
    //            //else
    //            //{
    //            //    url.Item = @"Default.aspx?op=validate&id_domain=" + actNum;
    //            //    mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
    //            //}
    //        }
    //    }
    //    else
    //    {
    //        //Application["ICCardLoginError"] = "true";
    //        //Application["ErrorString"] = strError;
    //        //url.Item = @"LogonError.aspx";
    //        //mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
    //        //throw new SoapException(strError, SoapException.ClientFaultCode);
    //    }
    //}   

    //chen add start

    /// <summary>
    /// Description for GetUserInfo()
    /// </summary>
    /// <param name="arg"></param>
    /// <param name="generic"></param>
    /// <param name="productfamily"></param>
    /// <param name="productversion"></param>
    /// <param name="operationversion"></param>
    /// <returns></returns>
    [WebMethod(Description = @"
			GetUserInfo() method is implemented by 
            external authority applications. It retrieves 
            information about the user identified by account-id.
            When the MFP needs information about the user, 
            it calls the GetUserInfo method exposed by the external 
            authority application's web service, passing the account-id. 
            This can occur just after EnableDevice or AuthenticateResponse.
            The CREDENTIALS_TYPE value returned tells the MFP information 
            about the user specified. Besides account-id, it contains metadata, 
            which in turn typically contains login-name, display-name and 
            email-address of the user.
			")]

    //[System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/GetUserInfo", RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("user-info")]
    public CREDENTIALS_TYPE GetUserInfo(GETUSERINFO_ARG_TYPE arg,
        [System.Xml.Serialization.XmlAttributeAttribute()] ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")] string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")] string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")] string operationversion)
    {

        string serialnumber = arg.deviceinfo.uuid;
        //Implementation for GetUserInfo()
        //if (null != Application["loggedinuser"])
        if (null != Application[serialnumber])
        {
            //string accountid = Application["loggedinuser"].ToString();
            string accountid = Application[serialnumber].ToString();

            CREDENTIALS_TYPE xmlCred = Helper.GetUserInfo(accountid);
            
            //CREDENTIALS_TYPE xmlCred = new CREDENTIALS_TYPE();
            //xmlCred.accountid = Application["loggedinuser"].ToString();

            //xmlCred.metadata = new OPAQUE_DATA_TYPE();
            //XmlElement[] elements = new XmlElement[3];

            //XmlDocument xmlDoc = new XmlDocument();
            //XmlElement element = xmlDoc.CreateElement("property");
            //XmlAttribute attrib = xmlDoc.CreateAttribute("sys-name");
            //attrib.Value = "login-name";
            //element.Attributes.Append(attrib);
            //element.InnerText = xmlCred.accountid;
            //elements[0] = element;

            //element = xmlDoc.CreateElement("property");
            //attrib = xmlDoc.CreateAttribute("sys-name");
            //attrib.Value = "display-name";
            //element.Attributes.Append(attrib);
            //element.InnerText = "User Name" + xmlCred.accountid;
            //elements[1] = element;

            //element = xmlDoc.CreateElement("property");
            //attrib = xmlDoc.CreateAttribute("sys-name");
            //attrib.Value = "email-address";
            //element.Attributes.Append(attrib);
            //element.InnerText = xmlCred.accountid + "@example.com";
            //elements[2] = element;

            //xmlCred.metadata.Any = elements;
            return xmlCred;
        }
        return null;
    }



    [WebMethod(Description = @"
        NotifyUserCredentials() is implemented in ExternalAuthority applications.
        This method is invoked when user tries to login using an ICCard in MFP/OsaSimulator.
        The inserted/selected card credentials are sent through this webmethod.
        Along with credentials MFP/OsaSimulator sends the supported ICCard type.
        If the cardinfo received in this webmethod matches with the cardinfo sent in
        Hello() then ACL page is shown to the user.
        If the credentials are not valid then error screen will be shown to the user.
        ")]
    /// <summary>
    /// This method invoked when user tries to login using an ICCard in MFP/OsaSimulator
    /// </summary>
    /// <param name="userinfo"></param>
    /// <param name="deviceinfo"></param>
    /// <param name="generic"></param>
    /// <param name="productfamily"></param>
    /// <param name="productversion"></param>
    /// <param name="operationversion"></param>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/NotifyUserCredentials", RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public void NotifyUserCredentials(
        [System.Xml.Serialization.XmlElementAttribute("user-info")] CREDENTIALS_TYPE userinfo,
        [System.Xml.Serialization.XmlElementAttribute("device-info")] DEVICE_INFO_TYPE deviceinfo,
        [System.Xml.Serialization.XmlAttributeAttribute()] ref string generic,
        [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")] string productfamily,
        [System.Xml.Serialization.XmlAttributeAttribute("product-version")] string productversion,
        [System.Xml.Serialization.XmlAttributeAttribute("operation-version")] string operationversion)
    {
     
        bool supportedCard = false;
        string strError = string.Empty;
        strError = "The IC card you used is not valid.";
        strError += "                                    ";
        strError += "Click the OK button and try again.";


        for (int i = 0; i < Global.acceptCardNum; i++)
        {
            if (userinfo.datatype == Global.acceptCard[i])
            {
                supportedCard = true;
            }
        }
        supportedCard = true;

        SHOWSCREEN_TYPE url = new SHOWSCREEN_TYPE();
        //CREDENTIALS_TYPE credential = Application[deviceinfo.uuid] as CREDENTIALS_TYPE;
        //MFPCoreWS mfpWS = Helper.GetConfiguredMfpCoreWs(deviceinfo.network_address, credential);
        //Helper.DeviceSession dev = Helper.DeviceSession.Get(sn);
        //MFPCoreWSEx mfpWS = dev.GetConfiguredMfpCoreWS();
        Helper.DeviceSession dev = Helper.DeviceSession.Get(deviceinfo.uuid);
        MFPCoreWSEx mfpWS = dev.GetConfiguredMfpCoreWS();

        //chen add for ICCARDLogin flg
        string ICCardLoginflg = UtilCommon.GetICCardLoginFlg;

        if (!ICCardLoginflg.Equals("true"))
        {
            url.Item = @"Default.aspx?op=noiccardlogin&iccard_id=1";
            //url.Item = @"OsaMain4.aspx?op=iccardlogin&iccard_id=" + CardDetails;
            mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
            return;
        }
        //


        if (supportedCard)
        {
            if ("hid" == userinfo.datatype)
            {
                string CardDetails = string.Empty;
                for (int i = 0; i < userinfo.metadata.Any.Length; i++)
                {
                    XmlElement data = userinfo.metadata.Any[i];
                    if (data.Name.CompareTo("data") == 0)
                    {
                        CardDetails = data.InnerText;
                        //string CardDetails = data.InnerText;
                        byte[] encodedDataAsBytes = System.Convert.FromBase64String(CardDetails);
                        CardDetails = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);

                        string endChar = CardDetails.Substring(CardDetails.Length - 1, 1);
                        Regex rex = new Regex("[a-z0-9A-Z_]+");
                        Match ma = rex.Match(endChar); 
                        if (ma.Success) 
                        {
                            CardDetails = CardDetails.Substring(0, CardDetails.Length);
                        } 
                        else 
                        {
                            CardDetails = CardDetails.Substring(0, CardDetails.Length - 1);
                        } 
                        
                        //chen add for ICCardLen start
                        int ICCardLen = UtilCommon.GetICCardLen;
                        if (CardDetails.Length < ICCardLen)
                        {
                            string ErroString = "ICCardLen is less than ICCardLen of WebConfig!";
                            throw new SoapException(ErroString, SoapException.ClientFaultCode);
                        }
                        else if (CardDetails.Length > ICCardLen)
                        {
                            CardDetails = CardDetails.Substring(0, UtilCommon.GetICCardLen);
                        }
                        //chen add for ICCardLen end

                        break;
                    }
                }

                //Read the user mapping file and then assign the account number to the "id_domain"
                //AccountDetails acctDet = null;
                //XmlSerializer ser = new XmlSerializer(typeof(AccountDetails));
                //string filePath = HostingEnvironment.ApplicationPhysicalPath + "data\\MfpExtDetails.xml";
                //using (TextReader tr = new StreamReader(filePath))
                //{
                //    acctDet = (AccountDetails)ser.Deserialize(tr);
                //}

               // string actNum = CardDetails;
                //if (acctDet != null)
                //{
                //    for (int i = 0; i < acctDet.accounts.Length; i++)
                //    {
                //        Account acct = acctDet.accounts[i];
                //        if (acct.Data == CardDetails)
                //        {
                //            actNum = acct.Id;
                //            break;
                //        }
                //    }
                //}

                //if (actNum == string.Empty)
                //{
                //    Application["ICCardLoginError"] = "true";
                //    Application["ErrorString"] = strError;
                //    url.Item = @"LogonError.aspx";
                //    mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
                //    throw new SoapException(strError, SoapException.ClientFaultCode);
                //}
                //else
                //{
                //    url.Item = @"Default.aspx?op=validate&id_domain=" + actNum;
                //    mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
                //}

                //20190611测试为了第一个数字0
                //CardDetails = "0012345678";
                //

                dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
                int Login_auth_method = settingDispRow.Login_Auth_method;
                if (Login_auth_method == UtilConst.USER_SYS)
                {
                    dtUserInfoTableAdapters.UserInfoTableAdapter UserinfoAdapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
                    dtUserInfo.UserInfoDataTable userinfoDT = UserinfoAdapter.GetDataByICCard(CardDetails);
                    if (userinfoDT != null && userinfoDT.Count > 0)
                    //if (UserInfo.Length > 0)
                    {
                        url.Item = @"Default.aspx?op=iccardlogin&iccard_id=" + CardDetails;
                        //url.Item = @"OsaMain4.aspx?op=iccardlogin&iccard_id=" + CardDetails;
                        mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
                    }
                    else
                    {
                        url.Item = @"Default.aspx?op=iccardlogin&iccard_id=" + CardDetails;
                        //url.Item = @"OsaMain4.aspx?op=iccardlogin&iccard_id=" + CardDetails;
                        //mfpWS.ShowScreen(E_MFP_SHOWSCREEN_TYPE.TOP_LEVEL_SCREEN);
                        mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
                        throw new SoapException(strError, SoapException.ClientFaultCode);
                    }
                }
                else if (Login_auth_method == UtilConst.USER_DB)
                {
                    DBAuthHandler authHandler = new DBAuthHandler();
                    string check = authHandler.DBAuthCard(CardDetails);
                    strError = check;

                    if (check.Equals(""))
                    {
                        url.Item = @"Default.aspx?op=iccardlogin&iccard_id=" + CardDetails;
                        //url.Item = @"OsaMain4.aspx?op=iccardlogin&iccard_id=" + CardDetails;
                        mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
                    }
                    else
                    {
                        //url.Item = @"Default.aspx?op=iccardlogin&iccard_id=" + CardDetails;
                        Application["ErrorString"] = strError;
                        url.Item = @"LogonError.aspx?ErrMsg=" +strError;
                        mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
                        throw new SoapException(strError, SoapException.ClientFaultCode);
                    }
                }
                else if (Login_auth_method == UtilConst.USER_LDAP)
                {
                    LDAPHandler ldapAuth = new LDAPHandler();
                    string ret = ldapAuth.LDAPAuthCard(CardDetails);
                    strError = ret;
                    if (ret.Equals(""))
                    {
                        url.Item = @"Default.aspx?op=iccardlogin&iccard_id=" + CardDetails;
                        mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
                    }
                    else if (ret == "iccardnotindb")
                    {
                        url.Item = @"Default.aspx?op=iccardlogin&iccard_id=" + CardDetails;
                        mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
                        throw new SoapException(strError, SoapException.ClientFaultCode);
                    }
                    else
                    {
                        Application["ErrorString"] = strError;
                        url.Item = @"LogonError.aspx?ErrMsg=" + strError;
                        mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
                        throw new SoapException(strError, SoapException.ClientFaultCode);

                    }
                }
               // string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
                //string UserInfo = SesMiddleware.ICCardData.GetUserInfo(CardDetails, serverPath);

            }
        }
        else
        {
            //Application["ICCardLoginError"] = "true";
            //Application["ErrorString"] = strError;
            //url.Item = @"LogonError.aspx";
            //mfpWS.ShowScreen(Global.UISessionId, url, ref generic);
            //throw new SoapException(strError, SoapException.ClientFaultCode);
        }
    }
    //chen add end


    #region "Release Job Item in Application for Simple EA."
    /// <summary>
    /// Release Job Item in Application for Simple EA.
    /// </summary>
    /// <param name="userinfo"></param>
    /// <param name="deviceinfo"></param>
    /// <param name="mfpjobid"></param>
    /// <Date>2010.07.30</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void ReleaseApplication(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, OSA_JOB_ID_TYPE mfpjobid)
    {

        if (Application[Helper.MFPApplication.MFPAPP_NAME] != null)
        {
            Helper.MFPApplication mfp = Application[Helper.MFPApplication.MFPAPP_NAME] as Helper.MFPApplication;

            mfp.Remove(new Helper.MFPApplicationItem(userinfo, deviceinfo, mfpjobid));

            Application[Helper.MFPApplication.MFPAPP_NAME] = mfp;
        }
    }
    #endregion

    #region "Create Job Item in Application for Simple EA."
    /// <summary>
    /// Create Job Item in Application for Simple EA.
    /// </summary>
    /// <param name="userinfo"></param>
    /// <param name="deviceinfo"></param>
    /// <param name="mfpjobid"></param>
    /// <Date>2010.07.30</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void CreateApplication(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, OSA_JOB_ID_TYPE mfpjobid)
    {

        // Add the Job Information into Application.
        if (Application[Helper.MFPApplication.MFPAPP_NAME] == null)
        {
            Helper.MFPApplication mfp = new Helper.MFPApplication();
            Helper.MFPApplicationItem mfpitem = new Helper.MFPApplicationItem(userinfo, deviceinfo, mfpjobid);
            mfp.AddMFP(mfpitem);
            Application.Add(Helper.MFPApplication.MFPAPP_NAME, mfp);
        }
        else
        {
            Helper.MFPApplication mfp = Application[Helper.MFPApplication.MFPAPP_NAME] as Helper.MFPApplication;
            // If the same job is exist in the Application, waiting for this job.
            if (!mfp.isExist(userinfo, deviceinfo, mfpjobid))
            {

                Helper.MFPApplicationItem mfpitem = new Helper.MFPApplicationItem(userinfo, deviceinfo, mfpjobid);
                mfp.AddMFP(mfpitem);
                Application[Helper.MFPApplication.MFPAPP_NAME] = mfp;
            }
        }
    }
    #endregion

    // 2011.03.22 Delete By SES Jijianxiong ST
    // Delete this process(Continuous operation Check).
    //#region "Job Check In Authorize for same job wait."
    ///// <summary>
    ///// Job Check In Authorize for same job wait.
    ///// </summary>
    ///// <param name="userinfo"></param>
    ///// <param name="deviceinfo"></param>
    ///// <param name="mfpjobid"></param>
    //private void CheckSameJob(CREDENTIALS_TYPE userinfo, DEVICE_INFO_TYPE deviceinfo, OSA_JOB_ID_TYPE mfpjobid)
    //{

    //    if (Application[Helper.MFPApplication.MFPAPP_NAME] != null)
    //    {
    //        Helper.MFPApplication mfp = Application[Helper.MFPApplication.MFPAPP_NAME] as Helper.MFPApplication;
    //        // If the same job is exist in the Application, waiting for this job.
    //        if (mfp.isExistJob(userinfo, deviceinfo, mfpjobid))
    //        {

    //            // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ST
    //            (new Helper.LogAccountant()).RecordUserSetLog(userinfo.accountid, deviceinfo, mfpjobid);
    //            // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ED


    //            Helper.DeviceSession.Get(deviceinfo.uuid).GetConfiguredMfpCoreWS().CancelJob(mfpjobid);


    //        }
    //    }

    //}
    //#endregion
    // 2011.03.22 Delete By SES Jijianxiong ED


    #region "Get Accountid from [MFPAPP_NAME] and set into event.userinfo "
    /// <summary>
    /// Get Accountid from [MFPAPP_NAME] and set into event.userinfo 
    /// </summary>
    /// <param name="userinfo"></param>
    /// <Date>2010.11.27</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.1</Version>
    private void SetAccountId(string strserialNumber, CREDENTIALS_TYPE userinfo, OSA_JOB_ID_TYPE mfpjobid)
    {
        bool isNeedSetValue = false;

        if (string.IsNullOrEmpty(userinfo.accountid))
        {
        }
        // Only for the OSA 2.0
        // Only for the accountid is not the int
        int intAccountid = UtilConst.USER_UNKNOW;
        try
        {

            if (string.IsNullOrEmpty(userinfo.accountid))
            {
                isNeedSetValue = true;
            }

            intAccountid = Convert.ToInt32(userinfo.accountid);

        }
        catch
        {

            isNeedSetValue = true;
        }

        if (intAccountid.Equals(UtilConst.USER_UNKNOW))
        {
            isNeedSetValue = true;
        }

        if (!isNeedSetValue) return;


        if (Application[Helper.MFPApplication.MFPAPP_NAME] != null)
        {
            Helper.MFPApplication mfp = Application[Helper.MFPApplication.MFPAPP_NAME] as Helper.MFPApplication;

            intAccountid = mfp.GetAccountIdOnExistJob(mfpjobid);
        }
        else
        {
            intAccountid = UtilConst.USER_UNKNOW;
        }

        userinfo.accountid = intAccountid.ToString();
        // 2010.12.21 Add By SES Jijianxong ST
        //Application["loggedinuser"] = userinfo.accountid;
        Application[strserialNumber] = userinfo.accountid;
        // 2010.12.21 Add By SES Jijianxong ED
    }

    #endregion

    //[System.Web.Services.WebMethodAttribute()]
    //[System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:schemas-sc-jp:mfp:osa-1-1/GetUserInfo", RequestNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", ResponseNamespace = "urn:schemas-sc-jp:mfp:osa-1-1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    //[return: System.Xml.Serialization.XmlElementAttribute("user-info")]
    //public CREDENTIALS_TYPE GetUserInfo(GETUSERINFO_ARG_TYPE arg,
    //    [System.Xml.Serialization.XmlAttributeAttribute()] ref string generic,
    //    [System.Xml.Serialization.XmlAttributeAttribute("product-family", DataType = "positiveInteger")] string productfamily,
    //    [System.Xml.Serialization.XmlAttributeAttribute("product-version")] string productversion,
    //    [System.Xml.Serialization.XmlAttributeAttribute("operation-version")] string operationversion)
    //{
    //    //Implementation for GetUserInfo()
    //    if (null != Application["loggedinuser"])
    //    {
            //string strAccountId = Application["loggedinuser"].ToString();
            //string strUserName = "";

            //if (string.IsNullOrEmpty(strAccountId))
            //{
            //    Application["loggedinuser"] = UtilConst.USER_UNKNOW.ToString();
            //    strAccountId = UtilConst.USER_UNKNOW.ToString();
            //    strUserName = "未知用户";
            //}
            //else if (strAccountId.Equals(UtilConst.USER_UNKNOW.ToString()))
            //{
            //    strAccountId = UtilConst.USER_UNKNOW.ToString();
            //    strUserName = "未知用户";
            //}
            //else
            //{
            //    int intAccountId = UtilConst.USER_UNKNOW;
            //    try
            //    {
            //        intAccountId = Convert.ToInt32(strAccountId);
            //    }
            //    catch (Exception)
            //    {

            //        intAccountId = UtilConst.USER_UNKNOW;
            //        Application["loggedinuser"] = UtilConst.USER_UNKNOW.ToString();
            //        strAccountId = UtilConst.USER_UNKNOW.ToString();
            //    }

            //    dtUserInfo.UserInfoDataTable userinfo = null;
            //    try
            //    {
            //        dtUserInfoTableAdapters.UserInfoTableAdapter adapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
            //        userinfo = adapter.GetDataByUserId(intAccountId);
            //    }
            //    catch (Exception)
            //    {

            //        return null;
            //    }
            //    if (userinfo == null || userinfo.Count == 0)
            //    {
            //        strUserName = "未知用户";
            //    }
            //    else
            //    {
            //        dtUserInfo.UserInfoRow userrow = userinfo.Rows[0] as dtUserInfo.UserInfoRow;
            //        strUserName = userrow.UserName;
            //    }
            //}

            //CREDENTIALS_TYPE xmlCred = new CREDENTIALS_TYPE();
            //xmlCred.accountid = Application["loggedinuser"].ToString();



            //xmlCred.metadata = new OPAQUE_DATA_TYPE();
            //XmlElement[] elements = new XmlElement[2];

            //XmlDocument xmlDoc = new XmlDocument();
            //XmlElement element = xmlDoc.CreateElement("property");
            //XmlAttribute attrib = xmlDoc.CreateAttribute("sys-name");
            //attrib.Value = "login-name";
            //element.Attributes.Append(attrib);
            //element.InnerText = xmlCred.accountid;
            //elements[0] = element;

            //element = xmlDoc.CreateElement("property");
            //attrib = xmlDoc.CreateAttribute("sys-name");
            //attrib.Value = "display-name";
            //element.Attributes.Append(attrib);
            //element.InnerText = strUserName;
            //elements[1] = element;

            //xmlCred.metadata.Any = elements;
            //return xmlCred;
    //    }
    //    return null;
    //}

}
