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

/// <summary>
/// SimpleEAMasterPage
/// </summary>
/// <Date>2010.06.15</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class Masterpage_SimpleEAMasterPage : System.Web.UI.MasterPage
{
    protected int IsAdmin = 0;

    #region"returns Icons"
    //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ST
    //Search Icons are Display
    public Boolean CheakSearchItem
    {
        set
        {
            // 2011.01.05 Update By SES zhoumiao Ver.1.1 Update ST
            //SearchTable.Visible = value;
            Search.Visible = value;
            txtUserName.Visible = value;
            btnSearch.Visible = value;
            ddlSearchList.Visible = value;
            // 2011.01.05 Update By SES zhoumiao Ver.1.1 Update ED
                        
        }
    }

    //return DropDownList 
    public DropDownList ddl_SearchList()
    {
        return ddlSearchList;
    }
    //return TextBox
    public TextBox txt_UserName()
    {
        return txtUserName;
    }
    //return Button
    public Button btn_Search()
    {
        return btnSearch;
    }
    //2010.11.30 Add By SES zhoumiao Ver.1.1 Update ED
    #endregion

    #region "Title"
    /// <summary>
    /// Title
    /// </summary>
    /// <Date>2010.06.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public string Title
    {
        set {
            this.Page.Header.Title = "::Simple EA Application :: " + value;
            this.lblTitle.InnerText= value;
        }
    }
    #endregion

    #region "SubTitle"
    /// <summary>
    /// Title
    /// </summary>
    /// <Date>2010.06.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void SubTitle( string subTitle , string mainUrl ,  bool isMandatoryNeed)
    {
        // Edit Screen with  Mandatory Need.
        if (isMandatoryNeed)
        {
            tblmust.Visible = true;
        }

        // Sub Title Visible = true.
        imgSubTitle.Visible = true;
        lblSubTitle.Visible = true;

        lblSubTitle.Text = subTitle;
        this.lblTitle.InnerHtml = "<a href='" + mainUrl + "' class='Black_FONT_a'>" + this.lblTitle.InnerText + "</a>";

    }
    #endregion

    #region "ViceSubTitle"
    /// <summary>
    /// Title
    /// </summary>
    /// <Date>2010.06.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void ViceSubTitle(string subTitle, string mainUrl, bool isMandatoryNeed)
    {
        // Sub Title Visible = true.
        imgViceSubTitle.Visible = true;
        lblViceSubTitle.Visible = true;

        lblViceSubTitle.Text = subTitle;
        //this.lblSubTitle.ResolveUrl("<a href='" + mainUrl + "' class='Black_FONT_a'>" + this.lblTitle.InnerText + "</a>");

    }
    #endregion

    #region "JobReportTitle"
    /// <summary>
    /// JobReportTitle
    /// </summary>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>1.1</Version>
    public void JobReportTitle()
    {
        this.lblSubTitle.Visible = false;
        this.cphreporttitle.Visible = true;
    }
    #endregion

    #region "LoginStatusEA_OnLoggedOut"
    /// <summary>
    /// LoginStatusEA_OnLoggedOut
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void LoginStatusEA_OnLoggedOut(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();

        Page.Response.Redirect("~/Login/Login.aspx", true);
    }
    #endregion

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
        
        // Add By SES JiJianXiong 2010.09.09 ST
        if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
        {
            Page.Response.Redirect("~/Login/Login.aspx", true);
            return;
        }
        // Add By SES JiJianXiong 2010.09.09 ED

        if (HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            alinkGrpList.Disabled = false;
            alinkResList.Disabled = false;
            alinkSet.Disabled = false;
            alinkUserList.Disabled = false;
            // 2010.12.17 Add By SES Jijianxiong Ver.1.1 Update ST
            a1inkLog.Disabled = false;
            alinkMFPRes.Disabled = false;
            // 2010.12.17 Add By SES Jijianxiong Ver.1.1 Update ED

            //pupeng 2014 05-30
            alinkPrice.Disabled = false;
           

            // 2011.03.28 Add By SES Jijjianxiong ST
            IsAdmin = 0;
            // 2011.03.28 Add By SES Jijjianxiong ED

        }
        else if (HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_SECUADMIN))
        {
            alinkGrpList.Disabled = false;
            alinkResList.Disabled = false;
            alinkSet.Disabled = false;
            alinkUserList.Disabled = false;
            // 2010.12.17 Add By SES Jijianxiong Ver.1.1 Update ST
            a1inkLog.Disabled = false;
            alinkMFPRes.Disabled = false;
            // 2010.12.17 Add By SES Jijianxiong Ver.1.1 Update ED

            //pupeng 2014 05-30
            alinkPrice.Disabled = false;


            // 2011.03.28 Add By SES Jijjianxiong ST
            IsAdmin = -1;
            // 2011.03.28 Add By SES Jijjianxiong ED

        }
        else
        {
            //pupeng 2014 05-30
            alinkPrice.Disabled = true;
            alinkPrice.HRef = "";
            // end 
            //
            alinkGrpList.Disabled = true;
            alinkResList.Disabled = true;
            alinkSet.Disabled = true;

            // 2012.05.29 add by Weichangye
            alinkMFPRes.Disabled = true;
            alinkMFPRes.HRef = "";
            //end 2012.05.29 

            // 2011.03.28 Add By SES Jijjianxiong ST
            IsAdmin = 1;
            // 2011.03.28 Add By SES Jijjianxiong ED

            // 2010.12.17 Add By SES Jijianxiong Ver.1.1 Update ST
            a1inkLog.Disabled = true;
            a1inkLog.HRef = "";
            // 2010.12.17 Add By SES Jijianxiong Ver.1.1 Update ED

            // Add By SES JiJianXiong 2010.09.09 ST
            alinkGrpList.HRef = "";
            alinkResList.HRef = "";
            alinkSet.HRef = "";
       
            
            // Add By SES JiJianXiong 2010.09.09 ED

            // Update By JJX 2010-07-28 ST
            // User can edit it's self password.
            // alinkUserList.Disabled = true;
            // Get User's UserID.
            dtUserInfoTableAdapters.UserInfoTableAdapter adapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
            dtUserInfo.UserInfoDataTable dt = adapter.GetDataByLoginName(HttpContext.Current.User.Identity.Name);
            alinkUserList.Disabled = false;
            alinkUserList.HRef = "~/UserInfo/UserInfoEdit.aspx?UserId=" + ((dtUserInfo.UserInfoRow) dt.Rows[0]).ID.ToString();
            // Update By JJX 2010-07-28 ED
            // 2010.11.22 Update By SES Jijianxiong Ver.1.1 Update ST
            alinkJobReport.HRef = "~/Report/UserJobReport.aspx";
            // 2010.11.22 Update By SES Jijianxiong Ver.1.1 Update ED

        }
        alinkJobReport.Disabled = false;
        a1inkAvailable.Disabled = false;

        // Add By SES.JiJianXiong 2010.08.25 ST
        TitleStyleSet();

        // Add By SES.JiJianXiong 2010.08.25 ED
        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
        //TextBox Enter Event to touch off Buttom Click Event
        this.txtUserName.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.txtUserName.ClientID + ".focus();document.all." + this.btnSearch.ClientID + ".click();}");

        //2010.12.3 Add By SES zhoumiao Ver.1.1 Update ST
    }
    #endregion

    #region "Show Message In IE."
    /// <summary>
    /// Show Message In IE.
    /// </summary>
    /// <param name="strMessage"></param>
    /// <Date>2010.06.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public void Alert(string strMessage)
    {
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertMessage", "<script>alert('" + strMessage + "')</script>");
        return;

    }
    #endregion

    #region "TitleStyleSet"
    /// <summary>
    /// TitleStyleSet
    /// </summary>
    /// <Date>2010.08.25</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private void TitleStyleSet()
    {
        // for the common title set it's display item.
        // 1.Get Now Type.
        string[] segments = HttpContext.Current.Request.Url.Segments;
        // Page Type
        string type = string.Empty;
        // Page
        string page = string.Empty;

        if (segments.Length > 1)
        {
            // Page Type
            type = segments[segments.Length - 2].ToLower();
            // Page
            page = segments[segments.Length - 1].ToLower();
        }

        if (string.IsNullOrEmpty(type))
        {
            return;
        }

        //// 2. Remove the Css and add the normal Css.
        //titleUser.Attributes.Remove("class");
        //titleUser.Attributes.Add("class", "MenuFont_1");
        //titleGroup.Attributes.CssStyle.Clear();
        //titleGroup.Attributes.Add("class", "MenuFont");
        //titleRes.Attributes.CssStyle.Clear();
        //titleRes.Attributes.Add("class", "MenuFont");
        //titleReport.Attributes.CssStyle.Clear();
        //titleReport.Attributes.Add("class", "MenuFont");
        //titleSet.Attributes.CssStyle.Clear();
        //titleSet.Attributes.Add("class", "MenuFont");
        //titleAvail.Attributes.CssStyle.Clear();
        //titleAvail.Attributes.Add("class", "MenuFont");

        // 3.Set the Css
        if (type.IndexOf("userinfo") > -1)
        {
            // User Information
            titleUser.Attributes.Remove("class");
            titleUser.Attributes.Add("class", "Select_menu_bg");
            // Icon
            div_icon_type.Attributes.Remove("class");

            if (page.Equals("UserList.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_User_IMG");
            }
            else if (page.Equals("UserInfoAdd.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_User_Add_IMG");
            }
            else if (page.Equals("UserInfoEdit.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_User_Edit_IMG");
            }
            else if (page.Equals("MFPList.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_MFP_Management_Limit_IMG");
            }

            // Remove the Css and add the normal Css.
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleGroup.Attributes.CssStyle.Clear();
            titleGroup.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleGroup.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleRes.Attributes.CssStyle.Clear();
            titleRes.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleRes.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleReport.Attributes.CssStyle.Clear();
            titleReport.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleReport.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleSet.Attributes.CssStyle.Clear();
            titleSet.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleSet.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleAvail.Attributes.CssStyle.Clear();
            titleAvail.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleAvail.Attributes.Add("class", "MenuFont_right");

            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
            // Log Menu Added
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleLog.Attributes.CssStyle.Clear();
            titleLog.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleLog.Attributes.Add("class", "MenuFont_right");
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED

        }
        else if (type.IndexOf("groupinfo") > -1)
        {
            // Group Information
            titleGroup.Attributes.Remove("class");
            titleGroup.Attributes.Add("class", "Select_menu_bg");
            // Icon
            div_icon_type.Attributes.Remove("class");
            if (page.Equals("GroupList.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_Group_IMG");
            }
            else if (page.Equals("GroupInfoAdd.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_Group_Add_IMG");
            }
            else if (page.Equals("GroupInfoEdit.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_Group_Edit_IMG");
            }
            // Remove the Css and add the normal Css.
            titleUser.Attributes.Remove("class");
            titleUser.Attributes.Add("class", "MenuFont_1");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleRes.Attributes.CssStyle.Clear();
            titleRes.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleRes.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleReport.Attributes.CssStyle.Clear();
            titleReport.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleReport.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleSet.Attributes.CssStyle.Clear();
            titleSet.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleSet.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleAvail.Attributes.CssStyle.Clear();
            titleAvail.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleAvail.Attributes.Add("class", "MenuFont_right");
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
            // Log Menu Added

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleLog.Attributes.CssStyle.Clear();
            titleLog.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleLog.Attributes.Add("class", "MenuFont_right");
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED
        }
        else if (type.IndexOf("printtask") > -1)
        {
            titlePrintTask.Attributes.Remove("class");
            titlePrintTask.Attributes.Add("class", "Select_menu_bg");

            // Icon
            div_icon_type.Attributes.Remove("class");
            if (page.Equals("GroupList.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_Group_IMG");
            }
            else if (page.Equals("GroupInfoAdd.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_Group_Add_IMG");
            }
            else if (page.Equals("GroupInfoEdit.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_Group_Edit_IMG");
            }
            // Remove the Css and add the normal Css.
            titleUser.Attributes.Remove("class");
            titleUser.Attributes.Add("class", "MenuFont_1");

            titleUser.Attributes.Remove("class");
            titleUser.Attributes.Add("class", "MenuFont_1");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleRes.Attributes.CssStyle.Clear();
            titleRes.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleRes.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleReport.Attributes.CssStyle.Clear();
            titleReport.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleReport.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleSet.Attributes.CssStyle.Clear();
            titleSet.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleSet.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleAvail.Attributes.CssStyle.Clear();
            titleAvail.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleAvail.Attributes.Add("class", "MenuFont_right");
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
            // Log Menu Added

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleLog.Attributes.CssStyle.Clear();
            titleLog.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleLog.Attributes.Add("class", "MenuFont_right");
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED
        }
        else if (type.IndexOf("restrictioninfo") > -1)
        {
            // Restriction Information
            titleRes.Attributes.Remove("class");
            titleRes.Attributes.Add("class", "Select_menu_bg");
            // Icon
            div_icon_type.Attributes.Remove("class");
            if (page.Equals("RestrictList.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_Res_IMG");
            }
            else if (page.Equals("RestrictInfoAdd.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_Res_Add_IMG");
            }
            else if (page.Equals("RestrictInfoEdit.aspx".ToLower()))
            {
                div_icon_type.Attributes.Add("class", "Icon_Res_Edit_IMG");
            }
            // Remove the Css and add the normal Css.
            titleUser.Attributes.Remove("class");
            titleUser.Attributes.Add("class", "MenuFont_1");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleGroup.Attributes.CssStyle.Clear();
            titleGroup.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleGroup.Attributes.Add("class", "MenuFont");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleReport.Attributes.CssStyle.Clear();
            titleMfp.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleMfp.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleReport.Attributes.CssStyle.Clear();
            titleReport.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleReport.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleSet.Attributes.CssStyle.Clear();
            titleSet.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleSet.Attributes.Add("class", "MenuFont_right");

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleAvail.Attributes.CssStyle.Clear();
            titleAvail.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleAvail.Attributes.Add("class", "MenuFont_right");
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
            // Log Menu Added

            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleLog.Attributes.CssStyle.Clear();
            titleLog.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleLog.Attributes.Add("class", "MenuFont_right");
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED
        }
        else if (type.IndexOf("priceinfo") > -1)
        {
            titleUser.Attributes.Remove("class");
            titleUser.Attributes.Add("class", "MenuFont_1");

            titleGroup.Attributes.Remove("class");
            titleGroup.Attributes.Add("class", "MenuFont");

            titleRes.Attributes.Remove("class");
            titleRes.Attributes.Add("class", "MenuFont");

            titlePrice.Attributes.Remove("class");
            titlePrice.Attributes.Add("class", "Select_menu_bg");

            titleMfp.Attributes.Remove("class");
            titleMfp.Attributes.Add("class", "MenuFont_right");

            titleReport.Attributes.Remove("class");
            titleReport.Attributes.Add("class", "MenuFont_right");

            titleSet.Attributes.Remove("class");
            titleSet.Attributes.Add("class", "MenuFont_right");

            titleAvail.Attributes.Remove("class");
            titleAvail.Attributes.Add("class", "MenuFont_right");

            titleLog.Attributes.Remove("class");
            titleLog.Attributes.Add("class", "MenuFont_right");
        }
        else if (type.IndexOf("report") > -1)
        {
            // Report
            if (page.Equals("AvailableReport.aspx".ToLower()))
            {
                // available report
                titleAvail.Attributes.Remove("class");
                titleAvail.Attributes.Add("class", "Select_menu_bg");
                // Icon
                div_icon_type.Attributes.Remove("class");
                div_icon_type.Attributes.Add("class", "Icon_Avail_IMG");

                // Remove the Css and add the normal Css.
                titleUser.Attributes.Remove("class");
                titleUser.Attributes.Add("class", "MenuFont_1");
              
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleGroup.Attributes.CssStyle.Clear();
                titleGroup.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleGroup.Attributes.Add("class", "MenuFont");
               
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleRes.Attributes.CssStyle.Clear();
                titleRes.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleRes.Attributes.Add("class", "MenuFont");
                
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleReport.Attributes.CssStyle.Clear();
                titleReport.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleReport.Attributes.Add("class", "MenuFont");
             
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleSet.Attributes.CssStyle.Clear();
                titleSet.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleSet.Attributes.Add("class", "MenuFont_right");
                // 2010.12.17 Add By SES Zhou Miao Ver.1.1 Update ST
                // Log Menu Added
                
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleLog.Attributes.CssStyle.Clear();
                titleLog.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleLog.Attributes.Add("class", "MenuFont_right");
                // 2010.12.17 Add By SES Zhou Miao Ver.1.1 Update ED
            }
            // 2010.12.17 Add By SES Zhou Miao Ver.1.1 Update ST
            // Log View
            else if (page.Equals("LogView.aspx".ToLower()))
            {
                // Log View report
                titleLog.Attributes.Remove("class");
                titleLog.Attributes.Add("class", "Select_menu_bg");
                // Icon
                div_icon_type.Attributes.Remove("class");
                div_icon_type.Attributes.Add("class", "Icon_Report_User_IMG");
                // Remove the Css and add the normal Css.
                titleUser.Attributes.Remove("class");
                titleUser.Attributes.Add("class", "MenuFont_1");
            
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleRes.Attributes.CssStyle.Clear();
                titleRes.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleRes.Attributes.Add("class", "MenuFont");
                
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleReport.Attributes.CssStyle.Clear();
                titleReport.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleReport.Attributes.Add("class", "MenuFont");
              
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleSet.Attributes.CssStyle.Clear();
                titleSet.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleSet.Attributes.Add("class", "MenuFont_right");
           
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleAvail.Attributes.CssStyle.Clear();
                titleAvail.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleAvail.Attributes.Add("class", "MenuFont_right");

            }
            // 2010.12.17 Add By SES Zhou Miao Ver.1.1 Update ED
            else
            {
                // normal report.
                titleReport.Attributes.Remove("class");
                titleReport.Attributes.Add("class", "Select_menu_bg");
                // Icon
                div_icon_type.Attributes.Remove("class");
                if (page.Equals("JobReport.aspx".ToLower()))
                {
                    div_icon_type.Attributes.Add("class", "Icon_Report_IMG");
                }
                else if (page.Equals("TotalJobReport.aspx".ToLower()))
                {
                    div_icon_type.Attributes.Add("class", "Icon_Report_Total_IMG");
                }
                else if (page.Equals("UserJobReport.aspx".ToLower()))
                {
                    div_icon_type.Attributes.Add("class", "Icon_Report_User_IMG");
                }
                else if (page.Equals("GroupJobReport.aspx".ToLower()))
                {
                    div_icon_type.Attributes.Add("class", "Icon_Report_Group_IMG");
                }
                else if (page.Equals("MFPJobReport.aspx".ToLower()))
                {
                    div_icon_type.Attributes.Add("class", "Icon_Report_MFP_IMG");
                }
                // Remove the Css and add the normal Css.
                titleUser.Attributes.Remove("class");
                titleUser.Attributes.Add("class", "MenuFont_1");
                
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleGroup.Attributes.CssStyle.Clear();
                titleGroup.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleGroup.Attributes.Add("class", "MenuFont");
           
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleRes.Attributes.CssStyle.Clear();
                titleRes.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleRes.Attributes.Add("class", "MenuFont");
                
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleSet.Attributes.CssStyle.Clear();
                titleSet.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleSet.Attributes.Add("class", "MenuFont_right");
                
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleAvail.Attributes.CssStyle.Clear();
                titleAvail.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleAvail.Attributes.Add("class", "MenuFont_right");
                // 2010.12.17 Add By SES Zhou Miao Ver.1.1 Update ST
                // Log Menu Added
               
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
                //titleLog.Attributes.CssStyle.Clear();
                titleLog.Attributes.Remove("class");
                // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
                titleLog.Attributes.Add("class", "MenuFont");
                // 2010.12.17 Add By SES Zhou Miao Ver.1.1 Update ED
            }
            // 2010.12.17 Delete By SES Zhou Miao Ver.1.1 Update ST
            //// 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
            //// Log Menu Added
            //titleLog.Attributes.CssStyle.Clear();
            //titleLog.Attributes.Add("class", "MenuFont_right");
            //// 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED
            // 2010.12.17 Delete By SES Zhou Miao Ver.1.1 Update ST

        }
        else if (type.IndexOf("settings") > -1)
        {
            // Settings
            titleSet.Attributes.Remove("class");
            titleSet.Attributes.Add("class", "Select_menu_bg");
            // Icon
            div_icon_type.Attributes.Remove("class");
            div_icon_type.Attributes.Add("class", "Icon_Set_IMG");
            // Remove the Css and add the normal Css.
            titleUser.Attributes.Remove("class");
            titleUser.Attributes.Add("class", "MenuFont_1");
           
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleGroup.Attributes.CssStyle.Clear();
            titleGroup.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleGroup.Attributes.Add("class", "MenuFont");
         
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleRes.Attributes.CssStyle.Clear();
            titleRes.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleRes.Attributes.Add("class", "MenuFont");
            
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleReport.Attributes.CssStyle.Clear();
            titleReport.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleReport.Attributes.Add("class", "MenuFont");
          
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleAvail.Attributes.CssStyle.Clear();
            titleAvail.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleAvail.Attributes.Add("class", "MenuFont");
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
            // Log Menu Added
           
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleLog.Attributes.CssStyle.Clear();
            titleLog.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleLog.Attributes.Add("class", "MenuFont");
            // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED
        }
        // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ST
        // Log Menu Added
        else
            // Add by Zheng Wei 2012.03.14
            if (type.IndexOf("mfpinfo") > -1)
            {
                titleMfp.Attributes.Remove("class");
                titleMfp.Attributes.Add("class", "Select_menu_bg");
                div_icon_type.Attributes.Remove("class");
                if (page.Equals("MFPRestrictionList.aspx".ToLower()))
                {
                    div_icon_type.Attributes.Add("class", "Icon_MFP_Management_IMG");
                }
                else if (page.Equals("AddMFPRestriction.aspx".ToLower()))
                {
                    div_icon_type.Attributes.Add("class", "Icon_MFP_Management_Add_IMG");
                }
                else if (page.Equals("EditMfpRestriction.aspx".ToLower()))
                {
                    div_icon_type.Attributes.Add("class", "Icon_MFP_Management_Edit_IMG");
                }

                titleUser.Attributes.Remove("class");
                titleUser.Attributes.Add("class", "MenuFont_right");
                titleGroup.Attributes.Remove("class");
                titleGroup.Attributes.Add("class", "MenuFont_right");
                titleRes.Attributes.Remove("class");
                titleRes.Attributes.Add("class", "MenuFont_right");
                titleReport.Attributes.Remove("class");
                titleReport.Attributes.Add("class", "MenuFont_right");

                titleSet.Attributes.Remove("class");
                titleSet.Attributes.Add("class", "MenuFont_right");

                titleAvail.Attributes.Remove("class");
                titleAvail.Attributes.Add("class", "MenuFont_right");

                titleLog.Attributes.Remove("class");
                titleLog.Attributes.Add("class", "MenuFont_right");


            }
        else 
        {
            // Settings
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleLog.Attributes.CssStyle.Clear();
            titleLog.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleLog.Attributes.Add("class", "Select_menu_bg");
            // Icon
            div_icon_type.Attributes.Remove("class");
            div_icon_type.Attributes.Add("class", "Icon_Set_IMG");
            // Remove the Css and add the normal Css.
            titleUser.Attributes.Remove("class");
            titleUser.Attributes.Add("class", "MenuFont_1");
        
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleGroup.Attributes.CssStyle.Clear();
            titleGroup.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleGroup.Attributes.Add("class", "MenuFont");
          
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleRes.Attributes.CssStyle.Clear();
            titleRes.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleRes.Attributes.Add("class", "MenuFont");
            
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleReport.Attributes.CssStyle.Clear();
            titleReport.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleReport.Attributes.Add("class", "MenuFont");
            
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ST
            //titleAvail.Attributes.CssStyle.Clear();
            titleAvail.Attributes.Remove("class");
            // 2010.12.28 Update By SES Zhoumiao Ver.1.1 Update ED
            titleAvail.Attributes.Add("class", "MenuFont");
            titleSet.Attributes.Remove("class");
            titleSet.Attributes.Add("class", "MenuFont_right");
        }

        // 2010.11.19 Add By SES Jijianxiong Ver.1.1 Update ED
        return;
    }
    #endregion

    #region "openPDF Clicked"
    /// <summary>
    /// Download User Guide PDF
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.12.25</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.1</Version>
    public void openPDF_Click(object sender, EventArgs e)
    {
        string PDFURL = "";

        if (HttpContext.Current.User.Identity.Name.Equals(UtilConst.CON_USER_LONINNAME))
        {
            PDFURL = "Admin";
        }
        else
        {
            PDFURL = "User";
        }
        // PDFWindow Open.
        string strScript = "<script language='javascript' type='text/javascript'>";
        strScript = strScript + "openPDFWindow('" + PDFURL + "')";
        strScript = strScript + "</script>";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "openPDFWindow", strScript);

    }

    #endregion

    #region "NewTitle"
    /// <summary>
    /// NewTitle
    /// </summary>
    /// <param name="newTitle"></param>
    /// <param name="mainUrl"></param>
    /// <param name="isMandatoryNeed"></param>
    /// 2012.03.14</Date>
    /// <Author>SLC Zheng Wei</Author>
    /// <Version>1.2</Version>
    public void NewTitle(string newTitle, string mainUrl, bool isMandatoryNeed)
    {
        // Edit Screen with  Mandatory Need.
        if (isMandatoryNeed)
        {
            tblmust.Visible = true;
        }
        // Sub Title Visible = true.
        imgSubTitle.Visible = true;

        lblSubTitle.Visible = true;

        lblSubTitle.Text = newTitle;

        this.lblTitle.InnerHtml = "<a href='" + mainUrl + "' class='Black_FONT_a'>" + this.lblTitle.InnerText + "</a>";
    }

    #endregion

}
