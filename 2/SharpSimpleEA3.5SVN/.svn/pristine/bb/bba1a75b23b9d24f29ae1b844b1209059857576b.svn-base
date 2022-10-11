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
using dtMFPInformationTableAdapters;

/// <summary>
/// SimpleDetailPage
/// </summary>
/// <Date>2010.06.24</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Masterpage_SimpleDetailPage : System.Web.UI.MasterPage
{
    // IN XXX JOB REPORT SCREEN-> REPORT SCREEN
    // REPORT TYPE
    Nullable<int> intREPORT_TYPE = null;
    public Nullable<int> REPORT_TYPE
    {
        get{
            return intREPORT_TYPE;
        }

        set
        {
            intREPORT_TYPE = value;
        }
    }

    // START_TIME
    DateTime datSTART_TIME;
    public DateTime START_TIME
    {
        get
        {
            return datSTART_TIME;
        }

        set
        {
            datSTART_TIME = value;
        }
    }

    // END_TIME
    DateTime datEND_TIME;
    public DateTime END_TIME
    {
        get
        {
            return datEND_TIME;
        }

        set
        {
            datEND_TIME = value;
        }
    }

    //ID LIST
    string[] strId_list = null;
    public string[] ID_LIST
    {
        get
        {
            return strId_list;
        }

        set
        {
            strId_list = value;
        }
    }
    // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
    //MFP SERIAL NUMBER
    string MfpSerialNumber = null;
    public string MFP_SERIAL_NUMBER
    {
        get
        {
            return MfpSerialNumber;
        }

        set
        {
            MfpSerialNumber = value;
        }
    }
    // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED
    // HeadTarget's Display
    public Boolean TargetDisplay
    {
        set
        {
            tblHeadTarget.Visible = value;
            // 2011.03.21 Update By SES zhoumiao Ver.1.1 Update ST
            //// 2011.03.21 Add By SES zhoumiao Ver.1.1 Update ST
            //if (value)
            //{
            //    thcMFP.Width = new Unit("14%");
            //    tbcMFPObj.Width = new Unit("86%");
            //}
            //// 2011.03.21 Add By SES zhoumiao Ver.1.1 Update ED
            if (value)
            {
                tbcPeriodObj.Width = new Unit(117);
                
            }

            // 2011.03.21 Update By SES zhoumiao Ver.1.1 Update ST
           
        }
    }

    // 2010.09.15 Delete By SES Ji.JianXiong ST
    //// Set the Title Icon
    //private string Title_Icon
    //{
    //    set
    //    {
    //        div_icon_type.Attributes.Remove("class");
    //        div_icon_type.Attributes.Add("class", value);
    //    }
    //}
    // 2010.09.15 Delete By SES Ji.JianXiong ED

    // 2010.11.23 Add By SES zhoumiao Ver.1.1 Update ST
    //CheckBox Icons are Display
    public Boolean cheakCellItem
    {
        set
        {
            tbcCheakCellItem.Visible = value;
            tbcTargetPeriod.Width = new Unit(350);
            tbcPeriodObj.Width = new Unit(40);
        }
    }
    //CheckBox Icons of Copy
    public CheckBox CheckBox_chkCopy()
    {
        return chkCopy;
    }
    //CheckBox Icons of Print
    public CheckBox CheckBox_chkPrint()
    {
        return chkPrint;
    }
    //CheckBox Icons of Scan
    public CheckBox CheckBox_chkScan()
    {
        return chkScan;
    }
    //CheckBox Icons of Fax
    public CheckBox CheckBox_chkFax()
    {
        return chkFax;
    }
    //CheckBox Icons of Other
    public CheckBox CheckBox_chkOther()
    {
        return chkOther;
    }
    // 2010.11.23 Add By SES zhoumiao Ver.1.1 Update ED
    // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
    //TableCell Period time 
    public TableCell tbcTargetPeriod_text()
    {
        return tbcTargetPeriod;
    }
    // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED

    #region "PageLoad"
    /// <summary>
    /// PageLoad
    /// </summary>
    /// <Date>2010.06.24</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void PageLoad()
    {
        // REPORT TYPE
        REPORT_TYPE = Convert.ToInt32( Request.Params[UtilConst.PARAM_REPORT_TYPE]);
        // START_TIME
        START_TIME = DateTime.Parse(Request.Params[UtilConst.PARAM_START_TIME] );
        // END_TIME
        END_TIME = DateTime.Parse(Request.Params[UtilConst.PARAM_END_TIME]);
        // ID List
        ID_LIST = Request.Params[UtilConst.PARAM_ID_LIST].Split(',');
        // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
        MFP_SERIAL_NUMBER = Request.Params["SERIAL_NUMBER"];
        // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED

        lblTitle.Text = "统计 - ";
        if (UtilConst.REPORT_TYPE_TOTAL_USER.Equals(REPORT_TYPE))
        {
            // 1.TITLE
            // RESULT SCREEN OF TOTAL JOB REPORT(USER)
            lblTitle.Text = lblTitle.Text + UtilConst.CON_PAGE_JOBREPORTTOTAL;

            // 2.Head Target
            tblHeadTarget.Visible = true;
            thcTarget.Text = UtilConst.CON_TARGET_USER;
            // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
            tblMFPTarget.Visible = true;
            this.tbcMFPObj.Text = GetMFPTargetName();
            // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED

            // 2010.09.15 Delete By SES Ji.JianXiong ST
            //// Set The title Icon
            //Title_Icon = "Icon_Report_Total_IMG";
            // 2010.09.15 Delete By SES Ji.JianXiong ED

        }
        else if (UtilConst.REPORT_TYPE_TOTAL_GROUP.Equals(REPORT_TYPE))
        {
            // RESULT SCREEN OF USER JOB REPORT(GROUP)
            lblTitle.Text = lblTitle.Text + UtilConst.CON_PAGE_JOBREPORTTOTAL;
            // 2.Head Target
            tblHeadTarget.Visible = true;
            thcTarget.Text = UtilConst.CON_TARGET_GROUP;
            // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
            tblMFPTarget.Visible = true;
            this.tbcMFPObj.Text = GetMFPTargetName();
            // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED

            // 2010.09.15 Delete By SES Ji.JianXiong ST
            //// Set The title Icon
            //Title_Icon = "Icon_Report_Total_IMG";
            // 2010.09.15 Delete By SES Ji.JianXiong ED

        }
        else if (UtilConst.REPORT_TYPE_USER.Equals(REPORT_TYPE))
        {
            // RESULT SCREEN OF USER JOB REPORT
            lblTitle.Text = lblTitle.Text + UtilConst.CON_PAGE_JOBREPORTUSER;
            // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
            tblMFPTarget.Visible = true;
            // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED

            // 2010.12.14 Add By SES zhoumiao Ver.1.1 Update ST
            this.tbcMFPObj.Text = GetMFPTargetName();
            // 2010.12.14 Add By SES zhoumiao Ver.1.1 Update ED

            // 2010.09.15 Delete By SES Ji.JianXiong ST
            //// Set The title Icon
            //Title_Icon = "Icon_Report_User_IMG";
            // 2010.09.15 Delete By SES Ji.JianXiong ED
        }
        else if (UtilConst.REPORT_TYPE_GROUP.Equals(REPORT_TYPE))
        {
            // RESULT SCREEN OF GROUP JOB REPORT
            lblTitle.Text = lblTitle.Text + UtilConst.CON_PAGE_JOBREPORTGROUP;
            // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
            tblMFPTarget.Visible = true;
            // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED

            // 2010.12.14 Add By SES zhoumiao Ver.1.1 Update ST
            this.tbcMFPObj.Text = GetMFPTargetName();
            // 2010.12.14 Add By SES zhoumiao Ver.1.1 Update ED

            // 2010.09.15 Delete By SES Ji.JianXiong ST
            //// Set The title Icon
            //Title_Icon = "Icon_Report_Group_IMG";
            // 2010.09.15 Delete By SES Ji.JianXiong ED
        }
        else if (UtilConst.REPORT_TYPE_MFP.Equals(REPORT_TYPE))
        {
            // RESULT SCREEN OF GROUP MFP REPORT
            lblTitle.Text = lblTitle.Text + UtilConst.CON_PAGE_JOBREPORTMFP;

            // 2010.09.15 Delete By SES Ji.JianXiong ST
            //// Set The title Icon
            //Title_Icon = "Icon_Report_MFP_IMG";
            // 2010.09.15 Delete By SES Ji.JianXiong ED
        }
        // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ST
        else if (UtilConst.REPORT_TYPE_LOG.Equals(REPORT_TYPE))
        {           
            lblTitle.Text = lblTitle.Text + UtilConst.CON_PAGE_JOBREPORTLOG;
        }
        // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ED        
        // 3.TARGET PERIOD.
        tbcTargetPeriod.Text = ((DateTime)START_TIME).ToString(UtilConst.TIME_FORMAT) +
                                "～" +
                               ((DateTime)END_TIME).ToString(UtilConst.TIME_FORMAT);
        // 3.WINDOW'S TITLE
        this.Page.Header.Title = lblTitle.Text;
        // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
        //4.SERIAL NUMBER
        this.thcMFP.Text = UtilConst.CON_TARGET_MFP;
        // 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED
    }
    #endregion

    #region "Target Type"
    /// <summary>
    /// Target Name(Group Name,User Name)
    /// </summary>
    /// <Date>2010.06.24</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public string TargetType
    {
        set {
            this.tbcTargetObj.Text = value;
        }
    }
    #endregion

    #region "Get the MFP Target Name"
    /// <summary>
    /// Get the MFP Target Name
    /// </summary>
    /// <Date>2010.12.13</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public string GetMFPTargetName()
    {
        string strName = "";
        if ("0".Equals(MFP_SERIAL_NUMBER))
        {
            strName = "未指定";
        }
        else
        {
            MFPInformationTableAdapter MFPAdapter = new MFPInformationTableAdapter();
            dtMFPInformation.MFPInformationRow MFPRow = MFPAdapter.GetData(MFP_SERIAL_NUMBER)[0];
            strName = MFPRow.ModelName + "(" + MFP_SERIAL_NUMBER + ")";

        }
        return strName;

    }
    #endregion

    #region "Show Message In IE."
    /// <summary>
    /// Show Message In IE.
    /// </summary>
    /// <param name="strMessage"></param>
    /// <Date>2010.06.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void Alert(string strMessage)
    {
        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertMessage", "<script>alert('" + strMessage + "')</script>");
        //error.InnerHtml = strMessage;
        //error.Style.Add(HtmlTextWriterStyle.Color, "red");
        return;

    }
    #endregion

    #region "PageLoad_Available"
    /// <summary>
    /// PageLoad
    /// </summary>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void PageLoad_Available()
    {
        // ID List
        ID_LIST = Request.Params[UtilConst.PARAM_ID_LIST].Split(',');

        // Get Period
        GetNowPeriod();

        // 3.TARGET PERIOD.
        tbcTargetPeriod.Text = ((DateTime)START_TIME).ToString(UtilConst.TIME_FORMAT) +
                                "～" +
                               ((DateTime)END_TIME).ToString(UtilConst.TIME_FORMAT);

        lblTitle.Text = "统计 - ";
        // Available Report SCREEN OF Available Report。
        lblTitle.Text = lblTitle.Text + UtilConst.CON_PAGE_AVAILREPORTMFP;

        // 2010.12.21 Add By SES zhoumiao Ver.1.1 Update ST
        dtSettingDisp.SettingDispRow Avai_Borrowrow = UtilCommon.GetDispSetting();
        if (Avai_Borrowrow.Dis_Avai_Borrow == 1)
        {            
            this.lblAnnotation2.Visible = true;
            // 2011.3.22 Update By SES zhoumiao Ver.1.1 Update ED
            //this.lblAnnotation1.Text = "(备注1)上表统计的页数是指：印刷的页数或者是送信的页数。";
            //this.lblAnnotation2.Text = "(备注2)上表统计的\"可借\"是指：\"黑白\"可以借用\"彩色\"的可使用量。";
            this.lblAnnotation1.Text = "(备注1)上表统计的面数是指：印刷的面数或者是送信的面数。";
            this.lblAnnotation2.Text = "(备注2)上表统计的\"可借\"是指：\"黑白\"可以借用\"彩色\"的可使用量。";
            // 2011.3.22 Update By SES zhoumiao Ver.1.1 Update ED
        }
        // 2010.12.21 Add By SES zhoumiao Ver.1.1 Update ED

        // 2010.09.15 Delete By SES Ji.JianXiong ST
        //// Set The title Icon
        //Title_Icon = "Icon_Avail_IMG";
        // 2010.09.15 Delete By SES Ji.JianXiong ED

        // WINDOW'S TITLE
        this.Page.Header.Title = lblTitle.Text;
    }
    #endregion

    #region "GetNowPeriod"
    /// <summary>
    /// GetNowPeriod
    /// </summary>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void GetNowPeriod()
    {
        DateTime start_time;
        DateTime end_time;
        //UtilCommon.GetPeriodBy(DateTime.Now, out start_time, out end_time);
        UtilCommon.GetSTartEndTimeBy(DateTime.Now, out start_time, out end_time);
        START_TIME = start_time;
        END_TIME = end_time;
    }
    #endregion

    #region"function:The default display item settings for Job Report Result screen to Checked"
    /// <summary>
    /// function:The default display item settings for Job Report Result screen to Checked
    /// </summary>
    /// <Date>2010.11.23</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void CheckBox_item_Set()
    {
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        if (row.Dis_Result_Copy == 1)
        {
            this.chkCopy.Checked = true;
        }
        if (row.Dis_Result_Print == 1)
        {
            this.chkPrint.Checked = true;
        }
        if (row.Dis_Result_Scan == 1)
        {
            this.chkScan.Checked = true;
        }
        if (row.Dis_Result_Fax == 1)
        {
            this.chkFax.Checked = true;
        }
        if (row.Dis_Result_Other == 1)
        {
            this.chkOther.Checked = true;

        }

    }

    #endregion

    #region"function:The default display item Whether selected"
    /// <summary>
    /// function:The default display item Whether selected
    /// </summary>
    /// <returns>if All CheckBoxs are Not selected then return true
    /// else return false</returns>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>

    public bool CheckBox_Selected_Check()
    {
  
        if (this.chkCopy.Checked)
        {
            return false;
        }
        if (this.chkPrint.Checked)
        {
            return false;
        }
        if (this.chkScan.Checked)
        {
            return false;
        }
        if (this.chkFax.Checked)
        {
            return false;
        }
        if (this.chkOther.Checked)
        {
            return false;

        }

        return true;

    }

    #endregion



}
