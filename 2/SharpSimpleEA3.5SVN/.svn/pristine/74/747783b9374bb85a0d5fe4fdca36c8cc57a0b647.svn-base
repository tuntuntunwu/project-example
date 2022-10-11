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

//界面控件对应的ID

//服务器名称   TB_ServerName
//端口  TB_Port
//用户名   TB_UserName
//密码  TB_Password
//选择数据库  TB_DB
//选择表名  TB_Table

//用户全名  TB_UserFullName
//用户登录名  TB_LoginName
//卡片ID  TB_CardID
//用户所属部门   Drop_Department
//Email  TB_Email
//PIN  TB_PIN

//强制卡号ID位数  TB_CardIDTotalnum
//补充位置   Drop_Position
//补充字符   TB_letter
//状态字段  TB_status



public partial class Settings_DBAuthSetting : MainPage
{
    //把数据库内容加载到界面上
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_LDAP_CONNECT_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_LDAP_CONNECT_SET, "DBAuthSetting.aspx", false);
        this.Master.JobReportTitle();

        //GroupInfoTableAdapter GroupInfoAdapter = new GroupInfoTableAdapter();
        //ddlGroupName1.DataSource = GroupInfoAdapter.GetGroupInfoData();
        //ddlGroupName1.DataBind();
        //ddlGroupName2.DataSource = GroupInfoAdapter.GetGroupInfoData();
        //ddlGroupName2.DataBind();

        if (!IsPostBack)//初始化页面或控件
        {
            initUI();
        }  


    }
    protected void initUI()
    {


        GroupInfoTableAdapter GroupInfoAdapter = new GroupInfoTableAdapter();
        ddlGroupName1.DataSource = GroupInfoAdapter.GetGroupInfoData();
        ddlGroupName1.DataBind();
        ddlGroupName2.DataSource = GroupInfoAdapter.GetGroupInfoData();
        ddlGroupName2.DataBind();


        BllDBAuthConfig bllDBAuth = new BllDBAuthConfig();
        //Model.LDAPModel model = new Model.LDAPModel();
        Model.DBThirdAuthConfigEntry bean = bllDBAuth.GetDBAutoInfo();

        //DBServer
        if (bean != null)
        {
            TB_ConnectStr.Text = bean.DBConnectStr;
            //用户信息匹配
            TB_SearchSql.Text = bean.DBSearchSql;
            TB_WhereSql.Text = bean.DBWhereSql;

            MTGrpVal1.Text = bean.MTGrpVal1;
            MTGrpVal2.Text = bean.MTGrpVal2;

            // GroupID
            ddlGroupName1.SelectedValue = bean.GroupID1.ToString();
            ddlGroupName2.SelectedValue = bean.GroupID2.ToString();

            AuthDBFlg.Text = bean.AuthDBFlg.ToString();
        }
    }
    //设定按钮动作
    protected void btnSetting_Click(object sender, EventArgs e)
    {
        //test();
        BllDBAuthConfig bllDBAuth = new BllDBAuthConfig();
        //Model.LDAPModel model = new Model.LDAPModel();
        Model.DBThirdAuthConfigEntry bean = new DBThirdAuthConfigEntry();
        getAuthInfo(bean);
        bllDBAuth.Update(bean);   
            
     }
    protected void getAuthInfo(Model.DBThirdAuthConfigEntry bean)
    {
        //DBServer
        bean.DBConnectStr = TB_ConnectStr.Text.Trim();
        //用户信息匹配
        bean.DBSearchSql = TB_SearchSql.Text.Trim();
        bean.DBWhereSql = TB_WhereSql.Text.Trim();
        bean.MTGrpVal1 = MTGrpVal1.Text.Trim();
        bean.MTGrpVal2 = MTGrpVal2.Text.Trim();
        try
        {
            bean.GroupID1 = Convert.ToInt32(ddlGroupName1.SelectedValue);
            bean.GroupID2 = Convert.ToInt32(ddlGroupName2.SelectedValue);
        }
        catch (Exception ex)
        {
            bean.GroupID1 = 0;
            bean.GroupID2 = 0;
        }
        try
        {
            bean.AuthDBFlg = Convert.ToInt16(AuthDBFlg.Text.Trim());
        }
        catch (Exception ex)
        {
            bean.AuthDBFlg = 0;
        }
            
    }
    
    //连接数据库测试按钮
    protected void btnTest_Click(object sender, EventArgs e)
    {
        DBAuthHandler authHandler = new DBAuthHandler();
        Model.DBThirdAuthConfigEntry bean = new DBThirdAuthConfigEntry();
        getAuthInfo(bean);

        string ret = authHandler.DBConnect(bean);
        if (ret.Trim().Equals(""))
        {
            this.lbl_con_status.Text = "连接数据库成功！";
        }
        else
        {
            this.lbl_con_status.Text = ret;
        }
    }
    //检索表测试按钮
    protected void btnTestSel_Click(object sender, EventArgs e)
    {
        // test();
        DBAuthHandler authHandler = new DBAuthHandler();
        Model.DBThirdAuthConfigEntry bean = new DBThirdAuthConfigEntry();
        getAuthInfo(bean);
        string ret = authHandler.DBTestSearch2(bean);
        if (ret.Trim().Equals(""))
        {
            this.lbl_testsearch.Text = "检索数据表成功！";
        }
        else
        {
            this.lbl_testsearch.Text = ret;
        }
    }
    //protected void btnTestWhere_Click(object sender, EventArgs e)
    //{
    //    // test();
    //    DBAuthHandler authHandler = new DBAuthHandler();
    //    Model.DBThirdAuthConfigEntry bean = new DBThirdAuthConfigEntry();
    //    getAuthInfo(bean);
    //    string ret = authHandler.DBTestSearch2(bean);
    //    if (ret.Trim().Equals(""))
    //    {
    //        this.lbl_testsearch.Text = "检索数据表成功！";
    //    }
    //    else
    //    {
    //        this.lbl_testsearch.Text = ret;
    //    }
    //}

    protected void btnAuthICCard_Click(object sender, EventArgs e)
    {
        // test();
        DBAuthHandler authHandler = new DBAuthHandler();
        string ret = authHandler.DBAuthCard("0003751090");
        if (ret.Trim().Equals(""))
        {
            this.lbl_testsearch.Text = "检索数据表成功！";
        }
        else
        {
            this.lbl_testsearch.Text = ret;
        }
    }
    #region onSelectedGroup1Changed
    protected void onSelectedGroup1Changed(object sender, EventArgs e)
    {
    }
    #endregion
    #region onSelectedGroup2Changed
    protected void onSelectedGroup2Changed(object sender, EventArgs e)
    {
    }
    #endregion
}
   