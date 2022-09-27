using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///UserAvailableCount 的摘要说明
/// </summary>
/// // add by Wei Changye 2012.03.11
public class UserAvailableCount 
{
    private string userID;
    private UserAvailableModel model;

    public List<JobTypeList> JobList;
    // JobList WithOut Other Item.
    public List<JobTypeList> JobListWithOutOther;

	public UserAvailableCount(string id)
	{
        userID = id;
        model = new UserAvailableModel();

        // Reprot Job Type List.
        JobList = new List<JobTypeList>();
        // Copy
        JobTypeList item = new JobTypeList(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1,
         UtilConst.ITEM_TITLE_Copy_FunctionId2, UtilConst.ITEM_TITLE_Copy);
        JobList.Add(item);
        // Print
        item = new JobTypeList(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1,
         UtilConst.ITEM_TITLE_Print_FunctionId2, UtilConst.ITEM_TITLE_Print);
        JobList.Add(item);
        // Scan
        item = new JobTypeList(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1,
         UtilConst.ITEM_TITLE_Scan_FunctionId2, UtilConst.ITEM_TITLE_Scan);
        JobList.Add(item);
        // Fax
        item = new JobTypeList(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1,
         99, UtilConst.ITEM_TITLE_Fax);
        JobList.Add(item);
        // Document Filing Print
        item = new JobTypeList(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1,
         UtilConst.ITEM_TITLE_DFPrint_FunctionId2, UtilConst.ITEM_TITLE_DFPrint);
        JobList.Add(item);
        // Scan Save
        item = new JobTypeList(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1,
         UtilConst.ITEM_TITLE_ScanSave_FunctionId2, UtilConst.ITEM_TITLE_ScanSave);
        JobList.Add(item);
        // List Print
        item = new JobTypeList(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1,
         UtilConst.ITEM_TITLE_ListPrint_FunctionId2, UtilConst.ITEM_TITLE_ListPrint);
        JobList.Add(item);
        // Fax (Channel2)
        item = new JobTypeList(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1,
         99, UtilConst.ITEM_TITLE_FaxC2);
        JobList.Add(item);
        // Internet Fax
        item = new JobTypeList(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1,
         99, UtilConst.ITEM_TITLE_IntFax);
        JobList.Add(item);
        // Other
        item = new JobTypeList(UtilConst.ITEM_TITLE_Other_JobId, UtilConst.ITEM_TITLE_Other_FunctionId1,
         UtilConst.ITEM_TITLE_Other_FunctionId2, UtilConst.ITEM_TITLE_Other);
        JobList.Add(item);


        // AvailableReportResult's Job List
        // With Out Other Item.
        JobListWithOutOther = new List<JobTypeList>();
        // Copy
        item = new JobTypeList(UtilConst.ITEM_TITLE_Copy_JobId, UtilConst.ITEM_TITLE_Copy_FunctionId1,
         UtilConst.ITEM_TITLE_Copy_FunctionId2, UtilConst.ITEM_TITLE_Copy);
        JobListWithOutOther.Add(item);
        // Print
        item = new JobTypeList(UtilConst.ITEM_TITLE_Print_JobId, UtilConst.ITEM_TITLE_Print_FunctionId1,
         UtilConst.ITEM_TITLE_Print_FunctionId2, UtilConst.ITEM_TITLE_Print);
        JobListWithOutOther.Add(item);
        // Document Filing Print
        item = new JobTypeList(UtilConst.ITEM_TITLE_DFPrint_JobId, UtilConst.ITEM_TITLE_DFPrint_FunctionId1,
         UtilConst.ITEM_TITLE_DFPrint_FunctionId2, UtilConst.ITEM_TITLE_DFPrint);
        JobListWithOutOther.Add(item);
        // Scan Save
        item = new JobTypeList(UtilConst.ITEM_TITLE_ScanSave_JobId, UtilConst.ITEM_TITLE_ScanSave_FunctionId1,
         UtilConst.ITEM_TITLE_ScanSave_FunctionId2, UtilConst.ITEM_TITLE_ScanSave);
        JobListWithOutOther.Add(item);
        // List Print
        item = new JobTypeList(UtilConst.ITEM_TITLE_ListPrint_JobId, UtilConst.ITEM_TITLE_ListPrint_FunctionId1,
         UtilConst.ITEM_TITLE_ListPrint_FunctionId2, UtilConst.ITEM_TITLE_ListPrint);
        JobListWithOutOther.Add(item);
        // Scan
        item = new JobTypeList(UtilConst.ITEM_TITLE_Scan_JobId, UtilConst.ITEM_TITLE_Scan_FunctionId1,
         UtilConst.ITEM_TITLE_Scan_FunctionId2, UtilConst.ITEM_TITLE_Scan);
        JobListWithOutOther.Add(item);
        // Fax
        item = new JobTypeList(UtilConst.ITEM_TITLE_Fax_JobId, UtilConst.ITEM_TITLE_Fax_FunctionId1,
         99, UtilConst.ITEM_TITLE_Fax);
        JobListWithOutOther.Add(item);
        // Fax (Channel2)
        item = new JobTypeList(UtilConst.ITEM_TITLE_FaxC2_JobId, UtilConst.ITEM_TITLE_FaxC2_FunctionId1,
         99, UtilConst.ITEM_TITLE_FaxC2);
        JobListWithOutOther.Add(item);
        // Internet Fax
        item = new JobTypeList(UtilConst.ITEM_TITLE_IntFax_JobId, UtilConst.ITEM_TITLE_IntFax_FunctionId1,
         99, UtilConst.ITEM_TITLE_IntFax);
        JobListWithOutOther.Add(item);
	}

