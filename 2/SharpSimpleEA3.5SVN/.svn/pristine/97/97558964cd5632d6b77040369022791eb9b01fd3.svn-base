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
    //cui20170624(已完成)
    public class DalLDAPTotal : CommonDal
    {
        //SQLHelper sqlHelper = new SQLHelper();

        protected SqlParameter[] CreateParamList(LDAPEntry beanitem)
        {
            SqlParameter[] ParamList ={
                 //用户设置
                sqlHelper.CreateInParam("@Con_IP",SqlDbType.NVarChar,50,beanitem.Con_IP),
                sqlHelper.CreateInParam("@Con_Port",SqlDbType.NVarChar,50,beanitem.Con_Port),
                sqlHelper.CreateInParam("@Con_Verification",SqlDbType.NVarChar,50,beanitem.Con_Verification),
                sqlHelper.CreateInParam("@Con_Account",SqlDbType.NVarChar,50,beanitem.Con_Account),
                sqlHelper.CreateInParam("@Con_Password",SqlDbType.NVarChar,50,beanitem.Con_Password),
                //DB使用规则
                sqlHelper.CreateInParam("@DB_Allowed",SqlDbType.NVarChar,50,beanitem.DB_Allowed),
                //认证设置
                sqlHelper.CreateInParam("@Ver_Verification",SqlDbType.NVarChar,50,beanitem.Ver_Verification),
                sqlHelper.CreateInParam("@Ver_Login",SqlDbType.NVarChar,50,beanitem.Ver_Login),
                sqlHelper.CreateInParam("@Ver_Type",SqlDbType.NVarChar,50,beanitem.Ver_Type),
                sqlHelper.CreateInParam("@Ver_NTorDNS",SqlDbType.NVarChar,50,beanitem.Ver_NTorDNS),
                //组设置
                sqlHelper.CreateInParam("@Group_Verification",SqlDbType.NVarChar,50,beanitem.Group_Verification),
                sqlHelper.CreateInParam("@Group_UserAttribute_or_DN",SqlDbType.NVarChar,50,beanitem.Group_UserAttribute_or_DN),
                sqlHelper.CreateInParam("@Group_AttributeName",SqlDbType.NVarChar,50,beanitem.Group_AttributeName),
                //用户设置
                sqlHelper.CreateInParam("@User_DNSetting",SqlDbType.NVarChar,100,beanitem.User_DNSetting),
                sqlHelper.CreateInParam("@User_Search",SqlDbType.NVarChar,50,beanitem.User_Search),
                sqlHelper.CreateInParam("@User_Name",SqlDbType.NVarChar,50,beanitem.User_Name),
                sqlHelper.CreateInParam("@User_Email",SqlDbType.NVarChar,50,beanitem.User_Email),
                sqlHelper.CreateInParam("@User_ICNum",SqlDbType.NVarChar,50,beanitem.User_ICNum),
                sqlHelper.CreateInParam("@User_LDAPName",SqlDbType.NVarChar,50,beanitem.User_LDAPName),
                sqlHelper.CreateInParam("@User_LDAPPassword",SqlDbType.NVarChar,50,beanitem.User_LDAPPassword),
               
               };
            return ParamList;
        }

        protected LDAPEntry Bind(SqlDataReader rec)
        {
            LDAPEntry beanitem = new LDAPEntry();
            //连接设置
            beanitem.Con_IP = ConvertObjToString(rec["Con_IP"]);
            beanitem.Con_Port = ConvertObjToString(rec["Con_Port"]);
            beanitem.Con_Verification = ConvertObjToString(rec["Con_Verification"]);
            beanitem.Con_Account = ConvertObjToString(rec["Con_Account"]);
            beanitem.Con_Password = ConvertObjToString(rec["Con_Password"]);
            //DB使用规则 Du Qinyi
            beanitem.DB_Allowed = ConvertObjToInt(rec["DB_Allowed"]);
            //认证设置
            beanitem.Ver_Verification = ConvertObjToString(rec["Ver_Verification"]);
            beanitem.Ver_Login = ConvertObjToString(rec["Ver_Login"]);
            beanitem.Ver_Type = ConvertObjToString(rec["Ver_Type"]);
            beanitem.Ver_NTorDNS = ConvertObjToString(rec["Ver_NTorDNS"]);
            ////组设置
            beanitem.Group_Verification = ConvertObjToString(rec["Group_Verification"]);
            beanitem.Group_AttributeName = ConvertObjToString(rec["Group_AttributeName"]);
            ////用户设置
            beanitem.User_DNSetting = ConvertObjToString(rec["User_DNSetting"]);
            beanitem.User_Search = ConvertObjToString(rec["User_Search"]);
            beanitem.User_Name = ConvertObjToString(rec["User_Name"]);
            beanitem.User_Email = ConvertObjToString(rec["User_Email"]);
            beanitem.User_ICNum = ConvertObjToString(rec["User_ICNum"]);
            beanitem.User_LDAPName = ConvertObjToString(rec["User_LDAPName"]);
            beanitem.User_LDAPPassword = ConvertObjToString(rec["User_LDAPPassword"]);
           
            rec.Close();

            return beanitem;
        } 

        //更新 Total
        public int Update(LDAPEntry userBean)
        {
            try
            {
                StringBuilder sb = new StringBuilder("");                
                sb.Append("UPDATE LDAPSetting SET ");
                //连接设置
                sb.Append("Con_IP=@Con_IP,");
                sb.Append("Con_Port=@Con_Port,");
                sb.Append("Con_Verification=@Con_Verification,");
                sb.Append("Con_Account=@Con_Account,");
                sb.Append("Con_Password=@Con_Password,");

                //Db使用规则 Du Qinyi
                sb.Append("DB_Allowed=@DB_Allowed,");

                 //认证设置
                sb.Append("Ver_Verification=@Ver_Verification,");
                sb.Append("Ver_Login=@Ver_Login,");
                sb.Append("Ver_Type=@Ver_Type,");
                sb.Append("Ver_NTorDNS=@Ver_NTorDNS,");
                
                //组设置
                sb.Append("Group_Verification=@Group_Verification,");
                sb.Append("Group_UserAttribute_or_DN = @Group_UserAttribute_or_DN,");
                sb.Append("Group_AttributeName=@Group_AttributeName,");

                //用户设置
                sb.Append("User_DNSetting=@User_DNSetting,");
                sb.Append("User_Search=@User_Search,");
                sb.Append("User_Name=@User_Name,");
                sb.Append("User_Email=@User_Email,");
                sb.Append("User_ICNum=@User_ICNum,");
                sb.Append("User_LDAPName=@User_LDAPName,");
                sb.Append("User_LDAPPassword=@User_LDAPPassword");

                SqlParameter[] sqlParam = CreateParamList(userBean);
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
        public LDAPEntry GetInfoByKey()
        {
            LDAPEntry entry = new LDAPEntry();
            //暂时选用端口作为where的条件
            //SqlParameter[] ParamList = { sqlHelper.CreateInParam("@Con_Port", SqlDbType.NVarChar, 50, Con_Port) };
            try
            {
                SqlDataReader rec;
                string sql = "select * from LDAPSetting ";
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
        // end GetInfoByKey



    }
}