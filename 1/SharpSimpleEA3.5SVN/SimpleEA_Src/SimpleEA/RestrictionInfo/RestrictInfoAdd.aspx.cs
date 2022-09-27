﻿#region Copyright SHARP Corporation
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
using dtRestrictionInfoTableAdapters;
using dtUserInfoTableAdapters;
using dtGroupInfoTableAdapters;
using dtFunctionTypeInformationTableAdapters;
using dtJobTypeInformationTableAdapters;
using dtRestrictionInformationTableAdapters;
using System.Data.SqlClient;

/// <summary>
/// Add Restriction Set Information
/// </summary>
/// <Date>2010.06.18</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class RestrictionInfo_RestrictInfoAdd : RestrictionInfoMain
{
    #region "Page_Load"
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Update By SES Jijianxiogn 2010-09-07 ST
        // Change to the Sub Title
        //this.Master.Title = UtilConst.CON_PAGE_RESTRICADD;

        // 2010.11.23 Update By SES Jijianxiong Ver.1.1 Update ST
        // Bug
        // this.Master.Title = UtilConst.CON_PAGE_RESTRICEDIT;
      //  this.Master.Title = UtilConst.CON_PAGE_RESTRICLIST;
        // 2010.11.23 Update By SES Jijianxiong Ver.1.1 Update ED
        
      //  Master.SubTitle(UtilConst.CON_PAGE_ADD, "RestrictList.aspx", true);
        // Update By SES Jijianxiogn 2010-09-07 ED


        // Check Access Role
        CheckUser();

        if (!IsPostBack)
        {
            JobTypeInformationTableAdapter JopTypeInforAdapter = new JobTypeInformationTableAdapter();
            //CustomersGridView.DataSource = JopTypeInforAdapter.GetData();
            //CustomersGridView.DataBind();

            // Error Message
            // Restriction information's Name is Must
            rfvResName.ErrorMessage = UtilConst.MSG_RESTRICT_NAMEMUST;
            // Can't Input ILLEGAL.
            revResName.ErrorMessage = UtilConst.MSG_ILLEGAL_STRING;
            // ValidationExpression
            revResName.ValidationExpression = UtilConst.CON_VAL_ILLEGAL;
            // Restriction Set Edit page Restriction Set Name is Exist
            valResName.ErrorMessage = UtilConst.MSG_RESTRICT_NAMEEXIST;

            //必填约束
            rfv_AllQuota.ErrorMessage = UtilConst.MSG_RESTRICT_AllQuota;
            rfv_ColorQuota.ErrorMessage =UtilConst.MSG_RESTRICT_ColorQuota ;
            rfv_OverLimit.ErrorMessage = UtilConst.MSG_RESTRICT_OverLimit;
            //正则约束
            rev_AllQuota.ErrorMessage = UtilConst.MSG_RESTRICT_AllQuota_NUM;
            rev_ColorQuota.ErrorMessage = UtilConst.MSG_RESTRICT_ColorQuota_NUM;
            rev_OverLimit.ErrorMessage = UtilConst.MSG_RESTRICT_OverLimit_NUM;
            //自定义约束
            cv_OverLimit.ErrorMessage = UtilConst.MSG_RESTRICT_OverLimit_CUSTOM;
            cv_ColorQuota.ErrorMessage = UtilConst.MSG_RESTRICT_ColorQuota_CUSTOM;
          
        }
       
        // Cancel Button's Confirm Msg
        btnCancel.OnClientClick = ConfirmFunctionCancel(UtilConst.MSG_UPDATE_CANCEL);
    }
    #endregion

    #region "Update Restriction Set Information"
    /// <summary>
    /// Update Restriction Set Information
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <Date>2010.06.17</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsValid)
            {
                return;
            }

            int printBW = 0;
            if (this.chk_PrintBW.Checked == true)
            {
                printBW = 1;
            }

            RestrictionInfoTableAdapter RestricInfoAdapter = new RestrictionInfoTableAdapter();
            int intResID = (int)RestricInfoAdapter.GetMaxId();
            intResID = intResID + 1;

            string strSql = "";
            using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    // 1. Add Restriction Set
                    strSql = " INSERT INTO [RestrictionInfo]    " + Environment.NewLine;
                    strSql += "            ([ID]                " + Environment.NewLine;
                    strSql += "            ,[RestrictionName]   " + Environment.NewLine;
                    strSql += "            ,[AllQuota]       " + Environment.NewLine;
                    strSql += "            ,[ColorQuota]       " + Environment.NewLine;
                    strSql += "            ,[OverLimit]       " + Environment.NewLine;
                    strSql += "            ,[PrintBW]       " + Environment.NewLine;
                    strSql += "            ,[CreateTime]        " + Environment.NewLine;
                    strSql += "            ,[UpdateTime])       " + Environment.NewLine;
                
                    // Update BY JiJianxiong 2010-07-09 ED

                    strSql += "      VALUES                     " + Environment.NewLine;
                    strSql += "            ({0}                 " + Environment.NewLine;
                    // Update BY JiJianxiong 2010-07-09 ST
                    //strSql += "            ,{1})                " + Environment.NewLine;
                    strSql += "             ,{1}                " + Environment.NewLine;
                    strSql += "             ,{2}                " + Environment.NewLine;
                    strSql += "             ,{3}                " + Environment.NewLine;
                    strSql += "             ,{4}                " + Environment.NewLine;
                    strSql += "             ,{5}                " + Environment.NewLine;
                    strSql += "             ,getdate() , getdate())  " + Environment.NewLine;
                    // Update BY JiJianxiong 2010-07-09 ED

                    strSql = string.Format(strSql, ConvertIntToSQL(intResID.ToString()), ConvertStringToSQL(txtResName.Text), ConvertMoneyToSQL(this.txtAllQuota.Text), ConvertMoneyToSQL(this.txtColorQuota.Text), ConvertMoneyToSQL(this.txtOverLimit.Text), printBW);
                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Insert Restriction Set
              
                    for (int i = 0; i < chk_job_function.Items.Count; i++)
                    {
                        //chen update
                        int status = UtilConst.STATUS_UNLIMITED;
                        if (chk_job_function.Items[i].Selected)
                        {
                            status = UtilConst.STATUS_UNLIMITED;
                        }else{
                            status = UtilConst.STATUS_PROHIBITION;
                        }

                        //if (chk_job_function.Items[i].Selected)
                        {
                            if (i  == 0)
                            {
                                strSql = UpdateRestrictInfomation(intResID, "1-2", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (i == 1)
                            {
                                strSql = UpdateRestrictInfomation(intResID, "1-1", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            else if (i == 2)
                            {
                                strSql = UpdateRestrictInfomation(intResID, "2-2", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "3-2", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "5-2", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (i == 3)
                            {
                                strSql = UpdateRestrictInfomation(intResID, "2-1", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "3-1", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "5-1", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (i == 4)//扫描
                            {
                                strSql = UpdateRestrictInfomation(intResID, "6-1", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "6-2", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "4-1", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "4-2", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "7-1", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "7-2", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (i == 5)//传真
                            {
                                strSql = UpdateRestrictInfomation(intResID, "8-1", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                strSql = UpdateRestrictInfomation(intResID, "8-2", status);
                                using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                                {
                                    cmd.ExecuteNonQuery();
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
            }
            this.Response.Redirect("RestrictList.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorAlert(ex);
        }

   }
    /// <summary>
    /// Update UpdateRestrictInfomation
    /// </summary>
    /// <param name="intResID">新增配额ID</param>
    /// <param name="job_function">选中的复选框所代表的job和function</param>
    /// <return>构造该功能的SQL字符串</param>
    /// <Date>2014.04.11</Date>
    /// <Author>Pupeng</Author>
    /// <Version>0.01</Version>
   private string UpdateRestrictInfomation(int intResID,string job_function, int status)
   {
       string strSql;
       strSql = " INSERT INTO [RestrictionInformation] " + Environment.NewLine;
       strSql += "           ([RestrictionID]          " + Environment.NewLine;
       strSql += "           ,[JobId]                  " + Environment.NewLine;
       strSql += "           ,[FunctionId]             " + Environment.NewLine;
       strSql += "           ,[Status]                 " + Environment.NewLine;
       strSql += "           ,[LimitNum])              " + Environment.NewLine;
       strSql += "     VALUES                          " + Environment.NewLine;
       strSql += "           ({0}                      " + Environment.NewLine;
       strSql += "           ,{1}                      " + Environment.NewLine;
       strSql += "           ,{2}                      " + Environment.NewLine;
       strSql += "           ,{3}                      " + Environment.NewLine;
       strSql += "           ,{4})                     " + Environment.NewLine;
       string[] paramslist = new string[5];
       // RestrictionID
       paramslist[0] = ConvertIntToSQL(intResID.ToString());
       string[] tmp = job_function.Split('-');
       // JobId
       paramslist[1] = ConvertIntToSQL(tmp[0]);
       // FunctionId
       paramslist[2] = ConvertIntToSQL(tmp[1]);

       /*
       // Update By SES.JiJianXiong 2010.09.16 ED
       // Status
       RadioButton rdoUnlimited = (RadioButton)gDetailRow.FindControl("rdoUnlimited");
       RadioButton rdoLimitTo = (RadioButton)gDetailRow.FindControl("rdoLimite");
       RadioButton rdoProhibition = (RadioButton)gDetailRow.FindControl("rdoProhibition");
       int intStatus = UtilConst.STATUS_UNLIMITED;
       if (rdoUnlimited.Checked)
       {
           // Unlimited RadioButton is Checked.
           intStatus = UtilConst.STATUS_UNLIMITED;
       }
       else if (rdoLimitTo.Checked)
       {
           // Limit To RadioButton is Checked.
           intStatus = UtilConst.STATUS_LIMIT;
       }
       else if (rdoProhibition.Checked)
       {
           // Prohibition RadioButton is Checked.
           intStatus = UtilConst.STATUS_PROHIBITION;
       }
       paramslist[3] = ConvertIntToSQL(intStatus.ToString());
       // LimitNum
       // Limit To Number.
       TextBox txt = (TextBox)gDetailRow.FindControl("txtLimitNum");
       paramslist[4] = ConvertIntToSQL(txt.Text);
       */
       //int intStatus = UtilConst.STATUS_UNLIMITED;
       //paramslist[3] = ConvertIntToSQL(intStatus.ToString());
       paramslist[3] = ConvertIntToSQL(status.ToString());
       // strSql = string.Format(strSql, paramslist);
       paramslist[4] = ConvertIntToSQL("0");
       strSql = string.Format(strSql, paramslist);
       return strSql;
     
   }

    #endregion

   #region"Check Restriction Set Name In RestrictionInfo Table."
    /// <summary>
    /// Check Restriction Set Name In RestrictionInfo Table.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    /// <Date>2010.06.15</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    protected void valResName_ServerValidate(Object source, ServerValidateEventArgs args)
    {
        string strResName = args.Value.ToString();
        // 2010.12.23 Add By SES Jijianixong ST
        strResName = strResName.Trim();
        // 2010.12.23 Add By SES Jijianxiong ED

        RestrictionInfoTableAdapter ResInfoAdapter = new RestrictionInfoTableAdapter();
        if ((int)ResInfoAdapter.CheckResInfoExistName(strResName) > 0)
        {
            args.IsValid = false;
        }
        else
        {
            // 2010.12.23 Add By SES Jijianixong ST
            txtResName.Text = strResName;
            // 2010.12.23 Add By SES Jijianxiong ED
            args.IsValid = true;
        }
    }

    #endregion

    protected void cv_ColorQuota_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string strResName = args.Value.ToString();
        strResName = strResName.Trim();
        float tmp = float.Parse(strResName);
        float tmp1 = float.Parse(txtAllQuota.Text);
        if (tmp>tmp1)//彩色配额超过了配额
        {
            args.IsValid = false;
        }
        else
        {
         
            txtColorQuota.Text = strResName;
            args.IsValid = true;
        }

    }
    protected void cv_OverLimit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        string strResName = args.Value.ToString();
        strResName = strResName.Trim();
        float tmp = float.Parse(strResName);
        
        if (tmp<0 ||tmp>10)//暂定投资上限在10左右
        {
            args.IsValid = false;
        }
        else
        {
         
            txtOverLimit.Text = strResName;
            args.IsValid = true;
        }

    }
}
