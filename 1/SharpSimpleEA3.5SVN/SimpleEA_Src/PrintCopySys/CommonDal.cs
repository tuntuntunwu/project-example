using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace PrintCopySys
{
    public  class CommonDal
    {
        protected SQLHelper sqlHelper;
        protected string TblName;
        protected string KeyName;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <returns></returns>
        protected CommonDal()
        {
            sqlHelper = new SQLHelper();
        }

        protected int ConvertObjToInt(Object val)
        {
            int ret = 0;
            try
            {
                if (val == null)
                {
                    ret = 0;
                }
                else
                {
                    ret = Convert.ToInt32(val);
                }
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
        protected string  ConvertObjToString(Object val)
        {
            string ret = "";
            try
            {
                if (val == null)
                {
                    ret = "";
                }
                else
                {
                    ret = Convert.ToString(val);
                }
            }
            catch
            {
                ret = "";
            }
            return ret;
        }
        protected char ConvertObjToChar(Object val)
        {
            char ret = '0';
            try
            {
                if (val == null)
                {
                    ret = '0';
                }
                else
                {
                    ret = Convert.ToChar(val);
                }
            }
            catch
            {
                ret = '0';
            }
            return ret;
        }
        protected DateTime ConvertObjToDateTime(Object val)
        {
            DateTime ret = DateTime.Now;
            try
            {
                if (val == null)
                {
                    ret= DateTime.Now;
                }
                else
                {
                    ret = Convert.ToDateTime(val);
                }
            }
            catch
            {
                ret = DateTime.Now;
            }
            return ret;
        }
        protected bool ConvertObjToBool(Object val)
        {
            bool ret = false;
            try
            {
                if (val == null)
                {
                    ret =  false;
                }
                else
                {
                    ret =  Convert.ToBoolean(val);
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        public int Get_NumBySql(string sql, SqlParameter[] prams)
        {
            int count = 0;
            try
            {
                count = sqlHelper.RunSelectSQLToScalar(sql, prams);
                return count;
            }
            catch (Exception ex)
            {
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public string createSqlHeader()
        {
            string sql = "SELECT * FROM (";
            return sql;
        }

        public string createSqlFooter(int CurrentPage, int PageSize)
        {
            int startnum = (CurrentPage - 1) * PageSize;
            int num = startnum + PageSize;
            StringBuilder sqlBuilder = new StringBuilder("");
            sqlBuilder.Append(" ) AS A  WHERE ROWNO > ");
            sqlBuilder.Append(startnum);
            sqlBuilder.Append(" AND ROWNO <= ");
            sqlBuilder.Append(num);
            sqlBuilder.Append(" ORDER BY ROWNO  ");
            return sqlBuilder.ToString();
        }
        public string createSqlBody(string oderby)
        {
            StringBuilder sqlBuilder = new StringBuilder("");
            sqlBuilder.Append(" SELECT ROW_NUMBER() over(order by ");
            sqlBuilder.Append(oderby);
            sqlBuilder.Append(" ) as ROWNO, * FROM ");
            sqlBuilder.Append(TblName);
            return sqlBuilder.ToString();
        }
        public string createSqlBodyM(string oderby)
        {
            StringBuilder sqlBuilder = new StringBuilder("");
            return sqlBuilder.ToString();
        }
        public string createSqlCount()
        {
            StringBuilder sqlBuilder = new StringBuilder("");
            sqlBuilder.Append(" SELECT COUNT(");
            sqlBuilder.Append(KeyName);
            sqlBuilder.Append(") FROM ");
            sqlBuilder.Append(TblName);
            return sqlBuilder.ToString();
        }
    }
}
