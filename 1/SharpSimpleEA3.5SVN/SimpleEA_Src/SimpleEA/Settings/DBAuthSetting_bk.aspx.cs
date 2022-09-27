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


        BllDBAuth bllDBAuth = new BllDBAuth();
        //Model.LDAPModel model = new Model.LDAPModel();
        Model.DBThirdAuthSettingEntry bean = bllDBAuth.GetDBAutoInfo();

        //DBServer
        TB_ServerName.Text = bean.DBAuthServerIP;
        TB_Port.Text = bean.DBAuthServerPort;
        TB_UserName.Text = bean.DBUserName;
        TB_Password.Text = bean.DBPassword;
        TB_DB.Text = bean.DBAuthDBNM;
        TB_Table.Text = bean.DBTableNM;
        //用户信息匹配
        TB_UserFullName.Text = bean.MTFullName;
        TB_LoginName.Text = bean.MTLoginName;
        TB_CardID.Text = bean.MTIDCard;
        TB_Group.Text = bean.MTGroup;
        TB_Email.Text = bean.MTEmail;
        TB_PIN.Text = bean.MTPINCode;

        // GroupID
        ddlGroupName1.SelectedValue = bean.GroupID1.ToString();
        ddlGroupName2.SelectedValue = bean.GroupID2.ToString();


        //卡同步规则
        if (bean.IDCordRule == 1 )
        {
            CHK_Start1.Checked = true;
        }
        else
        {
            CHK_Start1.Checked = false;
        }
        TB_CardIDTotalnum.Text = bean.IDCardDataLen ;
        if (bean.IDCardADDPos  == "0" )
        {
            Drop_Position.Text = "前缀";
        }
        else
        {
            Drop_Position.Text = "后缀";
        }
        TB_letter.Text = bean.IDCardADDCHR;
        //用户信息判断规则
        if (bean.UserInfoJudgeRule == 1)
        {
            CHK_Start2.Checked = true;
        }
        else
        {
            CHK_Start2.Checked = false;
        }
        TB_status.Text = bean.UserJudgeFeild ;
        TB_status_val.Text = bean.UserJudgeFeildVal;
    }
    //设定按钮动作
    protected void btnSetting_Click(object sender, EventArgs e)
    {
        //test();
        BllDBAuth bllDBAuth = new BllDBAuth();
        //Model.LDAPModel model = new Model.LDAPModel();
        Model.DBThirdAuthSettingEntry bean = new DBThirdAuthSettingEntry();
        
        //DBServer
        bean.DBAuthServerIP = TB_ServerName.Text.Trim();
        bean.DBAuthServerPort = TB_Port.Text.Trim();
        bean.DBUserName = TB_UserName.Text.Trim();
        bean.DBPassword = TB_Password.Text.Trim();
        bean.DBAuthDBNM = TB_DB.Text.Trim();
        bean.DBTableNM = TB_Table.Text.Trim();
        //用户信息匹配
        bean.MTFullName = TB_UserFullName.Text.Trim();
        bean.MTLoginName = TB_LoginName.Text.Trim();
        bean.MTPwd = TB_UserPwd.Text.Trim();
        bean.MTIDCard = TB_CardID.Text.Trim();
        bean.MTGroup = TB_Group.Text.Trim();
        bean.MTEmail = TB_Email.Text.Trim();
        bean.MTPINCode = TB_PIN.Text.Trim();
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

        //卡同步规则
		if( CHK_Start1.Checked)
		{
			bean.IDCordRule = 1;
		}else{
			bean.IDCordRule = 0;
		}
        bean.IDCardDataLen = TB_CardIDTotalnum.Text.Trim();
		if(Drop_Position.Text == "前缀" )
		{
			bean.IDCardADDPos = "0";
		}
		else
		{
			bean.IDCardADDPos = "1";
		}
        bean.IDCardADDCHR = TB_letter.Text.Trim();
        //用户信息判断规则
        if (CHK_Start2.Checked )
		{
			bean.UserInfoJudgeRule = 1;
		}else{
			bean.UserInfoJudgeRule = 0;
		}
        bean.UserJudgeFeild = TB_status.Text.Trim();
        bean.UserJudgeFeildVal = TB_status_val.Text.Trim();
        bllDBAuth.Update(bean);   
            
     }
    
    
    //连接数据库测试按钮
    protected void btnTest_Click(object sender, EventArgs e)
    {
       // test();
        DBAuthHandler2 authHandler = new DBAuthHandler2();
        //string DBAuthServerIP = TB_ServerName.Text.Trim();
        //string DBAuthServerPort = TB_Port.Text.Trim();
        //string DBUserName = TB_UserName.Text.Trim();
        //string DBPassword = TB_Password.Text.Trim();
        //string DBAuthDBNM = TB_DB.Text.Trim();
        //string DBTableNM = TB_Table.Text.Trim();
        Model.DBThirdAuthSettingEntry bean = new DBThirdAuthSettingEntry();

        //DBServer
        bean.DBAuthServerIP = TB_ServerName.Text.Trim();
        bean.DBAuthServerPort = TB_Port.Text.Trim();
        bean.DBUserName = TB_UserName.Text.Trim();
        bean.DBPassword = TB_Password.Text.Trim();
        bean.DBAuthDBNM = TB_DB.Text.Trim();

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
        DBAuthHandler2 authHandler = new DBAuthHandler2();
        //string DBTableNM = TB_Table.Text.Trim();
        //string MTFullName = TB_UserFullName.Text.Trim();
        //string MTLoginName = TB_LoginName.Text.Trim();
        //string MTIDCard = TB_CardID.Text.Trim();
        //string MTGroup = TB_Group.Text.Trim();
        //string MTEmail = TB_Email.Text.Trim();
        //string MTPINCode = TB_PIN.Text.Trim();
        Model.DBThirdAuthSettingEntry bean = new DBThirdAuthSettingEntry();

        //DBServer
        bean.DBAuthServerIP = TB_ServerName.Text.Trim();
        bean.DBAuthServerPort = TB_Port.Text.Trim();
        bean.DBUserName = TB_UserName.Text.Trim();
        bean.DBPassword = TB_Password.Text.Trim();
        bean.DBAuthDBNM = TB_DB.Text.Trim();
        bean.DBTableNM = TB_Table.Text.Trim();
        //用户信息匹配
        bean.MTFullName = TB_UserFullName.Text.Trim();
        bean.MTLoginName = TB_LoginName.Text.Trim();
        bean.MTPwd = TB_UserPwd.Text.Trim();
        bean.MTIDCard = TB_CardID.Text.Trim();
        bean.MTGroup = TB_Group.Text.Trim();
        bean.MTEmail = TB_Email.Text.Trim();
        bean.MTPINCode = TB_PIN.Text.Trim();
        string ret = authHandler.DBTestSearch(bean);
        if (ret.Trim().Equals(""))
        {
            this.lbl_testsearch.Text = "检索数据表成功！";
        }
        else
        {
            this.lbl_testsearch.Text = ret;
        }
    }
    //检索表测试按钮
    protected void btnTestSel2_Click(object sender, EventArgs e)
    {
        // test();
        DBAuthHandler2 authHandler = new DBAuthHandler2();
        Model.DBThirdAuthSettingEntry bean = new DBThirdAuthSettingEntry();

        //DBServer
        bean.DBAuthServerIP = TB_ServerName.Text.Trim();
        bean.DBAuthServerPort = TB_Port.Text.Trim();
        bean.DBUserName = TB_UserName.Text.Trim();
        bean.DBPassword = TB_Password.Text.Trim();
        bean.DBAuthDBNM = TB_DB.Text.Trim();
        bean.DBTableNM = TB_Table.Text.Trim();
        if (CHK_Start2.Checked)
        {
            bean.UserInfoJudgeRule = 1;
        }
        else
        {
            bean.UserInfoJudgeRule = 0;
            return;
        }
        bean.UserJudgeFeild = TB_status.Text.Trim();
        bean.UserJudgeFeildVal = TB_status_val.Text.Trim();

        string ret = authHandler.DBTestSearch2(bean);
        if (ret.Trim().Equals(""))
        {
            this.lbl_TestSel2.Text = "检索数据表成功！";
        }
        else
        {
            this.lbl_TestSel2.Text = ret;
        }

        //authHandler.DBLogin("chenyg", "123456");
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
   