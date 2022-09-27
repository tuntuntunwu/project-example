using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;
using SimpleEACommon;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;


namespace DAL
{
    public class DalSettingDisp : CommonDal
    {
        //SQLHelper sqlHelper = new SQLHelper();

        protected SqlParameter[] CreateParamList(SettingDispEntry beanitem)
        {
                SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@Dis_U_UserName",SqlDbType.Int,4,beanitem.Dis_U_UserName),
                sqlHelper.CreateInParam("@Dis_U_LoginName",SqlDbType.Int,4,beanitem.Dis_U_LoginName),
                sqlHelper.CreateInParam("@Dis_U_GroupName",SqlDbType.Int,4,beanitem.Dis_U_GroupName),
	            sqlHelper.CreateInParam("@Dis_U_CardID",SqlDbType.Int,4,beanitem.Dis_U_CardID),
	            sqlHelper.CreateInParam("@Dis_U_Restrict",SqlDbType.Int,4,beanitem.Dis_U_Restrict),
	            sqlHelper.CreateInParam("@Dis_G_GroupName",SqlDbType.Int,4,beanitem.Dis_G_GroupName),
	            sqlHelper.CreateInParam("@Dis_G_Number",SqlDbType.Int,4,beanitem.Dis_G_Number),
	            sqlHelper.CreateInParam("@Dis_G_Restrict",SqlDbType.Int,4,beanitem.Dis_G_Restrict),
	            sqlHelper.CreateInParam("@Dis_R_Restrict",SqlDbType.Int,4,beanitem.Dis_R_Restrict),
	            sqlHelper.CreateInParam("@Dis_R_Copy",SqlDbType.Int,4,beanitem.Dis_R_Copy),
	            sqlHelper.CreateInParam("@Dis_R_Print",SqlDbType.Int,4,beanitem.Dis_R_Print),
	            sqlHelper.CreateInParam("@Dis_R_Scan",SqlDbType.Int,4,beanitem.Dis_R_Scan),
	            sqlHelper.CreateInParam("@Dis_R_Fax",SqlDbType.Int,4,beanitem.Dis_R_Fax),
	            sqlHelper.CreateInParam("@Dis_Job_Total",SqlDbType.Int,4,beanitem.Dis_Job_Total),
	            sqlHelper.CreateInParam("@Dis_Job_CopyTotal",SqlDbType.Int,4,beanitem.Dis_Job_CopyTotal),
	            sqlHelper.CreateInParam("@Dis_Job_PrintTotal",SqlDbType.Int,4,beanitem.Dis_Job_PrintTotal),
	            sqlHelper.CreateInParam("@Dis_Job_ScanTotal",SqlDbType.Int,4,beanitem.Dis_Job_ScanTotal),
	            sqlHelper.CreateInParam("@Dis_Job_FaxTotal",SqlDbType.Int,4,beanitem.Dis_Job_FaxTotal),
	            sqlHelper.CreateInParam("@Dis_Result_Copy",SqlDbType.Int,4,beanitem.Dis_Result_Copy),
	            sqlHelper.CreateInParam("@Dis_Result_Print",SqlDbType.Int,4,beanitem.Dis_Result_Print),
	            sqlHelper.CreateInParam("@Dis_Result_Scan",SqlDbType.Int,4,beanitem.Dis_Result_Scan),
	            sqlHelper.CreateInParam("@Dis_Result_Fax",SqlDbType.Int,4,beanitem.Dis_Result_Fax),
	            sqlHelper.CreateInParam("@Dis_Result_Other",SqlDbType.Int,4,beanitem.Dis_Result_Other),
	            sqlHelper.CreateInParam("@Dis_Log_MaxCount",SqlDbType.Int,4,beanitem.Dis_Log_MaxCount),
	            sqlHelper.CreateInParam("@Dis_Avai_Borrow",SqlDbType.Int,4,beanitem.Dis_Avai_Borrow),
	            sqlHelper.CreateInParam("@Dis_Count_mode",SqlDbType.Int,4,beanitem.Dis_Count_mode),
	            sqlHelper.CreateInParam("@Dis_A3_A4",SqlDbType.Int,4,beanitem.Dis_A3_A4),
                sqlHelper.CreateInParam("@Login_Auth_method",SqlDbType.Int,4,beanitem.Login_Auth_method),
                
               };
            return ParamList;
        }

      


