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

namespace PrintCopyService
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

    }
}
