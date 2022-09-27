#region Copyright SHARP Corporation
//	Copyright (c) 2010 SHARP CORPORATION. All rights reserved.
//
//	SHARP Simple EA Application
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion
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
using System.Data.SqlClient;
using MFPOSAautosubmit;
using System.Net;
using System.Net.NetworkInformation;
using dtServerIPSettingTableAdapters;
using SnmpSharpNet;
/// <summary>
/// List MFP info
/// </summary>
/// <Date>2012.03.09</Date>
/// <Author>SLC Zheng wei</Author>
public partial class MfpInfo_MFPRestrictionList : ListMainPage
{
    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {

        this.Master.Title = UtilConst.CON_PAGE_MFP;

        this.Master.CheakSearchItem = true;
        // Check Access Role
        CheckUser();

        //Set SqlDataSource
        SqlDataListSource.ConnectionString = this.DBConnectionStrings;

        string strSql = null;
        if (!(ViewState["SearchKeyWord"] == null || string.IsNullOrEmpty(ViewState["SearchKeyWord"].ToString())))
        {
            strSql = ViewState["SearchKeyWord"].ToString();
        }
        else
        {
 
            strSql = "   SELECT  A.ModelName   AS    ModelName    " + Environment.NewLine +
                     "  ,A.IPAddress        AS    IPAddress" + Environment.NewLine +
                     "  ,A.Location      AS    Location" + Environment.NewLine +
                     " ,A.SerialNumber               AS SerialNumber" + Environment.NewLine +
                     "FROM  MFPInformation A" + Environment.NewLine +
            "WHERE 1= 1" + Environment.NewLine;

            strSql = strSql + "ORDER BY  SerialNumber " + Environment.NewLine;
        }
        this.CustomersGridView.DataSource = null;
        this.CustomersGridView.DataSourceID = SqlDataListSource.ID;


        SqlDataListSource.SelectCommand = strSql;
        SetListMainPgae(this.CustomersGridView,
            this.ddlNumPerPage,
            this.ddlIndexOfPage,
            this.lblTotalPage,
            this.btnSelectAll, "ModelName,IPAddress,Location,SerialNumber");

        if (!IsPostBack)
        {
            if (this.CustomersGridView.Rows.Count > 0)
            {
                //Function:Set the Width of GridView for Group Management.
                SetGridViewWidth();
                this.CustomersGridView.Sort("ModelName", SortDirection.Ascending);
            }

            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem("0", "MFP型号"));
            this.Master.ddl_SearchList().Items.Add(this.ComBoxListItem("1", "MFP IP地址"));

        }