    public UserAvailableModel Count()
    {
        dtSettingDisp.SettingDispRow Avai_Borrowrow = UtilCommon.GetDispSetting();
        int intUserID = Convert.ToInt32(userID);

        // Get User Name
        dtUserInfoTableAdapters.UserInfoTableAdapter userAdapter = new dtUserInfoTableAdapters.UserInfoTableAdapter();
        dtUserInfo.UserInfoRow userRow = userAdapter.GetDataByUserId(intUserID)[0];

        //get priod
        DateTime start_time;
        DateTime end_time;
        UtilCommon.GetPeriodBy(DateTime.Now, out start_time, out end_time);

        string strSql = "";
        // Get PageCount Group By JobId and GroupId
        strSql = " SELECT JobID , FunctionID , UserID , SUM(Number) AS PageCount " + Environment.NewLine;
        strSql += "  FROM [JobInformation] " + Environment.NewLine;
        strSql += " WHERE UserID in ({0}) " + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) >= " +
    UtilCommon.ConvertDateToSQL(start_time) + Environment.NewLine;
        strSql += "   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= " +
           UtilCommon.ConvertDateToSQL(end_time) + Environment.NewLine;
        strSql += "GROUP BY JobID , FunctionID , UserID " + Environment.NewLine;

        // 1. Get Data of used
        DataTable table = ExecuteDataTable(string.Format(strSql, intUserID));

        // 2.Restiction Date
        dtRestrictionInformationTableAdapters.RestrictionInformationTableAdapter resTableAdapter = new dtRestrictionInformationTableAdapters.RestrictionInformationTableAdapter();
        dtRestrictionInformation.RestrictionInformationDataTable resTable = resTableAdapter.GetDataByUserID(intUserID);

        // User
        model .userName  = userRow.UserName;

