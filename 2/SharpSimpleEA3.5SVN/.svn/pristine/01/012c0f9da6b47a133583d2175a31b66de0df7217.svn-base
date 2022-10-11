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
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Security;
using System.Diagnostics;
using Osa.MfpWebService;
using Osa.Util;
using DAL;
using Model;
/// <summary>
/// Summary of UserManager 
/// </summary>
public class UserManager
{
    private dtRestrictionInformation.RestrictionInformationDataTable resdatatable = null;
    private dtJobInformation.JobInformationDataTable jobdatatable = null;
    private dtUserLimit.UserLimitDataTable userlimittable = null;
    //chen add 20140426 for money manager start
    private dtRestrictionInfo.RestrictionInfoDataTable resmoneydatatable = null;
    //private dtUserInfo.UserInfoDataTable userdatatable = null;
    private dtUserPayDetail.UserPayDetailDataTable userPaydatatable = null;
    //chen add 20140426 for money manager end

	public UserManager()
	{
	}

    #region "Get The Defulat Screen."
    /// <summary>
    /// Get The Defulat Screen.
    /// </summary>
    /// <param name="accid"></param>
    /// <returns></returns>
    public LoginScreenType GetLoginScreen(string accid)
    {
        LoginScreenType tyLoginSc = new LoginScreenType();

        return tyLoginSc;
    }
    #endregion


