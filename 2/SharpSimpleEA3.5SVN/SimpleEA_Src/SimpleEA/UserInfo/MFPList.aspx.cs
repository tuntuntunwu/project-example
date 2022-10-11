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

public partial class UserInfo_MFPList : ListMainPage
{
    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.07</Date>
    /// <Author>SLC Zheng Wei</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Master.NewTitle (UtilConst.CON_PAGE_EDIT, "UserList.aspx", false);
        this.Master.Title = UtilConst.CON_PAGE_USERLIST;
        this.Master.ViceSubTitle(UtilConst.CON_PAGE_MFP_RES, "UserInfoEdit.aspx?UserId=" + this.Request.Params["UserID"], false);
        this.Master.CheakSearchItem = true;


        // edit by Wei Changye 2012.05.29
        if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            SetButtonEnable(false);
            this.Master.SubTitle(UtilConst.CON_PAGE_EDIT, "#", false);
        }
        else
        {
            SetButtonEnable(true);
            this.Master.SubTitle(UtilConst.CON_PAGE_EDIT, "UserList.aspx", false);
        }

        hidUserId.Value = Page.Request.Params["UserID"].ToString();
        // GroupId
        string UserId = hidUserId.Value;

        CheckUserInUserEdit(UserId);

        // Check Access Role
        //CheckUser();
        //end edit


        //Set SqlDataSource
        SqlDataListSource.ConnectionString = this.DBConnectionStrings;

        string strSql = null;
        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            strSql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {
            strSql = "   select A.SerialNumber as SerialNumber    " + Environment.NewLine +
                     "  ,A.Location as Location" + Environment.NewLine +
                     "  ,A.ModelName as ModelName" + Environment.NewLine +
                     "  ,'{0}'               AS    UserID " + Environment.NewLine +
                     "  ,A.IPAddress as IPAddress " + Environment.NewLine +
                     " ,CASE ISNULL( (SELECT COUNT(1) " + Environment.NewLine +
                     "      FROM MFPUserRes U" + Environment.NewLine +
                     "     WHERE       U.IPAddress = A.IPAddress" + Environment.NewLine +
                     "     AND      U.UserId = {0}) , 0 )  " + Environment.NewLine +
                     "    WHEN 0 THEN '允许'" + Environment.NewLine +
                     "    ELSE '禁止' END AS FLG " + Environment.NewLine +
                     " ,A.SerialNumber               AS SerialNumber" + Environment.NewLine +
                     "FROM  MFPInformation A" + Environment.NewLine +
            "WHERE 1= 1" + Environment.NewLine;

            strSql = strSql + "ORDER BY  A.SerialNumber " + Environment.NewLine;

            strSql = string.Format(strSql, this.Request.Params["UserID"]);
            
        }
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;


        SqlDataListSource.SelectCommand = strSql;
        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            this.btnSelectAll, "ModelName,IPAddress,Location,FLG,SerialNumber"); 

        if (!IsPostBack)
        {
            if (this.CustomersGridView.Rows.Count > 0)
            {
                //Function:Set the Width of GridView for Group Management.
                SetGridViewWidth();
                this.CustomersGridView.Sort("ModelName", SortDirection.Ascending);
            }

            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem("0", "MFP型号"));
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem("1", "MFP IP地址"));

        }

        this.Master.btn_Search().Click += new EventHandler(btn_SearchClick);
    }
    #endregion

    #region "Function:Set the Width of GridView for Group Management"
    /// <summary>
    /// Function:Set the Width of GridView for Group Management.
    /// </summary>
    /// <Date>2012.03.07</Date>
    /// <Author>SLC Zheng Wei</Author>
    /// <Version>1.1</Version>
    public void SetGridViewWidth()
    {
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        // Restrict is DIVISIBLE
        if (row.Dis_G_Restrict == 0)
        {
            CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_G_GROUPAME_WIDTH);
            CustomersGridView.Columns[5].Visible = false;
            CustomersGridView.Columns[6].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[6].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
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
    /// <Date>2012.03.07</Date>
    /// <Author>SLC Zheng Wei</Author>
    /// <Version>0.01</Version>
    public override void SortGridView(GridView cView)
    {
        // Sort ▲▼
        string sortString = "";
        int index = 0;

        cView.Columns[0].HeaderText = "";
        cView.Columns[2].HeaderText = "MFP型号";
        cView.Columns[4].HeaderText = "MFP IP地址";
        cView.Columns[6].HeaderText = "限制状态";
        cView.Columns[8].HeaderText = "描述";

        if (cView.SortExpression.Equals(""))
        {
            return;
        }
        else
            if ("ModelName".Equals(cView.SortExpression))
            {
                sortString = "MFP型号";
                index = 2;
            }
            else
                if ("IPAddress".Equals(cView.SortExpression))
                {
                    sortString = "MFP IP地址";
                    index = 4;
                }
                else
                    if ("FLG".Equals(cView.SortExpression))
                    {
                        sortString = "限制状态";
                        index = 6;

                    }
                    else
                        if ("Location".Equals(cView.SortExpression))
                        {
                            sortString = "描述";
                            index = 8;

                        }
        //2010.11.17 Add By SES zhoumiao Ver.1.1 Update ED

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

    #region "Occurs when a data row is bound to data in GridView."
    /// <summary>
    /// Occurs when a data row is bound to data in GridView.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.07</Date>
    /// <Author>SLC Zheng Wei</Author>
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
            strId = ((DataRowView)e.Row.DataItem).Row["id"].ToString();
            if (strId.Equals(UtilConst.CON_DATE_ID))
            {
                cbox.Enabled = false;
            }
            else
            {
                cbox.Enabled = true;
            }

            if (cbox.Checked)
            {
                e.Row.CssClass = "SelectedTR";
            }
            else
            {
                e.Row.CssClass = "UnselectedTR";
            }
        }
    }
    #endregion

    #region "btnSearch Clicked event"
    /// <summary>
    /// btnSearch Clicked  event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.07</Date>
    /// <Author>SLC Zheng Wei</Author>
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
             this.btnSelectAll, "ModelName,IPAddress,Location,UserID,FLG,SerialNumber");

        this.CustomersGridView.Sort("ModelName", SortDirection.Ascending);
        if (CustomersGridView.Rows.Count == 0)
        {
            // no date for Search process.
            ErrorAlert(UtilConst.MSG_NOTHING_SEARCH);
        }

    }
    #endregion

    #region"Function: March the Group Name with Search Words and displayed in  GroupList management"
    /// <summary>
    /// Function: March the Group Name with Search Words and displayed in  GroupList management
    /// </summary>
    /// <returns>Sql</returns>
    /// <Date>2012.03.07</Date>
    /// <Author>SLC Zheng Wei</Author>
    /// <Version>1.1</Version>
    private string SearchSql()
    {
        string strSql = "   SELECT  A.ModelName   AS    ModelName    " + Environment.NewLine +
                 "  ,A.IPAddress        AS    IPAddress" + Environment.NewLine +
                 "  ,A.Location      AS    Location" + Environment.NewLine +
                 "  ,'{0}'               AS    UserID " + Environment.NewLine +
                 " ,CASE ISNULL( (SELECT COUNT(1) " + Environment.NewLine +
                 "      FROM MFPUserRes U" + Environment.NewLine +
                 "     WHERE       U.IPAddress = A.IPAddress" + Environment.NewLine +
                 "     AND      U.UserId = {0}) , 0 )  " + Environment.NewLine +
                 "    WHEN 0 THEN '允许'" + Environment.NewLine +
                 "    ELSE '禁止' END AS FLG " + Environment.NewLine +
                 " ,A.SerialNumber               AS SerialNumber" + Environment.NewLine +
                 "FROM  MFPInformation A" + Environment.NewLine +
        "WHERE 1= 1" + Environment.NewLine;

       if (!(string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim())))
       {
           if ("0".Equals(this.Master.ddl_SearchList().SelectedValue))
           {
               strSql += " AND ModelName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
           }
           else
           {
               strSql += " AND IPAddress LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
           }
       }

       strSql = strSql + "ORDER BY  SerialNumber " + Environment.NewLine;
       strSql = string.Format(strSql, this.Request.Params["UserID"]);

        ViewState["SearchKeyWord"] = strSql;
        return strSql;
    }
    #endregion

    #region Button Event
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/UserInfo/UserInfoEdit.aspx?UserId=" + this.Request.Params["UserID"]);
    }

    /// <summary>
    /// Allow User to use the selected MFP
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            string strIPAddress = "";
            // the flg for the delete process
            bool process = false;


            foreach (GridViewRow gRow in CustomersGridView.Rows)
            {
                CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                if (ch.Checked)
                {

                    // Set the flg to true.
                    process = true;

                    break;
                }
            }

            //If Delete Group is nothing,then return
            if (process)
            {

                using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {


                        // 2,Insert User's Res To MFPUserRes

                        foreach (GridViewRow gRow in CustomersGridView.Rows)
                        {
                            CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                            if (ch.Checked)
                            {
                                strIPAddress = gRow.Cells[9].Text;

                                // 1,Delete User's Res Infromation
                                string sql = "DELETE FROM [MFPUserRes] WHERE IPAddress = '{0}' AND UserID = {1}";
                                sql = string.Format(sql, strIPAddress, this.Request.Params["UserID"]);

                                SqlCommand cmd = new SqlCommand(sql, con, tran);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                            }
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
            // check with the flg,and alert message.
            if (process)
            {
                // success in delete record.
                SuccessMessage(UtilConst.MSG_UPDATE_ALLOW_MFPRES);
            }
            else
            {
                // no date for delete process.
                ErrorAlert(UtilConst.MSG_UPDATE_NOTHING_MFPRES);
            }
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }
    }
    /// <summary>
    /// Not Allow to use MFP
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        try
        {
            string strIPAddress = "";
            // the flg for the delete process
            bool process = false;


            foreach (GridViewRow gRow in CustomersGridView.Rows)
            {
                CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                if (ch.Checked)
                {

                    // Set the flg to true.
                    process = true;

                    break;
                }
            }

            //If Delete Group is nothing,then return
            if (process)
            {

                using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {


                        // 2,Insert User's Res To MFPUserRes

                        foreach (GridViewRow gRow in CustomersGridView.Rows)
                        {
                            CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                            if (ch.Checked)
                            {
                                strIPAddress = gRow.Cells[9].Text;

                                // 1,Check the User's Res Exist
                                string sql = " SELECT COUNT(1) FROM MFPUserRes WHERE IPAddress = '{0}' And UserID = {1}";
                                sql = string.Format(sql, strIPAddress, this.Request.Params["UserID"]);
                                SqlCommand cmdSelect = new SqlCommand(sql, con, tran);
                                object result = cmdSelect.ExecuteScalar();
                                cmdSelect.Dispose();
                                
                                if (result == null || result.Equals(0))
                                {

                                    // 2,Insert User's Res Infromation
                                    sql = "INSERT INTO [MFPUserRes] ([IPAddress],[UserID]) VALUES ('{0}', {1})";
                                    sql = string.Format(sql, strIPAddress, this.Request.Params["UserID"]);

                                    SqlCommand cmd = new SqlCommand(sql, con, tran);
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                }
                            }
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
            // check with the flg,and alert message.
            if (process)
            {
                // success in delete record.
                SuccessMessage(UtilConst.MSG_UPDATE_PORFBI_MFPRES);
            }
            else
            {
                // no date for delete process.
                ErrorAlert(UtilConst.MSG_UPDATE_NOTHING_MFPRES);
            }
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }
    }
    #endregion

    /// <summary>
    /// only admin can allow and forbit
    /// </summary>
    /// <param name="isAdmin"></param>
    private void SetButtonEnable(bool isAdmin)
    {
        btnAllow.Enabled = isAdmin;
        btnNotAllow.Enabled = isAdmin;
    }

}
