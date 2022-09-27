using System.Collections.Generic;
//using System.Linq;
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Odbc;
using dtSettingManagementTableAdapters;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
//using Microsoft.VisualBasic.FileIO.TextFieldParser;
using BLL;
using Model;
using common;
public partial class Settings_LDAPSynchronization : MainPage
{
    public int ChosenMsgNum=2;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.Title = UtilConst.CON_PAGE_LDAP_SYNC_SET;
        this.Master.SubTitle(UtilConst.CON_PAGE_LDAP_SYNC_SET, "LDAPSynchronization.aspx", false);
        this.Master.JobReportTitle();

        if (!IsPostBack)
        {           
            

            BllLDAP bllLDAP = new BllLDAP();
            //DalLDAP dalLDAP = new DalLDAP();
            Model.LDAPModel bean = bllLDAP.GetLDAPInfo();

            this.TB_Feedback.Text = bean.Syn_label;
        }
    }

    
    //private void Load_Once()
    //{
    //    // each Month
    //    ddlMonth.Items.Clear();

    //    // 5
    //    ListItem item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME5_NAME, UtilConst.SETPERIODTIME_MONTH_ITME5.ToString());
    //    ddlMonth.Items.Add(item);
    //    // 10
    //    item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME10_NAME, UtilConst.SETPERIODTIME_MONTH_ITME10.ToString());
    //    ddlMonth.Items.Add(item);
    //    // 15
    //    item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME15_NAME, UtilConst.SETPERIODTIME_MONTH_ITME15.ToString());
    //    ddlMonth.Items.Add(item);
    //    // 20
    //    item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME20_NAME, UtilConst.SETPERIODTIME_MONTH_ITME20.ToString());
    //    ddlMonth.Items.Add(item);
    //    // 25
    //    item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITME25_NAME, UtilConst.SETPERIODTIME_MONTH_ITME25.ToString());
    //    ddlMonth.Items.Add(item);
    //    // last
    //    item = new ListItem(UtilConst.SETPERIODTIME_MONTH_ITMELAST_NAME, UtilConst.SETPERIODTIME_MONTH_ITMELAST.ToString());
    //    ddlMonth.Items.Add(item);

    //    //each Week
    //    ddlWeek.Items.Clear();

    //    item = new ListItem(UtilConst.SETPERIODTIME_WEEK_MON_NAME, UtilConst.SETPERIODTIME_WEEK_MON.ToString());
    //    ddlWeek.Items.Add(item);
    //    item = new ListItem(UtilConst.SETPERIODTIME_WEEK_TUE_NAME, UtilConst.SETPERIODTIME_WEEK_TUE.ToString());
    //    ddlWeek.Items.Add(item);
    //    item = new ListItem(UtilConst.SETPERIODTIME_WEEK_WED_NAME, UtilConst.SETPERIODTIME_WEEK_WED.ToString());
    //    ddlWeek.Items.Add(item);
    //    item = new ListItem(UtilConst.SETPERIODTIME_WEEK_THU_NAME, UtilConst.SETPERIODTIME_WEEK_THU.ToString());
    //    ddlWeek.Items.Add(item);
    //    item = new ListItem(UtilConst.SETPERIODTIME_WEEK_FRI_NAME, UtilConst.SETPERIODTIME_WEEK_FRI.ToString());
    //    ddlWeek.Items.Add(item);
    //    item = new ListItem(UtilConst.SETPERIODTIME_WEEK_SAT_NAME, UtilConst.SETPERIODTIME_WEEK_SAT.ToString());
    //    ddlWeek.Items.Add(item);
    //    item = new ListItem(UtilConst.SETPERIODTIME_WEEK_SUN_NAME, UtilConst.SETPERIODTIME_WEEK_SUN.ToString());
    //    ddlWeek.Items.Add(item);

    //    //each hour
    //    ddlHour.Items.Clear();

    //    int i = 1;
    //    item = new ListItem("凌晨一点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("凌晨二点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("凌晨三点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("凌晨四点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("早上五点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("早上六点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("早上七点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("上午八点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("上午九点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("上午十点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("上午十一点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("正午十二点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("下午一点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("下午二点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("下午三点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("下午四点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("下午五点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("下午六点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("晚上七点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("晚上八点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("晚上九点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("晚上十点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("深夜十一点", i.ToString());
    //    ddlHour.Items.Add(item);
    //    i = i + 1;
    //    item = new ListItem("深夜十二点", i.ToString());
    //    ddlHour.Items.Add(item);

    //    // Default Value
    //    // Get Setting Information from settingmanagement.
    //    SettingManagementTableAdapter settingAdapter = new SettingManagementTableAdapter();
    //    dtSettingManagement.SettingManagementDataTable settingtableset = new dtSettingManagement.SettingManagementDataTable();
    //    settingtableset = settingAdapter.GetData();

    //    rdoMonth.Checked = true;
    //    ddlMonth.SelectedValue = UtilConst.SETPERIODTIME_MONTH_ITMELAST.ToString();
    //    ddlWeek.SelectedValue = UtilConst.SETPERIODTIME_WEEK_SAT.ToString();
    //    ddlHour.SelectedValue = "24";

    //    if (settingtableset.Rows.Count > 0)
    //    {

    //        rdoMonth.Checked = false;
    //        rdoWeek.Checked = false;
    //        rdoDay.Checked = false;

    //        int intSetPeriod = settingtableset[0].SetPeriod;
    //        int intSetPeriodTime = settingtableset[0].SetPeriodTime;

    //        if (intSetPeriod.Equals(UtilConst.SETPERIOD_MONTH))
    //        {
    //            // each month
    //            rdoMonth.Checked = true;
    //            ddlMonth.SelectedValue = intSetPeriodTime.ToString();
    //        }
    //        else if (intSetPeriod.Equals(UtilConst.SETPERIOD_WEEK))
    //        {
    //            // each week
    //            rdoWeek.Checked = true;
    //            ddlWeek.SelectedValue = intSetPeriodTime.ToString();
    //        }
    //        else if (intSetPeriod.Equals(UtilConst.SETPERIOD_DAY))
    //        {
    //            // each day
    //            rdoDay.Checked = true;
    //            ddlWeek.SelectedValue = UtilConst.SETPERIODTIME_WEEK_SAT.ToString();
    //        }
    //        // 2014.04.28 Add By SES chen youguang Ver.1.1 Update ST
    //        else if (intSetPeriod.Equals(UtilConst.SETPERIOD_UNLIMIT))
    //        {
    //            rdoNoLimit.Checked = true;
    //        }
    //        // 2014.04.28 Add By SES chen youguang Ver.1.1 Update ED


    //        ddlHour.SelectedValue = settingtableset[0].SetTime.ToString();
    //    }


    //    dtSettingDisp.SettingDispRow settingDispRow = UtilCommon.GetDispSetting();
    //    int Dis_U_CardID = settingDispRow.Dis_U_CardID;
    //    int Dis_U_Restrict = settingDispRow.Dis_U_Restrict;
    //    int Dis_G_Restrict = settingDispRow.Dis_G_Restrict;
    //    int Dis_R_Copy = settingDispRow.Dis_R_Copy;
    //    int Dis_R_Print = settingDispRow.Dis_R_Print;
    //    int Dis_R_Scan = settingDispRow.Dis_R_Scan;
    //    int Dis_R_Fax = settingDispRow.Dis_R_Fax;
    //    int Dis_Job_BWTotal = settingDispRow.Dis_Job_ScanTotal;
    //    int Dis_Job_FCTotal = settingDispRow.Dis_Job_FaxTotal;
    //    int Dis_Result_Copy = settingDispRow.Dis_Result_Copy;
    //    int Dis_Result_Print = settingDispRow.Dis_Result_Print;
    //    int Dis_Result_Scan = settingDispRow.Dis_Result_Scan;
    //    int Dis_Result_Fax = settingDispRow.Dis_Result_Fax;
    //    int Dis_Result_Other = settingDispRow.Dis_Result_Other;
      


    //}
   
    //设定按钮
    protected void btnSetting_Click(object sender, EventArgs e)
    {
        BllLDAP LDAPbll = new BllLDAP();
        Model.LDAPModel model = new Model.LDAPModel();
        
        model.Syn_label = TB_Feedback.Text;

        if (!Page.IsValid)
        {
            return;
        }

        LDAPbll.Update_Syn(model);  
    }

    ////查找域服务器中用户信息（函数）已经封装到common下LDAPHandler下面了
    //private DataTable GetADUsers(String domainName, String adAdmin, String password, String ouName, String ouName1)
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("sAMAccountName");//帐号
    //    dt.Columns.Add("Name");//姓名
    //    dt.Columns.Add("mail"); //邮箱地址
    //    dt.Columns.Add("phoneNumber"); //邮箱地址
    //    dt.Columns.Add("department");  //部门
    //    dt.Columns.Add("memberOf");  //隶属于

    //    DirectoryEntry adRoot = new DirectoryEntry("LDAP://" + domainName, adAdmin, password, AuthenticationTypes.None);
    //    DirectoryEntry ou = adRoot.Children.Find("OU=" + ouName);
    //    DirectoryEntry ou1 = ou.Children.Find("OU=" + ouName1);

    //    DirectorySearcher mySearcher = new DirectorySearcher(ou1);
    //    //DirectorySearcher mySearcher = new DirectorySearcher(adRoot);
    //    mySearcher.Filter = ("(objectClass=user)"); //user表示用户，group表示组

    //    try
    //    {
    //        foreach (System.DirectoryServices.SearchResult resEnt in mySearcher.FindAll())
    //        {
    //            DataRow dr = dt.NewRow();
    //            dr["sAMAccountName"] = string.Empty;
    //            dr["Name"] = string.Empty;
    //            dr["mail"] = string.Empty;
    //            //dr["ou"] = string.Empty;
    //            dr["department"] = string.Empty;
    //            dr["memberOf"] = string.Empty;
    //            dr["phoneNumber"] = string.Empty;

    //            DirectoryEntry user = resEnt.GetDirectoryEntry();
    //            if (user.Properties.Contains("sAMAccountName"))
    //            {
    //                dr["sAMAccountName"] = user.Properties["sAMAccountName"][0].ToString();
    //            }
    //            if (user.Properties.Contains("Name"))
    //            {
    //                dr["Name"] = user.Properties["Name"][0].ToString();
    //            }
    //            if (user.Properties.Contains("TelephoneNumber"))
    //            {
    //                dr["phoneNumber"] = user.Properties["telephoneNumber"][0].ToString();
    //            }
    //            if (user.Properties.Contains("mail"))
    //            {
    //                dr["mail"] = user.Properties["mail"][0].ToString();
    //            }
    //            if (user.Properties.Contains("department"))
    //            {
    //                dr["department"] = user.Properties["department"][0].ToString();
    //            }
    //            if (user.Properties.Contains("company"))
    //            {
    //                dr["memberOf"] = user.Properties["company"][0].ToString();
    //            }
    //            dt.Rows.Add(dr);
    //        }

    //    }
    //    catch (System.Runtime.InteropServices.COMException e)
    //    {
    //        if (e.ErrorCode == -2147016646)
    //        {
    //            MessageBox.Show("连接服务器失败");
    //        }
    //        else if (e.ErrorCode == -2147023570)
    //        {
    //            MessageBox.Show("用户名或密码错误");
    //        }
    //        MessageBox.Show(e.ToString());
    //        MessageBox.Show(e.ErrorCode.ToString());
    //    }
    //    return dt;

    //}

    //立即同步按钮
    //cui20170716
    /*根据读LDAPSetting表中组设置的Group Attribute Name的值如department得到groupname，
     * 如果groupInfo中有，不用添加，根据groupname获取ID即可，如果没有，就添加，同时获取ID，
     * 把GroupInfo中的ID对应写到UserInfo的groupID中。
     * 配额方案RestrictionID默认设置为-1（暂时固定下来）。
     * 
     * 其他一些UserInfo的属性根据LDAPSetting的属性对应着写，实现动态控制。
     * 
     */



    protected void btnSyncnow_Click(object sender, EventArgs e)
    {
   
        LDAPHandler ldap = new LDAPHandler();
        int update_count = ldap.ImportSyncLDAP();
        TB_Feedback.Text = "反馈结果：此次同步共更新了" + update_count.ToString() + "条信息。";
    }
    
    //上传ICCard文件
    protected void Button4_Click(object sender, EventArgs e)
    {
        BllICCard bllICCard = new BllICCard();
        UserModel[] beanitem;
        string uploadName = this.FileUpload1.FileName;
        string CsvName = "";
        string path = "";
        try{
            if (FileUpload1.HasFile)
            {
                string Extension = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToString().ToLower();
                if (Extension != ".csv")
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "<script>alert('您导入的文件格式不正确，请确认后重试！');</script>");
                    return;
                }
                CsvName = uploadName;
                path = Request.PhysicalApplicationPath.ToString();
                path += "Img\\";
                path += uploadName;

                //将文件保存到服务器上OK
                FileUpload1.SaveAs(path);
                ImportData1 ExcelData = new ImportData1();
                //读取cvs里面的数据
                beanitem = ExcelData.ImportExcel11(path);
                //把读取的csv的数据放在数据库表UserInfo中（LoginName和ICCardID）
                int ret = bllICCard.ImportData(beanitem);

                if (ret == 1)
                {
                    label.Text = "导入成功!";
                    //MessageBox.Show("导入成功");
                    if (System.IO.File.Exists(path))//先判断文件是否存在，再执行操作
                    {
                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    label.Text = "导入失败!";
                    //MessageBox.Show("导入失败");
                }
            }
        }catch (Exception ex)
        {
            throw ex;
        }
    }
    //下载函数
    public static void DownloadFile(HttpResponse argResp, StringBuilder argFileStream, string strFileName)
    {
        try
        {
            string strResHeader = "attachment; filename=" + Guid.NewGuid().ToString() + ".csv";
            if (!string.IsNullOrEmpty(strFileName))
            {
                strResHeader = "inline; filename=" + strFileName;
            }
            argResp.AppendHeader("Content-Disposition", strResHeader);//attachment说明以附件下载，inline说明在线打开
            argResp.ContentType = "application/ms-excel";
            argResp.ContentEncoding = Encoding.GetEncoding("GB2312"); // Encoding.UTF8;//
            argResp.Write(argFileStream);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private StringBuilder AppendCSVFields(StringBuilder argSource, string argFields)
    {
        return argSource.Append(argFields.Replace(",", " ").Trim()).Append(",");
    }
      

    public DataTable msg(List<UserEntry> boi)
    {
        Dictionary<int, List<UserEntry>> myselfList1;
        int boinum;
        boinum = boi.Count;//获取表格的行数
        //获取列名
        string[] titlelist;//将获取的列名保存在该数组中
        string titleString = "LoginName,ICCardID,";
        titlelist = (titleString).Split(',');
        if (boi.Count > 0)
        {
            myselfList1 = new Dictionary<int, List<UserEntry>>();
            List<UserEntry> lst = new List<UserEntry>();
            BllICCard bllICCard = new BllICCard();
            lst = bllICCard.GetUserEntryByLoginName();
            DataTable dt = new DataTable();
            for(int i=0;i<2;i++){
                DataColumn newcolumn = new DataColumn(i.ToString(), typeof(string));
                dt.Columns.Add(newcolumn);
            }
            //把表头加到datatable中
            DataRow row1 = dt.NewRow();
            row1[0] = titlelist[0];
            row1[1] = titlelist[1];
            dt.Rows.Add(row1);
            foreach (UserEntry drop in lst)
            {                
                DataRow row = dt.NewRow();             
                row[0] = drop.LoginName;
                row[1] = drop.ICCardID;;
                dt.Rows.Add(row);
            }
            return dt;
        }
     
        return null;
    }

    
    //导出ICCard的信息（包括LoginName和ICCardID）
    protected void download_Click(object sender, EventArgs e)
    {
        List<UserEntry> lst = new List<UserEntry>();
        BllICCard bllICCard = new BllICCard();

        lst = bllICCard.GetUserEntryByLoginName();
       

        DataTable dt = msg(lst);

        if (dt != null)
        {
            try
            {
                StringWriter swCSV = new StringWriter();

                //遍历datatable导出数据
                foreach (DataRow drTemp in dt.Rows)
                {
                    StringBuilder sbText = new StringBuilder();
                    for (int i = 0; i < ChosenMsgNum; i++)
                    {
                        sbText = AppendCSVFields(sbText, drTemp[i].ToString());
                    }
                    if (!sbText.Equals("")) {
                        //去掉尾部的逗号
                        sbText.Remove(sbText.Length - 1, 1);
                        //写datatable的一行
                        swCSV.WriteLine(sbText.ToString());
                    }
                   
                }
                //下载文件
                DownloadFile(Response, swCSV.GetStringBuilder(), "SampleICCard.csv");

                swCSV.Close();
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            label.Text = "导出失败";
            //MessageBox.Show("导出失败！");
        }

    }
}


   
