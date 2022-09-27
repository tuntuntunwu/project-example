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


public partial class Password_Password : MainPage
{
    #region Page_Load
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check Access Role
        CheckUser();
        if (!IsPostBack)
        {
            loadOnce();
        }

        btnUpdate.OnClientClick = ConfirmFunctionUpd(UtilConst.MSG_UPDATE_UPDATE, btnUpdate.ValidationGroup);
        btnUpdate.Click+=new EventHandler(btnUpdate_Click);

    }

    private void loadOnce()
    {
        rfvOldPassword.ErrorMessage = UtilConst.MSG_ADMIN_OLDPASSWORD_MUST;
        revOldPassword.ErrorMessage = UtilConst.MSG_PASSWORD_CHECK;
        valOldPassword.ErrorMessage = UtilConst.MSG_ADMIN_OLDPASSWORD_ERR;
        // Must Input Password.
        rfvPassword.ErrorMessage = UtilConst.MSG_ADMIN_PASSWORD_MUST;
        // Must Input Password Confirm.
        rfvPasswordConfirm.ErrorMessage = UtilConst.MSG_ADMIN_PASSWORDCONFIRM_MUST;
        // Compare Equal With Confirm Password and Password
        cpvPasswordConfirm.ErrorMessage = UtilConst.MSG_ADMIN_PASSWORD_NOEQUAL;
        // Must Be Input English or Numberic In password and length > 4
        revPassword.ErrorMessage = UtilConst.MSG_PASSWORD_CHECK;

    }
    #endregion

    #region btnUpdate_Click
    /// <summary>
    /// btnUpdate_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string loginame = HttpContext.Current.User.Identity.Name;

            if (!Page.IsValid)
            {
                return;
            }
            using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    string strSql;
                    //1. Set User Group Information
                    strSql = "   UPDATE [UserInfo]          " + Environment.NewLine;
                    strSql += "  SET                        " + Environment.NewLine;
                    strSql += "      [Password] = {0}      " + Environment.NewLine;
                    strSql += "      ,[UpdateTime] = getdate() " + Environment.NewLine;
                    strSql += "WHERE [LoginName] = '{1}'   " + Environment.NewLine;

                    // Password
                    string newPassword = ConvertStringToSQL(txtPassword.Text);
                    //strSql = string.Format(strSql, newPassword, UtilConst.CON_DATE_ID);
                    strSql = string.Format(strSql, newPassword, loginame);

                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    tran.Commit();
                    this.Alert(UtilConst.MSG_PERIOD_SAVED);
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
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }

    }

    #endregion

    #region valOldPassword_ServerValidate
    /// <summary>
    /// valOldPassword_ServerValidate
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void valOldPassword_ServerValidate(Object source, ServerValidateEventArgs args)
    {
        string stroldPassword = args.Value.Trim().ToString();

        //chen add;
        string loginame = HttpContext.Current.User.Identity.Name;

        int iCount = 0;

        // Insert code that implements a site-specific custom 
        // authentication method here.
        dtUserInfoTableAdapters.UserInfoTableAdapter userInfo = new dtUserInfoTableAdapters.UserInfoTableAdapter();
        iCount = (int)userInfo.Authentication(loginame, stroldPassword);
        if (iCount != 1)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }


    }
    #endregion

    #region ConfirmFunctionUpd
    /// <summary>
    /// ConfirmFunctionUpd
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="strValidationGroup"></param>
    /// <returns></returns>
    private string ConfirmFunctionUpd(string msg, string strValidationGroup)
    {
        string strMsg;
        strMsg = "ScriptConfirmSet" + "('{0}')";
        strMsg = string.Format(strMsg, msg);

        ScriptConfirm("ScriptConfirmSet", strValidationGroup);
        return strMsg;
    }

    #endregion

    #region "Show Message In IE."
    /// <summary>
    /// Show Message In IE.
    /// </summary>
    /// <param name="strMessage"></param>
    /// <Date>2011.03.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.1</Version>
    public void Alert(string strMessage)
    {
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertMessage", "<script>alert('" + strMessage + "');</script>");
       
        return;

    }
        #endregion
}
