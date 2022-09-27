using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using SimpleEACommon;
using System.Configuration;
using System.Data.SqlClient;
using Model;

///// <summary>
/////DalGroup 的摘要说明
///// </summary>
//public class DalGroup
//{
//    public DalGroup()
//    {
//        //
//        //TODO: 在此处添加构造函数逻辑
//        //
//    }
//}

namespace DAL
{
    //cui20170709(已完成)
    public class DalRestrictionInfo : CommonDal
    {
        //SQLHelper sqlHelper = new SQLHelper();

        protected SqlParameter[] CreateParamList(RestrictionInfoEntry beanitem)
        {
            SqlParameter[] ParamList ={
                //连接设置
                sqlHelper.CreateInParam("@ID",SqlDbType.Int,10,beanitem.ID),
                sqlHelper.CreateInParam("@RestrictionName",SqlDbType.NVarChar,30,beanitem.RestrictionName),
                sqlHelper.CreateInParam("@AllQuota",SqlDbType.Float,4,beanitem.AllQuota),
                sqlHelper.CreateInParam("@ColorQuota",SqlDbType.Float,4,beanitem.ColorQuota),
                sqlHelper.CreateInParam("@OverLimit",SqlDbType.Float,4,beanitem.OverLimit),
                sqlHelper.CreateInParam("@PrintBW",SqlDbType.Float,4,beanitem.PrintBW),
                 };
            return ParamList;
        }



        protected RestrictionInfoEntry Bind(SqlDataReader rec)
        {
            RestrictionInfoEntry beanitem = new RestrictionInfoEntry();
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.RestrictionName = ConvertObjToString(rec["RestrictionName"]);
            beanitem.AllQuota = ConvertObjToFloat(rec["AllQuota"]);
            beanitem.ColorQuota = ConvertObjToFloat(rec["ColorQuota"]);
            beanitem.OverLimit = ConvertObjToFloat(rec["OverLimit"]);
            beanitem.PrintBW = ConvertObjToInt(rec["PrintBW"]);           

            rec.Close();

            return beanitem;
        }

        protected RestrictionInfoEntry Bind(DataRow rec)
        {
            RestrictionInfoEntry beanitem = new RestrictionInfoEntry();

            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.RestrictionName = ConvertObjToString(rec["RestrictionName"]);
            beanitem.AllQuota = ConvertObjToFloat(rec["AllQuota"]);
            beanitem.ColorQuota = ConvertObjToFloat(rec["ColorQuota"]);
            beanitem.OverLimit = ConvertObjToFloat(rec["OverLimit"]);
            beanitem.PrintBW = ConvertObjToInt(rec["PrintBW"]);           

            return beanitem;

        }


        //添加
        public int Add(RestrictionInfoEntry bean)
        {

            try
            {
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey() + 1;
                bean.ID = key;

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO RestrictionInfo (");
                sb.Append("ID,RestrictionName,AllQuota,ColorQuota,OverLimit,PrintBW,UpdateTime,CreateTime");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@ID,@RestrictionName,@AllQuota,@ColorQuota,@OverLimit,@PrintBW");
                sb.Append(",\'" + DateTime.Now.ToString() + "\'");
                sb.Append(",\'" + DateTime.Now.ToString() + "\'");
                sb.Append(")");

                String str = sb.ToString();

                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public int Update(RestrictionInfoEntry bean)
        {
            try
            {
                StringBuilder sb = new StringBuilder("");

                sb.Append(" UPDATE RestrictionInfo SET ");
                sb.Append(" RestrictionName=@RestrictionName,");
                sb.Append(" AllQuota=@AllQuota,");
                sb.Append(" ColorQuota=@ColorQuota,");
                sb.Append(" OverLimit=@OverLimit,");
                sb.Append(" PrintBW=@PrintBW");
                sb.Append(" WHERE ID=@ID ");

                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

       

        public int GetIDbyName(String RestrictionName)
        {
            RestrictionInfoEntry entry = new RestrictionInfoEntry();
            try
            {
                SqlDataReader rec;
                string gn = "\'" + RestrictionName + "\'";
                string sql = "select * from RestrictionInfo where RestrictionName=" + gn;                
                sqlHelper.RunSQL(sql, out rec);
                if (rec.Read())
                {
                    entry = Bind(rec);
                    return entry.ID;
                }
                else
                {
                    return -1;
                }


            }
            catch (Exception ex)
            {
                return -1; 
            }
        }

        //获取RestrictionInfo中ID的最大值
        public int GetMaxKey()
        {
            string sql;
            sql = "SELECT Max(ID) FROM RestrictionInfo ";
            try
            {
                int ret = sqlHelper.RunSelectSQLToScalar(sql);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
            //return 0;

        }
  
    }

}


