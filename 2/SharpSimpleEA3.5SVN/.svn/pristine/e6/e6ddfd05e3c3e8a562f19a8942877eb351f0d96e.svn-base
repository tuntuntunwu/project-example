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
using System.IO;
using System.Collections.Generic;
/// <summary>
/// PrintTask Information List
/// </summary>
/// <Date>2019.03.16</Date>
/// <Author>Wang ZiYang</Author>
/// <Version>0.01</Version>
public partial class PrintTask_PrintTaskList : ListMainPage
{
    public PrintTask_PrintTaskList()
    {

    }
    #region "Page_Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        // SQL
        string strSql;

        // Title
        this.Master.Title = UtilConst.CON_PAGE_PrintTask;

        if (!HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            this.Master.CheakSearchItem = false;
            this.Master.ddl_SearchList().Enabled = false;
        }
        else
        {
            this.Master.CheakSearchItem = true;
            this.Master.ddl_SearchList().Enabled = true;
        }
        // Check Access Role
        //CheckUser();

        //Set SqlDataSource
        SqlDataListSource.ConnectionString = this.DBConnectionStrings;


        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            strSql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {
            strSql = SearchSql();
        }
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;

        SqlDataListSource.SelectCommand = strSql;

        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            this.btnSelectAll, "UserName,LoginName,PrintFileName,CreateTime,Id");
        if (!IsPostBack)
        {
            if (this.CustomersGridView.Rows.Count > 0)
            {
                this.CustomersGridView.Sort("UserName", SortDirection.Ascending);
            }
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_USER, UtilConst.CON_ITEM_USERNAME));
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem(UtilConst.CON_ITEM_LOGIN, UtilConst.CON_ITEM_LOGINNAME));
        }

        btnDeleteGrp.OnClientClick = ConfirmFunction(UtilConst.MSG_PRINTTask_DELETE);

        this.Master.btn_Search().Click += new EventHandler(btn_SearchClick);
    }
    #endregion

    #region"Function:Check  options  Whether is seted of  Setting management"
    /// <summary>
    /// Function:Check  options  Whether is seted of  Setting management
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
    /// <Author>chen</Author>
    /// <Version>0.02</Version>
    public override void SortGridView(GridView cView)
    {
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
    protected void btnDeleteRes_Click(object sender, EventArgs e)
    {
        try
        {
            string strDeleteResId = "";
            string strResId = "";
            bool process = false;

            List<string> resList = new List<string>();

            foreach (GridViewRow gRow in CustomersGridView.Rows)
            {
                CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                if (ch.Checked)
                {
                    strResId = gRow.Cells[gRow.Cells.Count - 1].Text;

                    if ("".Equals(strDeleteResId))
                    {
                        strDeleteResId = strResId;
                    }
                    else
                    {
                        strDeleteResId += "," + strResId;
                    }
                    resList.Add(strResId);
                }
            }

            this.delPrintFile(resList);

            if (!string.IsNullOrEmpty(strDeleteResId))
            {
                process = true;
                using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {
                        string sql = "DELETE FROM [MFPPrintTask] WHERE MFPPrintTaskID in ({0})";

                        sql = "DELETE FROM [MFPPrintTask] WHERE MFPPrintTaskID in ({0})";
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

            if (process)
            {
                SuccessMessage(UtilConst.MSG_DELETEPRINTTASK_SUCCESS_RES);
            }
            else
            {
                ErrorAlert(UtilConst.MSG_DELETEPRINTTASK_NOTHING_RES);
            }
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }
    }
    #endregion


    #region "Occurs when a data row is bound to data in GridView."
    protected void CustomView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
    protected void btn_SearchClick(object sender, EventArgs e)
    {
        SqlDataListSource.SelectCommand = SearchSql();
        CustomersGridView.DataBind();
        this.ddlNumPerPage.SelectedIndex = 0;
        this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());

        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            this.btnSelectAll, "UserName, LoginName,PrintFileName,CreateTime,Id");
        this.CustomersGridView.Sort("LoginName", SortDirection.Ascending);
        if (CustomersGridView.Rows.Count == 0)
        {
            ErrorAlert(UtilConst.MSG_NOTHING_SEARCH);
        }

    }
    #endregion

    #region
    private string SearchSql()
    {
        string strSql = "";

        if (HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            strSql = "   SELECT isNULL(B.UserName,'未知用户') AS UserName, A.LoginName AS LoginName  " + Environment.NewLine;
            strSql += "    ,A.MFPPrintTaskID AS Id,A.PrintFileName,A.CreateTime,A.State  " + Environment.NewLine;
            strSql += "    FROM [MFPPrintTask] A   " + Environment.NewLine;
            strSql += "  LEFT JOIN [UserInfo] B on A.LoginName = B.LoginName " + Environment.NewLine;
            strSql += "  WHERE A.State = '1' " + Environment.NewLine;

        }
        else
        {
            strSql = "   SELECT B.UserName AS UserName, A.LoginName AS LoginName  " + Environment.NewLine;
            strSql += "    ,A.MFPPrintTaskID AS Id,A.PrintFileName,A.CreateTime,A.State   " + Environment.NewLine;
            strSql += "    FROM [MFPPrintTask] A, [UserInfo] B       " + Environment.NewLine;
            strSql += "  WHERE A.LoginName = B.LoginName" + Environment.NewLine;
            strSql += "  AND B.LoginName = '" + User.Identity.Name + "'";
            strSql += "  AND A.State = '1' " + Environment.NewLine;
        }


        if (!(string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim())))
        {
            if (UtilConst.CON_ITEM_USER.Equals(this.Master.ddl_SearchList().SelectedValue))
            {
                strSql += " AND B.UserName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
            }
            else
            {
                strSql += " AND A.LoginName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
            }

        }

        strSql += " ORDER BY CreateTime DESC                     " + Environment.NewLine;

        ViewState["SearchKeyWord"] = strSql;
        return strSql;
    }

    public void delPrintFile(List<string> resList)
    {

        foreach (string key in resList)
        {
            dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter adpter = new dtMFPPrintTaskTableAdapters.MFPPrintTaskTableAdapter();
            dtMFPPrintTask.MFPPrintTaskDataTable table = adpter.GetDataByID(Convert.ToInt32(key));

            if (table != null && table.Count > 0)
            {
                string pathfile = table[0].FileLocation + table[0].DiskFileName;
                bool isExist = File.Exists(pathfile);
                if (isExist)
                {
                    File.Delete(pathfile);
                }
            }
        }
    }

    #endregion

}