
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
using System.Diagnostics;
using System.Data.SqlClient;
using SesMiddleware;

public partial class UserInfo_UserList : ListMainPage
{


    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_USERLIST;
        //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ST
        this.Master.CheakSearchItem = true;
        //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ED
        // Check Access Role
        CheckUser();

        SqlDataListSource.ConnectionString = this.DBConnectionStrings;
        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ST       
        //string sql = "SELECT ";
        //sql += "  UserInfo.ID               AS Id";
        //sql += " ,UserName                  AS UserName";
        //sql += " ,LoginName                 AS LoginName";
        //sql += " ,Password                  AS Password";
        //sql += " ,ICCardID                  AS ICCardID";
        //sql += " ,UserInfo.RestrictionID    AS RestrictionID";
        //sql += " ,RestrictionName           AS RestrictionName";
        //sql += " ,GroupID                   AS GroupID";
        //sql += " ,GroupName                 AS GroupName";
        //sql += " FROM [UserInfo] LEFT JOIN ";
        //sql += "  [GroupInfo] ON GroupInfo.ID = GroupID ";
        //sql += "                 LEFT JOIN ";
        //sql += "  [RestrictionInfo] ON RestrictionInfo.ID = UserInfo.RestrictionID ";
        //sql += " ORDER BY UserInfo.UpdateTime DESC ";
        string sql = null;
        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            sql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {
            sql = "SELECT ";
            sql += "  UserInfo.ID               AS Id";
            sql += " ,UserName                  AS UserName";
            sql += " ,LoginName                 AS LoginName";
            sql += " ,Password                  AS Password";
            sql += " ,ICCardID                  AS ICCardID";
            //2015 04 22  add start
            sql += " ,PinCode                   AS PinCode";
            //end
            //chen add start
            sql += " ,Email                     AS Email";
            //chen add end
            sql += " ,UserInfo.RestrictionID    AS RestrictionID";
            sql += " ,RestrictionName           AS RestrictionName";
            sql += " ,GroupID                   AS GroupID";
            sql += " ,GroupName                 AS GroupName";
            sql += " FROM [UserInfo] LEFT JOIN ";
            sql += "  [GroupInfo] ON GroupInfo.ID = GroupID ";
            sql += "                 LEFT JOIN ";
            sql += "  [RestrictionInfo] ON RestrictionInfo.ID = UserInfo.RestrictionID ";

            //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ST
            sql += " WHERE [UserInfo].ID <> {0} ";
            sql += " AND [UserInfo].ID <> {1} ";
            //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ED

            sql += " ORDER BY UserInfo.UpdateTime DESC ";
        }

        //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ST
        sql = string.Format(sql, UtilConst.CON_DATE_ID, UtilConst.CON_SECUADMIN_ID);       
        //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ED

        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;

        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED
        SqlDataListSource.SelectCommand = sql;

        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ST
        //SetListMainPgae(this.CustomersGridView,
        //    this.ddlNumPerPage,
        //    this.ddlIndexOfPage,
        //    this.lblTotalPage,
        //    this.btnSelectAll, "UserName,LoginName,GroupName,Id");
        SetListMainPgae(this.CustomersGridView,
                this.ddlNumPerPage,
                this.ddlIndexOfPage,
                this.lblTotalPage,
                //chen update start
                //this.btnSelectAll, "Id,UserName,LoginName,Password,ICCardID,RestrictionID,RestrictionName,GroupID,GroupName");
                this.btnSelectAll, "Id,UserName,LoginName,Password,ICCardID,PinCode, Email, RestrictionID,RestrictionName,GroupID,GroupName");
                //chen update end
              

        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED

