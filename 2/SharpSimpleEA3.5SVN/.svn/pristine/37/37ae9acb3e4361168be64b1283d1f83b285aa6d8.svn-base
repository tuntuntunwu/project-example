using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using SimpleEACommon;
using System.Windows.Forms;
using Model;

/// <summary>
/// ProductSQL 的摘要说明
/// </summary>
namespace DAL
{
    public class DalUser : CommonDal
    {
        //protected SQLHelper sqlHelper;

        public DalUser()
        {
            //sqlHelper = new SQLHelper();
            //cqq20170620
            //this.TblName = "DUser";
            //this.KeyName = "UID";
            this.TblName = "UserName";
            this.KeyName = "ID";
        }
        protected SqlParameter[] CreateParamList(UserEntry beanitem)
        {

            SqlParameter[] ParamList ={
                
                //sqlHelper.CreateInParam("@UserName",SqlDbType.NVarChar,30,beanitem.UserName),
                //sqlHelper.CreateInParam("@LoginName",SqlDbType.NVarChar,30,beanitem.LoginName),
                //sqlHelper.CreateInParam("@Email",SqlDbType.NVarChar,50,beanitem.Email),                 
                //sqlHelper.CreateInParam("@GroupID",SqlDbType.Int,4,beanitem.GroupID),                        
                //sqlHelper.CreateInParam("@CreateTime",SqlDbType.DateTime,14,beanitem.CreateTime),
                //sqlHelper.CreateInParam("@UpdateTime",SqlDbType.DateTime,14,beanitem.UpdateTime),
                                      
                                          
                //cui20170620
                sqlHelper.CreateInParam("@ID",SqlDbType.Int,4,beanitem.ID),
                sqlHelper.CreateInParam("@UserName",SqlDbType.NVarChar,30,beanitem.UserName),
                sqlHelper.CreateInParam("@LoginName",SqlDbType.NVarChar,30,beanitem.LoginName),
                sqlHelper.CreateInParam("@Password",SqlDbType.NVarChar,20,beanitem.Password),
                sqlHelper.CreateInParam("@ICCardID",SqlDbType.NVarChar,20,beanitem.ICCardID),
                sqlHelper.CreateInParam("@PinCode",SqlDbType.NVarChar,20,beanitem.PinCode),
                sqlHelper.CreateInParam("@Email",SqlDbType.NVarChar,50,beanitem.Email),                
                sqlHelper.CreateInParam("@GroupID",SqlDbType.Int,4,beanitem.GroupID),
                sqlHelper.CreateInParam("@RestrictionID",SqlDbType.Int,4,beanitem.RestrictionID),
                sqlHelper.CreateInParam("@ComeFrom",SqlDbType.Int,4,beanitem.ComeFrom),

                sqlHelper.CreateInParam("@CreateTime",SqlDbType.DateTime,14,beanitem.CreateTime),
                sqlHelper.CreateInParam("@UpdateTime",SqlDbType.DateTime,14,beanitem.UpdateTime),
            
            };
            return ParamList;
        }

        protected UserEntry Bind(SqlDataReader rec)
        {

            UserEntry beanitem = new UserEntry();
            //cui20170620
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.UserName = ConvertObjToString(rec["UserName"]);
            beanitem.LoginName = ConvertObjToString(rec["LoginName"]);
            beanitem.Password = ConvertObjToString(rec["Password"]);
            beanitem.ICCardID = ConvertObjToString(rec["ICCardID"]);
            beanitem.PinCode = ConvertObjToString(rec["PinCode"]);
            beanitem.Email = ConvertObjToString(rec["Email"]);
            beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);
            beanitem.RestrictionID = ConvertObjToInt(rec["RestrictionID"]);
            beanitem.ComeFrom = ConvertObjToInt(rec["ComeFrom"]);
            beanitem.CreateTime = ConvertObjToDateTime(rec["CreateTime"]);
            beanitem.UpdateTime = ConvertObjToDateTime(rec["UpdateTime"]);
;

