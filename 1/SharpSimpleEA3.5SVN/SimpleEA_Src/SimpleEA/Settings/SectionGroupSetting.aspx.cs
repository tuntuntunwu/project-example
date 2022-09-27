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
/// author: wangziyang
/// date:2019.09.26
/// </summary>
/// 

public partial class Settings_SectionGroupSetting : MainPage
{
    DalSectionGroup dalsg = new DalSectionGroup();
    DalSectionInfo dalsi = new DalSectionInfo();
    DalGroup dalgroup = new DalGroup();
    Dictionary<string,int> glist = new Dictionary<string,int>();
    #region "Page_Load"r

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_SECTION_GROUP_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_SECTION_GROUP_SET, "SectionGroupSetting.aspx", false);
        this.Master.JobReportTitle();


        List<GroupEntry> sectionlist = dalgroup.getAllUserGroupList();
        List<string> s = new List<string>();
        foreach (GroupEntry g in sectionlist)
        {
            glist.Add(g.GroupName,g.ID);
            s.Add(g.GroupName);
        } 
        ddl1.DataSource = s;
        ddl1.DataBind();
        ddl1.SelectedIndex = 0;

        if (!IsPostBack)
        {
            int lebel = int.Parse(ddl2.SelectedValue);
            List<SectionInfoEntry> slist = dalsi.getSectionInfo(lebel);
            ddl3.DataSource = slist;
            ddl3.DataValueField = "SectionName";
            ddl3.DataTextField = "SectionName";
            ddl3.DataBind();
            ddl3.SelectedIndex = 0;
        }
    }
    #endregion

    #region "Insert User Information"
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            SectionGroupEntry bean = new SectionGroupEntry();
            bean.GroupID = glist[ddl1.SelectedValue];
            bean.SectionID = dalsi.GetInfoByName(ddl3.SelectedValue).SectionID;
            //this.Response.Redirect("SectionGroupSetting.aspx", false);
            //this.Response.Write("<script>function window.onload() {alert('弹出的消息'); } </script>");
            this.dalsg.Add(bean);
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }
    }
    #endregion


    #region onSelectedLebelChanged
    protected void uGonSelectedLebelChanged(object sender, EventArgs e)
    {
        int lebel = ddl1.SelectedIndex;//用户组选择
        //ddlParentSectuibBindData(lebel);
    }
    #endregion


    #region onSelectedGroupChanged
    protected void sGonSelectedLebelChanged(object sender, EventArgs e)
    {
        int lebel = int.Parse(ddl2.SelectedValue);
        List<SectionInfoEntry> slist = dalsi.getSectionInfo(lebel);
        if (slist.Count > 0)
        {
            ddl3.DataSource = slist;
            ddl3.DataValueField = "SectionName";
            ddl3.DataTextField = "SectionName";
            ddl3.DataBind();
            ddl3.SelectedIndex = 0;
        }
        else
        {
            ddl3.Items.Clear();
        }
    }
    #endregion


    #region onSelectedGroupChanged
    protected void sonSelectedLebelChanged(object sender, EventArgs e)
    {
        int lebel = ddl3.SelectedIndex;//集团选择

    }
    #endregion

}