         protected SettingDispEntry Bind(SqlDataReader rec)
        {
            SettingDispEntry beanitem = new SettingDispEntry();
            beanitem.Dis_U_UserName = ConvertObjToInt(rec["Dis_U_UserName"]);
            beanitem.Dis_U_LoginName = ConvertObjToInt(rec["Dis_U_LoginName"]);
            beanitem.Dis_U_GroupName = ConvertObjToInt(rec["Dis_U_GroupName"]);
	        beanitem.Dis_U_CardID = ConvertObjToInt(rec["Dis_U_CardID"]);
	        beanitem.Dis_U_Restrict = ConvertObjToInt(rec["Dis_U_Restrict"]);
	        beanitem.Dis_G_GroupName = ConvertObjToInt(rec["Dis_G_GroupName"]);
	        beanitem.Dis_G_Number = ConvertObjToInt(rec["Dis_G_Number"]);
	        beanitem.Dis_G_Restrict = ConvertObjToInt(rec["Dis_G_Restrict"]);
	        beanitem.Dis_R_Restrict = ConvertObjToInt(rec["Dis_R_Restrict"]);
	        beanitem.Dis_R_Copy = ConvertObjToInt(rec["Dis_R_Copy"]);
	        beanitem.Dis_R_Print = ConvertObjToInt(rec["Dis_R_Print"]);
	        beanitem.Dis_R_Scan = ConvertObjToInt(rec["Dis_R_Scan"]);
	        beanitem.Dis_R_Fax = ConvertObjToInt(rec["Dis_R_Fax"]);
	        beanitem.Dis_Job_Total = ConvertObjToInt(rec["Dis_Job_Total"]);
	        beanitem.Dis_Job_CopyTotal = ConvertObjToInt(rec["Dis_Job_CopyTotal"]);
	        beanitem.Dis_Job_PrintTotal = ConvertObjToInt(rec["Dis_Job_PrintTotal"]);
	        beanitem.Dis_Job_ScanTotal = ConvertObjToInt(rec["Dis_Job_ScanTotal"]);
	        beanitem.Dis_Job_FaxTotal = ConvertObjToInt(rec["Dis_Job_FaxTotal"]);
	        beanitem.Dis_Result_Copy = ConvertObjToInt(rec["Dis_Result_Copy"]);
	        beanitem.Dis_Result_Print = ConvertObjToInt(rec["Dis_Result_Print"]);
	        beanitem.Dis_Result_Scan = ConvertObjToInt(rec["Dis_Result_Scan"]);
	        beanitem.Dis_Result_Fax = ConvertObjToInt(rec["Dis_Result_Fax"]);
	        beanitem.Dis_Result_Other = ConvertObjToInt(rec["Dis_Result_Other"]);
	        beanitem.Dis_Log_MaxCount = ConvertObjToInt(rec["Dis_Log_MaxCount"]);
	        beanitem.Dis_Avai_Borrow = ConvertObjToInt(rec["Dis_Avai_Borrow"]);
	        beanitem.Dis_Count_mode = ConvertObjToInt(rec["Dis_Count_mode"]);
	        beanitem.Dis_A3_A4 = ConvertObjToInt(rec["Dis_A3_A4"]);
            beanitem.Login_Auth_method = ConvertObjToInt(rec["Login_Auth_method"]);

            
            rec.Close();

            return beanitem;
        }

