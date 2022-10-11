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
using dtPriceMasterTableAdapters;
using BLL;

/// <summary>
/// add by Zheng Wei 
/// </summary>
public partial class MfpInfo_AddMFPRestriction : GrpInfoMain
{

    #region Page_Load
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Change to the Sub Title
        this.Master.Title = UtilConst.CON_PAGE_MFP;
        Master.SubTitle(UtilConst.CON_PAGE_ADD, "MFPRestrictionList.aspx", true);


        // Check Access Role
        CheckUser();

        if (!IsPostBack)
        {

            //chen add for price select start
            PriceMasterTableAdapter RestrictionAdapter = new PriceMasterTableAdapter();
            ddlPriceSet.DataSource = RestrictionAdapter.GetData();
            ddlPriceSet.DataBind();
            //ddlPriceSet.Items.Insert(0, "");
        }
        this.txtAdministrator.Text = "admin";

        //this.txtAppName.Text = ConfigurationManager.AppSettings["AppName"].ToString();
        //this.txtAppUIAddress.Text = ConfigurationManager.AppSettings["AppUIAddress"].ToString();
        //this.txtAppServerAddress.Text = ConfigurationManager.AppSettings["AppServAddress"].ToString();
        //chen add for price select end

        //Error Message
        // Must Check
        rfvModelName.ErrorMessage = "请输入MFP 型号。";
        // ILLEGAL
        revModelName.ErrorMessage = UtilConst.MSG_ILLEGAL_STRING;
        // ValidationExpression
        revModelName.ValidationExpression = UtilConst.CON_VAL_ILLEGAL;

        // Must Check 
        //CusIPRequire.ErrorMessage = "请输入MFP IP 地址";
        CusIPRequire.ErrorMessage = "IP地址未填写或重复,请核对";
        // Special Code
        IpReqular.ErrorMessage = "Ip 地址格式不正确，请重新输入";
        //RegIPAddress.ErrorMessage = UtilConst.MSG_MFP_IPREG;
        // Must Check 
        ReqularMFPSN.ErrorMessage = "请输入MFP 序列号。";
        //Must Check
        rfvPSWD.ErrorMessage = "请输入管理员密码。";
        // Special Code
        RegularMFPSN.ErrorMessage = UtilConst.MSG_ICCARD_CODE;
        
        RegularMFPSN.ValidationExpression = "^[a-zA-Z0-9]{0,20}$";

        //Exist Check
        valMFPSN.ErrorMessage = UtilConst.MSG_MFP_SNEXIST;

        // Cancel Button's Confirm Msg
        btnCancel.OnClientClick = ConfirmFunctionCancel(UtilConst.MSG_UPDATE_CANCEL);

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

        BllMFP mfpBll = new BllMFP();

        Model.MFPEntry model = new Model.MFPEntry();

        model.SerialNumber = txtSev.Text;
        model.ModelName  = this.txtModel.Text;
        model.IPAddress  =this.txtIP1.Text + "." + this.txtIP2.Text + "." + this.txtIP3.Text + "." + this.txtIP4.Text;
        model.Location  = this.txtDes.Text;
        model.AdministratorID =this.txtAdministrator.Text;
        model.Password =this.txtPassword.Text;
        model.PriceID =int.Parse(this.ddlPriceSet.SelectedValue);
        model.Label = 0;
        //if (Monitor1.Checked && !Monitor2.Checked)
        //{
        //    model.Monitor = 1;//打印
        //}
        //else if (!Monitor1.Checked && Monitor2.Checked)
        //{
        //    model.Monitor = 2;//复印
        //}
        //else if (Monitor1.Checked && Monitor2.Checked)
        //{
        //    model.Monitor = 3;//打印和复印
        //}
        int monitor = 0;
        if (Monitor1.Checked)
        {
            monitor = 1;//打印 个位1表示打印留底
        }
        if (Monitor2.Checked)
        {
            monitor = monitor + 10;//复印 十位1表示复印留底
        }
        if (Monitor3.Checked)
        {
            monitor = monitor + 100;//强制黑八 百位1表示强制黑白
        }
        model.Monitor = monitor;

        //model.Monitor =  0;
        model.Prompt = "";
        mfpBll.Add(model);


        this.Response.Redirect("MFPRestrictionList.aspx", false);

        return;


        //try
        //{
        //    if (!Page.IsValid)
        //    {
        //        return;
        //    }
               
