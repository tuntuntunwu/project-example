﻿using System;
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
    public class DalSectionGroup : CommonDal
    {
        //SQLHelper sqlHelper = new SQLHelper();

        protected SqlParameter[] CreateParamList(SectionGroupEntry beanitem)
        {
                SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@GroupID",SqlDbType.Int,4,beanitem.GroupID),
                sqlHelper.CreateInParam("@SectionID",SqlDbType.NVarChar,32,beanitem.SectionID),
               };
            return ParamList;
        }




        protected SectionGroupEntry Bind(SqlDataReader rec)
        {
            SectionGroupEntry beanitem = new SectionGroupEntry();
            beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);
            beanitem.SectionID = ConvertObjToString(rec["SectionID"]);
            
            rec.Close();

            return beanitem;
        }

        protected SectionGroupEntry Bind(DataRow rec)
         {
             SectionGroupEntry beanitem = new SectionGroupEntry();
             beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);
             beanitem.SectionID = ConvertObjToString(rec["SectionID"]);

             return beanitem;

         }




        //添加
        public int Add(SectionGroupEntry bean)
        {
  
            try
            {
                StringBuilder sb = new StringBuilder("");
                sb.Append("INSERT INTO SectionGropy (");
                sb.Append("GroupID,");
                sb.Append("SectionID ");
                sb.Append(" )");
                sb.Append("Values (");
                sb.Append("@GroupID,");
                sb.Append("@SectionID ");
                sb.Append(")");

                SqlParameter[] sqlParam = CreateParamList(bean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }


        public int Update(SectionGroupEntry bean)
         {

             try
             {
                 StringBuilder sb = new StringBuilder("");
                 sb.Append(" UPDATE SectionGroup SET ");
                 sb.Append(" SectionID=@SectionID ");
                 sb.Append(" WHERE GroupID=@GroupID");

                 SqlParameter[] sqlParam = CreateParamList(bean);
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
        public int Delete(int GroupID)
         {
             int ret;
             SqlParameter[] ParamList ={ 
                sqlHelper.CreateInParam("@GroupID",SqlDbType.Int,4,GroupID)
            };
             try
             {
                 string sql = "delete from SectionGroup where GroupID =@GroupID";
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
        /// 根据主键的值删除某个记录，注意的是目前主键只支持int类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回影响的记录数量 </returns>
        public int DeleteGroup(int GroupID)
        {
            int ret;
            SqlParameter[] ParamList ={ 
                sqlHelper.CreateInParam("@GroupID",SqlDbType.Int,4,GroupID)
            };
            try
            {
                string sql = "delete from SectionGroup where GroupID =@GroupID";
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
        /// 根据主键的值删除某个记录，注意的是目前主键只支持int类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回影响的记录数量 </returns>
        public int DeleteSection(string SectionID)
        {
            int ret;
            SqlParameter[] ParamList ={ 
                sqlHelper.CreateInParam("@SectionID",SqlDbType.NVarChar,32,SectionID)
            };
            try
            {
                string sql = "delete from SectionGroup where SectionID =@SectionID";
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
        /// 通过GroupID查一条数据
         /// </summary>
         /// <param name="sUserName"></param>
         /// <returns></returns>
         public SectionGroupEntry GetInfoByKey(int GroupID)
         {
             SectionGroupEntry entry = new SectionGroupEntry();
             SqlParameter[] ParamList = { sqlHelper.CreateInParam("@GroupID", SqlDbType.Int, 4, GroupID) };
             try
             {
                 SqlDataReader rec;
                 string sql = "select * from SectionGroup  where GroupID=@GroupID ";
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
         /// 检查Group关联是否存在
         /// </summary>
         /// <param name="sUserName"></param>
         /// <returns></returns>

         public bool CheckSectionExist(int GroupID)
         {
             string condition = "GroupID= '" + GroupID + "'";
             return CheckExist(condition);
         }

         /// <summary>
         /// MFP是否存在
         /// </summary>
         /// <param name="sUserName"></param>
         /// <returns></returns>

         public bool CheckExist(SectionGroupEntry bean)
         {
             string condition = "GroupID= '" + bean.GroupID + "'";
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
                 sql = "select count(*) from SectionGroup  where " + condition;
             else
                 sql = "select count(*) from SectionGroup";
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
         protected GroupEntry Bind2(DataRow rec)
         {
             GroupEntry beanitem = new GroupEntry();
             beanitem.ID = ConvertObjToInt(rec["ID"]);
             beanitem.GroupName = ConvertObjToString(rec["GroupName"]);

             return beanitem;

         }

         /// <summary>
         /// 查所有数据
         /// add by wangziyang
         /// 2019.09.28
         /// </summary>
         /// <param name="sUserName"></param>
         /// <returns></returns>
         public List<GroupEntry> getAllGroupList(string SectionID)
         {
             StringBuilder sb = new StringBuilder("");
             sb.Append("SELECT ");
             sb.Append("  B.ID ");
             sb.Append("  ,B.GroupName  ");
             sb.Append(" FROM SectionGropy A, GroupInfo B ");
             sb.Append(" where SectionID =  '" + SectionID + "' ");
             sb.Append(" and B.ID =  A.GroupID ");

             String sql = sb.ToString();
             DataSet ds = this.sqlHelper.GetDataSet(sql);
             DataTable dt = new DataTable();
             List<GroupEntry> list = new List<GroupEntry>();
             if (ds != null)
             {
                 dt = ds.Tables[0];
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     GroupEntry bean = Bind2(dt.Rows[i]);
                     list.Add(bean);
                 }
             }
             return list;
         }


         public List<GroupBelongEntry> getGroupBelongList(string SectionID)
         {
             StringBuilder sb = new StringBuilder("");
             sb.Append("SELECT ");
             sb.Append("  B.ID ");
             sb.Append("  ,B.GroupName  ");
             sb.Append(" FROM SectionGropy A, GroupInfo B ");
             sb.Append(" where SecttionID ==  '" + SectionID + "' ");
             sb.Append(" and B.GroupID ==  A.GroupID ");

             String sql = sb.ToString();
             DataSet ds = this.sqlHelper.GetDataSet(sql);
             DataTable dt = new DataTable();
             List<GroupBelongEntry> list = new List<GroupBelongEntry>();
             if (ds != null)
             {
                 dt = ds.Tables[0];
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     GroupBelongEntry beanitem = new GroupBelongEntry();
                     beanitem.GroupID = ConvertObjToInt(dt.Rows[i]["ID"]);
                     beanitem.GroupNM = ConvertObjToString(dt.Rows[i]["GroupName"]);
                     list.Add(beanitem);
                 }
             }
             return list;
         }


         public List<GroupBelongEntry> getGroupNoBelongList()
         {
             StringBuilder sb = new StringBuilder("");
             sb.Append("SELECT ");
             sb.Append("  B.ID ");
             sb.Append("  ,B.GroupName  ");
             sb.Append(" FROM GroupInfo B ");

             String sql = sb.ToString();
             DataSet ds = this.sqlHelper.GetDataSet(sql);
             DataTable dt = new DataTable();
             List<GroupBelongEntry> list = new List<GroupBelongEntry>();
             if (ds != null)
             {
                 dt = ds.Tables[0];
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     GroupBelongEntry beanitem = new GroupBelongEntry();
                     beanitem.GroupID = ConvertObjToInt(dt.Rows[i]["ID"]);
                     beanitem.GroupNM = ConvertObjToString(dt.Rows[i]["GroupName"]);
                     list.Add(beanitem);
                 }
             }

             List<GroupBelongEntry> nolist = new List<GroupBelongEntry>();
             foreach (GroupBelongEntry group in list)
             {
                 if (CheckSectionExist(group.GroupID) == false)
                 {
                     nolist.Add(group);
                 }
             }
             return nolist;
         }

         public void UpdateBelong(string SectionID, int GroupID)
         {
             DeleteGroup(GroupID);
             SectionGroupEntry bean = new SectionGroupEntry();
             bean.SectionID = SectionID;
             bean.GroupID = GroupID;
             Add(bean);
         }
    }
}
