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
    public class DalDBThirdAuth : CommonDal
    {
        //SQLHelper sqlHelper = new SQLHelper();

        protected SqlParameter[] CreateParamList(DBThirdAuthSettingEntry beanitem)
        {
                SqlParameter[] ParamList ={
                 //DBServer
                sqlHelper.CreateInParam("@DBAuthServerIP",SqlDbType.NVarChar,50,beanitem.DBAuthServerIP),
                sqlHelper.CreateInParam("@DBAuthServerPort",SqlDbType.NVarChar,10,beanitem.DBAuthServerPort),
                sqlHelper.CreateInParam("@DBUserName",SqlDbType.NVarChar,50,beanitem.DBUserName),
                sqlHelper.CreateInParam("@DBPassword",SqlDbType.NVarChar,50,beanitem.DBPassword),
                sqlHelper.CreateInParam("@DBAuthDBNM",SqlDbType.NVarChar,50,beanitem.DBAuthDBNM),
                sqlHelper.CreateInParam("@DBTableNM",SqlDbType.NVarChar,50,beanitem.DBTableNM),
                //用户信息匹配
                sqlHelper.CreateInParam("@MTFullName",SqlDbType.NVarChar,50,beanitem.MTFullName),
                sqlHelper.CreateInParam("@MTLoginName",SqlDbType.NVarChar,50,beanitem.MTLoginName),
                sqlHelper.CreateInParam("@MTPwd",SqlDbType.NVarChar,50,beanitem.MTPwd),
                sqlHelper.CreateInParam("@MTIDCard",SqlDbType.NVarChar,50,beanitem.MTIDCard),
                sqlHelper.CreateInParam("@MTGroup",SqlDbType.NVarChar,50,beanitem.MTGroup),
                sqlHelper.CreateInParam("@MTEmail",SqlDbType.NVarChar,50,beanitem.MTEmail),
                sqlHelper.CreateInParam("@MTPINCode",SqlDbType.NVarChar,50,beanitem.MTPINCode),
                sqlHelper.CreateInParam("@GroupID1",SqlDbType.Int,4,beanitem.GroupID1),
                sqlHelper.CreateInParam("@GroupID2",SqlDbType.Int,4,beanitem.GroupID2),
                
                //卡同步规则
                sqlHelper.CreateInParam("@IDCordRule",SqlDbType.Int,4,beanitem.IDCordRule),
                sqlHelper.CreateInParam("@IDCardDataLen",SqlDbType.NVarChar,20,beanitem.IDCardDataLen),
                sqlHelper.CreateInParam("@IDCardADDPos",SqlDbType.NVarChar,10,beanitem.IDCardADDPos),
                sqlHelper.CreateInParam("@IDCardADDCHR",SqlDbType.NVarChar,10,beanitem.IDCardADDCHR),
                //用户信息判断规则
                sqlHelper.CreateInParam("@UserInfoJudgeRule",SqlDbType.Int,4,beanitem.UserInfoJudgeRule),
                sqlHelper.CreateInParam("@UserJudgeFeild",SqlDbType.NVarChar,20,beanitem.UserJudgeFeild),
                sqlHelper.CreateInParam("@UserJudgeFeildVal",SqlDbType.NVarChar,50,beanitem.UserJudgeFeildVal),
                
               };
            return ParamList;
        }

      


         protected DBThirdAuthSettingEntry Bind(SqlDataReader rec)
        {
            DBThirdAuthSettingEntry beanitem = new DBThirdAuthSettingEntry();

            //DBServer
            beanitem.DBAuthServerIP = ConvertObjToString(rec["DBAuthServerIP"]);
            beanitem.DBAuthServerPort = ConvertObjToString(rec["DBAuthServerPort"]);
            beanitem.DBUserName = ConvertObjToString(rec["DBUserName"]);
            beanitem.DBPassword = ConvertObjToString(rec["DBPassword"]);
            beanitem.DBAuthDBNM = ConvertObjToString(rec["DBAuthDBNM"]);
            beanitem.DBTableNM = ConvertObjToString(rec["DBTableNM"]);
            //用户信息匹配
            beanitem.MTFullName = ConvertObjToString(rec["MTFullName"]);
            beanitem.MTLoginName = ConvertObjToString(rec["MTLoginName"]);
            beanitem.MTPwd = ConvertObjToString(rec["MTPwd"]);
            beanitem.MTIDCard = ConvertObjToString(rec["MTIDCard"]);
            beanitem.MTGroup = ConvertObjToString(rec["MTGroup"]);
            beanitem.MTEmail = ConvertObjToString(rec["MTEmail"]); ;
            beanitem.MTPINCode = ConvertObjToString(rec["MTPINCode"]); ;
            beanitem.GroupID1 = ConvertObjToInt(rec["GroupID1"]); ;
            beanitem.GroupID2 = ConvertObjToInt(rec["GroupID2"]); ;
            //卡同步规则
            beanitem.IDCordRule = ConvertObjToInt(rec["IDCordRule"]); ;
            beanitem.IDCardDataLen = ConvertObjToString(rec["IDCardDataLen"]); ;
            beanitem.IDCardADDPos = ConvertObjToString(rec["IDCardADDPos"]); ;
            beanitem.IDCardADDCHR = ConvertObjToString(rec["IDCardADDCHR"]); ;
            //用户信息判断规则
            beanitem.UserInfoJudgeRule = ConvertObjToInt(rec["UserInfoJudgeRule"]); ;
            beanitem.UserJudgeFeild = ConvertObjToString(rec["UserJudgeFeild"]); ;
            beanitem.UserJudgeFeildVal = ConvertObjToString(rec["UserJudgeFeildVal"]); ;

            
            rec.Close();

            return beanitem;
        }

         protected DBThirdAuthSettingEntry Bind(DataRow rec)
         {
             DBThirdAuthSettingEntry beanitem = new DBThirdAuthSettingEntry();

             //DBServer
             beanitem.DBAuthServerIP = ConvertObjToString(rec["DBAuthServerIP"]);
             beanitem.DBAuthServerPort = ConvertObjToString(rec["DBAuthServerPort"]);
             beanitem.DBUserName = ConvertObjToString(rec["DBUserName"]);
             beanitem.DBPassword = ConvertObjToString(rec["DBPassword"]);
             beanitem.DBAuthDBNM = ConvertObjToString(rec["DBAuthDBNM"]);
             beanitem.DBTableNM = ConvertObjToString(rec["DBTableNM"]);
             //用户信息匹配
             beanitem.MTFullName = ConvertObjToString(rec["MTFullName"]);
             beanitem.MTLoginName = ConvertObjToString(rec["MTLoginName"]);
             beanitem.MTPwd = ConvertObjToString(rec["MTPwd"]);

             beanitem.MTIDCard = ConvertObjToString(rec["MTIDCard"]);
             beanitem.MTGroup = ConvertObjToString(rec["MTGroup"]);
             beanitem.MTEmail = ConvertObjToString(rec["MTEmail"]); ;
             beanitem.MTPINCode = ConvertObjToString(rec["MTPINCode"]); ;
             beanitem.GroupID1 = ConvertObjToInt(rec["GroupID1"]); ;
             beanitem.GroupID2 = ConvertObjToInt(rec["GroupID2"]); ;

             //卡同步规则
             beanitem.IDCordRule = ConvertObjToInt(rec["IDCordRule"]); ;
             beanitem.IDCardDataLen = ConvertObjToString(rec["IDCardDataLen"]); ;
             beanitem.IDCardADDPos = ConvertObjToString(rec["IDCardADDPos"]); ;
             beanitem.IDCardADDCHR = ConvertObjToString(rec["IDCardADDCHR"]); ;
             //用户信息判断规则
             beanitem.UserInfoJudgeRule = ConvertObjToInt(rec["UserInfoJudgeRule"]); ;
             beanitem.UserJudgeFeild = ConvertObjToString(rec["UserJudgeFeild"]); ;
             beanitem.UserJudgeFeildVal = ConvertObjToString(rec["UserJudgeFeildVal"]); ;


             return beanitem;

         }




        //添加
         public int Add(DBThirdAuthSettingEntry bean)
        {
  
            try
            {
                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO DBThirdAuthSetting (");
                sb.Append("DBAuthServerIP,DBAuthServerPort,DBUserName,DBPassword,DBAuthDBNM,DBTableNM,");
                sb.Append("MTFullName,MTLoginName,MTPwd,MTIDCard,MTGroup,MTEmail,MTPINCode,GroupID1,GroupID2,");                
                sb.Append("IDCordRule,IDCardDataLen,IDCardADDPos,IDCardADDCHR,");                
                sb.Append("UserInfoJudgeRule,UserJudgeFeild,UserJudgeFeildVal");                
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@DBAuthServerIP,@DBAuthServerPort,@DBUserName,@DBPassword,@DBAuthDBNM,@DBTableNM,");
                sb.Append("@MTFullName,@MTLoginName,@MTPwd,@MTIDCard,@MTGroup,@MTEmail,@MTPINCode,@GroupID1,@GroupID2,");
                sb.Append("@IDCordRule,@IDCardDataLen,@IDCardADDPos,@IDCardADDCHR,");
                sb.Append("@UserInfoJudgeRule,@UserJudgeFeild,@UserJudgeFeildVal");
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
         public int Update(DBThirdAuthSettingEntry bean)
        {

            try
            {
                StringBuilder sb = new StringBuilder("");
                //连接设置

                sb.Append("UPDATE DBThirdAuthSetting SET ");
                sb.Append("DBAuthServerIP=@DBAuthServerIP,");
                sb.Append("DBAuthServerPort=@DBAuthServerPort,");
                sb.Append("DBUserName=@DBUserName,");
                sb.Append("DBPassword=@DBPassword,");
                sb.Append("DBAuthDBNM=@DBAuthDBNM,");
                sb.Append("DBTableNM=@DBTableNM,");


                sb.Append("MTFullName=@MTFullName,");
                sb.Append("MTLoginName=@MTLoginName,");
                sb.Append("MTPwd=@MTPwd,");
                sb.Append("MTIDCard=@MTIDCard,");
                sb.Append("MTGroup=@MTGroup,");
                sb.Append("MTEmail=@MTEmail,");
                sb.Append("MTPINCode=@MTPINCode,");
                sb.Append("GroupID1=@GroupID1,");
                sb.Append("GroupID2=@GroupID2,");

                sb.Append("IDCordRule=@IDCordRule,");
                sb.Append("IDCardDataLen=@IDCardDataLen,");
                sb.Append("IDCardADDPos=@IDCardADDPos,");
                sb.Append("IDCardADDCHR=@IDCardADDCHR,");

                sb.Append("UserInfoJudgeRule=@UserInfoJudgeRule,");
                sb.Append("UserJudgeFeild=@UserJudgeFeild,");
                sb.Append("UserJudgeFeildVal=@UserJudgeFeildVal");


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
         public DBThirdAuthSettingEntry GetInfoByKey()
        {
            DBThirdAuthSettingEntry entry = new DBThirdAuthSettingEntry();
            try
            {
                SqlDataReader rec;
                string sql = "select * from DBThirdAuthSetting ";
                sqlHelper.RunSQL(sql, out rec);
                if (rec.Read())
                {
                    entry = Bind(rec);
                }
                else
                {
                    entry = null;
                }
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
