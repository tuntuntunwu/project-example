using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;
//using DataIDType = Model.JobInformationEntry;

/// <summary>
/// ProductSQL 的摘要说明
/// </summary>
namespace DAL
{
    public class DalJobInformation : CommonDal
    {
        public DalJobInformation()
        {          
        }
        protected  SqlParameter[] CreateParamList(JobInformationEntry beanitem)
        {
            SqlParameter[] ParamList ={
                sqlHelper.CreateInParam("@JobInfoID",SqlDbType.NVarChar,32,beanitem.JobInfoID),
                sqlHelper.CreateInParam("@UserID",SqlDbType.NVarChar,32,beanitem.UserID),
                sqlHelper.CreateInParam("@GroupID",SqlDbType.NVarChar,32,beanitem.GroupID),
                sqlHelper.CreateInParam("@JobID",SqlDbType.Int,4,beanitem.JobID),
                sqlHelper.CreateInParam("@FunctionID",SqlDbType.Int,4,beanitem.FunctionID),
                sqlHelper.CreateInParam("@PageID",SqlDbType.Int,4,beanitem.PageID),
                sqlHelper.CreateInParam("@Number",SqlDbType.Int,4,beanitem.Number),
                sqlHelper.CreateInParam("@PapeCount",SqlDbType.Int,4,beanitem.PapeCount),
                sqlHelper.CreateInParam("@CopyCount",SqlDbType.Int,4,beanitem.CopyCount),
                sqlHelper.CreateInParam("@Duplex",SqlDbType.Int,4,beanitem.Duplex),
                sqlHelper.CreateInParam("@SpendMoney",SqlDbType.Decimal,8,beanitem.SpendMoney),
                sqlHelper.CreateInParam("@PriceDetailID",SqlDbType.NVarChar,32,beanitem.PriceDetailID),
                sqlHelper.CreateInParam("@SerialNumber",SqlDbType.NVarChar,10,beanitem.SerialNumber),
                sqlHelper.CreateInParam("@Time",SqlDbType.DateTime,14,beanitem.Time),
                sqlHelper.CreateInParam("@DspNumber",SqlDbType.Int,4,beanitem.DspNumber),
                sqlHelper.CreateInParam("@DspPapeCount",SqlDbType.Int,4,beanitem.DspPapeCount),
            };
            return ParamList;
        }

        protected JobInformationEntry Bind(DataRow rec)
        {
            JobInformationEntry beanitem = new JobInformationEntry();//这句很重要，确保每次BIND前，对象清空
            beanitem.JobInfoID = ConvertObjToInt(rec["JobInfoID"]);
            beanitem.UserID = ConvertObjToInt(rec["UserID"]);
            beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);
            beanitem.JobID = ConvertObjToInt(rec["JobID"]);
            beanitem.FunctionID = ConvertObjToInt(rec["FunctionID"]);
            beanitem.PageID = ConvertObjToInt(rec["PageID"]);
            beanitem.Number = ConvertObjToInt(rec["Number"]);
            beanitem.PapeCount = ConvertObjToInt(rec["PapeCount"]);
            beanitem.CopyCount = ConvertObjToInt(rec["CopyCount"]);
            beanitem.Duplex = ConvertObjToInt(rec["Duplex"]);
            beanitem.SpendMoney = ConvertObjToFloat(rec["SpendMoney"]);
            beanitem.PriceDetailID = ConvertObjToInt(rec["PriceDetailID"]);
            beanitem.SerialNumber = ConvertObjToString(rec["SerialNumber"]);
            beanitem.Time = ConvertObjToDateTime(rec["Time"]);
            beanitem.DspNumber = ConvertObjToInt(rec["DspNumber"]);
            beanitem.DspPapeCount = ConvertObjToInt(rec["DspPapeCount"]);

            return beanitem;
        }