    #region "Get User's ACL which is based with baseAcl"
    /// <summary>
    /// Get User's ACL which is based with baseAcl
    /// </summary>
    /// <param name="accid"></param>
    /// <param name="baseAcl"></param>
    /// <returns></returns>
    /// <Date>2010.07.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public ACL_DOC_TYPE GetUserACL(string accid, ACL_DOC_TYPE baseAcl)
    {

        ACL_DOC_TYPE useracl = null;

        // 1.Create New ACL_DOC_TYPE
        useracl = new ACL_DOC_TYPE();

        // 1.1.Get User's RestrictionInformation

        if (resdatatable == null)
        {
            resdatatable = UtilCommon.GetUserLimitsFromDB(int.Parse(accid));
        }


        // 2.Set UserInfo to UserAcl
        useracl.userinfo = new CREDENTIALS_BASE_TYPE();
        useracl.userinfo.accountid = accid;

        // 3.Set Mfpfeature
        // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ST
        // For the MX-M35x / M450
        if (baseAcl.mfpfeature == null)
        {
            useracl.mfpfeature = null;
            return useracl;
        }
        // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ED
        useracl.mfpfeature = new ACL_MAIN_FEATURE_TYPE[baseAcl.mfpfeature.Length];

        // 3.1 Review all items in the baseAcl, Copy to userAcl And Control it.
        for (int k = 0; k < baseAcl.mfpfeature.Length; k++)
        {
            // mfpfeature
            useracl.mfpfeature[k] = new ACL_MAIN_FEATURE_TYPE();

            // sys-name
            useracl.mfpfeature[k].sysname = baseAcl.mfpfeature[k].sysname;


            // sub-feature
            if ( baseAcl.mfpfeature[k].subfeature == null ) {
                baseAcl.mfpfeature[k].subfeature = null;
            } else {
                useracl.mfpfeature[k].subfeature = new ACL_SUB_FEATURE_TYPE[baseAcl.mfpfeature[k].subfeature.Length];

                for (int intsubfeature = 0; intsubfeature < baseAcl.mfpfeature[k].subfeature.Length; intsubfeature++)
                {
                    ACL_SUB_FEATURE_TYPE useracl_sub_feature = new ACL_SUB_FEATURE_TYPE();
                    useracl_sub_feature.sysname = baseAcl.mfpfeature[k].subfeature[intsubfeature].sysname;
                    useracl_sub_feature.allow = true;

                    // sub-feature's details
                    if (baseAcl.mfpfeature[k].subfeature[intsubfeature].details == null)
                    {
                        useracl_sub_feature.details = null;
                    }
                    else
                    {
                        useracl_sub_feature.details = new ACL_DETAIL_TYPE[baseAcl.mfpfeature[k].subfeature[intsubfeature].details.Length];
                        for (int intdetail = 0; intdetail < baseAcl.mfpfeature[k].subfeature[intsubfeature].details.Length; intdetail++)
                        {
                            ACL_DETAIL_TYPE useracl_detail = new ACL_DETAIL_TYPE();
                            useracl_detail.sysname = baseAcl.mfpfeature[k].subfeature[intsubfeature].details[intdetail].sysname;
                            useracl_detail.Value = true;
                            // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ST
                            if ("OSA-SETTINGS".Equals(useracl_detail.sysname))
                            {
                                useracl_detail.Value = false;
                            }
                            if (baseAcl.mfpfeature[k].sysname == "SETTINGS" & baseAcl.mfpfeature[k].subfeature[intsubfeature].sysname == "SYSTEM-SETTINGS" &
                                    (useracl_detail.sysname == "USER-CONTROL" ||
                                     useracl_detail.sysname == "ENERGY-SAVE" ||
                                     useracl_detail.sysname == "OPERATION-SETTINGS" ||
                                     useracl_detail.sysname == "DEVICE-SETTINGS" ||
                                     useracl_detail.sysname == "COPY-SETTINGS" ||
                                     useracl_detail.sysname == "NETWORK-SETTINGS" ||
                                     useracl_detail.sysname == "PRINTER-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-OPERATION-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-SCANNER-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-IFAX-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-FAX-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-FAX2-SETTINGS" ||
                                     useracl_detail.sysname == "DOC-FILING-SETTINGS" ||
                                     useracl_detail.sysname == "LIST-PRINT-ADMIN" ||
                                     useracl_detail.sysname == "SECURITY-SETTINGS" ||
                                     useracl_detail.sysname == "PRODUCT-KEY" ||
                                     useracl_detail.sysname == "ESCP-SETTINGS" ||
                                     useracl_detail.sysname == "ENABLE-DISABLE-SETTINGS" ||
                                     useracl_detail.sysname == "FIELD-SUPPORT-SYSTEM" ||
                                     useracl_detail.sysname == "CHANGE-ADMIN-PASSWORD" ||
                                    useracl_detail.sysname == "RETENTION-CALLING"
                                ))
                            {
                                useracl_detail.Value = false;
                            }
                            else if (baseAcl.mfpfeature[k].sysname == "SETTINGS" & baseAcl.mfpfeature[k].subfeature[intsubfeature].sysname == "WEB-SETTINGS" &
                                (useracl_detail.sysname == "POWER-RESET" ||
                                 useracl_detail.sysname == "MACHINE-ID" ||
                                 useracl_detail.sysname == "APPLICATION-SETTINGS" ||
                                 useracl_detail.sysname == "REGISTER-PRE-SET-TEXT" ||
                                 useracl_detail.sysname == "EMAIL-ALERT" ||
                                 useracl_detail.sysname == "JOB-LOG" ||
                                 useracl_detail.sysname == "PORT-SETTINGS" ||
                                 useracl_detail.sysname == "STORAGE-BACKUP" ||
                                 useracl_detail.sysname == "CUSTOM-LINKS" ||
                                 useracl_detail.sysname == "OPERATION-MANUAL-DOWNLOAD" ||
                                 useracl_detail.sysname == "NETWORK-SETTINGS"
                            ))
                            {
                                useracl_detail.Value = false;
                            }
                            // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ED

                            useracl_sub_feature.details[intdetail] = useracl_detail;
                        }
                    }
                    useracl.mfpfeature[k].subfeature[intsubfeature] = useracl_sub_feature;
                }
            }



            // setting-constraints
            if (baseAcl.mfpfeature[k].settingconstraints == null)
            {
                useracl.mfpfeature[k].settingconstraints = null;
            }
            else
            {
                useracl.mfpfeature[k].settingconstraints = new ACL_PROPERTY_TYPE[baseAcl.mfpfeature[k].settingconstraints.Length];
                for (int intSettingconstraints = 0; intSettingconstraints < baseAcl.mfpfeature[k].settingconstraints.Length; intSettingconstraints++)
                {
                    ACL_PROPERTY_TYPE useracl_property = new ACL_PROPERTY_TYPE();
                    useracl_property.sysname = baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].sysname;

                    //setting-constraints's setting
                    // 2010.11.18 Update By SES Jijianxiong Ver.1.1 Update ST
                    //useracl_property.setting = new ACL_SETTING_TYPE[baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting.Length];
                    //for (int intsetting = 0; intsetting < baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting.Length; intsetting++)
                    //{
                    //    ACL_SETTING_TYPE useracl_setting = new ACL_SETTING_TYPE();
                    //    useracl_setting.sysname = baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting[intsetting].sysname;
                    //    useracl_setting.Value = true;
                    //    useracl_property.setting[intsetting] = useracl_setting;
                    //}
                    if (baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting != null)
                    {
                        useracl_property.setting = new ACL_SETTING_TYPE[baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting.Length];
                        for (int intsetting = 0; intsetting < baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting.Length; intsetting++)
                        {
                            ACL_SETTING_TYPE useracl_setting = new ACL_SETTING_TYPE();
                            useracl_setting.sysname = baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting[intsetting].sysname;
                            useracl_setting.Value = true;
                            useracl_property.setting[intsetting] = useracl_setting;
                        }
                    }
                    else
                    {
                        useracl_property.setting = null;
                    }
                    // 2010.11.18 Update By SES Jijianxiong Ver.1.1 Update ED

                    useracl.mfpfeature[k].settingconstraints[intSettingconstraints] = useracl_property;
                }
            }

            // allow 
            useracl.mfpfeature[k].allow = baseAcl.mfpfeature[k].allow;
            // Set All Item to Allow
            useracl.mfpfeature[k].allow = true;

            // Get it's JobId
            useracl.mfpfeature[k] = GetUserMainFeature(useracl.mfpfeature[k], resdatatable);

        }

