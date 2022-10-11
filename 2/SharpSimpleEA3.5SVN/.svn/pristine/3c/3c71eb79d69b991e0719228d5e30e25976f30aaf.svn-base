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
using dtRestrictionInfoTableAdapters;
using dtGroupInfoTableAdapters;
using System.Data.SqlClient;
/// <summary>
/// Edit Group screen
/// </summary>
/// <Date>2010.06.09</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class GroupInfoEdit : GrpInfoMain
{

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.14</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.02</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Update By SES Jijianxiogn 2010-08-30 ST
        // Change to the Sub Title
        // this.Master.Title = UtilConst.CON_PAGE_GROUPEDIT;
        this.Master.Title = UtilConst.CON_PAGE_GROUPLIST;
        Master.SubTitle(UtilConst.CON_PAGE_EDIT, "GroupList.aspx", true);
        // Update By SES Jijianxiogn 2010-08-30 ED


        // Check Access Role
        CheckUser();

        // Get GroupId from  Page Parms's.
        hidGroupId.Value = Page.Request.Params["GroupId"].ToString();
        //pupeng 2014 05 30
        if (hidGroupId.Value == "0")
        {
            txtGroupName.Enabled = false;
        }
        // GroupId
        int GroupId = int.Parse(hidGroupId.Value);
        
        if (!IsPostBack)
        {
            RestrictionInfoTableAdapter RestrictionAdapter = new RestrictionInfoTableAdapter();

            // Restriction Set DropList
            //chen update 20140513 st
            //lstRestriction.DataSource = RestrictionAdapter.GetRestrictionInfoData();
            lstRestriction.DataSource = RestrictionAdapter.GetRestrictionInfoData2();
            //chen update 20140513 ed
            this.lstRestriction.DataBind();
            // 2010.06.14 Add By Ji User can select ""(blank) in Restriction Set.
            //chen update 20140513 st
            //lstRestriction.Items.Insert(0, "");
            //chen update 20140513 ed

            // Set User Group Information
            GroupInfoTableAdapter GroupInfoAdapter = new GroupInfoTableAdapter();
            this.txtGroupName.Text = GroupInfoAdapter.GetGroupInfoDataById(GroupId)[0].GroupName;
            this.lstRestriction.SelectedValue = GroupInfoAdapter.GetGroupInfoDataById(GroupId)[0].RestrictionID.ToString();

            BelongUserTableAdapter BelongUserAdapter = new BelongUserTableAdapter();
            // Set Not Belong List 
            this.lstNoBelong.DataSource = BelongUserAdapter.GetNotBelongGroupInfoDataById(GroupId);
            this.lstNoBelong.DataBind();

            // Set Belong List 
            this.lstBelong.DataSource = BelongUserAdapter.GetBelongGroupInfoDataById(GroupId);
            this.lstBelong.DataBind();

            //Error Message
            // Exist Check
            valGroupName.ErrorMessage = UtilConst.MSG_GROUP_NAMEEXIST;
            // Must Check
            rfvGroupName.ErrorMessage = UtilConst.MSG_GROUP_NAMEMUST;
            // ILLEGAL
            revGroupName.ErrorMessage = UtilConst.MSG_ILLEGAL_STRING;
            // ValidationExpression
            revGroupName.ValidationExpression = UtilConst.CON_VAL_ILLEGAL;
        }
        // Update Button's Confirm Msg
        btnUpdate.OnClientClick = ConfirmFunctionUpd(UtilConst.MSG_UPDATE_UPDATE,btnUpdate.ValidationGroup);
        // Cancel Button's Confirm Msg
        btnCancel.OnClientClick = ConfirmFunctionCancel(UtilConst.MSG_UPDATE_CANCEL);
    }
    #endregion

    #region "Update Group Information"
    /// <summary>
    /// Update Group Information
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
           
            if (!Page.IsValid)
            {
                return;
            }
            // GroupId
            int GroupId = int.Parse(hidGroupId.Value);
            using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    string strSql;
                    //1. Set User Group Information
                    strSql = "   UPDATE GroupInfo         " + Environment.NewLine;
                    strSql += "  SET                      " + Environment.NewLine;
                    strSql += "      GroupName ={0},    " + Environment.NewLine;
                    strSql += "      RestrictionID ={1}   " + Environment.NewLine;
                    // Add BY JiJianxiong 2010-07-09 ST
                    strSql += "      ,UpdateTime = getdate() " + Environment.NewLine;
                    // Add BY JiJianxiong 2010-07-09 ED
                    strSql += "WHERE ID = {2}   " + Environment.NewLine;

                    strSql = string.Format(strSql, ConvertStringToSQL(txtGroupName.Text), ConvertIntToSQL(lstRestriction.SelectedValue), GroupId);

                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    //2. Update User's Belong Group.

                    //2.1 Set All User in this Group To "Not Belong" Group.
                    strSql = "   UPDATE UserInfo       " + Environment.NewLine;
                    strSql += "  SET                   " + Environment.NewLine;
                    strSql += "      GroupID = {0}     " + Environment.NewLine;
                    /// TODO
                    ////// Add BY JiJianxiong 2010-07-09 ST
                    ////strSql += "     ,[UpdateTime] = getdate() " + Environment.NewLine;
                    ////// Add BY JiJianxiong 2010-07-09 ED
                    strSql += "WHERE GroupID = {1}     " + Environment.NewLine;

                    strSql = string.Format(strSql, UtilConst.CON_DATE_ID, GroupId);
                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    //2.2 Set GroupID in User List( from Group Belong List)'s User
                    // Get User List In Group Belong List.
                    ArrayList userlist;
                    userlist = new ArrayList();

                    foreach (ListItem item in lstBelong.Items)
                    {
                        userlist.Add(item.Value);
                    }

                    if (userlist.Count > 0)
                    {

                        // List For LoginName
                        string userNameSql = "";
                        foreach (string username in userlist)
                        {
                            if ("".Equals(userNameSql))
                            {
                                userNameSql =  ConvertStringToSQL(username);
                            }
                            else
                            {
                                userNameSql += "," + ConvertStringToSQL(username);
                            }
                        }
                        strSql = "   UPDATE UserInfo         " + Environment.NewLine;
                        strSql += "  SET                     " + Environment.NewLine;
                        strSql += "      GroupID = {0}       " + Environment.NewLine;
                        /// TODO
                        ////// Add BY JiJianxiong 2010-07-09 ST
                        ////strSql += "     ,UpdateTime = getdate() " + Environment.NewLine;
                        ////// Add BY JiJianxiong 2010-07-09 ED
                        strSql += "WHERE LoginName in ({1})  " + Environment.NewLine;

                        strSql = string.Format(strSql, GroupId, userNameSql);
                        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
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
            // Changes are applied and then back to Group Managemnet Screen.
            this.Response.Redirect("GroupList.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }

    }

    #endregion

    #region "Move Items form Not Belong User List To Belong User List."
    /// <summary>
    /// Move Items form Not Belong User List To Belong User List.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnToBelong_Click(object sender, EventArgs e)
    {
        // Move Item from lstNoBelong to lstBelong.
        MoveList(lstNoBelong, lstBelong);
    }
    #endregion

    #region "Move Items form Belong User List To Not Belong User List."
    /// <summary>
    /// Move Items form Belong User List To Not Belong User List.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnToNoBelong_Click(object sender, EventArgs e)
    {
        // Move Item from lstBelong to lstNoBelong.
        MoveList(lstBelong, lstNoBelong);
    }
    #endregion

    #region"Check GroupName In GroupInfo Table."
    /// <summary>
    /// Check GroupName In GroupInfo Table.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <Date>2010.06.09</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void valGroupName_ServerValidate(Object source, ServerValidateEventArgs args)
    {
        string strGroupName = args.Value.ToString();
        // 2010.12.22 Add By SES Jijianxiong Bug to check the Same Group name ST
        strGroupName = strGroupName.Trim();
        // 2010.12.22 Add By SES Jijianxiong Bug to check the Same Group name ED

        // GroupId
        int GroupId = int.Parse(hidGroupId.Value);

        GroupInfoTableAdapter GroupInfoAdapter = new GroupInfoTableAdapter();
        if (GroupInfoAdapter.CheckGroupInfoExistNameBy(strGroupName, GroupId) > 0)
        {
            args.IsValid = false;
        }
        else
        {
            // 2010.12.22 Add By SES Jijianxiong Bug to check the Same Group name ST
            txtGroupName.Text = strGroupName;
            // 2010.12.22 Add By SES Jijianxiong Bug to check the Same Group name ED
            args.IsValid = true;
        }
    }

    #endregion

    #region "btnItemCount_Click"
    /// <summary>
    /// btnSetRestrictionName_click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2014¡£04.24</Date>
    /// <Author>MJ tan</Author>
    /// <Version>0.01</Version>
    protected void btnSetRestrictionName_click(object sender, EventArgs e)
    {
        string strScript = "<script language='javascript' type='text/javascript'>";
        strScript = strScript + "window.open('../RestrictionInfo/popRestrictInfoAdd.aspx', null, null)";
        strScript = strScript + "</script>";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "popWindowJob", strScript);
    }
    #endregion

    #region btnRefreshRestrictionSet_click
    protected void btnRefreshRestrictionSet_click(object sender, EventArgs e)
    {
        //RestrictionInfoTableAdapter RestrictionAdapter = new RestrictionInfoTableAdapter();
        //lstRestriction.DataSource = RestrictionAdapter.GetRestrictionInfoData();
        //lstRestriction.DataBind();
        //lstRestriction.Items.Insert(0, "");
        dtRestrictionInfo.RestrictionInfoDataTable dt = new dtRestrictionInfo.RestrictionInfoDataTable();
        DataView dv = new DataView();

        RestrictionInfoTableAdapter RestrictionAdapter = new RestrictionInfoTableAdapter();
        dt = RestrictionAdapter.GetRestrictionInfoData2();
        dv = dt.DefaultView;
        dv.Sort = "ID ";
        lstRestriction.DataSource = dv;
        lstRestriction.DataBind();

        lstRestriction.SelectedValue = dv.Table.Rows[dv.Table.Rows.Count - 1]["ID"].ToString();

    }
    #endregion


}