        // Add By JiJianxiong 2010-07-09 ST
        if (!IsPostBack)
        {
            if (this.CustomersGridView.Rows.Count > 0)
            {
                //2010.11.15 Add By SES zhoumiao Ver.1.1 Update ST
                //Function:Set the Width of GridView for User Management.
                SetGridViewWidth();
                //2010.11.15 Add By SES zhoumiao Ver.1.1 Update ED
                this.CustomersGridView.Sort("UserName", SortDirection.Ascending);
            }
            //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_USER, UtilConst.CON_ITEM_USERNAME));
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_LOGIN, UtilConst.CON_ITEM_LOGINNAME));
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_IDCARD, UtilConst.CON_ITEM_IDCARDNAME));
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_PINCODE, UtilConst.CON_ITEM_PINCODENAME));
            
            //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED
        }
        // Add By JiJianxiong 2010-07-09 ED

        btnDeleteUser.OnClientClick = ConfirmFunction(UtilConst.MSG_USER_DELETE);
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
        this.Master.btn_Search().Click += new EventHandler(btn_SearchClick);
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED
    }

    #region "Function:Set the Width of GridView for User Management"
    /// <summary>
    /// Function:Set the Width of GridView for User Management.
    /// </summary>
    /// <Date>2010.11.15</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>Ver.1.1 Update </Version>
    public void SetGridViewWidth()
    {
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        //CardID AND Restrict Are DIVISIBLE
        if ((row.Dis_U_CardID==0) && (row.Dis_U_Restrict==0))
        {
            CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_U_1_USERNAME_WIDTH);
            CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_U_1_LOGINNAME_WIDTH);
            CustomersGridView.Columns[5].Visible = false;
            CustomersGridView.Columns[6].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[6].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[8].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[8].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            //CustomersGridView.Columns[10].HeaderStyle.Width = new Unit(UtilConst.CSS_U_1_GROUPAME_WIDTH);
            CustomersGridView.Columns[10].Visible = false;
        }
        //Only CardID  DIVISIBLE
        else if ((row.Dis_U_CardID==0) && (row.Dis_U_Restrict==1))
        {
            CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_U_2_USERNAME_WIDTH);
            CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_U_2_LOGINNAME_WIDTH);
            CustomersGridView.Columns[5].Visible = false;
            CustomersGridView.Columns[6].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[6].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            //CustomersGridView.Columns[7].Visible = true;
            CustomersGridView.Columns[8].HeaderStyle.Width = new Unit(UtilConst.CSS_U_2_RESTRICT_WIDTH);
            //CustomersGridView.Columns[10].HeaderStyle.Width = new Unit(UtilConst.CSS_U_2_GROUPAME_WIDTH);
            CustomersGridView.Columns[10].Visible = true;

        }
        //Only Restrict DIVISIBLE
        else if ((row.Dis_U_CardID==1) && (row.Dis_U_Restrict==0))
        {
            CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_U_3_USERNAME_WIDTH);
            CustomersGridView.Columns[4].HeaderStyle.Width = new Unit(UtilConst.CSS_U_3_LOGINNAME_WIDTH);
            CustomersGridView.Columns[5].Visible = true;
            CustomersGridView.Columns[6].HeaderStyle.Width = new Unit(UtilConst.CSS_U_3_CARDID_WIDTH);
            //CustomersGridView.Columns[7].Visible = false;
            CustomersGridView.Columns[8].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[8].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            //CustomersGridView.Columns[10].HeaderStyle.Width = new Unit(UtilConst.CSS_U_3_GROUPAME_WIDTH);
            CustomersGridView.Columns[10].Visible = false;

        }        

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
        // 2010.08.23 Update By SES JiJianXiong ST
        // Change the columns id.
        //cView.Columns[1].HeaderText = UtilConst.CON_ITEM_USERNAME;
        //cView.Columns[2].HeaderText = UtilConst.CON_ITEM_LOGINNAME;
        //cView.Columns[3].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        cView.Columns[2].HeaderText = UtilConst.CON_ITEM_USERNAME;
        cView.Columns[4].HeaderText = UtilConst.CON_ITEM_LOGINNAME;
        //2010.11.15 Update By SES zhoumiao Ver.1.1 Update ST
        //cView.Columns[6].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        cView.Columns[6].HeaderText = UtilConst.CON_ITEM_ICCARD;
        //20140424 DELETE BY TAN FOR Email column START
        //cView.Columns[8].HeaderText = UtilConst.CON_ITEM_RESTRICSET;
        //20140424 DELETE BY TAN FOR Email column END
        cView.Columns[10].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        //2010.11.15 Update By SES zhoumiao Ver.1.1 Update ED
        // 2010.08.23 Update By SES JiJianXiong ED
        
        if (cView.SortExpression.Equals(""))
        {
            return;
        }

        if ("UserName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_USERNAME;
            // 2010.08.23 Update By SES JiJianXiong ST
            // Change the columns id.
            //index = 1;
            index = 2;
            // 2010.08.23 Update By SES JiJianXiong ED
        }

        if ("LoginName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_LOGINNAME;
            // 2010.08.23 Update By SES JiJianXiong ST
            // Change the columns id.
            //index = 2;
            index = 4;
            // 2010.08.23 Update By SES JiJianXiong ED
        }
        //2010.11.15 Add By SES zhoumiao Ver.1.1 Update ST
        if ("ICCardID".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_ICCARD;
            index = 6;

        }

        if ("RestrictionName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_RESTRICSET;
            index = 8;

        }
        //2010.11.15 Add By SES zhoumiao Ver.1.1 Update ED

        if ("GroupName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_GROUPNAME;
            // 2010.08.23 Update By SES JiJianXiong ST
            // Change the columns id.
            //index = 3;
            //2010.11.15 Update By SES zhoumiao Ver.1.1 Update ST
            //index = 6;
            index = 10;
            //2010.11.15 Update By SES zhoumiao Ver.1.1 Update ED
            // 2010.08.23 Update By SES JiJianXiong ED
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

    #region "Delete User List"
    /// <summary>
    /// Delete Group List
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.11</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.02</Version>
    protected void btnDeleteUser_Click(object sender, EventArgs e)
    {
        bool process = false;

        bool usedataflg = false;
        //
        try
        {
            string strDeleteLoginName = "";
            string strLogInName = "";

            string strDeleteUserId = "";
            string strUserId = "";

            //chen add for delete user
            dtJobInformationTableAdapters.JobInformationTableAdapter jobAdapter = new dtJobInformationTableAdapters.JobInformationTableAdapter();
            //
            foreach (GridViewRow gRow in CustomersGridView.Rows)
            {
                CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                if (ch.Checked)
                {
                    //2010.08.20 Update By SES JiJianXiong ST
                    // With New UI, changed.
                    // strLogInName = gRow.Cells[4].Text;
                    // strUserId = gRow.Cells[6].Text;
                    //2010.11.15 Update By SES zhoumiao Ver.1.1 Update ST
                    //strLogInName = gRow.Cells[7].Text;
                    //strUserId = gRow.Cells[9].Text;
                    strLogInName = gRow.Cells[gRow.Cells.Count - 3].Text;
                    strUserId = gRow.Cells[gRow.Cells.Count - 1].Text;
                    //2010.11.15 Update By SES zhoumiao Ver.1.1 Update ED
                    //2010.08.20 Update By SES JiJianXiong ED

                    //chen add 20140606
                    int jobCount = (int)jobAdapter.ScalarJobInfomationByUserID(int.Parse(strUserId));
                    if (jobCount > 0)
                    {
                        usedataflg = true;
                        //conitnue;
                        strDeleteLoginName = "";
                        break;
                    }
                    //
                    if ("".Equals(strDeleteLoginName))
                    {
                        strDeleteLoginName = ConvertStringToSQL(strLogInName);
                        strDeleteUserId = ConvertIntToSQL(strUserId);
                    }
                    else
                    {
                        strDeleteLoginName += "," + ConvertStringToSQL(strLogInName);
                        strDeleteUserId += "," + ConvertIntToSQL(strUserId);
                    }

                }
            }


            //If Delete Group is nothing,then return
            if (!string.IsNullOrEmpty(strDeleteLoginName))
            {

                // 2010.08.23 Add By SES JiJianXiong ST
                // Set the flg to true.
                process = true;

                // 2010.08.23 Add By SES JiJianXiong ED
                using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {
                        // 1,Delete GroupInfo
                        string sql = "DELETE FROM [UserInfo] WHERE LoginName in ({0})";
                        sql = string.Format(sql, strDeleteLoginName);

                        using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Delete JobInfo
                        sql = "DELETE FROM [JobInformation] WHERE UserID in ({0})";
                        sql = string.Format(sql, strDeleteUserId);

                        using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        // 3. Delete UserPayDetail
                        sql = "DELETE FROM [UserPayDetail] WHERE UserID in ({0})";
                        sql = string.Format(sql, strDeleteUserId);
                        using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // Delete User's XML
                        String strCardID = "";
                        foreach (GridViewRow gRow in CustomersGridView.Rows)
                        {
                            CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                            if (ch.Checked)
                            {
                                //chen add 20140606
                                //int jobCount = (int)jobAdapter.ScalarJobInfomationByUserID(int.Parse(strUserId));
                                //if (jobCount > 0)
                                //{
                                //    usedataflg = true;
                                //    continue;
                                //}
                                //
                                if (strCardID == "")
                                {
                                    //2010.08.20 Update By SES JiJianXiong ST
                                    // strCardID = gRow.Cells[5].Text;
                                    //2010.11.16 Update By SES zhoumiao Ver.1.1 Update ST
                                    //strCardID = gRow.Cells[8].Text;
                                    //strCardID = gRow.Cells[14].Text;
                                    strCardID = gRow.Cells[gRow.Cells.Count - 2].Text;
                                    //2010.11.16 Update By SES zhoumiao Ver.1.1 Update ED
                                    //2010.08.20 Update By SES JiJianXiong ED
                                }
                                else
                                {
                                    //2010.08.20 Update By SES JiJianXiong ST
                                    // strCardID = strCardID + "," + gRow.Cells[5].Text;
                                    //2010.11.16 Update By SES zhoumiao Ver.1.1 Update ST
                                    //strCardID = strCardID + "," + gRow.Cells[8].Text;
                                    //strCardID = strCardID + "," + gRow.Cells[14].Text;
                                    strCardID = strCardID + "," + gRow.Cells[gRow.Cells.Count - 2].Text;
                                    //2010.11.16 Update By SES zhoumiao Ver.1.1 Update ED                                    
                                    //2010.08.20 Update By SES JiJianXiong ED
                                }

                            }
                        }

                        int returnVal = ICCardData.DeleteICCardInfo(strCardID, this.ServerPath);
                        if (returnVal != 0)
                        {
                            throw new Exception(UtilConst.MSG_MIDDLEWARE_ERROR);
                        }

                        tran.Commit();
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


            CustomersGridView.DataBind();

            // 2010.08.23 Add By SES JiJianXiong ST
            // check with the flg,and alert message.
            if (process)
            {
                // success in delete record.
                if (usedataflg)
                {
                    SuccessMessage(UtilConst.MSG_DELETE_SUCCESS_2);
                }
                else
                {
                    SuccessMessage(UtilConst.MSG_DELETE_SUCCESS);
                }
            }
            else
            {
                // no date for delete process.
                if (usedataflg)
                {
                    ErrorAlert(UtilConst.MSG_DELETE_NOTHING_2);
                }
                else
                {
                    ErrorAlert(UtilConst.MSG_DELETE_NOTHING);
                }
            }
            // 2010.08.23 Add By SES JiJianXiong ED
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }

    }
    #endregion

    #region "Add New User."
    /// <summary>
    /// Add New User.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.11</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.02</Version>
    protected void btnAddNewUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserInfoAdd.aspx");
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
        GridViewRow gRow = (GridViewRow)e.Row;
        string strId = "";

        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            CheckBox cbox = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
            strId = ((DataRowView)e.Row.DataItem).Row["LoginName"].ToString();
            if (strId.Equals(UtilConst.CON_USER_LONINNAME))
            {
                cbox.Enabled = false;
            }
            else
            {
                cbox.Enabled = true;
            }
            // 2010.08.23 Update By SES JiJianXiong ST
            //// Set Color for even Row
            //if (!Convert.ToBoolean(e.Row.RowIndex % 2))
            //{
            //    e.Row.CssClass = UtilConst.CSS_ITEM_EVEN;
            //}

            // Css Set
            if (cbox.Checked)
            {
                e.Row.CssClass = "SelectedTR";
            }
            else
            {
                e.Row.CssClass = "UnselectedTR";
            }
            // 2010.08.23 Update By SES JiJianXiong ST
        }
    }
    #endregion

    #region "btnSearch Clicked event"
    /// <summary>
    /// btnSearch Clicked  event
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

        SetListMainPgae(this.CustomersGridView,
                this.ddlNumPerPage,
                this.ddlIndexOfPage,
                this.lblTotalPage,
                //chen add start
                //this.btnSelectAll, "Id,UserName,LoginName,Password,ICCardID,RestrictionID,RestrictionName,GroupID,GroupName");
                this.btnSelectAll, "Id,UserName,LoginName,Password,ICCardID, PinCode, Email,RestrictionID,RestrictionName,GroupID,GroupName");
                //chen add end

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

    #region"Function: March the User Name / Login Name with Search Words and displayed in  UserList management"
    /// <summary>
    /// Function: March the User Name / Login Name with Search Words and displayed in  UserList management
    /// </summary>
    /// <returns>Sql</returns>
    /// <Date>2010.12.3</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    private string SearchSql()
    {
        string sql = "SELECT ";
        sql += "  UserInfo.ID               AS Id";
        sql += " ,UserName                  AS UserName";
        sql += " ,LoginName                 AS LoginName";
        sql += " ,Password                  AS Password";
        sql += " ,ICCardID                  AS ICCardID";
        //2015 04 22  add start
        sql += " ,PinCode                   AS PinCode";
        //end

        //chen add
        sql += " ,Email                     AS Email";
        //
        sql += " ,UserInfo.RestrictionID    AS RestrictionID";
        sql += " ,RestrictionName           AS RestrictionName";
        sql += " ,GroupID                   AS GroupID";
        sql += " ,GroupName                 AS GroupName";
        sql += " FROM [UserInfo] LEFT JOIN ";
        sql += "  [GroupInfo] ON GroupInfo.ID = GroupID ";
        sql += "                 LEFT JOIN ";
        sql += "  [RestrictionInfo] ON RestrictionInfo.ID = UserInfo.RestrictionID ";
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ST
        //if (!(string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim())))
        //{
        //    if (UtilConst.CON_ITEM_USER.Equals(this.Master.ddl_SearchList().SelectedValue))
        //    {
        //        sql += "WHERE UserName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
        //    }
        //    else
        //    {
        //        sql += "WHERE LoginName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
        //    }
        //}

        sql += " WHERE [UserInfo].ID <> {0}  and [UserInfo].ID <> {1} ";
        if (!(string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim())))
        {
            if (UtilConst.CON_ITEM_USER.Equals(this.Master.ddl_SearchList().SelectedValue))
            {
                sql += " AND UserName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
            }
            else if (UtilConst.CON_ITEM_IDCARD.Equals(this.Master.ddl_SearchList().SelectedValue))
            {
                sql += " AND ICCardID LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
            }
            else if (UtilConst.CON_ITEM_PINCODE.Equals(this.Master.ddl_SearchList().SelectedValue))
            {
                sql += " AND PinCode LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
            }
            else 
            {
                sql += " AND LoginName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
            }

        }
        //2011.3.28 Update By SES zhoumiao Ver.1.1 Update ED
        sql += " ORDER BY UserInfo.UpdateTime DESC ";

        //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ST
        sql = string.Format(sql, UtilConst.CON_DATE_ID, UtilConst.CON_SECUADMIN_ID);
        //2011.3.28 Add By SES zhoumiao Ver.1.1 Update ED

        ViewState["SearchKeyWord"] = sql;
        return sql;
    }
    #endregion

}