         protected SettingDispEntry Bind(DataRow rec)
         {
            SettingDispEntry beanitem = new SettingDispEntry();
            beanitem.Dis_U_UserName = ConvertObjToInt(rec["Dis_U_UserName"]);
            beanitem.Dis_U_LoginName = ConvertObjToInt(rec["Dis_U_LoginName"]);
            beanitem.Dis_U_GroupName = ConvertObjToInt(rec["Dis_U_GroupName"]);
	        beanitem.Dis_U_CardID = ConvertObjToInt(rec["Dis_U_CardID"]);
	        beanitem.Dis_U_Restrict = ConvertObjToInt(rec["Dis_U_Restrict"]);
	        beanitem.Dis_G_GroupName = ConvertObjToInt(rec["Dis_G_GroupName"]);
	        beanitem.Dis_G_Number = ConvertObjToInt(rec["Dis_G_Number"]);
	        beanitem.Dis_G_Restrict = ConvertObjToInt(rec["Dis_G_Restrict"]);
	        beanitem.Dis_R_Restrict = ConvertObjToInt(rec["Dis_R_Restrict"]);
	        beanitem.Dis_R_Copy = ConvertObjToInt(rec["Dis_R_Copy"]);
	        beanitem.Dis_R_Print = ConvertObjToInt(rec["Dis_R_Print"]);
	        beanitem.Dis_R_Scan = ConvertObjToInt(rec["Dis_R_Scan"]);
	        beanitem.Dis_R_Fax = ConvertObjToInt(rec["Dis_R_Fax"]);
	        beanitem.Dis_Job_Total = ConvertObjToInt(rec["Dis_Job_Total"]);
	        beanitem.Dis_Job_CopyTotal = ConvertObjToInt(rec["Dis_Job_CopyTotal"]);
	        beanitem.Dis_Job_PrintTotal = ConvertObjToInt(rec["Dis_Job_PrintTotal"]);
	        beanitem.Dis_Job_ScanTotal = ConvertObjToInt(rec["Dis_Job_ScanTotal"]);
	        beanitem.Dis_Job_FaxTotal = ConvertObjToInt(rec["Dis_Job_FaxTotal"]);
	        beanitem.Dis_Result_Copy = ConvertObjToInt(rec["Dis_Result_Copy"]);
	        beanitem.Dis_Result_Print = ConvertObjToInt(rec["Dis_Result_Print"]);
	        beanitem.Dis_Result_Scan = ConvertObjToInt(rec["Dis_Result_Scan"]);
	        beanitem.Dis_Result_Fax = ConvertObjToInt(rec["Dis_Result_Fax"]);
	        beanitem.Dis_Result_Other = ConvertObjToInt(rec["Dis_Result_Other"]);
	        beanitem.Dis_Log_MaxCount = ConvertObjToInt(rec["Dis_Log_MaxCount"]);
	        beanitem.Dis_Avai_Borrow = ConvertObjToInt(rec["Dis_Avai_Borrow"]);
	        beanitem.Dis_Count_mode = ConvertObjToInt(rec["Dis_Count_mode"]);
	        beanitem.Dis_A3_A4 = ConvertObjToInt(rec["Dis_A3_A4"]);
            beanitem.Login_Auth_method = ConvertObjToInt(rec["Login_Auth_method"]);


             return beanitem;

         }




