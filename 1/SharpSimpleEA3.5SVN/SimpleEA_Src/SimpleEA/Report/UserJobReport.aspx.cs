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
using BLL;
using Model;
/// <summary>
/// User Job Report Screen.
/// </summary>
/// <Date>2010.07.01</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Report_UserJobReport : ListMainPage 
{
    private int Dsp_Count_mode = 0;
    private int Dsp_A3_A4 = 0;
 
    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.01</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_USERJOBREPORT;

        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;
        Dsp_A3_A4 = settingrow.Dis_A3_A4;

        // 2010.12.09 Add By SES zhoumiao Ver.1.1 Update ST 
        //Search Item: User Name
        this.Master.SearchItem = UtilConst.CON_PAGE_USERJOBREPORT;        
        //2010.12.09 Add By SES zhoumiao Ver.1.1 Update ED

        SqlDataListSource.ConnectionString = this.DBConnectionStrings;

        string sql = "";
     
        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            sql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {          
                sql = UserSql();
                sql += " ORDER BY UserInfo.ID";
                       
        }
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;


        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED
        // Get Now Period
        // Start Period.
        DateTime StartPeriod;
        // End Period.
        DateTime EndPeriod;
        // Get Period Time By Now.
        //UtilCommon.GetPeriodBy(DateTime.Now, out StartPeriod, out EndPeriod);
        UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
        //sql = string.Format(sql, UtilConst.CON_DATE_ID, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
        sql = string.Format(sql, null, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
        
        // 2010.11.22 Update By SES Jijianxiong Ver.1.1 Update ED

        SqlDataListSource.SelectCommand = sql;

      
        SetListMainPgae(this.CustomersGridView,
          this.ddlNumPerPage,
          this.ddlIndexOfPage,
          this.lblTotalPage,
          this.btnSelectAll, "UserName,Id,BWCopyTotal,FULLCopyTotal,BWPrintTotal,FULLPrintTotal,ScanTotal,FaxTotal,OtherTotal, Total", lbtnNextPage, lbtnPrePage);
        
        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED


        // Add By JiJianxiong 2010-07-09 ST
        if (!IsPostBack)
        {
            // 2010.11.22 Add By SES Jijianxiong Ver.1.1 Update ST
            // Display setting for the TotalJobReport Screen.
            SetGridViewWidth();
            // 2010.11.22 Add By SES Jijianxiong Ver.1.1 Update ED

            if (this.CustomersGridView.Rows.Count > 0)
            {
                this.CustomersGridView.Sort("UserName", SortDirection.Ascending);
            }
        }
        // Add By JiJianxiong 2010-07-09 ED

        // 2010.11.22 Delete By SES Jijianxiong Ver.1.1 Update ST
        //if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        //{
        //    btnCancel.Visible = false;
        //}
        // 2010.11.22 Delete By SES Jijianxiong Ver.1.1 Update ED

        // 2010.12.14 Update By SES zhoumiao Ver.1.1 Update ST
        this.Master.btn_Search_EAMaster().Click += new EventHandler(btn_SearchClick);
        this.Master.Btn_update().Click += new EventHandler(btn_updateClick);
        // 2010.12.14 Update By SES zhoumiao Ver.1.1 Update ED
        
    }
    #endregion

    #region"Function:User SQL Search"
    /// <summary>
    /// Function:User SQL Search
    /// </summary>
    /// <returns></returns>
    /// <Date>2010.12.09</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public String UserSql()
    {
        String sql = "";
        String MFPNumber = "";

        if (!(ViewState["MFPNumber"] == null || string.IsNullOrEmpty(ViewState["MFPNumber"].ToString())))
        {
            MFPNumber = " AND SerialNumber=" + ConvertStringToSQL(ViewState["MFPNumber"].ToString());
        }
        else
        {
            MFPNumber = "";
        }

        // 2014.04.28 By SES.chen youguang  Update ST
        //sql = "SELECT"
        //        + "  UserInfo.ID               AS Id"
        //        + " ,UserName                  AS UserName"
        //        + " ,''                        AS GrayCopyTotal"
        //        + " ,''                        AS GrayPrintTotal"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.UserID = UserInfo.ID"
        //        + "	   AND JobInformation.JobID = 1"
        //        + MFPNumber
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS CopyTotal"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.UserID = UserInfo.ID"
        //        + "	   AND JobInformation.JobID = 2"
        //        + MFPNumber
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS PrintTotal"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.UserID = UserInfo.ID"
        //        + "	   AND JobInformation.JobID = 6"
        //        + MFPNumber
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS ScanTotal"
        //        + " , ISNULL(("
        //         + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.UserID = UserInfo.ID"
        //        + "	   AND JobInformation.JobID = 8"
        //        + MFPNumber
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS FaxTotal"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.UserID = UserInfo.ID"
        //        + MFPNumber
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS Total"
        //        + " FROM [UserInfo] LEFT JOIN"
        //        + "  [GroupInfo] ON GroupInfo.ID = GroupID";

        //chen update 20140624 start
        string selSql = "";
        if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {
            selSql = " SELECT SUM(JobInformation.SpendMoney)";
        }
        else
        {
            if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
            {
                selSql = " SELECT SUM(JobInformation.DspNumber)";
            }
            else
            {
                selSql = " SELECT SUM(JobInformation.Number)";
            }
        }
        //chen update 20140624 end



        sql = "SELECT"
                + "  UserInfo.ID               AS Id"
                + " ,UserName                  AS UserName"
                //黑白复印
                + " , ISNULL(("
                //+ "	  SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	  WHERE JobInformation.UserID = UserInfo.ID"
                + "	   AND JobInformation.JobID = 1"        //复印
                + "	   AND JobInformation.FunctionID = 1"   //黑白
                +      MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	 ), 0 ) AS BWCopyTotal"

                //彩色复印
                + " , ISNULL(("
                //+ "	  SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	  WHERE JobInformation.UserID = UserInfo.ID"
                + "	   AND JobInformation.JobID = 1"        //复印
                + "	   AND JobInformation.FunctionID = 2"   //黑白
                +      MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	 ), 0 ) AS FULLCopyTotal"
                //黑白打印
                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                //CHEN UPDATE 20140515 (打印 文件归档打印 清单打印)都按打印统计 ST 
                //+ "	   AND JobInformation.JobID = 2 "
                + "	   AND (JobInformation.JobID = 2 OR JobInformation.JobID = 3 OR JobInformation.JobID = 5)"
                //CHEN UPDATE 20140515 ED
                + "	   AND JobInformation.FunctionID = 1"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS BWPrintTotal"

                //彩色打印
                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                //+ "	   AND JobInformation.JobID = 2"
                + "	   AND (JobInformation.JobID = 2 OR JobInformation.JobID = 3 OR JobInformation.JobID = 5)"
                + "	   AND JobInformation.FunctionID = 2"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS FULLPrintTotal"

                //扫描
                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                //+ "	   AND JobInformation.JobID = 6"
                // 扫描和扫描并保存和网络传真
                + "	   AND (JobInformation.JobID = 4 OR JobInformation.JobID = 6 OR JobInformation.JobID = 7)"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS ScanTotal"

                //传真
                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                + "	   AND JobInformation.JobID = 8"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS FaxTotal"

                //其它
                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                + "	   AND JobInformation.JobID = 0"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS OtherTotal"

                //总共
                + " , ISNULL(("
                // "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                //+ "    AND JobInformation.JobID IN (1, 2, 6, 8)"
                //+ "    AND JobInformation.JobID IN (1, 2, 3, 4, 5, 6, 7,8)"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS Total"
                + " FROM [UserInfo] LEFT JOIN"
                + "  [GroupInfo] ON GroupInfo.ID = GroupID";
        // 2014.04.28 By SES.chen youguang  Update end

        //管理员和安全管理员除外
        //sql += "  WHERE UserInfo.ID >  " + UtilConst.CON_DATE_ID;

        if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
            //sql += " AND UserInfo.LoginName = '" + User.Identity.Name + "'";
            sql += " WHERE UserInfo.LoginName = '" + User.Identity.Name + "'";
            //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
        }

        return sql;
    }
    #endregion

    #region "Function:Display setting for the TotalJobReport Screen."
    /// <summary>
    /// Function:Display setting for the TotalJobReport Screen.
    /// </summary>
    /// <Date>2010.11.22</Date>
    /// <Author>SES Jijianxiong</Author>
    /// <Version>1.10</Version>
    public void SetGridViewWidth()
    {
        //20140428 MJ tan DELETE START
        /*
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        // Display Setting

        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST

//        // Total Column
//        if (row.Dis_Job_Total == 0)
//        {
//            CustomersGridView.Columns[5].Visible = false;
//            CustomersGridView.Columns[6].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
//            CustomersGridView.Columns[6].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
//        }
//        else
//        {
//            CustomersGridView.Columns[5].Visible = true;
//            CustomersGridView.Columns[6].HeaderStyle.CssClass = "";
//            CustomersGridView.Columns[6].ItemStyle.CssClass = "";
//            CustomersGridView.Columns[6].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
//        }

//        // B/W Total Column
////        if (row.Dis_Job_BWTotal == 0)   2010.12.07 Update By SES.Jijianxiong Temp
//        if (row.Dis_Job_Total == 0)
//        {
//            CustomersGridView.Columns[7].Visible = false;
//            CustomersGridView.Columns[8].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
//            CustomersGridView.Columns[8].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
//        }
//        else
//        {
//            CustomersGridView.Columns[7].Visible = true;
//            CustomersGridView.Columns[8].HeaderStyle.CssClass = "";
//            CustomersGridView.Columns[8].ItemStyle.CssClass = "";
//            CustomersGridView.Columns[8].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
//        }

//        // Full-Color Total Column
//        //if (row.Dis_Job_FCTotal == 0)   2010.12.07 Update By SES.Jijianxiong Temp
//        if (row.Dis_Job_Total == 0)
//        {
//            CustomersGridView.Columns[9].Visible = false;
//            CustomersGridView.Columns[10].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
//            CustomersGridView.Columns[10].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
//        }
//        else
//        {
//            CustomersGridView.Columns[9].Visible = true;
//            CustomersGridView.Columns[10].HeaderStyle.CssClass = "";
//            CustomersGridView.Columns[10].ItemStyle.CssClass = "";
//            CustomersGridView.Columns[10].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
//        }

        if (row.Dis_Job_ScanTotal == 0)
        {
            CustomersGridView.Columns[7].Visible = false;
            CustomersGridView.Columns[8].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[8].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        }
        else
        {
            CustomersGridView.Columns[7].Visible = true;
            // 2011.1.05 Update By SES zhoumiao Ver.1.1 Update ST
            //CustomersGridView.Columns[8].HeaderStyle.CssClass = "";
            //CustomersGridView.Columns[8].ItemStyle.CssClass = "";
            CustomersGridView.Columns[8].HeaderStyle.CssClass = UtilConst.CSS_ITEM_WRAP;
            CustomersGridView.Columns[8].ItemStyle.CssClass = UtilConst.CSS_ITEM_WRAP;
            // 2011.1.05 Update By SES zhoumiao Ver.1.1 Update ED
            CustomersGridView.Columns[8].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
        }
        if (row.Dis_Job_FaxTotal == 0)
        {
            CustomersGridView.Columns[9].Visible = false;
            CustomersGridView.Columns[10].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[10].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        }
        else
        {
            CustomersGridView.Columns[9].Visible = true;
            // 2011.1.05 Update By SES zhoumiao Ver.1.1 Update ST
            //CustomersGridView.Columns[10].HeaderStyle.CssClass = "";
            //CustomersGridView.Columns[10].ItemStyle.CssClass = "";
            CustomersGridView.Columns[10].HeaderStyle.CssClass = UtilConst.CSS_ITEM_WRAP;
            CustomersGridView.Columns[10].ItemStyle.CssClass = UtilConst.CSS_ITEM_WRAP;
            // 2011.1.05 Update By SES zhoumiao Ver.1.1 Update ED
            CustomersGridView.Columns[10].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
        }
        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED

        // Get Display Count for the Columns
        // 1: Visible
        // 0: Divisible
        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST
        //int displaycount = row.Dis_Job_Total + row.Dis_Job_BWTotal + row.Dis_Job_FCTotal;   2010.12.07 Update By SES.Jijianxiong Temp
        //int displaycount = row.Dis_Job_Total + row.Dis_Job_Total + row.Dis_Job_Total;
        int displaycount = row.Dis_Job_ScanTotal + row.Dis_Job_FaxTotal;
        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED
        // All the Count Column's width is set by the aspx page. the width is the same 12%;
        // So only set the Other items width
        switch (displaycount)
        {
            // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST
            //case 0:
            //    // Normal Disply Mode
            //    CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_USERTOTAL_0_USERNAME_WIDTH);
            //    CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_USERTOTAL_0_GROUPNAME_WIDTH);
            //    break;
            //case 1:
            //    // Only One Count Column displayed.
            //    CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_USERTOTAL_1_USERNAME_WIDTH);
            //    CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_USERTOTAL_1_GROUPNAME_WIDTH);
            //    break;
            //case 2:
            //    // Only Two Count Column displayed.
            //    CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_USERTOTAL_2_USERNAME_WIDTH);
            //    CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_USERTOTAL_2_GROUPNAME_WIDTH);
            //    break;
            //case 3:
            //    // All Count Column displayed.
            //    CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_USERTOTAL_3_USERNAME_WIDTH);
            //    CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_USERTOTAL_3_GROUPNAME_WIDTH);
            //    break;
            //default:
            //    break;

            case 0:
                // Normal Disply Mode
                CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_TOTAL_0_NAME_WIDTH);
                break;
            case 1:
                // Only One Count Column displayed.
                CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_TOTAL_1_NAME_WIDTH);
                break;
            case 2:
                //All Count Column displayed.
                CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_TOTAL_2_NAME_WIDTH);
                break;
            default:
                break;

            // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED
        }
        */
        //20140428 MJ tan DELETE END
    }
    #endregion


    #region "Occurs when a data row is bound to data in GridView."
    /// <summary>
    /// Occurs when a data row is bound to data in GridView.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.01</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Check Row Records
        // Defulat Date Can't be Delete
        // User's Admin
        // Group's no belong
        GridView gridView = (GridView)sender;
        if (Dsp_Count_mode == 0)
        {
            ((BoundField)gridView.Columns[4]).HtmlEncode = false;
            ((BoundField)gridView.Columns[4]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
            ((BoundField)gridView.Columns[6]).HtmlEncode = false;
            ((BoundField)gridView.Columns[6]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
            ((BoundField)gridView.Columns[8]).HtmlEncode = false;
            ((BoundField)gridView.Columns[8]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
            ((BoundField)gridView.Columns[10]).HtmlEncode = false;
            ((BoundField)gridView.Columns[10]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
            ((BoundField)gridView.Columns[12]).HtmlEncode = false;
            ((BoundField)gridView.Columns[12]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
            ((BoundField)gridView.Columns[14]).HtmlEncode = false;
            ((BoundField)gridView.Columns[14]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
            ((BoundField)gridView.Columns[16]).HtmlEncode = false;
            ((BoundField)gridView.Columns[16]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
            //chen add
            ((BoundField)gridView.Columns[18]).HtmlEncode = false;
            ((BoundField)gridView.Columns[18]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
            //
        }
        else
        {
            ((BoundField)gridView.Columns[4]).HtmlEncode = false;
            ((BoundField)gridView.Columns[4]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
            ((BoundField)gridView.Columns[6]).HtmlEncode = false;
            ((BoundField)gridView.Columns[6]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
            ((BoundField)gridView.Columns[8]).HtmlEncode = false;
            ((BoundField)gridView.Columns[8]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
            ((BoundField)gridView.Columns[10]).HtmlEncode = false;
            ((BoundField)gridView.Columns[10]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
            ((BoundField)gridView.Columns[12]).HtmlEncode = false;
            ((BoundField)gridView.Columns[12]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
            ((BoundField)gridView.Columns[14]).HtmlEncode = false;
            ((BoundField)gridView.Columns[14]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
            ((BoundField)gridView.Columns[16]).HtmlEncode = false;
            ((BoundField)gridView.Columns[16]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
            //chen add
            ((BoundField)gridView.Columns[18]).HtmlEncode = false;
            ((BoundField)gridView.Columns[18]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
            //
        }

        GridViewRow gRow = (GridViewRow)e.Row;

        // Delete By SES.JiJianxiong 2010.09.10 ST
        //if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        //{
        //    // Set Color for even Row
        //    if (!Convert.ToBoolean(e.Row.RowIndex % 2))
        //    {
        //        e.Row.CssClass = UtilConst.CSS_ITEM_EVEN;
        //    }
        //}
        // Delete By SES.JiJianxiong 2010.09.10 ED
    }
    #endregion

    // 2010.11.22 Delete By SES Jijianxiong Ver.1.1 Update ST
    //#region "btnCancel_Click"
    ///// <summary>
    ///// btnCancel_Click
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Date>2010.07.01</Date>
    ///// <Author>SES Ji JianXiong</Author>
    ///// <Version>0.01</Version>
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    this.Page.Response.Redirect("JobReport.aspx", false);
    //}
    //#endregion
    // 2010.11.22 Delete By SES Jijianxiong Ver.1.1 Update ED

    #region "btnItemCount_Click"
    /// <summary>
    /// btnItemCount_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.01</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnItemCount_Click(object sender, EventArgs e)
    {
        // 1.Get ID List.
        string strId = "";
        string strIdList = "";
        // 2010.12.14 Add By SES Zhou Miao Ver.1.1 Update ST
        string MfpSerialNumber = "";
        // 2010.12.14 Add By SES Zhou Miao Ver.1.1 Update ED
        foreach (GridViewRow gRow in CustomersGridView.Rows)
        {
            CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
            if (ch.Checked || !HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
            {
                // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ST

                // 2010.11.22 Update By SES Jijianxiong Ver.1.1 Update ST
                //// Update By SES.JiJianxiong 2010.09.10 ST
                //// strId = gRow.Cells[3].Text;
                //strId = gRow.Cells[5].Text;
                //// Update By SES.JiJianxiong 2010.09.10 ED                
                //strId = gRow.Cells[11].Text;
                //// 2010.11.22 Update By SES Jijianxiong Ver.1.1 Update ST     
           
                //strId = gRow.Cells[13].Text;
                strId = gRow.Cells[gRow.Cells.Count - 1].Text;

                // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ED

                if ("".Equals(strIdList))
                {
                    strIdList = strId;
                }
                else
                {
                    strIdList += "," + strId;
                }

            }
        }

        if (strIdList != "")
        {
            // 2010.12.14 Add By SES Zhou Miao Ver.1.1 Update ST
            if (!(string.IsNullOrEmpty(this.Master.MFPTarget.ToString().Trim())))
            {
                MfpSerialNumber = this.Master.ddl_MFPItem().SelectedValue;
            }
            else
            {
                MfpSerialNumber = "0";
            }
           
            // 2010.12.14 Add By SES Zhou Miao Ver.1.1 Update ED
            // 2.Report Page Open.
            string strScript = "<script language='javascript' type='text/javascript'>";
            // 2010.12.14 Update By SES Zhou Miao Ver.1.1 Update ST
            //strScript = strScript + "popWindow(" + "'UserJobReportResult.aspx','"
            //      + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
            //      + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
            //      + UtilConst.REPORT_TYPE_USER.ToString() + "','"
            //      + strIdList + "')";
            //strScript = strScript + "</script>";

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "popWindow", strScript);

            strScript = strScript + "popWindowJob(" + "'UserJobReportResult.aspx','"
                  + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
                  + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
                  + UtilConst.REPORT_TYPE_USER.ToString() + "','"
                  + strIdList + "','"
                  + MfpSerialNumber + "')";
            strScript = strScript + "</script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "popWindowJob", strScript);
            // 2010.12.14 Update By SES Zhou Miao Ver.1.1 Update ED
        }
        // Add By SES.JiJianxiong 2010.09.10 ST
        else
        {
            ErrorAlert(UtilConst.MSG_SELECT_NOTHING);
        }
        // Add By SES.JiJianxiong 2010.09.10 ED
    }
    #endregion

    #region "Function:Raises the GridView.Sorted event."
    /// <summary>
    /// Function:Raises the GridView.Sorted event.
    /// It'll be overrided In each page.
    /// </summary>
    /// <param name="gv"></param>
    /// <seealso cref="SortGridView"/>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public override void SortGridView(GridView cView)
    {
        //20140428 MJ tan DELETE START
        /*
        // Sort ▲▼
        string sortString = "";
        int index = 0;


        cView.Columns[0].HeaderText = "";
        // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ST

        // Update By SES.JiJianxiong 2010.09.10 ST
        //cView.Columns[1].HeaderText = UtilConst.CON_ITEM_USERNAME;
        //cView.Columns[2].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        cView.Columns[2].HeaderText = UtilConst.CON_ITEM_USERNAME;
        //cView.Columns[4].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        //// Update By SES.JiJianxiong 2010.09.10 ED     
        //// 2010.11.22 Add By SES Jijianxiong Ver.1.1 Update ST
        //cView.Columns[6].HeaderText = UtilConst.CON_ITEM_TOTAL;
        //cView.Columns[8].HeaderText = UtilConst.CON_ITEM_BWTOTAL;
        //cView.Columns[10].HeaderText = UtilConst.CON_ITEM_FULLTOTAL;
        //// 2010.11.22 Add By SES Jijianxiong Ver.1.1 Update ED

        cView.Columns[4].HeaderText = UtilConst.CON_ITEM_COPYTOTAL;
        cView.Columns[6].HeaderText = UtilConst.CON_ITEM_PRINTTOTAL;
        cView.Columns[8].HeaderText = UtilConst.CON_ITEM_SCANTOTAL;
        cView.Columns[10].HeaderText = UtilConst.CON_ITEM_FAXTOTAL;
        cView.Columns[12].HeaderText = UtilConst.CON_ITEM_TOTAL;
        // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ED

        if (cView.SortExpression.Equals(""))
        {
            return;
        }

        if ("UserName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_USERNAME;
            // Update By SES.JiJianxiong 2010.09.10 ST
            // index = 1;
            index = 2;
            // Update By SES.JiJianxiong 2010.09.10 ED
        }
        // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ST

        //if ("GroupName".Equals(cView.SortExpression))
        //{
        //    sortString = UtilConst.CON_ITEM_GROUPNAME;
        //    // Update By SES.JiJianxiong 2010.09.10 ST
        //    // index = 2;
        //    index = 4;
        //    // Update By SES.JiJianxiong 2010.09.10 ED
        //}
        //// 2010.11.22 Add By SES Jijianxiong Ver.1.1 Update ST
        //if ("Total".Equals(cView.SortExpression))
        //{
        //    sortString = UtilConst.CON_ITEM_TOTAL;
        //    index = 6;
        //}
        //if ("BWTotal".Equals(cView.SortExpression))
        //{
        //    sortString = UtilConst.CON_ITEM_BWTOTAL;
        //    index = 8;
        //}
        //if ("FullTotal".Equals(cView.SortExpression))
        //{
        //    sortString = UtilConst.CON_ITEM_FULLTOTAL;
        //    index = 10;
        //}
        //// 2010.11.22 Add By SES Jijianxiong Ver.1.1 Update ED

        if ("CopyTotal".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_COPYTOTAL;
            index = 4;
        }
        if ("PrintTotal".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_PRINTTOTAL;
            index = 6;
        }
        if ("ScanTotal".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_SCANTOTAL;
            index = 8;
        }
        if ("FaxTotal".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_FAXTOTAL;
            index = 10;
        }
        if ("Total".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_TOTAL;
            index = 12;
        }
        // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ED

        if (SortDirection.Ascending.Equals(cView.SortDirection))
        {
            sortString = UtilConst.CON_ITEM_SORT_ASC + sortString;
        }
        else
        {
            sortString = UtilConst.CON_ITEM_SORT_DESC + sortString;
        }

        cView.Columns[index].HeaderText = sortString;
        */
        //20140428 MJ tan DELETE END
        //20140428 MJ tan ADD START
        if (this.CustomersGridView.Rows.Count == 0)
        {
            return;
        }
        string sc = SortDirection.Ascending.Equals(cView.SortDirection) ? UtilConst.CON_ITEM_SORT_ASC : UtilConst.CON_ITEM_SORT_DESC;

        int idx = 0;
        foreach (DataControlFieldHeaderCell cell in cView.HeaderRow.Cells)
        {
            cView.Columns[idx].HeaderText = cView.Columns[idx].HeaderText.Replace(UtilConst.CON_ITEM_SORT_ASC, "");
            cView.Columns[idx].HeaderText = cView.Columns[idx].HeaderText.Replace(UtilConst.CON_ITEM_SORT_DESC, "");
            if (cell.ContainingField.SortExpression.Equals(cView.SortExpression))
            {
                cView.Columns[idx].HeaderText = sc + cView.Columns[idx].HeaderText;
            }
            idx++;
        }
        //20140428 MJ tan ADD START
    }
    #endregion

    #region "btnSearch Clicked event"
    /// <summary>
    /// btnSearch Clicked  event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void btn_SearchClick(object sender, EventArgs e)
    {
     
        ViewState["SearchTxt"] = this.Master.txt_Search_EAMaster().Text.Trim();
        this.RunSQL();
        //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ST
        if (CustomersGridView.Rows.Count == 0)
        {
            // no date for Search process.
            ErrorAlert(UtilConst.MSG_NOTHING_SEARCH);
        }
        //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED
    }
    #endregion

    #region "btnupdate Clicked event"
    /// <summary>
    /// btnSearch Clicked  event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void btn_updateClick(object sender, EventArgs e)
    {

        ViewState["MFPNumber"] = this.Master.MFPTarget;
        ViewState["StartPeriod"] = ConvertDateToSQL(this.Master.StartDate);
        ViewState["EndPeriod"] = ConvertDateToSQL(this.Master.EndDate);

        this.RunSQL();
        this.Master.Btn_update().Visible = false;

    }
    #endregion

    #region"Function:SQL to Run "
    /// <summary>
    /// Function:SQL to Run 
    /// </summary>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public void RunSQL()
    {

        SqlDataListSource.SelectCommand = SearchSql();
        CustomersGridView.DataBind();

        this.ddlNumPerPage.SelectedIndex = 0;
        // Set the PageIndex property to display that page selected by the user.
        this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());

        SetListMainPgae(this.CustomersGridView,
         this.ddlNumPerPage,
         this.ddlIndexOfPage,
         this.lblTotalPage,
         this.btnSelectAll, "UserName,Id,BWCopyTotal,FULLCopyTotal,BWPrintTotal,FULLPrintTotal,ScanTotal,FaxTotal,OtherTotal, Total", lbtnNextPage, lbtnPrePage);

        this.CustomersGridView.Sort("UserName", SortDirection.Ascending);

    }
    #endregion

    #region"Function: Sql is Judged "
    /// <summary>
    /// Function: Sql is Judged 
    /// </summary>
    /// <returns>Sql</returns>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private string SearchSql()
    {
        // Get Now Period
        // Start Period.
        DateTime StartPeriod;
        String StartDate;
        // End Period.
        DateTime EndPeriod;
        String EndDate;
        // Get Period Time By Now.
        //UtilCommon.GetPeriodBy(DateTime.Now, out StartPeriod, out EndPeriod);
        UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);
        String sql = "";
        if (!(ViewState["StartPeriod"] == null || string.IsNullOrEmpty(ViewState["StartPeriod"].ToString())))
        {
            StartDate = ViewState["StartPeriod"].ToString();
        }
        else
        {
            StartDate = ConvertDateToSQL(StartPeriod);
        }
        if (!(ViewState["EndPeriod"] == null || string.IsNullOrEmpty(ViewState["EndPeriod"].ToString())))
        {
            EndDate = ViewState["EndPeriod"].ToString();
        }
        else
        {
            EndDate = ConvertDateToSQL(EndPeriod);
        }

        sql = UserSql();
            
        if (!(ViewState["SearchTxt"] == null || string.IsNullOrEmpty(ViewState["SearchTxt"].ToString())))
        {
            //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
            //sql += " AND UserName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
            sql += " WHERE UserName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
            //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
        }
                
        sql += " ORDER BY UserInfo.ID";
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
       // sql = string.Format(sql, UtilConst.CON_DATE_ID, StartDate, EndDate);
        sql = string.Format(sql, null, StartDate, EndDate);
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
      
        ViewState["SearchKeyWord"] = sql;

        return sql;
    }
    #endregion

    #region "ExportCSV"

    protected void btnExportCSV_Click(object sender, EventArgs e)
    {
        CsvDownloadProcess();
    }
    private void CsvDownloadProcess()
    {


        string Name = User.Identity.Name;
        String MFPNumber = "";

        //if (!(ViewState["MFPNumber"] == null || string.IsNullOrEmpty(ViewState["MFPNumber"].ToString())))
        //{
        //    MFPNumber = " AND SerialNumber=" + ConvertStringToSQL(ViewState["MFPNumber"].ToString());
        //}
        //else
        //{
        //    MFPNumber = "";
        //}
        MFPNumber = this.Master.MFPTarget;
        DateTime StartPeriod = this.Master.StartDate;
        DateTime EndPeriod = this.Master.EndDate;

        string SearchTxt = "";
        if (!(ViewState["SearchTxt"] == null || string.IsNullOrEmpty(ViewState["SearchTxt"].ToString())))
        {
            SearchTxt += " WHERE UserInfo.UserName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
        }

        SearchTxt += " ORDER BY UserInfo.ID";

        BllUser bll = new BllUser();
        List<JobInformationCSVModel> idList = null;
        idList = bll.getUserReportCSVList(SearchTxt);

        if (idList.Count == 0)
        {
            ErrorAlert(UtilConst.MSG_SELECT_EXPORT_NOTHING);
        }
        else
        {
            OutPutCsvFile("UserJobReport", idList, MFPNumber, StartPeriod, EndPeriod);
        }
    }
    private void OutPutCsvFile(string filename, List<JobInformationCSVModel> idList, string MfpSerialNumber, DateTime StartPeriod, DateTime EndPeriod)
    {
        // 1.Get Date
        BllJobInformation bll = new BllJobInformation();
        //UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);
        //if (!(ViewState["StartPeriod"] == null || string.IsNullOrEmpty(ViewState["StartPeriod"].ToString())))
        //{
        //    StartPeriod = this.Master.StartDate;
        //    ViewState["StartPeriod"] = ConvertDateToSQL(this.Master.StartDate);
        //    ViewState["EndPeriod"] = ConvertDateToSQL(this.Master.EndDate);

        //}
        //if (!(ViewState["EndPeriod"] == null || string.IsNullOrEmpty(ViewState["EndPeriod"].ToString())))
        //{
        //    string EndDate = ViewState["EndPeriod"].ToString();
        //    EndPeriod = Convert.ToDateTime(EndDate);
        //}



        Dictionary<string, JobInformationCSVModel> dict = bll.GetUserJobDict(idList, StartPeriod, EndPeriod, MfpSerialNumber, Dsp_Count_mode, Dsp_A3_A4);
        
        // 2.To Csv Date.
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("统计-用户使用报表,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,");
        //sb.Append("MFP型号（序列号）,未指定,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,");
        //sb.Append("时间段,2018-12-01 00:00:00～2018-12-31 23:59:59,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,");
        //sb.Append("用户名,用户组名,总使用量,黑白量合计,彩色量合计,复印,,,打印,,,扫描,,,传真,,文件归档打印,,,扫描并保存,,,清单打印,,,传真(Channel2),,网络传真,,其他,,");
        //sb.Append(",,,,,黑白,彩色,合计,黑白,彩色,合计,黑白,彩色,合计,黑白,合计,黑白,彩色,合计,黑白,彩色,合计,黑白,彩色,合计,黑白,合计,黑白,合计,黑白,彩色,合计");
        sb.Append(UtilConst.CON_PAGE_USERREPORT);
        sb.Append("\r\n");
        sb.Append((UtilConst.CON_TARGET_MFP));
        sb.Append("，");
        if (MfpSerialNumber != null && MfpSerialNumber.Length > 0)
        {
            sb.Append(MfpSerialNumber);
        }
        else
        {
            sb.Append("未指定");
        }
        sb.Append("\r\n");
        sb.Append(UtilConst.CON_TIME_PERIOD);
        sb.Append(",");
        sb.Append(ConvertDateToSQL(StartPeriod));
        sb.Append(":");
        sb.Append(ConvertDateToSQL(EndPeriod));
        sb.Append("\r\n");
        sb.Append("用户名,登录名,用户组名,总使用量,黑白量合计,彩色量合计,复印,,,打印,,,扫描,,,传真,,文件归档打印,,,扫描并保存,,,清单打印,,,传真(Channel2),,网络传真,,其他,,");
        sb.Append("\r\n");
        sb.Append(",,,,,,黑白,彩色,合计,黑白,彩色,合计,黑白,彩色,合计,黑白,合计,黑白,彩色,合计,黑白,彩色,合计,黑白,彩色,合计,黑白,合计,黑白,合计,黑白,彩色,合计");
        sb.Append("\r\n");

        JobInformationCSVModel allMoney = new JobInformationCSVModel();
        foreach (string key in dict.Keys)
        {
            JobInformationCSVModel bean = dict[key];
            allMoney.Add(bean);
            string strOutPut = bean.getUserReportData(Dsp_Count_mode);
            strOutPut = strOutPut + "\r\n";
            sb.Append(strOutPut);
        }
        string strOutAll = allMoney.getUserAllReportData(Dsp_Count_mode);
        strOutAll = strOutAll + "\r\n";
        sb.Append(strOutAll);

        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".csv");

        Response.ContentType = "application/text";

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

        Response.Write(sb);

        Response.End();

    }

    #endregion
}
