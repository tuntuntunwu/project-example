using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using SimpleEACommon;
using Model;

/// <summary>
/// ProductSQL 的摘要说明
/// </summary>
namespace DAL
{
    public class DalMFP : CommonDal
    {

        public DalMFP()
        {
            //sqlHelper = new SQLHelper();
            this.TblName = "MFPInformation";
            this.KeyName = "SerialNumber";
        }
        protected SqlParameter[] CreateParamList(MFPEntry beanitem)
        {

            SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@SerialNumber",SqlDbType.NVarChar,10,beanitem.SerialNumber),
                sqlHelper.CreateInParam("@ModelName",SqlDbType.NVarChar,2000,beanitem.ModelName),
                sqlHelper.CreateInParam("@IPAddress",SqlDbType.NVarChar,15,beanitem.IPAddress),
                sqlHelper.CreateInParam("@Location",SqlDbType.NVarChar,50,beanitem.Location),
                sqlHelper.CreateInParam("@AdministratorID",SqlDbType.NVarChar,50,beanitem.AdministratorID),
                sqlHelper.CreateInParam("@Password",SqlDbType.NVarChar,50,beanitem.Password),
                sqlHelper.CreateInParam("@PriceID",SqlDbType.Int,4,beanitem.PriceID),
                sqlHelper.CreateInParam("@Label",SqlDbType.Int,4,beanitem.Label),
                sqlHelper.CreateInParam("@Monitor",SqlDbType.Int,4,beanitem.Monitor),
                sqlHelper.CreateInParam("@Prompt",SqlDbType.NVarChar,1024,beanitem.Prompt),

            };
            return ParamList;
        }

        protected MFPEntry Bind(SqlDataReader rec)
        {

            MFPEntry beanitem = new MFPEntry();
            beanitem.SerialNumber = ConvertObjToString(rec["SerialNumber"]);
            beanitem.ModelName = ConvertObjToString(rec["ModelName"]);
            beanitem.IPAddress = ConvertObjToString(rec["IPAddress"]);
            beanitem.Location = ConvertObjToString(rec["Location"]);
            beanitem.AdministratorID = ConvertObjToString(rec["AdministratorID"]);
            beanitem.Password = ConvertObjToString(rec["Password"]);
            beanitem.PriceID = ConvertObjToInt(rec["PriceID"]);
            beanitem.Label = ConvertObjToInt(rec["Label"]);
            beanitem.Monitor = ConvertObjToInt(rec["Monitor"]);
            beanitem.Prompt = ConvertObjToString(rec["Prompt"]);

            rec.Close();

            return beanitem;
        }

        protected MFPEntry Bind(DataRow rec)
        {

            MFPEntry beanitem = new MFPEntry();
            beanitem.SerialNumber = ConvertObjToString(rec["SerialNumber"]);
            beanitem.ModelName = ConvertObjToString(rec["ModelName"]);
            beanitem.IPAddress = ConvertObjToString(rec["IPAddress"]);
            beanitem.Location = ConvertObjToString(rec["Location"]);
            beanitem.AdministratorID = ConvertObjToString(rec["AdministratorID"]);
            beanitem.Password = ConvertObjToString(rec["Password"]);
            beanitem.PriceID = ConvertObjToInt(rec["PriceID"]);
            beanitem.Label = ConvertObjToInt(rec["Label"]);
            beanitem.Monitor = ConvertObjToInt(rec["Monitor"]);
            beanitem.Prompt = ConvertObjToString(rec["Prompt"]);

            return beanitem;

       }

