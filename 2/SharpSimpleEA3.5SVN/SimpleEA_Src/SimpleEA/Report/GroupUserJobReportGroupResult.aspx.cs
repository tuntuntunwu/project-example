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
/// Group Job Report Result.
/// </summary>
/// <Date>2010.07.06</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Report_GroupUserJobReportGroupResult : MainPage
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

    #region "Get Result and displayed in Page."
    /// <summary>
    /// DisplayDetailResult
    /// </summary>
    /// <Date>2010.06.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void DisplayDetailResult()
    {
        int Dsp_Count_mode = 0;
        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;


        string strSql = "";
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

        // 2.For each Result Column's(Job and Function type) Name , Get DateInformation.
        // 2.1 New DateTable and Sql.
        string strColumnName = "";
        strSql = "";
        // Foot
        Hashtable foothashList = new Hashtable();

        // [All Count] Column
        strColumnName = "Count";
        decimal intAllCount = 0;
        foothashList.Add(strColumnName, (decimal)0);

        // [B/W Count] Column
        strColumnName = "BWCount";
        decimal intBWCount = 0;
        foothashList.Add(strColumnName, (decimal)0);

        // [FullColor Count] Column
        strColumnName = "FullColorCount";
        decimal intFCCount = 0;
        foothashList.Add(strColumnName, (decimal)0);

        //chen update 20140624 start
        string selSql = "";
        if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {
            selSql = " ,SUM(SpendMoney) AS PageCount ";
        }
        else
        {
            if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
            {
                selSql = " ,SUM(DspNumber) AS PageCount ";
            }
            else
            {
                selSql = " ,SUM(Number) AS PageCount ";
            }
        }
        //chen update 20140624 end


        // Get PageCount Group By JobId and GroupId
        //strSql = " SELECT JobID , FunctionID , GroupID, UserID , SUM(SpendMoney) AS PageCount " + Environment.NewLine;
        strSql = " SELECT JobID , FunctionID , GroupID, UserID " + Environment.NewLine;
        strSql += selSql + Environment.NewLine;

        strSql += "  FROM [JobInformation] " + Environment.NewLine;
        strSql += " WHERE GroupID in ({0}) " + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= " +
            ConvertDateToSQL(this.Master.START_TIME) + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= " +
            ConvertDateToSQL(this.Master.END_TIME) + Environment.NewLine;
        // 2010.12.14 Add By SES Zhou Miao Ver.1.1 Update ST
        if (!("0".Equals(this.Master.MFP_SERIAL_NUMBER)))
        {
            strSql += " AND [JobInformation].SerialNumber=" + ConvertStringToSQL(this.Master.MFP_SERIAL_NUMBER.ToString());

        }
        // 2010.12.14 Add By SES Zhou Miao Ver.1.1 Update ED
        strSql += "GROUP BY JobID , FunctionID , GroupID, UserID " + Environment.NewLine;


        // 3.2 Get Date
        // Detail Date
        DataTable table = ExecuteDataTable(string.Format(strSql, sqlId));

        // 3.3 Regulation for Display
        // 2010.11.24 Add By SES zhoumiao Ver.1.1 Update ST
        //The job item display when the item be checked.
        Checkbox_Delete_Set();
        // 2010.11.24 Add By SES zhoumiao Ver.1.1 Update ST

        #region 3.4 Title Of Head
        TableHeaderCell hCell;
        foreach (JobTypeList item in JobList)
        {
            // PageType
            hCell = new TableHeaderCell();
            if (item.FCFunctionId != 99)
            {
                hCell.ColumnSpan = 3;
                // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ST
                hCell.Width = WIDTH_BIGTITLE;
                // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ED
            }
            else
            {
                // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ST
                hCell.Width = WIDTH_SECONDTITLE;
                // 2010.12.17 Add By SES zhoumiao Ver.1.1 Update ED
                hCell.ColumnSpan = 2;
            }
            hCell.Text = item.JobName;
            // 2010.12.17 Delete By SES zhoumiao Ver.1.1 Update ST
            //hCell.Width = WIDTH_BIGTITLE;
            // 2010.12.17 Delete By SES zhoumiao Ver.1.1 Update ED

            // 2010.09.16 Update By SES Ji.JianXiong ST
            // hCell.CssClass = UtilConst.CSS_ITEM_TDWithTop;
            hCell.CssClass = UtilConst.CSS_SMALL_ROW;
            hCell.Height = new Unit(23);
            // 2010.09.16 Update By SES Ji.JianXiong ED
            tblHRBigTitle.Cells.Add(hCell);
            foothashList.Add(item.JobName, (decimal)0);
            // ColorType
            hCell = new TableHeaderCell();
            hCell.Text = UtilConst.COLOR_BW;
            hCell.Width = WIDTH_SMALLTITLE;
            hCell.Wrap = false;
            // 2010.09.16 Add By SES Ji.JianXiong ST
            hCell.Height = new Unit(23);
            // 2010.09.16 Add By SES Ji.JianXiong ED
            tblHRSmallTitle.Cells.Add(hCell);
            foothashList.Add(item.JobName + "BW", (decimal)0);

            if (item.FCFunctionId != 99)
            {
                hCell = new TableHeaderCell();
                hCell.Text = UtilConst.COLOR_FULLCOLOR;
                hCell.Width = WIDTH_SMALLTITLE;
                hCell.Wrap = false;
                tblHRSmallTitle.Cells.Add(hCell);
                foothashList.Add(item.JobName + "FC", (decimal)0);
            }

            hCell = new TableHeaderCell();
            hCell.Text = "合计";
            hCell.Width = WIDTH_SMALLTITLE;
            hCell.Wrap = false;
            // 2010.09.16 Add By SES Ji.JianXiong ST
            hCell.Height = new Unit(23);
            // 2010.09.16 Add By SES Ji.JianXiong ED
            tblHRSmallTitle.Cells.Add(hCell);
            foothashList.Add(item.JobName + "ALL", (decimal)0);
        }
        #endregion

        // 3.5 Detail
        foreach (string item in this.Master.ID_LIST)
        {
            #region 添加组的统计
            TableRow row = new TableRow();
            // Style
            if (!Convert.ToBoolean((tblDetail.Rows.Count - 2) % 2))
            {
                row.CssClass = UtilConst.CSS_ITEM_EVEN;
            }
            else
            {
                // 2010.09.16 Add By SES Ji.JianXiong ST
                row.CssClass = UtilConst.CSS_ITEM_ODD;
                // 2010.09.16 Add By SES Ji.JianXiong ED
            }

            // Group
            TableCell tCell = new TableCell();
            GroupInfoTableAdapter groupAdapter = new GroupInfoTableAdapter();
            tCell.Text = (string)groupAdapter.GetGroupNameById(int.Parse(item));
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);

            // 用户组时用户名为空
            tCell = new TableCell();
            row.Cells.Add(tCell);

            // All Count By Group
            TableCell tAllCountCell = new TableCell();
            row.Cells.Add(tAllCountCell);

            // B/W Count By Group
            TableCell tBWCountCell = new TableCell();
            row.Cells.Add(tBWCountCell);

            // FullCount Count By Group
            TableCell tFCCountCell = new TableCell();
            row.Cells.Add(tFCCountCell);

            intAllCount = 0;
            intBWCount = 0;
            intFCCount = 0;

            int intGroupID = int.Parse(item);

            foreach (JobTypeList jobItme in JobList)
            {
                decimal BWCount = 0;
                decimal FCCount = 0;
                // B/W
                strColumnName = jobItme.JobName + "BW";
                tCell = new TableCell();
                BWCount = GetDetailValueFromTable(table, jobItme.JobId, jobItme.BWFunctionId, intGroupID, "PageCount");
                //tCell.Text = UtilCommon.IntToMoney(BWCount);
                tCell.Text = UtilCommon.decimalToMoney(BWCount, Dsp_Count_mode);
                foothashList[strColumnName] = (decimal)foothashList[strColumnName] + BWCount;
                row.Cells.Add(tCell);

                // FullColor
                if (jobItme.FCFunctionId != 99)
                {
                    tCell = new TableCell();
                    FCCount = GetDetailValueFromTable(table, jobItme.JobId, jobItme.FCFunctionId, intGroupID, "PageCount");
                    //tCell.Text = UtilCommon.IntToMoney(FCCount);
                    tCell.Text = UtilCommon.decimalToMoney(FCCount, Dsp_Count_mode);
                    strColumnName = jobItme.JobName + "FC";
                    foothashList[strColumnName] = (decimal)foothashList[strColumnName] + FCCount;
                    row.Cells.Add(tCell);
                }
                // Count
                tCell = new TableCell();
                //tCell.Text = UtilCommon.IntToMoney(BWCount + FCCount);
                tCell.Text = UtilCommon.decimalToMoney(BWCount + FCCount, Dsp_Count_mode);
                strColumnName = jobItme.JobName + "ALL";
                foothashList[strColumnName] = (decimal)foothashList[strColumnName] + BWCount + FCCount;
                row.Cells.Add(tCell);

                intBWCount = intBWCount + BWCount;
                intFCCount = intFCCount + FCCount;

            }

            intAllCount = intBWCount + intFCCount;
            //tAllCountCell.Text = UtilCommon.IntToMoney(intAllCount);
            tAllCountCell.Text = UtilCommon.decimalToMoney(intAllCount, Dsp_Count_mode);
            strColumnName = "Count";
            foothashList[strColumnName] = (decimal)foothashList[strColumnName] + intAllCount;
            //tBWCountCell.Text = UtilCommon.IntToMoney(intBWCount);
            tBWCountCell.Text = UtilCommon.decimalToMoney(intBWCount, Dsp_Count_mode);
            strColumnName = "BWCount";
            foothashList[strColumnName] = (decimal)foothashList[strColumnName] + intBWCount;
            //tFCCountCell.Text = UtilCommon.IntToMoney(intFCCount);
            tFCCountCell.Text = UtilCommon.decimalToMoney(intFCCount, Dsp_Count_mode);
            strColumnName = "FullColorCount";
            foothashList[strColumnName] = (decimal)foothashList[strColumnName] + intFCCount;

            tblDetail.Rows.Add(row);
            #endregion

            //20140504 tan ADD START
            #region 添加组内用户的统计
            UserInfoTableAdapter userAdapter = new UserInfoTableAdapter();
            dtUserInfo.UserInfoDataTable UserTable = userAdapter.GetDataByGroupID(intGroupID);
            foreach (dtUserInfo.UserInfoRow urow in UserTable.Rows)
            {
                row = new TableRow();
                // Style
                if (!Convert.ToBoolean((tblDetail.Rows.Count - 2) % 2))
                {
                    row.CssClass = UtilConst.CSS_ITEM_EVEN;
                }
                else
                {
                    // 2010.09.16 Add By SES Ji.JianXiong ST
                    row.CssClass = UtilConst.CSS_ITEM_ODD;
                    // 2010.09.16 Add By SES Ji.JianXiong ED
                }

                // 用户时用户组为空
                tCell = new TableCell();
                row.Cells.Add(tCell);

                tCell = new TableCell();
                tCell.Text = (string)userAdapter.GetDataByUserId(urow.ID)[0].UserName;
                tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
                tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
                row.Cells.Add(tCell);

                // All Count By Group
                tAllCountCell = new TableCell();
                row.Cells.Add(tAllCountCell);

                // B/W Count By Group
                tBWCountCell = new TableCell();
                row.Cells.Add(tBWCountCell);

                // FullCount Count By Group
                tFCCountCell = new TableCell();
                row.Cells.Add(tFCCountCell);

                intAllCount = 0;
                intBWCount = 0;
                intFCCount = 0;

                intGroupID = int.Parse(item);

                foreach (JobTypeList jobItme in JobList)
                {
                    decimal BWCount = 0;
                    decimal FCCount = 0;
                    // B/W
                    strColumnName = jobItme.JobName + "BW";
                    tCell = new TableCell();
                    BWCount = GetUserDetailValueFromTable(table, jobItme.JobId, jobItme.BWFunctionId, intGroupID, urow.ID, "PageCount");
                    //tCell.Text = UtilCommon.IntToMoney(BWCount);
                    tCell.Text = UtilCommon.decimalToMoney(BWCount, Dsp_Count_mode);
                    //foothashList[strColumnName] = (decimal)foothashList[strColumnName] + BWCount;
                    row.Cells.Add(tCell);

                    // FullColor
                    if (jobItme.FCFunctionId != 99)
                    {
                        tCell = new TableCell();
                        FCCount = GetUserDetailValueFromTable(table, jobItme.JobId, jobItme.FCFunctionId, intGroupID, urow.ID, "PageCount");
                        //tCell.Text = UtilCommon.IntToMoney(FCCount);
                        tCell.Text = UtilCommon.decimalToMoney(FCCount, Dsp_Count_mode);
                        strColumnName = jobItme.JobName + "FC";
                        //foothashList[strColumnName] = (decimal)foothashList[strColumnName] + FCCount;
                        row.Cells.Add(tCell);
                    }
                    // Count
                    tCell = new TableCell();
                    //tCell.Text = UtilCommon.IntToMoney(BWCount + FCCount);
                    tCell.Text = UtilCommon.decimalToMoney(BWCount + FCCount, Dsp_Count_mode);
                    strColumnName = jobItme.JobName + "ALL";
                    //foothashList[strColumnName] = (decimal)foothashList[strColumnName] + BWCount + FCCount;
                    row.Cells.Add(tCell);

                    intBWCount = intBWCount + BWCount;
                    intFCCount = intFCCount + FCCount;

                }

                intAllCount = intBWCount + intFCCount;
                //tAllCountCell.Text = UtilCommon.IntToMoney(intAllCount);
                tAllCountCell.Text = UtilCommon.decimalToMoney(intAllCount, Dsp_Count_mode);
                strColumnName = "Count";
                //foothashList[strColumnName] = (decimal)foothashList[strColumnName] + intAllCount;
                //tBWCountCell.Text = UtilCommon.IntToMoney(intBWCount);
                tBWCountCell.Text = UtilCommon.decimalToMoney(intBWCount, Dsp_Count_mode);
                strColumnName = "BWCount";
                //foothashList[strColumnName] = (decimal)foothashList[strColumnName] + intBWCount;
                //tFCCountCell.Text = UtilCommon.IntToMoney(intFCCount);
                tFCCountCell.Text = UtilCommon.decimalToMoney(intFCCount, Dsp_Count_mode);
                strColumnName = "FullColorCount";
                //foothashList[strColumnName] = (decimal)foothashList[strColumnName] + intFCCount;

                tblDetail.Rows.Add(row);
            }
            #endregion
            //20140504 tan ADD END


        }
        // Foot
        // Foot Count
        TableFooterRow tblFootRow = new TableFooterRow();
        hCell = new TableHeaderCell();
        hCell.Text = "合计";
        hCell.ColumnSpan = 1;
        hCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        tblFootRow.Cells.Add(hCell);

        hCell = new TableHeaderCell();
        tblFootRow.Cells.Add(hCell);
        // Count
        hCell = new TableHeaderCell();
        strColumnName = "Count";
        //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
        hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName], Dsp_Count_mode);
        hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
        tblFootRow.Cells.Add(hCell);
        // B/W Count
        hCell = new TableHeaderCell();
        strColumnName = "BWCount";
        //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
        hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName], Dsp_Count_mode);
        hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
        tblFootRow.Cells.Add(hCell);
        // Full Color Count
        hCell = new TableHeaderCell();
        strColumnName = "FullColorCount";
        //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
        hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName], Dsp_Count_mode);
        hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
        tblFootRow.Cells.Add(hCell);

        // Foot Detail 
        foreach (JobTypeList jobItme in JobList)
        {
            // B/W
            strColumnName = jobItme.JobName + "BW";
            hCell = new TableHeaderCell();
            //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
            hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName], Dsp_Count_mode);
            hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            tblFootRow.Cells.Add(hCell);

            // FullColor
            if (jobItme.FCFunctionId != 99)
            {
                strColumnName = jobItme.JobName + "FC";
                hCell = new TableHeaderCell();
                //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
                hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName], Dsp_Count_mode);
                hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                tblFootRow.Cells.Add(hCell);
            }
            // Count
            strColumnName = jobItme.JobName + "ALL";
            hCell = new TableHeaderCell();
            //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
            hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName], Dsp_Count_mode);
            hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            tblFootRow.Cells.Add(hCell);



        }
        // 2010.09.16 Add By SES JiJianXiong ST
        tblFootRow.CssClass = UtilConst.CSS_FOOT_ROW;
        // 2010.09.16 Add By SES JiJianXiong ED
        tblDetail.Rows.Add(tblFootRow);

        // Display Set
        // Display Set
        int intWidth = (int)(300 + JobList.Count * WIDTH_BIGTITLE.Value);
        // 2011.1.12 Update By SES zhoumiao Ver.1.1 Update ST
        //if (intWidth > 1024)
        //{
        //    this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, intWidth.ToString() + "px");
        //}
        switch (JobList.Count)
        {
            case 1:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, "1000px");
                break;
            case 2:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, "1100px");
                break;
            case 3:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, "1200px");
                break;
            case 4:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, "1300px");
                break;
            default:
                this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, intWidth.ToString() + "px");
                break;
        }

        // 2011.1.12 Update By SES zhoumiao Ver.1.1 Update ED
    }

    #endregion

    #region "GetDetailValueFromTable"
    /// <summary>
    /// GetDetailValueFromTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    /// <Date>2010.06.29</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private decimal GetDetailValueFromTable(DataTable dt, int JobId, int FunId, int GroupID, string strColumnName)
    {
        string strSql = "JobId = {0} AND FunctionID = {1} AND GroupID = {2}";
        DataRow[] row = dt.Select(string.Format(strSql, JobId, FunId, GroupID));
        if (row == null || row.Length == 0)
        {
            return (0);
        }
        else
        {
            decimal sumdata = 0;
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i][strColumnName] == null || row[i][strColumnName].ToString().Trim().Equals(""))
                {
                    ;
                }
                else
                {
                    sumdata += decimal.Parse(row[i][strColumnName].ToString());
                }
            }
            //return decimal.Parse(row[0][strColumnName].ToString());
            return sumdata;
        }

    }

    private decimal GetUserDetailValueFromTable(DataTable dt, int JobId, int FunId, int GroupID, int UserID, string strColumnName)
    {
        string strSql = "JobId = {0} AND FunctionID = {1} AND GroupID = {2} AND UserID = {3}";
        DataRow[] row = dt.Select(string.Format(strSql, JobId, FunId, GroupID, UserID));
        if (row == null || row.Length == 0)
        {
            return (0);
        }
        else
        {
            return decimal.Parse(row[0][strColumnName].ToString());
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
        OutPutCsvFile("GroupJobReport", tblDetail);

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
        strHeadList.Add("统计 - 用户、组使用报表");
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
                    //2010.12.14 Update By SES zhoumiao Ver.1.1 Update ST
                    // Tbale Cell 
                    //if (j > 4)
                    if (j > 3)
                    //2010.12.14 Update By SES zhoumiao Ver.1.1 Update ED                    
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
                        //2010.12.14 Delete By SES zhoumiao Ver.1.1 Update ST
                        //strList.Add("");
                        //2010.12.14 Delete By SES zhoumiao Ver.1.1 Update ST
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
                foreach (TableCell detailcell in row.Cells)
                {
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