        return useracl;
    }
    #endregion


    #region "Get User's ACL which is based with baseAcl"
    /// <summary>
    /// Get User's ACL which is based with baseAcl
    /// </summary>
    /// <param name="accid"></param>
    /// <param name="baseAcl"></param>
    /// <returns></returns>
    /// <Date>2018.02.9</Date>
    /// <Author>chen</Author>
    /// <Version>0.01</Version>
    public ACL_DOC_TYPE GetUserACL(string sn, string accid, ACL_DOC_TYPE baseAcl)
    {

        ACL_DOC_TYPE useracl = null;


        // 1.Create New ACL_DOC_TYPE
        useracl = new ACL_DOC_TYPE();

        // 1.1.Get User's RestrictionInformation

        if (resdatatable == null)
        {
            resdatatable = UtilCommon.GetUserLimitsFromDB(int.Parse(accid));
        }


        // 2.Set UserInfo to UserAcl
        useracl.userinfo = new CREDENTIALS_BASE_TYPE();
        useracl.userinfo.accountid = accid;

        // 3.Set Mfpfeature
        // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ST
        // For the MX-M35x / M450
        if (baseAcl.mfpfeature == null)
        {
            useracl.mfpfeature = null;
            return useracl;
        }
        // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ED
        useracl.mfpfeature = new ACL_MAIN_FEATURE_TYPE[baseAcl.mfpfeature.Length];

        // 3.1 Review all items in the baseAcl, Copy to userAcl And Control it.
        for (int k = 0; k < baseAcl.mfpfeature.Length; k++)
        {
            // mfpfeature
            useracl.mfpfeature[k] = new ACL_MAIN_FEATURE_TYPE();

            // sys-name
            useracl.mfpfeature[k].sysname = baseAcl.mfpfeature[k].sysname;


            // sub-feature
            if (baseAcl.mfpfeature[k].subfeature == null)
            {
                baseAcl.mfpfeature[k].subfeature = null;
            }
            else
            {
                useracl.mfpfeature[k].subfeature = new ACL_SUB_FEATURE_TYPE[baseAcl.mfpfeature[k].subfeature.Length];

                for (int intsubfeature = 0; intsubfeature < baseAcl.mfpfeature[k].subfeature.Length; intsubfeature++)
                {
                    ACL_SUB_FEATURE_TYPE useracl_sub_feature = new ACL_SUB_FEATURE_TYPE();
                    useracl_sub_feature.sysname = baseAcl.mfpfeature[k].subfeature[intsubfeature].sysname;
                    useracl_sub_feature.allow = true;

                    // sub-feature's details
                    if (baseAcl.mfpfeature[k].subfeature[intsubfeature].details == null)
                    {
                        useracl_sub_feature.details = null;
                    }
                    else
                    {
                        useracl_sub_feature.details = new ACL_DETAIL_TYPE[baseAcl.mfpfeature[k].subfeature[intsubfeature].details.Length];
                        for (int intdetail = 0; intdetail < baseAcl.mfpfeature[k].subfeature[intsubfeature].details.Length; intdetail++)
                        {
                            ACL_DETAIL_TYPE useracl_detail = new ACL_DETAIL_TYPE();
                            useracl_detail.sysname = baseAcl.mfpfeature[k].subfeature[intsubfeature].details[intdetail].sysname;
                            useracl_detail.Value = true;
                            // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ST
                            if ("OSA-SETTINGS".Equals(useracl_detail.sysname))
                            {
                                useracl_detail.Value = false;
                            }
                            if (baseAcl.mfpfeature[k].sysname == "SETTINGS" & baseAcl.mfpfeature[k].subfeature[intsubfeature].sysname == "SYSTEM-SETTINGS" &
                                    (useracl_detail.sysname == "USER-CONTROL" ||
                                     useracl_detail.sysname == "ENERGY-SAVE" ||
                                     useracl_detail.sysname == "OPERATION-SETTINGS" ||
                                     useracl_detail.sysname == "DEVICE-SETTINGS" ||
                                     useracl_detail.sysname == "COPY-SETTINGS" ||
                                     useracl_detail.sysname == "NETWORK-SETTINGS" ||
                                     useracl_detail.sysname == "PRINTER-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-OPERATION-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-SCANNER-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-IFAX-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-FAX-SETTINGS" ||
                                     useracl_detail.sysname == "IMAGESEND-FAX2-SETTINGS" ||
                                     useracl_detail.sysname == "DOC-FILING-SETTINGS" ||
                                     useracl_detail.sysname == "LIST-PRINT-ADMIN" ||
                                     useracl_detail.sysname == "SECURITY-SETTINGS" ||
                                     useracl_detail.sysname == "PRODUCT-KEY" ||
                                     useracl_detail.sysname == "ESCP-SETTINGS" ||
                                     useracl_detail.sysname == "ENABLE-DISABLE-SETTINGS" ||
                                     useracl_detail.sysname == "FIELD-SUPPORT-SYSTEM" ||
                                     useracl_detail.sysname == "CHANGE-ADMIN-PASSWORD" ||
                                    useracl_detail.sysname == "RETENTION-CALLING"
                                ))
                            {
                                useracl_detail.Value = false;
                            }
                            else if (baseAcl.mfpfeature[k].sysname == "SETTINGS" & baseAcl.mfpfeature[k].subfeature[intsubfeature].sysname == "WEB-SETTINGS" &
                                (useracl_detail.sysname == "POWER-RESET" ||
                                 useracl_detail.sysname == "MACHINE-ID" ||
                                 useracl_detail.sysname == "APPLICATION-SETTINGS" ||
                                 useracl_detail.sysname == "REGISTER-PRE-SET-TEXT" ||
                                 useracl_detail.sysname == "EMAIL-ALERT" ||
                                 useracl_detail.sysname == "JOB-LOG" ||
                                 useracl_detail.sysname == "PORT-SETTINGS" ||
                                 useracl_detail.sysname == "STORAGE-BACKUP" ||
                                 useracl_detail.sysname == "CUSTOM-LINKS" ||
                                 useracl_detail.sysname == "OPERATION-MANUAL-DOWNLOAD" ||
                                 useracl_detail.sysname == "NETWORK-SETTINGS"
                            ))
                            {
                                useracl_detail.Value = false;
                            }
                            // 2010.11.18 Add By SES Jijianxiong Ver.1.1 Update ED

                            useracl_sub_feature.details[intdetail] = useracl_detail;
                        }
                    }
                    useracl.mfpfeature[k].subfeature[intsubfeature] = useracl_sub_feature;
                }
            }



            // setting-constraints
            if (baseAcl.mfpfeature[k].settingconstraints == null)
            {
                useracl.mfpfeature[k].settingconstraints = null;
            }
            else
            {
                useracl.mfpfeature[k].settingconstraints = new ACL_PROPERTY_TYPE[baseAcl.mfpfeature[k].settingconstraints.Length];
                for (int intSettingconstraints = 0; intSettingconstraints < baseAcl.mfpfeature[k].settingconstraints.Length; intSettingconstraints++)
                {
                    ACL_PROPERTY_TYPE useracl_property = new ACL_PROPERTY_TYPE();
                    useracl_property.sysname = baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].sysname;

                    //setting-constraints's setting
                    // 2010.11.18 Update By SES Jijianxiong Ver.1.1 Update ST
                    //useracl_property.setting = new ACL_SETTING_TYPE[baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting.Length];
                    //for (int intsetting = 0; intsetting < baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting.Length; intsetting++)
                    //{
                    //    ACL_SETTING_TYPE useracl_setting = new ACL_SETTING_TYPE();
                    //    useracl_setting.sysname = baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting[intsetting].sysname;
                    //    useracl_setting.Value = true;
                    //    useracl_property.setting[intsetting] = useracl_setting;
                    //}
                    if (baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting != null)
                    {
                        useracl_property.setting = new ACL_SETTING_TYPE[baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting.Length];
                        for (int intsetting = 0; intsetting < baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting.Length; intsetting++)
                        {
                            ACL_SETTING_TYPE useracl_setting = new ACL_SETTING_TYPE();
                            useracl_setting.sysname = baseAcl.mfpfeature[k].settingconstraints[intSettingconstraints].setting[intsetting].sysname;
                            useracl_setting.Value = true;
                            useracl_property.setting[intsetting] = useracl_setting;
                        }
                    }
                    else
                    {
                        useracl_property.setting = null;
                    }
                    // 2010.11.18 Update By SES Jijianxiong Ver.1.1 Update ED

                    useracl.mfpfeature[k].settingconstraints[intSettingconstraints] = useracl_property;
                }
            }

            // allow 
            useracl.mfpfeature[k].allow = baseAcl.mfpfeature[k].allow;
            // Set All Item to Allow
            useracl.mfpfeature[k].allow = true;

            // Get it's JobId
            useracl.mfpfeature[k] = GetUserMainFeature(useracl.mfpfeature[k], resdatatable, sn);

        }

        return useracl;
    }
    #endregion


    #region "Get User's Acl limit for each item"
    /// <summary>
    /// Get User's Acl limit for each item
    /// </summary>
    /// <param name="user_main_feature"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    private ACL_MAIN_FEATURE_TYPE GetUserMainFeature(ACL_MAIN_FEATURE_TYPE user_main_feature , dtRestrictionInformation.RestrictionInformationDataTable dt)
    {
        Boolean bolMONOCHROME = false;
        Boolean bolFULLCOLOR = false;

        // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
        ACL_SETTING_TYPE settingBW = null;

        bool canBorrow = false;
        //chen 20140605 delete  start
        //if (UtilCommon.GetDispSetting().Dis_Avai_Borrow.Equals(1))
        //{
        //    // While can borrow from the full color mode.
        //    canBorrow = true;
        //}
        //chen 20140605 delete  end
        // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED

        switch (user_main_feature.sysname)
        {
            //COPY
            case "COPY":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if ( setting.sysname.Equals("MONOCHROME") ) {
                                    setting.Value = false;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                                    settingBW = setting;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
                else
                {
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                    // if can borrow from fullcolor mode , then 
                    if (canBorrow)
                    {
                        if (settingBW != null)
                        {
                            settingBW.Value = true;
                        }
                    }
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                }


                break;

            //PRINT
            case "PRINT":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                                    settingBW = setting;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
                else
                {
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                    // if can borrow from fullcolor mode , then 
                    if (canBorrow)
                    {
                        if (settingBW != null)
                        {
                            settingBW.Value = true;
                        }
                    }
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                }


                break;
            //SCAN-TO-HDD
            case "SCAN-TO-HDD":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                                    settingBW = setting;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
                else
                {
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                    // if can borrow from fullcolor mode , then 
                    if (canBorrow)
                    {
                        if (settingBW != null)
                        {
                            settingBW.Value = true;
                        }
                    }
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                }

                break;

            //DOC-FILING-PRINT
            case "DOC-FILING-PRINT":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                                    settingBW = setting;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
                else
                {
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                    // if can borrow from fullcolor mode , then 
                    if (canBorrow)
                    {
                        if (settingBW != null)
                        {
                            settingBW.Value = true;
                        }
                    }
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                }

                break;
            //DOC-FILING-PRINT
            case "IMAGE-SEND":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;
                                    
                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
               
                break;
            case "SCAN-TO-EXTERNAL-MEMORY":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;

                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }

                break;
            default:
                // COMMON
                // SHARP-OSA
                // SETTINGS
                break;
        }

        return user_main_feature;
 
    }
    #endregion

    #region "Get User's Acl limit for each item"
    /// <summary>
    /// Get User's Acl limit for each item
    /// </summary>
    /// <param name="user_main_feature"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    private ACL_MAIN_FEATURE_TYPE GetUserMainFeature(ACL_MAIN_FEATURE_TYPE user_main_feature, dtRestrictionInformation.RestrictionInformationDataTable dt, string sn)
    {
        Boolean bolMONOCHROME = false;
        Boolean bolFULLCOLOR = false;

        // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
        ACL_SETTING_TYPE settingBW = null;

        bool canBorrow = false;
        //chen 20140605 delete  start
        //if (UtilCommon.GetDispSetting().Dis_Avai_Borrow.Equals(1))
        //{
        //    // While can borrow from the full color mode.
        //    canBorrow = true;
        //}
        //chen 20140605 delete  end
        // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED



        //2018-01-16 start 当复印留底时，设定复印不可用
        //DalMFP mfp = new DalMFP();
        //MFPEntry bean = mfp.GetInfoByKey(sn);
        //Boolean copyFunFlg = true;
        //if (bean.Monitor == 2 || bean.Monitor == 3)
        //{
        //    copyFunFlg = false;
        //}
        //2018-01-16 end 当复印留底时，设定复印不可用

        switch (user_main_feature.sysname)
        {
            //COPY
            case "COPY":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                                    settingBW = setting;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
                else
                {
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                    // if can borrow from fullcolor mode , then 
                    if (canBorrow)
                    {
                        if (settingBW != null)
                        {
                            settingBW.Value = true;
                        }
                    }
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                }


                break;

            //PRINT
            case "PRINT":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                                    settingBW = setting;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
                else
                {
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                    // if can borrow from fullcolor mode , then 
                    if (canBorrow)
                    {
                        if (settingBW != null)
                        {
                            settingBW.Value = true;
                        }
                    }
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                }


                break;
            //SCAN-TO-HDD
            case "SCAN-TO-HDD":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                                    settingBW = setting;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                                }
                            }

                        }
                    }

                }
                else
                {
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                    // if can borrow from fullcolor mode , then 
                    if (canBorrow)
                    {
                        if (settingBW != null)
                        {
                            settingBW.Value = true;
                        }
                    }
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                }
                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
                else
                {
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                    // if can borrow from fullcolor mode , then 
                    if (canBorrow)
                    {
                        if (settingBW != null)
                        {
                            settingBW.Value = true;
                        }
                    }
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                }

                break;

            //DOC-FILING-PRINT
            case "DOC-FILING-PRINT":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                                    settingBW = setting;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
                else
                {
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                    // if can borrow from fullcolor mode , then 
                    if (canBorrow)
                    {
                        if (settingBW != null)
                        {
                            settingBW.Value = true;
                        }
                    }
                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                }

                break;
            //DOC-FILING-PRINT
            case "IMAGE-SEND":

                    bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ST
                                    settingBW = setting;
                                    // 2010.12.14 Add By SES Jijianxiong Ver.1.1 Update ED
                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }
               
                break;
            case "SCAN-TO-EXTERNAL-MEMORY":
                // Get the restriction set about COPY mode
                // MONOCHROME
                bolMONOCHROME = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
                if (bolMONOCHROME)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("MONOCHROME"))
                                {
                                    setting.Value = false;

                                }
                            }

                        }
                    }

                }

                // FULLCOLOR
                bolFULLCOLOR = !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2) &&
                    !UtilCommon.GetResByJobAndFun(dt, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
                if (bolFULLCOLOR)
                {
                    // Config color-mode
                    foreach (ACL_PROPERTY_TYPE settingconstraints in user_main_feature.settingconstraints)
                    {
                        // Get color-mode
                        if (settingconstraints.sysname.Equals("color-mode"))
                        {
                            foreach (ACL_SETTING_TYPE setting in settingconstraints.setting)
                            {
                                if (setting.sysname.Equals("FULL-COLOR"))
                                {
                                    setting.Value = false;
                                }
                            }

                        }
                    }

                }

                break;
            default:
                // COMMON
                // SHARP-OSA
                // SETTINGS
                break;
        }

        return user_main_feature;

    }
    #endregion

    #region "Get User's LIMITS_TYPE which is based with baseLIMITS_TYPE"
    /// <summary>
    /// Get User's ACL which is based with baseAcl
    /// </summary>
    /// <param name="accid"></param>
    /// <param name="baseLimits"></param>
    /// <returns></returns>
    /// <Date>2014.04.28</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public LIMITS_TYPE[] GetUserAbleLimitsByMoney(string accid, string serialnumber, LIMITS_TYPE[] baseLimits)
    {
        LIMITS_TYPE[] userlimits = null;

        // 获得用户配额ID
        int restrictid = UtilCommon.GetUserRestrictidFromDB(int.Parse(accid));
    
        
        //管理员的场合，即accid是0   或   限制不存在
        if (accid.Equals("0") || restrictid == -1)
        {
            //获得用户的限额table，根据用户ID
            resmoneydatatable = UtilCommon.GetUserResmoneyLimitsFromDB(0);
            //获得不同限制，即限制ID为0的数据
            resdatatable = UtilCommon.ResSpecialProcess();
        }
        else
        {
            //获得用户的限额table，根据配额ID
            resmoneydatatable = UtilCommon.GetUserResmoneyLimitsFromDB(restrictid);
            //获得用户的限制table，根据配额ID
            //resdatatable = UtilCommon.GetUserLimitsFromDB(restrictid);
            resdatatable = UtilCommon.GetUserResLimitsFromDB(restrictid);
            
        }

        //if (userdatatable == null)
        //{
        //    //获得用户追加余额table，根据用户ID
        //    userdatatable = UtilCommon.GetUserInfoFromDB(int.Parse(accid));
        //}
        if (jobdatatable == null)
        {
            jobdatatable = UtilCommon.GetUserJobInfoFromDB(int.Parse(accid));
        }

        if (userPaydatatable == null)
        {
            userPaydatatable = UtilCommon.GetUserPayDataFromDB(int.Parse(accid));
        }

        if ( userlimittable == null)
        {
            //用户限制表
            userlimittable = UtilCommon.GetUserLimitByMoney(resmoneydatatable, resdatatable, jobdatatable, userPaydatatable, serialnumber);
        }
        

        userlimits = new LIMITS_TYPE[baseLimits.Length];

        for (int i = 0; i < baseLimits.Length; i++)
        {

            UtilCommon.MFPJob mfpjob = new UtilCommon.MFPJob(-1, -1);

            LIMITS_TYPE limits = new LIMITS_TYPE();
            limits.sysname = baseLimits[i].sysname;

            if ( baseLimits[i].property == null ) {
                limits.property = null;
            } else {
                limits.property = new PROPERTY_LIMIT_TYPE[baseLimits[i].property.Length];

                for (int p = 0 ; p < limits.property.Length ;p++ ) {
                    PROPERTY_LIMIT_TYPE property = new PROPERTY_LIMIT_TYPE();
                    property.sysname = baseLimits[i].property[p].sysname;

                    if (baseLimits[i].property[p].limit == null)
                    {
                        property.limit = null;
                    }
                    else
                    {
                        property.limit = new LIMIT_TYPE[baseLimits[i].property[p].limit.Length];

                        for (int l = 0; l < baseLimits[i].property[p].limit.Length; l++)
                        {
                            if (property.sysname.Equals("color-mode"))
                            {
                                mfpjob = GetMFPJobInfoByLimitsType(baseLimits[i], baseLimits[i].property[p].limit[l]);
                            } else {
                                mfpjob = new UtilCommon.MFPJob(-1,-1);
                            }

                            LIMIT_TYPE limit_type = new LIMIT_TYPE();
                            limit_type.sysname = baseLimits[i].property[p].limit[l].sysname;
                            // 2010.12.13 Update By SES Jijianxiong Ver.1.1 Update ST
                            // SimpleEA_V0.19_20101210.doc: Setting Item Add
                            ////// 2010.12.09 Update By SES Jijianxiong Ver.1.1 Update ST
                            ////// Cost Solution
                            ////// limit_type.Value = UtilCommon.GetUserLimitNum(resdatatable, jobdatatable, mfpjob.JOBId, mfpjob.FunctionId).ToString();
                            ////limit_type.Value = UtilCommon.GetUserLimitNum(userlimittable, mfpjob.JOBId, Convert.ToInt32(mfpjob.FunctionId)).ToString();
                            ////// 2010.12.09 Update By SES Jijianxiong Ver.1.1 Update ED
                            int intFuncId = -1;
                            if (mfpjob.FunctionId != null)
                            {
                                intFuncId = Convert.ToInt32(mfpjob.FunctionId);
                            }

                            //if (canBorrow)
                            //{
                            //    // can borrow
                            //    //设定限制值利用用户限制表
                            //    int limitValue = UtilCommon.GetUserLimitNum(userlimittable, mfpjob.JOBId, intFuncId);
                            //    if (limitValue < 0)
                            //    {
                            //        limitValue = 0;
                            //    }
                            //    limit_type.Value = limitValue.ToString();
                            //}
                            //else
                            //{
                            //    // can not borrow
                            //    //设定限制值利用限制表和用户JOB信息表
                            //    int limitValue = UtilCommon.GetUserLimitNum(resdatatable, jobdatatable, mfpjob.JOBId, intFuncId);
                            //    if (limitValue < 0)
                            //    {
                            //        limitValue = 0;
                            //    }
                            //    limit_type.Value = limitValue.ToString();
                            //}
                            //设定限制值利用限制表和用户JOB信息表
                            int limitValue = UtilCommon.GetUserLimitNum(userlimittable, mfpjob.JOBId, intFuncId);
                            if (limitValue < 0)
                            {
                                limitValue = 0;
                            }
                            limit_type.Value = limitValue.ToString();


                            // 2010.12.13 Update By SES Jijianxiong Ver.1.1 Update ED

                            property.limit[l] = limit_type;
                        }                        
                    }

                    limits.property[p] = property;
                }
            }
            userlimits[i] = limits;
        }
        return userlimits;
    }

    #endregion
    #region "Get User's LIMITS_TYPE which is based with base LIMITS_TYPE by function"
    /// <summary>
    /// Get User's Limits which is based with baseAcl
    /// </summary>
    /// <param name="accid"></param>
    /// <param name="baseLimits"></param>
    /// <returns></returns>
    /// <Date>2014.06.05</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public LIMITS_TYPE[] GetUserAbleLimitsByFunct(string accid, LIMITS_TYPE[] baseLimits)
    {
        LIMITS_TYPE[] userlimits = null;

        // 获得用户配额ID
        int restrictid = UtilCommon.GetUserRestrictidFromDB(int.Parse(accid));
        resdatatable = UtilCommon.GetUserResLimitsFromDB(restrictid);

         //用户限制表
        userlimittable = UtilCommon.GetUserLimitByFunct(resdatatable);

        userlimits = new LIMITS_TYPE[baseLimits.Length];

        for (int i = 0; i < baseLimits.Length; i++)
        {

            UtilCommon.MFPJob mfpjob = new UtilCommon.MFPJob(-1, -1);

            LIMITS_TYPE limits = new LIMITS_TYPE();
            limits.sysname = baseLimits[i].sysname;

            if (baseLimits[i].property == null)
            {
                limits.property = null;
            }
            else
            {
                limits.property = new PROPERTY_LIMIT_TYPE[baseLimits[i].property.Length];

                for (int p = 0; p < limits.property.Length; p++)
                {
                    PROPERTY_LIMIT_TYPE property = new PROPERTY_LIMIT_TYPE();
                    property.sysname = baseLimits[i].property[p].sysname;

                    if (baseLimits[i].property[p].limit == null)
                    {
                        property.limit = null;
                    }
                    else
                    {
                        property.limit = new LIMIT_TYPE[baseLimits[i].property[p].limit.Length];

                        for (int l = 0; l < baseLimits[i].property[p].limit.Length; l++)
                        {
                            if (property.sysname.Equals("color-mode"))
                            {
                                mfpjob = GetMFPJobInfoByLimitsType(baseLimits[i], baseLimits[i].property[p].limit[l]);
                            }
                            else
                            {
                                mfpjob = new UtilCommon.MFPJob(-1, -1);
                            }

                            LIMIT_TYPE limit_type = new LIMIT_TYPE();
                            limit_type.sysname = baseLimits[i].property[p].limit[l].sysname;
                            int intFuncId = -1;
                            if (mfpjob.FunctionId != null)
                            {
                                intFuncId = Convert.ToInt32(mfpjob.FunctionId);
                            }

                            //设定限制值利用限制表和用户JOB信息表
                            int limitValue = UtilCommon.GetUserLimitNum(userlimittable, mfpjob.JOBId, intFuncId);
                            if (limitValue < 0)
                            {
                                limitValue = 0;
                            }
                            limit_type.Value = limitValue.ToString();

                            property.limit[l] = limit_type;
                        }
                    }

                    limits.property[p] = property;
                }
            }
            userlimits[i] = limits;
        }

        resdatatable = null;
        userlimittable = null;


        return userlimits;
    }

    #endregion


    #region "Get User's LIMITS_TYPE which is based with base LIMITS_TYPE by function"
    /// <summary>
    /// Get User's Limits which is based with baseAcl
    /// </summary>
    /// <param name="accid"></param>
    /// <param name="baseLimits"></param>
    /// <returns></returns>
    /// <Date>2018.01.17</Date>
    /// <Author>SES chen youguang</Author>
    /// <Version>0.01</Version>
    public LIMITS_TYPE[] GetUserAbleLimitsByFunct(string sn, string accid, LIMITS_TYPE[] baseLimits)
    {
        LIMITS_TYPE[] userlimits = null;

        // 获得用户配额ID
        int restrictid = UtilCommon.GetUserRestrictidFromDB(int.Parse(accid));
        resdatatable = UtilCommon.GetUserResLimitsFromDB(restrictid);

        //用户限制表
        //userlimittable = UtilCommon.GetUserLimitByFunct(resdatatable);
        userlimittable = UtilCommon.GetUserLimitByFunct(sn, resdatatable);

        userlimits = new LIMITS_TYPE[baseLimits.Length];

        for (int i = 0; i < baseLimits.Length; i++)
        {

            UtilCommon.MFPJob mfpjob = new UtilCommon.MFPJob(-1, -1);

            LIMITS_TYPE limits = new LIMITS_TYPE();
            limits.sysname = baseLimits[i].sysname;

            if (baseLimits[i].property == null)
            {
                limits.property = null;
            }
            else
            {
                limits.property = new PROPERTY_LIMIT_TYPE[baseLimits[i].property.Length];

                for (int p = 0; p < limits.property.Length; p++)
                {
                    PROPERTY_LIMIT_TYPE property = new PROPERTY_LIMIT_TYPE();
                    property.sysname = baseLimits[i].property[p].sysname;

                    if (baseLimits[i].property[p].limit == null)
                    {
                        property.limit = null;
                    }
                    else
                    {
                        property.limit = new LIMIT_TYPE[baseLimits[i].property[p].limit.Length];

                        for (int l = 0; l < baseLimits[i].property[p].limit.Length; l++)
                        {
                            if (property.sysname.Equals("color-mode"))
                            {
                                mfpjob = GetMFPJobInfoByLimitsType(baseLimits[i], baseLimits[i].property[p].limit[l]);
                            }
                            else
                            {
                                mfpjob = new UtilCommon.MFPJob(-1, -1);
                            }

                            LIMIT_TYPE limit_type = new LIMIT_TYPE();
                            limit_type.sysname = baseLimits[i].property[p].limit[l].sysname;
                            int intFuncId = -1;
                            if (mfpjob.FunctionId != null)
                            {
                                intFuncId = Convert.ToInt32(mfpjob.FunctionId);
                            }

                            //设定限制值利用限制表和用户JOB信息表
                            int limitValue = UtilCommon.GetUserLimitNum(userlimittable, mfpjob.JOBId, intFuncId);
                            if (limitValue < 0)
                            {
                                limitValue = 0;
                            }
                            limit_type.Value = limitValue.ToString();

                            property.limit[l] = limit_type;
                        }
                    }

                    limits.property[p] = property;
                }
            }
            userlimits[i] = limits;
        }

        resdatatable = null;
        userlimittable = null;



        return userlimits;
    }

    #endregion


    #region "Get User's LIMITS_TYPE which is based with baseLIMITS_TYPE"
    /// <summary>
    /// Get User's ACL which is based with baseAcl
    /// </summary>
    /// <param name="accid"></param>
    /// <param name="baseLimits"></param>
    /// <returns></returns>
    /// <Date>2010.07.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public LIMITS_TYPE[] GetUserAbleLimits(string accid, LIMITS_TYPE[] baseLimits)
    {
        LIMITS_TYPE[] userlimits = null;
        // 1.1 Get user's restiction set in Simpl EA
        if (resdatatable == null)
        {
            resdatatable = UtilCommon.GetUserLimitsFromDB(int.Parse(accid));
        }

        // 2011.03.28 Add By SES jijianxiong ST
        // While:
        //  1. User's Res is null and Group's Res is null 
        //  Or 2. Admin User
        //  Set the res to no limit.
        if (accid.Equals("0") || resdatatable.Rows.Count == 0)
        {
            resdatatable = UtilCommon.ResSpecialProcess();
        }
        // 2011.03.28 Add By SES jijianxiong ED

        // 1.2 Get User's Job Information
        if (jobdatatable == null)
        {
            jobdatatable = UtilCommon.GetUserJobInfoFromDB(int.Parse(accid));
        }

        // 2010.12.09 Add By SES Jijianxiong Ver.1.1 Update ST
        // Cost Solution
        bool canBorrow = false;

        if (UtilCommon.GetDispSetting().Dis_Avai_Borrow == 1)
        {
            canBorrow = true;
        }
        // 1.3 Get User Limit Table
        if (canBorrow && userlimittable == null)
        {
            userlimittable = UtilCommon.GetUserLimit(resdatatable, jobdatatable);
        }
        // 2010.12.09 Add By SES Jijianxiong Ver.1.1 Update ED


        userlimits = new LIMITS_TYPE[baseLimits.Length];

        for (int i = 0; i < baseLimits.Length; i++)
        {

            UtilCommon.MFPJob mfpjob = new UtilCommon.MFPJob(-1, -1);

            LIMITS_TYPE limits = new LIMITS_TYPE();
            limits.sysname = baseLimits[i].sysname;

            if (baseLimits[i].property == null)
            {
                limits.property = null;
            }
            else
            {
                limits.property = new PROPERTY_LIMIT_TYPE[baseLimits[i].property.Length];

                for (int p = 0; p < limits.property.Length; p++)
                {
                    PROPERTY_LIMIT_TYPE property = new PROPERTY_LIMIT_TYPE();
                    property.sysname = baseLimits[i].property[p].sysname;

                    if (baseLimits[i].property[p].limit == null)
                    {
                        property.limit = null;
                    }
                    else
                    {
                        property.limit = new LIMIT_TYPE[baseLimits[i].property[p].limit.Length];

                        for (int l = 0; l < baseLimits[i].property[p].limit.Length; l++)
                        {
                            if (property.sysname.Equals("color-mode"))
                            {
                                mfpjob = GetMFPJobInfoByLimitsType(baseLimits[i], baseLimits[i].property[p].limit[l]);
                            }
                            else
                            {
                                mfpjob = new UtilCommon.MFPJob(-1, -1);
                            }

                            LIMIT_TYPE limit_type = new LIMIT_TYPE();
                            limit_type.sysname = baseLimits[i].property[p].limit[l].sysname;
                            // 2010.12.13 Update By SES Jijianxiong Ver.1.1 Update ST
                            // SimpleEA_V0.19_20101210.doc: Setting Item Add
                            ////// 2010.12.09 Update By SES Jijianxiong Ver.1.1 Update ST
                            ////// Cost Solution
                            ////// limit_type.Value = UtilCommon.GetUserLimitNum(resdatatable, jobdatatable, mfpjob.JOBId, mfpjob.FunctionId).ToString();
                            ////limit_type.Value = UtilCommon.GetUserLimitNum(userlimittable, mfpjob.JOBId, Convert.ToInt32(mfpjob.FunctionId)).ToString();
                            ////// 2010.12.09 Update By SES Jijianxiong Ver.1.1 Update ED
                            int intFuncId = -1;
                            if (mfpjob.FunctionId != null)
                            {
                                intFuncId = Convert.ToInt32(mfpjob.FunctionId);
                            }

                            if (canBorrow)
                            {
                                // can borrow
                                int limitValue = UtilCommon.GetUserLimitNum(userlimittable, mfpjob.JOBId, intFuncId);
                                if (limitValue < 0)
                                {
                                    limitValue = 0;
                                }
                                limit_type.Value = limitValue.ToString();
                            }
                            else
                            {
                                // can not borrow
                                int limitValue = UtilCommon.GetUserLimitNum(resdatatable, jobdatatable, mfpjob.JOBId, intFuncId);
                                if (limitValue < 0)
                                {
                                    limitValue = 0;
                                }
                                limit_type.Value = limitValue.ToString();
                            }


                            // 2010.12.13 Update By SES Jijianxiong Ver.1.1 Update ED

                            property.limit[l] = limit_type;
                        }
                    }

                    limits.property[p] = property;
                }
            }
            userlimits[i] = limits;
        }
        return userlimits;
    }

    #endregion

    #region "Get MFP Job Information From the LIMIT_TYPE's sysname."
    /// <summary>
    /// Get MFP Job Information From the LIMIT_TYPE's sysname.
    /// </summary>
    /// <param name="userlimits"></param>
    /// <param name="userlimit"></param>
    /// <returns></returns>
    /// <Date>2010.07.22</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private UtilCommon.MFPJob GetMFPJobInfoByLimitsType(LIMITS_TYPE userlimits , LIMIT_TYPE userlimit)
    {

        UtilCommon.MFPJob mfpjob = Helper.GetMFPJob(userlimits.sysname, userlimit.sysname);
        return mfpjob;
    }
    #endregion



}
