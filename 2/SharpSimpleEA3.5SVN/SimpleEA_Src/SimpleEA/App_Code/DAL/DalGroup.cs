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
    public class DalGroup : CommonDal
    {
        //SQLHelper sqlHelper = new SQLHelper();

        protected SqlParameter[] CreateParamList(GroupEntry beanitem)
        {
            SqlParameter[] ParamList ={
                //连接设置
                sqlHelper.CreateInParam("@ID",SqlDbType.Int,10,beanitem.ID),
                sqlHelper.CreateInParam("@GroupName",SqlDbType.NVarChar,30,beanitem.GroupName),
                sqlHelper.CreateInParam("@RestrictionID",SqlDbType.Int,4,beanitem.RestrictionID),
                 };
            return ParamList;
        }

   

        protected GroupEntry Bind(SqlDataReader rec)
        {
            GroupEntry beanitem = new GroupEntry();
            //GroupInfo里面的两个字段
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.GroupName = ConvertObjToString(rec["GroupName"]);
            beanitem.RestrictionID = ConvertObjToInt(rec["RestrictionID"]);           

            rec.Close();

            return beanitem;
        }

        protected GroupEntry Bind(DataRow rec)
        {
            GroupEntry beanitem = new GroupEntry();
            
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.GroupName = ConvertObjToString(rec["GroupName"]);
            beanitem.RestrictionID = ConvertObjToInt(rec["RestrictionID"]);           

            return beanitem;

        }

        protected GroupEntry BindM(DataRow rec)
        {
            GroupEntry beanitem = new GroupEntry();

            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.GroupName = ConvertObjToString(rec["GroupName"]);
            beanitem.RestrictionID = ConvertObjToInt(rec["RestrictionID"]);           

            return beanitem;
        }


        //添加
        public int Add(GroupEntry bean)
        {

            try
            {
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey() + 1;
                bean.ID = key;

                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO GroupInfo (");
                sb.Append("ID,GroupName,RestrictionID,UpdateTime,CreateTime");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@ID,@GroupName,@RestrictionID");
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

        //更新
        public int Update_Connection(GroupEntry userBean)
        {
            try
            {
                StringBuilder sb = new StringBuilder("");
                
                sb.Append("UPDATE GroupInfo SET ");
                sb.Append("ID=@ID,");
                sb.Append("GroupName=@GroupName");            
           
                SqlParameter[] sqlParam = CreateParamList(userBean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int Update(GroupEntry bean)
        {
            try
            {
                StringBuilder sb = new StringBuilder("");

                sb.Append(" UPDATE GroupInfo SET ");
                sb.Append(" GroupName=@GroupName,");
                sb.Append(" RestrictionID=@RestrictionID");
                sb.Append(" WHERE ID=@ID ");

                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 查一条数据（暂时先做连接界面20170709）
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public GroupEntry GetInfoByKey()
        {
            GroupEntry entry = new GroupEntry();
            try
            {
                SqlDataReader rec;
                string sql = "select * from GroupInfo ";
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
       
        public GroupEntry GetEntrybyGroupName(String GroupName)
        {
            GroupEntry entry = new GroupEntry();
            try
            {
                SqlDataReader rec;
                //string gn = "\'" + GroupName + "\'";
                //string sql = "select * from GroupInfo where GroupName = " + gn;                
                string sql = "select * from GroupInfo where GroupName=@GroupName";
                SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@GroupName",SqlDbType.NVarChar,30,GroupName),
                 };
                sqlHelper.RunSQL(sql,ParamList,out rec);
                if (rec.Read())
                    entry = Bind(rec);
                return entry;

            }
            catch (Exception ex)
            {
                return entry;; 
            }
        }
        public GroupEntry GetIdbyGroupName(String GroupName)
        {
            GroupEntry entry = new GroupEntry();
            try
            {
                SqlDataReader rec;
                string gn = "\'" + GroupName + "\'";
                string sql = "select * from GroupInfo where GroupName=" + gn;
                sqlHelper.RunSQL(sql, out rec);
                if (rec.Read())
                    entry = Bind(rec);
                return entry;

            }
            catch (Exception ex)
            {
                return entry; 
            }
        }


        public int GetGroupCount()
        {
            GroupEntry entry = new GroupEntry();
            try
            {
                //SqlDataReader rec;
                string sql = "select COUNT(*) from GroupInfo " ;
                int count = sqlHelper.RunSelectSQLToScalar(sql);
                return count;

            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        // end GetInfoByKey

        //获取GroupInfo中ID的最大值
        public int GetMaxKey()
        {
            string sql;
            sql = "SELECT Max(ID) FROM GroupInfo ";
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

        protected JobInformationCSVModel BindCSVM(DataRow rec)
        {
            JobInformationCSVModel bean = new JobInformationCSVModel();
            bean.GroupID = ConvertObjToInt(rec["GroupID"]);
            bean.GroupName = ConvertObjToString(rec["GroupName"]);
            return bean;
        }
        protected JobInformationCSVModel BindCountM(DataRow rec)
        {
            JobInformationCSVModel bean = new JobInformationCSVModel();
            bean.GroupID = ConvertObjToInt(rec["GroupID"]);
            bean.GroupName = ConvertObjToString(rec["GroupName"]);
            bean.UserCount = ConvertObjToInt(rec["UserCount"]);
            return bean;
        }

        public List<JobInformationCSVModel> getGroupReportCSVList(string SearchTxt)
        {
            // 2010.11.24 Update By SES zhoumiao Ver.1.1 Update ED

            StringBuilder sb = new StringBuilder("");
            sb.Append("SELECT ");
            sb.Append("  GroupInfo.ID               AS GroupID ");
            sb.Append("  ,GroupInfo.GroupName        AS GroupName ");
            sb.Append(" FROM GroupInfo ");
            sb.Append(SearchTxt);
            

            String sql = sb.ToString();
            DataSet ds = this.sqlHelper.GetDataSet(sql);
            DataTable dt = new DataTable();
            List<JobInformationCSVModel> list = new List<JobInformationCSVModel>();
            if (ds != null)
            {
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JobInformationCSVModel bean = BindCSVM(dt.Rows[i]);
                    list.Add(bean);
                }
            }
            return list;
        }
        public List<JobInformationCSVModel> getGroupUserCountCSVList(string SearchTxt)
        {
            // 2010.11.24 Update By SES zhoumiao Ver.1.1 Update ED

            StringBuilder sb = new StringBuilder("");
            sb.Append("SELECT ");
            sb.Append("  GroupInfo.ID               AS GroupID ");
            sb.Append("  ,Max(GroupInfo.GroupName)        AS GroupName ");
            sb.Append("  ,Count(UserInfo.ID)         AS UserCount ");

            sb.Append(" FROM [GroupInfo] LEFT JOIN ");
            sb.Append("  [UserInfo] ON UserInfo.GroupID = GroupInfo.ID ");
            sb.Append(SearchTxt);
            sb.Append("  Group By GroupInfo.ID ");


            String sql = sb.ToString();
            DataSet ds = this.sqlHelper.GetDataSet(sql);
            DataTable dt = new DataTable();
            List<JobInformationCSVModel> list = new List<JobInformationCSVModel>();
            if (ds != null)
            {
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JobInformationCSVModel bean = BindCountM(dt.Rows[i]);
                    list.Add(bean);
                }
            }
            return list;
        }

        /// <summary>
        /// 查所有数据
        /// add by wangziyang
        /// 2019.09.28
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public List<GroupEntry> getAllUserGroupList()
        {

            StringBuilder sb = new StringBuilder("");
            sb.Append("SELECT ");
            sb.Append("  *  ");
            sb.Append(" FROM GroupInfo ");
            String sql = sb.ToString();
            DataSet ds = this.sqlHelper.GetDataSet(sql);
            DataTable dt = new DataTable();
            List<GroupEntry> list = new List<GroupEntry>();
            if (ds != null)
            {
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GroupEntry bean = Bind(dt.Rows[i]);
                    list.Add(bean);
                }
            }
            return list;
        }

  
    }

}


