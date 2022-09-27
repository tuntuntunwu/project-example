﻿using System;
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
/// author: wangziyang
/// date:2019.09.26
/// </summary>
/// 

public partial class SectionGroupSetting : GrpInfoMain
{
    DalSectionGroup dalsg = new DalSectionGroup();
    DalSectionInfo dalsi = new DalSectionInfo();
    DalGroup dalgroup = new DalGroup();
    Dictionary<string, int> glist = new Dictionary<string, int>();

    #region "Page_Load"r

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_SECTION_GROUP_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_SECTION_GROUP_SET, "SectionGroupSetting.aspx", false);
        this.Master.JobReportTitle();


        if (!IsPostBack)
        {

            string SectionID = ddl2.SelectedValue;
            if (SectionID != "")
            {
                List<GroupBelongEntry> groupnobelonglist = dalsg.getGroupNoBelongList();
                this.lstNoBelong.DataSource = groupnobelonglist;
                this.lstNoBelong.DataBind();

                List<GroupBelongEntry> groupbelonglist = dalsg.getGroupBelongList(SectionID);
                this.lstBelong.DataSource = groupbelonglist;
                this.lstBelong.DataBind();
            }

        }

    }
    #endregion


    #region "Move Items form Not Belong User List To Belong User List."
    /// <summary>
    /// Move Items form Not Belong User List To Belong User List.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2019.06.09</Date>
    /// <Author>SES Wang Ziyang</Author>
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
    /// <Date>2019.06.09</Date>
    /// <Author>SES Wang Ziyang</Author>
    /// <Version>0.01</Version>
    protected void btnToNoBelong_Click(object sender, EventArgs e)
    {
        // Move Item from lstBelong to lstNoBelong.
        MoveList(lstBelong, lstNoBelong);
    }
    #endregion


    #region onSelectedGroupChanged
    /// <summary>
    /// Button; Selected Section Lebel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2019.06.09</Date>
    /// <Author>SES Wang Ziyang</Author>
    /// <Version>0.01</Version>
    protected void sGonSelectedLebelChanged(object sender, EventArgs e)
    {
        try
        {
            ddl2.Items.Clear();

            int lebel = int.Parse(ddl1.SelectedValue);
            List<SectionInfoEntry> slist = dalsi.getSectionInfo(lebel);
            if (slist.Count > 0)
            {
                ddl2.DataSource = slist;
                ddl2.DataValueField = "SectionID";
                ddl2.DataTextField = "SectionName";
                ddl2.DataBind();
                ddl2.SelectedIndex = 0;

                ddl2.SelectedIndex = 0;
            }
        }
        catch
        {
           
        }
    }
    #endregion

    #region btnBack_Click
    /// <summary>
    /// Button Back
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2019.06.09</Date>
    /// <Author>SES Wang Ziyang</Author>
    /// <Version>0.01</Version>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("GroupList.aspx", false);
    }
    #endregion

    #region "update Group  belong"
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string SectionID = ddl2.SelectedValue;

            if (!Page.IsValid)
            {
                return;
            }
            dalsg.DeleteSection(SectionID);
            foreach (ListItem item in lstBelong.Items)
            {
                dalgroup.UpdateBelong(SectionID, item.Value);
            }

        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }
    }
    #endregion
}