            rec.Close();

            return beanitem;
        }

        protected UserEntry Bind(DataRow rec)
        {
            //cui20170620
            UserEntry beanitem = new UserEntry();
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.UserName = ConvertObjToString(rec["UserName"]);
            beanitem.LoginName = ConvertObjToString(rec["LoginName"]);
            beanitem.Password = ConvertObjToString(rec["Password"]);
            beanitem.ICCardID = ConvertObjToString(rec["ICCardID"]);
            beanitem.PinCode = ConvertObjToString(rec["PinCode"]);
            beanitem.Email = ConvertObjToString(rec["Email"]);
            beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);
            beanitem.RestrictionID = ConvertObjToInt(rec["RestrictionID"]);
            beanitem.ComeFrom = ConvertObjToInt(rec["ComeFrom"]);

            beanitem.CreateTime = ConvertObjToDateTime(rec["CreateTime"]);
            beanitem.UpdateTime = ConvertObjToDateTime(rec["UpdateTime"]);

            return beanitem;

       }

        protected UserListModel BindM(DataRow rec)
        {
            //cui20170620
            UserListModel beanitem = new UserListModel();
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.UserName = ConvertObjToString(rec["UserName"]);
            beanitem.LoginName = ConvertObjToString(rec["LoginName"]);
            beanitem.Password = ConvertObjToString(rec["Password"]);
            beanitem.ICCardID = ConvertObjToString(rec["ICCardID"]);
            beanitem.PinCode = ConvertObjToString(rec["PinCode"]);
            beanitem.Email = ConvertObjToString(rec["Email"]);
            beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);
            beanitem.RestrictionID = ConvertObjToInt(rec["RestrictionID"]);
            beanitem.ComeFrom = ConvertObjToInt(rec["ComeFrom"]);
            beanitem.CreateTime = ConvertObjToDateTime(rec["CreateTime"]);
            beanitem.UpdateTime = ConvertObjToDateTime(rec["UpdateTime"]);

            return beanitem;

        }
       

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int Add(UserEntry userBean)
        {        
            try
            {
                StringBuilder sb = new StringBuilder( "");
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey() + 1;
                userBean.ID = key;

                /*sb.Append("INSERT INTO UserInfo (");
                sb.Append("ID,UserName,LoginName,Password,Email,GroupID,RestrictionID");
                sb.Append(" )");
                sb.Append("Values(");
                sb.Append("@ID,@UserName,@LoginName,@Password,@Email,@GroupId,@RestrictionID");*/



                //sb.Append(")");

                //sb.Append(",\'" + DateTime.Now.ToString() + "\'");
                //sb.Append(",\'" + DateTime.Now.ToString() + "\'");
                


                sb.Append("INSERT INTO UserInfo (");
                sb.Append("ID,UserName,LoginName,Password,ICCardID,PinCode,Email,GroupID,RestrictionID,ComeFrom,CreateTime,UpdateTime");
                sb.Append(")");
                sb.Append("Values(");
                sb.Append("@ID,@UserName,@LoginName,@Password,@ICCardID,@PinCode,@Email,@GroupID,@RestrictionID,@ComeFrom,@CreateTime,@UpdateTime");
                sb.Append(")");

                SqlParameter[] sqlParam = CreateParamList(userBean);
                string str = sb.ToString();
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
               // MessageBox.Show("测试立即同步按钮1");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int AddForDBAuth(UserEntry userBean)
        {
            try
            {
                StringBuilder sb = new StringBuilder("");
                //ID自动加1（查找到目前ID的最大值，在此基础上加1）
                int key = GetMaxKey() + 1;
                userBean.ID = key;
                userBean.UserName = userBean.UserName + key.ToString();
                sb.Append("INSERT INTO UserInfo (");
                sb.Append("ID,UserName,LoginName,Password,ICCardID,PinCode,Email,GroupID,RestrictionID,ComeFrom,CreateTime,UpdateTime");
                sb.Append(")");
                sb.Append("Values(");
                sb.Append("@ID,@UserName,@LoginName,@Password,@ICCardID,@PinCode,@Email,@GroupID,@RestrictionID,@ComeFrom,@CreateTime,@UpdateTime");
                sb.Append(")");

                SqlParameter[] sqlParam = CreateParamList(userBean);
                string str = sb.ToString();
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
                // MessageBox.Show("测试立即同步按钮1");
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
        public int Update(UserEntry userBean)
        {
            try
            {
                StringBuilder sb = new StringBuilder("");
                sb.Append("UPDATE UserInfo SET ");
                //sb.Append("ID=@ID,");
                sb.Append("UserName=@UserName,");
                sb.Append("LoginName=@LoginName,");
                sb.Append("Password=@Password,");
                sb.Append("ICCardID=@ICCardID,");
                sb.Append("PinCode=@PinCode,");
                sb.Append("Email=@Email,");
                sb.Append("GroupID=@GroupID,");
                sb.Append("RestrictionID=@RestrictionID,");
                sb.Append("ComeFrom=@ComeFrom,");
                sb.Append("CreateTime=@CreateTime,");
                sb.Append("UpdateTime=  ");
                sb.Append("\'" + DateTime.Now.ToString() + "\'");
                sb.Append("WHERE ID=@ID");
                //sb.Append(",\'" + DateTime.Now.ToString() + "\'");
                SqlParameter[] sqlParam = CreateParamList(userBean);
                return (sqlHelper.RunSQL(sb.ToString(), sqlParam));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public int UpdateForDBAuth(UserEntry userBean)
        {
            try
            {
                StringBuilder sb = new StringBuilder("");
                sb.Append("UPDATE UserInfo SET ");
                //sb.Append("ID=@ID,");
                sb.Append("UserName=@UserName,");
                sb.Append("LoginName=@LoginName,");
                sb.Append("Password=@Password,");
                sb.Append("ICCardID=@ICCardID,");
                sb.Append("PinCode=@PinCode,");
                sb.Append("Email=@Email,");
                sb.Append("GroupID=@GroupID,");
                sb.Append("RestrictionID=@RestrictionID,");
                sb.Append("ComeFrom=@ComeFrom,");
                sb.Append("CreateTime=@CreateTime,");
                sb.Append("UpdateTime=  ");
                sb.Append("\'" + DateTime.Now.ToString() + "\'");
                sb.Append("WHERE ID=@ID");
                //sb.Append(",\'" + DateTime.Now.ToString() + "\'");
                userBean.UserName = userBean.UserName + userBean.ID.ToString();
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
                string sql = "delete from UserInfo where ID =@ID";
                ret = sqlHelper.RunSQL(sql, ParamList);
                return ret;
            }
            catch (Exception ex)
            {
                ////ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        /// 通过UID查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public UserEntry GetInfoByID(int id)
        {
            UserEntry entry = new UserEntry();
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@ID", SqlDbType.Int, 4, id) };
            try
            {
                SqlDataReader rec;
                //string sql = "select * from DUser  where UID=@UID  and Valid='1' ";
                string sql = "select * from UserInfo  where ID=@ID   ";
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
        /// 通过UserName查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public UserEntry GetInfoByName(string sUserName)
        {
            UserEntry entry = new UserEntry();
            //SqlParameter[] ParamList = { sqlHelper.CreateInParam("@UName", SqlDbType.NVarChar, 20, sUserName) };
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@UserName", SqlDbType.NVarChar, 30, sUserName) };
            try
            {
                SqlDataReader rec;
                //string sql = "select * from DUser  where UName=@UName";
                string sql = "select * from Userinfo  where UserName=@UserName";
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
            return null;

        }
        public UserEntry GetInfoByICCard(string cardid)
        {
            UserEntry entry = new UserEntry();
            //SqlParameter[] ParamList = { sqlHelper.CreateInParam("@UName", SqlDbType.NVarChar, 20, sUserName) };
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@ICCardID", SqlDbType.NVarChar, 20, cardid) };
            try
            {
                SqlDataReader rec;
                //string sql = "select * from DUser  where UName=@UName";
                string sql = "select * from Userinfo  where ICCardID=@ICCardID";
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
            return entry;

        }
        public UserEntry GetInfoByLoginName(string LoginName)
        {
            UserEntry entry = new UserEntry();
            //SqlParameter[] ParamList = { sqlHelper.CreateInParam("@UName", SqlDbType.NVarChar, 20, sUserName) };
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@LoginName", SqlDbType.NVarChar, 30, LoginName) };
            try
            {
                SqlDataReader rec;
                //string sql = "select * from DUser  where UName=@UName";
                string sql = "select * from Userinfo  where LoginName=@LoginName";
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
            return entry;
        }
        /// <summary>
        /// 由JobID改为GroupID
        /// 通过GroupID查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public UserEntry GetInfoByGroupID(string GroupID)
        {
            UserEntry entry = new UserEntry();
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@GroupID", SqlDbType.Int, 4, GroupID) };
            try
            {
                SqlDataReader rec;
                string sql = "select * from UserInfo  where GroupID=@GroupID  ";
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
        /// 用户是否存在
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>

        public bool CheckExist(UserEntry bean)  
        {
            string condition = "UserName= '" + bean.UserName + "'";
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
                sql = "select count(*) from UserInfo  where " + condition;
            else
                sql = "select count(*) from UserInfo";
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
            sql = "SELECT Max(ID) FROM UserInfo ";
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

        //这部分功能不清楚 cui20170620
        /*
        public SqlParameter[] createCondition(Dictionary<String, String> cond, StringBuilder sqlBuilder)
        {

            string username = cond["username"];
            int dpid;
            if(cond["dpid"]=="") dpid=0;
            else dpid=int.Parse(cond["dpid"]);
            string type = cond["type"];
            string orderby = cond["orderby"];
            SqlParameter[] ParamList = null;
            if (username == "" && dpid==0)
            {
                sqlBuilder.Append(" WHERE Valid='1'  ");
            }
            else
            {
                if (username!="" && dpid == 0)
                {
                    ParamList = new SqlParameter[2];
                    sqlBuilder.Append(" WHERE UName LIKE @UName and type>@type  and Valid='1' ");
                    SqlParameter param1 = sqlHelper.CreateInParam("@UName", SqlDbType.NVarChar, 20, '%' + username + '%');
                    SqlParameter param2 = sqlHelper.CreateInParam("@type", SqlDbType.Int, 4, type );
                    ParamList[0] = param1;
                    ParamList[1] = param2;
                }
                if (username == "" && dpid != 0)
                {
                    ParamList = new SqlParameter[2];
                    sqlBuilder.Append(" WHERE DpID =@DpID and type>@type and Valid='1' ");
                    SqlParameter param1 = sqlHelper.CreateInParam("@DpID", SqlDbType.Int, 4, dpid);
                    SqlParameter param2 = sqlHelper.CreateInParam("@type", SqlDbType.Int, 4, type);
                    ParamList[0] = param1;
                    ParamList[1] = param2;
                }
                if (username != "" && dpid != 0)
                {
                    ParamList = new SqlParameter[3];
                    sqlBuilder.Append(" WHERE UName LIKE @UName and DpID =@DpID and type>@type and Valid='1' ");
                    SqlParameter param1 = sqlHelper.CreateInParam("@UName", SqlDbType.NVarChar, 20, '%' + username + '%');
                    SqlParameter param2 = sqlHelper.CreateInParam("@DpID", SqlDbType.Int, 4, dpid);
                    SqlParameter param3 = sqlHelper.CreateInParam("@type", SqlDbType.Int, 4, type);
                    ParamList[0] = param1;
                    ParamList[1] = param2;
                    ParamList[2] = param3;
                }                               
            }
            return ParamList;
        }


        /// <summary>
        /// 翻页检索
        /// </summary>
        /// <param name="page"></param>
        /// <param name="cond"></param>
        public void search(Page<UserEntry> page, Dictionary<String, String> cond)
        {
            try
            {
                string orderby = cond["orderby"];

                string sqlheader = createSqlHeader();
                string sqlfooter = createSqlFooter(page.CurrentPage, page.PageSize);
                string sqlBody = createSqlBody(orderby);


                StringBuilder sqlCondition1 = new StringBuilder();
                SqlParameter[] paramList1 = createCondition(cond, sqlCondition1);
                string sql2 = createSqlCount() + sqlCondition1.ToString();
                page.TotalCount = Get_NumBySql(sql2, paramList1);

                StringBuilder sqlCondition2 = new StringBuilder();
                SqlParameter[] paramList2 = createCondition(cond, sqlCondition2);
                string sql = sqlheader + sqlBody + sqlCondition2.ToString() + sqlfooter;
                DataSet ds = this.sqlHelper.GetDataSet(sql, paramList2);
                DataTable dt = new DataTable();
                List<UserEntry> userlist = new List<UserEntry>();
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        userlist.Add(Bind(dt.Rows[i]));
                    }
                    page.dataList = userlist;
                }
                return;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        /// <summary>
        /// 多表联合查询（连接数据还没确定）
        /// </summary>
        /// <param name="oderby"></param>
        /// <returns></returns>
        public string createSqlBodyM(string oderby)
        {
   
            StringBuilder sqlBuilder = new StringBuilder("");
            //sqlBuilder.Append(" SELECT ROW_NUMBER() over(order by ");
            //sqlBuilder.Append(oderby);
            //sqlBuilder.Append(" ) as ROWNO, A.UID, A.JobID, A.UName, A.type,A.Email,A.Tele,A.Password, A.DpID, B.DpName FROM DUser A , Department B ");

            sqlBuilder.Append(" SELECT ROW_NUMBER() over(order by ");
            sqlBuilder.Append(oderby);
            sqlBuilder.Append(" ) as ROWNO, A.ID, A.UserName, A.LoginName, A.Password,A.ICCardID,A.PinCode,A.Password,A.PinCode, A.Email,A.GroupID, A.RestrictionID,A.CreateTime,A.UpdateTime,B.DpName FROM UserInfo A , Department B ");
            //sqlBuilder.Append(TblName);
            return sqlBuilder.ToString();
        }
        //这部分功能不清楚  cui20170620
        public SqlParameter[] createConditionM(Dictionary<String, String> cond, StringBuilder sqlBuilder)
        {

            //string username = cond["username"];
            //string orderby = cond["orderby"];
            //SqlParameter[] ParamList = null;
            //if (username == "")
            //{
            //    sqlBuilder.Append(" ");
            //}
            //else
            //{
            //    ParamList = new SqlParameter[1];
            //    sqlBuilder.Append(" WHERE B.DpID = A.DpID and  UName LIKE @UName  ");

            //    SqlParameter param = sqlHelper.CreateInParam("@UName", SqlDbType.NVarChar, 20, '%' + username + '%');
            //    ParamList[0] = param;
            //}
            //return ParamList;
            string username = cond["username"];
            int dpid;
            if (cond["dpid"] == "") dpid = 0;
            else dpid = int.Parse(cond["dpid"]);
            string type = cond["type"];
            string orderby = cond["orderby"];
            SqlParameter[] ParamList = null;
            if (username == "" && dpid == 0)
            {
                sqlBuilder.Append("WHERE B.DpID = A.DpID  and A.Valid='1' ");
            }
            else
            {
                if (username != "" && dpid == 0)
                {
                    ParamList = new SqlParameter[2];
                    sqlBuilder.Append(" WHERE UName LIKE @UName and type>@type AND B.DpID = A.DpID  and A.Valid='1' ");
                    SqlParameter param1 = sqlHelper.CreateInParam("@UName", SqlDbType.NVarChar, 20, '%' + username + '%');
                    SqlParameter param2 = sqlHelper.CreateInParam("@type", SqlDbType.Int, 4, type);
                    ParamList[0] = param1;
                    ParamList[1] = param2;
                }
                if (username == "" && dpid != 0)
                {
                    ParamList = new SqlParameter[2];
                    sqlBuilder.Append(" WHERE A.DpID =@DpID and type>@type AND B.DpID = A.DpID  and A.Valid='1' ");
                    SqlParameter param1 = sqlHelper.CreateInParam("@DpID", SqlDbType.Int, 4, dpid);
                    SqlParameter param2 = sqlHelper.CreateInParam("@type", SqlDbType.Int, 4, type);
                    ParamList[0] = param1;
                    ParamList[1] = param2;
                }
                if (username != "" && dpid != 0)
                {
                    ParamList = new SqlParameter[3];
                    sqlBuilder.Append(" WHERE UName LIKE @UName and A.DpID =@DpID and type>@type AND B.DpID = A.DpID and A.Valid='1' ");
                    SqlParameter param1 = sqlHelper.CreateInParam("@UName", SqlDbType.NVarChar, 20, '%' + username + '%');
                    SqlParameter param2 = sqlHelper.CreateInParam("@DpID", SqlDbType.Int, 4, dpid);
                    SqlParameter param3 = sqlHelper.CreateInParam("@type", SqlDbType.Int, 4, type);
                    ParamList[0] = param1;
                    ParamList[1] = param2;
                    ParamList[2] = param3;
                }
            }
            return ParamList;
        }
        
        public void searchM(Page<UserListModel> page, Dictionary<String, String> cond)
        {
            try
            {
                string orderby = cond["orderby"];

                string sqlheader = createSqlHeader();
                string sqlfooter = createSqlFooter(page.CurrentPage, page.PageSize);
                string sqlBody = createSqlBodyM(orderby);


                StringBuilder sqlCondition1 = new StringBuilder();
                SqlParameter[] paramList1 = createConditionM(cond, sqlCondition1);
                string SqlCount = "SELECT COUNT(UID) FROM DUser A,Department B ";
                string sql2 = SqlCount + sqlCondition1.ToString();
                page.TotalCount = Get_NumBySql(sql2, paramList1);

                StringBuilder sqlCondition2 = new StringBuilder();
                SqlParameter[] paramList2 = createConditionM(cond, sqlCondition2);
                string sql = sqlheader + sqlBody + sqlCondition2.ToString() + sqlfooter;
                DataSet ds = this.sqlHelper.GetDataSet(sql, paramList2);
                DataTable dt = new DataTable();
                List<UserListModel> userlist = new List<UserListModel>();
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        userlist.Add(BindM(dt.Rows[i]));
                    }
                    page.dataList = userlist;
                }
                return;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        */
        /// <summary>
        /// 按条件查看数量,condition代表where后面的语句
        /// </summary>
        ///  <param name="condition">where后面的语句</param>
        /// <returns>int</returns>
        public int GetCount(string condition)
        {
            string sql;
            if (condition.Length > 1)
            {
                sql = "select count(ID) from UserInfo where " + condition;
            }
            else
            {
                sql = "select count(ID) from UserInfo";
            }

            int Num = 0;
            try
            {
                Num = sqlHelper.RunSelectSQLToScalar(sql);
                return Num;
            }
            catch (Exception ex)
            {
                //ClassError.CreateErrorLog(ex.Message);
                throw new Exception(ex.Message, ex);
            }
        }
        //public List<UserEntry> GetAllList(string sql)
        //{

        //    try
        //    {

        //        List<UserEntry> userlist = new List<UserEntry>();
        //        //DataTable dt = new DataTable();
        //        //sqlHelper.RunSQL(sql, ref dt);
        //        DataSet ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        ds = this.sqlHelper.GetDataSet(sql.ToString());
        //        if (ds != null)
        //        {
        //            dt = ds.Tables[0];
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                userlist.Add(Bind(dt.Rows[i]));
        //            }

        //        }
        //        return userlist;
        //    }
        //    catch (Exception ex)
        //    {
        //        ClassError.CreateErrorLog(ex.Message);
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

        //public List<UserEntry> GetList(string username)
        //{

        //    try
        //    {

        //        List<UserEntry> userlist = new List<UserEntry>();
        //        DataSet ds = new DataSet();
        //        DataTable dt = new DataTable();

        //        string sql = "SELECT * FROM User WHERE UName LIKE @UName ";
        //        SqlParameter[] ParamList = new  SqlParameter[1];
        //        SqlParameter param = sqlHelper.CreateInParam("@UName", SqlDbType.NVarChar, 50, username);
        //        ParamList[0] = param;

        //        ds = this.sqlHelper.GetDataSet(sql.ToString(), ParamList);
        //        if (ds != null)
        //        {
        //            dt = ds.Tables[0];
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                userlist.Add(Bind(dt.Rows[i]));
        //            }

        //        }
        //        return userlist;
        //    }
        //    catch (Exception ex)
        //    {
        //        ClassError.CreateErrorLog(ex.Message);
        //        throw new Exception(ex.Message, ex);
        //    }
        //}
        protected JobInformationCSVModel BindCSVM(DataRow rec)
        {
            JobInformationCSVModel bean = new JobInformationCSVModel();
            bean.UserID = ConvertObjToInt(rec["UserID"]);
            bean.UserName = ConvertObjToString(rec["UserName"]);
            bean.LoginName = ConvertObjToString(rec["LoginName"]);
            bean.GroupID = ConvertObjToInt(rec["GroupId"]);
            bean.GroupName = ConvertObjToString(rec["GroupName"]);

            return bean;
        }
        public List<JobInformationCSVModel> getUserReportCSVList(string SearchTxt)
        {
            // 2010.11.24 Update By SES zhoumiao Ver.1.1 Update ED

            StringBuilder sb = new StringBuilder("");
            sb.Append("SELECT ");
            sb.Append("  UserInfo.ID               AS UserId ");
            sb.Append(" ,UserInfo.UserName         AS UserName ");
            sb.Append(" ,UserInfo.LoginName         AS LoginName ");
            sb.Append(" ,GroupInfo.ID              AS GroupId ");
            sb.Append(" ,GroupInfo.GroupName        AS GroupName ");
            sb.Append(" FROM [UserInfo] LEFT JOIN ");
            sb.Append("  [GroupInfo] ON GroupInfo.ID = GroupID");
            sb.Append(SearchTxt);

            SqlParameter[] ParamList = null;

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
        public List<JobInformationCSVModel> getGroupUserVList(int groupID)
        {
            // 2010.11.24 Update By SES zhoumiao Ver.1.1 Update ED

            StringBuilder sb = new StringBuilder("");
            sb.Append("SELECT ");
            sb.Append("  UserInfo.ID               AS UserId ");
            sb.Append(" ,UserInfo.UserName         AS UserName ");
            sb.Append(" ,UserInfo.LoginName         AS LoginName ");
            sb.Append(" ,GroupInfo.ID              AS GroupId ");
            sb.Append(" ,GroupInfo.GroupName        AS GroupName ");
            sb.Append(" FROM [UserInfo] LEFT JOIN ");
            sb.Append("  [GroupInfo] ON GroupInfo.ID = GroupID");
            sb.Append(" WHERE UserInfo.GroupID = @GroupID" );
            sb.Append(" ORDER BY UserInfo.ID asc");

            
            SqlParameter[] ParamList = null;
            ParamList = new SqlParameter[1];
            SqlParameter param1 = sqlHelper.CreateInParam("@GroupID", SqlDbType.Int, 4, groupID);
            ParamList[0] = param1;

            String sql = sb.ToString();
            DataSet ds = this.sqlHelper.GetDataSet(sql, ParamList);
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
    }
}
