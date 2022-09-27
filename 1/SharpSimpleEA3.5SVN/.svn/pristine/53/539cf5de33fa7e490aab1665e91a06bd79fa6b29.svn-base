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
using dtRestrictionInformationTableAdapters;


/// <summary>
/// Available Report Result.
/// </summary>
/// <Date>2010.07.13</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Report_AvailableReportResult : MainPage
{


    #region "private const"
    // Cell Width for BigTitle.
    private Unit WIDTH_BIGTITLE = new Unit(150);
    // Cell Width for SmallTitle.
    private Unit WIDTH_SMALLTITLE = new Unit(75);
    //2011.01.07 Add By SES zhoumiao Ver.1.1 Update ST 
    // Cell Width for Fax2Title.
    private Unit WIDTH_FAX2TITLE = new Unit(100);
    //2011.01.07 Add By SES zhoumiao Ver.1.1 Update ED 

    #endregion

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        // SimpleDetailPage.PageLoad
        this.Master.PageLoad_Available();
        if (!IsPostBack)
        {
            // Get Result and displayed in Page.
           
        }
        DisplayDetailResult();
    }
    #endregion


    #region "Get Result and displayed in Page."
    /// <summary>
    /// DisplayDetailResult
    /// </summary>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void DisplayDetailResult()
    {
        
    dtJobInformation.JobInformationDataTable jobdatatable = null;
    
    
       dtRestrictionInfo.RestrictionInfoDataTable resmoneydatatable = null;
        //private dtUserInfo.UserInfoDataTable userdatatable = null;
       dtUserPayDetail.UserPayDetailDataTable userPaydatatable = null;
        string strSql = "";
        // 1.Get PageID List.
        // 1.1 ID
        string sqlId = "";
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ST
        dtSettingDisp.SettingDispRow Avai_Borrowrow = UtilCommon.GetDispSetting();
        //2010.12.10 Add By SES zhoumiao Ver.1.1 Update ED
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
        strSql = "";

        // Get PageCount Group By JobId and GroupId
        strSql = " SELECT JobID , FunctionID , UserID , SUM(Number) AS PageCount " + Environment.NewLine;
        strSql += "  FROM [JobInformation] " + Environment.NewLine;
        strSql += " WHERE UserID in ({0}) " + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= " +
            ConvertDateToSQL(this.Master.START_TIME) + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= " +
            ConvertDateToSQL(this.Master.END_TIME) + Environment.NewLine;
        strSql += "GROUP BY JobID , FunctionID , UserID " + Environment.NewLine;


        // 3.2 Get Date
        // Detail Date( Used Date ).
        DataTable table = ExecuteDataTable(string.Format(strSql, sqlId));

       
        //pupeng 2014 04 26 check if current user is admin
        bool isAdmin = HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME);
        // 3.5 Detail
        foreach (string item in this.Master.ID_LIST)
        {

           // string val = "<input type='text' class='Inputtextbox' style='width:80px' disabled='disabled' value='{0}'/>&nbsp;&nbsp;&nbsp;";
            string btn = "<input type='Button' value='追加'  OnClick=\"btnAddValue_click(this, '{0}');\"  class='Button_JobReport' style='height:28px !important;'/>&nbsp;";
            string txt = "<input type='text' value='0' onkeyup=\"onAddValueKeyUp(this)\" onbeforepaste=\"onAddValueKeyUp(this)\" name='{0}' class='Inputtextbox' style='width:80px' />";
          //  string txt = "<input type='text' onkeyup=\"value=value.replace(/^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$,'')\" onbeforepaste=\"clipboardData.setData('text',clipboardData.getData('text').replace(/^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$,''))\" name='{0}' class='Inputtextbox' style='width:80px' />";
        
            //pupeng 2014 04 26
            decimal UsedMoney=0, UsedColorMoney=0,PayMoney=0, PayColorMoney=0,RemainMoney=0,RemainColorMoney = 0.0M;
            int ridID = 0;
            // UserID 
            int intUserId = int.Parse(item);

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

            // User
            TableCell tCell = new TableCell();
            UserInfoTableAdapter userAdapter = new UserInfoTableAdapter();
            dtUserInfo.UserInfoRow userRow = userAdapter.GetDataByUserId(int.Parse(item))[0];
            //int restrictid = int.Parse(userRow.RestrictionID);
            int restrictid = userRow.RestrictionID;
            ////chen add st
            //int restrictid = int.Parse(userRow.RestrictionID);
            //dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter resAdapter = new dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter();
            //dtRestrictionInfo.RestrictionInfoDataTable resinfotable = resAdapter.GetRestrictionInfoDataByID(restrictid);
            //dtRestrictionInfo.RestrictionInfoRow resRow = resinfotable[0];
            //decimal allowMoney = resRow.AllQuota;
            //decimal allowColorMoney = resRow.ColorQuota;
            //// chen add ed
            //获得用户的限额table，根据配额ID
            resmoneydatatable = UtilCommon.GetUserResmoneyLimitsFromDB(restrictid);
            jobdatatable = UtilCommon.GetUserJobInfoFromDB(intUserId);
            userPaydatatable = UtilCommon.GetUserPayDataFromDB(intUserId);
            UsedColorMoney = UtilCommon.GetJobSpendMoneyFun(jobdatatable, 2);
            UsedMoney = UsedColorMoney + UtilCommon.GetJobSpendMoneyFun(jobdatatable, 1);
            //获得用户配额
            if (resmoneydatatable.Count > 0)
            {
                dtRestrictionInfo.RestrictionInfoRow resRow = resmoneydatatable[0];
                //RemainMoney =resRow.IsAllQuotaNull()?0: resRow.AllQuota;
                //RemainColorMoney =resRow.IsColorQuotaNull()?0:resRow.ColorQuota;
                RemainMoney = resRow.AllQuota;
                RemainColorMoney = resRow.ColorQuota;
            }
            //用户追加额度
            if (userPaydatatable.Count > 0)
            {
                dtUserPayDetail.UserPayDetailRow payRow = userPaydatatable[0];
                PayMoney = payRow.IsSumMoneyNull() ? 0 : payRow.SumMoney;
                PayColorMoney = payRow.IsSumColorMoneyNull() ? 0:payRow.SumColorMoney;
            }
            //用户配额 + 透支额度 -  已用额度  作为用户可用额度
            RemainMoney = RemainMoney + PayMoney - UsedMoney;
            RemainColorMoney = RemainColorMoney + PayColorMoney - UsedColorMoney;

            //RemainMoney = userRow.IsRemainMoneyNull() ? 0 : (allowMoney - userRow.RemainMoney);
            //RemainColorMoney = userRow.IsRemainColorMoneyNull() ? 0 : (allowColorMoney - userRow.RemainColorMoney);
            tCell.Text = userRow.UserName;
            //ridID = int.Parse(userRow.RestrictionID);
            ridID = userRow.RestrictionID;
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tCell.CssClass = UtilConst.CSS_ITEM_TDWithLeft;
            row.Cells.Add(tCell);

            // Group
            tCell = new TableCell();
            GroupInfoTableAdapter groupAdapter = new GroupInfoTableAdapter();
            tCell.Text = (string)groupAdapter.GetGroupNameById(userRow.GroupID);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            row.Cells.Add(tCell);


            // 余额
            tCell = new TableCell();
            //dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter risAdapter = new dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter();
            //dtRestrictionInfo.RestrictionInfoRow risRow = risAdapter.GetRestrictionInfoDataByID(ridID)[0];
            //取出配额

            //RisMoney = risRow.AllQuota;
            //RisColorMoney = risRow.ColorQuota;
            //UserPayDetailTableAdapters.UserPayDetailTableAdapter payAdapter = new UserPayDetailTableAdapters.UserPayDetailTableAdapter();
         
            //UserPayDetail.UserPayDetailRow payRow = payAdapter.GetMoneyByUserID(userRow.ID)[0];
            //     //取出汇总明细
            //PayMoney = payRow.IsMoneyNull() ? 0 : payRow.Money;
            //PayColorMoney = payRow.IsColorMoneyNull()?0: payRow.ColorMoney;


            ////取出使用的明细
            //dtJobInformationTableAdapters.JobInformationTableAdapter jobAdapter = new JobInformationTableAdapter();
            //dtJobInformation.JobInformationRow jobRow = jobAdapter.sumMoneyByUserID(userRow.ID, 1)[0];//黑白
            //UsedMoney = jobRow.IsSpendMoneyNull() ? 0 : jobRow.SpendMoney;
            //jobRow = jobAdapter.sumMoneyByUserID(userRow.ID, 2)[0];//彩色
            //UsedColorMoney = jobRow.IsSpendMoneyNull() ? 0 : jobRow.SpendMoney;
            //UsedMoney += UsedColorMoney;
            //RemainMoney = RisMoney + PayMoney - UsedMoney;
            //RemainColorMoney = RisColorMoney + PayColorMoney - UsedColorMoney;
            //余额
            tCell.Text = RemainMoney.ToString();
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            row.Cells.Add(tCell);
            //追加框
            tCell = new TableCell();
            tCell.Text = !isAdmin ? "" : String.Format(txt, "txtAddMoney_" + item);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            row.Cells.Add(tCell);

            //彩色余额
            tCell = new TableCell();
            tCell.Text = RemainColorMoney.ToString();
           // tCell.Text = String.Format(val, RemainColorMoney.ToString());
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            row.Cells.Add(tCell);
            //追加框
            tCell = new TableCell();
            tCell.Text = !isAdmin ? "" : String.Format(txt, "txtAddColorMoney_" + item);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            row.Cells.Add(tCell);

            //追加按钮
            tCell = new TableCell();
            tCell.Text = !isAdmin ? "" : String.Format(btn, userRow.ID);
            tCell.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
            row.Cells.Add(tCell);

            tblDetail.Rows.Add(row);

        }

        // Display Set
        // Display Set
        int intWidth = (int)(100 + JobListWithOutOther.Count * WIDTH_BIGTITLE.Value);
        if (intWidth > 1024)
        {
            this.Page.Form.Style.Add(HtmlTextWriterStyle.Width, intWidth.ToString() + "px");
        }
    }

    #endregion
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("MFPRestrictionList.aspx", false);
    }
    #region "GetDetailValueFromTable"
    /// <summary>
    /// GetDetailValueFromTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private int GetDetailValueFromTable(DataTable dt, int JobId, int FunId, int UserID, string strColumnName)
    {
        string strSql = "JobID = {0} AND FunctionID = {1} AND UserID = {2}";
        DataRow[] row = dt.Select(string.Format(strSql, JobId, FunId, UserID));
        if (row == null || row.Length == 0)
        {
            return (0);
        }
        else
        {
            return (int)row[0][strColumnName];
        }

    }
    #endregion

    #region "Get Detail RestrictionInformationDataRow"

    /// <summary>
    /// Get Detail RestrictionInformationDataRow
    /// </summary>
    /// <param name="table"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private dtRestrictionInformation.RestrictionInformationRow GetDetailResRow(dtRestrictionInformation.RestrictionInformationDataTable table, int JobId, int FunId)
    {
        string strSql = "JobID = {0} AND FunctionID = {1}";
        DataRow[] row = table.Select(string.Format(strSql, JobId, FunId));
        if (row.Length > 0)
        {
            return (dtRestrictionInformation.RestrictionInformationRow)row[0];
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region "btnCSV_Click"
    /// <summary>
    /// btnCSV_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnCSVOutPut_Click(object sender, EventArgs e)
    {
        OutPutCsvFile("AvailableReport", tblDetail);
    }
    #endregion

    #region "Get CSV OutPut Date"
    /// <summary>
    /// GetCsvDate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.13</Date>
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
        strHeadList.Add(UtilConst.CON_PAGE_AVAILREPORT);
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        //strHeadList.Add("");
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
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    // Tbale Cell 
                    if (j > 1)
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
                    // 2010.12.13 Update By SES zhoumiao Ver.1.1 Update ST
                    //strList.Add(detailcell.Text.Replace(",",""));
                    strList.Add(detailcell.Text.Replace(",", "，"));
                    // 2010.12.13 Update By SES zhoumiao Ver.1.1 Update ED
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
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void OutPutCsvFile(string filename, Table detail)
    {
        // 1.Get Date
        List<List<string>> CsvList = GetCsvDate(detail);

        // 2.To Csv Date.
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        int linenum = -1; //行数，因为希望第4行不要导出
        foreach (List<string> item in CsvList)
        {
            linenum++;
            if (linenum == 3)//第3行删除
                continue;
            string strOutPut = null;
            foreach (string strItem in item)
            {
                if (strItem.IndexOf("<input") == -1)
                {
                    if (strOutPut == null)
                    {
                        strOutPut = strItem;
                    }
                    else
                    {
                        if(strItem!="追加"&&strItem!="")
                        strOutPut = strOutPut + "," + strItem;
                    }
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

    //2010.12.13 Delete By SES zhoumiao Ver.1.1 Update ST
    //#region"function:Get unUsed Page Number for B/W mode and the Full color mode"
    ///// <summary>
    ///// function:Get unUsed Page Number for B/W mode and the Full color mode
    ///// </summary>
    ///// <param name="LimBWCount"></param>
    ///// <param name="LimFCCount"></param>
    ///// <param name="LimBWC"></param>
    ///// <param name="LimFCC"></param>
    ///// <Date>2010.12.10</Date>
    ///// <Author>SES Zhou Miao</Author>
    ///// <Version>1.10</Version>
    //public static void SetlimCountBy(String LimBWCount, String LimFCCount, out String LimBWC, out String LimFCC)
    //{
    //    int LimBW = 0;
    //    int LimFC = 0;
    //    LimBWC = LimBWCount;
    //    LimFCC = LimFCCount;
    //    if (!(LimBWCount.Equals(UtilConst.STATUS_UNLIMITED_NAME) || LimFCCount.Equals(UtilConst.STATUS_UNLIMITED_NAME)))
    //    {
    //        LimBW = int.Parse(LimBWCount);
    //        LimFC = int.Parse(LimFCCount);
    //        int total = LimBW + LimFC;
    //        if (total <= 0)
    //        {
    //            LimBWC = "0";
    //            LimFCC = "0";

    //        }
    //        else
    //        {
    //            if (LimBW < 0)
    //            {
    //                LimBWC = "0";
    //                LimFC += LimBW;
    //                LimFCC = UtilCommon.IntToMoney(LimFC);

    //            }
    //            if (LimFC < 0)
    //            {
    //                LimFCC = "0";
    //                LimBW += LimFC;
    //                LimBWC = UtilCommon.IntToMoney(LimBW);
    //            }
    //        }
    //    }



    //}
    //#endregion
    //2010.12.13 Delete By SES zhoumiao Ver.1.1 Update ED

    #region"function:Get unUsed Page Number for B/W mode and the Full color mode"
    /// <summary>
    /// function:Get unUsed Page Number for B/W mode and the Full color mode
    /// </summary>
    /// <param name="LimBWCount"></param>
    /// <param name="LimFCCount"></param>
    /// <Date>2010.12.13</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.10</Version>
    public static void SetlimCountBy(ref String LimBWCount, ref String LimFCCount)
    {
        int LimBW = 0;
        int LimFC = 0;
        // 2011.01.10 Add By SES zhoumiao Ver.1.1 Update ST
        if (UtilConst.STATUS_PROHIBITION_NAME.Equals(LimFCCount))
        {
            LimFCCount = "0";
        }
        // 2011.01.10 Add By SES zhoumiao Ver.1.1 Update ED       

        if (!(LimBWCount.Equals(UtilConst.STATUS_UNLIMITED_NAME) || LimFCCount.Equals(UtilConst.STATUS_UNLIMITED_NAME)))
        {

            LimBW = int.Parse(LimBWCount);
            LimFC = int.Parse(LimFCCount);
            LimBWCount = UtilCommon.IntToMoney(LimBW);
            LimFCCount = UtilCommon.IntToMoney(LimFC);
            int total = LimBW + LimFC;
            if (total > 0)
            {
                if (LimBW < 0)
                {
                    LimBWCount = "0";
                    LimFC += LimBW;
                    LimFCCount = UtilCommon.IntToMoney(LimFC);

                }
                if (LimFC < 0)
                {
                    LimFCCount = "0";
                    LimBW += LimFC;
                    LimBWCount = UtilCommon.IntToMoney(LimBW);
                }

            }
            else
            {
                LimBWCount = "0";
                LimFCCount = "0";

            }
        }
        else
        {
            if (!(LimBWCount.Equals(UtilConst.STATUS_UNLIMITED_NAME)))
            {
                LimBWCount = UtilCommon.IntToMoney(LimBWCount);
            }
            if (!(LimFCCount.Equals(UtilConst.STATUS_UNLIMITED_NAME)))
            {
                LimFCCount = UtilCommon.IntToMoney(LimFCCount);
            }


        }

    }
    #endregion

    protected void hidBtnAddMoney_Click(object sender, EventArgs e)
    {
        //decimal AddMoney, AddColorMoney;
        //int userid;
        //AddMoney = decimal.Parse(this.hidTxtBxGrayValue.Text);
        //AddColorMoney = decimal.Parse(this.hidTxtBxColorValue.Text);
        //userid = int.Parse(this.hidTxtBxUserId.Text);
       using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
       {
           con.Open();
           SqlTransaction tran = con.BeginTransaction();
           try
           {
               string strSql;
               string[] paramslist = new string[3];
               paramslist[0] = ConvertIntToSQL(this.hidTxtBxUserId.Text);
               paramslist[1] = ConvertMoneyToSQL(this.hidTxtBxGrayValue.Text);
               paramslist[2] = ConvertMoneyToSQL(this.hidTxtBxColorValue.Text);
               strSql = "   INSERT INTO [UserPayDetail]          " + Environment.NewLine;
               strSql += "             ([UserID]                " + Environment.NewLine;
               strSql += "             ,[Money]         " + Environment.NewLine;
               strSql += "             ,[ColorMoney])                " + Environment.NewLine;
               strSql += "       VALUES                     " + Environment.NewLine;
               strSql += "             ({0}                 " + Environment.NewLine;
               strSql += "             ,{1}                " + Environment.NewLine;
               strSql += "             ,{2})  " + Environment.NewLine;

               strSql = string.Format(strSql, paramslist);
               using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
               {
                   cmd.ExecuteNonQuery();
               }

               //strSql = "   update [UserInfo]  set        " + Environment.NewLine;
               //strSql += "   [RemainMoney]=[RemainMoney]-{1}                " + Environment.NewLine;
               //strSql += "  ,[RemainColorMoney]=[RemainColorMoney]-{2}         " + Environment.NewLine;
               //strSql += "  ,[UpdateTime]=getdate()                " + Environment.NewLine;
               //strSql += "       where ID={0}                    " + Environment.NewLine;
          
               //strSql = string.Format(strSql, paramslist);
               //using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
               //{
               //    cmd.ExecuteNonQuery();
               //}
               tran.Commit();
               DisplayDetailResult();
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
}
