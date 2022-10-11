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
    public class DalDBThirdConfig : CommonDal
    {
        //SQLHelper sqlHelper = new SQLHelper();

        protected SqlParameter[] CreateParamList(DBThirdAuthConfigEntry beanitem)
        {
                SqlParameter[] ParamList ={
                 //DBServer
                sqlHelper.CreateInParam("@DBConnectStr",SqlDbType.NVarChar,500,beanitem.DBConnectStr),
                sqlHelper.CreateInParam("@DBSearchSql",SqlDbType.NVarChar,500,beanitem.DBSearchSql),
                sqlHelper.CreateInParam("@DBWhereSql",SqlDbType.NVarChar,500,beanitem.DBWhereSql),
                //用户信息匹配
                sqlHelper.CreateInParam("@MTGrpVal1",SqlDbType.NVarChar,50,beanitem.MTGrpVal1),
                sqlHelper.CreateInParam("@MTGrpVal2",SqlDbType.NVarChar,50,beanitem.MTGrpVal2),
                sqlHelper.CreateInParam("@GroupID1",SqlDbType.Int,4,beanitem.GroupID1),
                sqlHelper.CreateInParam("@GroupID2",SqlDbType.Int,4,beanitem.GroupID2),
                sqlHelper.CreateInParam("@AuthDBFlg",SqlDbType.Int,4,beanitem.AuthDBFlg),

               };
            return ParamList;
        }

      


         protected DBThirdAuthConfigEntry Bind(SqlDataReader rec)
        {
            DBThirdAuthConfigEntry beanitem = new DBThirdAuthConfigEntry();

            //DBServer
            beanitem.DBConnectStr = ConvertObjToString(rec["DBConnectStr"]);
            beanitem.DBSearchSql = ConvertObjToString(rec["DBSearchSql"]);
            beanitem.DBWhereSql = ConvertObjToString(rec["DBWhereSql"]);
            //用户信息匹配
            beanitem.MTGrpVal1 = ConvertObjToString(rec["MTGrpVal1"]);
            beanitem.MTGrpVal2 = ConvertObjToString(rec["MTGrpVal2"]);
            beanitem.GroupID1 = ConvertObjToInt(rec["GroupID1"]); 
            beanitem.GroupID2 = ConvertObjToInt(rec["GroupID2"]);
            beanitem.AuthDBFlg = ConvertObjToInt(rec["AuthDBFlg"]); 

            
            rec.Close();

            return beanitem;
        }

         protected DBThirdAuthConfigEntry Bind(DataRow rec)
         {
             DBThirdAuthConfigEntry beanitem = new DBThirdAuthConfigEntry();

             //DBServer
             beanitem.DBConnectStr = ConvertObjToString(rec["DBConnectStr"]);
             beanitem.DBSearchSql = ConvertObjToString(rec["DBSearchSql"]);
             beanitem.DBWhereSql = ConvertObjToString(rec["DBWhereSql"]);
             //用户信息匹配
             beanitem.MTGrpVal1 = ConvertObjToString(rec["MTGrpVal1"]);
             beanitem.MTGrpVal2 = ConvertObjToString(rec["MTGrpVal2"]);
             beanitem.GroupID1 = ConvertObjToInt(rec["GroupID1"]); ;
             beanitem.GroupID2 = ConvertObjToInt(rec["GroupID2"]); ;
             beanitem.AuthDBFlg = ConvertObjToInt(rec["AuthDBFlg"]); ;


             return beanitem;

         }




        //添加
         public int Add(DBThirdAuthConfigEntry bean)
        {
  
            try
            {
                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO DBThirdAuthConfig (");
                sb.Append("DBConnectStr,DBSearchSql,DBWhereSql,");
                sb.Append("MTGrpVal1,MTGrpVal2,GroupID1,GroupID2,AuthDBFlg");                
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@DBConnectStr,@DBSearchSql,@DBWhereSql,");
                sb.Append("@MTGrpVal1,@MTGrpVal2,@GroupID1,@GroupID2,@AuthDBFlg ");
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
         public int Update(DBThirdAuthConfigEntry bean)
        {

            try
            {
                StringBuilder sb = new StringBuilder("");
                //连接设置

                sb.Append("UPDATE DBThirdAuthConfig SET ");
                sb.Append("DBConnectStr=@DBConnectStr,");
                sb.Append("DBSearchSql=@DBSearchSql,");
                sb.Append("DBWhereSql=@DBWhereSql,");
                sb.Append("MTGrpVal1=@MTGrpVal1,");
                sb.Append("MTGrpVal2=@MTGrpVal2,");
                sb.Append("GroupID1=@GroupID1,");
                sb.Append("GroupID2=@GroupID2,");
                sb.Append("AuthDBFlg=@AuthDBFlg ");


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
         public DBThirdAuthConfigEntry GetInfoByKey()
        {
            DBThirdAuthConfigEntry entry = new DBThirdAuthConfigEntry();
            try
            {
                SqlDataReader rec;
                string sql = "select * from DBThirdAuthConfig ";
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
