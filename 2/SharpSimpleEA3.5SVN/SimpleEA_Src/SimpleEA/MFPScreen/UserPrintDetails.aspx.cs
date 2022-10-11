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
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using System.Web.Services.Protocols;
using System.IO;
using System.Timers;
using Osa.Util;
using System.Net;

/// <summary>
/// 
/// </summary>
/// <Date>2012.02.27</Date>
/// <Author>Wei Changye</Author>
/// <Version>1.2</Version>
public partial class MFPScreen_UserPrintDetails : AbstractUserPrintDetails
{
    //FollowMEHandler handler;
    int pageSize = 5;
        public string timeOutPeriod = UtilCommon.GetAppSettingString("TimeOutPeriod").ToString();

    #region Page_Load
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //hidRecordTime.Value = DateTime.Now.Minute.ToString();



        if (hidSN.Value == "")
        {
            //hidSN.Value = Application["strserialNumber"].ToString();
            if (Request.Params["sn"] != null)
            {
                hidSN.Value = Request.Params["sn"].ToString();
            }
            else
            {
                hidSN.Value = Application["strserialNumber"].ToString();
            }

        }

        if (hidUID.Value == "")
        {
            //hidUID.Value = Application["loggedinuser"].ToString();
            if (Request.Params["uid"] != null)
            {
                hidUID.Value = Request.Params["uid"].ToString();
            }
            else
            {
                hidUID.Value = Application[hidSN.Value].ToString();
            }
        }
        Session["loggedinuser"] = hidUID.Value;

        if (Request.Params["REMOTE_ADDR"] != null && Request.Params["REMOTE_ADDR"] != "" && hidRemoteIP.Value == "")
        {
            hidRemoteIP.Value = Request.Params["REMOTE_ADDR"].ToString();
        }

        InitialLable(hidUID.Value);
        InitialGridView();


        //ADD by Zhengwei 20150113
        //string tmp = hidIsTimeout.Value.ToString();
        //if ("1".Equals(tmp))
        //{
        //    btnExit_OnClick(sender, e);
        //}