        protected JobInformationEntry Bind(SqlDataReader rec)
        {
            JobInformationEntry beanitem = new JobInformationEntry();
            beanitem.JobInfoID = ConvertObjToInt(rec["JobInfoID"]);
            beanitem.UserID = ConvertObjToInt(rec["UserID"]);
            beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);
            beanitem.JobID = ConvertObjToInt(rec["JobID"]);
            beanitem.FunctionID = ConvertObjToInt(rec["FunctionID"]);
            beanitem.PageID = ConvertObjToInt(rec["PageID"]);
            beanitem.Number = ConvertObjToInt(rec["Number"]);
            beanitem.PapeCount = ConvertObjToInt(rec["PapeCount"]);
            beanitem.CopyCount = ConvertObjToInt(rec["CopyCount"]);
            beanitem.Duplex = ConvertObjToInt(rec["Duplex"]);
            beanitem.SpendMoney = ConvertObjToFloat(rec["SpendMoney"]);
            beanitem.PriceDetailID = ConvertObjToInt(rec["PriceDetailID"]);
            beanitem.SerialNumber = ConvertObjToString(rec["SerialNumber"]);
            beanitem.Time = ConvertObjToDateTime(rec["Time"]);
            beanitem.DspNumber = ConvertObjToInt(rec["DspNumber"]);
            beanitem.DspPapeCount = ConvertObjToInt(rec["DspPapeCount"]);
           
            rec.Close();

