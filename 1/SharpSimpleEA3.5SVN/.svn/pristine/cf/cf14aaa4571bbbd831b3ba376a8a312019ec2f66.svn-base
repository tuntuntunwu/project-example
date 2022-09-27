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
using dtGroupInfoTableAdapters;
using System.Data.SqlClient;
using dtPaperSizeInformationTableAdapters;
using dtJobInformationTableAdapters;
using dtUserInfoTableAdapters;


/// <summary>
/// User Job Report Result.
/// </summary>
/// <Date>2010.07.02</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Report_GroupUserJobReportUserResult : MainPage
{

    #region "private const"
    // Cell Width for BigTitle.
    private Unit WIDTH_BIGTITLE = new Unit(180);
    // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ST
    // Cell Width for Second Title.
    private Unit WIDTH_SECONDTITLE = new Unit(120);
    // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ED
    // Cell Width for SmallTitle.
    private Unit WIDTH_SMALLTITLE = new Unit(60);


    private int Dsp_Count_mode = 0;
    private int Dsp_A3_A4 = 0;

    #endregion

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.24</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        // SimpleDetailPage.PageLoad
        this.Master.PageLoad();

        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;
        Dsp_A3_A4 = settingrow.Dis_A3_A4;

        // 2010.11.24 Update By SES zhoumiao Ver.1.1 Update ST
        // Get Result and displayed in Page.
        //DisplayDetailResult();
        if (!IsPostBack)
        {
            //CheckBox Icons are Display
            this.Master.cheakCellItem = true;
            //The default display item settings for User Job Report Result screen to Checked
            this.Master.CheckBox_item_Set();
            // Get Result and displayed in Page.
            DisplayDetailResult();
        }
        // CheckBox in this page
        this.Master.CheckBox_chkCopy().CheckedChanged += new EventHandler(chkCopy_OnCheckedChanged);
        this.Master.CheckBox_chkPrint().CheckedChanged += new EventHandler(chkPrint_OnCheckedChanged);
        this.Master.CheckBox_chkScan().CheckedChanged += new EventHandler(chkScan_OnCheckedChanged);
        this.Master.CheckBox_chkFax().CheckedChanged += new EventHandler(chkFax_OnCheckedChanged);
        this.Master.CheckBox_chkOther().CheckedChanged += new EventHandler(chkOther_OnCheckedChanged);
        // 2010.11.24 Update By SES zhoumiao Ver.1.1 Update ED
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
        int Dsp_Count_mode = 0;
        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;

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
                + " , ISNULL(("
                //+ "	  SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	  WHERE JobInformation.UserID = UserInfo.ID"
                + "	   AND JobInformation.JobID = 1"        //复印
                + "	   AND JobInformation.FunctionID = 1"   //黑白
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	 ), 0 ) AS BWCopyTotal"
                + " , ISNULL(("
                //+ "	  SELECT SUM(JobInformation.SpendMoney)"
                + selSql
                + "	  FROM JobInformation"
                + "	  WHERE JobInformation.UserID = UserInfo.ID"
                + "	   AND JobInformation.JobID = 1"        //复印
                + "	   AND JobInformation.FunctionID = 2"   //黑白
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	 ), 0 ) AS FULLCopyTotal"
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
                + "	),0) AS BWPrintTotal"
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
                + MFPNumber
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}"
                + "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}"
                + "	),0) AS Total"
                + " FROM [UserInfo] "
                //+ " WHERE UserInfo.GroupID IN ({0})";
                +" WHERE UserInfo.ID IN ({0})";

        DateTime StartPeriod;
        String StartDate;
        DateTime EndPeriod;
        String EndDate;
        UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);
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

        return string.Format(sql, "{0}", StartDate, EndDate);
    }
    #endregion

    #region "Get Result and displayed in Page."
    /// <summary>
    /// DisplayDetailResult
    /// </summary>
    /// <Date>2010.06.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void DisplayDetailResult()
    {
        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        int Dsp_Count_mode = settingrow.Dis_Count_mode;
        // 1.Get PageID List.
        // 1.1 ID
        string sqlId = "";
        foreach (string itme in this.Master.ID_LIST)
        {
            if ("".Equals(sqlId))
            {
                sqlId = itme;
            }
            else
            {
                sqlId += "," + itme;
            }
        }

        DataTable table = ExecuteDataTable(string.Format(UserSql(), sqlId));

        decimal tmpNumber = 0;
        // 3.5 Detail
        foreach (DataRow item in table.Rows)
        {
            TableRow row = new TableRow();
            // Style
            if (!Convert.ToBoolean((tblDetail.Rows.Count - 2) % 2))
            {
                row.CssClass = UtilConst.CSS_ITEM_EVEN;
            }
            else
            {
                row.CssClass = UtilConst.CSS_ITEM_ODD;
            }

            TableCell tCell = new TableCell();
            //用户名
            tCell.Text = item["UserName"].ToString();
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            //彩色复印量合计
            tCell = new TableCell();
            //tCell.Text = UtilCommon.toMoney(item["FULLCopyTotal"].ToString(), Dsp_Count_mode);
            tmpNumber = decimal.Parse(item["FULLCopyTotal"].ToString());
            tCell.Text = UtilCommon.decimalToMoney(tmpNumber, Dsp_Count_mode);
            
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            //黑白复印量合计
            tCell = new TableCell();
            //tCell.Text = UtilCommon.toMoney(item["BWCopyTotal"].ToString(), Dsp_Count_mode);
            tmpNumber = decimal.Parse(item["BWCopyTotal"].ToString());
            tCell.Text = UtilCommon.decimalToMoney(tmpNumber, Dsp_Count_mode);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            //彩色打印量合计
            tCell = new TableCell();
            //tCell.Text = UtilCommon.toMoney(item["FULLPrintTotal"].ToString(), Dsp_Count_mode);
            tmpNumber = decimal.Parse(item["FULLPrintTotal"].ToString());
            tCell.Text = UtilCommon.decimalToMoney(tmpNumber, Dsp_Count_mode);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            //黑白打印量合计
            tCell = new TableCell();
            //tCell.Text = UtilCommon.toMoney(item["BWPrintTotal"].ToString(), Dsp_Count_mode);
            tmpNumber = decimal.Parse(item["BWPrintTotal"].ToString());
            tCell.Text = UtilCommon.decimalToMoney(tmpNumber, Dsp_Count_mode);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            //扫描使用量合计
            tCell = new TableCell();
            //tCell.Text = UtilCommon.toMoney(item["ScanTotal"].ToString(), Dsp_Count_mode);
            tmpNumber = decimal.Parse(item["ScanTotal"].ToString());
            tCell.Text = UtilCommon.decimalToMoney(tmpNumber, Dsp_Count_mode);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            //传真使用量合计
            tCell = new TableCell();
            //tCell.Text = UtilCommon.toMoney(item["FaxTotal"].ToString(), Dsp_Count_mode);
            tmpNumber = decimal.Parse(item["FaxTotal"].ToString());
            tCell.Text = UtilCommon.decimalToMoney(tmpNumber, Dsp_Count_mode);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            //其它使用量合计
            tCell = new TableCell();
            //tCell.Text = UtilCommon.toMoney(item["FaxTotal"].ToString(), Dsp_Count_mode);
            tmpNumber = decimal.Parse(item["OtherTotal"].ToString());
            tCell.Text = UtilCommon.decimalToMoney(tmpNumber, Dsp_Count_mode);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            //总使用量合计
            tCell = new TableCell();
            //tCell.Text = UtilCommon.toMoney(item["Total"].ToString(), Dsp_Count_mode);
            tmpNumber = decimal.Parse(item["Total"].ToString());
            tCell.Text = UtilCommon.decimalToMoney(tmpNumber, Dsp_Count_mode);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);


            tblDetail.Rows.Add(row);
        }

    }

    #endregion

    #region "GetDetailValueFromTable"
    /// <summary>
    /// GetDetailValueFromTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobID"></param>
    /// <param name="FunId"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    /// <Date>2010.06.29</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private decimal GetDetailValueFromTable(DataTable dt, int JobID, int FunId, int UserId, string strColumnName)
    {
        string strSql = "JobID = {0} AND FunctionID = {1} AND UserID = {2}";
        DataRow[] row = dt.Select(string.Format(strSql, JobID, FunId , UserId));
        if (row == null || row.Length == 0)
        {
            return (0);
        } else {
            return (decimal)row[0][strColumnName];
        }
 
    }
    #endregion

    #region "btnCSV_Click"
    /// <summary>
    /// btnCSV_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.30</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnCSVOutPut_Click(object sender, EventArgs e)
    {
        // 2010.11.24 Add By SES zhoumiao Ver.1.1 Update ST
        this.DisplayDetailResult();
        // 2010.11.24 Add By SES zhoumiao Ver.1.1 Update ST
        OutPutCsvFile("UserJobReport",tblDetail);
    }
    #endregion

    #region "Get CSV OutPut Date" 
    /// <summary>
    /// GetCsvDate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.30</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected List<List<string>> GetCsvDate(Table detail)
    {
        List<List<string>> CsvList = new List<List<string>>();
        // Tbale Cell 
        TableCell cell;
        //2010.12.14 Add By SES zhoumiao Ver.1.1 Update ST
        //Add titles and Target period on the csv files
        List<string> strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_PAGE_USERREPORT);
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_TARGET_MFP);
        strHeadList.Add(this.Master.GetMFPTargetName());
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_TIME_PERIOD);
        strHeadList.Add(this.Master.tbcTargetPeriod_text().Text);
        CsvList.Add(strHeadList);
        //2010.12.14 Add By SES zhoumiao Ver.1.1 Update ED
        // Get Date
        for (int i = 0; i < detail.Rows.Count; i++)
        {
            
            List<string> strList = new List<string>();
            // Table Row
            TableRow row = detail.Rows[i];
            // 1 Get Header Date
            // 1.1 Get Big Header Date.
            if (i == 0)
            {
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    // Tbale Cell 
                    if (j > 4)
                    {
                        cell = row.Cells[j];
                        strList.Add(cell.Text);
                        for (int k = 0; k < cell.ColumnSpan - 1; k++)
                        {
                            strList.Add("");
                        }
                    }
                    else
                    {
                        cell = row.Cells[j];
                        strList.Add(cell.Text);
                    }
                }
            }
            else if (i == 1)
            {
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    // Tbale Cell 
                    cell = row.Cells[j];
                    if (j == 0)
                    {
                        // Header's Blank and Count.
                        strList.Add("");
                        strList.Add("");
                        strList.Add("");
                        strList.Add("");
                        strList.Add("");
                        strList.Add(cell.Text);
                    }
                    else
                    {
                        strList.Add(cell.Text);
                    }
                }
            }
            else
            {
                foreach ( TableCell detailcell in row.Cells ) {
                    //2010.12.14 Update By SES zhoumiao Ver.1.1 Update ST
                    //strList.Add(detailcell.Text.Replace(",",""));
                    strList.Add(detailcell.Text.Replace(",", "，"));
                    //2010.12.14 Update By SES zhoumiao Ver.1.1 Update ED
                    for (int k = 0; k < detailcell.ColumnSpan - 1; k++)
                    {
                        strList.Add("");
                    }

                }
            }
            CsvList.Add(strList);
        }

        return CsvList;
    }
    #endregion

    #region "OutPutCsvFile"
    /// <summary>
    /// OutPutCsvFile
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="detail"></param>
    /// <Date>2010.06.30</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void OutPutCsvFile(string filename, Table detail)
    {
        // 1.Get Date
        List<List<string>> CsvList = GetCsvDate(detail);

        // 2.To Csv Date.
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach ( List<string> item in CsvList ) {
            string strOutPut = null;
            foreach ( string strItem in item ) {
                if (strOutPut == null)
                {
                    strOutPut = strItem;
                }
                else
                {
                    strOutPut = strOutPut  + "," + strItem;
                }
            }
            strOutPut = strOutPut + "\r\n";
            sb.Append(strOutPut);
        }

        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename  + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".csv");

        Response.ContentType = "application/text";
        
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        
        Response.Write(sb);
        
        Response.End(); 

    }
    #endregion

    #region"chkCopy_OnCheckedChanged"
    /// <summary>
    /// chkCopy_OnCheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void chkCopy_OnCheckedChanged(object sender, EventArgs e)
    {
        if (this.Master.CheckBox_Selected_Check())
        {
            this.Master.CheckBox_chkCopy().Checked = true;
            //The Display job item control. At least one item must be selected.
            ErrorAlert(UtilConst.MSG_LEAST_ONE_SELECT);
        }

        this.DisplayDetailResult();

    }
    #endregion

    #region"chkPrint_OnCheckedChanged"
    /// <summary>
    /// chkPrint_OnCheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void chkPrint_OnCheckedChanged(object sender, EventArgs e)
    {
        if (this.Master.CheckBox_Selected_Check())
        {
            this.Master.CheckBox_chkPrint().Checked = true;
            //The Display job item control. At least one item must be selected.
            ErrorAlert(UtilConst.MSG_LEAST_ONE_SELECT);
        }

        this.DisplayDetailResult();


    }
    #endregion

    #region"chkScan_OnCheckedChanged"
    /// <summary>
    /// chkScan_OnCheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void chkScan_OnCheckedChanged(object sender, EventArgs e)
    {
        if (this.Master.CheckBox_Selected_Check())
        {
            this.Master.CheckBox_chkScan().Checked = true;
            //The Display job item control. At least one item must be selected.
            ErrorAlert(UtilConst.MSG_LEAST_ONE_SELECT);
        }

        this.DisplayDetailResult();


    }
    #endregion

    #region"chkFax_OnCheckedChanged"
    /// <summary>
    /// chkFax_OnCheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void chkFax_OnCheckedChanged(object sender, EventArgs e)
    {
        if (this.Master.CheckBox_Selected_Check())
        {
            this.Master.CheckBox_chkFax().Checked = true;
            //The Display job item control. At least one item must be selected.
            ErrorAlert(UtilConst.MSG_LEAST_ONE_SELECT);
        }

        this.DisplayDetailResult();


    }
    #endregion

    #region"chkOther_OnCheckedChanged"
    /// <summary>
    /// chkOther_OnCheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void chkOther_OnCheckedChanged(object sender, EventArgs e)
    {
        if (this.Master.CheckBox_Selected_Check())
        {            
            this.Master.CheckBox_chkOther().Checked = true;
            //The Display job item control. At least one item must be selected.
            ErrorAlert(UtilConst.MSG_LEAST_ONE_SELECT);
        }

        this.DisplayDetailResult();


    }
    #endregion     

    #region"Delete JobList then Regulation for Display "
    /// <summary>
    /// Delete JobList then Regulation for Display 
    /// </summary>
    /// <Date>2010.11.24</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.10</Version>
    public void Checkbox_Delete_Set()
    {

        if (!this.Master.CheckBox_chkCopy().Checked)
        {

            base.Delete_JobList(UtilConst.ITEM_TITLE_Copy);

        }
        if (!this.Master.CheckBox_chkPrint().Checked)
        {

            base.Delete_JobList(UtilConst.ITEM_TITLE_Print);

        }
        if (!this.Master.CheckBox_chkScan().Checked)
        {

            base.Delete_JobList(UtilConst.ITEM_TITLE_Scan);

        }
        if (!this.Master.CheckBox_chkFax().Checked)
        {

            base.Delete_JobList(UtilConst.ITEM_TITLE_Fax);

        }
        if (!this.Master.CheckBox_chkOther().Checked)
        {

            base.Delete_JobList(UtilConst.ITEM_TITLE_ScanSave);
            base.Delete_JobList(UtilConst.ITEM_TITLE_DFPrint);
            base.Delete_JobList(UtilConst.ITEM_TITLE_ListPrint);
            base.Delete_JobList(UtilConst.ITEM_TITLE_FaxC2);
            base.Delete_JobList(UtilConst.ITEM_TITLE_IntFax);
            base.Delete_JobList(UtilConst.ITEM_TITLE_Other);
        }

    }

    #endregion

}
