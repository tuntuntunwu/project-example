using System;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using dtPriceMasterTableAdapters;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using dtSettingManagementTableAdapters;
using dtRestrictionInfoTableAdapters;
using dtGroupInfoTableAdapters;
using System.Data;
using BLL;
using DAL;
using Model;
using common;
using System.Text;


public partial class Settings_SectionSetting : MainPage
{
    protected void Page_Load(object sender, EventArgs e) 
    {
        this.Master.Title = UtilConst.CON_PAGE_SECTION_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_SECTION_SET, "SectionSetting.aspx", false);
        this.Master.JobReportTitle();

        SqlDataListSource.ConnectionString = this.DBConnectionStrings;
       
        string strSql = null;
        //strSql = "   SELECT A.SectionName AS SectionName       " + Environment.NewLine +
        //        "         ,A.SectionID AS ID           " + Environment.NewLine +
        //        "         ,A.ParentSectionID AS ParentSectionID           " + Environment.NewLine +
        //        "         ,ISNULL( (SELECT U.SectionName    " + Environment.NewLine +
        //        "                     FROM SectionInfo U       " + Environment.NewLine +
        //        "                    WHERE U.SectionID = A.ParentSectionID   " + Environment.NewLine +
        //        "                   ), ' ')  AS ParentSectionName  " + Environment.NewLine +
        //        "         ,A.Lebel AS Lebel           " + Environment.NewLine +
        //        "    FROM  SectionInfo A                      " + Environment.NewLine +
        //        " ORDER BY A.Lebel ASC                  " + Environment.NewLine;

        StringBuilder  sql = new StringBuilder();
        sql.Append("SELECT A.SectionName AS SectionName       ");
        sql.Append(",A.SectionID AS ID          " );
        sql.Append(",A.ParentSectionID AS ParentSectionID          " );
        sql.Append(",ISNULL( (SELECT U.SectionName    ");
        sql.Append("                     FROM [SimpleEA].[dbo].[SectionInfo] U      " );
        sql.Append("                    WHERE U.SectionID = A.ParentSectionID  " );
        sql.Append("                   ), ' ')  AS ParentSectionName " );
        sql.Append("         ,A.Lebel AS Lebel          " );
        sql.Append("    FROM  [SimpleEA].[dbo].[SectionInfo] A                     " );
        sql.Append(" ORDER BY A.Lebel ASC ");
        strSql = sql.ToString();
        strSql = string.Format(strSql, UtilConst.CON_DATE_ADMIN_ID);
          
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;

        SqlDataListSource.SelectCommand = strSql;
        
        if (!IsPostBack)
        {
            //CustomersGridView.Columns[8].Visible = false;

        }
    }
    



    #region "Delete Group List"
    protected void btnDeleteGrp_Click(object sender, EventArgs e)
    {
        try
        {
            string strDeleteGroupId = "";
            string strGroupId = "";
            bool process = false;


            foreach (GridViewRow gRow in CustomersGridView.Rows)
            {
                System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                if (ch.Checked)
                {
                    strGroupId = gRow.Cells[8].Text;
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

            if (!string.IsNullOrEmpty(strDeleteGroupId))
            {
                process = true;
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
   protected void btnAddNewGrp_Click(object sender, EventArgs e)
   {
        Response.Redirect("SectionAdd.aspx");
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
           System.Web.UI.WebControls.CheckBox cbox = (System.Web.UI.WebControls.CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
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

   
}
   