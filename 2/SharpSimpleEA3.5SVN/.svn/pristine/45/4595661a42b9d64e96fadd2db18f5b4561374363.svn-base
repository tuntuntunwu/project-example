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


/// <summary>
/// Total Job Report Result.
/// </summary>
/// <Date>2010.06.24</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Report_TotalJobReportResult : MainPage
{
    #region "private const"
    // Cell Width for BigTitle.
    private Unit WIDTH_BIGTITLE = new Unit(150);
    // Cell Width for SmallTitle.
    private Unit WIDTH_SMALLTITLE = new Unit(75);

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

        this.Master.TargetDisplay = true;

        // Get the Target Name
        this.Master.TargetType = GetTagetName();

        dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        Dsp_Count_mode = settingrow.Dis_Count_mode;
        Dsp_A3_A4 = settingrow.Dis_A3_A4;

        // Get Result and displayed in Page.
        DisplayDetailResult();
    }
    #endregion

    #region "GetTagetName"
    /// <summary>
    /// GetTagetName
    /// </summary>
    /// <Date>2010.06.24</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected string GetTagetName()
    {
        string strName = "";
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

        string strSql = "";
        if (this.Master.REPORT_TYPE.Equals(UtilConst.REPORT_TYPE_TOTAL_USER))
        {
            // Get UserName By ID.
            strSql = "   SELECT UserName AS Name " +
                            "     FROM [UserInfo]       " +
                            "    WHERE (ID IN ({0}))    " +
                            " ORDER BY ID   ";
        }
        else
        {
            // Get GroupName By ID.
            strSql = "   SELECT GroupName AS Name " +
                            "     FROM [GroupInfo]       " +
                            "    WHERE (ID IN ({0}))     " +
                            " ORDER BY ID   ";
        }

        strSql = string.Format(strSql, sqlId);

        using (SqlDataReader reader = ExecuteReader(strSql))
        {
            while (reader.Read())
            {
                if (strName == "")
                {
                    strName = (string)reader["Name"];
                }
                else
                {
                    strName = strName + "£¬" + (string)reader["Name"];
                }
            }
        }

        return strName;

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

        ArrayList pageList = new ArrayList();
        // 2.Get Page Count By PageID.
        // 2011.3.22 Update By SES Zhou Miao Ver.1.1 Update ST
        //strSql = " SELECT PageID , PaperSize , SUM(Number) PageCount FROM [JobInformation] ";
        // 2014.4.29 Update By SES chen youguang  ST
        //strSql = " SELECT PageID , PaperSize ,PaperName ,SUM(Number) PageCount FROM [JobInformation] ";
        //strSql = " SELECT PageID , PaperSize ,PaperName ,SUM(SpendMoney) PageCount FROM [JobInformation] ";
        strSql = " SELECT PageID , PaperSize ,PaperName  ";
        strSql += selSql;
        strSql += " FROM [JobInformation] ";
        // 2014.4.29 Update By SES chen youguang  end
        // 2011.3.22 Update By SES Zhou Miao Ver.1.1 Update ED
        strSql += " LEFT OUTER JOIN [PaperSizeInformation] ";
        strSql += " ON PaperSizeInformation.ID = JobInformation.PageID ";
        strSql += " WHERE SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= {0} ";
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {1} ";
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ST
        if (!("0".Equals(this.Master.MFP_SERIAL_NUMBER)))
        {
            strSql += " AND [JobInformation].SerialNumber=" + ConvertStringToSQL(this.Master.MFP_SERIAL_NUMBER.ToString());

        }
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ED
        if (this.Master.REPORT_TYPE.Equals(UtilConst.REPORT_TYPE_TOTAL_USER))
        {
            strSql += "   AND UserID IN ({2}) ";
        }
        else
        {
            strSql += "   AND GroupID IN ({2}) ";
        }
        strSql += " GROUP BY PageID , PaperSize, PaperName";
        strSql += " ORDER BY PageCount DESC";

        DataTable tableCount = ExecuteDataTable(string.Format(strSql, 
            ConvertDateToSQL(this.Master.START_TIME), ConvertDateToSQL(this.Master.END_TIME), sqlId));

        foreach (DataRow dtCountRow in tableCount.Rows)
        {
            // While Page Count is over 0 , Add to PageList.
            // ( UnDisplyed while Page Count is 0 )

            // 2014.4.29 Update By SES chen youguang  ST
            //if ((int)dtCountRow["PageCount"] > 0)
            if (decimal.Parse(dtCountRow["PageCount"].ToString()) > 0)
            // 2014.4.29 Update By SES chen youguang  end
            {
                PageCountList pList = new PageCountList(dtCountRow);
                pageList.Add(pList);
            }
        }

        // 2010.08.31 Delete By SES.JiJianXiong ST
        // Delete for this date check move to the TotalJobReport page.
        //if (pageList.Count == 0)
        //{
        //    // Each PageID's PageCount is 0 , show alert Message in Page.
        //    if ( this.Master.REPORT_TYPE.Equals(UtilConst.REPORT_TYPE_TOTAL_USER)) {
        //        this.Master.Alert(UtilConst.MSG_NODATE_USER);
        //    } else {
        //        this.Master.Alert(UtilConst.MSG_NODATE_GROUP);
        //    }

        //    tblDetail.Visible = false;
        //    btnCSVOutPut.Visible = false;

        //    return;
        //}
        // 2010.08.31 Delete By SES.JiJianXiong ED

        // 3.For each Result Column's(page type) Name , Get DateInformation.
        // 3.1 New DateTable and Sql.
        string strColumnName = "";
        strSql = "";
        // Foot
        Hashtable foothashList = new Hashtable();




        // [Title] Column
        // [All Count] Column
        strColumnName = "Count";


        //chen update 20140624 start
        selSql = "";
        if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
        {
            selSql = " ,ISNULL((SELECT SUM(SpendMoney) ";
        }
        else
        {
            if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
            {
                selSql = " ,ISNULL((SELECT SUM(DspNumber) ";
            }
            else
            {
                selSql = " ,ISNULL((SELECT SUM(Number) ";
            }
        }
        //chen update 20140624 end

        // 2014.4.29 Update By SES chen youguang  ST
        //strSql = " ,ISNULL((SELECT SUM(Number) " + strColumnName + Environment.NewLine;
        //strSql = " ,ISNULL((SELECT SUM(SpendMoney) " + strColumnName + Environment.NewLine;
        strSql = selSql  + strColumnName + Environment.NewLine;

        // 2014.4.29 Update By SES chen youguang  ST
        strSql += "        FROM [JobInformation]  " + Environment.NewLine;
        strSql += "       WHERE [JobInformation].FunctionID = [FunctionTypeInformation].FunctionID  " + Environment.NewLine;
        strSql += "         AND [JobInformation].JobID = [JobTypeInformation].ID  " + Environment.NewLine;
        strSql += "         AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= " + 
            ConvertDateToSQL(this.Master.START_TIME) + Environment.NewLine;
        strSql += "         AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= " + 
            ConvertDateToSQL(this.Master.END_TIME) + Environment.NewLine;
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ST
        if (!("0".Equals(this.Master.MFP_SERIAL_NUMBER)))
        {
            strSql += " AND [JobInformation].SerialNumber=" + ConvertStringToSQL(this.Master.MFP_SERIAL_NUMBER.ToString());

        }
        // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ED
        if (this.Master.REPORT_TYPE.Equals(UtilConst.REPORT_TYPE_TOTAL_USER))
        {
            strSql += "   AND UserID IN (" + sqlId + ") ";
        }
        else
        {
            strSql += "   AND GroupID IN (" + sqlId + ") ";
        }
        strSql += "         ), 0)" + Environment.NewLine;
        strSql += "      AS " + strColumnName + Environment.NewLine;
        foothashList.Add(strColumnName, (decimal)0);

        // Add Page Type's Columns.
        int i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();

            // 2014.4.29 Update By SES chen youguang  ST
            //strSql += " ,ISNULL((SELECT SUM(Number) " + strColumnName + Environment.NewLine;
            //strSql += " ,ISNULL((SELECT SUM(SpendMoney) " + strColumnName + Environment.NewLine;
            strSql +=selSql + strColumnName + Environment.NewLine;

            // 2014.4.29 Update By SES chen youguang  end
            strSql += "        FROM [JobInformation]  " + Environment.NewLine;
            strSql += "       WHERE PageID = " + item.PageID.ToString() + Environment.NewLine;
            strSql += "         AND [JobInformation].FunctionID = [FunctionTypeInformation].FunctionID " + Environment.NewLine;
            strSql += "         AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= " + 
                ConvertDateToSQL(this.Master.START_TIME) + Environment.NewLine;
            strSql += "         AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= " + 
                ConvertDateToSQL(this.Master.END_TIME) + Environment.NewLine;
            // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ST
            if (!("0".Equals(this.Master.MFP_SERIAL_NUMBER)))
            {
                strSql += " AND [JobInformation].SerialNumber=" + ConvertStringToSQL(this.Master.MFP_SERIAL_NUMBER.ToString());

            }
            // 2010.12.13 Add By SES Zhou Miao Ver.1.1 Update ED
            if (this.Master.REPORT_TYPE.Equals(UtilConst.REPORT_TYPE_TOTAL_USER))
            {
                strSql += "   AND UserID IN (" + sqlId + ") ";
            }
            else
            {
                strSql += "   AND GroupID IN (" + sqlId + ") ";
            }
            strSql += "         AND [JobInformation].JobID = [JobTypeInformation].ID ), 0)" + Environment.NewLine;
            strSql += "      AS " + strColumnName + Environment.NewLine;

            // Foot
            foothashList.Add(strColumnName + "A", (decimal)0);
            foothashList.Add(strColumnName + "B", (decimal)0);

        }

        // Detail SQL
        strSql = " SELECT [JobTypeInformation].ID JOBID, [FunctionTypeInformation].FunctionID FUNCTIONID" +Environment.NewLine + strSql +Environment.NewLine;
        strSql += "  FROM [FunctionTypeInformation] INNER JOIN " + Environment.NewLine;
        strSql += "       [JobTypeInformation] ON " + Environment.NewLine;
        strSql += "       [FunctionTypeInformation].JobID = [JobTypeInformation].ID ";
        strSql += " ORDER BY [JobTypeInformation].ID,[FunctionTypeInformation].FunctionID ";

        // 3.2 Get Date
        // Detail Date
        DataTable table = ExecuteDataTable(strSql);
        // 3.3 Regulation for Display  
        
        //int Dsp_Count_mode = 0;
        //dtSettingDisp.SettingDispRow settingrow = UtilCommon.GetDispSetting();
        //Dsp_Count_mode = settingrow.Dis_Count_mode;

        // 3.4 Title Of Head
        TableHeaderCell hCell;
        foreach (PageCountList item in pageList)
        {
            // PageType
            hCell = new TableHeaderCell();
            hCell.ColumnSpan = 2;
            hCell.Text = item.PageName;
            hCell.Width = WIDTH_BIGTITLE;
            // 2010.09.16 Update By SES Ji.JianXiong ST
            // hCell.CssClass = UtilConst.CSS_ITEM_TDWithTop;
            hCell.CssClass = UtilConst.CSS_SMALL_ROW;
            hCell.Height = new Unit(23);
            // 2010.09.16 Update By SES Ji.JianXiong ED

            tblHRBigTitle.Cells.Add(hCell);
            // ColorType
            hCell = new TableHeaderCell();
            hCell.Text = UtilConst.COLOR_BW;
            hCell.Width = WIDTH_SMALLTITLE;
            hCell.Wrap = false;
            // 2010.09.16 Add By SES Ji.JianXiong ST
            hCell.Height = new Unit(23);
            // 2010.09.16 Add By SES Ji.JianXiong ED
            tblHRSmallTitle.Cells.Add(hCell);
            hCell = new TableHeaderCell();
            hCell.Text = UtilConst.COLOR_FULLCOLOR;
            hCell.Width = WIDTH_SMALLTITLE;
            hCell.Wrap = false;
            // 2010.09.16 Add By SES Ji.JianXiong ST
            hCell.Height = new Unit(23);
            // 2010.09.16 Add By SES Ji.JianXiong ED
            tblHRSmallTitle.Cells.Add(hCell);
        }
        

        // [Copy] Row
        TableRow row = new TableRow();
        row.CssClass = UtilConst.CSS_ITEM_EVEN;

        // Title
        TableCell tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_Copy;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        decimal Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1, strColumnName);
        //Get CountPage Form datetable By FucntionId and JobId(FullColor).
        Count += GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2, strColumnName);
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);

        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;

        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);

            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //Get CountPage Form datetable By FucntionId and JobId(FullColor).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId2, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);

            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // [Print] Row
        row = new TableRow();
        // 2010.09.16 Add By SES Ji.JianXiong ST
        row.CssClass = UtilConst.CSS_ITEM_ODD;
        // 2010.09.16 Add By SES Ji.JianXiong ED

        // Title
        tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_Print;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1, strColumnName);
        //Get CountPage Form datetable By FucntionId and JobId(FullColor).
        Count += GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2, strColumnName);
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);

        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;


        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //Get CountPage Form datetable By FucntionId and JobId(FullColor).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId2, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // [Document Filing Print] Row
        row = new TableRow();
        // Title
        tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_DFPrint;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.CssClass = UtilConst.CSS_ITEM_EVEN;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1, strColumnName);
        //Get CountPage Form datetable By FucntionId and JobId(FullColor).
        Count += GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2, strColumnName);
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);
        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;


        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //Get CountPage Form datetable By FucntionId and JobId(FullColor).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId2, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // [ Scan Save] Row
        row = new TableRow();
        // 2010.09.16 Add By SES Ji.JianXiong ST
        row.CssClass = UtilConst.CSS_ITEM_ODD;
        // 2010.09.16 Add By SES Ji.JianXiong ED

        // Title
        tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_ScanSave;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1, strColumnName);
        //Get CountPage Form datetable By FucntionId and JobId(FullColor).
        Count += GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2, strColumnName);
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);
        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;


        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //Get CountPage Form datetable By FucntionId and JobId(FullColor).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId2, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // [List Print] Row
        row = new TableRow();
        // Title
        tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_ListPrint;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.CssClass = UtilConst.CSS_ITEM_EVEN;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1, strColumnName);
        //Get CountPage Form datetable By FucntionId and JobId(FullColor).
        Count += GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2, strColumnName);
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);
        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;


        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //Get CountPage Form datetable By FucntionId and JobId(FullColor).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId2, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // [Scan] Row
        row = new TableRow();
        // 2010.09.16 Add By SES Ji.JianXiong ST
        row.CssClass = UtilConst.CSS_ITEM_ODD;
        // 2010.09.16 Add By SES Ji.JianXiong ED

        // Title
        tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_Scan;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1, strColumnName);
        //Get CountPage Form datetable By FucntionId and JobId(FullColor).
        Count += GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2, strColumnName);
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);
        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;


        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //Get CountPage Form datetable By FucntionId and JobId(FullColor).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId2, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // [Fax] Row
        row = new TableRow();
        // Title
        tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_Fax;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.CssClass = UtilConst.CSS_ITEM_EVEN;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1, strColumnName);
        //[Fax] Row's FullColor Page is 0.
        Count += 0;
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);
        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;


        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //[Fax] Row's FullColor Page is 0.
            Count = 0;
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // [Fax (Channel2)] Row
        row = new TableRow();
        // 2010.09.16 Add By SES Ji.JianXiong ST
        row.CssClass = UtilConst.CSS_ITEM_ODD;
        // 2010.09.16 Add By SES Ji.JianXiong ED

        // Title
        tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_FaxC2;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1, strColumnName);
        //[Fax (Channel2)] Row's FullColor Page is 0.
        Count +=0;
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);
        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;


        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //[Fax (Channel2)] Row's FullColor Page is 0.
            Count = 0;
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // [Internet Fax] Row
        row = new TableRow();
        // Title
        tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_IntFax;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.CssClass = UtilConst.CSS_ITEM_EVEN;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1, strColumnName);
        //[Internet Fax] Row's FullColor Page is 0.
        Count += 0;
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);
        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;


        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //[Internet Fax] Row's FullColor Page is 0.
            Count = 0;
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // [Other] Row
        row = new TableRow();
        // 2010.09.16 Add By SES Ji.JianXiong ST
        row.CssClass = UtilConst.CSS_ITEM_ODD;
        // 2010.09.16 Add By SES Ji.JianXiong ED

        // Title
        tCell = new TableCell();
        tCell.Text = UtilConst.ITEM_TITLE_Other;
        tCell.Wrap = false;
        tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
        tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        row.Cells.Add(tCell);

        // Count
        strColumnName = "Count";
        //Get CountPage Form datetable By FucntionId and JobId(B/W).
        Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Other_JobId, UtilConst.ITEM_TITLE_Other_FunctionId1, strColumnName);
        //Get CountPage Form datetable By FucntionId and JobId(FullColor).
        Count += GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Other_JobId, UtilConst.ITEM_TITLE_Other_FunctionId2, strColumnName);
        tCell = new TableCell();
        tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
        row.Cells.Add(tCell);
        // Foot
        foothashList[strColumnName] = (decimal)foothashList[strColumnName] + Count;


        // Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            //Get CountPage Form datetable By FucntionId and JobId(B/W).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Other_JobId, UtilConst.ITEM_TITLE_Other_FunctionId1, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "A"] = (decimal)foothashList[strColumnName + "A"] + Count;

            //Get CountPage Form datetable By FucntionId and JobId(FullColor).
            Count = GetDetailValueFromTable(table, UtilConst.ITEM_TITLE_Other_JobId, UtilConst.ITEM_TITLE_Other_FunctionId2, strColumnName);
            tCell = new TableCell();
            tCell.Text = UtilCommon.decimalToMoney(Count, Dsp_Count_mode);
            row.Cells.Add(tCell);
            // Foot
            foothashList[strColumnName + "B"] = (decimal)foothashList[strColumnName + "B"] + Count;

        }

        tblDetail.Rows.Add(row);

        // Foot
        // Foot Count
        TableFooterRow tblFootRow = new TableFooterRow();
        hCell = new TableHeaderCell();
        hCell.Text = "ºÏ¼Æ";
        hCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
        tblFootRow.Cells.Add(hCell);
        // Count
        hCell = new TableHeaderCell();
        strColumnName = "Count";
        //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName]);
        hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName], Dsp_Count_mode);
        hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
        // 2010.09.16 Add By SES JiJianXiong ST
        tblFootRow.CssClass = UtilConst.CSS_FOOT_ROW;
        // 2010.09.16 Add By SES JiJianXiong ED
        tblFootRow.Cells.Add(hCell);

        // Foot Detail 
        i = 0;
        foreach (PageCountList item in pageList)
        {
            i++;
            strColumnName = "PageCol" + i.ToString();
            hCell = new TableHeaderCell();
            //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName + "A"]);
            hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName + "A"], Dsp_Count_mode);
            hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            tblFootRow.Cells.Add(hCell);

            hCell = new TableHeaderCell();
            //hCell.Text = UtilCommon.IntToMoney((int)foothashList[strColumnName + "B"]);
            hCell.Text = UtilCommon.decimalToMoney((decimal)foothashList[strColumnName + "B"], Dsp_Count_mode);
            hCell.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            tblFootRow.Cells.Add(hCell);

        }
        tblDetail.Rows.Add(tblFootRow);

        // Display Set
        int intWidth = (int)(300 + pageList.Count * WIDTH_BIGTITLE.Value);
        if (intWidth > 1024)
        {
            this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, intWidth.ToString() + "px");
        }
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
    private decimal GetDetailValueFromTable(DataTable dt, int JobId, int FunId, string strColumnName)
    {
        string strSql = "JOBID = {0} AND FUNCTIONID = {1}";
        DataRow[] row = dt.Select(string.Format(strSql, JobId, FunId));
        if (row == null || row.Length == 0)
        {
            return (0);
        } else {
            return decimal.Parse(row[0][strColumnName].ToString() );
        }
 
    }
    #endregion

    #region "Page Count Class"
    /// <summary>
    /// Page Count Class
    /// </summary>
    /// <Date>2010.06.26</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public class PageCountList
    {
        int intPageId;
        
        //chen update 20140429 start
        //int intCountById;
        decimal intCountById;
        //chen update 20140429 end

        string strPageNm;
        string strPageSize;
        /// <summary>
        /// PageCountList
        /// </summary>
        /// <param name="dtRow"></param>
        /// <Date>2010.06.26</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public PageCountList(DataRow dtRow)
        {
            // Page Type's  Id.
            intPageId = (int)dtRow["PageID"];
            // PageCount
            //chen 20140429 update st
            //intCountById = (int)dtRow["PageCount"];
            intCountById = decimal.Parse(dtRow["PageCount"].ToString());
            //chen 20140429 update ed

            // 2011.03.22 Update By SES zhoumiao ST
            //// Page Type's Name.
            //strPageNm = dtRow["PaperSize"].ToString();
            // Page Type's Size.
            strPageSize = dtRow["PaperSize"].ToString();
            //  Page Type's Name.
            strPageNm = dtRow["PaperName"].ToString();

            // 2011.03.22 Update By SES zhoumiao ED
           
        }

        /// <summary>
        /// PageCountList
        /// </summary>
        /// <param name="intId"></param>
        /// <param name="intCount"></param>
        /// <param name="strPageNum"></param>
        /// <Date>2010.06.26</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public PageCountList(int intId, decimal intCount, string strPageNum)
        {
            // Page Type's  Id.
            intPageId = intId;
            // PageCount
            intCountById = intCount;
            // Page Type's Name.
            strPageNm = strPageNum;
        }

        /// <summary>
        /// Page Type's Id.
        /// </summary>
        /// <Date>2010.06.26</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public String PageName
        {
            get {
                return strPageNm;
            }
            set
            {
                strPageNm = value;
            }
        }

        /// <summary>
        /// Page Type's Id.
        /// </summary>
        /// <Date>2010.06.26</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public int PageID
        {
            get
            {
                return intPageId;
            }
            set
            {
                intPageId = value;
            }
        }

        /// <summary>
        /// Count Of PageNumber By PageID.
        /// </summary>
        /// <Date>2010.06.26</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public decimal Count
        {
            get
            {
                return intCountById;
            }
            set
            {
                intCountById = value;
            }
        }
    }
    #endregion

    #region "ReSet PageList Order By Count."
    /// <summary>
    /// ReSetPageListByCount
    /// </summary>
    /// <param name="oldList"></param>
    /// <returns></returns>
    /// <Date>2010.06.29</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private ArrayList ReSetPageListByCount(ArrayList oldList)
    {
        ArrayList NewList = new ArrayList();

        Boolean isMax = false;
        int i = 0;
        do
        {
            PageCountList item = (PageCountList)oldList[i];
            isMax = true;
            foreach (PageCountList item2 in oldList)
            {
                if (item.Count < item2.Count)
                {
                    isMax = false;
                    break;
                }
            }
            if (isMax)
            {
                NewList.Add(item);
                oldList.Remove(item);
                i = 0;
            }
            else
            {
                i = i + 1;
            }
        } while (oldList.Count > 0);

        return NewList;
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
        OutPutCsvFile("TotalJobReport",tblDetail);
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
        //2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
        //Add titles and Target period on the csv files
        List<string> strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_PAGE_TOTALREPORT);
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        if (UtilConst.REPORT_TYPE_TOTAL_GROUP.Equals(this.Master.REPORT_TYPE))
        {
            strHeadList.Add(UtilConst.CON_TARGET_GROUP);
        }
        else
        {
            strHeadList.Add(UtilConst.CON_TARGET_USER);
        }
        
        strHeadList.Add(this.GetTagetName().Replace(",", "£¬"));
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_TARGET_MFP);
        strHeadList.Add(this.Master.GetMFPTargetName());
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        strHeadList.Add(UtilConst.CON_TIME_PERIOD);
        strHeadList.Add(this.Master.tbcTargetPeriod_text().Text);
        CsvList.Add(strHeadList);
        //2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
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
                for (int j = 1; j < row.Cells.Count; j++)
                {
                    // Tbale Cell 
                    cell = row.Cells[j];
                    if ( j == 1)
                    {
                        // Header's Blank and Count.
                        strList.Add("");
                        strList.Add(cell.Text);
                    }
                    else
                    {
                        strList.Add(cell.Text);
                        strList.Add("");
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
                    
                    // 2010.12.13 Update By SES zhoumiao Ver.1.1 Update ST
                    //strList.Add(detailcell.Text.Replace(",",""));
                    strList.Add(detailcell.Text.Replace(",", "£¬"));
                    // 2010.12.13 Update By SES zhoumiao Ver.1.1 Update ED
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
}
