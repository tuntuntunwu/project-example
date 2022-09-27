using System;
using System.Collections.Generic;
using System.Web;
using Model;
using System.Data.SqlClient;
using System.Data;
using SimpleEACommon;
using System.Configuration;
using System.Text;





/// <summary>
///DalDropAndPlus 的摘要说明
/// </summary>
/// 

namespace DAL
{
    public class DalDropAndPlus : CommonDal
    {

        //SQLHelper sqlHelper = new SQLHelper();

        protected SqlParameter[] CreateParamList(DropAndPlusEntry beanitem)
        {
                SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@CD",SqlDbType.Int,16,beanitem.CD),
                sqlHelper.CreateInParam("@code",SqlDbType.Int,16,beanitem.code),
                sqlHelper.CreateInParam("@name",SqlDbType.NVarChar,50,beanitem.name),
               };
                return ParamList;
        }

        protected DropAndPlusEntry Bind(DataRow rec)
        {
            //cui20170620
            DropAndPlusEntry beanitem = new DropAndPlusEntry();
            beanitem.CD = ConvertObjToInt(rec["CD"]);
            beanitem.code = ConvertObjToInt(rec["code"]);
            beanitem.name = ConvertObjToString(rec["name"]);

            return beanitem;

        }
        public int Add1(DropAndPlusEntry bean)
        {
            try
            {
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey1() + 1;
                bean.code = key;

                StringBuilder sb = new StringBuilder("");                
                sb.Append("INSERT INTO DropdownSetting (");
                sb.Append("CD,code,name");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@CD,@code,@name");
                sb.Append(")");
                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int Add2(DropAndPlusEntry bean)
        {
            try
            {
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey2() + 1;
                bean.code = key;

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO DropdownSetting (");
                sb.Append("CD,code,name");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@CD,@code,@name");
                sb.Append(")");
                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int Add3(DropAndPlusEntry bean)
        {
            try
            {
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey3() + 1;
                bean.code = key;

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO DropdownSetting (");
                sb.Append("CD,code,name");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@CD,@code,@name");
                sb.Append(")");
                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int Add4(DropAndPlusEntry bean)
        {
            try
            {
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey4() + 1;
                bean.code = key;

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO DropdownSetting (");
                sb.Append("CD,code,name");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@CD,@code,@name");
                sb.Append(")");
                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int Add5(DropAndPlusEntry bean)
        {
            try
            {
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey5() + 1;
                bean.code = key;

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO DropdownSetting (");
                sb.Append("CD,code,name");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@CD,@code,@name");
                sb.Append(")");
                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int Add6(DropAndPlusEntry bean)
        {
            try
            {
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey6() + 1;
                bean.code = key;

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO DropdownSetting (");
                sb.Append("CD,code,name");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@CD,@code,@name");
                sb.Append(")");
                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        //获取DropdownSetting中ID的最大值
        public int GetMaxKey1()
        {           
            string sql;           
            sql = "SELECT Max(code) FROM DropdownSetting where CD = 1";            
            try
            {
                int ret = sqlHelper.RunSelectSQLToScalar(sql);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetMaxKey2()
        {
            string sql;
            sql = "SELECT Max(code) FROM DropdownSetting where CD = 2";
            try
            {
                int ret = sqlHelper.RunSelectSQLToScalar(sql);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int GetMaxKey3()
        {
            string sql;
            sql = "SELECT Max(code) FROM DropdownSetting where CD = 3";
            try
            {
                int ret = sqlHelper.RunSelectSQLToScalar(sql);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int GetMaxKey4()
        {
            string sql;
            sql = "SELECT Max(code) FROM DropdownSetting where CD = 4";
            try
            {
                int ret = sqlHelper.RunSelectSQLToScalar(sql);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int GetMaxKey5()
        {
            string sql;
            sql = "SELECT Max(code) FROM DropdownSetting where CD = 5";
            try
            {
                int ret = sqlHelper.RunSelectSQLToScalar(sql);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int GetMaxKey6()
        {
            string sql;
            sql = "SELECT Max(code) FROM DropdownSetting where CD = 6";
            try
            {
                int ret = sqlHelper.RunSelectSQLToScalar(sql);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Query(DropAndPlusEntry bean)
        {

            try
            {
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey1() + 1;
                bean.code = key;

                StringBuilder sb = new StringBuilder("");

                sb.Append("INSERT INTO DropdownSetting (");
                sb.Append("CD,code,name");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@CD,@code,@name");
                sb.Append(")");

                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        protected DropAndPlusEntry Bind(SqlDataReader rec)
        {
            DropAndPlusEntry beanitem = new DropAndPlusEntry();
            //beanitem.CD = ConvertObjToInt(rec["CD"]);
            beanitem.name = ConvertObjToString(rec["name"]);

            rec.Close();

            return beanitem;
        }

        public DropAndPlusEntry GetNameByCD()
        {
            DropAndPlusEntry entry = new DropAndPlusEntry();
            try
            {
                SqlDataReader rec;
                string sql = "select name from DropdownSetting where CD = 1";
                sqlHelper.RunSQL(sql, out rec);
                if (rec.Read())
                    entry = Bind(rec);
               // Console.WriteLine(entry.name);
                return entry;

            }
            catch (Exception ex)
            {
                ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
                        
        }
        public List<DropAndPlusEntry> GetDropPlusListByCD(int cd)
        {
            //DropAndPlusEntry entry = new DropAndPlusEntry();
            List<DropAndPlusEntry> lst = new List<DropAndPlusEntry>();
            try
            {
                //SqlDataReader rec;
                StringBuilder sb = new StringBuilder("");
                sb.Append( "select CD, code, name from DropdownSetting where CD = '");
                sb.Append(cd.ToString());
                sb.Append("'");
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = this.sqlHelper.GetDataSet(sb.ToString());
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lst.Add(Bind(dt.Rows[i]));
                    }

                }
                return lst;
            }
            catch (Exception ex)
            {
                ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }

        }


    }
}