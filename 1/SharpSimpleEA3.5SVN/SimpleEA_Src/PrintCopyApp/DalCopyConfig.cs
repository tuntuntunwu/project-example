using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

/// <summary>
/// ProductSQL 的摘要说明
/// </summary>
namespace PrintCopyApp
{
    public class DalCopyConfig : CommonDal
    {

        public DalCopyConfig()
        {
            this.TblName = "CopyConfig";
            this.KeyName = "CopyFileLocation";
        }
        protected SqlParameter[] CreateParamList(CopyConfigEntry beanitem)
        {

            SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@CopyFileLocation",SqlDbType.NVarChar,2048,beanitem.CopyFileLocation),

            };
            return ParamList;
        }

        protected CopyConfigEntry Bind(SqlDataReader rec)
        {

            CopyConfigEntry beanitem = new CopyConfigEntry();
            beanitem.CopyFileLocation = ConvertObjToString(rec["CopyFileLocation"]);

            rec.Close();

            return beanitem;
        }

        protected CopyConfigEntry Bind(DataRow rec)
        {

            CopyConfigEntry beanitem = new CopyConfigEntry();
            beanitem.CopyFileLocation = ConvertObjToString(rec["CopyFileLocation"]);

            return beanitem;

       }

        protected CopyConfigEntry BindM(DataRow rec)
        {

            CopyConfigEntry beanitem = new CopyConfigEntry();
            beanitem.CopyFileLocation = ConvertObjToString(rec["CopyFileLocation"]);

            return beanitem;

        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int Add(CopyConfigEntry bean)
        {
        
            try
            {
                StringBuilder sb = new StringBuilder( "");
                sb.Append("INSERT INTO ");
                sb.Append(TblName);
                sb.Append(" (");
                sb.Append("CopyFileLocation");
                sb.Append( " )");
                sb.Append( "Values(");
                sb.Append("@CopyFileLocation");
                sb.Append(")");

                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="product"></param>
        public int Update(CopyConfigEntry userBean)
        {
            
            try
            {
                StringBuilder sb = new StringBuilder("");
                sb.Append("UPDATE  ");
                sb.Append( TblName );
                sb.Append(" SET ");
                sb.Append("CopyFileLocation=@CopyFileLocation ");

                SqlParameter[] sqlParam = CreateParamList(userBean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }



        /// <summary>
        /// 根据主键的值删除某个记录，注意的是目前主键只支持int类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回影响的记录数量 </returns>
        public int Delete()
        {
            try
            {
                StringBuilder sb = new StringBuilder(""); 
                sb.Append("delete  from ");
                sb.Append( TblName );

                int ret = sqlHelper.RunSQL(sb.ToString());
                return ret;
            }
            catch (Exception ex)
            {
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        /// 通过UID查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public CopyConfigEntry GetCopyConfigInfo()
        {
            CopyConfigEntry entry = new CopyConfigEntry();
            try
            {
                SqlDataReader rec;
                StringBuilder sb = new StringBuilder("");
                sb.Append("select *  from ");
                sb.Append( TblName );
                sqlHelper.RunSQL(sb.ToString(), out rec);
                if (rec.Read())
                    entry = Bind(rec);
                return entry;

            }
            catch (Exception ex)
            {
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据输入的条件进行检测，注意条件是WHERE后面的部分
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool CheckExist()
        {
            string sql;
            sql = "select count(*) from CopyConfig";
            try
            {
                if (sqlHelper.RunSelectSQLToScalar(sql) > 0)  //RunSelectSQLToScalar为执行参数所带的sql语句
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return true;
            }

        }

    }
}
