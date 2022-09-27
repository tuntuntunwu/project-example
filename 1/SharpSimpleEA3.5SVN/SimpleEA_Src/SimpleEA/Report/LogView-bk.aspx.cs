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
using dtLogInformationTableAdapters;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// Available Report Screen.
/// </summary>
/// <Date>2010.12.14</Date>
/// <Author>SES Zhou Miao</Author>
/// <Version>1.1</Version>
public partial class LogInfo_LogView : ListMainPage
{

    int Dsp_Count_mode = 0;
    int Dsp_A3_A4 = 0;

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_LOG;


        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;
        Dsp_A3_A4 = settingrow.Dis_A3_A4;

        // 2010.12.22 Add By SES Jijianxiong Check Access Role ST
        // Check Access Role
        CheckUser();
        // 2010.12.22 Add By SES Jijianxiong Check Access Role ED

        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        SqlDataListSource.ConnectionString = this.DBConnectionStrings;
        // Get Now Period
        // Start Period.
        DateTime StartPeriod;     
        // End Period.
        DateTime EndPeriod;      ;
        // Get Period Time By Now.
        //UtilCommon.GetPeriodBy(DateTime.Now, out StartPeriod, out EndPeriod);
        UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);

        string sql = "";

        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            sql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {
            sql = LogViewSql();
            sql += " WHERE  SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {0}" + Environment.NewLine
                    + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {1}";
            sql += " AND  Status ='5'";
            sql = string.Format(sql, ConvertDateToSQL(StartPeriod), ConvertDateToSQL(EndPeriod));
            
            if (row.Dis_Log_MaxCount != 0)
            {  //Max display record count  setted
                IsMoreLimit(row.Dis_Log_MaxCount, sql);
                // 2010.12.21 Delete By SES Zhou Miao Ver.1.1 Update ST
               // sql = "SELECT TOP " + row.Dis_Log_MaxCount + "* FROM (" + sql + ")LIM ORDER BY Id DESC";
                sql = "SELECT TOP " + row.Dis_Log_MaxCount + "* FROM (" + sql + ")LIM";
                // 2010.12.21 Delete By SES Zhou Miao Ver.1.1 Update ED
                
            }
            // 2010.12.21 Delete By SES Zhou Miao Ver.1.1 Update ST
            //// 2010.12.20 Add By SES Zhou Miao Ver.1.1 Update ST
            //else
            //{
            //    sql += "ORDER BY Id DESC";
            //}
            //// 2010.12.20 Add By SES Zhou Miao Ver.1.1 Update ED
            // 2010.12.21 Delete By SES Zhou Miao Ver.1.1 Update ED
        }
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;

        SqlDataListSource.SelectCommand = sql;
        

        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            this.btnSelectAll, "ProcessTime,UserName,MFPModelName,MFPSerialNumber,JobType,ColorMode,case pagecount when 1   then   '单面' else '双面' end as pagecount,Status,Id,MFPPrintTaskID,Duplex,PriceDetailID,Cost,SheetCount,PapeCount,CopyCount,PageID,CheckCopy,FileName");

        if (!IsPostBack)
        {
            if (this.CustomersGridView.Rows.Count > 0)
            {
                // 2010.12.23 Update By SES Zhou Miao Ver.1.1 Update ST
               // // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ST
               // //// 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ST
               // ////// 2010.12.20 Update By SES Zhou Miao Ver.1.1 Update ST
               // //////this.CustomersGridView.Sort("ProcessTime", SortDirection.Ascending);
               // ////this.CustomersGridView.Sort("ProcessTime", SortDirection.Descending);
               // ////// 2010.12.20 Update By SES Zhou Miao Ver.1.1 Update ED
               // //this.CustomersGridView.Sort("Time", SortDirection.Descending);
               // //// 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ED
               /// this.CustomersGridView.Sort("ProcessTime", SortDirection.Ascending);
               // // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ED
                this.CustomersGridView.Sort("ProcessTime", SortDirection.Descending);
                // 2010.12.23 Update By SES Zhou Miao Ver.1.1 Update ED
            }
        }

        this.Master.Btn_Search().Click += new EventHandler(Btn_SearchClick);           

    }
    #endregion

    #region "BtnSearch Clicked event"
    /// <summary>
    /// BtnSearch Clicked  event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.15</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void Btn_SearchClick(object sender, EventArgs e)
    {
        ViewState["UserLogin"] = this.Master.UserLogin;
        ViewState["MFPNumber"] = this.Master.MFPTarget;
        ViewState["StatusTarget"] = this.Master.StatusTarget;
        ViewState["JobtypeTarget"] = this.Master.JobtypeTarget;
        ViewState["StartPeriod"] = ConvertDateToSQL(this.Master.StartDate);
        ViewState["EndPeriod"] = ConvertDateToSQL(this.Master.EndDate);

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


    //明细表示按钮
    #region "btnExecute_Click"
    /// <summary>
    /// btnExecute_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.16</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        // 1.Get select ID List.
        string strIdList = SelectstrIdList();


        if (strIdList != "")
        {
            // 2.Report Page Open.
            string strScript = "<script language='javascript' type='text/javascript'>";
            strScript = strScript + "popWindow(" + "'LogViewReportResult.aspx','"
                  + this.Master.StartDate.ToString(UtilConst.TIME_FORMAT) + "','"
                  + this.Master.EndDate.ToString(UtilConst.TIME_FORMAT) + "','"
                  + UtilConst.REPORT_TYPE_LOG.ToString() + "','"
                  + strIdList + "')";
            strScript = strScript + "</script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "popWindow", strScript);

        }
        else
        {
            ErrorAlert(UtilConst.MSG_SELECT_NOTHING);
        }

    }
    #endregion    
    //CSV文件导出（按钮事件）
    #region "btnCSV_Click"
    /// <summary>
    /// btnCSV_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.16</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    /// 

   
    protected void btnCSVOutPut_Click(object sender, EventArgs e)
    {
        string strIdList = "";
        String sql = "";
        strIdList = SelectstrIdList();

        if (!String.IsNullOrEmpty(strIdList))
        {
            sql = LogViewCSVSql();
            // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ST
            //// 2010.12.20 Update By SES Zhou Miao Ver.1.1 Update ST
            ////sql += "WHERE ID  IN ({0})  ORDER BY ID ";
            //sql += "WHERE ID  IN ({0})  ORDER BY Time DESC ";
            //// 2010.12.20 Update By SES Zhou Miao Ver.1.1 Update ED
            sql += "WHERE ID  IN ({0}) ";
            switch (this.CustomersGridView.SortExpression)
            {
                // 2010.12.23 Update By SES Zhou Miao Ver.1.1 Update ST
                //case "Time":
                case "ProcessTime":
                // 2010.12.23 Update By SES Zhou Miao Ver.1.1 Update ST
                    sql += " ORDER BY  Time";
                    break;
                case "UserName":
                    sql += " ORDER BY  用户名";
                    break;
                case "MFPModelName":
                    sql += " ORDER BY  MFP型号";
                    break;
                case "MFPSerialNumber":
                    sql += " ORDER BY  MFP序列号";
                    break;
                case "JobType":
                    sql += " ORDER BY  操作类型";
                    break;
                case "ColorMode":
                    sql += " ORDER BY  色彩";
                    break;
                //chen 20140514 add start
                case "Duplex":
                    sql += " ORDER BY  单双面";
                    break;
                case "PapeCount":
                    sql += " ORDER BY  张数";
                    break;
                case "CopyCount":
                    sql += " ORDER BY  份数";
                    break;
                case "Cost":
                    sql += " ORDER BY  使用金额";
                    break;
                //chen 20140514 add end
                case "PageCount":
                    //2011.3.23 Update By SES zhoumiao Ver.1.1 Update  ST
                    //sql += " ORDER BY  页数";
                    sql += " ORDER BY  面数";
                    //2011.3.23 Update By SES zhoumiao Ver.1.1 Update  ED
                    break;
                case "Status":
                    sql += " ORDER BY  状态";
                    break;
                default:
                    break;

            }
            if (SortDirection.Descending.Equals(this.CustomersGridView.SortDirection))
            {
                sql += " DESC";
            }
            // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ED
            DataTable tblDetail = ExecuteDataTable(string.Format(sql, strIdList));
            // 2010.12.20 Update By SES Zhou Miao Ver.1.1 Update ST
           // OutPutCsvFile("LogViewReport ", tblDetail);
            OutPutCsvFile("LogView", tblDetail);
            // 2010.12.20 Update By SES Zhou Miao Ver.1.1 Update ST
        }
        else
        {
            ErrorAlert(UtilConst.MSG_SELECT_NOTHING_CSV);
        }
    }
    #endregion
    //CSV文件导出函数
    #region "OutPutCsvFile"
    /// <summary>
    /// OutPutCsvFile
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="detail"></param>
    /// <Date>2010.12.16</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private void OutPutCsvFile(string filename, DataTable detail)
    {
        // 1.Get Date
        List<List<string>> CsvList = GetCsvDate(detail);

        // 2.To Csv Date.
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (List<string> item in CsvList)
        {
            string strOutPut = null;
            foreach (string strItem in item)
            {
                if (strOutPut == null)
                {
                    strOutPut = strItem;
                }
                else
                {
                    strOutPut = strOutPut + "," + strItem;
                }
            }
            strOutPut = strOutPut + "\r\n";
            sb.Append(strOutPut);
        }

        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".csv");

        Response.ContentType = "application/text";

        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

        Response.Write(sb);

        Response.End();

    }
    #endregion

    #region "Occurs when a data row is bound to data in GridView."
    /// <summary>
    /// Occurs when a data row is bound to data in GridView.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void CustomView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Check Row Records
        // Defulat Date Can't be Delete
        // User's Admin
        // Group's no belong
        GridView gridView = (GridView)sender;

        if (Dsp_Count_mode == 0)
        {
            ((BoundField)gridView.Columns[24]).HtmlEncode = false;
            ((BoundField)gridView.Columns[24]).DataFormatString = UtilConst.CON_MONEY_FORMAT;


        }
        else
        {
            ((BoundField)gridView.Columns[24]).HtmlEncode = false;
            ((BoundField)gridView.Columns[24]).DataFormatString = UtilConst.CON_PAPER_FORMAT;
        }

        GridViewRow gRow = (GridViewRow)e.Row;

   

    }
    #endregion

    #region"Function:LogView SQL Search"
    /// <summary>
    /// Function:LogView SQL Search
    /// </summary>
    /// <returns></returns>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public String LogViewSql()
    {
        String sql = "";

        //chen update 20140624 start
        string selNumber = "";
        string sqlPageCount = "";
        string sqlConst = "";
        if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A4))
        {
            selNumber = ",Number  AS SheetCount";
            sqlPageCount = ",PapeCount  AS PapeCount";
            if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
            {
                sqlConst = "  ,SpendMoney           AS Cost ";
            }
            else
            {
                sqlConst = "  ,Number           AS Cost ";
            }
        }
        else
        {
            selNumber = ",DspNumber  AS SheetCount";
            sqlPageCount = ",DspPapeCount  AS PapeCount";
            if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
            {
                sqlConst = "  ,SpendMoney           AS Cost ";
            }
            else
            {
                sqlConst = "  ,DspNumber           AS Cost ";
            }
        }

        //chen update 20140624 end


        sql = "SELECT"
                + "  ID                    AS Id" + Environment.NewLine
            //+ "  ,case Duplex when 1   then   '单面' else '双面' end            AS Duplex " + Environment.NewLine
                 + "  ,case Duplex when 1   then   '单面' when 2 then '双面' when 3 then '双面' when 4 then '双面' when 5 then '双面' else ' ' end            AS Duplex " + Environment.NewLine

                + "  ,PriceDetailID        AS PriceDetailID " + Environment.NewLine
            //+ "  ,SpendMoney           AS Cost " + Environment.NewLine
                + sqlConst + Environment.NewLine
            //+ "  ,PapeCount            AS PapeCount " + Environment.NewLine
                + sqlPageCount + Environment.NewLine
                + "  ,CopyCount            AS CopyCount " + Environment.NewLine
            // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ST
            //// 2010.12.21 Add By SES Zhou Miao Ver.1.1 Update ST
            //    + " ,Time               AS Time"                                               + Environment.NewLine
            //// 2010.12.21 Add By SES Zhou Miao Ver.1.1 Update ED
            //    //ProcessTime
            //    + " ,REPLACE(CONVERT(VARCHAR,Time ,20),'-','/')   AS ProcessTime"              + Environment.NewLine
            //ProcessTime
                + " ,Time   AS ProcessTime"
                + " ,Filename   " + Environment.NewLine
            // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ED   
            //UserName
            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ST
            //+ " ,ISNULL((SELECT UserInfo.UserName  FROM UserInfo"                          + Environment.NewLine
            //+ "  WHERE UserInfo.ID = LogInformation.UserID), '未知') as UserName"          + Environment.NewLine
                 + " ,UserName  AS UserName" + Environment.NewLine

            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ED  

                //MFP  ModelName
                + ",MFPModel   AS  MFPModelName" + Environment.NewLine
            //Get Spl file ID
                + ",MFPPrintTaskID   AS  MFPPrintTaskID" + Environment.NewLine
            //MFP SerialNumber
                + ",SerialNumber   AS MFPSerialNumber" + Environment.NewLine
            //Job Type
                + " ,JobType=CASE WHEN LogInformation.JobID='8' " + Environment.NewLine
                + "  AND LogInformation.FunctionID='2'" + Environment.NewLine
                + "  THEN (SELECT JobTypeInformation.JobNameDisp" + Environment.NewLine
                + "  + FunctionTypeInformation.FunctionNameDisp"
                + "  FROM FunctionTypeInformation,JobTypeInformation" + Environment.NewLine
                + "  WHERE FunctionTypeInformation.JobID = JobTypeInformation.ID" + Environment.NewLine
                + "  AND FunctionTypeInformation.FunctionID = LogInformation.FunctionID" + Environment.NewLine
                + "  AND JobTypeInformation.ID = '8')" + Environment.NewLine
            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ST
            //+ "  ELSE (SELECT JobTypeInformation.JobNameDisp "                             + Environment.NewLine
            //+ "  FROM JobTypeInformation"                                                  + Environment.NewLine
            //+ "  WHERE JobTypeInformation.ID = LogInformation.JobID)"                      + Environment.NewLine
                + "  ELSE ISNULL((SELECT JobTypeInformation.JobNameDisp " + Environment.NewLine
                + "  FROM JobTypeInformation" + Environment.NewLine
                + "  WHERE JobTypeInformation.ID = LogInformation.JobID), '未知')" + Environment.NewLine
            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ED 
                + "  END" + Environment.NewLine
            //Color Mode
                + " ,ColorMode=CASE WHEN LogInformation.JobID='8' " + Environment.NewLine
                + "  AND LogInformation.FunctionID='2'" + Environment.NewLine
                + "  THEN '黑白'" + Environment.NewLine
                + "  WHEN LogInformation.FunctionID IS NULL" + Environment.NewLine
                + "  THEN '-'" + Environment.NewLine
                + "  ELSE (SELECT FunctionTypeInformation.FunctionNameDisp " + Environment.NewLine
                + "  FROM FunctionTypeInformation" + Environment.NewLine
                + "  WHERE FunctionTypeInformation.JobID = LogInformation.JobID" + Environment.NewLine
                + "  AND FunctionTypeInformation.FunctionID = LogInformation.FunctionID)" + Environment.NewLine
                + "  END" + Environment.NewLine
            //Page Count
            //+ ",Number  AS SheetCount" + Environment.NewLine  
                + selNumber + Environment.NewLine
            // Status
                + " ,Status=CASE WHEN LogInformation.Status ='0'" + Environment.NewLine
                + "  THEN '开始'" + Environment.NewLine
                + "  WHEN LogInformation.Status ='3'" + Environment.NewLine
                + "  THEN '取消'" + Environment.NewLine
                + "  WHEN LogInformation.Status ='4'" + Environment.NewLine
                + "  THEN '挂起'" + Environment.NewLine
                + "  WHEN LogInformation.Status ='5'" + Environment.NewLine
                + "  THEN '完成'" + Environment.NewLine
                + "  ELSE '错误'" + Environment.NewLine
                + "  END" + Environment.NewLine
                + ",PageID=CASE WHEN PageID IS NULL" + Environment.NewLine
                + "  THEN '-'" + Environment.NewLine
                + "  ELSE (SELECT PaperName " + Environment.NewLine
                + "  FROM PaperSizeInformation" + Environment.NewLine
                + "  WHERE ID = LogInformation.PageID)" + Environment.NewLine
                + "  END "

                //checkcopy//11
                + " ,CheckCopy=CASE WHEN LogInformation.FileName is null" + Environment.NewLine
                + "  THEN ''" + Environment.NewLine
                + "  ELSE '查看'" + Environment.NewLine
                + "  END" + Environment.NewLine
                + "  FROM [LogInformation]" + Environment.NewLine;

        return sql;

    }
    #endregion

    #region"Function:LogView CSV  DataTable SQL Search"
    /// <summary>
    /// Function:LogView CSV DataTable SQL Search
    /// </summary>
    /// <returns></returns>
    /// <Date>2010.12.16</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public String LogViewCSVSql()
    {

        //chen update 20140624 start
        string selNumber = "";
        string sqlPageCount = "";
        string sqlConst = "";
        if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A4))
        {
            selNumber = ",LogInformation.Number  AS \"面数\" ";
            sqlPageCount = ",LogInformation.PapeCount  AS \"张数\" ";
            if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
            {
                sqlConst = "  ,LogInformation.SpendMoney           AS \"总计\" ";
            }
            else
            {
                sqlConst = "  ,LogInformation.Number           AS \"总计\" ";
            }
        }
        else
        {
            selNumber = ",LogInformation.DspNumber  AS \"面数\" ";
            sqlPageCount = ",LogInformation.DspPapeCount  AS \"张数\" ";
            if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
            {
                sqlConst = "  ,LogInformation.SpendMoney           AS \"总计\" ";
            }
            else
            {
                sqlConst = "  ,LogInformation.DspNumber           AS \"总计\" ";
            }
        }

        //chen update 20140624 end
        
        String sql = "";

        sql = "SELECT"
           
                + "  ID                    AS Id"                                              + Environment.NewLine
            // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ST
                //Process Time
                //+ " ,replace(convert(varchar,Time ,20),'-','/')   AS \"操作时间\""             + Environment.NewLine
                + " ,Time     AS \"操作时间\""                                                 + Environment.NewLine
            // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ED
                //UserName              
            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ST
            //+ " ,ISNULL((SELECT UserInfo.UserName  FROM UserInfo"                          + Environment.NewLine
            //+ "  WHERE UserInfo.ID = LogInformation.UserID), '未知') as \"用户名\""        + Environment.NewLine
                 + " ,UserName  AS \"用户名\""                                               + Environment.NewLine
            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ED 
               //MFP Model Name
                + ",MFPModel   AS  \"MFP型号\""                                                + Environment.NewLine
                + ",FileName   " + Environment.NewLine
            //MFP Serial Number
                + ",SerialNumber   AS \"MFP序列号\"" + Environment.NewLine
            //Job Type
                + " ,\"操作类型\"=CASE WHEN LogInformation.JobID='8' " + Environment.NewLine
                + "  AND LogInformation.FunctionID='2'" + Environment.NewLine
                + "  THEN (SELECT JobTypeInformation.JobNameDisp" + Environment.NewLine
                + "  + FunctionTypeInformation.FunctionNameDisp"
                + "  FROM FunctionTypeInformation,JobTypeInformation" + Environment.NewLine
                + "  WHERE FunctionTypeInformation.JobID = JobTypeInformation.ID" + Environment.NewLine
                + "  AND FunctionTypeInformation.FunctionID = LogInformation.FunctionID" + Environment.NewLine
                + "  AND JobTypeInformation.ID = '8')" + Environment.NewLine
            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ST
            //+ "  ELSE (SELECT JobTypeInformation.JobNameDisp "                             + Environment.NewLine
            //+ "  FROM JobTypeInformation"                                                  + Environment.NewLine
            //+ "  WHERE JobTypeInformation.ID = LogInformation.JobID)"                      + Environment.NewLine
                + "  ELSE ISNULL((SELECT JobTypeInformation.JobNameDisp " + Environment.NewLine
                + "  FROM JobTypeInformation" + Environment.NewLine
                + "  WHERE JobTypeInformation.ID = LogInformation.JobID), '未知')" + Environment.NewLine
            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ED
                + "  END" + Environment.NewLine

                //Color Mode
                + " ,\"色彩\"=CASE WHEN LogInformation.JobID='8' " + Environment.NewLine
                + "  AND LogInformation.FunctionID='2'" + Environment.NewLine
                + "  THEN '黑白'" + Environment.NewLine
                + "  WHEN LogInformation.FunctionID IS NULL" + Environment.NewLine
                + "  THEN '-'" + Environment.NewLine
                + "  ELSE (SELECT FunctionTypeInformation.FunctionNameDisp " + Environment.NewLine
                + "  FROM FunctionTypeInformation" + Environment.NewLine
                + "  WHERE FunctionTypeInformation.JobID = LogInformation.JobID" + Environment.NewLine
                + "  AND FunctionTypeInformation.FunctionID = LogInformation.FunctionID)" + Environment.NewLine
                + "  END"    

                //+ ",Duplex as \"单双面\""                                                      + Environment.NewLine
                 + "  ,case Duplex when 1   then   '单面' when 2 then '双面' when 3 then '双面' when 4 then '双面' when 5 then '双面' else ' ' end            AS  \"单双面\"  " + Environment.NewLine

            //Page Count
            //2011.3.23 Update By SES zhoumiao Ver.1.1 Update  ST
            //+ " ,\"页数\"=CASE WHEN LogInformation.Number IS NULL "                        + Environment.NewLine
             //      + " ,\"面数\"=CASE WHEN LogInformation.Number IS NULL " + Environment.NewLine
            //2011.3.23 Update By SES zhoumiao Ver.1.1 Update  ED
                //+ "  THEN '-'  ELSE" + Environment.NewLine
                //+ "  REPLACE(CONVERT(varchar, CONVERT(money, Number), 1),'.00','')" + Environment.NewLine
                //+ "  END"  
            //+ " ,\"页数\"=CASE WHEN LogInformation.Number IS NULL "                        + Environment.NewLine
            + selNumber  + Environment.NewLine
            //chen add 20140514 start
                //   + " ,\"张数\"=CASE WHEN LogInformation.PapeCount IS NULL " + Environment.NewLine
                //+ "  THEN '-'  ELSE" + Environment.NewLine
                //+ "  REPLACE(CONVERT(varchar, CONVERT(money, PapeCount), 1),'.00','')" + Environment.NewLine
                //+ "  END" + Environment.NewLine
                   + " ,\"份数\"=CASE WHEN LogInformation.CopyCount IS NULL " + Environment.NewLine
                + "  THEN '-'  ELSE" + Environment.NewLine
                + "  REPLACE(CONVERT(varchar, CONVERT(money, CopyCount), 1),'.00','')" + Environment.NewLine
                + "  END" + Environment.NewLine
            + sqlPageCount + Environment.NewLine
            //chen add 20140514 end
            //+ ",SpendMoney as \"使用金额\""                                                + Environment.NewLine
                //+ " ,\"使用金额\"=CASE WHEN LogInformation.SpendMoney IS NULL " + Environment.NewLine
                //+ "  THEN '-'  ELSE" + Environment.NewLine
                //+ "  REPLACE(CONVERT(varchar, CONVERT(money, SpendMoney), 1),'.00','')" + Environment.NewLine
                //+ "  END" + Environment.NewLine
                + sqlConst + Environment.NewLine

          
                                                                  + Environment.NewLine
                                                                    + Environment.NewLine                 
                //Status
                + " ,Status=CASE WHEN LogInformation.Status ='0'" + Environment.NewLine
                + "  THEN '开始'"                                                              + Environment.NewLine
                + "  WHEN LogInformation.Status ='3'"                                          + Environment.NewLine
                + "  THEN '取消'"                                                              + Environment.NewLine
                + "  WHEN LogInformation.Status ='4'"                                          + Environment.NewLine
                + "  THEN '挂起'"                                                              + Environment.NewLine
                + "  WHEN LogInformation.Status ='5'"                                          + Environment.NewLine
                + "  THEN '完成'"                                                              + Environment.NewLine
                + "  ELSE '错误'"                                                              + Environment.NewLine
                + "  END"                                                                      + Environment.NewLine

                //CopyCheck
                + " ,CheckCopy=CASE WHEN LogInformation.FileName is null" + Environment.NewLine
                + "  THEN ''" + Environment.NewLine
                + "  ELSE '查看'" + Environment.NewLine
                + "  END"   


                + " FROM [LogInformation]"                                                     + Environment.NewLine;
              

        return sql;

    }
    #endregion
    #region "Function:Is more than Count  Limit"
 
    private void IsMoreLimit(int MaxCount, String SearchSql)
    {
        string strSql = " SELECT Count(1) AS PageCount FROM ";
        strSql += "({0})MoreLimit";
        
        int dateCount = 0;
        using (SqlDataReader reader = ExecuteReader(string.Format(strSql,SearchSql)))
        {
            while (reader.Read())
            {
                dateCount = (int)reader["PageCount"];
            }
        }
        // While Page Count is over MaxCount , show alert Message in Page
        // ( UnDisplyed while Page Count over MaxCount )
        if (dateCount > MaxCount)
        {
            // Each PageID's PageCount over MaxCount , show alert Message in Page.
            WarninngMessage(string.Format(UtilConst.MSG_LIMIT_LOG, MaxCount));           
        }
     
    }
    #endregion    

    #region"Function:SQL to Run "
    public void RunSQL()
    {
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        string sql = SearchSql();

        if (row.Dis_Log_MaxCount != 0)
        {
            //Max display record count  setted
            IsMoreLimit(row.Dis_Log_MaxCount, sql);
            // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ST
           //sql = "SELECT TOP " + row.Dis_Log_MaxCount + "* FROM (" + sql + ")LIM ORDER BY Id DESC";
            sql = "SELECT TOP " + row.Dis_Log_MaxCount + "* FROM (" + sql + ")LIM";
            // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ED

        }      
        ViewState["SearchKeyWord"] = sql;
        SqlDataListSource.SelectCommand = sql;
        CustomersGridView.DataBind();

        this.ddlNumPerPage.SelectedIndex = 0;
        // Set the PageIndex property to display that page selected by the user.
        this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());

        SetListMainPgae(this.CustomersGridView,
             this.ddlNumPerPage,
             this.ddlIndexOfPage,
             this.lblTotalPage,
             this.btnSelectAll, "ProcessTime,UserName,MFPModelName,MFPSerialNumber,JobType,ColorMode,PageCount,Status,Id,MFPPrintTaskID,Duplex,PriceDetailID,Cost,SheetCount,PapeCount,CopyCount,PageID,CheckCopy,FileName");
             this.CustomersGridView.Sort("ProcessTime", SortDirection.Descending);
    }
    #endregion

    #region"Function: Sql is Judged "
    /// <summary>
    /// Function: Sql is Judged 
    /// </summary>
    /// <returns>Sql</returns>
    /// <Date>2010.12.15</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private string SearchSql()
    {
        // Get Now Period
        // Start Period.
        DateTime StartPeriod;
        // End Period.
        DateTime EndPeriod;
        // Get Period Time By Now.
        //UtilCommon.GetPeriodBy(DateTime.Now, out StartPeriod, out EndPeriod);
        UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);
        String StartDate = ConvertDateToSQL(StartPeriod);
        String EndDate = ConvertDateToSQL(EndPeriod);
        String sql = "";
        //The Search Condition for the search button.
        if (!(ViewState["StartPeriod"] == null || string.IsNullOrEmpty(ViewState["StartPeriod"].ToString())))
        {
            StartDate = ViewState["StartPeriod"].ToString();
        }

        if (!(ViewState["EndPeriod"] == null || string.IsNullOrEmpty(ViewState["EndPeriod"].ToString())))
        {
            EndDate = ViewState["EndPeriod"].ToString();
        }

        sql = LogViewSql();
        sql += " WHERE  SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {0}" + Environment.NewLine
                   + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {1}";
        //The Search Condition for the search button. The Search Rule is Partial match: Like '% Search Words %'
        if (!(ViewState["UserLogin"] == null || string.IsNullOrEmpty(ViewState["UserLogin"].ToString())))
        {           
            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ST
            //sql += " AND UserID IN (SELECT ID FROM UserInfo WHERE  " + Environment.NewLine
            //    + "UserName LIKE " + ConvertStringToSQL("%" + ViewState["UserLogin"].ToString() + "%");
            //sql += "OR LoginName LIKE " + ConvertStringToSQL("%" + ViewState["UserLogin"].ToString() + "%") + ")";
            sql += "AND (UserName LIKE " + ConvertStringToSQL("%" + ViewState["UserLogin"].ToString() + "%");
            sql += "OR LoginName LIKE " + ConvertStringToSQL("%" + ViewState["UserLogin"].ToString() + "%") + ")";
            // 2011.1.10 Update By SES Zhou Miao Ver.1.1 Update ED
        }
        //The Search Condition for the search button. Select the MFP.
        if (!(ViewState["MFPNumber"] == null || string.IsNullOrEmpty(ViewState["MFPNumber"].ToString())))
        {
            sql += " AND SerialNumber = " + ConvertStringToSQL(ViewState["MFPNumber"].ToString());

        }
        //The Search Condition for the search button. Select the Status.
        if (!(ViewState["StatusTarget"] == null || string.IsNullOrEmpty(ViewState["StatusTarget"].ToString())))
        {
            sql += " AND Status IN (" + ViewState["StatusTarget"].ToString() + ")";

        }
        //The Search Condition for the search button. Select the Job Type.
        if (!(ViewState["JobtypeTarget"] == null || string.IsNullOrEmpty(ViewState["JobtypeTarget"].ToString())))
        {
            sql += " AND JobID = " + ConvertStringToSQL(ViewState["JobtypeTarget"].ToString());
        }        

        sql = string.Format(sql, StartDate, EndDate);        

        return sql;
    }
    #endregion    

    //对当前GridView进行排序（未使用）
    #region "Function:Raises the GridView.Sorted event."
    /// <summary>
    /// Function:Raises the GridView.Sorted event.
    /// It'll be overrided In each page.
    /// </summary>
    /// <param name="cView"></param>
    /// <Date>2010.12.14</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version> 
    public override void SortGridView(GridView cView)
    {
        if (this.CustomersGridView.Rows.Count == 0)
        {
            return;
        }
        string sc = SortDirection.Ascending.Equals(cView.SortDirection) ? UtilConst.CON_ITEM_SORT_ASC : UtilConst.CON_ITEM_SORT_DESC;

        int idx = 0;
       // 2014-5-3 pupeng
        if (cView.HeaderRow != null)
        {
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
        }
        //20140428 MJ tan ADD END
    }
    #endregion

    //在两个按钮事件里使用
    #region "Function:Select Id for GridView"
    /// <summary>
    /// Function:Select Id for GridView
    /// </summary>
    /// <returns></returns>
    /// <Date>2010.12.16</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private String SelectstrIdList()
    {
        //Get select ID List.
        string strId = "";
        string strIdList = "";
        foreach (GridViewRow gRow in CustomersGridView.Rows)
        {
            CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
            if (ch.Checked)
            {
                strId = gRow.Cells[gRow.Cells.Count-1].Text;
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

        return strIdList;
    }
    #endregion
    //CSV文件导出函数里面使用
    #region "Get CSV OutPut Date"
    /// <summary>
    /// Get CSV OutPut Date
    /// </summary>
    /// <param name="detail"></param>
    /// <returns></returns>
    /// <Date>2010.12.16</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected List<List<string>> GetCsvDate(DataTable detail)
    {
        List<List<string>> CsvList = new List<List<string>>();
   
        string data = "";
        //Add titles and Target period on the csv files
        List<string> strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_PAGE_LOG);
        CsvList.Add(strHeadList);
        // 1 Get Header Date
        // 1.1 Get Big Header Date
        List<string> strList = new List<string>(); 
        for (int j = 1; j < detail.Columns.Count; j++)
        {
            // DataTable Date 
            data = detail.Columns[j].ColumnName.ToString();
            strList.Add(data);
        }
        CsvList.Add(strList);
       // Get Date
        for (int i = 0; i < detail.Rows.Count; i++)
        {
             strList = new List<string>();           
            // 1 Get  Date
            // 1.1 Get Big  Date.          
                for (int j = 1; j < detail.Columns.Count; j++)
                {
                    // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ST
                    // DataTable Date 
                    //data = detail.Rows[i][j].ToString();
                    if ("操作时间".Equals(detail.Columns[j].ColumnName))
                    {
                        data = Convert.ToDateTime(detail.Rows[i][j]).ToString("yyyy/MM/dd HH:mm:ss");                      
                    }
                    else
                    {
                        data = detail.Rows[i][j].ToString();
                    } 
                    // 2010.12.21 Update By SES Zhou Miao Ver.1.1 Update ED
                    strList.Add(data.Replace(",", "，"));
                }               
            
            CsvList.Add(strList);
        }

        return CsvList;
    }
    #endregion

    

    #region "ShowPDF"
    protected void CustomersGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ErrorAlert("金额不能为0，并且余额必须大于彩色余额");
    }

    private void dataGridView1_CellClick(object sender, GridViewUpdateEventArgs e)
    {
        ErrorAlert("1");
       
    }

#endregion
    protected void CustomersGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
