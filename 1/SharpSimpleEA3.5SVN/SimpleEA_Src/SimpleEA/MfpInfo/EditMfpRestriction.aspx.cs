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
using dtPriceMasterTableAdapters;
using BLL;
using Model;
/// <summary>
/// Edit Group screen
/// </summary>
/// <Date>2012.03.09</Date>
/// <Author>SLC Zheng wei</Author>
/// <Version>0.01</Version>
public partial class EditMfpRestriction : GrpInfoMain
{

    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>0.02</Version>
    protected void Page_Load(object sender, EventArgs e)
    {

 

        // Change to the Sub Title
        this.Master.Title = UtilConst.CON_PAGE_MFP;
        Master.SubTitle(UtilConst.CON_PAGE_EDIT, "MFPRestrictionList.aspx", true);


        // Check Access Role
        CheckUser();

        // Get SerialNumber from  Page Parms's.
        hidMFPSN.Value = Page.Request.Params["SerialNumber"].ToString();
        //dtMFPInformationTableAdapters.MFPInformationTableAdapter MfpAdapter = new dtMFPInformationTableAdapters.MFPInformationTableAdapter();
        //dtMFPInformation.MFPInformationDataTable table = MfpAdapter.GetDataBySerialNumber(hidMFPSN.Value);

        if (!IsPostBack)
        {
            //chen add for price select start
            PriceMasterTableAdapter PriceAdapter = new PriceMasterTableAdapter();
            ddlPriceSet.DataSource = PriceAdapter.GetData();
            ddlPriceSet.DataBind();
            //ddlPriceSet.Items.Insert(0, "");
            //chen add for price select end
            // Set User MFP Information
            BllMFP bllMFP = new BllMFP();
            Model.MFPEntry bean = bllMFP.GetMFPInfo(hidMFPSN.Value);

            //this.txtModel.Text = table[0].ModelName;
            //string strIp = table[0].IPAddress;

            //int label = table[0].Label;
            //if (label == 1)
            //{
            //    txtSev.Enabled = false;
            //}
            //else
            //{
            //    txtSev.Enabled = true;
            //}

            //// 2012.01.19 Update
            //// this.txtIP.Text = MfpAdapter.GetDataBySerialNumber(hidMFPIp.Value)[0].IPAddress;
            //if (!string.IsNullOrEmpty(strIp))
            //{
            //    string[] ipList = strIp.Split('.');
            //    if (ipList.Length > 3)
            //    {
            //        this.txtIP1.Text = ipList[0];
            //        this.txtIP2.Text = ipList[1];
            //        this.txtIP3.Text = ipList[2];
            //        this.txtIP4.Text = ipList[3];
            //    }
            //}

            //this.txtSev.Text = table[0].SerialNumber;
            //this.txtDes.Text = table[0].IsLocationNull() == true ? "" : table[0].Location;

            ////chen add for price select start
            //this.txtAdministrator.Text = table[0].AdministratorID.ToString();
            //if (this.txtAdministrator.Text.Trim() == "")
            //{
            //    this.txtAdministrator.Text = "admin";
            //}
            //this.txtPassword.Text = table[0].Password.ToString();
            //ddlPriceSet.SelectedValue = table[0].PriceID.ToString();
            //chen add for price select end

            this.txtModel.Text = bean.ModelName;
            string strIp = bean.IPAddress;

            int label = bean.Label;
            if (label == 1)
            {
                txtSev.Enabled = false;
            }
            else
            {
                txtSev.Enabled = true;
            }

            // 2012.01.19 Update
            // this.txtIP.Text = MfpAdapter.GetDataBySerialNumber(hidMFPIp.Value)[0].IPAddress;
            if (!string.IsNullOrEmpty(strIp))
            {
                string[] ipList = strIp.Split('.');
                if (ipList.Length > 3)
                {
                    this.txtIP1.Text = ipList[0];
                    this.txtIP2.Text = ipList[1];
                    this.txtIP3.Text = ipList[2];
                    this.txtIP4.Text = ipList[3];
                }
            }

            this.txtSev.Text = bean.SerialNumber;
            this.txtDes.Text = bean.Location;

            //chen add for price select start
            this.txtAdministrator.Text = bean.AdministratorID;
            if (this.txtAdministrator.Text.Trim() == "")
            {
                this.txtAdministrator.Text = "admin";
            }
            this.txtPassword.Text = bean.Password;
            ddlPriceSet.SelectedValue = bean.PriceID.ToString();
            int monitor = bean.Monitor;
            int printMonitor = monitor % 10;
            int copyMonitor = monitor / 10 % 10;
            int bw = monitor / 100;
            if (printMonitor == 1)
            {
                Monitor1.Checked = true;
            }
            else
            {
                Monitor1.Checked = false;
            }
            if (copyMonitor == 1)
            {
                Monitor2.Checked = true;
            }
            else
            {
                Monitor2.Checked = false;
            }
            if (bw == 1)
            {
                Monitor3.Checked = true;
            }
            else
            {
                Monitor3.Checked = false;
            }
            //switch(bean.Monitor)
            //{
            //    case 1:
            //        Monitor1.Checked = true;
            //        Monitor2.Checked = false;
            //        break;
            //    case 2:
            //        Monitor1.Checked = false;
            //        Monitor2.Checked = true;
            //        break;
            //    case 3:
            //        Monitor1.Checked = true;
            //        Monitor2.Checked = true;
            //        break;
            //    default:
            //        Monitor1.Checked = false;
            //        Monitor2.Checked = false;
            //        break;
            
            //}
           //Error Message
            // Must Check
            rfvModelName.ErrorMessage = "请输入MFP型号。";
            // Check Password
            rfvPSWD.ErrorMessage = "请输入管理员密码。";
            // ILLEGAL
            revModelName.ErrorMessage = UtilConst.MSG_ILLEGAL_STRING;
            // ValidationExpression
            revModelName.ValidationExpression = UtilConst.CON_VAL_ILLEGAL;
        }
        // Update Button's Confirm Msg
        btnUpdate.OnClientClick = ConfirmFunctionUpd(UtilConst.MSG_UPDATE_UPDATE,btnUpdate.ValidationGroup);
        // Cancel Button's Confirm Msg
        btnCancel.OnClientClick = ConfirmFunctionCancel(UtilConst.MSG_UPDATE_CANCEL);
    }
    #endregion

