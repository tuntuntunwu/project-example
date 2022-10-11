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

/// <summary>
/// GroupList
/// </summary>
/// <Date>2010.06.07</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class GroupList : ListMainPage
{
    public GroupList()
    {
    }

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e) 
    {

        this.Master.Title = UtilConst.CON_PAGE_GROUPLIST;
        //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ST
        this.Master.CheakSearchItem = true;
        //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ED
        //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ST
        this.Master.ddl_SearchList().Enabled = false;
        //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED
        // Check Access Role
        CheckUser();

        //Set SqlDataSource
        SqlDataListSource.ConnectionString = this.DBConnectionStrings;

        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ST 

        //2010.11.17 Update By SES zhoumiao Ver.1.1 Update ST
        //// Update By JiJianxiong 2010-07-09 ST
        //// Select Group Infor
        ////strSql = "   SELECT A.GroupName AS GroupName            " + Environment.NewLine;
        ////strSql += "         ,A.Id AS Id                         " + Environment.NewLine;
        ////strSql += "         ,COUNT(B.GroupId) AS UserCount      " + Environment.NewLine;
        ////strSql += "    FROM  [GroupInfo] A                      " + Environment.NewLine;
        ////strSql += "    LEFT OUTER JOIN                          " + Environment.NewLine;
        ////strSql += "          [UserInfo] B                       " + Environment.NewLine;
        ////strSql += "      ON A.Id = B.GroupId                    " + Environment.NewLine;
        ////strSql += "GROUP BY A.GroupName,A.Id                    " + Environment.NewLine;
        ////strSql += "ORDER BY A.Id                                " + Environment.NewLine;
        //string strSql = "   SELECT A.GroupName AS GroupName    " + Environment.NewLine +
        //         "         ,A.ID AS Id                         " + Environment.NewLine +
        //         "         ,ISNULL( (SELECT COUNT(B.GroupID)   " + Environment.NewLine +
        //         "                     FROM [UserInfo] B       " + Environment.NewLine +
        //         "                    WHERE B.GroupID = A.ID   " + Environment.NewLine +
        //         "                   ) ,0)  AS UserCount       " + Environment.NewLine +
        //         "    FROM  [GroupInfo] A                      " + Environment.NewLine +
        //         "   WHERE  A.ID <> {0}                        " + Environment.NewLine +
        //         " ORDER BY A.UpdateTime DESC                  " + Environment.NewLine;
        //// Update By JiJianxiong 2010-07-09 ED
        
        //string strSql = "   SELECT A.GroupName AS GroupName    " + Environment.NewLine +
        //         "         ,A.ID AS Id                         " + Environment.NewLine +
        //         "         ,ISNULL( (SELECT COUNT(U.GroupID)   " + Environment.NewLine +
        //         "                     FROM [UserInfo] U       " + Environment.NewLine +
        //         "                    WHERE U.GroupID = A.ID   " + Environment.NewLine +
        //         "                   ) ,0)  AS UserCount       " + Environment.NewLine +
        //         "       ,R.RestrictionName  AS RestrictionName" + Environment.NewLine +
        //         "    FROM  [GroupInfo] A                      " + Environment.NewLine +
        //         "LEFT JOIN [RestrictionInfo] R ON             " + Environment.NewLine +
        //         "             R.ID = A.RestrictionID          " + Environment.NewLine +
        //         "   WHERE  A.ID <> {0}                        " + Environment.NewLine +
        //         " ORDER BY A.UpdateTime DESC                  " + Environment.NewLine;
        //2010.11.17 Update By SES zhoumiao Ver.1.1 Update ED
        // strSql = string.Format(strSql, UtilConst.CON_DATE_ADMIN_ID);

        string strSql = null;
         if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
         {
             strSql = ViewState["SearchKeyWord"].ToString();
         }
         else
         {
              strSql = "   SELECT A.GroupName AS GroupName    " + Environment.NewLine +
                      "         ,A.ID AS Id                         " + Environment.NewLine +
                      "         ,ISNULL( (SELECT COUNT(U.GroupID)   " + Environment.NewLine +
                      "                     FROM [UserInfo] U       " + Environment.NewLine +
                      "                    WHERE U.GroupID = A.ID   " + Environment.NewLine +
                      "                   ) ,0)  AS UserCount       " + Environment.NewLine +
                      "       ,R.RestrictionName  AS RestrictionName" + Environment.NewLine +
                      "    FROM  [GroupInfo] A                      " + Environment.NewLine +
                      "LEFT JOIN [RestrictionInfo] R ON             " + Environment.NewLine +
                      "             R.ID = A.RestrictionID          " + Environment.NewLine +
                      "   WHERE  A.ID <> {0}                        " + Environment.NewLine +
                      " ORDER BY A.UpdateTime DESC                  " + Environment.NewLine;



              strSql = string.Format(strSql, UtilConst.CON_DATE_ADMIN_ID);
          }
          this.CustomersGridView.DataSource = null;
          this.CustomersGridView.DataSourceID = SqlDataListSource.ID;

          //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED 

        SqlDataListSource.SelectCommand = strSql;
        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ST
        //SetListMainPgae(this.CustomersGridView,
        //    this.ddlNumPerPage,
        //    this.ddlIndexOfPage,
        //    this.lblTotalPage,
        //    this.btnSelectAll, "GroupName,UserCount,Id");
        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            this.btnSelectAll, "GroupName,UserCount,Id,RestrictionName");

        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED

        // Add By JiJianxiong 2010-07-09 ST
        if (!IsPostBack)
        {
            if (this.CustomersGridView.Rows.Count > 0)
            {
                //2010.11.17 Add By SES zhoumiao Ver.1.1 Update ST
                //Function:Set the Width of GridView for Group Management.
                SetGridViewWidth();
                //2010.11.17 Add By SES zhoumiao Ver.1.1 Update ED
                this.CustomersGridView.Sort("GroupName", SortDirection.Ascending);
            }

            //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_GROUP, UtilConst.CON_ITEM_GROUPNAME));

            //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED
        }
        // Add By JiJianxiong 2010-07-09 ED

        btnDeleteGrp.OnClientClick = ConfirmFunction(UtilConst.MSG_GROUP_DELETE);
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
        this.Master.btn_Search().Click += new EventHandler(btn_SearchClick);
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED
    }
    #endregion

    #region "Function:Set the Width of GridView for Group Management"
    /// <summary>
    /// Function:Set the Width of GridView for Group Management.
    /// </summary>
    /// <Date>2010.11.17</Date>
    /// <Author>SES zhoumiao</Author>
    /// <Version>1.1</Version>
    public void SetGridViewWidth()
    {
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        // Restrict is DIVISIBLE
        if (row.Dis_G_Restrict==0)
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
    /// <Date>2010.06.07</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public override void SortGridView(GridView cView)
    {
        // Sort ¡ø¨‹
        string sortString = "";
        int index = 0;

        // 2010.09.09 Update By SES JiJianXiong ST
        // Change the columns id.
        //cView.Columns[0].HeaderText = "";
        //cView.Columns[1].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        //cView.Columns[2].HeaderText = UtilConst.CON_ITEM_GROUPUSERCOUNT;
        cView.Columns[0].HeaderText = "";
        cView.Columns[2].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        cView.Columns[4].HeaderText = UtilConst.CON_ITEM_GROUPUSERCOUNT;
        // 2010.09.09 Update By SES JiJianXiong ED
        //2010.11.17 Add By SES zhoumiao Ver.1.1 Update ST
        cView.Columns[6].HeaderText = UtilConst.CON_ITEM_RESTRICSET;
        //2010.11.17 Add By SES zhoumiao Ver.1.1 Update ED
        if (cView.SortExpression.Equals(""))
        {
            return;
        }

        if ("GroupName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_GROUPNAME;
            // 2010.09.09 Update By SES JiJianXiong ST
            // Change the columns id.
            // index = 1;
            index = 2;
            // 2010.09.09 Update By SES JiJianXiong ED
        }

        if ("UserCount".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_GROUPUSERCOUNT;
            // 2010.09.09 Update By SES JiJianXiong ST
            // Change the columns id.
            // index = 2;
            index = 4;
            // 2010.09.09 Update By SES JiJianXiong ED
        }
        //2010.11.17 Add By SES zhoumiao Ver.1.1 Update ST
        if ("RestrictionName".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_RESTRICSET;
            index = 6;

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

    #region "Delete Group List"
    /// <summary>
    /// Delete Group List
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.02</Version>
    protected void btnDeleteGrp_Click(object sender, EventArgs e)
    {
        try
        {
            string strDeleteGroupId = "";
            string strGroupId = "";
            // 2010.09.10 Add By SES JiJianXiong ST
            // the flg for the delete process
            bool process = false;
            // 2010.09.10 Add By SES JiJianXiong ED


            foreach (GridViewRow gRow in CustomersGridView.Rows)
            {
                CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                if (ch.Checked)
                {
                    //2010.11.17 Update By SES zhoumiao Ver.1.1 Update ST
                    // 2010.09.09 Update By SES JiJianXiong ST
                    // Change the columns id.
                    // strGroupId = gRow.Cells[3].Text;
                    //strGroupId = gRow.Cells[5].Text;
                    // 2010.09.09 Update By SES JiJianXiong ED
                    strGroupId = gRow.Cells[8].Text;
                    //2010.11.17 Update By SES zhoumiao Ver.1.1 Update ED
                    if ("".Equals(strDeleteGroupId))
                    {
                        strDeleteGroupId = strGroupId;
                    }
                    else
                    {
                        strDeleteGroupId += "," + strGroupId;
                    }

                }
            }

            //If Delete Group is nothing,then return
            if (!string.IsNullOrEmpty(strDeleteGroupId))
            {
                // 2010.09.10 Add By SES JiJianXiong ST
                // Set the flg to true.
                process = true;
                // 2010.09.10 Add By SES JiJianXiong ED
                using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {
                        // 1,Delete GroupInfo
                        string sql = "DELETE FROM [GroupInfo] WHERE ID in ({0})";
                        sql = string.Format(sql, strDeleteGroupId);

                        SqlCommand cmd = new SqlCommand(sql, con, tran);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        // 2,Set User's Group To No Belong Group.
                        sql = "UPDATE [UserInfo] SET GroupID = {0} WHERE GroupID in ({1})";
                        sql = string.Format(sql, UtilConst.CON_DATE_ID, strDeleteGroupId);

                        cmd = new SqlCommand(sql, con, tran);
                        cmd.ExecuteNonQuery();

                        // 3,Set Job Information Group To No Belong Group.
                        sql = "UPDATE [JobInformation] SET GroupID = {0} WHERE GroupID in ({1})";
                        sql = string.Format(sql, UtilConst.CON_DATE_ID, strDeleteGroupId);

                        cmd = new SqlCommand(sql, con, tran);
                        cmd.ExecuteNonQuery();

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
            // 2010.09.10 Add By SES JiJianXiong ST
            // check with the flg,and alert message.
            if (process)
            {
                // success in delete record.
                SuccessMessage(UtilConst.MSG_DELETE_SUCCESS_GROUP);
            }
            else
            {
                // no date for delete process.
                ErrorAlert(UtilConst.MSG_DELETE_NOTHING_GROUP);
            }
            // 2010.09.10 Add By SES JiJianXiong ED
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }

   }
    #endregion

    #region "Add New Group"
   /// <summary>
   /// Add New Group
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   /// <Date>2010.06.11</Date>
   /// <Author>SES Ji JianXiong</Author>
   /// <Version>0.02</Version>
   protected void btnAddNewGrp_Click(object sender, EventArgs e)
   {
        Response.Redirect("GroupInfoAdd.aspx");
   }
   #endregion

    #region "Occurs when a data row is bound to data in GridView."
   /// <summary>
   /// Occurs when a data row is bound to data in GridView.
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   /// <Date>2010.06.08</Date>
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
           strId = ((DataRowView)e.Row.DataItem).Row["id"].ToString();
           if (strId.Equals(UtilConst.CON_DATE_ID))
           {
               cbox.Enabled = false;
           }
           else
           {
               cbox.Enabled = true;
           }

           // 2010.09.09 Update By SES JiJianXiong ST
           // Change the CSS.
           // Set Color for even Row
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
           // 2010.09.09 Update By SES JiJianXiong ED
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
            this.btnSelectAll, "GroupName,UserCount,Id,RestrictionName");

       this.CustomersGridView.Sort("GroupName", SortDirection.Ascending);
       //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ST
       if (CustomersGridView.Rows.Count == 0)
       {
           // no date for Search process.
           ErrorAlert(UtilConst.MSG_NOTHING_SEARCH);
       }
       //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED

   }
   #endregion

    #region"Function: March the Group Name with Search Words and displayed in  GroupList management"
   /// <summary>
   /// Function: March the Group Name with Search Words and displayed in  GroupList management
   /// </summary>
   /// <returns>Sql</returns>
   /// <Date>2010.12.3</Date>
   /// <Author>SES Zhou Miao</Author>
   /// <Version>1.1</Version>
   private string SearchSql()
   {
       string sql = "   SELECT A.GroupName AS GroupName      " + Environment.NewLine +
                "         ,A.ID AS Id                         " + Environment.NewLine +
                "         ,ISNULL( (SELECT COUNT(U.GroupID)   " + Environment.NewLine +
                "                     FROM [UserInfo] U       " + Environment.NewLine +
                "                    WHERE U.GroupID = A.ID   " + Environment.NewLine +
                "                   ) ,0)  AS UserCount       " + Environment.NewLine +
                "       ,R.RestrictionName  AS RestrictionName" + Environment.NewLine +
                "    FROM  [GroupInfo] A                      " + Environment.NewLine +
                "LEFT JOIN [RestrictionInfo] R ON             " + Environment.NewLine +
                "             R.ID = A.RestrictionID          " + Environment.NewLine +
                "   WHERE  A.ID <> {0}                        " + Environment.NewLine;

       if (!(string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim())))
       {
           sql += "AND A.GroupName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");          
       }

       sql += " ORDER BY A.UpdateTime DESC                  " + Environment.NewLine;
       sql = string.Format(sql, UtilConst.CON_DATE_ADMIN_ID);

       ViewState["SearchKeyWord"] = sql;
       return sql;
   }
   #endregion

    //#region
   protected void btnSectionSet_Click(object sender, EventArgs e)
   {
       this.Response.Redirect("SectionGroupSetting.aspx", false);
   }
    //#endregion

}
