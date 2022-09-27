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
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// Available Report Screen.
/// </summary>
/// <Date>2010.07.01</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Report_AvailableReport : ListMainPage 
{
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
        this.Master.Title = UtilConst.CON_PAGE_AVAILABLE;
        //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ST
        if (HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            this.Master.CheakSearchItem = true;
        }
        DateTime start,end;
        UtilCommon.GetPeriodBy(DateTime.Now,out start,out end);
        if (end.Year > 2400)
        {
            lbl_period.Text = string.Format("无限");
        }
        else
        {
            lbl_period.Text = string.Format("{0}~{1}", start, end);
        }
        //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ED

        SqlDataListSource.ConnectionString = this.DBConnectionStrings;
        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ST 
        //string sql = "";
        //sql = "SELECT                                       " + Environment.NewLine;
        //sql += "  UserInfo.ID               AS Id           " + Environment.NewLine;
        //sql += " ,UserName                  AS UserName     " + Environment.NewLine;
        //sql += " ,LoginName                 AS LoginName    " + Environment.NewLine;
        //sql += " ,GroupName                 AS GroupName    " + Environment.NewLine;
        //sql += " FROM [UserInfo] LEFT JOIN                  " + Environment.NewLine;
        //sql += "  [GroupInfo] ON GroupInfo.ID = GroupID     " + Environment.NewLine;
        //// Add BY JiJianxiong 2010-07-09 ST
        //sql += " WHERE UserInfo.ID <> 0                     " + Environment.NewLine;
        //// Add BY JiJianxiong 2010-07-09 ED

        //// Add By Jijianxiong 2010-07-14 ST
        //if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        //{
        //    sql += " AND LoginName = '" + User.Identity.Name + "'";
        //}
        //// Add By Jijianxiong 2010-07-14 ED
        //sql += " ORDER BY UserInfo.ID " + Environment.NewLine;
        string sql = "";
        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            sql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {           
            sql = "SELECT                                       " + Environment.NewLine;
            sql += "  UserInfo.ID               AS Id           " + Environment.NewLine;
            sql += " ,UserName                  AS UserName     " + Environment.NewLine;
            sql += " ,LoginName                 AS LoginName    " + Environment.NewLine;
            sql += " ,GroupName                 AS GroupName    " + Environment.NewLine;
            sql += " FROM [UserInfo] LEFT JOIN                  " + Environment.NewLine;
            sql += "  [GroupInfo] ON GroupInfo.ID = GroupID     " + Environment.NewLine;
            //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ST
           // sql += " WHERE UserInfo.ID <> 0                     " + Environment.NewLine;
            //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ED
            if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
            {
                //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
                //sql += " AND LoginName = '" + User.Identity.Name + "'";
                sql += " WHERE LoginName = '" + User.Identity.Name + "'";
                //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
            }
    
            sql += " ORDER BY UserInfo.ID " + Environment.NewLine;
        }
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;
        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED
        SqlDataListSource.SelectCommand = sql;
        /*
        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            this.btnSelectAll, "UserName,LoginName,GroupName,Id");
         * */

        SetListMainPgae(this.CustomersGridView,
           this.ddlNumPerPage,
           this.ddlIndexOfPage,
           this.lblTotalPage,
           null,"UserName,LoginName,GroupName,Id");
        if (!IsPostBack)
        {
            if (this.CustomersGridView.Rows.Count > 0)
            {
                this.CustomersGridView.Sort("UserName", SortDirection.Ascending);
            }
            //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_USER, UtilConst.CON_ITEM_USERNAME));
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_GROUP, UtilConst.CON_ITEM_GROUPNAME));
            //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED
        }

        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
        this.Master.btn_Search().Click += new EventHandler(btn_SearchClick);
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED
       
        CustomersGridView.DataKeyNames = new String[] { "ID" };
    }
    #endregion


    #region "Occurs when a data row is bound to data in GridView."
    /// <summary>
    /// Occurs when a data row is bound to data in GridView.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void CustomView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Check Row Records
        // Defulat Date Can't be Delete
        // User's Admin
        // Group's no belong
        GridView gridView = (GridView)sender;
        GridViewRow gRow = (GridViewRow)e.Row;
        //pupeng
        //2014-5-30
        //if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        //{
        //    if (int.Parse(e.Row.Cells[6].Text) <= int.Parse(e.Row.Cells[10].Text))
        //    {
        //        e.Row.Cells[10].Text = e.Row.Cells[6].Text;
        //    }
        //}
        if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                TextBox txtc = (TextBox)gRow.FindControl("txtRemainColor");
                //txtc.Visible = false;
                txtc.Enabled = false;
                TextBox txt = (TextBox)gRow.FindControl("txtRemain");
                txt.Enabled = false;
                Button lb = (Button)gRow.FindControl("Button1");
                if (lb != null)
                {
                    lb.Enabled = false;
                    lb.Visible = false;
                }
              
               // lb.Visible = false;
                
            }
        }
       
       
    }
    #endregion

    #region "btnSearch_Click"
    /// <summary>
    /// btnSearch_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
      
        Table tblDetail = new Table();
        for (int i = 0; i < CustomersGridView.Rows.Count; i++)
        {
              TableRow row = new TableRow();
              for (int j = 0; j < CustomersGridView.Columns.Count; j++)
              {
                  TableCell tCell = new TableCell();
                  if (j == 1 || j == 3)
                  {
                      tCell.Text = CustomersGridView.Rows[i].Cells[j].Text.ToString();
                      row.Cells.Add(tCell);
                  }
                  else if(j==5)
                  {
                      tCell.Text = ((Label)(CustomersGridView.Rows[i].Cells[j].FindControl("lblRemain"))).Text;
                      row.Cells.Add(tCell);
                  }
                  else if (j == 10)
                  {
                      tCell.Text = ((Label)(CustomersGridView.Rows[i].Cells[j].FindControl("lblRemainColor"))).Text;
                      row.Cells.Add(tCell);
                      break;
                  }
              }
              tblDetail.Rows.Add(row);
        }
            OutPutCsvFile("AvailableReport", tblDetail);

    }
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
      
        List<string> strHeadList = new List<string>();
        strHeadList.Add("用户余额管理");
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
   
        strHeadList.Add(UtilConst.CON_TIME_PERIOD);
        strHeadList.Add(this.lbl_period.Text);
       
        
        CsvList.Add(strHeadList);
        strHeadList = new List<string>();
        strHeadList.Add("用户,用户组名,余额,彩色余额");
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
                      //  strList.Add("");
                      //  strList.Add("");
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
      //  sb.Append("用户，用户组名，余额，彩色余额");
        int linenum = -1; //行数，因为希望第4行不要导出
        foreach (List<string> item in CsvList)
        {
            linenum++;
        
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

        //Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.Write(sb);
        Response.End();
    }
    #endregion
    #endregion

    #region "Function:Raises the GridView.Sorted event."
    /// <summary>
    /// Function:Raises the GridView.Sorted event.
    /// It'll be overrided In each page.
    /// </summary>
    /// <param name="gv"></param>
    /// <seealso cref="SortGridView"/>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public override void SortGridView(GridView cView)
    {
        // Sort ▲▼
        string sortString = "";
        int index = 0;


        // 2010.09.09 Update By SES JiJianXiong ST
        // Change the columns id.
        //cView.Columns[0].HeaderText = "";
        //cView.Columns[1].HeaderText = UtilConst.CON_ITEM_USERNAME;
        //cView.Columns[2].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        cView.Columns[0].HeaderText = "";
        cView.Columns[1].HeaderText = UtilConst.CON_ITEM_USERNAME;
        cView.Columns[3].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        // 2010.09.09 Update By SES JiJianXiong ED
        if (cView.SortExpression.Equals(""))
        {
            return;
        }

        if ("UserName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_USERNAME;
            // 2010.09.09 Update By SES JiJianXiong ST
            // Change the columns id.
            // index = 1;
            index = 1;
            // 2010.09.09 Update By SES JiJianXiong ED
        }

        if ("GroupName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_GROUPNAME;
            // 2010.09.09 Update By SES JiJianXiong ST
            // Change the columns id.
            // index = 2;
            index = 3;
            // 2010.09.09 Update By SES JiJianXiong ED
        }

        if (SortDirection.Ascending.Equals(cView.SortDirection))
        {
            sortString = UtilConst.CON_ITEM_SORT_ASC + sortString;
        }
        else
        {
            sortString = UtilConst.CON_ITEM_SORT_DESC + sortString;
        }

        cView.Columns[index].HeaderText = sortString;
    }
    #endregion

    #region "User or Group btnSearch Clicked event"
    /// <summary>
    /// User or Group btnSearch Clicked event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.3</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    protected void btn_SearchClick(object sender, EventArgs e)
    {
        SqlDataListSource.SelectCommand = SearchSql();

        CustomersGridView.DataBind();
        this.ddlNumPerPage.SelectedIndex = 0;
        // Set the PageIndex property to display that page selected by the user.
        this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());
        /*
        SetListMainPgae(this.CustomersGridView,
           this.ddlNumPerPage,
           this.ddlIndexOfPage,
           this.lblTotalPage,
           this.btnSelectAll, "UserName,LoginName,GroupName,Id");
         * */
        SetListMainPgae(this.CustomersGridView,
          this.ddlNumPerPage,
          this.ddlIndexOfPage,
          this.lblTotalPage,
         null, "UserName,LoginName,GroupName,Id");
        this.CustomersGridView.Sort("UserName", SortDirection.Ascending);
        //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ST
        if (CustomersGridView.Rows.Count == 0)
        {
            // no date for Search process.
            ErrorAlert(UtilConst.MSG_NOTHING_SEARCH);
        }
        //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED

    }
    #endregion

    #region"Function: March the User Name / Login Name with Search Words and displayed in  AvailableReport management"
    /// <summary>
    /// Function: March the User Name / Group Name with Search Words and displayed in  AvailableReport management
    /// </summary>
    /// <returns>Sql</returns>
    /// <Date>2010.12.3</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private string SearchSql()
    {
        string sql = "SELECT                                " + Environment.NewLine;
        sql += "  UserInfo.ID               AS Id           " + Environment.NewLine;
        sql += " ,UserName                  AS UserName     " + Environment.NewLine;
        sql += " ,LoginName                 AS LoginName    " + Environment.NewLine;
        sql += " ,GroupName                 AS GroupName    " + Environment.NewLine;
        sql += " FROM [UserInfo] LEFT JOIN                  " + Environment.NewLine;
        sql += "  [GroupInfo] ON GroupInfo.ID = GroupID     " + Environment.NewLine;
        //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ST
        //sql += " WHERE UserInfo.ID <> 0                     " + Environment.NewLine;
        //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ED
        if (!(string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim())))
        {
            if (UtilConst.CON_ITEM_USER.Equals(this.Master.ddl_SearchList().SelectedValue))
            {
                //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ST
                //sql += "AND UserName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
                sql += "WHERE UserName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
                //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ED
            }
            else
            {
                //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ST
                //sql += "AND GroupName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
                sql += "WHERE GroupName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
                //2011.3.28 Delete By SES zhoumiao Ver.1.1 Update ED
            }
        }

        sql += " ORDER BY UserInfo.ID " + Environment.NewLine;

        ViewState["SearchKeyWord"] = sql;
        return sql;
    }
    public string getRemainByUserID(int intUserId)
    {

        dtJobInformation.JobInformationDataTable jobdatatable = null;


        dtRestrictionInfo.RestrictionInfoDataTable resmoneydatatable = null;
        //private dtUserInfo.UserInfoDataTable userdatatable = null;
        dtUserPayDetail.UserPayDetailDataTable userPaydatatable = null;
            //pupeng 2014 04 26
            decimal UsedMoney = 0, UsedColorMoney = 0, PayMoney = 0, PayColorMoney = 0, RemainMoney = 0, RemainColorMoney = 0.0M;
       
            // UserID 
       
            UserInfoTableAdapter userAdapter = new UserInfoTableAdapter();
            dtUserInfo.UserInfoRow userRow = userAdapter.GetDataByUserId(intUserId)[0];
          
            int restrictid = UtilCommon.GetUserRestrictidFromDB(userRow.ID);
         
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
                //RemainMoney = resRow.IsAllQuotaNull() ? 0 : resRow.AllQuota;
                //RemainColorMoney = resRow.IsColorQuotaNull() ? 0 : resRow.ColorQuota;
                RemainMoney = resRow.AllQuota;
                RemainColorMoney = resRow.ColorQuota;
            }
            //用户追加额度
            if (userPaydatatable.Count > 0)
            {
                dtUserPayDetail.UserPayDetailRow payRow = userPaydatatable[0];
                PayMoney = payRow.IsSumMoneyNull() ? 0 : payRow.SumMoney;
                PayColorMoney = payRow.IsSumColorMoneyNull() ? 0 : payRow.SumColorMoney;
            }
            //用户配额 + 透支额度 -  已用额度  作为用户可用额度
            RemainMoney = RemainMoney + PayMoney - UsedMoney;
            RemainColorMoney = RemainColorMoney + PayColorMoney - UsedColorMoney;
            return RemainMoney.ToString();

        
    }
    public string getRemainColorByUserID(int intUserId)
    {

        dtJobInformation.JobInformationDataTable jobdatatable = null;


        dtRestrictionInfo.RestrictionInfoDataTable resmoneydatatable = null;
        //private dtUserInfo.UserInfoDataTable userdatatable = null;
        dtUserPayDetail.UserPayDetailDataTable userPaydatatable = null;
        //pupeng 2014 04 26
        decimal UsedMoney = 0, UsedColorMoney = 0, PayMoney = 0, PayColorMoney = 0, RemainMoney = 0, RemainColorMoney = 0.0M;

        // UserID 

        UserInfoTableAdapter userAdapter = new UserInfoTableAdapter();
        dtUserInfo.UserInfoRow userRow = userAdapter.GetDataByUserId(intUserId)[0];

        int restrictid = UtilCommon.GetUserRestrictidFromDB(userRow.ID);

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
            //RemainMoney = resRow.IsAllQuotaNull() ? 0 : resRow.AllQuota;
            //RemainColorMoney = resRow.IsColorQuotaNull() ? 0 : resRow.ColorQuota;
            RemainMoney = resRow.AllQuota;
            RemainColorMoney = resRow.ColorQuota;
        }
        //用户追加额度
        if (userPaydatatable.Count > 0)
        {
            dtUserPayDetail.UserPayDetailRow payRow = userPaydatatable[0];
            PayMoney = payRow.IsSumMoneyNull() ? 0 : payRow.SumMoney;
            PayColorMoney = payRow.IsSumColorMoneyNull() ? 0 : payRow.SumColorMoney;
        }
        //用户配额 + 透支额度 -  已用额度  作为用户可用额度
        RemainMoney = RemainMoney + PayMoney - UsedMoney;
        RemainColorMoney = RemainColorMoney + PayColorMoney - UsedColorMoney;
        if (RemainColorMoney > RemainMoney)
            RemainColorMoney = RemainMoney;
        return RemainColorMoney.ToString();


    }
    #endregion


    protected void CustomersGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string remain, remainColor, lblremain, lblremainColor;
        string userid;
        remain = ((TextBox)(CustomersGridView.Rows[e.RowIndex].FindControl("txtRemain"))).Text;
        remainColor = ((TextBox)(CustomersGridView.Rows[e.RowIndex].FindControl("txtRemainColor"))).Text;
        lblremain = ((Label)(CustomersGridView.Rows[e.RowIndex].FindControl("lblRemain"))).Text;
        lblremainColor = ((Label)(CustomersGridView.Rows[e.RowIndex].FindControl("lblRemainColor"))).Text;
        userid=CustomersGridView.DataKeys[e.RowIndex].Value.ToString();
        
        using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {

                if (float.Parse(remain) == 0F && float.Parse(remainColor) == 0F || (float.Parse(remain)+float.Parse(lblremain)) < (float.Parse(remainColor)+float.Parse(lblremainColor)))
                {
                    //Response.Write("<script language='javascript'>alert('金额不能为0，并且余额必须大于彩色余额')</script>");
                    ErrorAlert("金额不能为0，并且余额必须大于彩色余额");
                    return;
                }
                if (float.Parse(remain) >= 99999.99F || float.Parse(remainColor) >= 99999.99F )
                {
                
                    ErrorAlert("余额和彩色余额不能超过最大上限99999.99");
                    return;
                }
                if (float.Parse(remain) <= -99999.99F || float.Parse(remainColor) <= -99999.99F)
                {

                    ErrorAlert("余额和彩色余额不能小于下限-99999.99");
                    return;
                }
                if (float.Parse(remain) + float.Parse(lblremain) <= -99999.99F || float.Parse(remain) + float.Parse(lblremain) >= 99999.99F)
                {

                    ErrorAlert("余额必须位于-99999.99和99999.99之间");
                    return;
                }
                if (float.Parse(lblremainColor) + float.Parse(remainColor) <= -99999.99F || float.Parse(lblremainColor) + float.Parse(remainColor) >= 99999.99F)
                {

                    ErrorAlert("彩色余额必须位于-99999.99和99999.99之间");
                    return;
                }
                string strSql;
                string[] paramslist = new string[3];
                paramslist[0] = ConvertIntToSQL(userid);
                paramslist[1] = ConvertMoneyToSQL(remain);
                paramslist[2] = ConvertMoneyToSQL(remainColor);
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

                tran.Commit();
             //   DisplayDetailResult();
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
                e.Cancel = true;
            }
           
        }
    }
    protected void CustomersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}
