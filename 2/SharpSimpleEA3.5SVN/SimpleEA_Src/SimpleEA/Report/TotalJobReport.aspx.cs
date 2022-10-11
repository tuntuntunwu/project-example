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
using BLL;
using System.Collections.Generic;
using Model;

/// <summary>
/// Total Job Report Screen.
/// </summary>
/// <Date>2010.06.21</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Report_TotalJobReport : ListMainPage
{
    private int Dsp_Count_mode = 0;
    private int Dsp_A3_A4 = 0;


    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.21</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;
        Dsp_A3_A4 = settingrow.Dis_A3_A4;

        bool isGroup = UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget);
        CustomersGridView.Columns[2].Visible = !isGroup;
        CustomersGridView.Columns[3].Visible = !isGroup;
        CustomersGridView.Columns[4].Visible = isGroup;
        CustomersGridView.Columns[5].Visible = isGroup;

        // Group List is visible
        this.Master.GroupDisplay = true;

        this.Master.Title = UtilConst.CON_PAGE_TOTALJOBREPORT;

        // Check Access Role
        CheckUser();

        // Total Taget's SelectIndexChanged Event.
        this.Master.SelectedIndexChanged_ddlMaster += new CommandEventHandler(Master_SelectedIndexChanged_ddlMaster);

        SqlDataListSource.ConnectionString = this.DBConnectionStrings;

        // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
        // Get Now Period
        // Start Period.
        DateTime StartPeriod;
        // End Period.
        DateTime EndPeriod;
        // Get Period Time By Now.
        //UtilCommon.GetPeriodBy(DateTime.Now, out StartPeriod, out EndPeriod);
        UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);
        // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST

        // 2010.12.09 Add By SES zhoumiao Ver.1.1 Update ST
        if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
        {
            //Search Item: Group Name
            this.Master.SearchItem = UtilConst.CON_PAGE_GROUPJOBREPORT;
        }
        else
        {
            //Search Item: User Name
            this.Master.SearchItem = UtilConst.CON_PAGE_USERJOBREPORT;
        }
        //2010.12.09 Add By SES zhoumiao Ver.1.1 Update ED

        string sql = string.Empty;
        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST
        //if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
        //{
        //    // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ST
        //    //sql = "   SELECT A.GroupName AS GroupName            " + Environment.NewLine;
        //    //sql += "         ,A.ID AS Id                         " + Environment.NewLine;
        //    //sql += "         ,'' AS UserName                     " + Environment.NewLine;
        //    //sql += "         ,'' AS LoginName                    " + Environment.NewLine;
        //    //sql += "    FROM  [GroupInfo] A                      " + Environment.NewLine;
        //    //// Add By KI 2010-07-09 ST
        //    //sql += "  WHERE ID <> {0}                             " + Environment.NewLine;
        //    //// Add By KI 2010-07-09 ED
        //    //sql += "ORDER BY A.ID                                " + Environment.NewLine;
        //    // sql = string.Format(sql, UtilConst.CON_DATE_ADMIN_ID);

        //    sql = "   SELECT A.GroupName AS GroupName"
        //        + " ,A.ID AS Id"
        //        + " ,''   AS UserName"
        //        + " ,''   AS LoginName"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.GroupID = A.ID"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS Total"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.GroupID = A.ID"
        //        + "	   AND JobInformation.FunctionID = 1"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS BWTotal"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.GroupID = A.ID"
        //        + "	   AND JobInformation.FunctionID = 2"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS FullTotal"
        //        + "    FROM  [GroupInfo] A"
        //        + "  WHERE ID <> {0}"
        //        + "ORDER BY A.ID";


        //    sql = string.Format(sql, UtilConst.CON_DATE_ADMIN_ID, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
        //    // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ED
        //}
        //else
        //{
        //    // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ST
        //    //sql = "SELECT                                       " + Environment.NewLine;
        //    //sql += "  UserInfo.ID               AS Id           " + Environment.NewLine;
        //    //sql += " ,UserName                  AS UserName     " + Environment.NewLine;
        //    //sql += " ,LoginName                 AS LoginName    " + Environment.NewLine;
        //    //sql += " ,GroupName                 AS GroupName    " + Environment.NewLine;
        //    //sql += " FROM [UserInfo] LEFT JOIN                  " + Environment.NewLine;
        //    //sql += "  [GroupInfo] ON GroupInfo.ID = GroupID     " + Environment.NewLine;
        //    //// Add BY JiJianxiong 2010-07-09 ST
        //    //sql += " WHERE UserInfo.ID <> {0}                   " + Environment.NewLine;
        //    //// Add BY JiJianxiong 2010-07-09 ED
        //    //sql += " ORDER BY UserInfo.ID " + Environment.NewLine;
        //    //sql = string.Format(sql, UtilConst.CON_DATE_ID);
        //    sql = "SELECT"
        //        + "  UserInfo.ID               AS Id"
        //        + " ,UserName                  AS UserName"
        //        + " ,LoginName                 AS LoginName"
        //        + " ,GroupName                 AS GroupName"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.UserID = UserInfo.ID"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS Total"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.UserID = UserInfo.ID"
        //        + "	   AND JobInformation.FunctionID = 1"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS BWTotal"
        //        + " , ISNULL(("
        //        + "	SELECT SUM(JobInformation.Number)"
        //        + "	  FROM JobInformation"
        //        + "	 WHERE JobInformation.UserID = UserInfo.ID"
        //        + "	   AND JobInformation.FunctionID = 2"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
        //        + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
        //        + "	),0) AS FullTotal"
        //        + " FROM [UserInfo] LEFT JOIN"
        //        + "  [GroupInfo] ON GroupInfo.ID = GroupID"
        //        + " WHERE UserInfo.ID <> {0}"
        //        + " ORDER BY UserInfo.ID";

        //    sql = string.Format(sql, UtilConst.CON_DATE_ID, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
        //    // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ED
        //}
        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            sql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {
            if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
            {              
                sql = GroupSql();
                sql += " ORDER BY A.ID";
                //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
               //sql = string.Format(sql, UtilConst.CON_DATE_ADMIN_ID, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
                sql = string.Format(sql, null, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
                //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
                
            }
            else
            {              
                sql = UserSql();
                sql += " ORDER BY UserInfo.ID";

                //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
                // sql = string.Format(sql, UtilConst.CON_DATE_ID, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
                sql = string.Format(sql, null, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
                //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
            }
        }
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;

        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED
        SqlDataListSource.SelectCommand = sql;

        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST
        // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ST
        //SetListMainPgae(this.CustomersGridView,
        //    this.ddlNumPerPage,
        //    this.ddlIndexOfPage,
        //    this.lblTotalPage,
        //    this.btnSelectAll, "UserName,LoginName,GroupName,Id");
        //SetListMainPgae(this.CustomersGridView,
        //    this.ddlNumPerPage,
        //    this.ddlIndexOfPage,
        //    this.lblTotalPage,
        //    this.btnSelectAll, "UserName,LoginName,GroupName,Id,Total,BWTotal,FullTotal");
        // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ED

        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            //this.btnSelectAll, "UserName,LoginName,GroupName,Id,Total,CopyTotal,FaxTotal,ScanTotal,OtherTotal,PrintTotal", lbtnNextPage, lbtnPrePage);
            this.btnSelectAll, "UserName,LoginName,GroupName,Id,GrayCopyTotal,FULLCopyTotal,GrayPrintTotal, FULLPrintTotal, ScanTotal,FaxTotal,OtherTotal,Total", lbtnNextPage, lbtnPrePage);
        

        // 2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED
        // Add By JiJianxiong 2010-07-09 ST
        if (!IsPostBack)
        {
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
            // Display setting for the TotalJobReport Screen.
            SetGridViewWidth();
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED

            if (this.CustomersGridView.Rows.Count > 0)
            {
                if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
                {
                    this.CustomersGridView.Sort("GroupName", SortDirection.Ascending);
                }
                else
                {
                    this.CustomersGridView.Sort("UserName", SortDirection.Ascending);
                }
            }
        }
        // Add By JiJianxiong 2010-07-09 ED

        // 2010.12.10 Update By SES zhoumiao Ver.1.1 Update ST
        this.Master.btn_Search_EAMaster().Click += new EventHandler(btn_SearchClick);
        this.Master.Btn_update().Click += new EventHandler(btn_updateClick);
        // 2010.12.10 Update By SES zhoumiao Ver.1.1 Update ED

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


        sql = "   SELECT A.GroupName AS GroupName"
            + " ,A.ID AS Id"
            + " ,''   AS UserName"
            + " ,''   AS LoginName"
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
        // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED
        //+ "    FROM  [GroupInfo] A"
        //    + "  WHERE ID <> {0}";
        + "    FROM  [GroupInfo] A";
        // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED

        return sql;
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
                + " ,LoginName                 AS LoginName"
                + " ,GroupName                 AS GroupName"
                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                + "	   AND JobInformation.JobID = 1"
                + "	   AND JobInformation.FunctionID = 1"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS GrayCopyTotal"

                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                + "	   AND JobInformation.JobID = 1"
                + "	   AND JobInformation.FunctionID = 2"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS FULLCopyTotal"

                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                //+ "	   AND JobInformation.JobID = 2"
                + "	   AND (JobInformation.JobID = 2 OR JobInformation.JobID = 3 OR JobInformation.JobID = 5)"
                + "	   AND JobInformation.FunctionID = 1"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS GrayPrintTotal"

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

                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	 WHERE JobInformation.UserID = UserInfo.ID"
                //+ "	   AND JobInformation.JobID = 6"
                + "	   AND (JobInformation.JobID = 4 OR JobInformation.JobID = 6 OR JobInformation.JobID = 7)"
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS ScanTotal"

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



                + " , ISNULL(("
                //+ "	SELECT SUM(JobInformation.SpendMoney)"
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
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED
            //+ "  [GroupInfo] ON GroupInfo.ID = GroupID"
            //+ " WHERE UserInfo.ID <> {0}";
        + "  [GroupInfo] ON GroupInfo.ID = GroupID";
        // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED


        return sql;
    }
    #endregion

    #region "Function:Display setting for the TotalJobReport Screen."
    /// <summary>
    /// Function:Display setting for the TotalJobReport Screen.
    /// </summary>
    /// <Date>2010.11.19</Date>
    /// <Author>SES Jijianxiong</Author>
    /// <Version>1.10</Version>
    public void SetGridViewWidth()
    {
        //20140224 tan DELETE & ADD START
        /*
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        // Display Setting
        //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST

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
        
        // Scan Total Column
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
        //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED

        //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST

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

        // Fax Total Column
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
        //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED

        // Get Display Count for the Columns
        // 1: Visible
        // 0: Divisible

        //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST

        //int displaycount = row.Dis_Job_Total + row.Dis_Job_BWTotal + row.Dis_Job_FCTotal;   2010.12.07 Update By SES.Jijianxiong Temp
        //int displaycount = row.Dis_Job_Total + row.Dis_Job_Total + row.Dis_Job_Total;
        //if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
        //{
        //    SetGridViewWidthGroupTotal(displaycount);
        //}
        //else
        //{
        //    SetGridViewWidthUserTotal(displaycount);
        //}

        int displaycount = row.Dis_Job_ScanTotal + row.Dis_Job_FaxTotal;

        if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
        {

            CustomersGridView.Columns[3].Visible = false;
            CustomersGridView.Columns[2].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[2].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[4].HeaderStyle.CssClass = "";
            CustomersGridView.Columns[4].ItemStyle.CssClass = "";
            SetGridViewWidthGroupTotal(displaycount);
        }
        else
        {
            CustomersGridView.Columns[3].Visible = false;
            CustomersGridView.Columns[2].HeaderStyle.CssClass = "";
            CustomersGridView.Columns[2].ItemStyle.CssClass = "";
            CustomersGridView.Columns[4].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[4].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            SetGridViewWidthUserTotal(displaycount);
        }
        //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED
        */

        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        int displaycount = row.Dis_Job_ScanTotal + row.Dis_Job_FaxTotal;
        SetGridViewWidthUserTotal(displaycount);
        //20140224 tan DELETE & ADD END
    }
    #endregion

    #region "Function:Display setting for the TotalJobReport Screen(User Total)."
    /// <summary>
    /// Function:Display setting for the TotalJobReport Screen(User Total).
    /// </summary>
    /// <param name="displaycount"></param>
    /// <Date>2010.11.19</Date>
    /// <Author>SES Jijianxiong</Author>
    /// <Version>1.10</Version>
    public void SetGridViewWidthUserTotal(int displaycount)
    {
        //20140424 tan DELETE START
        /*
        // All the Count Column's width is set by the aspx page. the width is the same 12%;
        // So only set the Other items width
        switch (displaycount)
        {
            //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST
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

            //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED
        }
        */
        //20140424 tan DELETE END


    }
    #endregion

    #region "Function:Display setting for the TotalJobReport Screen(Group Total)."
    /// <summary>
    /// Function:Display setting for the TotalJobReport Screen(Group Total).
    /// </summary>
    /// <param name="displaycount"></param>
    /// <Date>2010.11.19</Date>
    /// <Author>SES Jijianxiong</Author>
    /// <Version>1.10</Version>
    public void SetGridViewWidthGroupTotal(int displaycount)
    {
        //20140424 tan DELETE START
        /*
        // All the Count Column's width is set by the aspx page. the width is the same 12%;
        // So only set the Other items width
        switch (displaycount)
        {
            //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ST
            //case 0:
            //    // Normal Disply Mode
            //    CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUPTOTAL_0_GROUPNAME_WIDTH);
            //    break;
            //case 1:
            //    // Only One Count Column displayed.
            //    CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUPTOTAL_1_GROUPNAME_WIDTH);
            //    break;
            //case 2:
            //    // Only Two Count Column displayed.
            //    CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUPTOTAL_2_GROUPNAME_WIDTH);
            //    break;
            //case 3:
            //    // All Count Column displayed.
            //    CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_GROUPTOTAL_3_GROUPNAME_WIDTH);
            //    break;
            //default:
            //    break;

            case 0:
                // Normal Disply Mode
                CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_TOTAL_0_NAME_WIDTH);
                break;
            case 1:
                // Only One Count Column displayed.
                CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_TOTAL_1_NAME_WIDTH);
                break;
            case 2:
                //All Count Column displayed.
                CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_TOTAL_2_NAME_WIDTH);
                break;    
            default:
                break;
            //2010.12.09 Update By SES zhoumiao Ver.1.1 Update ED
        }
        */
        //20140424 tan DELETE START

    }
    #endregion

    #region "Occurs when a data row is bound to data in GridView."
    /// <summary>
    /// Occurs when a data row is bound to data in GridView.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.11</Date>
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
           ((BoundField)gridView.Columns[18]).HtmlEncode = false;
           ((BoundField)gridView.Columns[18]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
           ((BoundField)gridView.Columns[20]).HtmlEncode = false;
           ((BoundField)gridView.Columns[20]).DataFormatString = UtilConst.CON_MONEY_FORMAT;
           
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
           ((BoundField)gridView.Columns[18]).HtmlEncode = false;
           ((BoundField)gridView.Columns[18]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
           ((BoundField)gridView.Columns[20]).HtmlEncode = false;
           ((BoundField)gridView.Columns[20]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
       }
      //  ((BoundField)e.Row.Cells[3].Controls[0]).DataFormatString = "{0:2}";
        GridViewRow gRow = (GridViewRow)e.Row;
      
    
        //if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        //{
        //    // Set Color for even Row
        //    if (!Convert.ToBoolean(e.Row.RowIndex % 2))
        //    {
        //        e.Row.CssClass = UtilConst.CSS_ITEM_EVEN;
        //    }
        //}
    }
    #endregion

    #region "Master_SelectedIndexChanged_ddlMaster"
    /// <summary>
    /// Master_SelectedIndexChanged_ddlMaster
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.11</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Master_SelectedIndexChanged_ddlMaster(object sender, EventArgs e)
    {
        // 2010.12.20 Add By SES zhoumiao Ver.1.1 Update ST
        ViewState["SearchKeyWord"] = "";
        ViewState["SearchTxt"] = "";
        // 2010.12.20 Add By SES zhoumiao Ver.1.1 Update ST
        // 2010.12.10 Update By SES zhoumiao Ver.1.1 Update ST
        //this.ddlIndexOfPage.SelectedIndex = 0;
        //CustomersGridView.PageIndex = 0;
        //CustomersGridView.DataBind();

        ViewState["MFPNumber"] = "";
        ViewState["StartPeriod"] = "";
        ViewState["EndPeriod"] = "";
        this.Master.ddl_MFPItem().SelectedIndex = 0;
        this.Master.txt_Search_EAMaster().Text = "";
        this.RunSQL();
        // 2010.12.10 Update By SES zhoumiao Ver.1.1 Update ED

        if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
        {
            // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ST
            //// 2010.08.24 Update By SES JiJianXiong ST
            //// Change the columns id.
            //// CustomersGridView.Columns[1].Visible = false;
            //CustomersGridView.Columns[2].Visible = false;
            //CustomersGridView.Columns[3].Visible = false;
            //CustomersGridView.Columns[4].HeaderStyle.Width = new Unit("92%");
            //// 2010.08.24 Update By SES JiJianXiong ED

            CustomersGridView.Columns[2].Visible = false;
            CustomersGridView.Columns[3].Visible = false;

            SetGridViewWidth();
            // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ED

            if (this.CustomersGridView.Rows.Count > 0)
            {
                this.CustomersGridView.Sort("GroupName", SortDirection.Ascending);
            }

        }
        else
        {
            // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ST
            //// 2010.08.24 Update By SES JiJianXiong ST
            //// Change the columns id.
            //// CustomersGridView.Columns[1].Visible = true;
            //CustomersGridView.Columns[2].Visible = true;
            //CustomersGridView.Columns[3].Visible = true;
            //CustomersGridView.Columns[4].HeaderStyle.Width = new Unit("66%");
            //// 2010.08.24 Update By SES JiJianXiong ED
            CustomersGridView.Columns[2].Visible = true;
            CustomersGridView.Columns[3].Visible = true;

            SetGridViewWidth();
            // 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ED

            if (this.CustomersGridView.Rows.Count > 0)
            {
                this.CustomersGridView.Sort("UserName", SortDirection.Ascending);
            }
        }


    }
    #endregion

    // 2010.11.22 Delete By SES Jijianxiong Ver.1.1 Update ST
    //#region "btnCancel_Click"
    ///// <summary>
    ///// btnCancel_Click
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    ///// <Date>2010.06.11</Date>
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
    /// <Date>2010.06.24</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnItemCount_Click(object sender, EventArgs e)
    {
        // 1.Get ID List.
        string strId = "";
        string strIdList = "";
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ST
        string MfpSerialNumber = "";
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ED
        foreach (GridViewRow gRow in CustomersGridView.Rows)
        {
            CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
            if (ch.Checked)
            {
                // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ST
                //// 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ST
                ////if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
                ////{
                ////    // Update By SES.JiJianxiong 2010.08.25 ST
                ////    // strId = gRow.Cells[3].Text;
                ////    strId = gRow.Cells[5].Text;
                ////    // Update By SES.JiJianxiong 2010.08.25 ED
                ////}
                ////else
                ////{
                ////    // Update By SES.JiJianxiong 2010.08.25 ST
                ////    // strId = gRow.Cells[3].Text;
                ////    strId = gRow.Cells[5].Text;
                ////    // Update By SES.JiJianxiong 2010.08.25 ED
                ////}
                //strId = gRow.Cells[11].Text;
                //// 2010.11.19 Update By SES Jijianxiong Ver.1.1 Update ED

                //strId = gRow.Cells[15].Text;
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

            // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ST
            if (!(string.IsNullOrEmpty(this.Master.MFPTarget.ToString().Trim())))
            {
                MfpSerialNumber = this.Master.ddl_MFPItem().SelectedValue;
            }
            else
            {
                MfpSerialNumber = "0";
            }
            // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ED

            // Add By SES Ji.JianXiong 2010.08.31 ST
            // Base with the select item, is date exist.
            //20140717 delete by chen st
            //if (!IsDateExist(strIdList))
            //{
            //    return;
            //}
            //20140717 delete by chen ed
            // Add By SES Ji.JianXiong 2010.08.31 ED

            // 2.Report Page Open.
            string strScript = "<script language='javascript' type='text/javascript'>";
            if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
            {
                // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ST
                //strScript = strScript + "popWindow(" + "'TotalJobReportResult.aspx','"
                //      + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
                //      + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
                //      + UtilConst.REPORT_TYPE_TOTAL_GROUP.ToString() + "','"
                //      + strIdList + "')";
                strScript = strScript + "popWindowJob(" + "'TotalJobReportResult.aspx','"
                     + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
                     + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
                     + UtilConst.REPORT_TYPE_TOTAL_GROUP.ToString() + "','"
                     + strIdList + "','"
                     + MfpSerialNumber + "')";
                // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED

            }
            else
            {
                // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ST
                //strScript = strScript + "popWindow(" + "'TotalJobReportResult.aspx','"
                //      + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
                //      + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
                //      + UtilConst.REPORT_TYPE_TOTAL_USER.ToString() + "','"
                //      + strIdList + "')";
                strScript = strScript + "popWindowJob(" + "'TotalJobReportResult.aspx','"

                      + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
                      + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
                      + UtilConst.REPORT_TYPE_TOTAL_USER.ToString() + "','"
                      + strIdList + "','"
                      + MfpSerialNumber + "')";
                // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED
            }
            strScript = strScript + "</script>";
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ST
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "popWindow", strScript);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "popWindowJob", strScript);
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED
        }
        // Add By SES.JiJianxiong 2010.08.25 ST
        else
        {
            ErrorAlert(UtilConst.MSG_SELECT_NOTHING);
        }
        // Add By SES.JiJianxiong 2010.08.25 ED

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
        
        // 2010.08.24 Update By SES JiJianXiong ST
        // Change the columns id.
        cView.Columns[0].HeaderText = "";
        //cView.Columns[1].HeaderText = UtilConst.CON_ITEM_USERNAME;
        //cView.Columns[2].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        cView.Columns[2].HeaderText = UtilConst.CON_ITEM_USERNAME;
        cView.Columns[4].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        // 2010.08.24 Update By SES JiJianXiong ED

        // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ST
        //// 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
        //cView.Columns[6].HeaderText = UtilConst.CON_ITEM_TOTAL;
        //cView.Columns[8].HeaderText = UtilConst.CON_ITEM_BWTOTAL;
        //cView.Columns[10].HeaderText = UtilConst.CON_ITEM_FULLTOTAL;
        //// 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED
        cView.Columns[6].HeaderText = UtilConst.CON_ITEM_COPYTOTAL;
        cView.Columns[8].HeaderText = UtilConst.CON_ITEM_PRINTTOTAL;
        cView.Columns[10].HeaderText = UtilConst.CON_ITEM_SCANTOTAL;
        cView.Columns[12].HeaderText = UtilConst.CON_ITEM_FAXTOTAL;
        cView.Columns[14].HeaderText = UtilConst.CON_ITEM_TOTAL;

        // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ED

        if (cView.SortExpression.Equals(""))
        {
            return;
        }

        if ("UserName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_USERNAME;
            // 2010.08.24 Update By SES JiJianXiong ST
            // Change the columns id.
            // index = 1;
            index = 2;
            // 2010.08.24 Update By SES JiJianXiong ED
        }

        if ("GroupName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_GROUPNAME;
            // 2010.08.24 Update By SES JiJianXiong ST
            // Change the columns id.
            // index = 2;
            index = 4;
            // 2010.08.24 Update By SES JiJianXiong ED
        }
        // 2010.12.9 Update By SES Zhou Miao Ver.1.1 Update ST
        //// 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
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
        //// 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED
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

    #region "Is date exist"
    /// <summary>
    /// Is date exist
    /// </summary>
    /// <returns></returns>
    /// <Date>2010.08.31</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private bool IsDateExist(string sqlId)
    {
        string strSql = " SELECT Count(1) AS PageCount FROM [JobInformation]";
        strSql += " LEFT OUTER JOIN [PaperSizeInformation] ";
        strSql += " ON PaperSizeInformation.ID = JobInformation.PageID ";
        strSql += " WHERE SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= {0} ";
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {1} ";
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ST
        if (!(string.IsNullOrEmpty(this.Master.MFPTarget.ToString().Trim())))
        {
            strSql += " AND SerialNumber=" + ConvertStringToSQL(this.Master.MFPTarget.ToString());
        }
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ED
        if (this.Master.TotalTarget.Equals(UtilConst.CON_TOTALTARGET_GROUP))
        {
            strSql += "   AND GroupID IN ({2}) ";
        }
        else
        {
            strSql += "   AND UserID IN ({2}) ";
        }

        int dateCount = 0;

        strSql = string.Format(strSql, ConvertDateToSQL(this.Master.StartDate), ConvertDateToSQL(this.Master.EndDate), sqlId);
        //using (SqlDataReader reader = ExecuteReader(string.Format(strSql,
        //    ConvertDateToSQL(this.Master.StartDate), ConvertDateToSQL(this.Master.EndDate), sqlId)))
        using (SqlDataReader reader = ExecuteReader(strSql) )
        {
            while (reader.Read())
            {
                dateCount = (int)reader["PageCount"];
            }
        }

        // While Page Count is over 0 , Add to PageList.
        // ( UnDisplyed while Page Count is 0 )

        if (dateCount == 0)
        {
            // Each PageID's PageCount is 0 , show alert Message in Page.
            if (this.Master.TotalTarget.Equals(UtilConst.CON_TOTALTARGET_GROUP))
            {
                ErrorAlert(UtilConst.MSG_NODATE_GROUP);
            }
            else
            {
                ErrorAlert(UtilConst.MSG_NODATE_USER);
            }

            return false;
        }

        return true;
    }
    #endregion

    #region "btnSearch Clicked event"
    /// <summary>
    /// btnSearch Clicked  event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.10</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void btn_SearchClick(object sender, EventArgs e)
    {
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ST
        ViewState["SearchTxt"] = this.Master.txt_Search_EAMaster().Text.Trim();
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ED
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
    /// <Date>2010.12.10</Date>
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
    /// <Date>2010.12.10</Date>
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
            //this.btnSelectAll, "UserName,LoginName,GroupName,Id,GrayCopyTotal,FULLCopyTotal,GrayPrintTotal, FULLPrintTotal, ScanTotal,FaxTotal,OtherTotal,Total", lbtnNextPage, lbtnPrePage);
            this.btnSelectAll, "UserName,LoginName,GroupName,Id,GrayCopyTotal,FULLCopyTotal,GrayPrintTotal, FULLPrintTotal, ScanTotal,FaxTotal,OtherTotal,Total", lbtnNextPage, lbtnPrePage);

        if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
        {
            this.CustomersGridView.Sort("GroupName", SortDirection.Ascending);
        }
        else
        {
            this.CustomersGridView.Sort("UserName", SortDirection.Ascending);
        }

    }
    #endregion

    #region"Function: Sql is Judged "
    /// <summary>
    /// Function: Sql is Judged 
    /// </summary>
    /// <returns>Sql</returns>
    /// <Date>2010.12.10</Date>
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


        if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
        {

            sql = GroupSql();
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ST
            //if (!(string.IsNullOrEmpty(this.Master.txt_Search_EAMaster().Text.Trim())))
            //{

            //    sql += " AND GroupName LIKE " + ConvertStringToSQL("%" + this.Master.txt_Search_EAMaster().Text.Trim() + "%");

            //}
            if (!(ViewState["SearchTxt"] == null || string.IsNullOrEmpty(ViewState["SearchTxt"].ToString())))
            {

                //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ST
                // sql += " AND GroupName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
                sql += " WHERE GroupName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
                //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ED

            }
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED
            sql += " ORDER BY A.ID";


           
            //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ST
            // sql = string.Format(sql, UtilConst.CON_DATE_ADMIN_ID, StartDate, EndDate);
            sql = string.Format(sql, null, StartDate, EndDate);
            //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ED

        }
        else
        {

            sql = UserSql();
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ST
            //if (!(string.IsNullOrEmpty(this.Master.txt_Search_EAMaster().Text.Trim())))
            //{

            //    sql += " AND UserName LIKE " + ConvertStringToSQL("%" + this.Master.txt_Search_EAMaster().Text.Trim() + "%");

            //}
            if (!(ViewState["SearchTxt"] == null || string.IsNullOrEmpty(ViewState["SearchTxt"].ToString())))
            {
                //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ST
                //sql += " AND UserName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
                sql += " WHERE UserName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
                //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ED
                

            }
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED           
            sql += " ORDER BY UserInfo.ID";

            
            //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ST
            //sql = string.Format(sql, UtilConst.CON_DATE_ID, StartDate, EndDate);
            sql = string.Format(sql, null, StartDate, EndDate);
            //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ED
        }

        ViewState["SearchKeyWord"] = sql;

        return sql;
    }
    #endregion


    #region "ExportCSV"

    protected void btnExportCSV_Click(object sender, EventArgs e)
    {
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

        Dictionary<string, int> jobTypeDict = new Dictionary<string, int>();
        jobTypeDict.Add(UtilConst.ITEM_TITLE_Copy, UtilConst.ITEM_TITLE_Copy_JobId);
        jobTypeDict.Add(UtilConst.ITEM_TITLE_Print, UtilConst.ITEM_TITLE_Print_JobId);
        jobTypeDict.Add(UtilConst.ITEM_TITLE_DFPrint, UtilConst.ITEM_TITLE_DFPrint_JobId);
        jobTypeDict.Add(UtilConst.ITEM_TITLE_ScanSave, UtilConst.ITEM_TITLE_ScanSave_JobId);
        jobTypeDict.Add(UtilConst.ITEM_TITLE_ListPrint, UtilConst.ITEM_TITLE_ListPrint_JobId);
        jobTypeDict.Add(UtilConst.ITEM_TITLE_Scan, UtilConst.ITEM_TITLE_Scan_JobId);
        jobTypeDict.Add(UtilConst.ITEM_TITLE_Fax, UtilConst.ITEM_TITLE_Fax_JobId);
        jobTypeDict.Add(UtilConst.ITEM_TITLE_FaxC2, UtilConst.ITEM_TITLE_FaxC2_JobId);
        jobTypeDict.Add(UtilConst.ITEM_TITLE_IntFax, UtilConst.ITEM_TITLE_IntFax_JobId);
        jobTypeDict.Add(UtilConst.ITEM_TITLE_Other, UtilConst.ITEM_TITLE_Other_JobId);

        string SearchTxt = "";
        if (UtilConst.CON_TOTALTARGET_GROUP.Equals(this.Master.TotalTarget))
        {

            if (!(ViewState["SearchTxt"] == null || string.IsNullOrEmpty(ViewState["SearchTxt"].ToString())))
            {
                SearchTxt = " WHERE GroupInfo.GroupName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
            }
            SearchTxt = SearchTxt + " ORDER BY GroupInfo.ID";
            BllGroup bll = new BllGroup();
            List<JobInformationCSVModel> groupList = bll.getGroupReportCSVList(SearchTxt);
            if (groupList.Count == 0)
            {
                ErrorAlert(UtilConst.MSG_SELECT_EXPORT_NOTHING);
            }
            else
            {
                OutPutCsvFile("TotalJobReport", 1, groupList, jobTypeDict, MFPNumber, StartPeriod, EndPeriod);
            }
        }
        else
        {
            if (!(ViewState["SearchTxt"] == null || string.IsNullOrEmpty(ViewState["SearchTxt"].ToString())))
            {
                SearchTxt = " WHERE UserInfo.UserName LIKE " + ConvertStringToSQL("%" + ViewState["SearchTxt"].ToString() + "%");
            }
            SearchTxt = SearchTxt + " ORDER BY UserInfo.ID";

            BllUser bll = new BllUser();
            List<JobInformationCSVModel> userList = null;
            userList = bll.getUserReportCSVList(SearchTxt);

            if (userList.Count == 0)
            {
                ErrorAlert(UtilConst.MSG_SELECT_EXPORT_NOTHING);
            }
            else
            {

                OutPutCsvFile("TotalJobReport", 2, userList, jobTypeDict, MFPNumber, StartPeriod, EndPeriod);
            }
        }

       

       
    }
    private void OutPutCsvFile(string filename, int flg, List<JobInformationCSVModel> idList, Dictionary<string, int> jobTypeDict, string MFPNumber, DateTime StartPeriod, DateTime EndPeriod)
    {
        // 1.Get Date
        BllJobInformation bll = new BllJobInformation();

        string strIdList = "";
        string strUserName = "";
        if( flg == 1 ) //组
        {
            foreach (JobInformationCSVModel bean in idList)
            {
                if ("".Equals(strIdList))
                {
                    strIdList = bean.GroupID.ToString();
                    //strUserName = bean.GroupName;
                }
                else
                {
                    strIdList += "," + bean.GroupID.ToString();
                    //strUserName += "," + bean.GroupName;
                }
            }
        }else{  //用户
            foreach (JobInformationCSVModel bean in idList)
            {
                if ("".Equals(strIdList))
                {
                    strIdList = bean.UserID.ToString();
                    //strUserName = bean.UserName;
                }
                else
                {
                    strIdList += "," + bean.UserID.ToString();
                    //strUserName += "," + bean.UserName;
                }
            }
        }
        Dictionary<string, JobTypeInfoCSVModel> dict = bll.GetJobTypeDict(flg, strIdList, jobTypeDict, StartPeriod, EndPeriod, MFPNumber, Dsp_Count_mode, Dsp_A3_A4);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(UtilConst.CON_PAGE_TOTALREPORT);
        sb.Append("\r\n");
        //sb.Append(UtilConst.CON_TARGET_ALL);
        if( flg == 1 ) //组
        {
            sb.Append(UtilConst.CON_TARGET_GROUP);
        }
        else
        {
            sb.Append(UtilConst.CON_TARGET_USER);
        }

        sb.Append(strUserName);
        sb.Append("\r\n");

        sb.Append((UtilConst.CON_TARGET_MFP));
        sb.Append("，");
        if (MFPNumber != null && MFPNumber.Length > 0)
        {
            sb.Append(MFPNumber);
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
        sb.Append("  , 总使用量");
        sb.Append("\r\n");

        double allmoney = 0;
        foreach (string key in dict.Keys)
        {
            JobTypeInfoCSVModel bean = dict[key];
            string strOutPut = key;
            strOutPut = strOutPut + ",";
            strOutPut = strOutPut + bean.getReportData(Dsp_Count_mode) + "\r\n";
            sb.Append(strOutPut);
            allmoney += bean.AllMoney;
        }
        string strlastOutPut = "合计";
        strlastOutPut = strlastOutPut + ",";
        strlastOutPut = strlastOutPut + UtilCommon.doubleToMoney(allmoney, Dsp_Count_mode) +"\r\n";
        sb.Append(strlastOutPut);


        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".csv");

        Response.ContentType = "application/text";

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

        Response.Write(sb);

        Response.End();

    }

    #endregion
}
