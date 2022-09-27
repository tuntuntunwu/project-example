using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BLL;
using DAL;
using Model;



public partial class Settings_LDAPVerification : MainPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_LDAP_VERIFICATION_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_LDAP_VERIFICATION_SET, "LDAPVerification.aspx", false);
        this.Master.JobReportTitle();

        if (!IsPostBack)//初始化页面或控件
        {
            //下拉菜单
            List<string> strList = new List<string>();
            strList.Add("sAMAccountName");
            strList.Add("uid");
            DropDownList1.DataSource = strList;
            DropDownList1.DataBind();

            BllLDAP bllLDAP = new BllLDAP();                       
            Model.LDAPModel bean = bllLDAP.GetLDAPInfo();

            //this.TB_LoginType.Text = bean.Ver_Login;         
            DropDownList3.Items.Clear();
            List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
            BllDropAndPlus bllDrop = new BllDropAndPlus();
            lst = bllDrop.GetDropPlusListByCD(1);
            foreach (DropAndPlusEntry drop in lst)
            {
                DropDownList3.Items.Add(drop.name);
            }

        }


    }

    //把textbox内容添加到dropdownlist的按钮
    List<string> strList3 = new List<string>();
    protected void AddBtn_Click(object sender, EventArgs e)
    {
       //List<string> strList2 = new List<string>();
        //string value = TB_LoginType.Text;
        //strList3.Add(value);
        //DropDownList3.DataSource = strList3;
        //DropDownList3.DataBind();

        //添加时把内容存到数据库表dropdownSetting表中
        BllDropAndPlus DropAndPlusbll = new BllDropAndPlus();
        Model.DropAndPlusModel model = new Model.DropAndPlusModel();

        model.name = TB_LoginType.Text;
        model.CD = 1;
        DropAndPlusbll.Add1(model);

        DropDownList3.Items.Clear();
        List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
        BllDropAndPlus bllDrop = new BllDropAndPlus();
        lst = bllDrop.GetDropPlusListByCD(1);
        foreach (DropAndPlusEntry drop in lst)
        {
            DropDownList3.Items.Add(drop.name);
        }
        DropDownList3.SelectedIndex = (DropDownList3.Items.Count - 1);


    }

    //当dropDrowList2为sAMAccountName时，下面的textbox不可编辑，是其他的时候，可编辑(已实现cui20170718)
   
    protected void myListDropDown_Change(object sender, EventArgs e)
    {
        //stuff that never gets hit
        
        if (DropDownList2.SelectedValue == "sAMAccountName")
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

    protected void btnSetting_Click(object sender, EventArgs e)
    {
        BllLDAP LDAPbll = new BllLDAP();
        Model.LDAPModel model = new Model.LDAPModel();
        model.Ver_Verification = DropDownList1.Text;
        model.Ver_Login = TB_LoginType.Text;
        model.Ver_Type = DropDownList2.Text;
        if (DropDownList2.SelectedValue == "sAMAccountName")
        {
            model.Ver_NTorDNS = "";
        }
        else 
        {
            model.Ver_NTorDNS = TB_NTorDNS.Text;
        }
       // model.Ver_NTorDNS = TB_NTorDNS.Text;
       
        if (!Page.IsValid)
        {
            return;
        }

        LDAPbll.Update_Verification(model);   
            
     }

    //当用户需要添加新的属性时，点击此按钮，textbox和button可见
    protected void ImageBtn_Add_Click(object sender, ImageClickEventArgs e)
    {
        TB_LoginType.Visible = true;
        AddBtn.Visible = true;
    }
}
