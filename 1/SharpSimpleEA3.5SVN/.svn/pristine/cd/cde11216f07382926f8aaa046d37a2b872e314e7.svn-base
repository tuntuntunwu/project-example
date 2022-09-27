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
public partial class RestrictionInfo_RestrictList : ListMainPage
{
    public RestrictionInfo_RestrictList()
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
        this.Master.Title = UtilConst.CON_PAGE_RESTRICLIST;
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

            if (!ContentSetCheck())
            {
                //No  options set of  Setting management
                // Select Group Infor
                strSql = "   SELECT A.RestrictionName AS RestrictionName  " + Environment.NewLine;
                strSql += "    ,A.ID AS Id,AllQuota,colorQuota,OverLimit  " + Environment.NewLine;
                strSql += "    FROM  [RestrictionInfo] A                  " + Environment.NewLine;
                //chen add 20140616 start
                strSql += "    WHERE ID <> '-1'                  " + Environment.NewLine;
                //chen add 20140616 end

                // Update By JiJianxiong 2010-07-09 ST
                //strSql += "ORDER BY A.Id                                  " + Environment.NewLine;
                strSql += " ORDER BY A.UpdateTime DESC                     " + Environment.NewLine;
                // Select Group Infor
            }
            else
            {
                //pupeng 201404016 只显示配额名称，点击后显示明细
                // options is seted in  Setting management
                //strSql = SetListSql();
                strSql = "   SELECT A.RestrictionName AS RestrictionName  " + Environment.NewLine;
                strSql += "    ,A.ID AS Id,AllQuota,colorQuota,OverLimit  " + Environment.NewLine;
                strSql += "    FROM  [RestrictionInfo] A                   " + Environment.NewLine;
                //chen add 20140616 start
                strSql += "    WHERE ID <> '-1'                  " + Environment.NewLine;
                //chen add 20140616 end
                strSql += " ORDER BY A.UpdateTime DESC                     " + Environment.NewLine;
                //pupeng end
               
         
            }
        }
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;        

        //2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED 
        SqlDataListSource.SelectCommand = strSql;

        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            this.btnSelectAll, "RestrictionName,AllQuota,colorQuota,OverLimit,Id");

        // Add By JiJianxiong 2010-07-09 ST
        if (!IsPostBack)
        {
            if (this.CustomersGridView.Rows.Count > 0)
            {
                this.CustomersGridView.Sort("RestrictionName", SortDirection.Ascending);
            }
            //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_STRIC, UtilConst.CON_ITEM_RESTRICNAME));
         
            //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ED
        }
        // Add By JiJianxiong 2010-07-09 ED

        btnDeleteGrp.OnClientClick = ConfirmFunction(UtilConst.MSG_RESTRIC_DELETE);
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


    #region"Function: the options is seted in  Setting management"
    /// <summary>
    /// Function: the options is seted in  Setting management
    /// </summary>
    /// <Date>2010.12.3</Date>
    /// <Author>SES zhou miao</Author>
    /// <Version>1.1</Version>
    private string SetListSql()
    {

        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        string strSql;
        strSql = " SELECT RestrictionID AS Id,                             " + Environment.NewLine;
        strSql += "(RestrictionName+'( '+BWCopy+FCCopy+BWPrint+            " + Environment.NewLine;
        strSql += " FCPrint+BWScan+FCScan+Fax+')') AS RestrictionName      " + Environment.NewLine;
        strSql += " FROM(SELECT A.RestrictionID,A.RestrictionName          " + Environment.NewLine;
        strSql += ",B.BWCopy  AS BWCopy                                    " + Environment.NewLine;
        strSql += ",C.FCCopy  AS FCCopy                                    " + Environment.NewLine;
        strSql += ",D.BWPrint AS BWPrint                                   " + Environment.NewLine;
        strSql += ",E.FCPrint  AS FCPrint                                  " + Environment.NewLine;
        strSql += ",Q.BWScan   AS BWScan                                   " + Environment.NewLine;
        strSql += ",W.FCScan AS FCScan                                     " + Environment.NewLine;
        strSql += ",M.Fax  AS Fax  FROM                                    " + Environment.NewLine;
        strSql += "(SELECT ID AS RestrictionID                             " + Environment.NewLine;
        strSql += ",RestrictionName AS RestrictionName                     " + Environment.NewLine;
        strSql += " FROM RestrictionInfo)A LEFT JOIN                       " + Environment.NewLine;

        // 2010.12.07 Update By SES.Jijianxiong ST
        // Specification_SimpleEA(V1.27)_20101203.doc Update.
        ///* B/W Copy Restriction display property in the Restriction Set Management screen.*/
        //strSql += SetSql("BWCopy", UtilConst.ITEM_TITLE_Copy_FunctionId1, UtilConst.ITEM_TITLE_Copy_JobId, row.Dis_R_BWCopy, "B");
        ///* Full-Color Copy Restriction display property in the Restriction Set Management screen.*/
        //strSql += SetSql("FCCopy", UtilConst.ITEM_TITLE_Copy_FunctionId2, UtilConst.ITEM_TITLE_Copy_JobId, row.Dis_R_FCCopy, "C");
        ///*When B/W Print Restriction display property in the Restriction Set Management screen.*/
        //strSql += SetSql("BWPrint", UtilConst.ITEM_TITLE_Print_FunctionId1, UtilConst.ITEM_TITLE_Print_JobId, row.Dis_R_BWPrint, "D");
        ///* Full-Color Print Restriction display property in the Restriction Set Management screen.*/
        //strSql += SetSql("FCPrint", UtilConst.ITEM_TITLE_Print_FunctionId2, UtilConst.ITEM_TITLE_Print_JobId, row.Dis_R_FCPrint, "E");
        ///*B/W Scan Restriction display property in the Restriction Set Management screen.*/
        //strSql += SetSql("BWScan", UtilConst.ITEM_TITLE_Scan_FunctionId1, UtilConst.ITEM_TITLE_Scan_JobId, row.Dis_R_BWScan, "Q");
        ///*Full-Color Scan Restriction display property in the Restriction Set Management screen.*/
        //strSql += SetSql("FCScan", UtilConst.ITEM_TITLE_Scan_FunctionId2, UtilConst.ITEM_TITLE_Scan_JobId, row.Dis_R_FCScan, "W");
        ///*Fax Restriction display property in the Restriction Set Management screen.*/
        //strSql += SetSql("Fax", UtilConst.ITEM_TITLE_Fax_FunctionId1, UtilConst.ITEM_TITLE_Fax_JobId, row.Dis_R_Fax, "M");
        /* B/W Copy Restriction display property in the Restriction Set Management screen.*/
        strSql += SetSql("BWCopy", UtilConst.ITEM_TITLE_Copy_FunctionId1, UtilConst.ITEM_TITLE_Copy_JobId, row.Dis_R_Copy, "B");
        /* Full-Color Copy Restriction display property in the Restriction Set Management screen.*/
        strSql += SetSql("FCCopy", UtilConst.ITEM_TITLE_Copy_FunctionId2, UtilConst.ITEM_TITLE_Copy_JobId, row.Dis_R_Copy, "C");
        /*When B/W Print Restriction display property in the Restriction Set Management screen.*/
        strSql += SetSql("BWPrint", UtilConst.ITEM_TITLE_Print_FunctionId1, UtilConst.ITEM_TITLE_Print_JobId, row.Dis_R_Print, "D");
        /* Full-Color Print Restriction display property in the Restriction Set Management screen.*/
        strSql += SetSql("FCPrint", UtilConst.ITEM_TITLE_Print_FunctionId2, UtilConst.ITEM_TITLE_Print_JobId, row.Dis_R_Print, "E");
        /*B/W Scan Restriction display property in the Restriction Set Management screen.*/
        strSql += SetSql("BWScan", UtilConst.ITEM_TITLE_Scan_FunctionId1, UtilConst.ITEM_TITLE_Scan_JobId, row.Dis_R_Scan, "Q");
        /*Full-Color Scan Restriction display property in the Restriction Set Management screen.*/
        strSql += SetSql("FCScan", UtilConst.ITEM_TITLE_Scan_FunctionId2, UtilConst.ITEM_TITLE_Scan_JobId, row.Dis_R_Scan, "W");
        /*Fax Restriction display property in the Restriction Set Management screen.*/
        strSql += SetSql("Fax", UtilConst.ITEM_TITLE_Fax_FunctionId1, UtilConst.ITEM_TITLE_Fax_JobId, row.Dis_R_Fax, "M");
        // 2010.12.07 Update By SES.Jijianxiong ED


        return strSql;

    }

    #endregion

    #region"display property in the Restriction Set Management screen"
    /// <summary>
    /// display property in the Restriction Set Management screen
    /// </summary>
    /// <param name="Jobname"></param>
    /// <param name="FunctionId"></param>
    /// <param name="JobId"></param>
    /// <param name="DisItem"></param>
    /// <param name="tablename"></param>
    /// <returns>SQL</returns>
    /// <Date>2010.12.3</Date>
    /// <Author>SES zhou miao</Author>
    /// <Version>1.1</Version>
    public String SetSql(string Jobname, int FunctionId, int JobId, int DisItem, string tablename)
    {
        string strSql = "";
        string strJobname = "";
        string strjoin = "LEFT JOIN  ";
        //  B/W or Full-Color to determine
        if (FunctionId==1)
        {
            strJobname = UtilConst.COLOR_BW;
        }
        else if (FunctionId==2)
        {
            strJobname = UtilConst.COLOR_FULLCOLOR;
        }
        //Copy,Print,Scan,Fax to determine
        if (JobId == UtilConst.ITEM_TITLE_Copy_JobId)
        {
            strJobname += UtilConst.ITEM_TITLE_Copy;
        }
        else if (JobId == UtilConst.ITEM_TITLE_Print_JobId)
        {
            strJobname += UtilConst.ITEM_TITLE_Print;
        }
        else if (JobId == UtilConst.ITEM_TITLE_Scan_JobId)
        {
            strJobname += UtilConst.ITEM_TITLE_Scan;
        }
        else if (JobId == UtilConst.ITEM_TITLE_Fax_JobId)
        {
            strJobname = UtilConst.ITEM_TITLE_Fax;
            strjoin = ") Y";
        }
        //Sql setted
        strSql = " (SELECT RestrictionID                                               " + Environment.NewLine;
        if (DisItem == 0)
        {
            //The display property of the Restriction screen not  show
            strSql += ",'" + Jobname + "'=''                                           " + Environment.NewLine;            
        }
        else
        {
            //The display property of the Restriction screen   show
            strSql += "," +ConvertStringToSQL(Jobname) + "= CASE WHEN                  " + Environment.NewLine;
            strSql += "  Status='1'                                                    " + Environment.NewLine;
            strSql += " THEN " +ConvertStringToSQL(strJobname + "：无限制；")            + Environment.NewLine;
            //Fax Restriction  prohibition  Limit to
            strSql += " WHEN  Status='3'                                               " + Environment.NewLine;
            strSql += " THEN " +ConvertStringToSQL(strJobname + "：禁用；")              + Environment.NewLine;
            //Fax Restriction
            strSql += " WHEN  Status='2'                                               " + Environment.NewLine;
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED
           //strSql += " THEN '" + strJobname +"：不多于'+LTRIM(LimitNum)+'页；'        " + Environment.NewLine;
            strSql += " THEN '" + strJobname + "：不多于' +                            " + Environment.NewLine;
            strSql += "REPLACE(CONVERT(varchar, CONVERT(money, ISNULL(LimitNum,0)), 1),'.00','') " + Environment.NewLine;
            // 2011.3.24 Update By SES Zhou Miao Ver.1.1 Update ST
            //strSql += "+'页；'                                                         " + Environment.NewLine;
            strSql += "+'面；'                                                         " + Environment.NewLine;
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED
            // 2010.12.13 Update By SES Zhou Miao Ver.1.1 Update ED
            //other
            strSql += " ELSE '' END                                                    " + Environment.NewLine;
        }
        strSql += "FROM RestrictionInformation" + Environment.NewLine;
        strSql += "WHERE FunctionId='" + FunctionId + "' AND JobId='" + JobId+"'       " + Environment.NewLine;
        strSql += ")" + tablename + " ON                                               " + Environment.NewLine;
        strSql += tablename + ".RestrictionID=A.RestrictionID                " + strjoin + Environment.NewLine;

        return strSql;
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
        //// Sort ▲▼
        //string sortString = "";
        //int index = 0;


        //cView.Columns[0].HeaderText = "";
        ////2010.12.3 Update By SES zhoumiao Ver.1.1 Update ST
        //// 2010.09.09 Update By SES JiJianXiong ST
        //// Change the columns id.
        //// cView.Columns[1].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        ////cView.Columns[2].HeaderText = UtilConst.CON_ITEM_GROUPNAME;
        //cView.Columns[2].HeaderText = UtilConst.CON_ITEM_RESTRICNAME;
        //// 2010.09.09 Update By SES JiJianXiong ED
        ////2010.12.3 Update By SES zhoumiao Ver.1.1 Update ED
        //if (cView.SortExpression.Equals(""))
        //{
        //    return;
        //}

        //if ("RestrictionName".Equals(cView.SortExpression))
        //{
        //    sortString = UtilConst.CON_ITEM_RESTRICNAME;
        //    // 2010.09.09 Update By SES JiJianXiong ST
        //    // Change the columns id.
        //    // index = 1;
        //    index = 2;
        //    // 2010.09.09 Update By SES JiJianXiong ED
        //}

        //if (SortDirection.Ascending.Equals(cView.SortDirection))
        //{
        //    sortString = UtilConst.CON_ITEM_SORT_ASC + sortString;
        //}
        //else
        //{
        //    sortString = UtilConst.CON_ITEM_SORT_DESC + sortString;
        //}

        //cView.Columns[index].HeaderText = sortString;


         ///////////////////////////////////
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
        /////////////////////////////////
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
                    //strResId = gRow.Cells[11].Text;
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
                        string sql = "DELETE FROM [RestrictionInfo] WHERE ID in ({0})";
                        sql = string.Format(sql, strDeleteResId);

                        using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // 2,Delete Restriction Set Detail Information.
                        sql = "DELETE FROM [RestrictionInformation] WHERE RestrictionID in ({0})";
                        sql = string.Format(sql, strDeleteResId);

                        using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // 3,Set User's Restriction Set To "User Restriction".
                        sql = "UPDATE [UserInfo] SET RestrictionID = {0} WHERE RestrictionID in ({1})";
                        sql = string.Format(sql, UtilConst.CON_DATE_ID, strDeleteResId);

                        using (SqlCommand cmd = new SqlCommand(sql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        // 4，Set Group's Restriction Set To "User Restriction".
                        sql = "UPDATE [GroupInfo] SET RestrictionID = {0} WHERE RestrictionID in ({1})";
                        sql = string.Format(sql, UtilConst.CON_DATE_ID, strDeleteResId);

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
                SuccessMessage(UtilConst.MSG_DELETE_SUCCESS_RES);
            }
            else
            {
                // no date for delete process.
                ErrorAlert(UtilConst.MSG_DELETE_NOTHING_RES);
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
       Response.Redirect("RestrictInfoAdd.aspx");
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
           if (strId.Equals(UtilConst.CON_ID_RESTRICLIST_GENERAL) || strId.Equals(UtilConst.CON_ID_RESTRICLIST_GROUP))
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

       //SetListMainPgae(this.CustomersGridView,
       //           this.ddlNumPerPage,
       //           this.ddlIndexOfPage,
       //           this.lblTotalPage,
       //           this.btnSelectAll, "RestrictionName,Id");
       SetListMainPgae(this.CustomersGridView,
           this.ddlNumPerPage,
           this.ddlIndexOfPage,
           this.lblTotalPage,
           this.btnSelectAll, "RestrictionName,AllQuota,colorQuota,OverLimit,Id");
       this.CustomersGridView.Sort("RestrictionName", SortDirection.Ascending);
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

       if (!ContentSetCheck())
       {
           strSql = "   SELECT A.RestrictionName AS RestrictionName  " + Environment.NewLine;
           strSql += "    ,A.ID AS Id,,AllQuota,colorQuota,OverLimit                                " + Environment.NewLine;
           strSql += "    FROM  [RestrictionInfo] A                  " + Environment.NewLine;
           if (!string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim()))
           {
               strSql += "WHERE RestrictionName LIKE  " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
               //chen add 20140616 start
               strSql += "    AND  ID <> '-1'                  " + Environment.NewLine;
               //chen add 20140616 end
           }
           //chen add 20140616 start
           else
           {
               strSql += "    WHERE  ID <> '-1'                  " + Environment.NewLine;
           }
           //chen add 20140616 end
           strSql += " ORDER BY A.UpdateTime DESC                     " + Environment.NewLine;
       }
       else
       {
           //pupeng 2014-4-16 start
           strSql = "   SELECT A.RestrictionName AS RestrictionName  " + Environment.NewLine;
           strSql += "    ,A.ID AS Id ,AllQuota,colorQuota,OverLimit                               " + Environment.NewLine;
           strSql += "    FROM  [RestrictionInfo] A                  " + Environment.NewLine;
           if (!string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim()))
           {
               strSql += "WHERE RestrictionName LIKE  " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
               //chen add 20140616 start
               strSql += "    AND  ID <> '-1'                  " + Environment.NewLine;
               //chen add 20140616 end
           }
           //chen add 20140616 start
           else
           {
               strSql += "    WHERE  ID <> '-1'                  " + Environment.NewLine;
           }
           //chen add 20140616 end
           strSql += " ORDER BY A.UpdateTime DESC                     " + Environment.NewLine;
           //end


           //strSql += SetListSql();
           //if (!string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim()))
           //{
           //    strSql += "WHERE RestrictionName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");

           //}
       }

       ViewState["SearchKeyWord"] = strSql;
       return strSql;
   }
   #endregion
   public string getFunctionList(string strResId)
   {
       string flist = "";
       using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
       {
           con.Open();
           SqlTransaction tran = con.BeginTransaction();
           try
           {
               
               // 1,Delete Restriction Set Management Information
               string sql = "select * FROM [RestrictionInformation] WHERE RestrictionID in ({0}) and status={1} and jobid not in(3,4,5,7)";
               sql = string.Format(sql, strResId, UtilConst.STATUS_UNLIMITED.ToString());
               SqlDataReader sda;
               using (SqlCommand cmd = new SqlCommand(sql, con, tran))
               {
                  sda= cmd.ExecuteReader();
                  while (sda.Read())
                  {
                      if (sda["FunctionId"].ToString() == "2") //彩色
                          flist += "彩色";
                      else if (sda["FunctionId"].ToString() == "1") //黑白
                          flist += "黑白";
                      if (sda["Jobid"].ToString() == "1") //复印
                          flist += "复印，";
                      else if (sda["Jobid"].ToString() == "2" || sda["Jobid"].ToString() == "3"|| sda["Jobid"].ToString() == "5") //打印
                          flist += "打印，";
                      else if (sda["Jobid"].ToString() == "6" || sda["Jobid"].ToString() == "7" || sda["Jobid"].ToString() == "4") //扫描
                          flist += "扫描，";
                      else if (sda["Jobid"].ToString() == "8") //传真
                          flist += "传真，";
                  }
                  sda.Close();
                  flist=flist.Replace("黑白扫描，彩色扫描", "扫描");
                  flist=flist.Replace("黑白传真，彩色传真", "传真");
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
       if (flist == "")
       {
           //flist = "无限制";
           flist = "禁用";
       }
       else
       {
           flist = flist.Substring(0, flist.Length - 1);
       }
       return flist;
 
   }

   public string getPrintBW(string strResId)
   {
       string printBW = "";
       using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
       {
           con.Open();
           SqlTransaction tran = con.BeginTransaction();
           try
           {

               // 1,Delete Restriction Set Management Information
               string sql = "select PrintBW FROM [RestrictionInfo] WHERE ID = {0} ";
               sql = string.Format(sql, strResId);
               SqlDataReader sda;
               using (SqlCommand cmd = new SqlCommand(sql, con, tran))
               {
                   sda = cmd.ExecuteReader();
                   if (sda.Read())
                   {
                       if (sda["PrintBW"].ToString() == "0") //彩色
                           printBW = "无限制";
                       else
                           printBW += "黑白";
                   }
                   sda.Close();
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
       return printBW;

   }
}