        foreach (JobTypeList jobItme in JobListWithOutOther)
        {
            // Already Used
            int BWCount = GetDetailValueFromTable(table, jobItme.JobId, jobItme.BWFunctionId, intUserID, "PageCount");
            // Limit Restiction
            dtRestrictionInformation.RestrictionInformationRow Resrow = GetDetailResRow(resTable, jobItme.JobId, jobItme.BWFunctionId);

            String LimBWCount = UtilCommon.GetLimitInfo(Resrow, BWCount);

            int FCCount = 0;
            String LimFCCount = "0";

            if (jobItme.FCFunctionId != 99)
            {
                //FullColor Already Used
                FCCount = GetDetailValueFromTable(table, jobItme.JobId, jobItme.FCFunctionId, intUserID, "PageCount");
                //FullColor  Limit Restiction
                Resrow = GetDetailResRow(resTable, jobItme.JobId, jobItme.FCFunctionId);
                //FullColor Can to used
                LimFCCount = UtilCommon.GetLimitInfo(Resrow, FCCount);
            }

            if ((Avai_Borrowrow.Dis_Avai_Borrow == 1) && (!UtilConst.STATUS_PROHIBITION_NAME.Equals(LimBWCount)))
            {
                //2011.1.10 Add By SES zhoumiao Ver.1.1 Update ED
                SetlimCountBy(ref LimBWCount, ref LimFCCount);
            }

            string b = LimBWCount;

            if (jobItme.JobId == 1)
            {
                model.copyColor = LimFCCount;
                model.copyBW = LimBWCount;
            }
            else
                if (jobItme.JobId == 2)
                {
                    model.printColor = LimFCCount;
                    model.printBW = LimBWCount;
                }
                else
                    if (jobItme.JobId == 6)
                    {
                        model.scanColor = LimFCCount;
                        model.scanBW = LimBWCount;
                        break;
                    }
        }