        this.Master.btn_Search().Click += new EventHandler(btn_SearchClick);
        btnDeleteMFP.OnClientClick += ConfirmFunction(UtilConst.MSG_MAN_DEL_CONFIRM);
    }
    #endregion

    #region "Function:Set the Width of GridView for Group Management"
    /// <summary>
    /// Function:Set the Width of GridView for Group Management.
    /// </summary>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>1.1</Version>
    public void SetGridViewWidth()
    {
        dtSettingDisp.SettingDispRow row = UtilCommon.GetDispSetting();
        // Restrict is DIVISIBLE
        if (row.Dis_G_Restrict == 0)
        {
            CustomersGridView.Columns[2].HeaderStyle.Width = new Unit(UtilConst.CSS_G_GROUPAME_WIDTH);
            CustomersGridView.Columns[5].Visible = false;
            CustomersGridView.Columns[6].HeaderStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
            CustomersGridView.Columns[6].ItemStyle.CssClass = UtilConst.CSS_ITEM_DISPLAY;
        }

    }
    #endregion

    #region "Function:Raises the GridView.Sorted event."
    /// <summary>
    /// Function:Raises the GridView.Sorted event.
    /// It'll be overrided In each page.
    /// </summary>
    /// <param name="gv"></param>
    /// <seealso cref="SortGridView"/>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>0.01</Version>
    public override void SortGridView(GridView cView)
    {
        // Sort ▲▼
        string sortString = "";
        int index = 0;

        cView.Columns[0].HeaderText = "";
        cView.Columns[2].HeaderText = "MFP型号";
        cView.Columns[4].HeaderText = "MFP IP地址";
        cView.Columns[6].HeaderText = "MFP序列号";
        cView.Columns[8].HeaderText = "描述";

        if (cView.SortExpression.Equals(""))
        {
            return;
        }

        if ("ModelName".Equals(cView.SortExpression))
        {
            sortString = "MFP型号";
            index = 2;
        }

        if ("IPAddress".Equals(cView.SortExpression))
        {
            sortString = "MFP IP地址";
            index = 4;
        }
        if ("SerialNumber".Equals(cView.SortExpression))
        {
            sortString = "MFP序列号";
            index = 6;

        }
        if ("Location".Equals(cView.SortExpression))
        {
            sortString = "描述";
            index = 8;

        }
        //2010.11.17 Add By SES zhoumiao Ver.1.1 Update ED

        if (SortDirection.Ascending.Equals(cView.SortDirection))
        {
            sortString = UtilConst.CON_ITEM_SORT_ASC + sortString;
        }
        else
        {
            sortString = UtilConst.CON_ITEM_SORT_DESC + sortString;
        }

        cView.Columns[index].HeaderText = sortString;
    }
    #endregion

    #region "Occurs when a data row is bound to data in GridView."
    /// <summary>
    /// Occurs when a data row is bound to data in GridView.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>0.01</Version>
    protected void CustomView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Check Row Records
        // Defulat Date Can't be Delete
        // User's Admin
        // Group's no belong
        GridView gridView = (GridView)sender;
        GridViewRow gRow = (GridViewRow)e.Row;
        string strId = "";

        if (e.Row.RowType.Equals(DataControlRowType.DataRow))
        {
            CheckBox cbox = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
            strId = ((DataRowView)e.Row.DataItem).Row["id"].ToString();
            if (strId.Equals(UtilConst.CON_DATE_ID))
            {
                cbox.Enabled = false;
            }
            else
            {
                cbox.Enabled = true;
            }

            if (cbox.Checked)
            {
                e.Row.CssClass = "SelectedTR";
            }
            else
            {
                e.Row.CssClass = "UnselectedTR";
            }
        }
    }
    #endregion

    #region "btnSearch Clicked event"
    /// <summary>
    /// btnSearch Clicked  event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>1.1</Version>
    protected void btn_SearchClick(object sender, EventArgs e)
    {
        SqlDataListSource.SelectCommand = SearchSql();

        CustomersGridView.DataBind();
        this.ddlNumPerPage.SelectedIndex = 0;
        // Set the PageIndex property to display that page selected by the user.
        this.CustomGridView.PageSize = int.Parse(this.CustomNumPerPage.SelectedValue.ToString());

        SetListMainPgae(this.CustomersGridView,
             this.ddlNumPerPage,
             this.ddlIndexOfPage,
             this.lblTotalPage,
             this.btnSelectAll,"ModelName,IPAddress,Location,SerialNumber");

        this.CustomersGridView.Sort("ModelName", SortDirection.Ascending);
        if (CustomersGridView.Rows.Count == 0)
        {
            // no date for Search process.
            ErrorAlert(UtilConst.MSG_NOTHING_SEARCH);
        }

    }
    #endregion

    #region"Function: March the Group Name with Search Words and displayed in  GroupList management"
    /// <summary>
    /// Function: March the Group Name with Search Words and displayed in  GroupList management
    /// </summary>
    /// <returns>Sql</returns>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>1.1</Version>
    private string SearchSql()
   
    {
        string strSql;
          strSql = "   SELECT  A.ModelName   AS    ModelName    " + Environment.NewLine +
                     "  ,A.IPAddress        AS    IPAddress" + Environment.NewLine +
                     "  ,A.Location      AS    Location" + Environment.NewLine +
                     " ,A.SerialNumber               AS SerialNumber" + Environment.NewLine +
                     "FROM  MFPInformation A" + Environment.NewLine +
            "WHERE 1= 1" + Environment.NewLine;

       if (!(string.IsNullOrEmpty(this.Master.txt_UserName().Text.Trim())))
       {
           if ("0".Equals(this.Master.ddl_SearchList().SelectedValue))
           {
               strSql += " AND ModelName LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
           }
           else
           {
               strSql += " AND IPAddress LIKE " + ConvertStringToSQL("%" + this.Master.txt_UserName().Text.Trim() + "%");
           }
       }

       strSql = strSql + "ORDER BY  SerialNumber " + Environment.NewLine;
       strSql = string.Format(strSql);

        ViewState["SearchKeyWord"] = strSql;
        return strSql;
    }
    #endregion

    #region Delete MFP
    /// <summary>
    /// Delete MFP
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    protected void btnDeleteMFP_Click(object sender, EventArgs e)
    {
        try
        {
            string IPAddress = "";
            // the flg for the delete process
            bool process = false;


            foreach (GridViewRow gRow in CustomersGridView.Rows)
            {
                CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                if (ch.Checked)
                {

                    // Set the flg to true.
                    process = true;

                    break;
                }
            }

            //If Delete Group is nothing,then return
            if (process)
            {

                using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
                {
                    con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    try
                    {


                        // 2,Insert User's Res To MFPUserRes

                        foreach (GridViewRow gRow in CustomersGridView.Rows)
                        {
                            CheckBox ch = (CheckBox)gRow.FindControl(UtilConst.CON_ITEM_DETAIL_CHECKBOX);
                            if (ch.Checked)
                            {
                                IPAddress = gRow.Cells[gRow.Cells.Count - 1].Text;
                                // 1,Check the User's Res Exist
                                string sql = " DELETE FROM MFPInformation WHERE IPAddress = '{0}'";
                                sql = string.Format(sql, IPAddress);
                                SqlCommand cmdSelect = new SqlCommand(sql, con, tran);
                                object result = cmdSelect.ExecuteScalar();
                                cmdSelect.Dispose();

                                if (result == null || result.Equals(0))
                                {

                                    // 2,Insert User's Res Infromation
                                    sql = " DELETE FROM MFPUserRes WHERE IPAddress = '{0}'";
                                    sql = string.Format(sql, IPAddress);

                                    SqlCommand cmd = new SqlCommand(sql, con, tran);
                                    cmd.ExecuteNonQuery();
                                    cmd.Dispose();
                                }
                            }
                        }


                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (tran.Connection != null)
                        {
                            tran.Rollback();
                        }
                        throw ex;
                    }
                    finally
                    {
                        tran.Dispose();
                        tran = null;
                    }
                }
            }

            CustomersGridView.DataBind();
            // check with the flg,and alert message.
            if (process)
            {
                // success in delete record.
                SuccessMessage(UtilConst.MSG_MAN_DEL_MFPRES);
            }
            else
            {
                // no date for delete process.
                ErrorAlert(UtilConst.MSG_MAN_NOTHING_MFPRES);
            }
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }
    }
    #endregion

    #region Add New MFP
    /// <summary>
    /// Add New MFP
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    protected void btnAddNewGrp_Click(object sender, EventArgs e)
    {

        //chen 2017/06/05 comment for test start
        // 可控台数
        //SLCRegister.RegisterHandler.Initiate("A");
        //string path = "";
        //int count = SLCRegister.RegisterHandler.GetOperateCount(out path);
        //dtMFPInformationTableAdapters.MFPInformationTableAdapter adpter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();
        //int i = (int)adpter.GetCount();

        //if (i >= count)
        //{
        //   // ErrorAlert("已授权的MFP数量已经超过上限，无法继续添加！"+path);
        //    ErrorAlert("已授权的MFP数量已经超过上限，无法继续添加！");
        //    return;
        //}
        //chen 2017/06/05 comment for test end

        this.Response.Redirect("AddMFPRestriction.aspx");
    }
    #endregion

    #region setting MFP 
    /// <summary>
    /// Setting MFP
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2014.04.23</Date>
    /// <Author>SLC chen</Author>
    protected void btnMachineSettingClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string SerialNumber = btn.CommandArgument;


        PageSubmit("1", SerialNumber);
    }

    //页面提交处理
    private void PageSubmit(string strEnableOrDisable, string SerialNumber)
    {
        #region 变量定义
        //变量定义
        string strMFPip = "";
        string strLoginid = "";
        string strPassword = "";

        string strEAname = "";
        string strEAUIaddr = "";
        string strEASERVICESaddr = "";
        #endregion

        #region 变量赋值

        string serialNumber = SerialNumber;
        dtMFPInformationTableAdapters.MFPInformationTableAdapter MfpAdapter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();
        dtMFPInformation.MFPInformationDataTable table = MfpAdapter.GetDataBySerialNumber(serialNumber);
        //复合机IP地址
        strMFPip = table[0].IPAddress;
        //登录名
        strLoginid = table[0].AdministratorID.ToString();
        //密码
        strPassword = table[0].Password.ToString();


        string strServerIP = "";
        dtServerIPSettingTableAdapters.SettinServerIPTableAdapter adpter = new SettinServerIPTableAdapter();
        dtServerIPSetting.SettinServerIPDataTable dt = adpter.GetData();
        if (dt != null && dt.Count > 0)
        {
            strServerIP = dt[0].IPAddress;
        }
        if (strServerIP.Trim() == "")
        {
            //IPHostEntry ipHostInfo = Dns.Resolve(Request.Url.Host);
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Request.Url.Host);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            //chen add
            for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
            {
                ipAddress = ipHostInfo.AddressList[i];
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    break;
                }
            }
            //
            if (ipAddress.ToString().Equals("127.0.0.1"))
            {
                //ipHostInfo = Dns.Resolve(Dns.GetHostName());
                ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                ipAddress = ipHostInfo.AddressList[0];
                //chen add
                for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
                {
                    ipAddress = ipHostInfo.AddressList[i];
                    if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        break;
                    }
                }
                //
            }
            strServerIP = ipAddress.ToString();
        }
        ////应用程序名称
        //strEAname = ConfigurationManager.AppSettings["AppName"].ToString();
        ////应用程序UI地址
        //strEAUIaddr = ConfigurationManager.AppSettings["AppUIAddress"].ToString();
        ////网页服务地址
        //strEASERVICESaddr = ConfigurationManager.AppSettings["AppServAddress"].ToString();

        //应用程序名称
        strEAname = "SimpleEA"; 
        //应用程序UI地址
        strEAUIaddr = "http://" + strServerIP + "/" + UtilConst.CON_APP_NAME + "/" + UtilConst.CON_APP_UI_ADDR;
        //网页服务地址
        strEASERVICESaddr = "http://" + strServerIP + "/" + UtilConst.CON_APP_NAME + "/" + UtilConst.CON_APP_SERVICE_ADDR;

        if (strMFPip == "")
        {
           // untCommon.InfoMsg("IP不能为空，请确认！");
            return;
        }
        if (strLoginid == "")
        {
            //untCommon.InfoMsg("管理员不能为空，请确认！");
            return ;
        }
        if (strPassword == "")
        {
            //untCommon.InfoMsg("密码不能为空，请确认！");
            return ;
        }
        if (MFPonlineCheck(strMFPip) == false)
        {
            //untCommon.InfoMsg("复合机未开启或未联网，请确认！");
            //Response.Write("<script language='javascript'>alert('复合机未开启或未联网，请确认！')</script>");
            ErrorAlert("复合机未开启或未联网，请确认！");
            return;
        }

        #endregion

        #region 处理
        //处理
        try
        {
            bool boolMFPOsaPageSubmit = MfpAutoSubmit.AutoSubmitProcess(strMFPip, strLoginid, strPassword, strEAname, strEAUIaddr, strEASERVICESaddr, strEnableOrDisable);
            if (boolMFPOsaPageSubmit)
            {
                //2014-6-22 pupeng
                SuccessMessage("复合机配置成功!");
            }
            else
            {
                //2014-6-22 pupeng
                ErrorAlert("复合机配置失败!");
            }
        }
        catch (Exception exPageSubmit)
        {
            exPageSubmit.ToString();
        }
        #endregion
    }


    #region OnlineCheck
    //MFP在线检测
    public static bool MFPonlineCheck(string strMFPip)
    {
        bool boolMFPoblineCheck = false;
        
        try
        {
            Ping ping = new Ping();
            string ip = strMFPip;
            PingReply reply = ping.Send(ip, 100);
            if (reply.Status == IPStatus.Success)
            {
                boolMFPoblineCheck = true;
            }
            return boolMFPoblineCheck;
        }
        catch (Exception exMFPonlineCheck)
        {
            exMFPonlineCheck.ToString();
            return boolMFPoblineCheck;
        }
    }
    //检测MFP是否在线;
    public static string getMFPOnlineByIP(string tmpip)
    {
        return "点击查看";

        bool ret = MFPonlineCheck(tmpip);
        String status = "";
        if (ret)
        {
            status = "联机";
        }
        else
        {
            status = "脱机";
        }

        return status;
    }
    #endregion

    #region ColorStatus
    private static int GetTonerThreshold()
    {
        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["SimpleEAConnectionString"].ToString();
        SqlConnection con = new SqlConnection(constr);

        try
        {
            con.Open();
            SqlCommand command = new SqlCommand("select * from SimpleEA.dbo.SettinServerIP", con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int tonerThreshold = int.Parse(reader[4].ToString());//设置墨盒内的墨粉量阈值;
            int paperThreshold = int.Parse(reader[5].ToString());//设置纸张数量的阈值;
            return tonerThreshold;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    //检测MFP的墨粉状态;
    private static bool getMFPColStatus(string strDevIP)
    {
        string oid = "";
        int ret = 0;
        int tonerThreshold = GetTonerThreshold();
        bool status = true;
        
        //cyan toner
        oid = "1.3.6.1.2.1.43.11.1.1.9.1.1";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        if (ret < tonerThreshold)
        {
            status = false;
            return status;
        }
        
        //Magenta Toner
        oid = "1.3.6.1.2.1.43.11.1.1.9.1.2";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        if (ret < tonerThreshold)
        {
            status = false;
            return status;
        }
 
        //Yellow Toner
        oid = "1.3.6.1.2.1.43.11.1.1.9.1.3";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        if (ret < tonerThreshold)
        {
            status = false;
            return status;
        }

        //Black Toner
        oid = "1.3.6.1.2.1.43.11.1.1.9.1.4";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        if (ret < tonerThreshold)
        {
            status = false;
            return status;
        }

        return status;
    }
    public static string getMFPColorStatusByIP(string tmpip)
    {
        return "点击查看";
        string status = "";
        bool online = MFPonlineCheck(tmpip);
        if (online == false)
        {
            status = "脱机";
            return status;
        }

        bool ret = getMFPColStatus(tmpip);
        if (ret)
        {
            status = "正常";
        }
        else
        {
            status = "异常";
        }
        return status;
    }
    #endregion

    #region PaperStatus
    private static int GetPaperThreshold()
    {
        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["SimpleEAConnectionString"].ToString();
        SqlConnection con = new SqlConnection(constr);

        try
        {
            con.Open();
            SqlCommand command = new SqlCommand("select * from SimpleEA.dbo.SettinServerIP", con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int tonerThreshold = int.Parse(reader[4].ToString());//设置墨盒内的墨粉量阈值;
            int paperThreshold = int.Parse(reader[5].ToString());//设置纸张数量的阈值;
            return paperThreshold;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    private static bool getMFPPaperStatus(string strDevIP)
    {


        string oid = "";
        int ret = 0;
        int paperThreshold = GetPaperThreshold();
        int percent = 0;
        bool status = true;

        String[] paperName = new String[5];
        int[] nowVal = new int[5];
        int[] maxVal = new int[5];

        //纸盒规格
        oid = "1.3.6.1.2.1.43.8.2.1.12.1.1";
        string papername = GetMIBvalue(strDevIP, oid);
        paperName[0] = papername;
        //纸盒估计值
        oid = "1.3.6.1.2.1.43.8.2.1.10.1.1";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        nowVal[0] = ret;
        //纸盒最大值
        oid = "1.3.6.1.2.1.43.8.2.1.9.1.1";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        maxVal[0] = ret;
        if (maxVal[0] == -1 || maxVal[0] == 0)
        {
            status = false;
            return status;
        }
        else
        {
            percent = (nowVal[0] / maxVal[0]) * 100;
            if (percent < paperThreshold)
            {
                status = false;
                return status;
            }
        }

        //纸盒规格
        oid = "1.3.6.1.2.1.43.8.2.1.12.1.2";
        papername = GetMIBvalue(strDevIP, oid);
        paperName[1] = papername;
        //纸盒估计值
        oid = "1.3.6.1.2.1.43.8.2.1.10.1.2";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        nowVal[1] = ret;
        //纸盒最大值
        oid = "1.3.6.1.2.1.43.8.2.1.9.1.2";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        maxVal[1] = ret;
        //percent = (nowVal[1] / maxVal[1]) * 100;
        //if (percent < paperThreshold)
        //{
        //    status = false;
        //    return status;
        //}
        if (maxVal[1] == -1 || maxVal[1] == 0)
        {
            status = false;
            return status;
        }
        else
        {
            percent = (nowVal[1] / maxVal[1]) * 100;
            if (percent < paperThreshold)
            {
                status = false;
                return status;
            }
        }


        //纸盒规格
        oid = "1.3.6.1.2.1.43.8.2.1.12.1.3";
        papername = GetMIBvalue(strDevIP, oid);
        paperName[2] = papername;
        //纸盒估计值
        oid = "1.3.6.1.2.1.43.8.2.1.10.1.3";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        nowVal[2] = ret;
        //纸盒估计值
        oid = "1.3.6.1.2.1.43.8.2.1.9.1.3";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        maxVal[2] = ret;
        //percent = (nowVal[2] / maxVal[2]) * 100;
        //if (percent < paperThreshold)
        //{
        //    status = false;
        //    return status;
        //}
        if (maxVal[2] == -1 || maxVal[2] == 0)
        {
            status = false;
            return status;
        }
        else
        {
            percent = (nowVal[2] / maxVal[2]) * 100;
            if (percent < paperThreshold)
            {
                status = false;
                return status;
            }
        }

        //纸盒规格
        oid = "1.3.6.1.2.1.43.8.2.1.12.1.4";
        papername = GetMIBvalue(strDevIP, oid);
        paperName[3] = papername;
        //纸盒估计值
        oid = "1.3.6.1.2.1.43.8.2.1.10.1.4";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        nowVal[3] = ret;
        //纸盒最大值
        oid = "1.3.6.1.2.1.43.8.2.1.9.1.4";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        maxVal[3] = ret;
        //percent = (nowVal[3] / maxVal[3]) * 100;
        //if (percent < paperThreshold)
        //{
        //    status = false;
        //    return status;
        //}
        if (maxVal[3] == -1 || maxVal[3] == 0)
        {
            status = false;
            return status;
        }
        else
        {
            percent = (nowVal[3] / maxVal[3]) * 100;
            if (percent < paperThreshold)
            {
                status = false;
                return status;
            }
        }
        //纸盒规格
        oid = "1.3.6.1.2.1.43.8.2.1.12.1.5";
        papername = GetMIBvalue(strDevIP, oid);
        paperName[4] = papername;
        //纸盒估计值
        oid = "1.3.6.1.2.1.43.8.2.1.10.1.5";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        nowVal[4] = ret;
        //纸盒最大值
        oid = "1.3.6.1.2.1.43.8.2.1.9.1.5";
        ret = int.Parse(GetMIBvalue(strDevIP, oid));
        maxVal[4] = ret;
        //percent = (nowVal[4] / maxVal[4]) * 100;
        //if (percent < paperThreshold)
        //{
        //    status = false;
        //    return status;
        //}
        if (maxVal[4] == -1 || maxVal[4] == 0)
        {
            status = false;
            return status;
        }
        else
        {
            percent = (nowVal[4] / maxVal[4]) * 100;
            if (percent < paperThreshold)
            {
                status = false;
                return status;
            }
        }
        return status;
    }
    //检测MFP的纸盒状态;
    public static string getMFPPaperStatusByIP(string tmpip)
    {
        return "点击查看";
        string status = "";

        bool online = MFPonlineCheck(tmpip);
        if (online == false)
        {
            status = "无法检测";
            return status;
        }
        bool ret = getMFPPaperStatus(tmpip);

        if (ret)
        {
            status = "正常";
        }
        else
        {
            status = "异常";
        }
        return status;
    }
    #endregion

    #region StatusCheck
    //检测MFP的其他状态;
    public static string getMFPStatusByIP(string tmpip)
    {
        return "点击查看";
        string status = "";
        bool online = MFPonlineCheck(tmpip);
        if (online == false)
        {
            status = "脱机";
            return status;
        }
        int ret = MFPStatusByIP(tmpip);

        switch (ret)
        {
            case 5:
                status = "异常";
                break;
            case 3:
                status = "警告";
                break;
            case 2:
                status = "正常";
                break;
            default:
                status = "无法检测";
                break;
        }
        return status;
    }
    private static int  MFPStatusByIP(string strDevIP)
    {
        int deviceStatus;
        deviceStatus = int.Parse(GetMIBvalue(strDevIP, "1.3.6.1.2.1.25.3.2.1.5.1"));

        //int printerStatus;
        //printerStatus = int.Parse(GetMIBvalue(strDevIP, "1.3.6.1.2.1.25.3.5.1.1.1"));

        //bool status = true;
        int status = 0;
        switch (deviceStatus)
        {
            case 5:
                status = 5;
                break;
            case 3:
                status = 3;
                break;
            case 2:
                status = 2;
                break;
            default:
                status = 0;
                break;
        //    case 5:
        //        {
        //            ArrayList array = new ArrayList();
        //            array.Add("0x0100");
        //            array.Add("0x0800");
        //            array.Add("0x0400");
        //            array.Add("0x4000");
        //            array.Add("0x0008");
        //            array.Add("0x0020");
        //            array.Add("0x1000");
        //            array.Add("0x0002");

        //            string errorState;
        //            errorState = GetMIBvalue(strDevIP, "1.3.6.1.2.1.25.3.5.1.2.1");

        //            if (printerStatus == 1)
        //            {
        //                foreach (string error in array)
        //                {
        //                    if (error == errorState)
        //                    {
        //                        status = false;
        //                        return status;
        //                    }
        //                }
        //            }
        //        }; break;
        //    case 3:
        //        {
        //            ArrayList array = new ArrayList();
        //            array.Add("0x0100");
        //            array.Add("0x2000");
        //            array.Add("0x0002");

        //            string errorState;
        //            errorState = GetMIBvalue(strDevIP, "1.3.6.1.2.1.25.3.5.1.2.1");

        //            if (printerStatus == 1)
        //            {
        //                if (errorState == "0x0040")
        //                {
        //                    status = false;
        //                    return status;
        //                }
        //            }
        //            else if (printerStatus == 3)
        //            {
        //                foreach (string s in array)
        //                {
        //                    if (errorState == s)
        //                    {
        //                        status = false;
        //                        return status;
        //                    }
        //                }
        //            }
        //        }; break;
        //    case 2:
        //        {
        //            if (printerStatus == 4||printerStatus==1||printerStatus==5||printerStatus==3)
        //            {
        //                status = true;
        //                return status;
        //            }
        //        }; break;
        }
        return status;
    }
    #endregion

    public static string GetMIBvalue(string strDevIP, string oid)
    {
        try
        {
            string returnVal = "0";

            // SNMP community name
            OctetString community = new OctetString("public");

            // Define agent parameters class
            AgentParameters param = new AgentParameters(community);
            // Set SNMP version to 1 (or 2)
            param.Version = SnmpVersion.Ver1;
            // Construct the agent address object
            // IpAddress class is easy to use here because
            //  it will try to resolve constructor parameter if it doesn't
            //  parse to an IP address
            IpAddress agent = new IpAddress(strDevIP);

            // Construct target
            UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);

            // Pdu class used for all requests
            Pdu pdu = new Pdu(PduType.Get);


            ////pdu.VbList.Add("1.3.6.1.4.1.2385.1.1.19.2.1.3.1.4.60"); //sysName
            pdu.VbList.Add(oid);

            ////// Make SNMP request
            SnmpV1Packet result = (SnmpV1Packet)target.Request(pdu, param);
            if (result != null)
            {
                if (result.Pdu.ErrorStatus != 0)
                {
                    pdu.VbList.RemoveAt(0);
                    return "0";
                }
                else
                {
                    //returnVal = int.Parse(result.Pdu.VbList[0].Value.ToString());
                    returnVal = result.Pdu.VbList[0].Value.ToString();
                    pdu.VbList.RemoveAt(0);
                    return returnVal;
                }
            }
            else
            {
                pdu.VbList.RemoveAt(0);
                return "0";
            }
        }
        catch
        {
            return "-1";
        }
    }

    #endregion
}
