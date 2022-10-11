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
using SesMiddleware;
using System.Threading;

using dtServerIPSettingTableAdapters;
using dtUserInfoTableAdapters;
using Model;
using DAL;


/// <summary>
/// The BasePage For Simple EA Application OSA Screen.
/// </summary>
/// <Date>2010.06.07</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public class MainOSA : System.Web.UI.Page
{
    private string strserialNumber = string.Empty;
    public string div_error = "";

    public MainOSA()
    {
        //
        // TODO: Add constructor logic here
        //
        this.Load+=new EventHandler(MainOSA_Load);
    }

    #region "MainOSA_Load"
    /// <summary>
    /// MainOSA_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.10</Version>
    /// 
    //protected void MainOSA_Load(object sender, EventArgs e)
    protected void MainOSA_Load(object sender, EventArgs e)
    {
        div_error = "";
        // Get the Device Information
        // Get DeviceID
        string devid = Request.Params["DeviceId"];

        if (string.IsNullOrEmpty(devid) ) {
            div_error = "System Error! Please contect to Administrator.";
            // system error, stop the process , return the Login Screen.
        }

        string[] strserialNumbers = devid.Split(',');

        if (1 < strserialNumbers.Length)
        {
            strserialNumber = strserialNumbers[1].ToString();
        }
        else
        {
            strserialNumber = devid;
        }
        // add by Wei Changye 2012.01.18
        Application["strserialNumber"] = strserialNumber;
    }
    #endregion
    //    {
    //    div_error = "";
    //    // Get the Device Information
    //    // Get DeviceID
    //    string devid = Request.Params["DeviceId"];

    //    if (string.IsNullOrEmpty(devid) ) {
    //        div_error = "System Error! Please contect to Administrator.";
    //        // system error, stop the process , return the Login Screen.
    //    }

    //        string[] strserialNumbers = devid.Split(',');

    //    if (1 < strserialNumbers.Length)
    //    {
    //        strserialNumber = strserialNumbers[1].ToString();
    //    }   
    //    else
    //    {
    //        strserialNumber = devid;
    //    }
    //    // add by Wei Changye 2012.01.18
    //    Application["strserialNumber"] = strserialNumber;
    //}
    //#endregion


    #region "Login in to the MFP"
    /// <summary>
    /// Login in to the MFP
    /// </summary>
    /// <param name="accid"></param>
    /// <Date>2010.12.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.10</Version>
    public void LoginMFP(string accid, E_EA_OSA_TYPE type)
    {
        
        Helper.DeviceSession dev = null;
        dev = Helper.DeviceSession.Get(strserialNumber);

        try
        {
            // 2010.12.21 Add By SES Jijianxong ST
            //Application["loggedinuser"] = accid;
            Application[strserialNumber] = accid;
            // 2010.12.21 Add By SES Jijianxong ED
            //dev.LogUserIn(accid);
            MFPModel mode = new MFPModel(Request);

            // add by WeiChangye 2012.05.17
            if (!UtilCommon.GetAppSettingString("isAllowFollowME").Trim().Equals("true"))
            {
                //不启动followme，直接到打印机
                //dev.LogUserIn(accid);
                dev.LogUserIn(strserialNumber, accid);

                Thread ftpThread = new Thread(new ParameterizedThreadStart(DirectPrint));
                ftpThread.Start(new IpAndKeyModel(Request.Params["REMOTE_ADDR"].ToString(), accid));

                // add by Wei Changye 2012.05.29
                //dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
                //dtMFPPrintTask.MFPPrintTaskDataTable taskTable = adpter.GetDataByLoginName(UtilCommon.GetLoginName(accid));

                //foreach (dtMFPPrintTask.MFPPrintTaskRow item in taskTable.Rows)
                //{
                //    Thread ftpThread = new Thread(new ParameterizedThreadStart(new FtpSend().UploadFile));
                //    ftpThread.Start(new IpAndKeyModel(Request.Params["REMOTE_ADDR"].ToString(), item.MFPPrintTaskID.ToString()));
                    
                //    Thread.Sleep(10);
                //    adpter.UpdateTaskState(item.MFPPrintTaskID);
                //}
                //end add 2012.05.29

            }
            //end 2012.05.17
            else
                // 2012.02.08 Add By Wei Changye
                // update by Wei Changye 2012.03.09
                //dev.LogUserInShowTask();
                if (type.Equals(E_EA_OSA_TYPE.OSA20) || E_EA_COLOR_TYPE.BWCOLOR == mode.ColorMode)
                    Response.Redirect("MFPScreen/UserPrintDetailsFor2.aspx?uid=" + accid + "&sn=" + strserialNumber, false);
                else
                    if (type.Equals(E_EA_OSA_TYPE.OSAMAIN) || type.Equals(E_EA_OSA_TYPE.OSA30))
                        Response.Redirect("MFPScreen/UserPrintDetails.aspx?uid=" + accid + "&sn=" + strserialNumber, false);
                    else
                        if (type.Equals(E_EA_OSA_TYPE.OSA40))
                            Response.Redirect("MFPScreen/UserPrintDetailsFor4.aspx?uid=" + accid + "&sn=" + strserialNumber, false);
                        else

                            //dev.LogUserIn(accid);
                            dev.LogUserIn(strserialNumber, accid);
        }
        catch (Exception ex)
        {
            Helper.LogAccountant.RecordErrLog("-1", dev.deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_SYSERROR);
            throw ex;
        }
    }
    #endregion

    #region "OK Butoon Click."
    /// <summary>
    /// direct_Login
    /// </summary>
    /// <param name="id_Login"></param>
    /// <param name="id_Password"></param>
    /// <Date>2010.12.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.10</Version>
    public void direct_Login(string id_Login, string id_Password, E_EA_OSA_TYPE type)
    {
        dtServerIPSettingTableAdapters.SettinServerIPTableAdapter adpter = new SettinServerIPTableAdapter();
        dtServerIPSetting.SettinServerIPDataTable dt = adpter.GetData();
        String loginType = "0";
        if (dt != null && dt.Count > 0)
        {
            loginType = dt[0].LoginType;
        }
        String pwd = id_Password.Trim();
        //if (loginType.Equals("0") || pwd.Equals(""))
        if (loginType.Equals("0"))
        {
            username_Login(id_Login, id_Password, type);
        }
        else
        {
            pincode_Login(id_Login, id_Password, type);
        }

    }

    public void LDAP_Login(string id_Login, string id_Password, E_EA_OSA_TYPE type)
    {
        id_Password = UtilConst.USER_PASSWORD;
        username_Login(id_Login, id_Password, type);
    }
    public void DB_Login(string id_Login, string id_Password, E_EA_OSA_TYPE type)
    {
        id_Password = UtilConst.USER_PASSWORD;
        username_Login(id_Login, id_Password, type);
    }
    public void pincode_Login(string id_Login, string id_Password, E_EA_OSA_TYPE type)
    {
        // Login Name is Must.
        if (string.IsNullOrEmpty(id_Login) & string.IsNullOrEmpty(div_error))
        {
            div_error = UtilConst.MSG_LOGIN_PINCODE;
        }

        id_Login = id_Login.Trim();

        UserInfoTableAdapter UserInfoAdapter = new UserInfoTableAdapter();
        object count = UserInfoAdapter.CheckUserInfoExistPinCode(id_Login);
        int intcount = 0;
        try
        {
            intcount = Convert.ToInt32(count);
        }
        catch (Exception)
        {

            intcount = 0;
        }

        if (intcount == 0)
        {
            div_error = UtilConst.MSG_LOGIN_PINCODE;
        }
        else
        {
            dtUserInfo.UserInfoDataTable userinfoDT = UserInfoAdapter.GetDataByPinCode(id_Login);
            if (userinfoDT != null && userinfoDT.Count > 0)
            {
                id_Login = userinfoDT[0].LoginName.ToString();
                id_Password = userinfoDT[0].Password.ToString();
            }
        }

        // While Check is Ok
        if (string.IsNullOrEmpty(div_error))
        {
            // User Authorize
            Helper.SimpleEAUser user = new Helper.SimpleEAUser(id_Login, id_Password);
            // Add by Zheng Wei 2012.03.14
            // check if the user could use this machine
            String IpAddress = Request.Params["REMOTE_ADDR"].ToString();
            if (!user.CanUserIt(IpAddress, user.accountid))
            {
                // failed
                div_error = UtilConst.MSG_MFP_USER_NOTA;
                return;
            }
            //end

            // 2012.06.13 Add by Wei CHangye
            Helper.DeviceSession dev = null;
            dev = Helper.DeviceSession.Get(strserialNumber);
            if (!UtilCommon.IsSerialNOExsit(dev.deviceinfo.serialnumber))
            {
                Helper.LogAccountant.RecordErrLog(user.accountid, dev.deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_REGISTER_LIMIT);
                div_error = UtilConst.MSG_MFP_NOT_REGISTER;
                return;
            }
            // end

            if (user.isAuthorized)
            {
                // OK
                LoginMFP(user.accountid, type);
                return;
            }
            else
            {
                // failed
                div_error = UtilConst.MSG_MFP_LOGIN_ERROR;
            }
        }

        // While Error
        if (!string.IsNullOrEmpty(div_error))
        {
            Helper.DeviceSession dev = null;
            dev = Helper.DeviceSession.Get(strserialNumber);
            Helper.LogAccountant.RecordErrLog("-1", dev.deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
            return;
        }

    }


    public void username_Login(string id_Login, string id_Password, E_EA_OSA_TYPE type)
    {
        // Login Name is Must.
        if (string.IsNullOrEmpty(id_Login) & string.IsNullOrEmpty(div_error))
        {
            div_error = UtilConst.MSG_LOGIN_NAMEMUST;
        }

        id_Login = id_Login.Trim();


        //2015 04 22 ????????
        // Passwrod is Must.
        if (string.IsNullOrEmpty(id_Password) & string.IsNullOrEmpty(div_error))
        {
            div_error = UtilConst.MSG_PASSWORD_NAMEMUST;
        }


        // While Check is Ok
        if (string.IsNullOrEmpty(div_error))
        {
            // User Authorize
            Helper.SimpleEAUser user = new Helper.SimpleEAUser(id_Login, id_Password);
            // Add by Zheng Wei 2012.03.14
            // check if the user could use this machine
            String IpAddress = Request.Params["REMOTE_ADDR"].ToString();
            if (!user.CanUserIt(IpAddress, user.accountid))
            {
                // failed
                div_error = UtilConst.MSG_MFP_USER_NOTA;
                return;
            }
            //end

            // 2012.06.13 Add by Wei CHangye
            Helper.DeviceSession dev = null;
            dev = Helper.DeviceSession.Get(strserialNumber);
            if (!UtilCommon.IsSerialNOExsit(dev.deviceinfo.serialnumber))
            {
                Helper.LogAccountant.RecordErrLog(user.accountid, dev.deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_REGISTER_LIMIT);
                div_error = UtilConst.MSG_MFP_NOT_REGISTER;
                return;
            }
            // end

            if (user.isAuthorized)
            {
                // OK
                LoginMFP(user.accountid, type);
                return;
            }
            else
            {
                // failed
                div_error = UtilConst.MSG_MFP_LOGIN_ERROR;
            }
        }

        // While Error
        if (!string.IsNullOrEmpty(div_error))
        {
            Helper.DeviceSession dev = null;
            dev = Helper.DeviceSession.Get(strserialNumber);
            Helper.LogAccountant.RecordErrLog("-1", dev.deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
            return;
        }

    }

    /// <summary>
    /// iccard_Login
    /// </summary>
    /// <param name="iccardid"></param>
    /// <Date>2010.12.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.10</Version>
    

    //ADD BY ZHENGWEI 2013.12.29
    public string ICCARD(string iccardid)
    {
        string after = "";
        for (int i = 0; i < iccardid.Length; i++)
        {
            string a = iccardid.Substring(i, 1);
            char b = Convert.ToChar(a);
            if ((b >= 'a' && b <= 'z') || (b >= 'A' && b <= 'Z') || a == "`" || (b >= '0' && b <= '9'))
            {
                if (a == "`") after += "0";
                else if (a == "a") after += "1";
                else if (a == "b") after += "2";
                else if (a == "c") after += "3";
                else if (a == "d") after += "4";
                else if (a == "e") after += "5";
                else if (a == "f") after += "6";
                else if (a == "g") after += "7";
                else if (a == "h") after += "8";
                else if (a == "i") after += "9";
                else after += a;
            }
        }
        return after;
    }
    public void iccard_Login(string iccardid, E_EA_OSA_TYPE type)
    {
        //ADD BY ZHENGWEI 2013.12.29
        
        string test;
        test = ICCARD(iccardid);
        iccardid = test;


        // Check
        if (string.IsNullOrEmpty(iccardid))
        {
            div_error = UtilConst.MSG_MFP_CARD_MUST;
        }
           
        //ADD BY ZHENGWEI 2013.9.22
        if (iccardid.IndexOf("\n") != -1)
        {
            iccardid = iccardid.Replace("\n", "");
        }
        if (iccardid.IndexOf("\r") != -1)
        {
            iccardid = iccardid.Replace("\r", "");
        }
        //END

        // While Check is Ok
        //2015 04 23 update start
        /*
        if (string.IsNullOrEmpty(div_error))
        {
            // Login Name
            string id_Login;
            // Password
            string id_Password;

            // Check OK
            // Get User Infomation.
            int nPos;
            string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");

            string UserInfo = ICCardData.GetUserInfo(iccardid, serverPath);

            if (UserInfo.Length > 0)
            {
                nPos = UserInfo.IndexOf(",");
                id_Login = UserInfo.Substring(0, nPos);
                id_Password = UserInfo.Substring(nPos + 1);

                // User Authorize
                Helper.SimpleEAUser user = new Helper.SimpleEAUser(id_Login, id_Password);

                // Add by Zheng Wei 2012.03.14
                // check if the user could use this machine
                String IpAddress = Request.Params["REMOTE_ADDR"].ToString();
                if (!user.CanUserIt(IpAddress, user.accountid))
                {
                    // failed
                    div_error = UtilConst.MSG_MFP_USER_NOTA;
                    return;
                }
                if (user.isAuthorized)
                {
                    // OK
                    LoginMFP(user.accountid, type);
                    return;
                }
                else
                {
                    // Failed
                    div_error = UtilConst.MSG_MFP_LOGIN_ERROR;
                }
            }
            else
            {
                div_error = string.Format(UtilConst.MSG_LOGIN_CARD_NOTEXIST, iccardid);

                // edit by Weichangye 2012.03.20 
                RegisterMessage(iccardid, type);

            }

        }
         */


        if (string.IsNullOrEmpty(div_error))
        {
            // Login Name
            string id_Login;
            // Password
            string id_Password;

            UserInfoTableAdapter UserInfoAdapter = new UserInfoTableAdapter();
            dtUserInfo.UserInfoDataTable userinfoDT = UserInfoAdapter.GetDataByICCard(iccardid);
            if (userinfoDT != null && userinfoDT.Count > 0)
            {
                //id_Login = userinfoDT[0].ID.ToString();
                id_Login = userinfoDT[0].LoginName.ToString();
                id_Password = userinfoDT[0].Password.ToString();

                Helper.SimpleEAUser user = new Helper.SimpleEAUser(id_Login, id_Password);

                String IpAddress = Request.Params["REMOTE_ADDR"].ToString();
                if (!user.CanUserIt(IpAddress, user.accountid))
                {
                    // failed
                    div_error = UtilConst.MSG_MFP_USER_NOTA;
                    return;
                }
                if (user.isAuthorized)
                {
                    // OK
                    LoginMFP(user.accountid, type);
                    return;
                }
                else
                {
                    // Failed
                    div_error = UtilConst.MSG_MFP_LOGIN_ERROR;
                }
            }
            else
            {
                div_error = string.Format(UtilConst.MSG_LOGIN_CARD_NOTEXIST, iccardid);

                // edit by Weichangye 2012.03.20 
                //20180830 chen add 
                dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
                int Login_auth_method = settingDispRow.Login_Auth_method;
                if (Login_auth_method == 0)
                {
                    RegisterMessage(iccardid, type);
                }
                else if (Login_auth_method == 1)
                {

                    DalLDAP dalLDAP = new DalLDAP();
                    LDAPEntry bean = dalLDAP.GetInfoByKey();
                    String User_ICNum = bean.User_ICNum;
                    if (User_ICNum.Trim().Equals(""))
                    {
                        RegisterMessage(iccardid, type);
                    }

                }
            }
        }
        // While Error
        if (!string.IsNullOrEmpty(div_error))
        {
            dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
            int Login_auth_method = settingDispRow.Login_Auth_method;
            if (Login_auth_method == UtilConst.USER_DB)
            {
                return;
            }
            Helper.DeviceSession dev = null;
            dev = Helper.DeviceSession.Get(strserialNumber);
            Helper.LogAccountant.RecordErrLog("-1", dev.deviceinfo, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
            return;
        }

    }

    #endregion

    #region "IC card Register Message."
    /// <summary>
    /// IC card Register Message. add by Weichangye 2011.12.28
    /// </summary>
    /// <param name="cardId"></param>
    /// <param name="type"></param>
    public void RegisterMessage(string cardId, E_EA_OSA_TYPE type)
    {
        dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
        int Login_auth_method = settingDispRow.Login_Auth_method;
        if (Login_auth_method == UtilConst.USER_DB)
        {
            return;
        }


        //string strScript = "";
        //strScript =
        //    "    <script language='javascript' type='text/javascript'>" + "\r\n" +
        //    "        function register() {" + "\r\n" +
        //    "            if(confirm('需要对编号:"+ cardId +"的IC卡进行注册吗？')) {window.parent.location.href='" + GetRegisterUrl(cardId, type) + "'\r\n" +
        //    "        }}" + "\r\n" +
        //    "        // Registermsg." + "\r\n" +
        //    "        if ( window.attachEvent)  window.attachEvent('onload',function() { register();});" + "\r\n" +
        //    "        else window.addEventListener('load',function() { register();},true);" + "\r\n" +
        //    "    </script>" + "\r\n";
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RegisterMessage", strScript);


        string registerURL = GetRegisterUrl(cardId, type);
        
        Response.Redirect(registerURL);

    }
    #endregion

    #region Get IC card register url for diffirent OSA version
    /// <summary>
    /// Get IC card register url for diffirent OSA version.
    /// </summary>
    /// <param name="cardId"></param>
    /// <param name="type"></param>
    /// <Date>2011.12.28</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>1.2</Version>
    /// <returns></returns>
    private string GetRegisterUrl(string cardId, E_EA_OSA_TYPE type)
    {
        if (type == E_EA_OSA_TYPE.OSA40)
            return string.Format("RegisterICCardForOSA4.aspx?id={0}", cardId);
        else
            if (type == E_EA_OSA_TYPE.OSA30)
                return string.Format("RegisterICCardForOSA3.aspx?id={0}", cardId);
            else
                return string.Format("RegisterICCard.aspx?id={0}", cardId);

    }
    #endregion

    #region "Success message."
    /// <summary>
    /// Success message.
    /// </summary>
    /// <param name="msg"></param>
    /// <Date>2010.08.20</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void SuccessMessage(string msg)
    {
        string strScript = "";
        strScript =
            "    <script language='javascript' type='text/javascript'>" + "\r\n" +
            "        function success() {" + "\r\n" +
            "            alert('" + msg + "','操作已成功!');" + "\r\n" +
            "        }" + "\r\n" +
            "        // Successmsg." + "\r\n" +
            "        if ( window.attachEvent)  window.attachEvent('onload',function() { success();});" + "\r\n" +
            "        else window.addEventListener('load',function() { success();},true);" + "\r\n" +
            "    </script>" + "\r\n";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SuccessMessage", strScript);
    }
    #endregion

    private void DirectPrint(object o)
    {
        IpAndKeyModel model = (IpAndKeyModel)o;
        dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
        dtMFPPrintTask.MFPPrintTaskDataTable taskTable = adpter.GetDataByLoginName(UtilCommon.GetLoginName(model.Key.ToString()));
        //20180621 chen update 为了批量打印顺序问题
        //foreach (dtMFPPrintTask.MFPPrintTaskRow item in taskTable.Rows)
        //{

        //    Thread ftpThread = new Thread(new ParameterizedThreadStart(new FtpSend().UploadFile));
        //    ftpThread.Start(new IpAndKeyModel(model.IP, item.MFPPrintTaskID.ToString()));
        //    adpter.UpdateTaskState(item.MFPPrintTaskID);
        //    Thread.Sleep(Convert.ToInt32(UtilCommon.GetAppSettingString("Interval")) * 1000);
        //}
        FtpSend ftpSend = new FtpSend();
        foreach (dtMFPPrintTask.MFPPrintTaskRow item in taskTable.Rows)
        {
            ftpSend.UploadFile(new IpAndKeyModel(model.IP, item.MFPPrintTaskID.ToString()));
            adpter.UpdateTaskState(item.MFPPrintTaskID);
            Thread.Sleep(10);
        }


    }

    //#region "OK Butoon Click."
    ///// <summary>
    ///// OK Butoon Click.
    ///// </summary>
    ///// <Date>2010.12.09</Date>
    ///// <Author>SES Ji JianXiong</Author>
    ///// <Version>1.10</Version>
    //private void btn_Login()
    //{
    //    try
    //    {

    //        string id_Login = "";
    //        string id_Password = "";

    //        // Check the Item
    //        if (bolICCardFlg != true)
    //        {
    //            id_Login = Request.Params["id_Login"];
    //            // Login Name is Must.
    //            if (string.IsNullOrEmpty(id_Login))
    //            {
    //                div_error = UtilConst.MSG_LOGIN_NAMEMUST;
    //            }

    //            // Passwrod is Must.
    //            id_Password = Request.Params["id_password"];
    //            if (string.IsNullOrEmpty(id_Password))
    //            {
    //                div_error = UtilConst.MSG_PASSWORD_NAMEMUST;
    //            }
    //        }
    //        else
    //        {
    //            string iccardid = Request.Params["id_ic"];
    //            if (string.IsNullOrEmpty(iccardid))
    //            {
    //                div_error = UtilConst.MSG_MFP_CARD_MUST;
    //            }

    //            // Get User Infomation.
    //            int nPos;
    //            string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");

    //            string UserInfo = ICCardData.GetUserInfo(iccardid, serverPath);

    //            if (UserInfo.Length > 0)
    //            {
    //                nPos = UserInfo.IndexOf(",");
    //                id_Login = UserInfo.Substring(0, nPos);
    //                id_Password = UserInfo.Substring(nPos + 1);

    //            }
    //            else
    //            {
    //                div_error = string.Format(UtilConst.MSG_LOGIN_CARD_NOTEXIST, iccardid);
    //            }

    //        }


    //        // While Check is Ok
    //        if (!string.IsNullOrEmpty(div_error))
    //        {
    //            Helper.SimpleEAUser user = new Helper.SimpleEAUser(id_Login, id_Password);
    //            if (user.isAuthorized)
    //            {
    //                LoginMFP(user.accountid);
    //                return;
    //            }
    //            else
    //            {
    //                div_error = UtilConst.MSG_MFP_LOGIN_ERROR;
    //            }
    //        }

    //        // While Error
    //        if (string.IsNullOrEmpty(div_error))
    //        {
    //            Helper.DeviceSession dev = null;
    //            dev = Helper.DeviceSession.Get(str_uisid);
    //            Helper.LogAccountant.RecordErrLog("-1", dev, E_EA_MESSAGE_TYPE.SIMPLEEA_LOGINERROR);
    //            return;
    //        }

    //    }
    //    catch
    //    {
    //        div_error = "System Error! Please contect to Administrator.";
    //    }

    //}
    //#endregion
}



