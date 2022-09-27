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
using DAL;
using Model;
using System.Collections.Generic;

/// <summary>
/// Add User Information
/// </summary>
/// <Date>2010.06.15</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>1.1</Version>
public partial class Settings_SectionAdd : MainPage
{

    DalSectionInfo dalsection = new DalSectionInfo();
    #region "Page_Load"
   
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title ="集团设置";
        Master.SubTitle("集团添加", "SectionSetting.aspx", true);

        //CheckUser();


        if (!IsPostBack)
        {
            int lebel = 0;
            ddlLebel.SelectedIndex = lebel;

           
            ddlParentSectuibBindData(lebel);
            

        }
     //btnCancel.OnClientClick = ConfirmFunctionCancel(UtilConst.MSG_UPDATE_CANCEL);

    }
    #endregion

    protected void ddlParentSectuibBindData(int lebel)
    {
        List<SectionInfoEntry> sectionlist = dalsection.getSectionInfo(lebel);
        ddlParentSectionName.DataSource = sectionlist;
        ddlParentSectionName.DataValueField = "SectionID";
        ddlParentSectionName.DataTextField = "SectionID";
        ddlParentSectionName.DataBind();
        ddlParentSectionName.SelectedIndex = 0;

    }
    #region "Insert User Information"
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SectionInfoEntry bean = new SectionInfoEntry();
            bean.SectionID = Guid.NewGuid().ToString("N");
            bean.SectionName = this.txtSectionName.Text.ToString();
            bean.Level = Convert.ToInt32(this.ddlLebel.SelectedItem.ToString());
            bean.ParentSectionID = this.ddlParentSectionName.DataValueField;
            this.Response.Redirect("SectionSetting.aspx", false);
            this.dalsection.Add(bean);
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }

    }



    #endregion



    #region "Check SectionName In UserInfo Table"
    protected void valSectionName_ServerValidate(Object source, ServerValidateEventArgs args)
    {
        string strSectionName = args.Value.Trim().ToString();
        bool flg = dalsection.CheckSectionNameExist(strSectionName);

        if (flg == true)
        {
            args.IsValid = false;
        }
        else
        {
            txtSectionName.Text = strSectionName;
            args.IsValid = true;
        }
    }
    #endregion

    #region onSelectedLebelChanged
    protected void onSelectedLebelChanged(object sender, EventArgs e)
    {
        int lebel = ddlLebel.SelectedIndex;
        ddlParentSectuibBindData(lebel);
    }
    #endregion
    #region onSelectedGroupChanged
    protected void onSelectedParentSectionChanged(object sender, EventArgs e)
    {

    }
    #endregion
    /// <summary>
    /// add by wangziyang
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region btnCancel_Click
    protected new void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("SectionSetting.aspx", false);
    }
    #endregion


}
