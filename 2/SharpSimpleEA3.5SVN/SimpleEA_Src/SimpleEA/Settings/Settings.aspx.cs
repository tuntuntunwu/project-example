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
using System.Data.SqlClient;
using dtSettingManagementTableAdapters;

/// <summary>
/// GroupList
/// </summary>
/// <Date>2010.07.08</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Settings_Settings : MainPage
{

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e) 
    {

        this.Master.Title = UtilConst.CON_PAGE_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_SET, "Settings.aspx", true);
        this.Master.JobReportTitle();

        // Check Access Role
        CheckUser();
      
        if (!IsPostBack)
        {

            chk_Money.Enabled = false;
            chk_Money.Checked = false;

            chk_Paper.Enabled = false;
            chk_Money.Checked = false;
            Load_Once();
        }
    }
    #endregion

    #region "Load_Once"
    /// <summary>
    /// Load_Once
    /// </summary>
    /// <Date>2010.07.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void Load_Once()
    {
        // each Month
        ddlMonth.Items.Clear();

        // 5
        ListItem item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME5_NAME,UtilConst.SETPERIODTIME_MONTH_ITME5.ToString());
        ddlMonth.Items.Add(item);
        // 10
        item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME10_NAME,UtilConst.SETPERIODTIME_MONTH_ITME10.ToString());
        ddlMonth.Items.Add(item);
        // 15
        item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME15_NAME,UtilConst.SETPERIODTIME_MONTH_ITME15.ToString());
        ddlMonth.Items.Add(item);
        // 20
        item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME20_NAME, UtilConst.SETPERIODTIME_MONTH_ITME20.ToString());
        ddlMonth.Items.Add(item);
        // 25
        item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME25_NAME, UtilConst.SETPERIODTIME_MONTH_ITME25.ToString());
        ddlMonth.Items.Add(item);
        // last
        item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITMELAST_NAME,UtilConst.SETPERIODTIME_MONTH_ITMELAST.ToString());
        ddlMonth.Items.Add(item);

        //each Week
        ddlWeek.Items.Clear();

        item = new ListItem(UtilConst.SETPERIODTIME_WEEK_MON_NAME, UtilConst.SETPERIODTIME_WEEK_MON.ToString());
        ddlWeek.Items.Add(item);
        item = new ListItem(UtilConst.SETPERIODTIME_WEEK_TUE_NAME,UtilConst.SETPERIODTIME_WEEK_TUE.ToString());
        ddlWeek.Items.Add(item);
        item = new ListItem(UtilConst.SETPERIODTIME_WEEK_WED_NAME,UtilConst.SETPERIODTIME_WEEK_WED.ToString());
        ddlWeek.Items.Add(item);
        item = new ListItem(UtilConst.SETPERIODTIME_WEEK_THU_NAME,UtilConst.SETPERIODTIME_WEEK_THU.ToString());
        ddlWeek.Items.Add(item);
        item = new ListItem(UtilConst.SETPERIODTIME_WEEK_FRI_NAME,UtilConst.SETPERIODTIME_WEEK_FRI.ToString());
        ddlWeek.Items.Add(item);
        item = new ListItem(UtilConst.SETPERIODTIME_WEEK_SAT_NAME,UtilConst.SETPERIODTIME_WEEK_SAT.ToString());
        ddlWeek.Items.Add(item);
        item = new ListItem(UtilConst.SETPERIODTIME_WEEK_SUN_NAME,UtilConst.SETPERIODTIME_WEEK_SUN.ToString());
        ddlWeek.Items.Add(item);

        //each hour
        ddlHour.Items.Clear();

        int i = 1;
        item = new ListItem("凌晨一点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("凌晨二点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("凌晨三点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("凌晨四点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("早上五点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("早上六点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("早上七点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("上午八点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("上午九点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("上午十点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("上午十一点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("正午十二点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("下午一点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("下午二点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("下午三点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("下午四点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("下午五点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("下午六点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("晚上七点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("晚上八点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("晚上九点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("晚上十点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("深夜十一点", i.ToString());
        ddlHour.Items.Add(item);
        i = i + 1;
        item = new ListItem("深夜十二点", i.ToString());
        ddlHour.Items.Add(item);

        // Default Value
        // Get Setting Information from settingmanagement.
        SettingManagementTableAdapter settingAdapter = new SettingManagementTableAdapter();
        dtSettingManagement.SettingManagementDataTable settingtableset = new dtSettingManagement.SettingManagementDataTable();
        settingtableset = settingAdapter.GetData();

        rdoMonth.Checked = true;
        ddlMonth.SelectedValue = UtilConst.SETPERIODTIME_MONTH_ITMELAST.ToString();
        ddlWeek.SelectedValue = UtilConst.SETPERIODTIME_WEEK_SAT.ToString();
        ddlHour.SelectedValue = "24";

        if (settingtableset.Rows.Count > 0)
        {

            rdoMonth.Checked = false;
            rdoWeek.Checked = false;
            rdoDay.Checked = false;

            int intSetPeriod = settingtableset[0].SetPeriod;
            int intSetPeriodTime = settingtableset[0].SetPeriodTime;

            if (intSetPeriod.Equals(UtilConst.SETPERIOD_MONTH))
            {
                // each month
                rdoMonth.Checked = true;
                ddlMonth.SelectedValue = intSetPeriodTime.ToString();
            }
            else if (intSetPeriod.Equals(UtilConst.SETPERIOD_WEEK))
            {
                // each week
                rdoWeek.Checked = true;
                ddlWeek.SelectedValue = intSetPeriodTime.ToString();
            }
            else if (intSetPeriod.Equals(UtilConst.SETPERIOD_DAY))
            {
                // each day
                rdoDay.Checked = true;
                ddlWeek.SelectedValue = UtilConst.SETPERIODTIME_WEEK_SAT.ToString();
            }
    // 2014.04.28 Add By SES chen youguang Ver.1.1 Update ST
            else if (intSetPeriod.Equals(UtilConst.SETPERIOD_UNLIMIT))
            {
                rdoNoLimit.Checked = true;
            }
    // 2014.04.28 Add By SES chen youguang Ver.1.1 Update ED


            ddlHour.SelectedValue = settingtableset[0].SetTime.ToString();


            rdoSystem.Checked = true;
        }

        
        dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
        //rdoUserName.Checked = true;
        //rdoLoginName.Checked = true;
        //rdoGroupName.Checked = true;

        //chkIcCardID.Checked = false;
        //chkU_Restrict.Checked = false;
        //chkG_Restrict.Checked = false;
        //chkR_Copy.Checked = false;
        //chkR_Print.Checked = false;
        //chkR_Scan.Checked = false;
        //chkR_Fax.Checked = false;

        //chkJob_ScanTotal.Checked = false;
        //chkJob_FaxTotal.Checked = false;
        //chkResult_Copy.Checked = false;
        //chkResult_Print.Checked = false;
        //chkResult_Scan.Checked = false;
        //chkResult_Fax.Checked = false;
        //chkResult_Other.Checked = false;


        // Set Card ID display property in the User Management screen.
        int Dis_U_CardID = settingDispRow.Dis_U_CardID;
        //if (Dis_U_CardID.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkIcCardID.Checked = true;
        //}
        //else
        //{
        //    chkIcCardID.Checked = false;
        //}
        // SetRestriction Set Name display property in the User Management screen.
        int Dis_U_Restrict = settingDispRow.Dis_U_Restrict;
        //if (Dis_U_Restrict.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkU_Restrict.Checked = true;
        //}
        //else
        //{
        //    chkU_Restrict.Checked = false;
        //}

        //SetRestriction Set Name display property in the Group Management screen.
        int Dis_G_Restrict = settingDispRow.Dis_G_Restrict;
        //if (Dis_G_Restrict.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkG_Restrict.Checked = true;
        //}
        //else
        //{
        //    chkG_Restrict.Checked = false;
        //}
        //SetCopy Restriction display property in the Restriction Set Management screen.
        int Dis_R_Copy = settingDispRow.Dis_R_Copy;
        //if (Dis_R_Copy.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkR_Copy.Checked = true;
        //}
        //else
        //{
        //    chkR_Copy.Checked = false;
        //}
        //SetPrint Restriction display property in the Restriction Set Management screen.
        int Dis_R_Print = settingDispRow.Dis_R_Print;
        //if (Dis_R_Print.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkR_Print.Checked = true;
        //}
        //else
        //{
        //    chkR_Print.Checked = false;
        //}
        //SetScan Restriction display property in the Restriction Set Management screen.
        int Dis_R_Scan = settingDispRow.Dis_R_Scan;
        //if (Dis_R_Scan.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkR_Scan.Checked = true;
        //}
        //else
        //{
        //    chkR_Scan.Checked = false;
        //}
        //SetFax Restriction display property in the Restriction Set Management screen.
        int Dis_R_Fax = settingDispRow.Dis_R_Fax;
        //if (Dis_R_Fax.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkR_Fax.Checked = true;
        //}
        //else
        //{
        //    chkR_Fax.Checked = false;
        //}

        // SetB/W Total Column display property in Job Report screen.
        int Dis_Job_BWTotal = settingDispRow.Dis_Job_ScanTotal;
        //if (Dis_Job_BWTotal.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkJob_ScanTotal.Checked = true;
        //}
        //else
        //{
        //    chkJob_ScanTotal.Checked = false;
        //}
        //SetFull-Color Total Column display property in Job Report screen.
        int Dis_Job_FCTotal = settingDispRow.Dis_Job_FaxTotal;
        //if (Dis_Job_FCTotal.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkJob_FaxTotal.Checked = true;
        //}
        //else
        //{
        //    chkJob_FaxTotal.Checked = false;
        //}
        //SetCopy Job item's default value for Job Report Result screen.
        int Dis_Result_Copy = settingDispRow.Dis_Result_Copy;
        //if (Dis_Result_Copy.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkResult_Copy.Checked = true;
        //}
        //else
        //{
        //    chkResult_Copy.Checked = false;
        //}

        //SetPrint Job item's default value for Job Report Result screen.
        int Dis_Result_Print = settingDispRow.Dis_Result_Print;
        //if (Dis_Result_Print.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkResult_Print.Checked = true;
        //}
        //else
        //{
        //    chkResult_Print.Checked = false;
        //}
        //SetScan Job item's default value for Job Report Result screen.
        int Dis_Result_Scan = settingDispRow.Dis_Result_Scan;
        //if (Dis_Result_Scan.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkResult_Scan.Checked = true;
        //}
        //else
        //{
        //    chkResult_Scan.Checked = false;
        //}

        //SetFax Job item's default value for Job Report Result screen.
        int Dis_Result_Fax = settingDispRow.Dis_Result_Fax;
        //if (Dis_Result_Fax.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkResult_Fax.Checked = true;
        //}
        //else
        //{
        //    chkResult_Fax.Checked = false;
        //}
        //SetOther Job (Except Copy, Print, Scan and Fax) item's default value for Job Report Result screen.
        int Dis_Result_Other = settingDispRow.Dis_Result_Other;
        //if (Dis_Result_Other.Equals(UtilConst.CON_DISP_TRUE))
        //{
        //    chkResult_Other.Checked = true;
        //}
        //else
        //{
        //    chkResult_Other.Checked = false;
        //}

        // Max display record count for the Log View Screen.
        ddlAvailBorrow.SelectedValue = settingDispRow.Dis_Log_MaxCount.ToString();

        // For the B/W limit number and Full Color limit number. 
        // 1: Can Borrow (Default). User can borrow from Full Color limit number.
        // 0: Cannot Borrow. User cannot borrow from Full Color limit number.
    // 2014.04.28 Add By SES chen youguang Ver.1.1 Update ST
        //if (settingDispRow.Dis_Avai_Borrow == UtilConst.CON_DISP_TRUE)
        //{
        //    rdoCan_Borrow.Checked =true;
        //    rdoNot_Borrow.Checked = false;
        //}
        //else
        //{
        //    rdoNot_Borrow.Checked = true;
        //    rdoCan_Borrow.Checked = false;
        //}
        chk_Money.Checked = false;
        
        chk_Paper.Checked = false;
        int Dis_Count_mode = settingDispRow.Dis_Count_mode;
        int Dis_A3_A4 = settingDispRow.Dis_A3_A4;
        if (Dis_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {
            rdoMoneyCount.Checked = true;
            rdoPaperCount.Checked = false;
            chk_Money.Enabled = true;
            chk_Paper.Enabled = false;
            chk_Paper.Checked = false;
            if (Dis_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
            {
                chk_Money.Checked = false;
            }
            else
            {
                chk_Money.Checked = true;
            }
        }
        else
        {
            rdoPaperCount.Checked = true;
            rdoMoneyCount.Checked = false;
            chk_Money.Enabled = false;
            chk_Money.Checked = false;
            chk_Paper.Enabled = true;
            if (Dis_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
            {
                chk_Paper.Checked = false;
            }
            else
            {
                chk_Paper.Checked = true;
            }
        }
        //认证方式
        int Login_auth_method = settingDispRow.Login_Auth_method;
        if (Login_auth_method == 0)
        {
            rdoSystem.Checked = true;
            rdoLDAP.Checked = false;
            rdoDBAuth.Checked = false;
        }else
            if (Login_auth_method == 1)
            {
                rdoSystem.Checked = false;
                rdoLDAP.Checked = true;
                rdoDBAuth.Checked = false;
            }
            else
            {
                rdoSystem.Checked = false;
                rdoLDAP.Checked = false;
                rdoDBAuth.Checked = true;
            }

        // 2014.04.28 Add By SES chen youguang Ver.1.1 Update ED

        
        // 2010.12.16 Add By SES.Jijianxiong( Edit By SES Zhengwei ) Ver.1.1 Update ED

    
    }
    #endregion

    #region "btnReset_Click"
    /// <summary>
    /// btnReset_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnReset_Click(object sender, EventArgs e) 
    {
        Load_Once();
    }
    #endregion

    #region "btnApply_Click"
    /// <summary>
    /// btnApply_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnApply_Click(object sender, EventArgs e) 
    {
        using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                string strSql;
                //1. Delete Date
                strSql = " DELETE FROM [SettingManagement]; ";
                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    cmd.ExecuteNonQuery();
                }


                //2.Add Data
                strSql = "   INSERT INTO [SettingManagement]  " + Environment.NewLine +
                    "             ([SetPeriod]                " + Environment.NewLine +
                    "             ,[SetPeriodTime]            " + Environment.NewLine + 
                    "             ,[SetTime])                 " + Environment.NewLine +
                    "       VALUES                            " + Environment.NewLine +
                    "             ({0}                        " + Environment.NewLine +
                    "             ,{1}                        " + Environment.NewLine +
                    "             ,{2})                       " + Environment.NewLine;

                string[] paramslist = new string[3];
                if (rdoMonth.Checked)
                {
                    // SetPeriod
                    paramslist[0] = UtilConst.SETPERIOD_MONTH.ToString();
                    // SetPeriodTime
                    paramslist[1] = ConvertIntToSQL(ddlMonth.SelectedValue);
                }
                else if (rdoWeek.Checked)
                {
                    // SetPeriod
                    paramslist[0] = UtilConst.SETPERIOD_WEEK.ToString();
                    // SetPeriodTime
                    paramslist[1] = ConvertIntToSQL(ddlWeek.SelectedValue);
                }
                else if (rdoDay.Checked)
                {
                    // SetPeriod
                    paramslist[0] = UtilConst.SETPERIOD_DAY.ToString();
                    // SetPeriodTime
                    paramslist[1] = "0";
                }
                //chen add for price 20140428 start
                else if( rdoNoLimit.Checked )
                {
                    paramslist[0] = UtilConst.SETPERIOD_UNLIMIT.ToString();
                    paramslist[1] = "0";

                }
                //chen add for price 20140428 end

                // SetTime
                paramslist[2] = ConvertIntToSQL(ddlHour.SelectedValue);

                strSql = string.Format(strSql, paramslist);

                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2010.12.16 Add By SES.Jijianxiong( Edit By SES Zhengwei ) Ver.1.1 Update ST
                strSql = "DELETE FROM [SettingDisp];";
                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2010.12.16 Add By SES.Jijianxiong( Edit By SES Zhengwei ) Ver.1.1 Update ST
                strSql = "INSERT INTO [SettingDisp]      " + Environment.NewLine +
                     "   ([Dis_U_UserName]               " + Environment.NewLine +
                     "   ,[Dis_U_LoginName]              " + Environment.NewLine +
                     "   ,[Dis_U_GroupName]              " + Environment.NewLine +
                     "   ,[Dis_U_CardID]                 " + Environment.NewLine +
                     "   ,[Dis_U_Restrict]               " + Environment.NewLine +
                     "   ,[Dis_G_GroupName]              " + Environment.NewLine +
                     "   ,[Dis_G_Number]                 " + Environment.NewLine +
                     "   ,[Dis_G_Restrict]               " + Environment.NewLine +
                     "   ,[Dis_R_Restrict]               " + Environment.NewLine +
                     "   ,[Dis_R_Copy]                   " + Environment.NewLine +
                     "   ,[Dis_R_Print]                  " + Environment.NewLine +
                     "   ,[Dis_R_Scan]                   " + Environment.NewLine +
                     "   ,[Dis_R_Fax]                    " + Environment.NewLine +
                     "   ,[Dis_Job_Total]                " + Environment.NewLine +
                     "   ,[Dis_Job_CopyTotal]            " + Environment.NewLine +
                     "   ,[Dis_Job_PrintTotal]           " + Environment.NewLine +
                     "   ,[Dis_Job_ScanTotal]            " + Environment.NewLine +
                     "   ,[Dis_Job_FaxTotal]             " + Environment.NewLine +
                     "   ,[Dis_Result_Copy]              " + Environment.NewLine +
                     "   ,[Dis_Result_Print]             " + Environment.NewLine +
                     "   ,[Dis_Result_Scan]              " + Environment.NewLine +
                     "   ,[Dis_Result_Fax]               " + Environment.NewLine +
                     "   ,[Dis_Result_Other]             " + Environment.NewLine +
                     "   ,[Dis_Log_MaxCount]             " + Environment.NewLine +
                     "   ,[Dis_Avai_Borrow]              " + Environment.NewLine +
                     "   ,[Dis_Count_Mode]               " + Environment.NewLine +
                     "   ,[Dis_A3_A4]                    " + Environment.NewLine +
                     "   ,[Login_Auth_method])           " + Environment.NewLine +
                     "   VALUES                          " + Environment.NewLine +
                     "   ({0}                            " + Environment.NewLine +
                     "   ,{1}                            " + Environment.NewLine +
                     "   ,{2}                            " + Environment.NewLine +
                     "   ,{3}                            " + Environment.NewLine +
                     "   ,{4}                            " + Environment.NewLine +
                     "   ,{5}                            " + Environment.NewLine +
                     "   ,{6}                            " + Environment.NewLine +
                     "   ,{7}                            " + Environment.NewLine +
                     "   ,{8}                            " + Environment.NewLine +
                     "   ,{9}                            " + Environment.NewLine +
                     "   ,{10}                           " + Environment.NewLine +
                     "   ,{11}                           " + Environment.NewLine +
                     "   ,{12}                           " + Environment.NewLine +
                     "   ,{13}                           " + Environment.NewLine +
                     "   ,{14}                           " + Environment.NewLine +
                     "   ,{15}                           " + Environment.NewLine +
                     "   ,{16}                           " + Environment.NewLine +
                     "   ,{17}                           " + Environment.NewLine +
                     "   ,{18}                           " + Environment.NewLine +
                     "   ,{19}                           " + Environment.NewLine +
                     "   ,{20}                           " + Environment.NewLine +
                     "   ,{21}                           " + Environment.NewLine +
                     "   ,{22}                           " + Environment.NewLine +
                     "   ,{23}                           " + Environment.NewLine +
                     "   ,{24}                           " + Environment.NewLine +
                     "   ,{25}                           " + Environment.NewLine +
                     "   ,{26}                           " + Environment.NewLine +
                     "   ,{27})                          " + Environment.NewLine;

                paramslist = new string[28];
                //User Name display property in the User Management screen.
                //1: Visible(The value is always 1)
                paramslist[0] = UtilConst.CON_DISP_TRUE.ToString();
                //Login Name display property in the User Management screen.
                //1: Visible(The value is always 1)
                paramslist[1] = UtilConst.CON_DISP_TRUE.ToString();
                //Group Name display property in the User Management screen.
                //1: Visible(The value is always 1)
                paramslist[2] = UtilConst.CON_DISP_TRUE.ToString();
                //Card ID display property in the User Management screen.
                //1: Visible
               // if (chkIcCardID.Checked)
                if(true)
                {
                    paramslist[3] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Divisible(Default)
                else
                {
                    paramslist[3] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Restriction Set Name display property in the User Management screen.
                //1: Visible
               // if (chkU_Restrict.Checked)
                if (true)
                {
                    paramslist[4] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Divisible(Default)
                else
                {
                    paramslist[4] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Group Name display property in the Group Management screen.
                //1: Visible(The value is always 1)
                paramslist[5] = UtilConst.CON_DISP_TRUE.ToString();
                //Number display property in the Group Management screen.
                //1: Visible(The value is always 1)
                paramslist[6] = UtilConst.CON_DISP_TRUE.ToString();
                //Restriction Set Name display property in the Group Management screen.
                //1: Visible
                //if (chkG_Restrict.Checked)
                if (true)
                {
                    paramslist[7] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Divisible(Default)
                else
                {
                    paramslist[7] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Restriction Set Name display property in the Restriction Set Management screen.
                //1: Visible(The value is always 1)
                paramslist[8] = UtilConst.CON_DISP_TRUE.ToString();
                //Copy Restriction display property in the Restriction Set Management screen.
                //1: Visible
               // if (chkR_Copy.Checked)
                if (true)
                {
                    paramslist[9] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Divisible(Default)
                else
                {
                    paramslist[9] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Print Restriction display property in the Restriction Set Management screen.
                //1: Visible
               // if (chkR_Print.Checked)
                if (true)
                {
                    paramslist[10] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Divisible(Default)
                else
                {
                    paramslist[10] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Scan Restriction display property in the Restriction Set Management screen.
                //1: Visible
               // if (chkR_Scan.Checked)
                if (true)
                {
                    paramslist[11] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Divisible(Default)
                else
                {
                    paramslist[11] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Fax Restriction display property in the Restriction Set Management screen.
                //1: Visible
                //if (chkR_Fax.Checked)
                if (true)
                {
                    paramslist[12] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Divisible(Default)
                else
                {
                    paramslist[12] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Total Column display property in Job Report screen.
                //1: Visible
                paramslist[13] = UtilConst.CON_DISP_TRUE.ToString();
                paramslist[14] = UtilConst.CON_DISP_TRUE.ToString();
                paramslist[15] = UtilConst.CON_DISP_TRUE.ToString();
                //Scan Total Column display property in Job Report screen.
                //1: Visible
               // if (chkJob_ScanTotal.Checked)
                if (true)
                {
                    paramslist[16] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Divisible(Default)
                else
                {
                    paramslist[16] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Fax Total Column display property in Job Report screen.
                //1: Visible
               // if (chkJob_FaxTotal.Checked)
                if (true)
                {
                    paramslist[17] = UtilConst.CON_DISP_TRUE.ToString();
                }
                else
                {
                    paramslist[17] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Copy Job item's default value for Job Report Result screen.
                //1: Checked(Default)
                //if (chkResult_Copy.Checked)
                if (true)
                {
                    paramslist[18] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Unchecked
                else
                {
                    paramslist[18] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Print Job item's default value for Job Report Result screen.
                //1: Checked(Default)
                //if (chkResult_Print.Checked)
                if (true)
                {
                    paramslist[19] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Unchecked
                else
                {
                    paramslist[19] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Scan Job item's default value for Job Report Result screen.
                //1: Checked
               // if (chkResult_Scan.Checked)
                if (true)
                {
                    paramslist[20] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Unchecked(Default)
                else
                {
                    paramslist[20] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Fax Job item's default value for Job Report Result screen.
                //1: Checked
                //if (chkResult_Fax.Checked)
                if (true)
                {
                    paramslist[21] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Unchecked(Default)
                else
                {
                    paramslist[21] = UtilConst.CON_DISP_FALSE.ToString();
                }
                //Other Job (Except Copy, Print, Scan and Fax) item's default value for Job Report Result screen.
                //1: Checked
               // if (chkResult_Other.Checked)
                if (true)
                {
                    paramslist[22] = UtilConst.CON_DISP_TRUE.ToString();
                }
                //0: Unchecked(Default)
                else
                {
                    paramslist[22] = UtilConst.CON_DISP_FALSE.ToString();
                }

                // Max display record count for the Log View Screen.
                paramslist[23] = UtilCommon.ConvertStringToSQL( ddlAvailBorrow.SelectedValue.ToString() );

                // For the B/W limit number and Full Color limit number. 
                // 1: Can Borrow (Default). User can borrow from Full Color limit number.
                // 0: Cannot Borrow. User cannot borrow from Full Color limit number.

                //chen add for price 20140428 start
                //if (rdoCan_Borrow.Checked)
                //{
                //    paramslist[24] = UtilConst.CON_DISP_TRUE.ToString();
                //}
                //else
                //{
                //    paramslist[24] = UtilConst.CON_DISP_FALSE.ToString();
                //}
                paramslist[24] = UtilConst.CON_DISP_TRUE.ToString();

                paramslist[26] = "0";

                if (rdoMoneyCount.Checked)
                {
                    paramslist[25] = UtilConst.CON_COUNT_MODE_MONEY.ToString();
                    if (chk_Money.Checked)
                    {
                        paramslist[26] = UtilConst.CON_DISP_A3_A4.ToString();
                    }
                    else
                    {
                        paramslist[26] = UtilConst.CON_DISP_A3_A3.ToString();
                    }
                }
                else
                {
                    paramslist[25] = UtilConst.CON_COUNT_MODE_PAPER.ToString();
                    if (chk_Paper.Checked)
                    {
                        paramslist[26] = UtilConst.CON_DISP_A3_A4.ToString();
                    }
                    else
                    {
                        paramslist[26] = UtilConst.CON_DISP_A3_A3.ToString();
                    }
                }

                //登录认证方式
                if (rdoSystem.Checked)
                {
                    paramslist[27] = UtilConst.CON_LOGIN_AUTH_METHOD_SYS.ToString();
                }
                if (rdoLDAP.Checked)
                {
                    paramslist[27] = UtilConst.CON_LOGIN_AUTH_METHOD_LDAP.ToString();
                }
                if (rdoDBAuth.Checked)
                {
                    paramslist[27] = UtilConst.CON_LOGIN_AUTH_METHOD_DB.ToString();
                }
                //chen add for price 20140428 end

                strSql = string.Format(strSql, paramslist);
                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    cmd.ExecuteNonQuery();
                }

                // 2010.12.16 Add By SES.Jijianxiong( Edit By SES Zhengwei ) Ver.1.1 Update ED


                tran.Commit();
                this.Master.Alert(UtilConst.MSG_PERIOD_SAVED);
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
    #endregion



    protected void rdoPaperCount_CheckedChanged(object sender, EventArgs e)
    {
        chk_Paper.Enabled = rdoPaperCount.Checked;
        chk_Money.Enabled = !rdoPaperCount.Checked;
        chk_Money.Checked = false;
    }
    protected void rdoMoneyCount_CheckedChanged(object sender, EventArgs e)
    {
        chk_Paper.Enabled = !rdoMoneyCount.Checked;
        chk_Money.Enabled = rdoMoneyCount.Checked;
        chk_Paper.Checked = false;
    }
}