            return beanitem;
        }

        protected JobInformationCSVModel BindCSVBean(DataRow rec)
        {
            JobInformationCSVModel beanitem = new JobInformationCSVModel();
            //beanitem.UserID = ConvertObjToInt(rec["UserID"]);
            //beanitem.UserName = ConvertObjToString(rec["UserName"]);
            //beanitem.GroupID = ConvertObjToInt(rec["GroupID"]);
            //beanitem.GroupName = ConvertObjToString(rec["GroupName"]);

            beanitem.CopyBWMoney =  ConvertObjToFloat(rec["CopyBWMoney"]);
            beanitem.CopyColorMoney = ConvertObjToFloat(rec["CopyColorMoney"]);


            beanitem.PrintBWMoney = ConvertObjToFloat(rec["PrintBWMoney"]);
            beanitem. PrintColorMoney=  ConvertObjToFloat(rec["PrintColorMoney"]);

            beanitem. ScanBWMoney=  ConvertObjToFloat(rec["ScanBWMoney"]);
            beanitem. ScanColorMoney=  ConvertObjToFloat(rec["ScanColorMoney"]);

            beanitem. FaxBWMoney=  ConvertObjToFloat(rec["FaxBWMoney"]);

            beanitem. DFPrintBWMoney=  ConvertObjToFloat(rec["DFPrintBWMoney"]);
            beanitem. DFPrintColorMoney=  ConvertObjToFloat(rec["DFPrintColorMoney"]);

            beanitem. ScanSaveBWMoney=  ConvertObjToFloat(rec["ScanSaveBWMoney"]);
            beanitem. ScanSaveColorMoney=  ConvertObjToFloat(rec["ScanSaveColorMoney"]);

            beanitem. ListPrintBWMoney=  ConvertObjToFloat(rec["ListPrintBWMoney"]);
            beanitem. ListPrintColorMoney=  ConvertObjToFloat(rec["ListPrintColorMoney"]);

            beanitem. FaxChannelBWMoney=  ConvertObjToFloat(rec["FaxChannelBWMoney"]);

            beanitem. FaxNetBWMoney=  ConvertObjToFloat(rec["FaxNetBWMoney"]);

            beanitem. OtherBWMoney=  ConvertObjToFloat(rec["OtherBWMoney"]);
            beanitem. OtherColorMoney=  ConvertObjToFloat(rec["OtherColorMoney"]);

            beanitem.SetAllMoney();


            return beanitem;
        }

        public JobInformationCSVModel getUserJobInfomation(int UserID, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            JobInformationCSVModel bean = new JobInformationCSVModel();
            

            String MFPNumber = "";

            if ( !string.IsNullOrEmpty(SerialNumber) )
            {
                MFPNumber = " AND SerialNumber=" + ConvertStringToSQL(SerialNumber);
            }
            else
            {
                MFPNumber = "";
            }

            string selSql = "";
            if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
            {
                selSql = " SELECT SUM(JobInformation.SpendMoney)";
            }
            else
            {
                if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
                {
                    selSql = " SELECT SUM(JobInformation.DspNumber)";
                }
                else
                {
                    selSql = " SELECT SUM(JobInformation.Number)";
                }
            }

            StringBuilder sb = new StringBuilder("");
            sb.Append("SELECT ");
            sb.Append("  UserInfo.ID               AS UserId ");
            //sb.Append(" ,UserInfo.UserName         AS UserName ");
            //sb.Append(" ,GroupInfo.ID              AS GroupId ");
            //sb.Append(" ,GroupInfo.GroupName        AS GroupName ");
            //黑白复印
            sb.Append( " , ISNULL(( ");
            sb.Append( selSql );
            sb.Append( "	  FROM JobInformation" );
            sb.Append( "	  WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Copy_JobId.ToString());       
            sb.Append( "AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Copy_FunctionId1.ToString());  
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	 ), 0 ) AS CopyBWMoney");

            //彩色复印
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	  WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Copy_JobId.ToString());      
            sb.Append( "	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Copy_FunctionId2);   
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	 ), 0 ) AS CopyColorMoney");
            
        //黑白打印
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Print_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Print_FunctionId1);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS PrintBWMoney");

            //彩色打印
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND JobInformation.JobID = "+ UtilConst.ITEM_TITLE_Print_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = "+ UtilConst.ITEM_TITLE_Print_FunctionId2);
            sb.Append(  MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS PrintColorMoney");
            
            //黑白扫描
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND JobInformation.JobID = "+ UtilConst.ITEM_TITLE_Scan_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = "+ UtilConst.ITEM_TITLE_Scan_FunctionId1);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanBWMoney");

            //彩色扫描
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND JobInformation.JobID = "+ UtilConst.ITEM_TITLE_Scan_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = "+ UtilConst.ITEM_TITLE_Scan_FunctionId2);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanColorMoney");

              //传真
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND JobInformation.JobID = "+ UtilConst.ITEM_TITLE_Fax_JobId);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS FaxBWMoney");


            //黑白文件归档打印
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND  JobInformation.JobID = "+ UtilConst.ITEM_TITLE_DFPrint_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = "+ UtilConst.ITEM_TITLE_DFPrint_FunctionId1);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS DFPrintBWMoney");

            //彩色文件归档打印
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND  JobInformation.JobID = "+ UtilConst.ITEM_TITLE_DFPrint_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = "+ UtilConst.ITEM_TITLE_DFPrint_FunctionId2);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS DFPrintColorMoney");


             //扫描并保存黑白
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_ScanSave_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = "+ UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanSaveBWMoney");

            //彩色扫描并保存
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_ScanSave_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = "+ UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanSaveColorMoney");

           
            //黑白清单打印
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND  JobInformation.JobID = "+ UtilConst.ITEM_TITLE_ListPrint_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = "+ UtilConst.ITEM_TITLE_ListPrint_FunctionId1);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ListPrintBWMoney");

            //彩色清单打印
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append( "	   AND JobInformation.JobID = "+ UtilConst.ITEM_TITLE_ListPrint_JobId);
            sb.Append( "	   AND JobInformation.FunctionID = "+ UtilConst.ITEM_TITLE_ListPrint_FunctionId2);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ListPrintColorMoney");


            //传真2
            sb.Append( " , ISNULL((");
            sb.Append( selSql);
            sb.Append( "	  FROM JobInformation");
            sb.Append( "	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_FaxC2_JobId);
            sb.Append( MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS FaxChannelBWMoney");
            
            //网络传真
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_IntFax_JobId);
            sb.Append(MFPNumber);
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS FaxNetBWMoney");

            //其它黑白
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append("	   AND  JobInformation.JobID = " + UtilConst.ITEM_TITLE_Other_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Other_FunctionId1);
            sb.Append(MFPNumber);
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS OtherBWMoney");

            //其他彩色
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.UserID = UserInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Other_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Other_FunctionId2);
            sb.Append(MFPNumber);
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS OtherColorMoney");

            sb.Append( " FROM [UserInfo] LEFT JOIN");
            sb.Append( "  [GroupInfo] ON GroupInfo.ID = GroupID");
            sb.Append(" WHERE  UserInfo.ID = @UserID ");

            SqlParameter[] ParamList = null;
            ParamList = new SqlParameter[3];
            SqlParameter param1 = sqlHelper.CreateInParam("@STARTTIME", SqlDbType.DateTime, 14, START_TIME);
            SqlParameter param2 = sqlHelper.CreateInParam("@ENDTIME", SqlDbType.DateTime, 14, END_TIME);
            SqlParameter param3 = sqlHelper.CreateInParam("@UserID", SqlDbType.Int, 4, UserID);
            ParamList[0] = param1;
            ParamList[1] = param2;
            ParamList[2] = param3;

            String sql = sb.ToString();

            DataSet ds = this.sqlHelper.GetDataSet(sql, ParamList);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bean = BindCSVBean(dt.Rows[i]);
                    break;
                }
            }
            return bean;
        }
        public JobInformationCSVModel getGroupJobInfomation(int GroupID, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            JobInformationCSVModel bean = new JobInformationCSVModel();


            String MFPNumber = "";

            if (!string.IsNullOrEmpty(SerialNumber))
            {
                MFPNumber = " AND SerialNumber=" + ConvertStringToSQL(SerialNumber);
            }
            else
            {
                MFPNumber = "";
            }

            string selSql = "";
            if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
            {
                selSql = " SELECT SUM(JobInformation.SpendMoney)";
            }
            else
            {
                if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
                {
                    selSql = " SELECT SUM(JobInformation.DspNumber)";
                }
                else
                {
                    selSql = " SELECT SUM(JobInformation.Number)";
                }
            }

            StringBuilder sb = new StringBuilder("");
            sb.Append("SELECT ");
            sb.Append(" GroupInfo.ID              AS GroupId ");
            //黑白复印
            sb.Append(" , ISNULL(( ");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Copy_JobId.ToString());
            sb.Append("AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Copy_FunctionId1.ToString());
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	 ), 0 ) AS CopyBWMoney");

            //彩色复印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Copy_JobId.ToString());
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Copy_FunctionId2);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	 ), 0 ) AS CopyColorMoney");

            //黑白打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Print_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Print_FunctionId1);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS PrintBWMoney");

            //彩色打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Print_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Print_FunctionId2);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS PrintColorMoney");

            //黑白扫描
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Scan_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Scan_FunctionId1);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanBWMoney");

            //彩色扫描
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Scan_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Scan_FunctionId2);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanColorMoney");

            //传真
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Fax_JobId);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS FaxBWMoney");


            //黑白文件归档打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND  JobInformation.JobID = " + UtilConst.ITEM_TITLE_DFPrint_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_DFPrint_FunctionId1);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS DFPrintBWMoney");

            //彩色文件归档打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND  JobInformation.JobID = " + UtilConst.ITEM_TITLE_DFPrint_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_DFPrint_FunctionId2);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS DFPrintColorMoney");


            //扫描并保存黑白
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_ScanSave_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanSaveBWMoney");

            //彩色扫描并保存
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_ScanSave_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanSaveColorMoney");


            //黑白清单打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND  JobInformation.JobID = " + UtilConst.ITEM_TITLE_ListPrint_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_ListPrint_FunctionId1);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ListPrintBWMoney");

            //彩色清单打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_ListPrint_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_ListPrint_FunctionId2);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ListPrintColorMoney");


            //传真2
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_FaxC2_JobId);
            sb.Append(MFPNumber);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS FaxChannelBWMoney");

            //网络传真
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_IntFax_JobId);
            sb.Append(MFPNumber);
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS FaxNetBWMoney");

            //其它黑白
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND  JobInformation.JobID = " + UtilConst.ITEM_TITLE_Other_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Other_FunctionId1);
            sb.Append(MFPNumber);
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS OtherBWMoney");

            //其他彩色
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	 WHERE JobInformation.GroupID = GroupInfo.ID");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Other_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Other_FunctionId2);
            sb.Append(MFPNumber);
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS OtherColorMoney");

            sb.Append(" FROM [GroupInfo]");
            sb.Append(" WHERE  GroupInfo.ID = @GroupID ");

            SqlParameter[] ParamList = null;
            ParamList = new SqlParameter[3];
            SqlParameter param1 = sqlHelper.CreateInParam("@STARTTIME", SqlDbType.DateTime, 14, START_TIME);
            SqlParameter param2 = sqlHelper.CreateInParam("@ENDTIME", SqlDbType.DateTime, 14, END_TIME);
            SqlParameter param3 = sqlHelper.CreateInParam("@GroupID", SqlDbType.Int, 4, GroupID);
            ParamList[0] = param1;
            ParamList[1] = param2;
            ParamList[2] = param3;

            String sql = sb.ToString();

            DataSet ds = this.sqlHelper.GetDataSet(sql, ParamList);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bean = BindCSVBean(dt.Rows[i]);
                    break;
                }
            }
            return bean;
        }


        public JobInformationCSVModel getMFPJobInfomation(string SerialNumber, DateTime START_TIME, DateTime END_TIME, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            JobInformationCSVModel bean = new JobInformationCSVModel();


            string selSql = "";
            if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
            {
                selSql = " SELECT SUM(JobInformation.SpendMoney)";
            }
            else
            {
                if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
                {
                    selSql = " SELECT SUM(JobInformation.DspNumber)";
                }
                else
                {
                    selSql = " SELECT SUM(JobInformation.Number)";
                }
            }

            StringBuilder sb = new StringBuilder("");
            sb.Append("SELECT ");
            //黑白复印
            sb.Append(" A.SerialNumber as SerialNumber ");
            sb.Append(" , ISNULL(( ");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Copy_JobId.ToString());
            sb.Append("AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Copy_FunctionId1.ToString());
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	 ), 0 ) AS CopyBWMoney");

            //彩色复印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Copy_JobId.ToString());
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Copy_FunctionId2);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	 ), 0 ) AS CopyColorMoney");

            //黑白打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Print_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Print_FunctionId1);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS PrintBWMoney");

            //彩色打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Print_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Print_FunctionId2);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS PrintColorMoney");

            //黑白扫描
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Scan_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Scan_FunctionId1);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanBWMoney");

            //彩色扫描
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Scan_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Scan_FunctionId2);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanColorMoney");

            //传真
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Fax_JobId);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS FaxBWMoney");


            //黑白文件归档打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND  JobInformation.JobID = " + UtilConst.ITEM_TITLE_DFPrint_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_DFPrint_FunctionId1);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS DFPrintBWMoney");

            //彩色文件归档打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND  JobInformation.JobID = " + UtilConst.ITEM_TITLE_DFPrint_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_DFPrint_FunctionId2);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS DFPrintColorMoney");


            //扫描并保存黑白
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_ScanSave_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_ScanSave_FunctionId1);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanSaveBWMoney");

            //彩色扫描并保存
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_ScanSave_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_ScanSave_FunctionId2);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ScanSaveColorMoney");


            //黑白清单打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND  JobInformation.JobID = " + UtilConst.ITEM_TITLE_ListPrint_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_ListPrint_FunctionId1);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ListPrintBWMoney");

            //彩色清单打印
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_ListPrint_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_ListPrint_FunctionId2);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS ListPrintColorMoney");


            //传真2
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_FaxC2_JobId);
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append( "	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS FaxChannelBWMoney");

            //网络传真
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_IntFax_JobId);
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS FaxNetBWMoney");

            //其它黑白
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND  JobInformation.JobID = " + UtilConst.ITEM_TITLE_Other_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Other_FunctionId1);
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS OtherBWMoney");

            //其他彩色
            sb.Append(" , ISNULL((");
            sb.Append(selSql);
            sb.Append("	  FROM JobInformation");
            sb.Append("	  WHERE JobInformation.SerialNumber = A.SerialNumber");
            sb.Append("	   AND JobInformation.JobID = " + UtilConst.ITEM_TITLE_Other_JobId);
            sb.Append("	   AND JobInformation.FunctionID = " + UtilConst.ITEM_TITLE_Other_FunctionId2);
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20)  >= {1}");
            //sb.Append("	   AND SUBSTRING(CONVERT(CHAR(23), [Time], 21),0,20) <= {2}");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append("	),0) AS OtherColorMoney");

            sb.Append(" FROM [MFPInformation] A ");
            sb.Append(" WHERE  A.SerialNumber = @SerialNumber ");

            SqlParameter[] ParamList = null;
            ParamList = new SqlParameter[3];
            SqlParameter param1 = sqlHelper.CreateInParam("@STARTTIME", SqlDbType.DateTime, 14, START_TIME);
            SqlParameter param2 = sqlHelper.CreateInParam("@ENDTIME", SqlDbType.DateTime, 14, END_TIME);
            SqlParameter param3 = sqlHelper.CreateInParam("@SerialNumber", SqlDbType.NVarChar, 10, SerialNumber);
            ParamList[0] = param1;
            ParamList[1] = param2;
            ParamList[2] = param3;

            String sql = sb.ToString();

            DataSet ds = this.sqlHelper.GetDataSet(sql, ParamList);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bean = BindCSVBean(dt.Rows[i]);
                    break;
                }
            }
            return bean;
        }



        protected JobTypeInfoCSVModel BindJobTypeCSVBean(DataRow rec)
        {
            JobTypeInfoCSVModel beanitem = new JobTypeInfoCSVModel();

            beanitem.AllMoney = ConvertObjToFloat(rec["AllMoney"]);

            return beanitem;
        }

        public JobTypeInfoCSVModel getJobTypeCSVInfomation(int flg, string strIDList, int JobTypeID, DateTime START_TIME, DateTime END_TIME, string SerialNumber, int Dsp_Count_mode, int Dsp_A3_A4)
        {
            JobTypeInfoCSVModel bean = new JobTypeInfoCSVModel();

            String MFPNumber = "";

            if (!string.IsNullOrEmpty(SerialNumber))
            {
                MFPNumber = " AND SerialNumber=" + ConvertStringToSQL(SerialNumber);
            }
            else
            {
                MFPNumber = "";
            }

            string selSql = "";
            if (Dsp_Count_mode.Equals(UtilConst.CON_COUNT_MODE_MONEY))
            {
                selSql = " SELECT SUM(JobInformation.SpendMoney) AS AllMoney ";
            }
            else
            {
                if (Dsp_A3_A4.Equals(UtilConst.CON_DISP_A3_A3))
                {
                    selSql = " SELECT SUM(JobInformation.DspNumber) AS AllMoney ";
                }
                else
                {
                    selSql = " SELECT SUM(JobInformation.Number) AS AllMoney ";
                }
            }

            string searchText = "";
            if (flg == 1)
            {
                searchText = "   AND GroupID IN (" + strIDList + ") ";
            }
            else
            {
                searchText = "   AND UserID IN (" + strIDList + ") ";
            }
            StringBuilder sb = new StringBuilder("");
            sb.Append( selSql );
            sb.Append(" FROM [JobInformation]  ");
            sb.Append(" WHERE [JobInformation].JobID = @JobID  ");
            sb.Append("	   AND Time  >= @STARTTIME");
            sb.Append("	   AND Time  <= @ENDTIME");
            sb.Append(MFPNumber);
            sb.Append(searchText);


            SqlParameter[] ParamList = null;
            ParamList = new SqlParameter[3];
            SqlParameter param1 = sqlHelper.CreateInParam("@STARTTIME", SqlDbType.DateTime, 14, START_TIME);
            SqlParameter param2 = sqlHelper.CreateInParam("@ENDTIME", SqlDbType.DateTime, 14, END_TIME);
            SqlParameter param3 = sqlHelper.CreateInParam("@JobID", SqlDbType.Int, 4, JobTypeID);
            ParamList[0] = param1;
            ParamList[1] = param2;
            ParamList[2] = param3;

            String sql = sb.ToString();

            DataSet ds = this.sqlHelper.GetDataSet(sql, ParamList);
            DataTable dt = new DataTable();

            if (ds != null)
            {
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bean = BindJobTypeCSVBean(dt.Rows[i]);
                    break;
                }
            }
            return bean;
        }
        

    }
}