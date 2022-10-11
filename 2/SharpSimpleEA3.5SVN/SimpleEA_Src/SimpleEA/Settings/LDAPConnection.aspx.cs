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
using BLL;
using DAL;
using Model;
using common;


public partial class Settings_LDAPConnectionn : MainPage
{
    //把数据库内容加载到界面上
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_LDAP_CONNECT_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_LDAP_CONNECT_SET, "LDAPConnection.aspx", false);
        this.Master.JobReportTitle();

        if (!IsPostBack)//初始化页面或控件
        {
            //下拉菜单
            List<string> strList = new List<string>();
            strList.Add("None");
            strList.Add("Anonymous");
            DropDownList1.DataSource = strList;
            DropDownList1.DataBind();

            BllLDAP bllLDAP = new BllLDAP();
            //DalLDAP dalLDAP = new DalLDAP();
            Model.LDAPModel bean = bllLDAP.GetLDAPInfo();


            this.TB_IP.Text = bean.Con_IP;
            this.TB_Port.Text = bean.Con_Port;
            this.TB_Account.Text = bean.Con_Account;
            this.TB_Password.Text = bean.Con_Password;

        }  


    }
    //设定按钮动作
    protected void btnSetting_Click(object sender, EventArgs e)
    {
        test();
        //因为数据库中始终保持一条数据，因此没必要做add操作，只需要更新即可（cui20170707）
        BllLDAP LDAPbll = new BllLDAP();
        Model.LDAPModel model = new Model.LDAPModel();
        model.Con_IP = TB_IP.Text;
        model.Con_Port = TB_Port.Text;
        model.Con_Verification = DropDownList1.Text;
        model.Con_Account = TB_Account.Text;
        model.Con_Password = TB_Password.Text;
        if (!Page.IsValid)
        {
            return;
        }

        LDAPbll.Update_Connection(model);   
            
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
        //test.IDAuthentication(server, username, password);

        if (test.IDAuthentication(server, username, password) == 0)
        {
            label.Text = "认证成功";
            //MessageBox.Show("认证成功");
        }
        else
        {
            label.Text = "认证失败";
            //MessageBox.Show("认证失败");
        }
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        test();
    }
}
   