    #region "Update MFP Information"
    /// <summary>
    /// Update Group Information
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>0.01</Version>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        
            if (!Page.IsValid)
            {
                return;
            }

            BllMFP mfpBll = new BllMFP();

            Model.MFPEntry model = new Model.MFPEntry();

            model.SerialNumber = txtSev.Text;
            model.ModelName = this.txtModel.Text;
            model.IPAddress = this.txtIP1.Text + "." + this.txtIP2.Text + "." + this.txtIP3.Text + "." + this.txtIP4.Text;
            model.Location = this.txtDes.Text;   //描述
            model.AdministratorID = this.txtAdministrator.Text;
            model.Password = this.txtPassword.Text;
            model.PriceID = int.Parse(this.ddlPriceSet.SelectedValue);
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
            if (Monitor1.Checked )
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

            model.Prompt = "";
            mfpBll.Update(model);
            this.Response.Redirect("MFPRestrictionList.aspx", false);

        //    // GroupId
        //    using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
        //    {
        //        con.Open();

        //        SqlTransaction tran = con.BeginTransaction();
        //        try
        //        {
        //            //SQL 语句被替换（cuiqingqing）
        //            string strSql;
        //            strSql = "   UPDATE [MFPInformation]" + Environment.NewLine;
        //            strSql += "  SET [ModelName] = {1}" + Environment.NewLine;
        //            strSql += "     ,[Location] = {2}" + Environment.NewLine;
        //            //chen add for price start
        //            strSql += "     ,[AdministratorID] = {3}" + Environment.NewLine;
        //            strSql += "     ,[Password] = {4}" + Environment.NewLine;
        //            strSql += "     ,[PriceID] = {5}" + Environment.NewLine;
        //            //chen add for price start
        //            strSql += "  WHERE SerialNumber = {0}" + Environment.NewLine;
        //            strSql = string.Format(strSql,
        //                ConvertStringToSQL(hidMFPSN.Value),
        //                ConvertStringToSQL(this.txtModel.Text),
        //                ConvertStringToSQL(this.txtDes.Text),
        //                //chen add for price start
        //                ConvertStringToSQL(this.txtAdministrator.Text),
        //                ConvertStringToSQL(this.txtPassword.Text),
        //                ConvertStringToSQL(this.ddlPriceSet.SelectedValue)
        //                //chen add for price start
        //                );

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
        //    // Changes are applied and then back to Group Managemnet Screen.
        //    this.Response.Redirect("MFPRestrictionList.aspx", false);
        //}
        //catch (Exception ex)
        //{
        //    ErrorAlert(ex);
        //}

    }

    #endregion

    #region "btnCancel_Click"
    /// <summary>
    /// Back to Group Management Screen
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <Date>2012.03.09</Date>
    /// <Author>SLC Zheng wei</Author>
    /// <Version>0.01</Version>
    protected void btnCancelMFP_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("MFPRestrictionList.aspx", false);
    }
    #endregion

}
