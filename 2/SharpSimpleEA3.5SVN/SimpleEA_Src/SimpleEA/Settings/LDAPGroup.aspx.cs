using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Diagnostics;
//using System.DirectoryServices;
using dtSettingManagementTableAdapters;
using BLL;
using Model;
using DAL;

using dtRestrictionInfoTableAdapters;
using dtGroupInfoTableAdapters;



public partial class Settings_LDAPGroup : MainPage 
{
    

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_LDAP_GROUP_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_LDAP_GROUP_SET, "LDAPGroup.aspx", false);
        this.Master.JobReportTitle();


        if (!IsPostBack)//初始化页面或控件
        {
            BllLDAP bllLDAP = new BllLDAP();
            Model.LDAPModel bean = bllLDAP.GetLDAPInfo();

            //下拉菜单
            //List<string> strList = new List<string>();
            //strList.Add("None");
            //strList.Add("Anonymous");
            //DropDownList1.DataSource = strList;
            //DropDownList1.DataBind();
            RestrictionInfoTableAdapter RestrictionAdapter = new RestrictionInfoTableAdapter();
            lstRestriction.DataSource = RestrictionAdapter.GetRestrictionInfoData2();
            this.lstRestriction.DataBind();
            this.lstRestriction.SelectedValue = UtilConst.CON_DATE_ID;




            DropDownList2.Items.Clear();
            List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
            BllDropAndPlus bllDrop = new BllDropAndPlus();
            lst = bllDrop.GetDropPlusListByCD(2);
            foreach (DropAndPlusEntry drop in lst)
            {
                DropDownList2.Items.Add(drop.name);
            }
            //DropDownList2.SelectedIndex = (DropDownList2.Items.Count - 1);
        }
    }

    //和认证设置里面相似的问题，已解决（cui201707018）
    List<string> strList2 = new List<string>();
    protected void onAckTypeChanged(object sender, EventArgs e)
    {
        DalDropAndPlus DropAndPlusdal = new DalDropAndPlus();
        BllDropAndPlus DropAndPlusbll = new BllDropAndPlus();
        Model.DropAndPlusModel model = new Model.DropAndPlusModel();
        if (RadioButton1.SelectedItem.Text == "User Attribute")
        {
            DropAndPlusdal.GetNameByCD();                
            strList2.Add("department");
            DropDownList2.DataSource = strList2;
            DropDownList2.DataBind();
        }
        else
        {
           // strList2.Add("");
            strList2.Add("OU");
            DropDownList2.DataSource = strList2;
            DropDownList2.DataBind();
        }
    }

    //当用户需要添加新的属性时，点击此按钮，textbox和button可见
    protected void ImageBtn_Add_Click(object sender, ImageClickEventArgs e)
    {
        TB_AttName.Visible = true;
        AddBtn.Visible = true;

    }

    //把textbox内容添加到dropdownlist的按钮
    protected void AddBtn_Click(object sender, EventArgs e)
    {
        string value = TB_AttName.Text;
        if (RadioButton1.SelectedItem.Text == "User Attribute")
        {
            //strList2.Add(value);
            //DropDownList2.DataSource = strList2;
            //DropDownList2.DataBind();

            //添加时把内容存到数据库表dropdownSetting表中
            BllDropAndPlus DropAndPlusbll = new BllDropAndPlus();
            Model.DropAndPlusModel model = new Model.DropAndPlusModel();

            model.name = TB_AttName.Text;
            model.CD = 2;
            DropAndPlusbll.Add2(model);


            DropDownList2.Items.Clear();
            List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
            BllDropAndPlus bllDrop = new BllDropAndPlus();
            lst = bllDrop.GetDropPlusListByCD(2);
            foreach (DropAndPlusEntry drop in lst)
            {
                DropDownList2.Items.Add(drop.name);
            }
            DropDownList2.SelectedIndex = (DropDownList2.Items.Count -1);
        }
        else
        {
            strList2.Add(value);
            DropDownList2.DataSource = strList2;
            DropDownList2.DataBind();
        }
        ////添加时把内容存到数据库表dropdownSetting表中
        //BllDropAndPlus DropAndPlusbll = new BllDropAndPlus();
        //Model.DropAndPlusModel model = new Model.DropAndPlusModel();
        
        //model.name = TB_AttName.Text;
        //model.CD = 2;
        //DropAndPlusbll.Add2(model);

    }

    protected void btnSetting_Click(object sender, EventArgs e)
    {
        
        BllLDAP LDAPbll = new BllLDAP();
        Model.LDAPModel model = new Model.LDAPModel();
        model.Group_Verification = lstRestriction.SelectedValue;
        model.Group_UserAttribute_or_DN = RadioButton1.Text;
        model.Group_AttributeName = DropDownList2.Text;
                
       
        if (!Page.IsValid)
        {
            return;
        }

        LDAPbll.Update_Group(model);   
            
     }

    #region btnRefreshRestrictionSet_click
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
    #endregion
    #region "btnItemCount_Click"
    /// <summary>
    /// btnSetRestrictionName_click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2014。04.24</Date>
    /// <Author>MJ tan</Author>
    /// <Version>0.01</Version>
    protected void btnSetRestrictionName_click(object sender, EventArgs e)
    {
        string strScript = "<script language='javascript' type='text/javascript'>";
        strScript = strScript + "window.open('../RestrictionInfo/popRestrictInfoAdd.aspx', null, null)";
        strScript = strScript + "</script>";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "popWindowJob", strScript);
    }
    #endregion
}