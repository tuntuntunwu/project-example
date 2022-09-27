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


public partial class Settings_LDAPSetting : MainPage
{
    //把数据库内容加载到界面上
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_LDAP_CONNECT_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_LDAP_CONNECT_SET, "LDAPSetting.aspx", false);
        this.Master.JobReportTitle();

        if (!IsPostBack)//初始化页面或控件
        {
            BllLDAPTotal bllLDAPTotal = new BllLDAPTotal();
            Model.LDAPModel bean = bllLDAPTotal.GetLDAPInfo();

            //LDAP离线时DB是否可用
            if (bean.DB_Allowed == 1)
            {
                this.DBAllowed.Checked = true;
            }
            else
            {
                this.DBAllowed.Checked = false;
            }
            //连接设置（其中认证方式类型Drop_VerType1是固定的只有none）
            this.TB_IP.Text = bean.Con_IP;
            this.TB_Port.Text = bean.Con_Port;

            //认证类型
            Drop_VerType1.Text = bean.Con_Verification;
            //连接认证账号
            this.TB_Account.Text = bean.Con_Account;
            //连接认证密码
            this.TB_Password.Text = bean.Con_Password;

            //认证设置（其中认证方式类型Drop_VerType2是固定的只有none）
            //用户登录属性
            this.TB_UserLoginAttri.Text = bean.Ver_Login;
           
            //针对Active Directory使用者
            //登录形式
            Drop_LoginType.Text = bean.Ver_Type;
            //NT或DNS域名
            this.TB_NTorDNS.Text = bean.Ver_NTorDNS;
            
            //LDAP组设置
            //用户组默认配额方案
            RestrictionInfoTableAdapter RestrictionAdapter = new RestrictionInfoTableAdapter();
            lstRestriction.DataSource = RestrictionAdapter.GetRestrictionInfoData2();
            this.lstRestriction.DataBind();
            this.lstRestriction.SelectedValue = bean.Group_Verification;
            //获取用户组信息
            Drop_AttriName.Text = bean.Group_AttributeName;

            //用户设置
            //搜索默认DN设置
            this.TB_DefaultDN.Text = bean.User_DNSetting;
            //搜索范围
            Drop_Search.Text = bean.User_Search;
            //用户名
            this.TB_ObtainUserName.Text = bean.User_Name;
            //Email
            this.TB_Email.Text = bean.User_Email;
            //IC卡编号
            this.TB_ICCard.Text = bean.User_ICNum;
            //
            this.TB_UserName.Text = bean.User_LDAPName;
            this.TB_UserPassword.Text = bean.User_LDAPPassword;

        }  


    }
    //设定按钮动作
    protected void btnSetting_Click(object sender, EventArgs e)
    {
        //test();
        BllLDAPTotal bllLDAPTotal = new BllLDAPTotal();
        Model.LDAPModel model = new Model.LDAPModel();
        model.Con_IP = TB_IP.Text;
        model.Con_Port = TB_Port.Text;
        model.Con_Verification = Drop_VerType1.Text;
        model.Con_Account = TB_Account.Text;
        model.Con_Password = TB_Password.Text;

        //DB使用规则 Du Qinyi
        if (DBAllowed.Checked)
        {
            model.DB_Allowed = 1;
        }
        else
        {
            model.DB_Allowed = 0;
        }
        // 认证设置
        //model.Ver_Verification = Drop_VerType2.Text;
        model.Ver_Verification = Drop_VerType1.Text;
        model.Ver_Login = TB_UserLoginAttri.Text;
        model.Ver_Type = Drop_LoginType.Text;
        model.Ver_NTorDNS = TB_NTorDNS.Text;
        
        //组设置
        model.Group_Verification = lstRestriction.SelectedValue;
        model.Group_UserAttribute_or_DN = "";
        model.Group_AttributeName = Drop_AttriName.Text;
        

        //用户设置
        model.User_DNSetting = TB_DefaultDN.Text;
        model.User_Search = Drop_Search.Text;
        model.User_Name = TB_ObtainUserName.Text;
        model.User_Email = TB_Email.Text;
        model.User_ICNum = TB_ICCard.Text;
        model.User_LDAPName = TB_UserName.Text;
        model.User_LDAPPassword = TB_UserPassword.Text;


        if (!Page.IsValid)
        {
            return;
        }

        bllLDAPTotal.Update(model);   
            
     }
    //cui20170709
    //测试域服务器主机名/IP地址，连接认证账号，连接认证密码是否正确
    //（设定也要有这部分内容，先验证再写数据库）
    public void test()
    {
        String server = this.TB_IP.Text;
        String username = this.TB_Account.Text;
        String password = this.TB_Password.Text;

        LDAPHandler test = new LDAPHandler();

        test.IDAuthentication(username, password);
    }

    //当dropDrowList2为sAMAccountName时，下面的textbox不可编辑，是其他的时候，可编辑(已实现cui20170718)

    protected void myListDropDown_Change(object sender, EventArgs e)
    {
        //stuff that never gets hit

        if (Drop_LoginType.SelectedValue == "sAMAccountName")
        {
            this.TB_NTorDNS.Enabled = false;
        }
        else //if (DropDownList2.SelectedValue == "NT Domain Name" || DropDownList2.SelectedValue == "User Prinicipal")
        {
            //LDAPbll.Update_Verification_Single(model);
            this.TB_NTorDNS.Enabled = true;
            //this.TB_NTorDNS.Text = bean.Ver_NTorDNS;
        }

    }

    /// <summary>
    /// 组设置（单选按钮事件）
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    //连接认证部分的按钮
    protected void btnTest_Click(object sender, EventArgs e)
    {

    
        String server = this.TB_IP.Text;
        String username = this.TB_Account.Text;
        String password = this.TB_Password.Text;

        LDAPHandler test = new LDAPHandler();
        //test.IDAuthentication(server, username, password);
        if (test.TestIDAuthentication(server, username, password) == 0)
        //string ret = test.IDAuthentication(username, password);
        // if (ret.Equals(""))
        {
            label.Text = "认证成功";
            //MessageBox.Show("认证成功");
        }
        else
        {
            //label.Text = "认证失败";
            //MessageBox.Show("认证失败");
            label.Text = "认证失败";
             
        }
    }
    //Test User Account部分的测试按钮
    protected void Bt_Test_Click(object sender, EventArgs e)
    {


        String server = this.TB_IP.Text;
        String username = this.TB_UserName.Text;
        String password = this.TB_UserPassword.Text;

        LDAPHandler test = new LDAPHandler();

        test.IDAuthentication(username, password);

        //test.LDAPAuthCard("1234567890");
    }
    //组设置
    //和认证设置里面相似的问题，已解决（cui201707018）
    //List<string> strList2 = new List<string>();
    //protected void onAckTypeChanged(object sender, EventArgs e)
    //{
    //    //DalDropAndPlus DropAndPlusdal = new DalDropAndPlus();
    //    //BllDropAndPlus DropAndPlusbll = new BllDropAndPlus();
    //    //Model.DropAndPlusModel model = new Model.DropAndPlusModel();
    //    if (RadioButton1.SelectedItem.Text == " User Attribute")
    //    {
    //        //DropAndPlusdal.GetNameByCD();
    //        strList2.Add("department");
    //        strList2.Add("memberOf");
    //        Drop_AttriName.DataSource = strList2;
    //        Drop_AttriName.DataBind();
    //    }
    //    else
    //    {
    //        // strList2.Add("");
    //        strList2.Add("OU");
    //        Drop_AttriName.DataSource = strList2;
    //        Drop_AttriName.DataBind();
    //    }
    //}

    //protected void DBAllowed_CheckedChanged(object sender, EventArgs e)
    //{
    //    DBAllowed.Checked = !DBAllowed.Checked;
    //}

    protected void PingTest_Click(object sender, EventArgs e)
    {
        LDAPHandler handler = new LDAPHandler();

        if (this.TB_IP.Text.Trim().Equals(""))
        {
            label1.Text = "连接服务器IP地址不能为空！";
            return;
        }

        if (handler.PingCheck(this.TB_IP.Text))
        {
            //MessageBox.Show("连接服务器成功");
            label1.Text = "连接服务器成功";
        }
        else
        {
            //MessageBox.Show("连接服务器失败");
            label1.Text = "连接服务器失败";
        }
    }
}
   