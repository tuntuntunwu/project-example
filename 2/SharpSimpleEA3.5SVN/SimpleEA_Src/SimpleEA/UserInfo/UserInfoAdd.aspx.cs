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
using dtUserInfoTableAdapters;
using dtGroupInfoTableAdapters;
using System.Data.SqlClient;
using SesMiddleware;

/// <summary>
/// Add User Information
/// </summary>
/// <Date>2010.06.15</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>1.1</Version>
public partial class UserInfo_UserInfoAdd : UserInfoMain
{
    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Update By SES Jijianxiogn 2010-09-07 ST
        // Change to the Sub Title
        // this.Master.Title = UtilConst.CON_PAGE_USERADD;
        this.Master.Title = UtilConst.CON_PAGE_USERLIST;
        Master.SubTitle(UtilConst.CON_PAGE_ADD, "UserList.aspx", true);
        // Update By SES Jijianxiogn 2010-09-07 ED

        // Check Access Role
        CheckUser();

        if (!IsPostBack)
        {
            // Get Restriction Set Information
            RestrictionInfoTableAdapter RestrictionAdapter = new RestrictionInfoTableAdapter();
            // Restriction Set DropList
            ddlRestrictionSet.DataSource = RestrictionAdapter.GetRestrictionInfoData();
            ddlRestrictionSet.DataBind();
            // 2010.06.14 Add By Ji User can select ""(blank) in Restriction Set.
            
            //chen 20140513 update 
            //ddlRestrictionSet.Items.Insert(0, "");
            //
            ddlRestrictionSet.SelectedValue = UtilConst.CON_INHERIT_GROUP;

            // Get Group Information
            // Update By JiJianxiong 2010-07-09 ST
            if (txtLoginName.Text.Equals(UtilConst.CON_USER_LONINNAME))
            {
                GroupInfoTableAdapter GroupInfoAdapter = new GroupInfoTableAdapter();
                ddlGroupName.DataSource = GroupInfoAdapter.GetGroupInfoData();
            }
            else
            {
                GroupInfoTableAdapter GroupInfoAdapter = new GroupInfoTableAdapter();
                ddlGroupName.DataSource = GroupInfoAdapter.GetGroupInfoDataWithOutID(int.Parse(UtilConst.CON_DATE_ADMIN_ID));
            }
            ddlGroupName.DataBind();

            // Update By JiJianxiong 2010-07-09 ED
            ddlGroupName.SelectedValue = UtilConst.CON_DATE_ID;

            // Set User Check
            // Exist LoginName.
            valLoginName.ErrorMessage = UtilConst.MSG_LOGIN_NAMEEXIST;
            // 2010.12.3 Add By SES Jijianxiong Update Ver.1.1 ST
            // Exist UserName.
            // valUserName.ErrorMessage = UtilConst.MSG_USER_NAMEEXIST;
            valUserName.ErrorMessage = "该用户名已被占用。";
            // 2010.12.3 Add By SES Jijianxiong Update Ver.1.1 ED

            // Must Input User Name.
            rfvUserName.ErrorMessage = UtilConst.MSG_USER_NAMEMUST;
            // Must Input Login Name.
            rfvLoginName.ErrorMessage = UtilConst.MSG_LOGIN_NAMEMUST;
            // Must Input Password.
            rfvPassword.ErrorMessage = UtilConst.MSG_PASSWORD_NAMEMUST;
            // Must Input Password Confirm.
            rfvPasswordConfirm.ErrorMessage = UtilConst.MSG_PASSWORDCONFIRM_NAMEMUST;

            // ILLEGAL
            // UserName
            revUserName.ErrorMessage = UtilConst.MSG_ILLEGAL_STRING;
            revUserName.ValidationExpression = UtilConst.CON_VAL_ILLEGAL;
            // LoginName
            revLoginName.ErrorMessage = UtilConst.MSG_ILLEGAL_STRING;
            revLoginName.ValidationExpression = UtilConst.CON_VAL_ILLEGAL;
            // Compare Equal With Confirm Password and Password
            cpvPasswordConfirm.ErrorMessage = UtilConst.MSG_PASSWORD_NOEQUAL;

            // 2011.01.10 Delete By SES Jijianxiong ST
            // IC Card Must Input Delete.
            //// Must Input IC CardId.
            //rfvCardID.ErrorMessage = UtilConst.MSG_ICCARD_MUST;
            // 2011.01.10 Delete By SES Jijianxiong ED
            // Exist IC CardId.
            valCardID.ErrorMessage = UtilConst.MSG_ICCARD_EXIST;
            //IC CardID Must be Numberic and English.
            revCardID.ErrorMessage = UtilConst.MSG_ICCARD_CODE;

            valPinCode.ErrorMessage = UtilConst.MSG_PINCODE_EXIST;
            revPinCode.ErrorMessage = UtilConst.MSG_PINCODE_CODE;


            // Must Be Input English or Numberic In password and length > 4
            revPassword.ErrorMessage = UtilConst.MSG_PASSWORD_CHECK;
            revEmail.ErrorMessage = UtilConst.MSG_EMAIL_CHECK;

        }

