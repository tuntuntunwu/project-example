using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;

public partial class Settings_LDAPUser : MainPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_LDAP_USER_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_LDAP_USER_SET, "LDAPUser.aspx", false);
        this.Master.JobReportTitle();

        if (!IsPostBack)//初始化页面或控件
        {
            DropDownList1.Items.Clear();
            List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
            BllDropAndPlus bllDrop = new BllDropAndPlus();
            lst = bllDrop.GetDropPlusListByCD(3);
            foreach (DropAndPlusEntry drop in lst)
            {
                DropDownList1.Items.Add(drop.name);
            }

            DropDownList3.Items.Clear();
            List<DropAndPlusEntry> lst3 = new List<DropAndPlusEntry>();
            BllDropAndPlus bllDrop3 = new BllDropAndPlus();
            lst3 = bllDrop3.GetDropPlusListByCD(4);
            foreach (DropAndPlusEntry drop in lst3)
            {
                DropDownList3.Items.Add(drop.name);
            }

            DropDownList4.Items.Clear();
            List<DropAndPlusEntry> lst4 = new List<DropAndPlusEntry>();
            BllDropAndPlus bllDrop4 = new BllDropAndPlus();
            lst4 = bllDrop4.GetDropPlusListByCD(5);
            foreach (DropAndPlusEntry drop in lst4)
            {
                DropDownList4.Items.Add(drop.name);
            }

            DropDownList6.Items.Clear();
            List<DropAndPlusEntry> lst6 = new List<DropAndPlusEntry>();
            BllDropAndPlus bllDrop6 = new BllDropAndPlus();
            lst6 = bllDrop6.GetDropPlusListByCD(6);
            foreach (DropAndPlusEntry drop in lst6)
            {
                DropDownList6.Items.Add(drop.name);
            }

            //下拉菜单          
            //List<string> strList1 = new List<string>();
            //strList1.Add("DC=example,DC=com");
            //DropDownList1.DataSource = strList1;
            //DropDownList1.DataBind();

            //List<string> strList2 = new List<string>();
            //strList2.Add("Base");
            //strList2.Add("One Level");
            //strList2.Add("Subtree");
            //DropDownList2.DataSource = strList2;
            //DropDownList2.DataBind();

            //List<string> strList3 = new List<string>();
            //strList3.Add("displayName");
            //DropDownList3.DataSource = strList3;
            //DropDownList3.DataBind();

            //List<string> strList4 = new List<string>();
            //strList4.Add("mail");
            //DropDownList4.DataSource = strList4;
            //DropDownList4.DataBind();

            //List<string> strList5 = new List<string>();
            
            //DropDownList5.DataSource = strList5;
            //DropDownList5.DataBind();



            BllLDAP bllLDAP = new BllLDAP();
            Model.LDAPModel bean = bllLDAP.GetLDAPInfo();

            this.TB_UserName.Text = bean.User_LDAPName;
            this.TB_UserPassword.Text = bean.User_LDAPPassword;
           

        }  
    }
    protected void btnSetting_Click(object sender, EventArgs e)
    {
        BllLDAP LDAPbll = new BllLDAP();
        Model.LDAPModel model = new Model.LDAPModel();

        model.User_DNSetting = DropDownList1.Text;
        model.User_Search = DropDownList2.Text;
        model.User_Name = DropDownList3.Text;
        model.User_Email = DropDownList4.Text;
        model.User_ICNum = TB_IC.Text;
        model.User_LDAPName = TB_UserName.Text;
        model.User_LDAPPassword = TB_UserPassword.Text;

        if (!Page.IsValid)
        {
            return;
        }

        LDAPbll.Update_User(model); 

    }
    //DN Setting
    protected void ImageBtn_Add1_Click(object sender, ImageClickEventArgs e)
    {
        TB_DN.Visible = true;
        AddDNBtn.Visible = true;
    }

    //搜索默认DN设置
    protected void AddDNBtn_Click(object sender, EventArgs e)
    {
        //List<string> strList1 = new List<string>();
        //string value = TB_DN.Text;
        //strList1.Add(value);
        //DropDownList1.DataSource = strList1;
        //DropDownList1.DataBind();

        //添加时把内容存到数据库表dropdownSetting表中
        BllDropAndPlus DropAndPlusbll = new BllDropAndPlus();
        Model.DropAndPlusModel model = new Model.DropAndPlusModel();

        model.name = TB_DN.Text;
        model.CD = 3;
        DropAndPlusbll.Add3(model);

        DropDownList1.Items.Clear();
        List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
        BllDropAndPlus bllDrop = new BllDropAndPlus();
        lst = bllDrop.GetDropPlusListByCD(3);
        foreach (DropAndPlusEntry drop in lst)
        {
            DropDownList1.Items.Add(drop.name);
        }
        DropDownList1.SelectedIndex = (DropDownList1.Items.Count - 1);



    }
    //Username
    protected void ImageBtn_Add2_Click(object sender, ImageClickEventArgs e)
    {
        TB_Name.Visible = true;
        AddNameBtn.Visible = true;
    }
    protected void AddNameBtn_Click(object sender, EventArgs e)
    {
        //List<string> strList3 = new List<string>();
        //string value = TB_Name.Text;
        //strList3.Add(value);
        //DropDownList3.DataSource = strList3;
        //DropDownList3.DataBind();

        //添加时把内容存到数据库表dropdownSetting表中
        BllDropAndPlus DropAndPlusbll = new BllDropAndPlus();
        Model.DropAndPlusModel model = new Model.DropAndPlusModel();

        model.name = TB_Name.Text;
        model.CD = 4;
        DropAndPlusbll.Add4(model);

        DropDownList3.Items.Clear();
        List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
        BllDropAndPlus bllDrop = new BllDropAndPlus();
        lst = bllDrop.GetDropPlusListByCD(4);
        foreach (DropAndPlusEntry drop in lst)
        {
            DropDownList3.Items.Add(drop.name);
        }
        DropDownList3.SelectedIndex = (DropDownList3.Items.Count - 1);
    }

    //Email
    protected void ImageBtn_Add3_Click(object sender, ImageClickEventArgs e)
    {
        TB_Email.Visible = true;
        AddEmailBtn.Visible = true;
    }
    protected void AddEmailBtn_Click(object sender, EventArgs e)
    {
        //List<string> strList5 = new List<string>();
        //string value = TB_Email.Text;
        //strList5.Add(value);
        //DropDownList4.DataSource = strList5;
        //DropDownList4.DataBind();

        //添加时把内容存到数据库表dropdownSetting表中
        BllDropAndPlus DropAndPlusbll = new BllDropAndPlus();
        Model.DropAndPlusModel model = new Model.DropAndPlusModel();

        model.name = TB_Email.Text;
        model.CD = 5;
        DropAndPlusbll.Add5(model);

        DropDownList4.Items.Clear();
        List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
        BllDropAndPlus bllDrop = new BllDropAndPlus();
        lst = bllDrop.GetDropPlusListByCD(5);
        foreach (DropAndPlusEntry drop in lst)
        {
            DropDownList4.Items.Add(drop.name);
        }
        DropDownList4.SelectedIndex = (DropDownList4.Items.Count - 1);
    }

    //ICCard
    protected void ImageBtn_Add6_Click(object sender, ImageClickEventArgs e)
    {
        TB_IC.Visible = true;
        AddICBtn.Visible = true;
    }
    protected void AddICBtn_Click(object sender, EventArgs e)
    {
        //List<string> strList6 = new List<string>();
        //string value = TB_IC.Text;
        //strList6.Add(value);
        //DropDownList6.DataSource = strList6;
        //DropDownList6.DataBind();

        //添加时把内容存到数据库表dropdownSetting表中
        BllDropAndPlus DropAndPlusbll = new BllDropAndPlus();
        Model.DropAndPlusModel model = new Model.DropAndPlusModel();

        model.name = TB_IC.Text;
        model.CD = 6;
        DropAndPlusbll.Add6(model);

        DropDownList6.Items.Clear();
        List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
        BllDropAndPlus bllDrop = new BllDropAndPlus();
        lst = bllDrop.GetDropPlusListByCD(6);
        foreach (DropAndPlusEntry drop in lst)
        {
            DropDownList6.Items.Add(drop.name);
        }
        DropDownList6.SelectedIndex = (DropDownList6.Items.Count - 1);
    }
}