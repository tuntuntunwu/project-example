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
namespace CopyPringPDFService
{
    public class DalPrintCopy : CommonDal
    {
        protected SQLHelper sqlHelper;

        public DalPrintCopy()
        {
          
        }
        protected SqlParameter[] CreateParamList(PrintCopyEntry beanitem)
        {

            SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@ID",SqlDbType.Int,4,beanitem.ID),
                sqlHelper.CreateInParam("@Time",SqlDbType.DateTime,14,beanitem.Time),
                sqlHelper.CreateInParam("@UserID",SqlDbType.Int,15,beanitem.UserID),
                sqlHelper.CreateInParam("@IpAddress",SqlDbType.NVarChar,50,beanitem.IpAddress),
                sqlHelper.CreateInParam("@CopyType",SqlDbType.Int,1,beanitem.CopyType),
                sqlHelper.CreateInParam("@CopyFile",SqlDbType.NVarChar,50,beanitem.CopyFile),
                sqlHelper.CreateInParam("@Finished",SqlDbType.Int,4,beanitem.Finished),
                sqlHelper.CreateInParam("@CopyTimes",SqlDbType.Int,4,beanitem.CopyTimes),

            };
            return ParamList;
        }

        protected PrintCopyEntry Bind(SqlDataReader rec)
        {

            PrintCopyEntry beanitem = new PrintCopyEntry();
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.Time = ConvertObjToDateTime(rec["Time"]);
            beanitem.UserID = ConvertObjToInt(rec["UserID"]);
            beanitem.IpAddress = ConvertObjToString(rec["IpAddress"]);
            beanitem.CopyType = ConvertObjToInt(rec["CopyType"]);
            beanitem.CopyFile = ConvertObjToString(rec["CopyFile"]);
            beanitem.Finished = ConvertObjToInt(rec["Finished"]);
            beanitem.CopyTimes = ConvertObjToInt(rec["CopyTimes"]);
            rec.Close();

            return beanitem;
        }

        protected PrintCopyEntry Bind(DataRow rec)
        {

            PrintCopyEntry beanitem = new PrintCopyEntry();
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.Time = ConvertObjToDateTime(rec["Time"]);
            beanitem.UserID = ConvertObjToInt(rec["UserID"]);
            beanitem.IpAddress = ConvertObjToString(rec["IpAddress"]);
            beanitem.CopyType = ConvertObjToInt(rec["CopyType"]);
            beanitem.CopyFile = ConvertObjToString(rec["CopyFile"]);
            beanitem.Finished = ConvertObjToInt(rec["Finished"]);
            beanitem.CopyTimes = ConvertObjToInt(rec["CopyTimes"]);
            return beanitem;

       }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int Add(PrintCopyEntry bean)
        {
        
            try
            {
                StringBuilder sb = new StringBuilder( "");
                //int key = GetMaxKey() + 1;
                //bean.ID = key;
                sb.Append("INSERT INTO PrintCopy (");
                sb.Append("ID,Time,UserID,IpAddress,CopyType,OrigFile,CopyFile,Finished,CopyTimes");
                sb.Append(" )");
                sb.Append("Values(");
                sb.Append("@ID,@Time,@UserID,@IpAddress,@CopyType,@OrigFile,@CopyFile,@Finished,@CopyTimes");
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
        public int Update(PrintCopyEntry userBean)
        {
            try
            {
                StringBuilder sb = new StringBuilder("");
                sb.Append("UPDATE PrintCopy SET ");
                sb.Append("ID=@ID,");
                sb.Append("Time=@Time,");
                sb.Append("UserID=@UserID,");
                sb.Append("IpAddress=@IpAddress, ");
                sb.Append("CopyType=@CopyType,");
                sb.Append("OrigFile=@OrigFile, ");
                sb.Append("CopyFile=@CopyFile, ");
                sb.Append("Finished=@Finished, ");
                sb.Append("CopyTimes=@CopyTimes ");
                sb.Append("WHERE UID=@UID");

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
        public int Delete(int id)
        {
            int ret;
            SqlParameter[] ParamList ={ 
                sqlHelper.CreateInParam("@ID",SqlDbType.Int,4,id)
            };
            try
            {
                string sql = "DELETE FROM PrintCopy WHERE ID =@ID";
                ret = sqlHelper.RunSQL(sql, ParamList);
                return ret;
            }
            catch (Exception ex)
            {
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 根据生成日期CreateTime删除记录,删除开始时间和结束时间之间的数据between
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回删除的记录（用于在本地删除表中的文件名对应的文件） </returns>
        public int Delete(DateTime startTime, DateTime endTime)
        {
          
            int ret;
            SqlParameter[] ParamList = null;
            ParamList = new SqlParameter[2];
            SqlParameter param1 = sqlHelper.CreateInParam("@StartTime", SqlDbType.DateTime,27, startTime);
            SqlParameter param2 = sqlHelper.CreateInParam("@EndTime", SqlDbType.DateTime,27,endTime);
            ParamList[0] = param1;
            ParamList[1] = param2;

          
            try
            {
     
               
                string sql = "DELETE FROM PrintCopy WHERE Time between @StartTime and @EndTime";
                ret = sqlHelper.RunSQL(sql, ParamList);
                return ret;
                
            }
            catch (Exception ex)
            {
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
                   
           
        }
        public List<PrintCopyEntry> SearchRecord()
        {
            string selectsql = "select * from PrintCopy  WHERE Finished = '0' ";
            DataSet ds = this.sqlHelper.GetDataSet(selectsql);
            DataTable dt = new DataTable();
            List<PrintCopyEntry> beanlist = new List<PrintCopyEntry>();
            if (ds != null)
            {
               dt = ds.Tables[0];
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   PrintCopyEntry pcentry = Bind(dt.Rows[i]);
                   beanlist.Add(pcentry);
                }
             }
            return beanlist;
    
    }


        /// <summary>
        /// 通过ID查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public PrintCopyEntry GetInfoByID(int id)
        {
            PrintCopyEntry entry = new PrintCopyEntry();
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@ID", SqlDbType.Int, 4, id) };
            try
            {
                SqlDataReader rec;
                string sql = "select * from PrintCopy  where ID=@ID";
                sqlHelper.RunSQL(sql, ParamList, out rec);
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
        /// 通过UName查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>

        public bool CheckExist(PrintCopyEntry bean)  
        {
            string condition = "ID= '" + bean.ID + "'";
            return CheckExist(condition);
        }


        /// <summary>
        /// 根据输入的条件进行检测，注意条件是WHERE后面的部分
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool CheckExist(string condition)
        {
            string sql;
            if (condition.Length > 0)
                sql = "select count(*) from PrintCopy  where " + condition;
            else
                sql = "select count(*) from PrintCopy";
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

        public int  GetMaxKey()
        {
            string sql;
            sql = "SELECT Max(ID) FROM PrintCopy ";
            try
            {
                int ret = sqlHelper.RunSelectSQLToScalar(sql);
                return ret;
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }


    }
}
