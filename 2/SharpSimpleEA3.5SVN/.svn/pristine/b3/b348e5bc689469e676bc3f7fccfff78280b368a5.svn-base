using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace FollowMEService
{

    public class ServiceCommon
    {

        public static Queue<string> logQueue = new Queue<string>();

        // Get AppConfig Data

        #region "ConnectionStrings For SimpleEA"
        /// <summary>
        /// ConnectionStrings For SimpleEA
        /// </summary>
        /// <Date>2010.06.07</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static String DBConnectionStrings
        {
            get
            {
                return ConfigurationManager.AppSettings["SimpleEAConnectionString"].ToString();
            }
        }

        #endregion

        #region InitRawRecvLog
        // create new log file if webconfig setting is
        // <add key="RawRecvLog" value="true" />
        /// <summary>
        /// for pull print http transfer
        /// </summary>
        /// <returns></returns>
        /// /// <Date>2012.02.17</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static bool InitRawRecvLog()
        {
            bool bRet = false;
            if (ConfigurationManager.AppSettings["RawRecvLog"].ToString().Trim() == "true")
            {
                if (!Directory.Exists(RawRecvLogPath))
                    Directory.CreateDirectory(RawRecvLogPath);
                bRet = true;
            }
            return bRet;
        }

        #endregion

        #region RawRecvLogPath

        /// <summary>
        ///  Raw Recv log path
        /// </summary>
        /// <returns></returns>
        /// /// <Date>2012.02.17</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static String RawRecvLogPath
        {
            get
            {
                //if (ConfigurationManager.AppSettings["RawRecvLogPath"].ToString().Contains("~"))
                //{
                //    return HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["RawRecvLogPath"].ToString());
                //}
                //else
                //    return ConfigurationManager.AppSettings["RawRecvLogPath"].ToString(); 
                return Environment.CurrentDirectory + ConfigurationManager.AppSettings["RawRecvLogPath"].ToString();
            }
        }

        #endregion

        #region RawRecvPort

        /// <summary>
        ///  Raw Recv port
        /// </summary>
        /// <returns></returns>
        /// /// <Date>2012.02.17</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static String RawRecvPort
        {
            get { return ConfigurationManager.AppSettings["RawRecvPort"].ToString(); }
        }

        #endregion

        #region MaxSize

        /// <summary>
        ///  Raw Recv port
        /// </summary>
        /// <returns></returns>
        /// /// <Date>2012.02.22</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static String MaxSize
        {
            get { return ConfigurationManager.AppSettings["MaxSize"].ToString(); }
        }

        #endregion

        #region AnalyseMethod

        /// <summary>
        ///  Raw Recv port
        /// </summary>
        /// <returns></returns>
        /// /// <Date>2012.02.22</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static String AnalyseMethod
        {
            get { return ConfigurationManager.AppSettings["ExtractMethod"].ToString();  }
        }

        #endregion

        #region ImportMethod

        /// <summary>
        ///  Raw Recv port
        /// </summary>
        /// <returns></returns>
        /// /// <Date>2012.02.22</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static String ImportMethod
        {
            get { return ConfigurationManager.AppSettings["ImportMethod"].ToString(); }
        }

        #endregion

        #region SplFileLocation

        /// <summary>
        ///  Raw Recv port
        /// </summary>
        /// <returns></returns>
        /// /// <Date>2012.02.22</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static String SplFileLocation
        {
            get { return ConfigurationManager.AppSettings["FileLocation"].ToString(); }
        }

        #endregion

        #region GetAppSettingString

        /// <summary>
        /// Get the application setting from WebConfig
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <Date>2012.02.23</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static string GetAppSettingString(string key)
        {
            string value = string.Empty;
            if (string.IsNullOrEmpty(key))
            {
                return value;
            }
            value = ConfigurationManager.AppSettings.Get(key);

            if (null == value)
            {
                return string.Empty;
            }

            return value;
        }

        #endregion


        // end Get Data

        // common method

        #region "SQL CONVERT(STRING)"
        /// <summary>
        /// SQL CONVERT(STRING)
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        /// <Date>2010.06.08</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static string ConvertStringToSQL(string strInput)
        {
            // 2010.11.16 Add By SES Jijianxiong Ver.1.1 Update ST
            // Add null
            if (string.IsNullOrEmpty(strInput))
            {
                return "NULL";
            }
            // 2010.11.16 Add By SES Jijianxiong Ver.1.1 Update ED
            strInput = strInput.Trim().Replace("'", "''");
            strInput = "'" + strInput + "'";
            return strInput;
        }
        #endregion

        #region "SQL CONVERT(INT)"
        /// <summary>
        /// SQL CONVERT(INT)
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        /// <Date>2010.06.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static string ConvertIntToSQL(string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                return ((IConvertible)Input).ToInt32(null).ToString();
            }
            return "NULL";
        }
        #endregion

        #region "SQL CONVERT(DATETIME)"
        /// <summary>
        /// SQL CONVERT(STRING)
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        /// <Date>2010.06.08</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static string ConvertDateToSQL(DateTime datInput)
        {
            string strOutPut = datInput.ToString("yyyy-MM-dd HH:mm:ss");
            strOutPut = "'" + strOutPut + "'";
            return strOutPut;
        }
        #endregion

        #region StringToByte

        /// <summary>
        /// StringToByte
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <Date>2012.02.23</Date>
        /// <Author>SLC Wei changye</Author>
        /// <Version>1.2</Version>
        public static string StringToByte(string str)
        {
            string value = string.Empty;
            if (string.IsNullOrEmpty(str))
            {
                return value;
            }

            UnicodeEncoding uni = new UnicodeEncoding();

            byte[] tmp = uni.GetBytes(str);
            StringBuilder sb = new StringBuilder();

            foreach (byte item in tmp)
            {
                sb.Append(item.ToString());
            }
            string folder = sb.ToString();

            return folder;
        }

        #endregion 

        // end common method

        // DB method

        #region "ExecuteDataTable"
        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// <Date>2011.01.19</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        internal static DataTable ExecuteDataTable(string sql)
        {
            DataTable data = new DataTable();

            using (SqlConnection con = new SqlConnection(DBConnectionStrings))
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

        // end DB method

    }
}
