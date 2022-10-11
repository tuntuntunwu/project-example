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
    public class DalLogInfomation : CommonDal
    {

        public DalLogInfomation()
        {
            this.TblName = "MFPInformation";
            this.KeyName = "SerialNumber";
        }
        protected SqlParameter[] CreateParamList(LogInfomationEntry beanitem)
        {

            SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@ID",SqlDbType.Int,4,beanitem.ID),
                sqlHelper.CreateInParam("@Time",SqlDbType.DateTime,14,beanitem.Time),
                sqlHelper.CreateInParam("@UserName",SqlDbType.NVarChar,30,beanitem.UserName),
                sqlHelper.CreateInParam("@LoginName",SqlDbType.NVarChar,30,beanitem.LoginName),
                sqlHelper.CreateInParam("@GroupID",SqlDbType.Int,4,beanitem.GroupID),

                sqlHelper.CreateInParam("@GroupName",SqlDbType.NVarChar,30,beanitem.GroupName),
                sqlHelper.CreateInParam("@Jobuid",SqlDbType.NVarChar,25,beanitem.Jobuid),
                sqlHelper.CreateInParam("@SerialNumber",SqlDbType.NVarChar,10,beanitem.SerialNumber),
                sqlHelper.CreateInParam("@MFPName",SqlDbType.NVarChar,50,beanitem.MFPName),
                sqlHelper.CreateInParam("@MFPModel",SqlDbType.NVarChar,2000,beanitem.MFPModel),
                sqlHelper.CreateInParam("@MFPIPAddress",SqlDbType.NVarChar,15,beanitem.MFPIPAddress),

                sqlHelper.CreateInParam("@Duplex",SqlDbType.Int,4,beanitem.Duplex),
                sqlHelper.CreateInParam("@JobID",SqlDbType.Int,4,beanitem.JobID),
                sqlHelper.CreateInParam("@FunctionID",SqlDbType.Int,4,beanitem.FunctionID),
                sqlHelper.CreateInParam("@FileName",SqlDbType.NVarChar,256,beanitem.FileName),
                sqlHelper.CreateInParam("@PageID",SqlDbType.Int,4,beanitem.PageID),

                sqlHelper.CreateInParam("@Number",SqlDbType.Int,4,beanitem.Number),
                sqlHelper.CreateInParam("@PapeCount",SqlDbType.Int,4,beanitem.PapeCount),
                sqlHelper.CreateInParam("@CopyCount",SqlDbType.Int,4,beanitem.CopyCount),                
                sqlHelper.CreateInParam("@SpendMoney",SqlDbType.Decimal,10,beanitem.SpendMoney),
                sqlHelper.CreateInParam("@PriceDetailID",SqlDbType.Int,4,beanitem.PriceDetailID),

                sqlHelper.CreateInParam("@Status",SqlDbType.Int,4,beanitem.Status),
                sqlHelper.CreateInParam("@ErrorCode",SqlDbType.NVarChar,50,beanitem.ErrorCode),
                sqlHelper.CreateInParam("@MFPPrintTaskID",SqlDbType.Int,4,beanitem.MFPPrintTaskID),
                sqlHelper.CreateInParam("@DspNumber",SqlDbType.Int,4,beanitem.DspNumber),
                sqlHelper.CreateInParam("@DspPapeCount",SqlDbType.Int,4,beanitem.DspPapeCount),
            };
            return ParamList;
        }

        protected LogInfomationEntry Bind(SqlDataReader rec)
        {

            LogInfomationEntry beanitem = new LogInfomationEntry();
            beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.Time = ConvertObjToDateTime(rec["Time"]);
            beanitem.UserName = ConvertObjToString(rec["UserName"]);
            beanitem.LoginName = ConvertObjToString(rec["LoginName"]);
            beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);

            beanitem.GroupName = ConvertObjToString(rec["GroupName"]);
            beanitem.Jobuid = ConvertObjToString(rec["Jobuid"]);
            beanitem.SerialNumber = ConvertObjToString(rec["SerialNumber"]);
            beanitem.MFPName = ConvertObjToString(rec["MFPName"]);
            beanitem.MFPModel = ConvertObjToString(rec["MFPModel"]);
            beanitem.MFPIPAddress = ConvertObjToString(rec["MFPIPAddress"]);
            
            beanitem.Duplex = ConvertObjToInt(rec["Duplex"]);
            beanitem.JobID = ConvertObjToInt(rec["JobID"]);
            beanitem.FunctionID = ConvertObjToInt(rec["FunctionID"]);
            beanitem.FileName = ConvertObjToString(rec["FileName"]);
            beanitem.PageID = ConvertObjToInt(rec["PageID"]);

            beanitem.Number = ConvertObjToInt(rec["Number"]);
             beanitem.PapeCount = ConvertObjToInt(rec["PapeCount"]);
             beanitem.CopyCount = ConvertObjToInt(rec["CopyCount"]);
             beanitem.SpendMoney = ConvertObjToInt(rec["SpendMoney"]);
             beanitem.PriceDetailID = ConvertObjToInt(rec["PriceDetailID"]);

            beanitem.Status = ConvertObjToInt(rec["Status"]);
            beanitem.ErrorCode = ConvertObjToString(rec["ErrorCode"]);
            beanitem.MFPPrintTaskID = ConvertObjToInt(rec["MFPPrintTaskID"]);
            beanitem.DspNumber = ConvertObjToInt(rec["DspNumber"]);
            beanitem.DspPapeCount = ConvertObjToInt(rec["DspPapeCount"]);

            rec.Close();

            return beanitem;
        }

        protected LogInfomationEntry Bind(DataRow rec)
        {

            LogInfomationEntry beanitem = new LogInfomationEntry(); beanitem.ID = ConvertObjToInt(rec["ID"]);
            beanitem.Time = ConvertObjToDateTime(rec["Time"]);
            beanitem.UserName = ConvertObjToString(rec["UserName"]);
            beanitem.LoginName = ConvertObjToString(rec["LoginName"]);
            beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);

            beanitem.GroupName = ConvertObjToString(rec["GroupName"]);
            beanitem.Jobuid = ConvertObjToString(rec["Jobuid"]);
            beanitem.SerialNumber = ConvertObjToString(rec["SerialNumber"]);
            beanitem.MFPName = ConvertObjToString(rec["MFPName"]);
            beanitem.MFPModel = ConvertObjToString(rec["MFPModel"]);
            beanitem.MFPIPAddress = ConvertObjToString(rec["MFPIPAddress"]);

            beanitem.Duplex = ConvertObjToInt(rec["Duplex"]);
            beanitem.JobID = ConvertObjToInt(rec["JobID"]);
            beanitem.FunctionID = ConvertObjToInt(rec["FunctionID"]);
            beanitem.FileName = ConvertObjToString(rec["FileName"]);
            beanitem.PageID = ConvertObjToInt(rec["PageID"]);

            beanitem.Number = ConvertObjToInt(rec["Number"]);
            beanitem.PapeCount = ConvertObjToInt(rec["PapeCount"]);
            beanitem.CopyCount = ConvertObjToInt(rec["CopyCount"]);
            beanitem.SpendMoney = ConvertObjToInt(rec["SpendMoney"]);
            beanitem.PriceDetailID = ConvertObjToInt(rec["PriceDetailID"]);

            beanitem.Status = ConvertObjToInt(rec["Status"]);
            beanitem.ErrorCode = ConvertObjToString(rec["ErrorCode"]);
            beanitem.MFPPrintTaskID = ConvertObjToInt(rec["MFPPrintTaskID"]);
            beanitem.DspNumber = ConvertObjToInt(rec["DspNumber"]);
            beanitem.DspPapeCount = ConvertObjToInt(rec["DspPapeCount"]);

            return beanitem;

       }

        protected LogInfomationModel BindM(DataRow rec)
        {

            LogInfomationModel beanitem = new LogInfomationModel();
            //cui20170820(ColorMode;//色彩  SheetCount;//面数  未添加)
            beanitem.ID = ConvertObjToInt(rec["Id"]);
            beanitem.PriceDetailID = ConvertObjToString(rec["PriceDetailID"]);
            beanitem.FileName = ConvertObjToString(rec["FileName"]);
            beanitem.Number = ConvertObjToString(rec["Number"]);
            beanitem.MFPPrintTaskID = ConvertObjToString(rec["MFPPrintTaskID"]);
            beanitem.ProcessTime = ConvertObjToDateTime(rec["ProcessTime"]);
            beanitem.UserName = ConvertObjToString(rec["UserName"]);
            beanitem.MFPModelName = ConvertObjToString(rec["MFPModelName"]);
            beanitem.MFPSerialNumber = ConvertObjToString(rec["MFPSerialNumber"]);
            beanitem.JobType = ConvertObjToInt(rec["JobType"]);
            beanitem.PageID = ConvertObjToString(rec["PageID"]);
            beanitem.Duplex = ConvertObjToString(rec["Duplex"]);
            beanitem.PapeCount = ConvertObjToString(rec["PapeCount"]);
            beanitem.CopyCount = ConvertObjToString(rec["CopyCount"]);
            beanitem.Cost = ConvertObjToString(rec["Cost"]);
            beanitem.Status = ConvertObjToString(rec["Status"]);

            return beanitem;

        }

        /// <summary>
        /// 通过UID查一条数据
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public LogInfomationEntry GetInfoByKey(string SerialNumber)
        {
            LogInfomationEntry entry = new LogInfomationEntry();
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
        public void search(Page<LogInfomationEntry> page, Dictionary<String, String> cond)
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

                List<LogInfomationEntry> beanlist = new List<LogInfomationEntry>();
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
        public string createSqlBodyM(string oderby)
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
            string strUserLogin = cond["UserLogin"];
            string strMFPNumber = cond["MFPNumber"];
            string strStatusTarget = cond["StatusTarget"];
            string strJobtypeTarget = cond["JobtypeTarget"];
            string strStartPeriod = cond["StartPeriod"];
            string strEndPeriod = cond["EndPeriod"];
            SqlParameter[] ParamList = new SqlParameter[7];

            DateTime StartPeriod;
            DateTime EndPeriod;
            UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);
            String StartDate = ConvertDateToSQL(StartPeriod);
            String EndDate = ConvertDateToSQL(EndPeriod);
            String sql = "";
            //The Search Condition for the search button.
            if (!(string.IsNullOrEmpty(strUserLogin)))
            {
                StartDate = strUserLogin;
            }

            if (!(string.IsNullOrEmpty(strEndPeriod)))
            {
                EndDate = strEndPeriod;
            }
			SqlParameter param  = null;
            sqlBuilder.Append(" WHERE  SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= @StartDate ");
            sqlBuilder.Append("  AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= @EndDate ");
			param = sqlHelper.CreateInParam("@StartDate", SqlDbType.DateTime, 20, StartDate);
			ParamList[0] = param;
			param = sqlHelper.CreateInParam("@EndDate", SqlDbType.DateTime, 20, EndDate);
			ParamList[1] = param;

			if (!(string.IsNullOrEmpty(strUserLogin)))
			{           
				sqlBuilder.Append(" AND (UserName LIKE @UserName" );
                param = sqlHelper.CreateInParam("@UserName", SqlDbType.NVarChar, 10,  "%" + strUserLogin + "%");
                ParamList[2] = param;
				sqlBuilder.Append(" OR LoginName LIKE @LoginName" );
                param = sqlHelper.CreateInParam("@LoginName", SqlDbType.NVarChar, 10,  "%" + strUserLogin + "%");
                ParamList[3] = param;
			}
			if (!(string.IsNullOrEmpty(strMFPNumber)))
			{
				sqlBuilder.Append(" AND SerialNumber = @SerialNumber" );
                param = sqlHelper.CreateInParam("@SerialNumber", SqlDbType.NVarChar, 10, strMFPNumber);
                ParamList[4] = param;
			}
			if (!(string.IsNullOrEmpty(strStatusTarget)))
			{
				sqlBuilder.Append(" AND Status IN @Status " );
                param = sqlHelper.CreateInParam("@Status", SqlDbType.Int, 4,  strStatusTarget );
                ParamList[5] = param;
			}
			if (!(string.IsNullOrEmpty(strJobtypeTarget)))
			{
				sqlBuilder.Append(" AND JobID = @JobID" );
                param = sqlHelper.CreateInParam("@JobID", SqlDbType.Int, 4, strJobtypeTarget);
                ParamList[6] = param;
			}        

            return ParamList;
        }

        public string LogViewSql( int Dsp_Count_mode, int Dsp_A3_A4 )
        {
            string sql = "";

            string selNumber = "";
            string sqlPageCount = "";
            string sqlConst = "";
            if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A4))
            {
                selNumber = ",Number  AS SheetCount";
                sqlPageCount = ",PapeCount  AS PapeCount";
                if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
                {
                    sqlConst = "  ,SpendMoney           AS Cost ";
                }
                else
                {
                    sqlConst = "  ,Number           AS Cost ";
                }
            }
            else
            {
                selNumber = ",DspNumber  AS SheetCount";
                sqlPageCount = ",DspPapeCount  AS PapeCount";
                if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
                {
                    sqlConst = "  ,SpendMoney           AS Cost ";
                }
                else
                {
                    sqlConst = "  ,DspNumber           AS Cost ";
                }
            }


            sql = "SELECT"
                    + "  ID                    AS Id" + Environment.NewLine
                     + "  ,case Duplex when 1   then   '单面' when 2 then '双面' when 3 then '双面' when 4 then '双面' when 5 then '双面' else ' ' end            AS Duplex " + Environment.NewLine

                    + "  ,PriceDetailID        AS PriceDetailID " + Environment.NewLine
                    + sqlConst + Environment.NewLine
                    + sqlPageCount + Environment.NewLine
                    + "  ,CopyCount            AS CopyCount " + Environment.NewLine
                    + " ,Time   AS ProcessTime"
                    + " ,Filename   " + Environment.NewLine
                     + " ,UserName  AS UserName" + Environment.NewLine
                    + ",MFPModel   AS  MFPModelName" + Environment.NewLine
                    + ",MFPPrintTaskID   AS  MFPPrintTaskID" + Environment.NewLine
                    + ",SerialNumber   AS MFPSerialNumber" + Environment.NewLine
                    + " ,JobType=CASE WHEN LogInformation.JobID='8' " + Environment.NewLine
                    + "  AND LogInformation.FunctionID='2'" + Environment.NewLine
                    + "  THEN (SELECT JobTypeInformation.JobNameDisp" + Environment.NewLine
                    + "  + FunctionTypeInformation.FunctionNameDisp"
                    + "  FROM FunctionTypeInformation,JobTypeInformation" + Environment.NewLine
                    + "  WHERE FunctionTypeInformation.JobID = JobTypeInformation.ID" + Environment.NewLine
                    + "  AND FunctionTypeInformation.FunctionID = LogInformation.FunctionID" + Environment.NewLine
                    + "  AND JobTypeInformation.ID = '8')" + Environment.NewLine
                    + "  ELSE ISNULL((SELECT JobTypeInformation.JobNameDisp " + Environment.NewLine
                    + "  FROM JobTypeInformation" + Environment.NewLine
                    + "  WHERE JobTypeInformation.ID = LogInformation.JobID), '未知')" + Environment.NewLine
                    + "  END" + Environment.NewLine
                    + " ,ColorMode=CASE WHEN LogInformation.JobID='8' " + Environment.NewLine
                    + "  AND LogInformation.FunctionID='2'" + Environment.NewLine
                    + "  THEN '黑白'" + Environment.NewLine
                    + "  WHEN LogInformation.FunctionID IS NULL" + Environment.NewLine
                    + "  THEN '-'" + Environment.NewLine
                    + "  ELSE (SELECT FunctionTypeInformation.FunctionNameDisp " + Environment.NewLine
                    + "  FROM FunctionTypeInformation" + Environment.NewLine
                    + "  WHERE FunctionTypeInformation.JobID = LogInformation.JobID" + Environment.NewLine
                    + "  AND FunctionTypeInformation.FunctionID = LogInformation.FunctionID)" + Environment.NewLine
                    + "  END" + Environment.NewLine
                    + selNumber + Environment.NewLine
                    + " ,Status=CASE WHEN LogInformation.Status ='0'" + Environment.NewLine
                    + "  THEN '开始'" + Environment.NewLine
                    + "  WHEN LogInformation.Status ='3'" + Environment.NewLine
                    + "  THEN '取消'" + Environment.NewLine
                    + "  WHEN LogInformation.Status ='4'" + Environment.NewLine
                    + "  THEN '挂起'" + Environment.NewLine
                    + "  WHEN LogInformation.Status ='5'" + Environment.NewLine
                    + "  THEN '完成'" + Environment.NewLine
                    + "  ELSE '错误'" + Environment.NewLine
                    + "  END" + Environment.NewLine
                    + ",PageID=CASE WHEN PageID IS NULL" + Environment.NewLine
                    + "  THEN '-'" + Environment.NewLine
                    + "  ELSE (SELECT PaperName " + Environment.NewLine
                    + "  FROM PaperSizeInformation" + Environment.NewLine
                    + "  WHERE ID = LogInformation.PageID)" + Environment.NewLine
                    + "  END "
                    + " ,CheckCopy=CASE WHEN LogInformation.FileName is null" + Environment.NewLine
                    + "  THEN ''" + Environment.NewLine
                    + "  ELSE '查看'" + Environment.NewLine
                    + "  END" + Environment.NewLine
                    + "   FROM  [LogInformation]" + Environment.NewLine;

            return sql;

        }
        public string createConditionNoParameter(Dictionary<String, String> cond)
        {
            string strUserLogin = cond["UserLogin"];
            string strMFPNumber = cond["MFPNumber"];
            string strStatusTarget = cond["StatusTarget"];
            string strJobtypeTarget = cond["JobtypeTarget"];
            string strStartPeriod = cond["StartPeriod"];
            string strEndPeriod = cond["EndPeriod"];
            int Dsp_Count_mode = int.Parse(cond["Dsp_Count_mode"]);
            int Dsp_A3_A4 = int.Parse(cond["Dsp_A3_A4"]);


            DateTime StartPeriod;
            DateTime EndPeriod;
            UtilCommon.GetSTartEndTimeBy(DateTime.Now, out StartPeriod, out EndPeriod);
            String StartDate = ConvertDateToSQL(StartPeriod);
            String EndDate = ConvertDateToSQL(EndPeriod);
            if (!(string.IsNullOrEmpty(strUserLogin)))
            {
                StartDate = strUserLogin;
            }

            if (!(string.IsNullOrEmpty(strEndPeriod)))
            {
                EndDate = strEndPeriod;
            }

            StringBuilder sqlBuilder = new StringBuilder( "");
            string sqlBody = LogViewSql(Dsp_Count_mode, Dsp_A3_A4);
            sqlBuilder.Append(sqlBody);

            sqlBuilder.Append(" WHERE  SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {0}");
		    sqlBuilder.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {1}");
			if (string.IsNullOrEmpty(strUserLogin))
			{           
				sqlBuilder.Append(" AND (UserName LIKE " + ConvertStringToSQL("%" + strUserLogin + "%") );
				sqlBuilder.Append(" OR LoginName LIKE " + ConvertStringToSQL("%" + strUserLogin + "%") + ")");
			}
			if (!(string.IsNullOrEmpty(strMFPNumber)))
			{
				sqlBuilder.Append(" AND SerialNumber = " + ConvertStringToSQL(strMFPNumber));
			}
			if (!(string.IsNullOrEmpty(strStatusTarget)))
			{
				sqlBuilder.Append(" AND Status IN (" + strStatusTarget + ")");
			}
			if (!(string.IsNullOrEmpty(strJobtypeTarget)))
			{
				sqlBuilder.Append(" AND JobID = " + ConvertStringToSQL(strJobtypeTarget) );
			}        

			string sql = sqlBuilder.ToString();
            string.Format(sql, StartDate, EndDate);  
            return sql;
        }
        public void searchM(Page<LogInfomationModel> page, Dictionary<String, String> cond)
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
                List<LogInfomationModel> beanlist = new List<LogInfomationModel>();
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
        public List<LogInfomationModel> searchNOPage(Dictionary<String, String> cond)
        {
            try
            {

                string sql = createConditionNoParameter(cond);
                DataSet ds = this.sqlHelper.GetDataSet(sql);
                DataTable dt = new DataTable();
                List<LogInfomationModel> beanlist = new List<LogInfomationModel>();
                if (ds != null)
                {
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        beanlist.Add(BindM(dt.Rows[i]));
                    }
                }
                return beanlist;
            }
            catch (Exception e)
            {

                throw e;
                return null;
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
        

    }
}
