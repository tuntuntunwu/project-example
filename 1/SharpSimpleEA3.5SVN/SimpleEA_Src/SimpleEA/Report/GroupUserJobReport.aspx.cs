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
using dtUserInfoTableAdapters;
using BLL;
using Model;
using System.Collections.Generic;

/// <summary>
/// Group Job Report Screen.
/// </summary>
/// <Date>2010.07.06</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Report_GroupUserJobReport : ListMainPage 
{
    private int Dsp_Count_mode = 0;
    private int Dsp_A3_A4 = 0;

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.06</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        const string title = UtilConst.CON_PAGE_GROUPUSERJOBREPORT;
        this.Master.Title = title;
        this.Master.SearchItem = title;
       // this.Master.SearchItem = UtilConst.CON_PAGE_GROUPJOBREPORT; 
        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;
        Dsp_A3_A4 = settingrow.Dis_A3_A4;

        // Check Access Role
        CheckUser();

        SqlDataListSource.ConnectionString = this.DBConnectionStrings;
        
        string sql = "";
        // 2010.12.9 Update By SES zhoumiao Ver.1.1 Update ST

        // 2010.11.23 Update By SES zhoumiao Ver.1.1 Update ST
        //sql = " SELECT ID, GroupName , ISNULL((                    " + Environment.NewLine;
        //sql += "                        SELECT COUNT(UserInfo.ID ) " + Environment.NewLine;
        //sql += "                          FROM UserInfo            " + Environment.NewLine;
        //sql += "                         WHERE UserInfo.GroupID = GroupInfo.ID " + Environment.NewLine;
        //sql += "                      ),0) SUMUSER                 " + Environment.NewLine;
        //sql += "  FROM [GroupInfo]                                 " + Environment.NewLine;
        //// Add By KI 2010-07-09 ST
        //sql += "  WHERE ID <> {0}                                  " + Environment.NewLine;
        //// Add By KI 2010-07-09 ED
        //sql += " ORDER BY ID                                       " + Environment.NewLine;
        //// Add By KI 2010-07-09 ST
        //sql = string.Format(sql, UtilConst.CON_DATE_ADMIN_ID);
        //// Add By KI 2010-07-09 ED
        //sql = "   SELECT A.GroupName AS GroupName"
        //       + " ,A.ID AS ID"
        //       + " ,ISNULL((SELECT COUNT(UserInfo.ID )FROM UserInfo"
        //       + "  WHERE UserInfo.GroupID = A.ID),0) SUMUSER"
        //       + " , ISNULL(("
        //       + "	SELECT SUM(JobInformation.Number)"
        //       + "	  FROM JobInformation"
        //       + "	 WHERE JobInformation.GroupID = A.ID"
        //       + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //       + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //       + "	),0) AS Total"
        //       + " , ISNULL(("
        //       + "	SELECT SUM(JobInformation.Number)"
        //       + "	  FROM JobInformation"
        //       + "	 WHERE JobInformation.GroupID = A.ID"
        //       + "	   AND JobInformation.FunctionID = 1"
        //       + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //       + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //       + "	),0) AS BWTotal"
        //       + " , ISNULL(("
        //       + "	SELECT SUM(JobInformation.Number)"
        //       + "	  FROM JobInformation"
        //       + "	 WHERE JobInformation.GroupID = A.ID"
        //       + "	   AND JobInformation.FunctionID = 2"
        //       + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //       + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //       + "	),0) AS FullTotal"
        //       + "    FROM  [GroupInfo] A"
        //       + "  WHERE ID <> {0}"
        //       + "ORDER BY A.ID";

        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            sql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {            
                sql = GroupSql();
                sql += " ORDER BY A.ID";                
           
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
        UtilCommon.GetPeriodBy(DateTime.Now, out StartPeriod, out EndPeriod);
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
       //sql = string.Format(sql, UtilConst.CON_DATE_ADMIN_ID, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
        sql = string.Format(sql,null, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
        
        // 2010.11.23 Update By SES zhoumiao Ver.1.1 Update ED               
      
        SqlDataListSource.SelectCommand = sql;

        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST

        //// 2010.12.01 Update By SES zhoumiao Ver.1.1 Update ST
        ////SetListMainPgae(this.CustomersGridView,
        ////    this.ddlNumPerPage,
        ////    this.ddlIndexOfPage,
        ////    this.lblTotalPage,
        ////    this.btnSelectAll, "GroupName,SUMUSER,ID");
        //SetListMainPgae(this.CustomersGridView,
        //    this.ddlNumPerPage,
        //    this.ddlIndexOfPage,
        //    this.lblTotalPage,
        //    this.btnSelectAll, "GroupName,SUMUSER,ID,Total,BWTotal,FullTotal");
        //// 2010.12.01 Update By SES zhoumiao Ver.1.1 Update ED 
        SetListMainPgae(this.CustomersGridView,
           this.ddlNumPerPage,
           this.ddlIndexOfPage,
           this.lblTotalPage,
           this.btnSelectAll, "GroupName,SUMUSER,ID,GrayCopyTotal,FULLCopyTotal,GrayPrintTotal,FULLPrintTotal,ScanTotal,FaxTotal,OtherTotal,Total", lbtnNextPage, lbtnPrePage);
        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED

        // Add By JiJianxiong 2010-07-09 ST
        if (!IsPostBack)
        {
            // 2010.11.23 Add By SES zhoumiao Ver.1.1 Update ST
            // Display setting for the TotalJobReport Screen.
            SetGridViewWidth();
            // 2010.11.23 Add By SES zhoumiao Ver.1.1 Update ED
            if (this.CustomersGridView.Rows.Count > 0)
            {
                this.CustomersGridView.Sort("GroupName", SortDirection.Ascending);
            }
        }
        // Add By JiJianxiong 2010-07-09 ED

        // 2010.12.14 Update By SES zhoumiao Ver.1.1 Update ST
        this.Master.btn_Search_EAMaster().Click += new EventHandler(btn_SearchClick);
        this.Master.Btn_update().Click += new EventHandler(btn_updateClick);
        // 2010.12.14 Update By SES zhoumiao Ver.1.1 Update ED
        
    }
    #endregion

    #region"Function:Group SQL Search"
    /// <summary>
    /// Function:Group SQL Search
    /// </summary>
    /// <returns></returns>
    /// <Date>2010.12.09</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public String GroupSql()
    {
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



        String sql = string.Empty;
        String MFPNumber = string.Empty;
        if (!(ViewState["MFPNumber"] == null || string.IsNullOrEmpty(ViewState["MFPNumber"].ToString())))
        {
            MFPNumber = " AND SerialNumber=" + ConvertStringToSQL(ViewState["MFPNumber"].ToString());
        }
        else
        {
            MFPNumber = "  ";
        }

        sql = "SELECT A.GroupName AS GroupName"
            + " ,A.ID AS ID"
            + " ,ISNULL((SELECT COUNT(UserInfo.ID )FROM UserInfo"
            + "  WHERE UserInfo.GroupID = A.ID),0) AS SUMUSER"
            //黑白复印
            + " , ISNULL(("
            //+ "	SELECT SUM(JobInformation.SpendMoney)"
            + selSql
            + "	  FROM JobInformation"
            + "	 WHERE JobInformation.GroupID = A.ID"
            + "	   AND JobInformation.JobID = 1"
            + "	   AND JobInformation.FunctionID = 1"
            + MFPNumber
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
            + "	),0) AS GrayCopyTotal"

            //彩色复印
            + " , ISNULL(("
            //+ "	SELECT SUM(JobInformation.SpendMoney)"
            + selSql
            + "	  FROM JobInformation"
            + "	 WHERE JobInformation.GroupID = A.ID"
            + "	   AND JobInformation.JobID = 1"
            + "	   AND JobInformation.FunctionID = 2"
            + MFPNumber
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
            + "	),0) AS FULLCopyTotal"

            //黑白打印
            + " , ISNULL(("
            //+ "	SELECT SUM(JobInformation.SpendMoney)"
            + selSql
            + "	  FROM JobInformation"
            + "	 WHERE JobInformation.GroupID = A.ID"
            //+ "	   AND JobInformation.JobID = 2"
            + "	   AND (JobInformation.JobID = 2 OR JobInformation.JobID = 3 OR JobInformation.JobID = 5)"
            + "	   AND JobInformation.FunctionID = 1"
            + MFPNumber
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
            + "	),0) AS GrayPrintTotal"

            //彩色打印
            + " , ISNULL(("
            //+ "	SELECT SUM(JobInformation.SpendMoney)"
            + selSql
            + "	  FROM JobInformation"
            + "	 WHERE JobInformation.GroupID = A.ID"
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
            + "	 WHERE JobInformation.GroupID = A.ID"
            //+ "	   AND JobInformation.JobID = 6"
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
            + "	 WHERE JobInformation.GroupID = A.ID"
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
            + "	 WHERE JobInformation.GroupID = A.ID"
            + "	   AND JobInformation.JobID = 0"
            + MFPNumber
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
            + "	),0) AS OtherTotal"

            //总计
            + " , ISNULL(("
            //+ "	SELECT SUM(JobInformation.SpendMoney)"
            + selSql
            + "	  FROM JobInformation"
            + "	 WHERE JobInformation.GroupID = A.ID"
            //+ "    AND JobInformation.JobID IN (1, 2, 6, 8)"
            //+ "    AND JobInformation.JobID IN (1, 2, 3, 4, 5, 6, 7,8)"
            + MFPNumber
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
            + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
            + "	),0) AS Total"
            + "    FROM  [GroupInfo] A";
        //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ST
         //   + "  WHERE ID <> {0}";
        //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ED

        return sql;
    }

    #endregion

    #region "Function:Display setting for the Group JobReport Screen."
    /// <summary>
    /// Function:Display setting for the GroupJobReport Screen.
    /// </summary>
    /// <Date>2010.11.23</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void SetGridViewWidth()
    {
        //20140424 MJ tan DELETE START
        /*
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        // Display Setting

        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST
        //// Total Column
        //if (row.Dis_Job_Total == 0)
        //{
        //    CustomersGridView.Columns[5].Visible = false;
        //    CustomersGridView.Columns[6].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        //    CustomersGridView.Columns[6].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        //}
        //else
        //{
        //    CustomersGridView.Columns[5].Visible = true;
        //    CustomersGridView.Columns[6].HeaderStyle.CssClass = "";
        //    CustomersGridView.Columns[6].ItemStyle.CssClass = "";
        //    CustomersGridView.Columns[6].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
        //}

        //// B/W Total Column
        ////if (row.Dis_Job_BWTotal == 0)   2010.12.07 Update By SES.Jijianxiong Temp
        //if (row.Dis_Job_Total == 0)
        //{
        //    CustomersGridView.Columns[7].Visible = false;
        //    CustomersGridView.Columns[8].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        //    CustomersGridView.Columns[8].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        //}
        //else
        //{
        //    CustomersGridView.Columns[7].Visible = true;
        //    CustomersGridView.Columns[8].HeaderStyle.CssClass = "";
        //    CustomersGridView.Columns[8].ItemStyle.CssClass = "";
        //    CustomersGridView.Columns[8].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
        //}

        //// Full-Color Total Column
        ////if (row.Dis_Job_FCTotal == 0)   2010.12.07 Update By SES.Jijianxiong Temp
        //if (row.Dis_Job_Total == 0)
        //{
        //    CustomersGridView.Columns[9].Visible = false;
        //    CustomersGridView.Columns[10].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        //    CustomersGridView.Columns[10].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        //}
        //else
        //{
        //    CustomersGridView.Columns[9].Visible = true;
        //    CustomersGridView.Columns[10].HeaderStyle.CssClass = "";
        //    CustomersGridView.Columns[10].ItemStyle.CssClass = "";
        //    CustomersGridView.Columns[10].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
        //}
        if (row.Dis_Job_ScanTotal == 0)
        {
            CustomersGridView.Columns[9].Visible = false;
            CustomersGridView.Columns[10].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[10].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        }
        else
        {
            CustomersGridView.Columns[9].Visible = true;
            CustomersGridView.Columns[10].HeaderStyle.CssClass = "";
            CustomersGridView.Columns[10].ItemStyle.CssClass = "";
            CustomersGridView.Columns[10].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
        }
        if (row.Dis_Job_FaxTotal == 0)
        {
            CustomersGridView.Columns[11].Visible = false;
            CustomersGridView.Columns[12].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[12].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        }
        else
        {
            CustomersGridView.Columns[11].Visible = true;
            CustomersGridView.Columns[12].HeaderStyle.CssClass = "";
            CustomersGridView.Columns[12].ItemStyle.CssClass = "";
            CustomersGridView.Columns[12].HeaderStyle.Width = new Unit(UtilConst.CSS_JOB_COUNT_WIDTH);
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
            //    CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUP_TOTAL_0_GROUPNAME_WIDTH);
            //    break;
            //case 1:
            //    // Only One Count Column displayed.
            //    CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUP_TOTAL_1_GROUPNAME_WIDTH);
            //    break;
            //case 2:
            //    // Only Two Count Column displayed.
            //    CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUP_TOTAL_2_GROUPNAME_WIDTH);
            //    break;
            //case 3:
            //    // All Count Column displayed.
            //    CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUP_TOTAL_3_GROUPNAME_WIDTH);
            //    break;
            //default:
            //    break;
            case 0:
                // Normal Disply Mode
                CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUP_0_NAME_WIDTH);
                break;
            case 1:
                // Only One Count Column displayed.
                CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUP_1_NAME_WIDTH);
                break;
            case 2:
                //All Count Column displayed.
                CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUP_2_NAME_WIDTH);
                break;
            default:
                break;
            // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED
        }
        */
        //20140424 MJ tan DELETE END
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
            ((BoundField)gridView.Columns[18]).HtmlEncode = false;
            ((BoundField)gridView.Columns[18]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
            ((BoundField)gridView.Columns[20]).HtmlEncode = false;
            ((BoundField)gridView.Columns[20]).DataFormatString = UtilConst.CON_MONEY_FORMAT;


        }
        else
        {
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
            ((BoundField)gridView.Columns[18]).HtmlEncode = false;
            ((BoundField)gridView.Columns[18]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
            ((BoundField)gridView.Columns[20]).HtmlEncode = false;
            ((BoundField)gridView.Columns[20]).DataFormatString = UtilConst.CON_PAPER_FORMAT;

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

    // 2010.11.23 Delete By SES zhoumiao Ver.1.1 Update ST
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
    // 2010.11.23 Delete By SES zhoumiao Ver.1.1 Update ED

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
            if (ch.Checked)
            {
                // 2010.12.9 Update By SES zhoumiao Ver.1.1 Update ST
                // 2010.11.23 Update By SES zhoumiao Ver.1.1 Update ST
                // Update By SES.JiJianxiong 2010.09.10 ST
                //strId = gRow.Cells[3].Text;
                //strId = gRow.Cells[5].Text;
               // strId = gRow.Cells[11].Text;
                // Update By SES.JiJianxiong 2010.09.10 ED
                // 2010.11.23 Update By SES zhoumiao Ver.1.1 Update ED

                //strId = gRow.Cells[15].Text;
                strId = gRow.Cells[gRow.Cells.Count - 1].Text;

                // 2010.12.9 Update By SES zhoumiao Ver.1.1 Update ED
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
            //// 2.Report Page Open.
            //string strScript = "<script language='javascript' type='text/javascript'>";
            //// 2010.12.14 Update By SES Zhou Miao Ver.1.1 Update ST
            //strScript = strScript + "popWindow(" + "'GroupJobReportResult.aspx','"
            //      + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
            //      + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
            //      + UtilConst.REPORT_TYPE_GROUP.ToString() + "','"
            //      + strIdList + "')";
            //strScript = strScript + "</script>";

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "popWindow", strScript);



            // 2.Report Page Open.
            string strScript = "<script language='javascript' type='text/javascript'>";
            // 2010.12.14 Update By SES Zhou Miao Ver.1.1 Update ST
            strScript = strScript + "popWindowJob(" + "'GroupUserJobReportGroupResult.aspx','"
                  + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
                  + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
                  + UtilConst.REPORT_TYPE_GROUP.ToString() + "','"
                  + strIdList + "','"
                  + MfpSerialNumber + "')";
            strScript = strScript + "</script>";


            // 2012.04.28 Add By Weichangye SLC ST
            //string strScript = "<script language='javascript' type='text/javascript'>";
            //// 2010.12.14 Update By SES Zhou Miao Ver.1.1 Update ST
            //strScript = strScript + "popWindowJob(" + "'GroupUserMFPJobReportResult.aspx','"
            //      + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
            //      + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
            //      + UtilConst.REPORT_TYPE_GROUP.ToString() + "','"
            //      + strIdList + "','"
            //      + MfpSerialNumber + "')";
            //strScript = strScript + "</script>";
            // 2012.04.28 Add By Weichangye SLC ED


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
        //20140424 MJ tan DELETE START
        /*
        // Sort ▲▼
        string sortString = "";
        int index = 0;


        cView.Columns[0].HeaderText = "";
        // Update By SES.JiJianxiong 2010.09.10 ST
        //cView.Columns[1].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        //cView.Columns[2].HeaderText = UtilConst.CON_ITEM_GROUPUSERCOUNT;
        cView.Columns[2].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        cView.Columns[4].HeaderText = UtilConst.CON_ITEM_GROUPUSERCOUNT;
        // Update By SES.JiJianxiong 2010.09.10 ED
        // Update By SES.JiJianxiong 2010.09.10 ED
        // 2010.11.22 Update By SES zhoumiao Ver.1.1 Update ST
        //// 2010.11.22 Update By SES zhoumiao Ver.1.1 Update ST
        //cView.Columns[6].HeaderText = UtilConst.CON_ITEM_TOTAL;
        //cView.Columns[8].HeaderText = UtilConst.CON_ITEM_BWTOTAL;
        //cView.Columns[10].HeaderText = UtilConst.CON_ITEM_FULLTOTAL;
        //// 2010.11.22 Update By SES zhoumiao Ver.1.1 Update ED
        cView.Columns[6].HeaderText = UtilConst.CON_ITEM_COPYTOTAL;
        cView.Columns[8].HeaderText = UtilConst.CON_ITEM_PRINTTOTAL;
        cView.Columns[10].HeaderText = UtilConst.CON_ITEM_SCANTOTAL;
        cView.Columns[12].HeaderText = UtilConst.CON_ITEM_FAXTOTAL;
        cView.Columns[14].HeaderText = UtilConst.CON_ITEM_TOTAL;
        // 2010.11.22 Update By SES zhoumiao Ver.1.1 Update ST
        if (cView.SortExpression.Equals(""))
        {
            return;
        }

        if ("GroupName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_GROUPNAME;
            // Update By SES.JiJianxiong 2010.09.10 ST
            // index = 1;
            index = 2;
            // Update By SES.JiJianxiong 2010.09.10 ED
        }

        if ("SUMUSER".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_GROUPUSERCOUNT;
            // Update By SES.JiJianxiong 2010.09.10 ST
            // index = 2;
            index = 4;
            // Update By SES.JiJianxiong 2010.09.10 ED
        }
        // 2010.12.9 Update By SES zhoumiao Ver.1.1 Update ST
        //// 2010.11.22 Add By SES zhoumiao Ver.1.1 Update ST
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
        //// 2010.11.22 Add By SES zhoumiao Ver.1.1 Update ED
        if ("CopyTotal".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_COPYTOTAL;
            index = 6;
        }
        if ("PrintTotal".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_PRINTTOTAL;
            index = 8;
        }
        if ("ScanTotal".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_SCANTOTAL;
            index = 10;
        }
        if ("FaxTotal".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_FAXTOTAL;
            index = 12;
        }
        if ("Total".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_TOTAL;
            index = 14;
        }
        // 2010.12.9 Update By SES zhoumiao Ver.1.1 Update ED
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
        //20140424 MJ tan DELETE END
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
           this.btnSelectAll, "GroupName,SUMUSER,ID,GrayCopyTotal,FULLCopyTotal,GrayPrintTotal,FULLPrintTotal,ScanTotal,FaxTotal,OtherTotal,Total", lbtnNextPage, lbtnPrePage);

        this.CustomersGridView.Sort("GroupName", SortDirection.Ascending);

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
        UtilCommon.GetPeriodBy(DateTime.Now, out StartPeriod, out EndPeriod);
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

        sql = GroupSql();

        if (!(ViewState["SearchTxt"] == null || string.IsNullOrEmpty(ViewState["SearchTxt"].ToString())))
        {
            //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
          //  sql += " AND GroupName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
            sql += " WHERE GroupName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
            //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
        }

        sql += " ORDER BY A.ID";

        
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
        //sql = string.Format(sql, UtilConst.CON_DATE_ADMIN_ID, StartDate, EndDate);
        sql = string.Format(sql, null, StartDate, EndDate);
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED

        ViewState["SearchKeyWord"] = sql;

        return sql;
    }
    #endregion

    #region btnGroupName_Click
    protected void btnGroupName_Click(object sender, EventArgs e)
    {
        string groupid = ((LinkButton)sender).CommandArgument.ToString();
        string uid = null;
        UserInfoTableAdapter userAdapter = new UserInfoTableAdapter();
        foreach (dtUserInfo.UserInfoRow row in userAdapter.GetDataByGroupID(int.Parse(groupid)).Rows)
        {
            uid += row.ID + ",";
        }
        if (uid == null) 
        {
            ErrorAlert("该组没有用户");
            return;
        }
        string script = "<script language='javascript' type='text/javascript'>"
                      + "popWindowJob(" + "'GroupUserJobReportUserResult.aspx','"
                      + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
                      + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
                      + UtilConst.REPORT_TYPE_USER.ToString() + "','"
                      + uid.Substring(0, uid.Length - 1) + "','"
                      + "0')"
                      + "</script>";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "popWindowJob", script);
    }
    #endregion



    #region "ExportCSV"

    protected void btnExportCSV_Click(object sender, EventArgs e)
    {
        string Name = User.Identity.Name;
        String MFPNumber = "";

      
        MFPNumber = this.Master.MFPTarget;
        DateTime StartPeriod = this.Master.StartDate;
        DateTime EndPeriod = this.Master.EndDate;


        string SearchTxt = "";
        if (!(ViewState["SearchTxt"] == null || string.IsNullOrEmpty(ViewState["SearchTxt"].ToString())))
        {
            SearchTxt = " WHERE GroupInfo.GroupName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
        }

        BllGroup bll = new BllGroup();
        List<JobInformationCSVModel> groupList = bll.getGroupReportCSVList(SearchTxt);
        if (groupList.Count == 0)
        {
            ErrorAlert(UtilConst.MSG_SELECT_EXPORT_NOTHING);
        }
        else
        {
            OutPutCsvFile("GroupUserJobReport", groupList, MFPNumber, StartPeriod, EndPeriod);
        }
    }
    private void OutPutCsvFile(string filename, List<JobInformationCSVModel> groupList, string MfpSerialNumber, DateTime StartPeriod, DateTime EndPeriod)
    {
        // 1.Get Date
        BllUser blluser = new BllUser();
        BllJobInformation bll = new BllJobInformation();
        //DateTime StartPeriod;
        //DateTime EndPeriod;
        //UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);
        //if (!(ViewState["StartPeriod"] == null || string.IsNullOrEmpty(ViewState["StartPeriod"].ToString())))
        //{
        //    string StartDate = ViewState["StartPeriod"].ToString();
        //    StartPeriod = Convert.ToDateTime(StartPeriod);
        //}
        //if (!(ViewState["EndPeriod"] == null || string.IsNullOrEmpty(ViewState["EndPeriod"].ToString())))
        //{
        //    string EndDate = ViewState["EndPeriod"].ToString();
        //    EndPeriod = Convert.ToDateTime(EndDate);
        //}

        Dictionary<string, JobInformationCSVModel> dict = bll.GetGroupJobDict(groupList, StartPeriod, EndPeriod, MfpSerialNumber, Dsp_Count_mode, Dsp_A3_A4);
        List<JobInformationCSVModel> jobdict = new List<JobInformationCSVModel>();

        JobInformationCSVModel allmoney = new JobInformationCSVModel();
        foreach(string key in dict.Keys)
        {
            JobInformationCSVModel group = dict[key];
            allmoney.Add(group);

            group.Type = 0;
            jobdict.Add( group);
            List<JobInformationCSVModel> userlist = blluser.getGroupUserVList(group.GroupID);

            Dictionary<string, JobInformationCSVModel> userJobdict = bll.GetUserJobDict(userlist, StartPeriod, EndPeriod, MfpSerialNumber, Dsp_Count_mode, Dsp_A3_A4);
            foreach(string id in userJobdict.Keys)
            {
                JobInformationCSVModel userJob = userJobdict[id];
                userJob.Type = 1;
                jobdict.Add(userJob);
            }
        }

        // 2.To Csv Date.
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("统计 - 用户、组使用报表");
        sb.Append("\r\n");
        sb.Append((UtilConst.CON_TARGET_MFP));
        sb.Append(",");
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
        sb.Append("用户组名,用户名,登录名,总使用量,黑白量合计,彩色量合计,复印,,,打印,,,扫描,,,传真,,文件归档打印,,,扫描并保存,,,清单打印,,,传真(Channel2),,网络传真,,其他,,");
        sb.Append("\r\n");
        sb.Append(",,,,,,黑白,彩色,合计,黑白,彩色,合计,黑白,彩色,合计,黑白,合计,黑白,彩色,合计,黑白,彩色,合计,黑白,彩色,合计,黑白,合计,黑白,合计,黑白,彩色,合计");
        sb.Append("\r\n");

        foreach (JobInformationCSVModel bean in jobdict)
        {
            string strOutPut = bean.getGroupUserJobData(Dsp_Count_mode);
            strOutPut = strOutPut + "\r\n";
            sb.Append(strOutPut);
        }

        //allmoney
        string strOutAll = allmoney.getGroupUserAllJobData(Dsp_Count_mode);
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
