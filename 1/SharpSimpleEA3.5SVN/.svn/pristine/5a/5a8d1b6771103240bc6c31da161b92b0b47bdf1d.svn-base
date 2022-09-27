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
using dtRestrictionInfoTableAdapters;
using dtUserInfoTableAdapters;
using dtGroupInfoTableAdapters;
using dtFunctionTypeInformationTableAdapters;
using dtJobTypeInformationTableAdapters;
using dtRestrictionInformationTableAdapters;
using System.Data.SqlClient;

/// <summary>
/// Edit Restriction Set Information
/// </summary>
/// <Date>2010.06.15</Date>
/// <Author>SES Ji JianXiong</Author>
/// <Version>0.01</Version>
public partial class RestrictionInfo_RestrictInfoEdit : RestrictionInfoMain
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
        // Update By SES Jijianxiogn 2010-08-23 ST
        // Change to the Sub Title
        // this.Master.Title = UtilConst.CON_PAGE_RESTRICEDIT;
        // 2010.11.23 Update By SES Jijianxiong Ver.1.1 Update ST
        // Bug
        // this.Master.Title = UtilConst.CON_PAGE_RESTRICEDIT;
        this.Master.Title = UtilConst.CON_PAGE_RESTRICLIST;
        // 2010.11.23 Update By SES Jijianxiong Ver.1.1 Update ED
        Master.SubTitle(UtilConst.CON_PAGE_EDIT, "RestrictList.aspx", true);
        // Update By SES Jijianxiogn 2010-08-23 ED


        // Check Access Role
        CheckUser();

        // Get RestrictionInfo Id from  Page Parms's.
        hidResId.Value = Page.Request.Params["RestrictionId"].ToString();
        //pupeng 2014 05 30
        if (hidResId.Value == UtilConst.CON_ID_RESTRICLIST_GENERAL || hidResId.Value == UtilConst.CON_ID_RESTRICLIST_GROUP)
        {
           txtResName.Enabled = false;
        }
        if (!IsPostBack)
        {
         
              // GET RESTRICT INFO BY PUPENG 2014 4-14
            RestrictionInfoTableAdapter ResTableAdapter = new RestrictionInfoTableAdapter();
            dtRestrictionInfo.RestrictionInfoDataTable restrict_table = ResTableAdapter.GetRestrictionInfoDataByID(int.Parse(hidResId.Value));
            txtResName.Text = restrict_table[0].RestrictionName;
            txtAllQuota.Text = restrict_table[0].AllQuota.ToString();
            txtColorQuota.Text = restrict_table[0].ColorQuota.ToString();
            txtOverLimit.Text = restrict_table[0].OverLimit.ToString();
            int printBW = Convert.ToInt32(restrict_table[0].PrintBW);
            if (printBW == 1)
            {
                this.chk_PrintBW.Checked = true;
            }
            else
            {
                this.chk_PrintBW.Checked = false;
            }
            RestrictionInformationTableAdapter rita = new RestrictionInformationTableAdapter();
            dtRestrictionInformation.RestrictionInformationDataTable r_info_table = rita.GetDataByResId(int.Parse(hidResId.Value));
            for (int i = 0; i < r_info_table.Count;i++ )
            {
                if (r_info_table[i].JobId == 1 && r_info_table[i].FunctionId == 2 && r_info_table[i].Status == UtilConst.STATUS_UNLIMITED)
                {
                    chk_job_function.Items[0].Selected = true;
                }
                else if (r_info_table[i].JobId == 1 && r_info_table[i].FunctionId == 1 && r_info_table[i].Status == UtilConst.STATUS_UNLIMITED)
                {
                    chk_job_function.Items[1].Selected = true;
                }
                else if (r_info_table[i].JobId == 2 && r_info_table[i].FunctionId == 2 && r_info_table[i].Status == UtilConst.STATUS_UNLIMITED)
                {
                    chk_job_function.Items[2].Selected = true;
                }
                else if (r_info_table[i].JobId == 2 && r_info_table[i].FunctionId == 1 && r_info_table[i].Status == UtilConst.STATUS_UNLIMITED)
                {
                    chk_job_function.Items[3].Selected = true;
                }
                else if (r_info_table[i].JobId == 6 && r_info_table[i].Status == UtilConst.STATUS_UNLIMITED)
                {
                    chk_job_function.Items[4].Selected = true;
                }
                else if (r_info_table[i].JobId == 8 && r_info_table[i].Status == UtilConst.STATUS_UNLIMITED)
                {
                    chk_job_function.Items[5].Selected = true;
                }
            }
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
            rfv_ColorQuota.ErrorMessage = UtilConst.MSG_RESTRICT_ColorQuota;
            rfv_OverLimit.ErrorMessage = UtilConst.MSG_RESTRICT_OverLimit;
            //正则约束
            rev_AllQuota.ErrorMessage = UtilConst.MSG_RESTRICT_AllQuota_NUM;
            rev_ColorQuota.ErrorMessage = UtilConst.MSG_RESTRICT_ColorQuota_NUM;
            rev_OverLimit.ErrorMessage = UtilConst.MSG_RESTRICT_OverLimit_NUM;
            //自定义约束
            cv_OverLimit.ErrorMessage = UtilConst.MSG_RESTRICT_OverLimit_CUSTOM;
            cv_ColorQuota.ErrorMessage = UtilConst.MSG_RESTRICT_ColorQuota_CUSTOM;




        }

        // Update Button's Confirm Msg
        btnUpdate.OnClientClick = ConfirmFunctionUpd(UtilConst.MSG_UPDATE_UPDATE, btnUpdate.ValidationGroup);
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
            int printBW = 0;
            if (this.chk_PrintBW.Checked == true)
            {
                printBW = 1;
            }


            int intResID=int.Parse(hidResId.Value);
            if (!Page.IsValid)
            {
                return;
            }

            string strSql = "";
            using (SqlConnection con = new SqlConnection(this.DBConnectionStrings))
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                try
                {
                    // 1. Update Restriction Set's Name
                    strSql = " UPDATE [RestrictionInfo]         " + Environment.NewLine;
                    strSql += "    SET                          " + Environment.NewLine;
                    strSql += "        [RestrictionName] = {0}  " + Environment.NewLine;
                    strSql += "       ,[AllQuota] = {2}  " + Environment.NewLine;
                    strSql += "       ,[ColorQuota] = {3}  " + Environment.NewLine;
                    strSql += "       ,[OverLimit] = {4}  " + Environment.NewLine;
                    strSql += "       ,[PrintBW] = {5}  " + Environment.NewLine;
                    // Add BY JiJianxiong 2010-07-09 ST
                    strSql += "       ,[UpdateTime] = getdate() " + Environment.NewLine;
                    // Add BY JiJianxiong 2010-07-09 ED
                    strSql += "  WHERE [ID] = {1}               " + Environment.NewLine;
                    strSql = string.Format(strSql, ConvertStringToSQL(txtResName.Text), ConvertIntToSQL(hidResId.Value), ConvertMoneyToSQL(this.txtAllQuota.Text), ConvertMoneyToSQL(this.txtColorQuota.Text), ConvertMoneyToSQL(this.txtOverLimit.Text), printBW);
                    using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    for (int i = 0; i < chk_job_function.Items.Count; i++)
                    {
                        //if (chk_job_function.Items[i].Selected)
                        //{
                        //    if (i < 4)
                        //    {
                        //        strSql=CheckRestrictInfomation(intResID, chk_job_function.Items[i].Value);
                        //        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        //        {
                        //            if((int)cmd.ExecuteScalar()==0)//不存在该情况的信息，则添加
                        //            {
                        //                strSql = UpdateRestrictInfomation(intResID, chk_job_function.Items[i].Value);
                        //                using (SqlCommand cmd1 = new SqlCommand(strSql, con, tran))
                        //                {
                        //                    cmd1.ExecuteNonQuery();
                        //                }
                        //            }
                                        
                        //        }
                                
                        //    }
                        //    else if (i == 4)//扫描
                        //    {
                        //        strSql=CheckRestrictInfomation(intResID, chk_job_function.Items[i].Value);
                        //        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        //        {
                        //            if((int)cmd.ExecuteScalar()==0)//不存在该情况的信息，则添加
                        //            {
                        //                strSql = UpdateRestrictInfomation(intResID, chk_job_function.Items[i].Value);
                        //                using (SqlCommand cmd1 = new SqlCommand(strSql, con, tran))
                        //                {
                        //                    cmd1.ExecuteNonQuery();
                        //                }
                        //            }
                                        
                        //        }
                        //         strSql=CheckRestrictInfomation(intResID, "6-2");
                        //        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        //        {
                        //            if((int)cmd.ExecuteScalar()==0)//不存在该情况的信息，则添加
                        //            {
                        //                strSql = UpdateRestrictInfomation(intResID, "6-2");
                        //                using (SqlCommand cmd1 = new SqlCommand(strSql, con, tran))
                        //                {
                        //                    cmd1.ExecuteNonQuery();
                        //                }
                        //            }
                                        
                        //        }
                        //    }
                        //    else if (i == 5)//传真
                        //    { strSql=CheckRestrictInfomation(intResID, "8-1");
                        //        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        //        {
                        //            if((int)cmd.ExecuteScalar()==0)//不存在该情况的信息，则添加
                        //            {
                        //                strSql = UpdateRestrictInfomation(intResID, "8-1");
                        //                using (SqlCommand cmd1 = new SqlCommand(strSql, con, tran))
                        //                {
                        //                    cmd1.ExecuteNonQuery();
                        //                }
                        //            }
                                        
                        //        }
                        //         strSql=CheckRestrictInfomation(intResID, "8-2");
                        //        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        //        {
                        //            if((int)cmd.ExecuteScalar()==0)//不存在该情况的信息，则添加
                        //            {
                        //                strSql = UpdateRestrictInfomation(intResID, "8-2");
                        //                using (SqlCommand cmd1 = new SqlCommand(strSql, con, tran))
                        //                {
                        //                    cmd1.ExecuteNonQuery();
                        //                }
                        //            }
                                        
                        //        }
                        //    }

                        //}
                        //else//如果该项没有选择 删除restrictinfo的信息
                        //{
                        //     if (i < 4)
                        //    {
                        //        strSql=DeleteRestrictInfomation(intResID, chk_job_function.Items[i].Value);
                        //        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        //        {
                        //            cmd.ExecuteNonQuery();
                        //        }
                                
                        //    }
                        //    else if (i == 4)//扫描
                        //    {
                        //          strSql = " delete from [RestrictionInformation] " + Environment.NewLine;
                        //            strSql += "  WHERE [RestrictionID] = {0}    " + Environment.NewLine;
                        //            strSql += "    AND [JobId] = {1}            " + Environment.NewLine;
                        //        strSql = string.Format(strSql, intResID.ToString(),"6");
                        //        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        //        {
                        //            cmd.ExecuteNonQuery();
                        //        }
                        //    }
                        //    else if (i == 5)//传真
                        //    { 

                        //          strSql = " delete from [RestrictionInformation] " + Environment.NewLine;
                        //            strSql += "  WHERE [RestrictionID] = {0}    " + Environment.NewLine;
                        //            strSql += "    AND [JobId] = {1}            " + Environment.NewLine;
                        //        strSql = string.Format(strSql, intResID.ToString(),"8");
                        //        using (SqlCommand cmd = new SqlCommand(strSql, con, tran))
                        //        {
                        //            cmd.ExecuteNonQuery();
                        //        }
                        //    }
                        //}
                        int status = UtilConst.STATUS_UNLIMITED;
                        if (chk_job_function.Items[i].Selected)
                        {
                            status = UtilConst.STATUS_UNLIMITED;
                        }
                        else
                        {
                            status = UtilConst.STATUS_PROHIBITION;
                        }

                        //if (chk_job_function.Items[i].Selected)
                        {
                            if (i == 0)
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

    #endregion
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
       strSql = " UPDATE  [RestrictionInformation] " + Environment.NewLine;
       strSql += "        SET                 " + Environment.NewLine;
       strSql += "        [Status]={3}                 " + Environment.NewLine;
       strSql += "  WHERE [RestrictionID] = {0}    " + Environment.NewLine;
       strSql += "    AND [JobId] = {1}            " + Environment.NewLine;
       strSql += "    AND [FunctionId] = {2}       " + Environment.NewLine;
       string[] paramslist = new string[4];
       // RestrictionID
       paramslist[0] = ConvertIntToSQL(intResID.ToString());
       string[] tmp = job_function.Split('-');
       // JobId
       paramslist[1] = ConvertIntToSQL(tmp[0]);
       // FunctionId
       paramslist[2] = ConvertIntToSQL(tmp[1]);
       
       paramslist[3] = ConvertIntToSQL(status.ToString());

       strSql = string.Format(strSql, paramslist);
       return strSql;
     
   }
     /// <summary>
    /// CheckRestrictInfomation
    /// </summary>
    /// <param name="intResID">被修改的配额ID</param>
    /// <param name="job_function">选中的复选框所代表的job和function</param>
    /// <return>构造该功能的SQL字符串</param>
    /// <Date>2014.04.11</Date>
    /// <Author>Pupeng</Author>
    /// <Version>0.01</Version>
   private string CheckRestrictInfomation(int intResID,string job_function)
   {
       string strSql;
       strSql = " select count(*) from [RestrictionInformation] " + Environment.NewLine;
                                    strSql += "  WHERE [RestrictionID] = {0}    " + Environment.NewLine;
                                    strSql += "    AND [JobId] = {1}            " + Environment.NewLine;
                                    strSql += "    AND [FunctionId] = {2}       " + Environment.NewLine;
       string[] paramslist = new string[3];
      
       paramslist[0] = ConvertIntToSQL(intResID.ToString());
       string[] tmp = job_function.Split('-');
       // JobId
       paramslist[1] = ConvertIntToSQL(tmp[0]);
       // FunctionId
       paramslist[2] = ConvertIntToSQL(tmp[1]);
       strSql = string.Format(strSql, paramslist);
       return strSql;
     
   }
     /// <summary>
    /// DeleteRestrictInfomation
    /// </summary>
    /// <param name="intResID">被修改的配额ID</param>
    /// <param name="job_function">选中的复选框所代表的job和function</param>
    /// <return>构造该功能的SQL字符串</param>
    /// <Date>2014.04.11</Date>
    /// <Author>Pupeng</Author>
    /// <Version>0.01</Version>
   private string DeleteRestrictInfomation(int intResID,string job_function)
   {
       string strSql;
       strSql = " delete from [RestrictionInformation] " + Environment.NewLine;
                                    strSql += "  WHERE [RestrictionID] = {0}    " + Environment.NewLine;
                                    strSql += "    AND [JobId] = {1}            " + Environment.NewLine;
                                    strSql += "    AND [FunctionId] = {2}       " + Environment.NewLine;
       string[] paramslist = new string[3];
      
       paramslist[0] = ConvertIntToSQL(intResID.ToString());
       string[] tmp = job_function.Split('-');
       // JobId
       paramslist[1] = ConvertIntToSQL(tmp[0]);
       // FunctionId
       paramslist[2] = ConvertIntToSQL(tmp[1]);
       strSql = string.Format(strSql, paramslist);
       return strSql;
     
   }

    

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
        // Get RestrictionInfo Id 
        int intResId = int.Parse(hidResId.Value);

        RestrictionInfoTableAdapter ResInfoAdapter = new RestrictionInfoTableAdapter();
        if ((int)ResInfoAdapter.CheckResInfoExistNameBy(strResName, intResId) > 0)
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
        if (tmp > tmp1)//彩色配额超过了配额
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

        if (tmp < 0 || tmp > 10)//暂定投资上限在10左右
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