        // Cancel Button's Confirm Msg
        btnCancel.OnClientClick = ConfirmFunctionCancel(UtilConst.MSG_UPDATE_CANCEL);
    }
    #endregion

    #region "Insert User Information"
    /// <summary>
    /// Insert User Information
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.15</Date>
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

            //1. Get Max User Id From UserInfor Table
            UserInfoTableAdapter UserInfoAdapter = new UserInfoTableAdapter();
            Int64 UserId = Convert.ToInt64(UserInfoAdapter.GetMaxId()) + 1;

            //edit by WeiChangye 2012.05.17
            //insure max ID < 32767
            if (UserId > 32767 && UtilCommon.GetUsableID() != -1)
            {
                UserId = UtilCommon.GetUsableID();
            }
            //end

            using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    string strSql;
                    //1. Set User Group Information
                    strSql = "   INSERT INTO [UserInfo]          " + Environment.NewLine;
                    strSql += "             ([ID]                " + Environment.NewLine;
                    strSql += "             ,[UserName]          " + Environment.NewLine;
                    strSql += "             ,[LoginName]         " + Environment.NewLine;
                    strSql += "             ,[Password]          " + Environment.NewLine;
                    strSql += "             ,[ICCardID]          " + Environment.NewLine;
					//2015 04 23 add
                    strSql += "             ,[PinCode]          " + Environment.NewLine;
					//
                    strSql += "             ,[Email]             " + Environment.NewLine;
                    strSql += "             ,[GroupID]           " + Environment.NewLine;
                    // Update BY JiJianxiong 2010-07-09 ST
                    // strSql += "             ,[RestrictionID])    " + Environment.NewLine;
                    strSql += "             ,[RestrictionID]     " + Environment.NewLine;
                    strSql += "             ,[ComeFrom]     " + Environment.NewLine;
                    strSql += "             ,[CreateTime]        " + Environment.NewLine;
                    strSql += "             ,[UpdateTime])       " + Environment.NewLine;
                    // Update BY JiJianxiong 2010-07-09 ED
                    strSql += "       VALUES                     " + Environment.NewLine;
                    strSql += "             ({0}                 " + Environment.NewLine;
                    strSql += "             ,{1}                 " + Environment.NewLine;
                    strSql += "             ,{2}                 " + Environment.NewLine;
                    strSql += "             ,{3}                 " + Environment.NewLine;
                    strSql += "             ,{4}                 " + Environment.NewLine;
					//2015 04 23 add start
                    strSql += "             ,{5}                 " + Environment.NewLine;
					//end
                    strSql += "             ,{6}                 " + Environment.NewLine;
                    // Update BY JiJianxiong 2010-07-09 ST
                    // strSql += "             ,{6})                " + Environment.NewLine;
                    strSql += "             ,{7}                 " + Environment.NewLine;
                    strSql += "             ,{8}                 " + Environment.NewLine;
                    strSql += "             ,{9}                 " + Environment.NewLine;
                    strSql += "             ,getdate() , getdate())  " + Environment.NewLine;
                    // Update BY JiJianxiong 2010-07-09 ED

                    string[] paramslist = new string[10];
                    // User Id
                    paramslist[0] = ConvertIntToSQL(UserId.ToString());
                    // UserName
                    paramslist[1] = ConvertStringToSQL(txtUserName.Text);
                    // LoginName
                    paramslist[2] = ConvertStringToSQL(txtLoginName.Text);
                    // Password
                    paramslist[3] = ConvertStringToSQL(txtPassword.Text);
                    // ICCardID
                    paramslist[4] = ConvertStringToSQL(txtCardID.Text);

                    // PinCode
                    paramslist[5] = ConvertStringToSQL(txtPinCode.Text);
					
                    // Email //chen
                    paramslist[6] = ConvertStringToSQL(txtEmail.Text);
                    // GroupID
                    paramslist[7] = ConvertIntToSQL(ddlGroupName.SelectedValue);
                    // RestrictionID
                    paramslist[8] = ConvertIntToSQL(ddlRestrictionSet.SelectedValue);
                    paramslist[9] = ConvertIntToSQL(UtilConst.USER_SYS.ToString());

                    //chen 20140429 add start
                    //int restrictID = int.Parse(ddlRestrictionSet.SelectedValue);
                    //decimal remainMoney = 0;
                    //decimal remainColorMoney = 0;
                    //dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter resAdapter = new dtRestrictionInfoTableAdapters.RestrictionInfoTableAdapter();
                    //dtRestrictionInfo.RestrictionInfoDataTable dt = resAdapter.GetRestrictionInfoDataByID(restrictID);
                    //if (dt.Count != 0)
                    //{
                    //    dtRestrictionInfo.RestrictionInfoRow resRow = dt[0];
                    //    remainMoney = resRow.AllQuota;
                    //    remainColorMoney = resRow.ColorQuota;
                    //}
                    //paramslist[8] = UtilCommon.ConvertDecimalToSQL(remainMoney.ToString());
                    //paramslist[9] = UtilCommon.ConvertDecimalToSQL(remainColorMoney.ToString());
                    //chen 20140429 add end

                    strSql = string.Format(strSql, paramslist);

                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Add User's XML
					//2015 04 23 delete 
					/*
                    if (!txtLoginName.Text.Equals(UtilConst.CON_USER_LONINNAME))
                    {
                        string strICCardID = txtCardID.Text.Trim();
                        if (!string.IsNullOrEmpty(strICCardID))
                        {
                            int returnVal = ICCardData.AddICCardInfo(strICCardID, txtLoginName.Text.Trim(), txtPassword.Text.Trim(), this.ServerPath);
                            if (returnVal != 0)
                            {
                                throw new Exception(UtilConst.MSG_MIDDLEWARE_ERROR);
                            }
                        }
                    }
					*/
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
            // Changes are applied and then back to User Managemnet Screen.
            this.Response.Redirect("UserList.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }

    }

    #endregion

    #region"Check LoginName In UserInfo Table."
    /// <summary>
    /// Check LoginName In UserInfo Table.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <Date>2010.06.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void valLoginName_ServerValidate(Object source, ServerValidateEventArgs args)
    {
        string strLoginName = args.Value.Trim().ToString();

        UserInfoTableAdapter UserInfoAdapter = new UserInfoTableAdapter();
        object count = UserInfoAdapter.CheckUserInfoExistName(strLoginName);
        int intcount = 0;
        try
        {
            intcount = Convert.ToInt32(count);
        }
        catch (Exception)
        {

            intcount = 0;
        }

        if (intcount > 0)
        {
            args.IsValid = false;
        }
        else
        {
            txtLoginName.Text = strLoginName;
            args.IsValid = true;
        }
    }

    #endregion

    #region "Check IC CardID In UserInfo Table."
    /// <summary>
    /// Check IC CardID In UserInfo Table.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <Date>2010.06.23</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void valCardID_ServerValidate(Object source, ServerValidateEventArgs args)
    {
        string strICCardID = args.Value.ToString();

        // 2011.01.10 Add By SES Jijianxiong ST
        strICCardID = strICCardID.Trim();
        if (string.IsNullOrEmpty(strICCardID))
        {
            args.IsValid = true;
            return;
        }
        // 2011.01.10 Add By SES Jijianxiong ED

        UserInfoTableAdapter UserInfoAdapter = new UserInfoTableAdapter();
        object obj = UserInfoAdapter.CheckUserInfoExistICCardID(strICCardID);
        if (obj != null && (int)obj > 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
    #endregion

    #region "Check Pincode In UserInfo Table."
    /// <summary>
    /// Check Pincode In UserInfo Table.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <Date>2015.06.26</Date>
    /// <Author>SES chen</Author>
    /// <Version>0.01</Version>
    protected void valPincode_ServerValidate(Object source, ServerValidateEventArgs args)
    {
        string strPincode = args.Value.ToString();

        // 2011.01.10 Add By SES Jijianxiong ST
        strPincode = strPincode.Trim();
        if (string.IsNullOrEmpty(strPincode))
        {
            args.IsValid = true;
            return;
        }
        // 2011.01.10 Add By SES Jijianxiong ED

        UserInfoTableAdapter UserInfoAdapter = new UserInfoTableAdapter();
        object obj = UserInfoAdapter.CheckUserInfoExistPinCode(strPincode);
        if (obj != null && (int)obj > 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
    #endregion
    #region "Check UserName In UserInfo Table"
    /// <summary>
    /// Check UserName In UserInfo Table.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <Date>2010.12.02</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.1</Version>
    protected void valUserName_ServerValidate(Object source, ServerValidateEventArgs args)
    {
        string strUserName = args.Value.Trim().ToString();

        UserInfoTableAdapter UserInfoAdapter = new UserInfoTableAdapter();
        if (Convert.ToInt32(UserInfoAdapter.CheckUserInfoExistUserName(strUserName)) > 0)
        {
            args.IsValid = false;
        }
        else
        {
            txtUserName.Text = strUserName;
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
    /// <Date>2014。04.24</Date>
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

    #region onSelectedGroupChanged
    protected void onSelectedGroupChanged(object sender, EventArgs e)
    {
        //DropDownList list = (DropDownList)sender;
        //dtGroupInfoTableAdapters.GroupInfoTableAdapter groupAdapter = new dtGroupInfoTableAdapters.GroupInfoTableAdapter();
        //dtGroupInfo.GroupInfoDataTable groupDT = groupAdapter.GetDataByGroupName(list.SelectedItem.Text);
        //if (groupDT.Count == 0)
        //{
        //    return;
        //}
        //int idx = ddlRestrictionSet.SelectedIndex;
        //for (int i = 0; i < ddlRestrictionSet.Items.Count; i++)
        //{
        //    if (ddlRestrictionSet.Items[i].Value == groupDT[0].RestrictionID)
        //    {
        //        idx = i;
        //        break;
        //    }
        //}
        //ddlRestrictionSet.SelectedIndex = idx;
        ddlRestrictionSet.SelectedValue = UtilConst.CON_INHERIT_GROUP;
    }
    #endregion

    #region btnRefreshRestrictionSet_click
    protected void btnRefreshRestrictionSet_click(object sender, EventArgs e)
    {
        dtRestrictionInfo.RestrictionInfoDataTable dt = new dtRestrictionInfo.RestrictionInfoDataTable();
        DataView dv = new DataView();
       
        RestrictionInfoTableAdapter RestrictionAdapter = new RestrictionInfoTableAdapter();
        dt = RestrictionAdapter.GetRestrictionInfoData();
        dv = dt.DefaultView;
        dv.Sort = "ID ";
        ddlRestrictionSet.DataSource = dv;
        ddlRestrictionSet.DataBind();
        
        ddlRestrictionSet.SelectedValue = dv.Table.Rows[dv.Table.Rows.Count-1]["ID"].ToString();
    }
    #endregion
}
