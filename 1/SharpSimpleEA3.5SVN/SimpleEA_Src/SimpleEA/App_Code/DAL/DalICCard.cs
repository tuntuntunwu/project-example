using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using Model;
using SimpleEACommon;
using System.Configuration;
using System.Data.SqlClient;


/// <summary>
///DalICCard 的摘要说明
/// </summary>
/// 
namespace DAL
{
    public class DalICCard : CommonDal
    {
        //SQLHelper sqlHelper = new SQLHelper();
        public DalICCard()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        protected SqlParameter[] CreateParamList(UserModel beanitem)
        {
            SqlParameter[] ParamList ={
                //用户设置
                sqlHelper.CreateInParam("@LoginName",SqlDbType.NVarChar,30,beanitem.LoginName),
                sqlHelper.CreateInParam("@ICCardID",SqlDbType.NVarChar,20,beanitem.ICCardID),
                
               };
            return ParamList;
        }

        protected SqlParameter[] CreateParamListTotal(UserModel beanitem)
        {
            SqlParameter[] ParamList ={
                //用户设置
                //ID,UserName,LoginName,Password,ICCardID,PinCode,Email,GroupID,RestrictionID,CreateTime,UpdateTime
                sqlHelper.CreateInParam("@ID",SqlDbType.Int,4,beanitem.ID),
                sqlHelper.CreateInParam("@UserName",SqlDbType.NVarChar,30,beanitem.UserName),
                sqlHelper.CreateInParam("@LoginName",SqlDbType.NVarChar,30,beanitem.LoginName),
                sqlHelper.CreateInParam("@ICCardID",SqlDbType.NVarChar,20,beanitem.ICCardID),
                sqlHelper.CreateInParam("@PinCode",SqlDbType.NVarChar,30,beanitem.PinCode),
                sqlHelper.CreateInParam("@Email",SqlDbType.NVarChar,50,beanitem.ICCardID),
                sqlHelper.CreateInParam("@GroupID",SqlDbType.Int,4,beanitem.GroupID),
                sqlHelper.CreateInParam("@RestrictionID",SqlDbType.Int,4,beanitem.RestrictionID),
                sqlHelper.CreateInParam("@CreateTime",SqlDbType.Time,50,beanitem.CreateTime),
                sqlHelper.CreateInParam("@UpdateTime",SqlDbType.Time,50,beanitem.UpdateTime),
                
               };
            return ParamList;
        }
        public int getImportSql(UserModel userBean)
        {
            StringBuilder sb = new StringBuilder("");
            //更新ICCard操作
            try{

                sb.Append("UPDATE UserInfo SET ");
                sb.Append("ICCardID=@ICCardID");
                sb.Append(" WHERE LoginName=@LoginName");

                SqlParameter[] sqlParam = CreateParamList(userBean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        
        }

        protected UserEntry Bind(DataRow rec)
        {
            //cui20170620
            UserEntry beanitem = new UserEntry();
            beanitem.LoginName = ConvertObjToString(rec["LoginName"]);
            beanitem.ICCardID = ConvertObjToString(rec["ICCardID"]);
            return beanitem;
        }
        protected UserInfoModel BindTotal(DataRow rec)
        {
            //cui20170903
            //ID,UserName,LoginName,Password,ICCardID,PinCode,Email,GroupID,RestrictionID,CreateTime,UpdateTime
            UserInfoModel beanitem = new UserInfoModel();
            beanitem.UserName = ConvertObjToString(rec["UserName"]);
            beanitem.LoginName = ConvertObjToString(rec["LoginName"]);
            beanitem.Password = ConvertObjToString(rec["Password"]);      
            beanitem.ICCardID = ConvertObjToString(rec["ICCardID"]);
            beanitem.PinCode = ConvertObjToString(rec["PinCode"]);
            beanitem.Email = ConvertObjToString(rec["Email"]);
            beanitem.GroupName = ConvertObjToString(rec["GroupName"]);
            beanitem.RestrictionName = ConvertObjToString(rec["RestrictionName"]);
            beanitem.ComeFrom = ConvertObjToString(rec["ComeFrom"]);
            //beanitem.CreateTime = ConvertObjToDateTime(rec["CreateTime"]);
            //beanitem.UpdateTime = ConvertObjToDateTime(rec["UpdateTime"]);
            return beanitem;
        }

        //查询数据库表UserInfo的LoginName和ICCardID
        public List<UserEntry> GetUserEntryByLoginName()
        {
            //DropAndPlusEntry entry = new DropAndPlusEntry()
            UserModel userBean = new UserModel(); ;
            List<UserEntry> lst = new List<UserEntry>();
            try
            {
                SqlDataReader rec;
                StringBuilder sb = new StringBuilder("");
                sb.Append("select LoginName, ICCardId from UserInfo");
                SqlParameter[] sqlParam = CreateParamList(userBean);
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

        ////查询数据库表UserInfo的所有信息
        //public List<UserEntry> GetUserEntry()
        //{
        //    //DropAndPlusEntry entry = new DropAndPlusEntry()
        //    UserModel userBean = new UserModel(); ;
        //    List<UserEntry> lst = new List<UserEntry>();
        //    try
        //    {
        //        SqlDataReader rec;
        //        StringBuilder sb = new StringBuilder("");
        //        //sb.Append("select ID,UserName,LoginName,Password,ICCardID,PinCode,Email,GroupID,RestrictionID,CreateTime,UpdateTime from UserInfo");
        //        sb.Append("select A.UserName,LoginName,Password,ICCardID,PinCode,Email,B.groupName, RestrictionID,CreateTime,UpdateTime from UserInfo from UserInfo A, GroupInfo B, RestrictionInfo C  where ");
        //        SqlParameter[] sqlParam = CreateParamList(userBean);
        //        DataSet ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        ds = this.sqlHelper.GetDataSet(sb.ToString());
        //        if (ds != null)
        //        {
        //            dt = ds.Tables[0];
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                lst.Add(BindTotal(dt.Rows[i]));
        //            }

        //        }
        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        ClassError.CreateErrorLog(ex.Message);
        //        throw new Exception(ex.Message, ex);
        //    }

        //}

        //查询数据库表UserInfo的所有信息
        public List<UserInfoModel> GetUserEntry()
        {
            //DropAndPlusEntry entry = new DropAndPlusEntry()
           // UserModel userBean = new UserModel(); ;
            List<UserInfoModel> lst = new List<UserInfoModel>();
            try
            {
                SqlDataReader rec;
                StringBuilder sb = new StringBuilder("");
                //sb.Append("select ID,UserName,LoginName,Password,ICCardID,PinCode,Email,GroupID,RestrictionID,CreateTime,UpdateTime from UserInfo");
                sb.Append("select A.UserName,A.LoginName,A.Password,A.ICCardID,A.PinCode,A.Email,B.groupName,C.RestrictionName,A.ComeFrom from UserInfo A, GroupInfo B, RestrictionInfo C  where A.LoginName <> 'admin' and A.GroupID = B.ID and A.RestrictionID = C.ID");
                
                
               // SqlParameter[] sqlParam = CreateParamList(userBean);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = this.sqlHelper.GetDataSet(sb.ToString());
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lst.Add(BindTotal(dt.Rows[i]));
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