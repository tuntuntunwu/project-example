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
using System.Collections.Generic;

/// <summary>
/// add by Zheng Wei 
/// </summary>
public partial class PirceInfo_AddPrice : PriceInfoMain
{
    #region Page_Load
    private string action = "add";
    private string priceid = "0";
    public string btnAddClientid;
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Change to the Sub Title
        this.Master.Title = UtilConst.CON_PAGE_Price;
        Master.SubTitle(UtilConst.CON_PAGE_ADD, "PriceList.aspx", true);
        // Check Access Role
        CheckUser();
        btnAddClientid = btnAdd.ClientID;
        //Error Message
        // Must Check
        rfvModelName.ErrorMessage = "请输入价格方案名称。";
        // ILLEGAL
        revModelName.ErrorMessage = UtilConst.MSG_ILLEGAL_STRING;
        // ValidationExpression
        revModelName.ValidationExpression = UtilConst.CON_VAL_ILLEGAL;
        
        if (!this.IsPostBack)//第一次打开
        {
            foreach (Control cp in Page.Controls)
            {
                foreach (System.Web.UI.Control ct in cp.Controls)
                {
                    if (ct is HtmlForm)
                    {
                        foreach (Control con in ct.Controls)
                        {
                            foreach (Control c in con.Controls)
                            {
                                if (c is TextBox)
                                {
                                    if (c.ID.IndexOf("txt_Print") == 0 || c.ID.IndexOf("txt_Scan") == 0 || c.ID.IndexOf("txt_Fax") == 0)
                                    {
                                        (c as TextBox).Text = "0.00";
                                        (c as TextBox).Attributes.Add("onfocus", "HookUpControl(this,'" + CustomValidator1.ClientID + "')");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (Request["priceid"] == null)
            {
                action = "add";
                this.btnAdd.Text = "添加";
                //Exist Check
                valPirceSN.ErrorMessage = "该价格方案名称已被占用";

            }
            else
            {
                action = "edit";

                priceid = Request["priceid"].ToString();
               
                hidPriceId.Value = priceid;
                if (hidPriceId.Value == "0")
                {
                   txt_price_name.Enabled = false;
                }
                this.btnAdd.Text = "保存";
                valPirceSN.ErrorMessage = "该价格方案名称已被占用";
                BindTextBox(priceid);
                BindScanTextBox(priceid);
                BindFaxTextBox(priceid);

            }
            ViewState["action"] = action;

        }

        
    
        // Cancel Button's Confirm Msg
        btnCancel.OnClientClick = ConfirmFunctionCancel(UtilConst.MSG_UPDATE_CANCEL);

    }
     
    #endregion
    #region Bind data to textbox
    private void BindTextBox(string pid)
     {
         SqlDataReader sda;
         using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
         {
             con.Open();
             SqlTransaction tran = con.BeginTransaction();
             try
             {
                 string strSql = string.Format("select PRICENM from PRICEMASTER where priceid ={0}", pid);
                 using (SqlCommand cmd = new SqlCommand(strSql, con,tran))
                {
                     txt_price_name.Text = cmd.ExecuteScalar().ToString();
                }
                 strSql = string.Format("select * from pricedetail where priceid ={0} and JobID='1'", pid);
                 using (SqlCommand cmd = new SqlCommand(strSql, con,tran))
                 {
                    sda= cmd.ExecuteReader();
                    while (sda.Read())
                    {
                        //PaperTypeID  A3--1  A4--2  A5--3  B4--4  B5--7
                        if (sda["papertypeid"].ToString() == "1") //A3
                        {
                            txt_Print_A3_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Print_A3_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Print_A3_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "2")
                        {
                            txt_Print_A4_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Print_A4_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Print_A4_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "3")
                        {
                            txt_Print_A5_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Print_A5_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Print_A5_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "4")
                        {
                            txt_Print_B4_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Print_B4_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Print_B4_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "5")
                        {
                            txt_Print_B5_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Print_B5_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Print_B5_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "6")
                        {
                            txt_Print_SA_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Print_SA_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Print_SA_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "7")
                        {
                            txt_Print_BA_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Print_BA_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Print_BA_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "8")
                        {
                            txt_Print_OT_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Print_OT_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Print_OT_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                    }
                    sda.Close();
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
    private void BindScanTextBox(string pid)
    {
        SqlDataReader sda;
        using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                string strSql = string.Format("select PRICENM from PRICEMASTER where priceid ={0}", pid);
                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    txt_price_name.Text = cmd.ExecuteScalar().ToString();
                }
                strSql = string.Format("select * from pricedetail where priceid ={0} and JobID='6'", pid);
                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    sda = cmd.ExecuteReader();
                    while (sda.Read())
                    {
                        //PaperTypeID  A3--1  A4--2  A5--3  B4--4  B5--7
                        if (sda["papertypeid"].ToString() == "1") //A3
                        {
                            txt_Scan_A3_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Scan_A3_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Scan_A3_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "2")
                        {
                            txt_Scan_A4_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Scan_A4_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Scan_A4_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "3")
                        {
                            txt_Scan_A5_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Scan_A5_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Scan_A5_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "4")
                        {
                            txt_Scan_B4_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Scan_B4_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Scan_B4_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "5")
                        {
                            txt_Scan_B5_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Scan_B5_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Scan_B5_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "6")
                        {
                            txt_Scan_SA_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Scan_SA_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Scan_SA_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "7")
                        {
                            txt_Scan_BA_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Scan_BA_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Scan_BA_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "8")
                        {
                            txt_Scan_OT_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Scan_OT_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Scan_OT_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                    }
                    sda.Close();
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
    private void BindFaxTextBox(string pid)
    {
        SqlDataReader sda;
        using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                string strSql = string.Format("select PRICENM from PRICEMASTER where priceid ={0}", pid);
                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    txt_price_name.Text = cmd.ExecuteScalar().ToString();
                }
                strSql = string.Format("select * from pricedetail where priceid ={0} and JobID='8'", pid);
                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    sda = cmd.ExecuteReader();
                    while (sda.Read())
                    {
                        //PaperTypeID  A3--1  A4--2  A5--3  B4--4  B5--7
                        if (sda["papertypeid"].ToString() == "1") //A3
                        {
                            txt_Fax_A3_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Fax_A3_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Fax_A3_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "2")
                        {
                            txt_Fax_A4_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Fax_A4_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Fax_A4_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "3")
                        {
                            txt_Fax_A5_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Fax_A5_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Fax_A5_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "4")
                        {
                            txt_Fax_B4_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Fax_B4_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Fax_B4_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "5")
                        {
                            txt_Fax_B5_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Fax_B5_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Fax_B5_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "6")
                        {
                            txt_Fax_SA_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Fax_SA_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Fax_SA_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "7")
                        {
                            txt_Fax_BA_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Fax_BA_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Fax_BA_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                        else if (sda["papertypeid"].ToString() == "8")
                        {
                            txt_Fax_OT_Paper_Price.Text = sda["PaperPrice"].ToString();
                            txt_Fax_OT_FullColor_Price.Text = sda["ColorPrice"].ToString();
                            txt_Fax_OT_BW_Price.Text = sda["GrayPrice"].ToString();
                        }
                    }
                    sda.Close();
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
    #endregion
    #region Add New MFP
    /// <summary>
    /// Add New MFP
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
          // Page.Validate();
           if (!Page.IsValid)
           {
                return;
            }
            if (txt_price_name.Text.Length > 50)
            {
                Response.Write("价格方案名称不能超过50个字符的长度");
            }
            using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    
                    string strSql;
                    //1. Set MFP Group Information
                    //job id print 2  copy 1  scan 6  fax 8 
                    //PaperTypeID  A3--1  A4--2  A5--3  B4--4  B5--5  others 8
                    int[] job_arr = { 1, 2, 6, 8 };
                    int[] paper_arr = { 1, 2, 3, 4, 5, 6, 7,8 };
                    Dictionary<string, string> Price = new Dictionary<string, string>();

                    //添加元素
                    Price.Add("1-1", string.Format("{0}-{1}-{2}", txt_Print_A3_Paper_Price.Text, txt_Print_A3_BW_Price.Text, txt_Print_A3_FullColor_Price.Text));
                    Price.Add("1-2", string.Format("{0}-{1}-{2}", txt_Print_A4_Paper_Price.Text, txt_Print_A4_BW_Price.Text, txt_Print_A4_FullColor_Price.Text));
                    Price.Add("1-3", string.Format("{0}-{1}-{2}", txt_Print_A5_Paper_Price.Text, txt_Print_A5_BW_Price.Text, txt_Print_A5_FullColor_Price.Text));
                    Price.Add("1-4", string.Format("{0}-{1}-{2}", txt_Print_B4_Paper_Price.Text, txt_Print_B4_BW_Price.Text, txt_Print_B4_FullColor_Price.Text));
                    Price.Add("1-5", string.Format("{0}-{1}-{2}", txt_Print_B5_Paper_Price.Text, txt_Print_B5_BW_Price.Text, txt_Print_B5_FullColor_Price.Text));
                    Price.Add("1-6", string.Format("{0}-{1}-{2}", txt_Print_SA_Paper_Price.Text, txt_Print_SA_BW_Price.Text, txt_Print_SA_FullColor_Price.Text));
                    Price.Add("1-7", string.Format("{0}-{1}-{2}", txt_Print_BA_Paper_Price.Text, txt_Print_BA_BW_Price.Text, txt_Print_BA_FullColor_Price.Text));
                    Price.Add("1-8", string.Format("{0}-{1}-{2}", txt_Print_OT_Paper_Price.Text, txt_Print_OT_BW_Price.Text, txt_Print_OT_FullColor_Price.Text));

                    //添加元素
                    Price.Add("2-1", string.Format("{0}-{1}-{2}", txt_Print_A3_Paper_Price.Text, txt_Print_A3_BW_Price.Text, txt_Print_A3_FullColor_Price.Text));
                    Price.Add("2-2", string.Format("{0}-{1}-{2}", txt_Print_A4_Paper_Price.Text, txt_Print_A4_BW_Price.Text, txt_Print_A4_FullColor_Price.Text));
                    Price.Add("2-3", string.Format("{0}-{1}-{2}", txt_Print_A5_Paper_Price.Text, txt_Print_A5_BW_Price.Text, txt_Print_A5_FullColor_Price.Text));
                    Price.Add("2-4", string.Format("{0}-{1}-{2}", txt_Print_B4_Paper_Price.Text, txt_Print_B4_BW_Price.Text, txt_Print_B4_FullColor_Price.Text));
                    Price.Add("2-5", string.Format("{0}-{1}-{2}", txt_Print_B5_Paper_Price.Text, txt_Print_B5_BW_Price.Text, txt_Print_B5_FullColor_Price.Text));
                    Price.Add("2-6", string.Format("{0}-{1}-{2}", txt_Print_SA_Paper_Price.Text, txt_Print_SA_BW_Price.Text, txt_Print_SA_FullColor_Price.Text));
                    Price.Add("2-7", string.Format("{0}-{1}-{2}", txt_Print_BA_Paper_Price.Text, txt_Print_BA_BW_Price.Text, txt_Print_BA_FullColor_Price.Text));
                    Price.Add("2-8", string.Format("{0}-{1}-{2}", txt_Print_OT_Paper_Price.Text, txt_Print_OT_BW_Price.Text, txt_Print_OT_FullColor_Price.Text));

                    Price.Add("6-1", string.Format("{0}-{1}-{2}", txt_Scan_A3_Paper_Price.Text, txt_Scan_A3_BW_Price.Text, txt_Scan_A3_FullColor_Price.Text));
                    Price.Add("6-2", string.Format("{0}-{1}-{2}", txt_Scan_A4_Paper_Price.Text, txt_Scan_A4_BW_Price.Text, txt_Scan_A4_FullColor_Price.Text));
                    Price.Add("6-3", string.Format("{0}-{1}-{2}", txt_Scan_A5_Paper_Price.Text, txt_Scan_A5_BW_Price.Text, txt_Scan_A5_FullColor_Price.Text));
                    Price.Add("6-4", string.Format("{0}-{1}-{2}", txt_Scan_B4_Paper_Price.Text, txt_Scan_B4_BW_Price.Text, txt_Scan_B4_FullColor_Price.Text));
                    Price.Add("6-5", string.Format("{0}-{1}-{2}", txt_Scan_B5_Paper_Price.Text, txt_Scan_B5_BW_Price.Text, txt_Scan_B5_FullColor_Price.Text));
                    Price.Add("6-6", string.Format("{0}-{1}-{2}", txt_Scan_SA_Paper_Price.Text, txt_Scan_SA_BW_Price.Text, txt_Scan_SA_FullColor_Price.Text));
                    Price.Add("6-7", string.Format("{0}-{1}-{2}", txt_Scan_BA_Paper_Price.Text, txt_Scan_BA_BW_Price.Text, txt_Scan_BA_FullColor_Price.Text));
                    Price.Add("6-8", string.Format("{0}-{1}-{2}", txt_Scan_OT_Paper_Price.Text, txt_Scan_OT_BW_Price.Text, txt_Scan_OT_FullColor_Price.Text));



                    Price.Add("8-1", string.Format("{0}-{1}-{2}", txt_Fax_A3_Paper_Price.Text, txt_Fax_A3_BW_Price.Text, txt_Fax_A3_FullColor_Price.Text));
                    Price.Add("8-2", string.Format("{0}-{1}-{2}", txt_Fax_A4_Paper_Price.Text, txt_Fax_A4_BW_Price.Text, txt_Fax_A4_FullColor_Price.Text));
                    Price.Add("8-3", string.Format("{0}-{1}-{2}", txt_Fax_A5_Paper_Price.Text, txt_Fax_A5_BW_Price.Text, txt_Fax_A5_FullColor_Price.Text));
                    Price.Add("8-4", string.Format("{0}-{1}-{2}", txt_Fax_B4_Paper_Price.Text, txt_Fax_B4_BW_Price.Text, txt_Fax_B4_FullColor_Price.Text));
                    Price.Add("8-5", string.Format("{0}-{1}-{2}", txt_Fax_B5_Paper_Price.Text, txt_Fax_B5_BW_Price.Text, txt_Fax_B5_FullColor_Price.Text));
                    Price.Add("8-6", string.Format("{0}-{1}-{2}", txt_Fax_SA_Paper_Price.Text, txt_Fax_SA_BW_Price.Text, txt_Fax_SA_FullColor_Price.Text));
                    Price.Add("8-7", string.Format("{0}-{1}-{2}", txt_Fax_BA_Paper_Price.Text, txt_Fax_BA_BW_Price.Text, txt_Fax_BA_FullColor_Price.Text));
                    Price.Add("8-8", string.Format("{0}-{1}-{2}", txt_Fax_OT_Paper_Price.Text, txt_Fax_OT_BW_Price.Text, txt_Fax_OT_FullColor_Price.Text));
                    string[] paramslist = new string[3];

                    paramslist[0] = ConvertStringToSQL(this.txt_price_name.Text);
                    //if (ridMethod1.Checked)
                    //    paramslist[1] = ConvertIntToSQL("0");
                    //else if (ridMethod2.Checked)
                    //    paramslist[1] = ConvertIntToSQL("1");
                    paramslist[1] = ConvertIntToSQL("0");

                    if(ViewState["action"]==null ||ViewState["action"].ToString()=="add")
                    {
                        int pkid = 0;
                        strSql = "select  max(PriceID) from pricemaster";
                        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        {
                            priceid = cmd.ExecuteScalar().ToString();
                            pkid = int.Parse(priceid);
                            pkid += 1;
                        }
                        paramslist[2] = ConvertIntToSQL(pkid.ToString());

                        strSql = "   INSERT INTO [PriceMaster]          " + Environment.NewLine;
                        strSql += "             ([PriceID]              " + Environment.NewLine;
                        strSql += "             ,[PriceNM]              " + Environment.NewLine;
                        strSql += "             ,[PriceCalMode]         " + Environment.NewLine;
                        strSql += "             ,[CreateTime]           " + Environment.NewLine;
                        strSql += "             ,[UpdateTime])          " + Environment.NewLine;
                        strSql += "       VALUES                        " + Environment.NewLine;
                        strSql += "             ({2},{0}                    " + Environment.NewLine;
                        strSql += "             ,{1}                    " + Environment.NewLine;
                        strSql += "             ,getdate() , getdate())  " + Environment.NewLine;
                    
                        strSql = string.Format(strSql, paramslist);
                        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        strSql = "select  max(PriceID) from pricemaster";
                         using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        {
                            priceid = cmd.ExecuteScalar().ToString();
                        }
                  

                        foreach (int jobid in job_arr)
                        {

                            foreach (int paperid in paper_arr)
                            {
                                string key = string.Format("{0}-{1}", jobid.ToString(), paperid.ToString());
                                if (Price.ContainsKey(key))
                                {
                                    strSql = this.CreateInsertSql(paperid.ToString(), jobid.ToString(), Price[key]);
                                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        priceid = hidPriceId.Value;
                        strSql = "   update [PriceMaster]  set        " + Environment.NewLine;
                        strSql += "   [PriceNM]={0}                " + Environment.NewLine;
                        strSql += "  ,[PriceCalMode]={1}         " + Environment.NewLine;
                        strSql += "  ,[UpdateTime]=getdate()                " + Environment.NewLine;
                        strSql += "       where priceid={2}                    " + Environment.NewLine;
                        paramslist[2] = ConvertIntToSQL(priceid);
                        strSql = string.Format(strSql, paramslist);
                        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        foreach (int jobid in job_arr)
                        {

                            foreach (int paperid in paper_arr)
                            {
                                string key = string.Format("{0}-{1}", jobid.ToString(), paperid.ToString());
                                if (Price.ContainsKey(key))
                                {
                                    strSql = this.CreateInsertSql(paperid.ToString(), jobid.ToString(), Price[key]);
                                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                }
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
            // Changes are applied and then back to MFPRestrictionList Screen.
            this.Response.Redirect("PriceList.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }
    }

    #endregion
    private string CreateInsertSql(string paperid,string jobid,string price)
    {
        ///////////////////////////////////////////////////////////////
        string[] tmp_arr;
        tmp_arr=price.Split('-');
        string strSql;
        strSql = "   INSERT INTO [PriceDetail]          " + Environment.NewLine;
        strSql += "             ([PriceID]                " + Environment.NewLine;
        strSql += "             ,[PaperTypeID]                " + Environment.NewLine;
        strSql += "             ,[JobID]                " + Environment.NewLine;
        strSql += "             ,[PaperPrice]                " + Environment.NewLine;
        strSql += "             ,[GrayPrice]                " + Environment.NewLine;
        strSql += "             ,[ColorPrice]               " + Environment.NewLine;
        strSql += "             ,[CreateTime]               " + Environment.NewLine;
        strSql += "             ,[UpdateTime])                " + Environment.NewLine;
        strSql += "       VALUES                     " + Environment.NewLine;
        strSql += "             ({0}                 " + Environment.NewLine;
        strSql += "             ,{1}                 " + Environment.NewLine;
        strSql += "             ,{2}                 " + Environment.NewLine;
        strSql += "             ,{3}                 " + Environment.NewLine;
        strSql += "             ,{4}                 " + Environment.NewLine;
        strSql += "             ,{5}                 " + Environment.NewLine;
        strSql += "             ,getdate() , getdate())  " + Environment.NewLine;

        string[] paramslist = new string[6];
        paramslist[0] = ConvertIntToSQL(priceid);
        paramslist[1] = ConvertIntToSQL(paperid);//papertype id
        paramslist[2] = ConvertIntToSQL(jobid);//jobid
        paramslist[3] = ConvertMoneyToSQL(tmp_arr[0]);
        paramslist[4] = ConvertMoneyToSQL(tmp_arr[1]);
        paramslist[5] = ConvertMoneyToSQL(tmp_arr[2]);
        strSql = string.Format(strSql, paramslist);
        return strSql;
    }
    private string CreateUpdateSql(string paperid, string jobid, string price)
    {
        ///////////////////////////////////////////////////////////////
        string[] tmp_arr;
        tmp_arr = price.Split('-');
        string strSql;
        strSql = "   update    [PriceDetail]  set         " + Environment.NewLine;
        strSql += "             [PaperPrice]={3}                " + Environment.NewLine;
        strSql += "             ,[GrayPrice]={4}                " + Environment.NewLine;
        strSql += "             ,[ColorPrice]={5}               " + Environment.NewLine;
        strSql += "             ,[UpdateTime]=getdate()                " + Environment.NewLine;
        strSql += "       where priceid={0} and papertypeid={1} and jobid={2}                    " + Environment.NewLine;
        
        string[] paramslist = new string[6];
        paramslist[0] = ConvertIntToSQL(priceid);
        paramslist[1] = ConvertIntToSQL(paperid);//papertype id
        paramslist[2] = ConvertIntToSQL(jobid);//jobid
        paramslist[3] = ConvertMoneyToSQL(tmp_arr[0]);
        paramslist[4] = ConvertMoneyToSQL(tmp_arr[1]);
        paramslist[5] = ConvertMoneyToSQL(tmp_arr[2]);
        strSql = string.Format(strSql, paramslist);
        return strSql;
    }
    #region Add New MFP
    /// <summary>
    /// cancel button event, back to List page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("PriceList.aspx", false);
    }

    #endregion

    #region Validate Check
    /// <summary>
    /// Check the MFP SerialNumber
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void valPriceSN_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string strSN = args.Value.Trim().ToString();
        int count=0;
        string strSql="";
        
        if(ViewState["action"]==null||"add"==ViewState["action"].ToString())
            strSql=string.Format("select count(*) from pricemaster where pricenm='{0}'",strSN);
        else 
        {
            priceid = hidPriceId.Value;
            strSql = string.Format("select count(*) from pricemaster where pricenm='{0}' and priceid !={1}", strSN, priceid);
        }
        using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        {
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                {
                    count = (int)cmd.ExecuteScalar();
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
       
        if (count> 0)
        {
          
            args.IsValid = false;
            
        }
        else
        {
            if (Page.IsValid)
                args.IsValid = true;
            else
                args.IsValid = false;
        }
    }

    #endregion
}