        return model;
    }

    #region"function:Get unUsed Page Number for B/W mode and the Full color mode"
    /// <summary>
    /// function:Get unUsed Page Number for B/W mode and the Full color mode
    /// </summary>
    /// <param name="LimBWCount"></param>
    /// <param name="LimFCCount"></param>
    /// <Date>2010.12.13</Date>
    /// <Author>SES Zhou Miao</Author>
    /// <Version>1.10</Version>
    public static void SetlimCountBy(ref String LimBWCount, ref String LimFCCount)
    {
        int LimBW = 0;
        int LimFC = 0;
        // 2011.01.10 Add By SES zhoumiao Ver.1.1 Update ST
        if (UtilConst.STATUS_PROHIBITION_NAME.Equals(LimFCCount))
        {
            LimFCCount = "0";
        }
        // 2011.01.10 Add By SES zhoumiao Ver.1.1 Update ED       

        if (!(LimBWCount.Equals(UtilConst.STATUS_UNLIMITED_NAME) || LimFCCount.Equals(UtilConst.STATUS_UNLIMITED_NAME)))
        {

            LimBW = int.Parse(LimBWCount);
            LimFC = int.Parse(LimFCCount);
            LimBWCount = UtilCommon.IntToMoney(LimBW);
            LimFCCount = UtilCommon.IntToMoney(LimFC);
            int total = LimBW + LimFC;
            if (total > 0)
            {
                if (LimBW < 0)
                {
                    LimBWCount = "0";
                    LimFC += LimBW;
                    LimFCCount = UtilCommon.IntToMoney(LimFC);

                }
                if (LimFC < 0)
                {
                    LimFCCount = "0";
                    LimBW += LimFC;
                    LimBWCount = UtilCommon.IntToMoney(LimBW);
                }

            }
            else
            {
                LimBWCount = "0";
                LimFCCount = "0";

            }
        }
        else
        {
            if (!(LimBWCount.Equals(UtilConst.STATUS_UNLIMITED_NAME)))
            {
                LimBWCount = UtilCommon.IntToMoney(LimBWCount);
            }
            if (!(LimFCCount.Equals(UtilConst.STATUS_UNLIMITED_NAME)))
            {
                LimFCCount = UtilCommon.IntToMoney(LimFCCount);
            }


        }

    }
    #endregion

    #region "GetDetailValueFromTable"
    /// <summary>
    /// GetDetailValueFromTable
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <param name="strColumnName"></param>
    /// <returns></returns>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private int GetDetailValueFromTable(DataTable dt, int JobId, int FunId, int UserID, string strColumnName)
    {
        string strSql = "JobID = {0} AND FunctionID = {1} AND UserID = {2}";
        DataRow[] row = dt.Select(string.Format(strSql, JobId, FunId, UserID));
        if (row == null || row.Length == 0)
        {
            return (0);
        }
        else
        {
            return (int)row[0][strColumnName];
        }

    }
    #endregion

    #region "ExecuteDataTable"
    /// <summary>
    /// ExecuteDataTable
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    /// <Date>2010.06.28</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public DataTable ExecuteDataTable(string sql)
    {
        DataTable data = new DataTable();

        using (SqlConnection con = new SqlConnection(UtilCommon.DBConnectionStrings))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(data);
                return data;
            }
        }
    }
    #endregion

    #region "Get Detail RestrictionInformationDataRow"

    /// <summary>
    /// Get Detail RestrictionInformationDataRow
    /// </summary>
    /// <param name="table"></param>
    /// <param name="JobId"></param>
    /// <param name="FunId"></param>
    /// <returns></returns>
    /// <Date>2010.07.13</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    private dtRestrictionInformation.RestrictionInformationRow GetDetailResRow(dtRestrictionInformation.RestrictionInformationDataTable table, int JobId, int FunId)
    {
        string strSql = "JobID = {0} AND FunctionID = {1}";
        DataRow[] row = table.Select(string.Format(strSql, JobId, FunId));
        if (row.Length > 0)
        {
            return (dtRestrictionInformation.RestrictionInformationRow)row[0];
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region "Job Type Class"
    /// <summary>
    /// Job Type Class
    /// </summary>
    /// <Date>2010.07.01</Date>
    /// <Author>SES Ji JianXiong</Author>
    /// <Version>0.01</Version>
    public class JobTypeList
    {
        // Job Type's  Id.
        int intJobId;
        // Function Type's  Id(B/W).
        int intBWFunctionId;
        // Function Type's  Id(FullColor).
        int intFCFunctionId;
        // Job Type's Name.
        string strJobNm;

        /// <summary>
        /// JobTypeList
        /// </summary>
        /// <param name="intJobIdIn">Job Type's  Id</param>
        /// <param name="intBWFunctionIdIn">Function Type's  Id(B/W).</param>
        /// <param name="intFCFunctionIdIn">Function Type's  Id(FullColor)</param>
        /// <param name="strJobNmIn">Job Type's Name</param>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public JobTypeList(int intJobIdIn, int intBWFunctionIdIn, int intFCFunctionIdIn, string strJobNmIn)
        {
            // Job Type's  Id.
            intJobId = intJobIdIn;
            // Function Type's  Id(B/W).
            intBWFunctionId = intBWFunctionIdIn;
            // Function Type's  Id(FullColor).
            intFCFunctionId = intFCFunctionIdIn;
            // Job Type's Name.
            strJobNm = strJobNmIn;
        }

        /// <summary>
        /// Job Type's  Id
        /// </summary>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public int JobId
        {
            get
            {
                return intJobId;
            }
        }

        /// <summary>
        /// Function Type's  Id(B/W).
        /// </summary>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public int BWFunctionId
        {
            get
            {
                return intBWFunctionId;
            }
        }

        /// <summary>
        /// Function Type's  Id(Full Color).
        /// </summary>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public int FCFunctionId
        {
            get
            {
                return intFCFunctionId;
            }
        }

        /// <summary>
        /// JobName
        /// </summary>
        /// <Date>2010.07.01</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public String JobName
        {
            get
            {
                return strJobNm;
            }
        }
    }
    #endregion
}