        protected MFPListModel BindM(DataRow rec)
        {

            MFPListModel beanitem = new MFPListModel();
            beanitem.SerialNumber = ConvertObjToString(rec["SerialNumber"]);
            beanitem.ModelName = ConvertObjToString(rec["ModelName"]);
            beanitem.IPAddress = ConvertObjToString(rec["IPAddress"]);
            beanitem.Location = ConvertObjToString(rec["Location"]);
            beanitem.AdministratorID = ConvertObjToString(rec["AdministratorID"]);
            beanitem.Password = ConvertObjToString(rec["Password"]);
            beanitem.PriceID = ConvertObjToInt(rec["PriceID"]);
            beanitem.Label = ConvertObjToInt(rec["Label"]);
            beanitem.Monitor = ConvertObjToInt(rec["Monitor"]);
            beanitem.Prompt = ConvertObjToString(rec["Prompt"]);

            return beanitem;

        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int Add(MFPEntry bean)
        {
        
            try
            {
                StringBuilder sb = new StringBuilder( "");
                sb.Append("INSERT INTO MFPInformation (");
                sb.Append("SerialNumber,ModelName,IPAddress,Location,AdministratorID,Password,PriceID,Label,Monitor,Prompt");
                sb.Append( " )");
                sb.Append( "Values(");
                sb.Append("@SerialNumber,@ModelName,@IPAddress,@Location,@AdministratorID,@Password,@PriceID,@Label,@Monitor,@Prompt");
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
        public int Update(MFPEntry userBean)
        {

            try
            {
                StringBuilder sb = new StringBuilder("");
                sb.Append("UPDATE MFPInformation SET ");
                sb.Append("ModelName=@ModelName,");
                sb.Append("IPAddress=@IPAddress,");
                sb.Append("Location=@Location,");
                sb.Append("AdministratorID=@AdministratorID,");
                sb.Append("Password=@Password,");
                sb.Append("PriceID=@PriceID,");
                sb.Append("Label=@Label,");
                sb.Append("Monitor=@Monitor,");
                sb.Append("Prompt=@Prompt ");
                sb.Append("WHERE SerialNumber=@SerialNumber");

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
        public int Delete(string SerialNumber)
        {
            int ret;
            SqlParameter[] ParamList ={ 
                sqlHelper.CreateInParam("@SerialNumber",SqlDbType.NVarChar,10,SerialNumber)
            };
            try
            {
                string sql = "delete from MFPInformation where SerialNumber =@SerialNumber";
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
        /// 通过UID查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public MFPEntry GetInfoByKey(string SerialNumber)
        {
            MFPEntry entry = new MFPEntry();
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@SerialNumber", SqlDbType.NVarChar, 10, SerialNumber) };
            try
            {
                SqlDataReader rec;
                string sql = "select * from MFPInformation  where SerialNumber=@SerialNumber ";
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
        /// 通过IP查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public MFPEntry GetInfoByIP(string Ip)
        {
            MFPEntry entry = new MFPEntry();
            SqlParameter[] ParamList = { sqlHelper.CreateInParam("@IPAddress", SqlDbType.NVarChar, 15, Ip) };
            try
            {
                SqlDataReader rec;
                string sql = "select * from MFPInformation  where IPAddress=@IPAddress ";
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
        /// MFP是否存在
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>

        public bool CheckMFPExist(string SerialNumber)
        {
            string condition = "SerialNumber= '" + SerialNumber + "'";
            return CheckExist(condition);
        }
        
        /// <summary>
        /// MFP是否存在
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>

        public bool CheckExist(MFPEntry bean)  
        {
            string condition = "SerialNumber= '" + bean.SerialNumber + "'";
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
                sql = "select count(*) from MFPInformation  where " + condition;
            else
                sql = "select count(*) from MFPInformation";
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

        //public int  GetMaxKey()
        //{
        //    string sql;
        //    sql = "SELECT Max(UID) FROM MFPInfomation ";
        //    try
        //    {
        //        int ret = sqlHelper.RunSelectSQLToScalar(sql);
        //        return ret;
        //    }
        //    catch (Exception)
        //    {
        //        return 0;
        //    }
        //    return 0;
        //}


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
                    sqlBuilder.Append(" WHERE MFPInformation LIKE @UName and type>@type  and Valid='1' ");
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
                    sqlBuilder.Append(" WHERE MFPInformation LIKE @UName and DpID =@DpID and type>@type and Valid='1' ");
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
        public void search(Page<MFPEntry> page, Dictionary<String, String> cond)
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
                List<MFPEntry> beanlist = new List<MFPEntry>();
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        beanlist.Add(Bind(dt.Rows[i]));
                    }
                    page.dataList = beanlist;
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
        override public string createSqlBodyM(string oderby)
        {
   
            StringBuilder sqlBuilder = new StringBuilder("");
            sqlBuilder.Append(" SELECT ROW_NUMBER() over(order by ");
            sqlBuilder.Append(oderby);
            sqlBuilder.Append(" ) as ROWNO, A.SerialNumber, A.ModelName, A.IPAddress, A.Location,A.AdministratorID,A.Password,A.PriceID, A.Label, B.Monitor, B.Prompt FROM MFPInformation A ");
//            sqlBuilder.Append(TblName);
            return sqlBuilder.ToString();
        }

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
                    sqlBuilder.Append(" WHERE MFPInformation LIKE @UName and type>@type AND B.DpID = A.DpID  and A.Valid='1' ");
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
                    sqlBuilder.Append(" WHERE MFPInformation LIKE @UName and A.DpID =@DpID and type>@type AND B.DpID = A.DpID and A.Valid='1' ");
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

        public void searchM(Page<MFPListModel> page, Dictionary<String, String> cond)
        {
            try
            {
                string orderby = cond["orderby"];

                string sqlheader = createSqlHeader();
                string sqlfooter = createSqlFooter(page.CurrentPage, page.PageSize);
                string sqlBody = createSqlBodyM(orderby);


                StringBuilder sqlCondition1 = new StringBuilder();
                SqlParameter[] paramList1 = createConditionM(cond, sqlCondition1);
                string SqlCount = "SELECT COUNT(UID) FROM MFPInformation A ";
                string sql2 = SqlCount + sqlCondition1.ToString();
                page.TotalCount = Get_NumBySql(sql2, paramList1);

                StringBuilder sqlCondition2 = new StringBuilder();
                SqlParameter[] paramList2 = createConditionM(cond, sqlCondition2);
                string sql = sqlheader + sqlBody + sqlCondition2.ToString() + sqlfooter;
                DataSet ds = this.sqlHelper.GetDataSet(sql, paramList2);
                DataTable dt = new DataTable();
                List<MFPListModel> beanlist = new List<MFPListModel>();
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        beanlist.Add(BindM(dt.Rows[i]));
                    }
                    page.dataList = beanlist;
                }
                return;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

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
                sql = "select count(SerialNumber) from MFPInformation where " + condition;
            }
            else
            {
                sql = "select count(SerialNumber) from MFPInformation";
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
        protected JobInformationCSVModel BindCSVM(DataRow rec)
        {
            JobInformationCSVModel bean = new JobInformationCSVModel();
            bean.SerialNumber = ConvertObjToString(rec["SerialNumber"]);
            bean.ModelName = ConvertObjToString(rec["ModelName"]);
            return bean;
        }

        public List<JobInformationCSVModel> getMFPReportCSVList(string SearchTxt)
        {
            // 2010.11.24 Update By SES zhoumiao Ver.1.1 Update ED

            StringBuilder sb = new StringBuilder("");
            sb.Append("SELECT ");
            sb.Append("  MFPInformation.SerialNumber      AS SerialNumber ");
            sb.Append("  ,MFPInformation.ModelName      AS ModelName ");
            sb.Append(" FROM MFPInformation  ");
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


    }
}
