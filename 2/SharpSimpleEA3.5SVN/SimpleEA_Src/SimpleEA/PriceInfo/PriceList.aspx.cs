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
/// Restriction Information List
/// </summary>
/// <Date>2010.06.14</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class PriceInfo_PriceList : ListMainPage
{
    public PriceInfo_PriceList()
    {
    }

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.14</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e) 
    {
        // SQL
        string strSql;

        // Title
        this.Master.Title = UtilConst.CON_PAGE_Price;
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
        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            strSql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {

            strSql = "   SELECT PriceNM " + Environment.NewLine;
            strSql += "    ,PriceID                                " + Environment.NewLine;
            strSql += "    FROM  [PriceMaster]                    " + Environment.NewLine;
            strSql += " ORDER BY UpdateTime DESC                     " + Environment.NewLine;
        }
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;        

        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED 
        SqlDataListSource.SelectCommand = strSql;

        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            this.btnSelectAll, "PriceNM,PriceID");

   
        if (!IsPostBack)
        {
            if (this.CustomersGridView.Rows.Count > 0)
            {
                this.CustomersGridView.Sort("PriceNM", SortDirection.Ascending);
            }

            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_STRIC, UtilConst.CON_ITEM_PRICENAME));
         
     
        }
    
        btnDeleteGrp.OnClientClick = ConfirmFunction("是否删除选中的价格");
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
        this.Master.btn_Search().Click += new EventHandler(btn_SearchClick);
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED
    }
    #endregion

    #region"Function:Check  options  Whether is seted of  Setting management"
    /// <summary>
    /// Function:Check  options  Whether is seted of  Setting management
    /// </summary>
    /// <Date>2010.11.19</Date>
    /// <Author>SES zhou miao</Author>
    /// <Version>1.1</Version>
    private Boolean ContentSetCheck()
    {
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();

        if (row.Dis_R_Copy == 1)
        {
            return true;
        }
        else if (row.Dis_R_Print == 1)
        {
            return true;
        }
        else if (row.Dis_R_Scan == 1)
        {
            return true;
        }
        else if (row.Dis_R_Fax == 1)
        {
            return true;
        }

        return false;

       
    }
    #endregion

  
    #region "Function:Raises the GridView.Sorted event."
    /// <summary>
    /// Function:Raises the GridView.Sorted event.
    /// It'll be overrided In each page.
    /// </summary>
    /// <param name="gv"></param>
    /// <seealso cref="SortGridView"/>
    /// <Date>2010.06.14</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public override void SortGridView(GridView cView)
    {
        // Sort ▲▼
        string sortString = "";
        int index = 0;


        cView.Columns[0].HeaderText = "";
      
        cView.Columns[2].HeaderText = UtilConst.CON_ITEM_RESTRICNAME;
        // 2010.09.09 Update By SES JiJianXiong ED
        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED
        if (cView.SortExpression.Equals(""))
        {
            return;
        }

        if ("PriceNM".Equals(cView.SortExpression))
        {
            sortString = UtilConst.CON_ITEM_PRICENAME;
            // 2010.09.09 Update By SES JiJianXiong ST
            // Change the columns id.
            // index = 1;
            index = 2;
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

    #region "Delete Restriction Set List"
    /// <summary>
    /// Delete Restriction Set List
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.08</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.02</Version>
    protected void btnDeleteRes_Click(object sender, EventArgs e)
    {
        try
        {
            string strDeleteResId = "";
            string strResId = "";
            // 2010.09.10 Add By SES JiJianXiong ST
            // the flg for the delete process
            bool process = false;
            // 2010.09.10 Add By SES JiJianXiong ED

            foreach (GridViewRow gRow in CustomersGridView.Rows)
            {
                CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                if (ch.Checked)
                {
                    // 2010.09.09 Update By SES JiJianXiong ST
                    // Change the columns id.
                    // strResId = gRow.Cells[2].Text;
                    //strResId = gRow.Cells[3].Text;
                    strResId = gRow.Cells[gRow.Cells.Count - 1].Text;
                    // 2010.09.09 Update By SES JiJianXiong ED
                    if ("".Equals(strDeleteResId))
                    {
                        strDeleteResId = strResId;
                    }
                    else
                    {
                        strDeleteResId += "," + strResId;
                    }

                }
            }

            //If Delete Group is nothing,then return
            if (!string.IsNullOrEmpty(strDeleteResId))
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
                        // 1,Delete Restriction Set Management Information
                        string sql = "DELETE FROM [PriceMaster] WHERE PriceID in ({0})";
                        sql = string.Format(sql, strDeleteResId);

                        using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // 2,Delete Restriction Set Detail Information.
                        sql = "DELETE FROM [PriceDetail] WHERE PriceID in ({0})";
                        sql = string.Format(sql, strDeleteResId);

                        using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        //pupeng 2014 6-4
                        //3,UDATE MFP DETAIL.
                        sql = "UPDATE [MFPInformation] SET PRICEID=" + UtilConst.CON_ID_RESTRICLIST_GENERAL + " WHERE PriceID in ({0})";
                        sql = string.Format(sql, strDeleteResId);

                        using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
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

            // 2010.09.10 Add By SES JiJianXiong ST
            // check with the flg,and alert message.
            if (process)
            {
                // success in delete record.
                SuccessMessage(UtilConst.MSG_DELETE_SUCCESS_PRICE);
            }
            else
            {
                // no date for delete process.
                ErrorAlert(UtilConst.MSG_DELETE_NOTHING_PRICE);
            }
            // 2010.09.10 Add By SES JiJianXiong ED
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }
    }
    #endregion

   #region "Add New Restriction Set"
   /// <summary>
   /// Add New Restriction Set
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
   /// <Date>2010.06.14</Date>
   /// <Author>SES Ji JianXiong</Author>
   /// <Version>0.02</Version>
   protected void btnAddNewRes_Click(object sender, EventArgs e)
   {
       Response.Redirect("AddPrice.aspx");
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
           strId = ((DataRowView)e.Row.DataItem).Row["Priceid"].ToString();
           if (strId.Equals(UtilConst.CON_DATE_ID))
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
           // 2010.08.23 Update By SES JiJianXiong ED
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
                  this.btnSelectAll, "PriceNM,PriceId");

       this.CustomersGridView.Sort("PriceNM", SortDirection.Ascending);
       //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ST
       if (CustomersGridView.Rows.Count == 0)
       {
           // no date for Search process.
           ErrorAlert(UtilConst.MSG_NOTHING_SEARCH);
       }
       //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED

   }
   #endregion

   #region"Function: March the Restriction Set Name with Search Words and displayed in  Restriction management"
   /// <summary>
   /// Function: March the User Restriction Set Name with Search Words and displayed in  Restriction management
   /// </summary>
   /// <returns>Sql</returns>
   /// <Date>2010.12.3</Date>
   /// <Author>SES Zhou Miao</Author>
   /// <Version>1.1</Version>
   private string SearchSql()
   {
       string strSql = "";
           strSql = "   SELECT PriceNM  " + Environment.NewLine;
           strSql += "    ,PriceID                                " + Environment.NewLine;
           strSql += "    FROM  [PriceMaster] A                  " + Environment.NewLine;
           if (!string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim()))
           {
               strSql += "WHERE PriceNM LIKE  " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
           }
           strSql += " ORDER BY UpdateTime DESC                     " + Environment.NewLine;
       ViewState["SearchKeyWord"] = strSql;
       return strSql;
   }
   #endregion

}