        //END
    }

    #endregion

    #region InitialGridView
    /// <summary>
    /// InitialGridView
    /// </summary>
    private void InitialGridView()
    {
        if (handler == null)
            handler = new FollowMEHandler();

        // btn available check
        RegisteAvailableCheck();

        int userID = hidUID.Value == null ? -1 : Convert.ToInt32(hidUID.Value);
        dtUserInfoTableAdapters .UserInfoTableAdapter adpter =new dtUserInfoTableAdapters.UserInfoTableAdapter ();
        dtUserInfo .UserInfoDataTable table = adpter.GetDataByUserId(userID);

        if (table != null && table.Count > 0)
        {
            if (hidLoginName.Value == "")
            {
                hidLoginName.Value = table[0].LoginName;
            }

        string sql = handler.BuildSQL(UtilCommon.ConvertStringToSQL(table[0].LoginName));
        
        this.CustomersGridView.DataSource = null;
        SqlDataListSource.ConnectionString = this.DBConnectionStrings;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;
        SqlDataListSource.SelectCommand = sql;

        SetListMainPgae(this.CustomersGridView,
            this.lblCurrentPage,
                this.lblTotalPage, "MFPPrintTaskID,CreateTime,PrintFileName");

        }
        else
            btnExit_OnClick(new object(), new EventArgs());

    }

    #endregion

    #region InitialLable
    /// <summary>
    /// InitialLable
    /// </summary>
    private void InitialLable(string userID)
    {
        //UserAvailableCount calculator = new UserAvailableCount(userID);
        //UserAvailableModel model = calculator.Count();

         
        //if (model != null)
        //{
            //lblUserName.Text = model.userName;
            //lblCopyColor.Text = model.copyColor;
            //lblCopyBW.Text = model.copyBW;
            //lblPrintColor.Text = model.printColor;
            //lblPrintBW.Text = model.printBW;
            // delete by Wei Changye 2012.03.29
            //lblScanColor.Text = model.scanColor;
            //lblScanBW.Text = model.scanBW;
        //}

            //chen update 20140623
        int intUserID = int.Parse(userID);
        string username = "";
        decimal allmoney = 0;
        decimal colmoney = 0;
        UtilCommon.getRemainMoneyUserID(intUserID, ref username, ref allmoney, ref colmoney);

        lblUserName.Text = username;

        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        int Dsp_Count_mode = settingrow.Dis_Count_mode;

        //lblAllMoney.Text = allmoney.ToString();
        //lblColorMoney.Text = colmoney.ToString();
        lblAllMoney.Text = UtilCommon.decimalToMoney(allmoney, Dsp_Count_mode);
        lblColorMoney.Text = UtilCommon.decimalToMoney(colmoney, Dsp_Count_mode); 


        // 获得用户配额ID
        int restrictid = UtilCommon.GetUserRestrictidFromDB(intUserID);
        dtRestrictionInformation.RestrictionInformationDataTable resdatatable = UtilCommon.GetUserResLimitsFromDB(restrictid);

        int status = UtilCommon.GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2);
        if (status == 2)
        {
            imgColCopy.ImageUrl = @"~/Images/Radio_Unselected.png";
            // chkColCopy.Visible = false;
        }
        else
        {
            imgColCopy.ImageUrl = @"~/Images/Radio_Selected.png";
            //chkColCopy.Visible = true;
        }
        status = UtilCommon.GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1);
        if (status == 2)
        {
            // chkBWCopy.Visible = false;
            imgBWCopy.ImageUrl = @"~/Images/Radio_Unselected.png";
        }
        else
        {
            // chkBWCopy.Visible = true;
            imgBWCopy.ImageUrl = @"~/Images/Radio_Selected.png";
        }

        //print color
        status = UtilCommon.GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2);
        if (status == 2)
        {
            //chkColPrint.Visible = false;
            imgColPrint.ImageUrl = @"~/Images/Radio_Unselected.png";
        }
        else
        {
            //chkColPrint.Visible = true;
            imgColPrint.ImageUrl = @"~/Images/Radio_Selected.png";

        }

        //bw print
        status = UtilCommon.GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1);
        if (status == 2)
        {
            imgBWPrint.ImageUrl = @"~/Images/Radio_Unselected.png";
            // chkBWPrint.Visible = false;
        }
        else
        {
            imgBWPrint.ImageUrl = @"~/Images/Radio_Selected.png";
            //   chkBWPrint.Visible = true;
        }
        //scan
        status = UtilCommon.GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1);
        if (status == 2)
        {
            //chkScan.Visible = false;
            imgScan.ImageUrl = @"~/Images/Radio_Unselected.png";
        }
        else
        {
            //chkScan.Visible = true;
            imgScan.ImageUrl = @"~/Images/Radio_Selected.png";
        }
        status = UtilCommon.GetUserLimitStatus(resdatatable, UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1);
        if (status == 2)
        {
            imgFax.ImageUrl = @"~/Images/Radio_Unselected.png";
            //chkFax.Visible = false;
        }
        else
        {
            imgFax.ImageUrl = @"~/Images/Radio_Selected.png";
            //chkFax.Visible = true;
        }

    }

    #endregion

    #region "imgBtnRegister"
    /// <summary>
    /// imgBtnRegister_OnClick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.15</Date>
    /// <Author>Wei Changye</Author>
    /// <Version>0.01</Version>
    public override void imgBtnRegister_OnClick(object sender, EventArgs e)
    {
        if (IsTimeOut())
            return;
        Response.Redirect("~/MFPScreen/RecordCard.aspx?type=" + E_EA_OSA_TYPE.OSAMAIN.ToString() + "&loginid=" + GetHidUID());
    }
    #endregion

    // move to AbstractUserPrintDetails
    // 2012.08.31
    //#region Botton Event

    //#region btnPrint_Click
    ///// <summary>
    ///// btnPrint_Click
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    if (IsTimeOut())
    //        return;

    //    //handler.PrintTask(SelectedItem(), hidRemoteIP.Value, hidLoginName.Value);

    //    try
    //    {
    //        IPHostEntry ipHostInfo = Dns.Resolve(Request.Url.Host);
    //        IPAddress ipAddress = ipHostInfo.AddressList[0];
    //        if (ipAddress.ToString().Equals("127.0.0.1"))
    //        {
    //            ipHostInfo = Dns.Resolve(Dns.GetHostName());
    //            ipAddress = ipHostInfo.AddressList[0];
    //        }

    //        handler.SendPrintTaskID(SelectedItem(), hidRemoteIP.Value, ipAddress.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorAlert("打印过程中出现错误，请联系管理员！");
    //        Global.Log(ex.ToString());
    //    }
    //}

    //#endregion

    //#region btnPrintAndDelete_Click
    ///// <summary>
    ///// btnPrintAndDelete_Click
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnPrintAndDelete_Click(object sender, EventArgs e)
    //{
    //    if (IsTimeOut())
    //        return;

    //    // 2012.04.28 Update by Weichangye ST
    //    //handler.PrintTask(SelectedItem(), hidRemoteIP.Value, hidLoginName.Value);
        
    //    try
    //    {
    //        IPHostEntry ipHostInfo = Dns.Resolve(Request.Url.Host);
    //        IPAddress ipAddress = ipHostInfo.AddressList[0];
    //        if (ipAddress.ToString().Equals("127.0.0.1"))
    //        {
    //            ipHostInfo = Dns.Resolve(Dns.GetHostName());
    //            ipAddress = ipHostInfo.AddressList[0];
    //        }

    //        List<string> tmpList = SelectedItem();
    //        handler.SendPrintTaskID(tmpList, hidRemoteIP.Value, ipAddress.ToString());
    //        handler.DeleteTask(tmpList);
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorAlert("打印过程中出现错误，请联系管理员！");
    //        Global.Log(ex.ToString());
    //    }

        
    //    // 2012.04.28 Update by Weichangye ED

    //    //delete by Wei Changye 2012.03.29
    //    //PrintTask(SelectedItem());
    //    //DeleteTask(SelectedItem());
    //}

    //#endregion

    //#region btnDelete_Click
    ///// <summary>
    ///// btnDelete_Click
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    if (IsTimeOut())
    //        return;
    //    handler.DeleteTask(SelectedItem());

    //    //delete by Wei Changye 2012.03.29
    //    //DeleteTask(SelectedItem());
    //}

    //#endregion

    //#region "BtnExit_OnClick"
    ///// <summary>
    ///// BtnExit_OnClick
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Date>2012.01.07</Date>
    ///// <Author>Wei Changye</Author>
    ///// <Version>0.01</Version>
    //public void btnExit_OnClick(object sender, EventArgs e)
    //{
       
    //    Application.Clear();
    //    Session.Clear();
    //    handler.MFPExit(hidSN.Value);
    //}
    //#endregion

    //#region "BtnEntry_OnClick"
    ///// <summary>
    ///// BtnEntry_OnClick
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Date>2012.01.07</Date>
    ///// <Author>Wei Changye</Author>
    ///// <Version>0.01</Version>
    //public void btnEntry_OnClick(object sender, EventArgs e)
    //{
    //    if (IsTimeOut())
    //        return;
    //    handler.MFPEntry(hidSN.Value, hidUID.Value);
    //}
    //#endregion

    //#region "btnFlash_Click"
    ///// <summary>
    ///// BtnEntry_OnClick
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Date>2012.01.07</Date>
    ///// <Author>Wei Changye</Author>
    ///// <Version>0.01</Version>
    //public void btnFlash_Click(object sender, EventArgs e)
    //{
    //    if (IsTimeOut())
    //        return;
    //    //ChgGridViewToHtml(CustomersGridView);
    //}
    //#endregion

    //#region "imgBtnRegister"
    ///// <summary>
    ///// imgBtnRegister_OnClick
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Date>2012.03.15</Date>
    ///// <Author>Wei Changye</Author>
    ///// <Version>0.01</Version>
    //public void imgBtnRegister_OnClick(object sender, EventArgs e)
    //{
    //    //ChgGridViewToHtml(CustomersGridView);
    //    if (IsTimeOut())
    //        return;
    //    Response.Redirect("RecordCard.aspx?type=" + E_EA_OSA_TYPE.OSAMAIN.ToString() + "&loginid=" + hidUID.Value,false);
    //}
    //#endregion

    //#endregion

    #region Abstract method list

    #region SelectedItem
    /// <summary>
    /// SelectedItem
    /// </summary>
    /// <returns></returns>
    protected override List<string> SelectedItem()
    {
        List<string> strLst = new List<string>();
        foreach (GridViewRow gRow in CustomersGridView.Rows)
        {
            CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
            if (ch.Checked)
            {
                strLst.Add(CustomersGridView.DataKeys[gRow.DataItemIndex%pageSize].Value.ToString());
            }
        }
        return strLst;
    }

    #endregion

    #region IsTimeOut
    /// <summary>
    /// CheckTimeOut
    /// </summary>
    protected override bool IsTimeOut()
    {
        if (Session.Count <= 0 || Application.Count <= 0)
        {
            btnExit_OnClick(new object(), new EventArgs());
            return true;
        }
        else
            return false;
    }

    #endregion

    protected override string GetHidRemote()
    {
        return hidRemoteIP.Value;
    }

    protected override string GetHidSN()
    {
        return hidSN.Value;
    }

    protected override string GetHidUID()
    {
        return hidUID.Value;
    }
    protected override string GetHidLoginName()
    {
        return hidLoginName.Value;
    }

    #endregion

    // move to AbstractUserPrintDetails
    // 2012.08.31
    //#region PartSubString
    ///// <summary>
    ///// PartSubString
    ///// </summary>
    //public string PartSubString(string s)
    //{
    //    if (s.Length > 29)
    //    {
    //        return s.Substring(0, 29) + "...";//这里也可以控制的判断被截取的字符有几个就输出几个点
    //    }
    //    return s;
    //}

    //#endregion

    private void RegisteAvailableCheck()
    {
        if (UtilCommon.GetAppSettingString("ICCardLogin").ToString().Equals("false"))
        {
            imgBtnRegister.Enabled = false;
        }
    }
    
}