        //添加
         public int Add(SettingDispEntry bean)
        {
  
            try
            {
                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO SettingDisp (");
                sb.Append("Dis_U_UserName,");
                sb.Append("Dis_U_LoginName,");
                sb.Append("Dis_U_GroupName,");
	            sb.Append("Dis_U_CardID,");
	            sb.Append("Dis_U_Restrict,");
	            sb.Append("Dis_G_GroupName,");
	            sb.Append("Dis_G_Number,");
	            sb.Append("Dis_G_Restrict,");
	            sb.Append("Dis_R_Restrict,");
	            sb.Append("Dis_R_Copy,");
	            sb.Append("Dis_R_Print,");
	            sb.Append("Dis_R_Scan,");
	            sb.Append("Dis_R_Fax,");
	            sb.Append("Dis_Job_Total,");
	            sb.Append("Dis_Job_CopyTotal,");
	            sb.Append("Dis_Job_PrintTotal,");
	            sb.Append("Dis_Job_ScanTotal,");
	            sb.Append("Dis_Job_FaxTotal,");
	            sb.Append("Dis_Result_Copy,");
	            sb.Append("Dis_Result_Print,");
	            sb.Append("Dis_Result_Scan,");
	            sb.Append("Dis_Result_Fax,");
	            sb.Append("Dis_Result_Other,");
	            sb.Append("Dis_Log_MaxCount,");
	            sb.Append("Dis_Avai_Borrow,");
	            sb.Append("Dis_Count_mode,");
	            sb.Append("Dis_A3_A4,");
                sb.Append("Login_Auth_method");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@Dis_U_UserName,");
                sb.Append("@Dis_U_LoginName,");
                sb.Append("@Dis_U_GroupName,");
                sb.Append("@Dis_U_CardID,");
                sb.Append("@Dis_U_Restrict,");
                sb.Append("@Dis_G_GroupName,");
                sb.Append("@Dis_G_Number,");
                sb.Append("@Dis_G_Restrict,");
                sb.Append("@Dis_R_Restrict,");
                sb.Append("@Dis_R_Copy,");
                sb.Append("@Dis_R_Print,");
                sb.Append("@Dis_R_Scan,");
                sb.Append("@Dis_R_Fax,");
                sb.Append("@Dis_Job_Total,");
                sb.Append("@Dis_Job_CopyTotal,");
                sb.Append("@Dis_Job_PrintTotal,");
                sb.Append("@Dis_Job_ScanTotal,");
                sb.Append("@Dis_Job_FaxTotal,");
                sb.Append("@Dis_Result_Copy,");
                sb.Append("@Dis_Result_Print,");
                sb.Append("@Dis_Result_Scan,");
                sb.Append("@Dis_Result_Fax,");
                sb.Append("@Dis_Result_Other,");
                sb.Append("@Dis_Log_MaxCount,");
                sb.Append("@Dis_Avai_Borrow,");
                sb.Append("@Dis_Count_mode,");
                sb.Append("@Dis_A3_A4,");
                sb.Append("@Login_Auth_method"); 
                sb.Append(")");

                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        
        //更新(连接设置)
         public int Update_Connection(SettingDispEntry bean)
        {

            try
            {
                StringBuilder sb = new StringBuilder("");
                //连接设置

                sb.Append("UPDATE LDAPSetting SET ");
                sb.Append("Dis_U_UserName = @Dis_U_UserName,");
                sb.Append("Dis_U_LoginName= @Dis_U_LoginName,");
                sb.Append("Dis_U_GroupName= @Dis_U_GroupName,");
                sb.Append("Dis_U_CardID= @Dis_U_CardID,");
                sb.Append("Dis_U_Restrict= @Dis_U_Restrict,");
                sb.Append("Dis_G_GroupName= @Dis_G_GroupName,");
                sb.Append("Dis_G_Number= @Dis_G_Number,");
                sb.Append("Dis_G_Restrict= @Dis_G_Restrict,");
                sb.Append("Dis_R_Restrict= @Dis_R_Restrict,");
                sb.Append("Dis_R_Copy= @Dis_R_Copy,");
                sb.Append("Dis_R_Print= @Dis_R_Print,");
                sb.Append("Dis_R_Scan= @Dis_R_Scan,");
                sb.Append("Dis_R_Fax= @Dis_R_Fax,");
                sb.Append("Dis_Job_Total= @Dis_Job_Total,");
                sb.Append("Dis_Job_CopyTotal= @Dis_Job_CopyTotal,");
                sb.Append("Dis_Job_PrintTotal= @Dis_Job_PrintTotal,");
                sb.Append("Dis_Job_ScanTotal= @Dis_Job_ScanTotal,");
                sb.Append("Dis_Job_FaxTotal= @Dis_Job_FaxTotal,");
                sb.Append("Dis_Result_Copy= @Dis_Result_Copy,");
                sb.Append("Dis_Result_Print= @Dis_Result_Print,");
                sb.Append("Dis_Result_Scan= @Dis_Result_Scan,");
                sb.Append("Dis_Result_Fax= @Dis_Result_Fax,");
                sb.Append("Dis_Result_Other= @Dis_Result_Other,");
                sb.Append("Dis_Log_MaxCount= @Dis_Log_MaxCount,");
                sb.Append("Dis_Avai_Borrow= @Dis_Avai_Borrow,");
                sb.Append("Dis_Count_mode= @Dis_Count_mode,");
                sb.Append("Dis_A3_A4= @Dis_A3_A4,");
                sb.Append("Login_Auth_method= @Login_Auth_method");


                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


       
        /// <summary>
        /// 查一条数据（暂时先做连接界面20170624）
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
         public SettingDispEntry GetInfoByKey()
        {
            SettingDispEntry entry = new SettingDispEntry();
            try
            {
                SqlDataReader rec;
                string sql = "select * from SettingDisp ";
                sqlHelper.RunSQL(sql, out rec);
                if (rec.Read())
                    entry = Bind(rec);
                return entry;

            }
            catch (Exception ex)
            {
                ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