        //    using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        //    {
        //        con.Open();
        //        SqlTransaction tran = con.BeginTransaction();
        //        try
        //        {
                    
        //            string strSql;
        //            //1. Set MFP Group Information
        //            strSql = "   INSERT INTO [MFPInformation]          " + Environment.NewLine;
        //            strSql += "             ([SerialNumber]                " + Environment.NewLine;
        //            strSql += "             ,[ModelName]          " + Environment.NewLine;
        //            strSql += "             ,[IPAddress]         " + Environment.NewLine;
        //            strSql += "             ,[Location]          " + Environment.NewLine;
        //            //chen add for price start
        //            strSql += "             ,[AdministratorID]          " + Environment.NewLine;
        //            strSql += "             ,[Password]          " + Environment.NewLine;
        //            strSql += "             ,[PriceID])          " + Environment.NewLine;
        //            //strSql += "             ,[AppName]          " + Environment.NewLine;
        //            //strSql += "             ,[AppUIAddress]          " + Environment.NewLine;
        //            //strSql += "             ,[AppServAddress] )         " + Environment.NewLine;
        //            //chen add for price end
        //            strSql += "       VALUES                     " + Environment.NewLine;
        //            strSql += "             ({0}                 " + Environment.NewLine;
        //            strSql += "             ,{1}                 " + Environment.NewLine;
        //            strSql += "             ,{2}                 " + Environment.NewLine;
        //            strSql += "             ,{3}                 " + Environment.NewLine;
        //            //chen add for price start
        //            strSql += "             ,{4}                 " + Environment.NewLine;
        //            strSql += "             ,{5}                 " + Environment.NewLine;
        //            strSql += "             ,{6})                 " + Environment.NewLine;
        //            //chen add for price end

        //            //chen add for price start
        //            //string[] paramslist = new string[4];
        //            string[] paramslist = new string[7];
        //            //chen add for price end
        //            //SerialNumber
        //            paramslist[0] = ConvertStringToSQL(this.txtSev.Text);
        //            // ModelName
        //            paramslist[1] = ConvertStringToSQL(this.txtModel.Text);
        //            // IPAddress
        //            paramslist[2] = ConvertStringToSQL(this.txtIP1.Text + "." + this.txtIP2.Text + "." + this.txtIP3.Text + "." + this.txtIP4.Text);
        //            // Location
        //            paramslist[3] = ConvertStringToSQL(this.txtDes.Text);
        //            //chen add for price start
        //            paramslist[4] = ConvertStringToSQL(this.txtAdministrator.Text);
        //            paramslist[5] = ConvertStringToSQL(this.txtPassword.Text);
        //            paramslist[6] = ConvertStringToSQL(this.ddlPriceSet.SelectedValue);
        //            //chen add for price end

        //            strSql = string.Format(strSql, paramslist);

        //            using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
        //            {
        //                cmd.ExecuteNonQuery();
        //            }

        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            if (tran.Connection != null)
        //            {
        //                tran.Rollback();
        //            }
        //            throw ex;
        //        }
        //        finally
        //        {
        //            tran.Dispose();
        //            tran = null;
        //        }
        //    }
        //    // Changes are applied and then back to MFPRestrictionList Screen.
        //    this.Response.Redirect("MFPRestrictionList.aspx", false);
        //}
        //catch (Exception ex)
        //{
        //    ErrorAlert(ex);
        //}
    }

    #endregion

    #region Add New MFP
    /// <summary>
    /// cancel button event, back to List page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected new void btnCancel_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("MFPRestrictionList.aspx", false);
    }

    #endregion

    #region Validate Check
    /// <summary>
    /// Check the MFP SerialNumber
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void valMFPSN_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string strSN = args.Value.Trim().ToString();

        dtMFPInformationTableAdapters.MFPInformationTableAdapter MfpInfoAdapter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();


        if (MfpInfoAdapter.GetData(strSN).Count> 0)
        {
            args.IsValid = false;
            
        }
        else
        {
            args.IsValid = true;
        }
    }


    protected void CusIPRequire_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string strIP = txtIP1.Text + "." + txtIP2.Text + "." + txtIP3.Text + "." + txtIP4.Text;
        

        dtMFPInformationTableAdapters.MFPInformationTableAdapter MfpInfoAdapter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();

        object o = MfpInfoAdapter.GetByIPAddress(strIP);
        if (o != null && (int)o > 0)
        {
            args.IsValid = false;
            
        }
        else
        {
            args.IsValid = true;
        
        }
    }

    #endregion
}
