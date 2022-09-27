#region Copyright SHARP Corporation
//	Copyright (c) 2010 SHARP CORPORATION. All rights reserved.
//
//	SHARP Simple EA Application
//
//	This software is protected under the Copyright Laws of the United States,
//	Title 17 USC, by the Berne Convention, and the copyright laws of other
//	countries.
//
//	THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS 
//	OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//	OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//
//==============================================================================
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Text.RegularExpressions;
using CustomAction.Properties;
using SesMiddleware;
using System.Data;
using SimpleEACommon;

namespace CustomAction
{
    class SqlCommon
    {

        // Class Name.
        internal const string ClassName = "CustomAction";

        internal const string DefaultConnectionString = "Integrated Security=SSPI; Data Source=(local)\\SQLEXPRESS";
      //  internal const string DefaultConnectionString = "Data Source=202.120.87.237\\SQLEXPRESS;Initial Catalog=SimpleEA;User ID=sa;Password=sharp";
        internal const string ParmConnectionString = "Integrated Security=SSPI; Data Source={0};";
        private static string _connectionString = string.Empty;

        #region "ConnectionString"
        /// <summary>
        /// ConnectionString
        /// </summary>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    return DefaultConnectionString;
                }
                else
                {
                    return _connectionString;
                }
            }
            set
            {
                string servername = "";
                if (string.IsNullOrEmpty(value.Trim()))
                {
                   servername = "(local)\\SQLEXPRESS";
                  //  servername = "202.120.87.237\\SQLEXPRESS";
                }
                else
                {
                    servername = value.Trim();
                }

                _connectionString = string.Format(ParmConnectionString, servername);
            }
        }
        #endregion

        #region "InitConnection"
        /// <summary>
        /// InitConnection
        /// </summary>
        /// <returns></returns>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static bool InitConnection()
        {
            string FunctionName = "InitConnection";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            try
            {
                string query = "SELECT TOP 1 * FROM sys.objects";

                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Connection = new SqlConnection(ConnectionString);

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            finally
            {
                // --> Log
                CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            }
        }
        #endregion

        #region "Is Special DB Exist"
        /// <summary>
        /// Is Special DB Exist
        /// </summary>
        /// <param name="strDBName"></param>
        /// <returns></returns>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static bool IsSpecailDbExist(string strDBName)
        {
            string FunctionName = "IsSpecailDbExist";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            // Create the sqlConnection.
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                // Connection the Sql Server.
                ServerConnection serverConnection = new ServerConnection(sqlConnection);

                // Open the Server.
                Server server = new Server(serverConnection);

                foreach (Database db in server.Databases)
                {
                    if (db.Name.Trim().ToLower().Equals(strDBName.Trim().ToLower()))
                    {
                        return true;
                    }
                }
            }
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
            return false;
        }
        #endregion

        #region "Install DataBase"
        /// <summary>
        /// Install DataBase
        /// </summary>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static void InstallDatabase()
        {
            string FunctionName = "InstallDatabase";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            querySql(Resources.Create_database);
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        }
        #endregion

        #region "Update DateBase"
        /// <summary>
        /// Update DateBase
        /// </summary>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static void UpdateDatabase()
        {
            string FunctionName = "UpdateDatabase";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            querySql(Resources.Update_database);
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        }
        #endregion

        #region "run sql"
        /// <summary>
        /// run sql
        /// </summary>
        /// <param name="txtSQL"></param>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        internal static void querySql(string txtSQL)
        {
            string FunctionName = "querySql";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);
            string[] SqlLine;
            Regex regex = new Regex("^GO", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            string password = new Random().Next(1000000000).ToString();

            SqlLine = regex.Split(txtSQL);

            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = new SqlConnection(ConnectionString);
                cmd.Connection.Open();

                foreach (string line in SqlLine)
                {
                    if (line.Length > 0)
                    {
                        cmd.CommandText = line;
                        cmd.ExecuteNonQuery();
                    }
                }

                string[] connStringParts = ConnectionString.Split(';');

            }
            finally
            {
                cmd.Connection.Close();
            }
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        }
        #endregion

        #region "Update UserInfo.xml File"
        /// <summary>
        /// Update UserInfo.xml File
        /// </summary>
        /// <param name="filepath"></param>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static void UpdateUserXml(string filepath)
        {
            string FunctionName = "CreateXml";
            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            string strSql = " SELECT "
                + "       [LoginName]"
                + "      ,[Password]"
                + "      ,[ICCardID]"
                + "  FROM [SimpleEA].[dbo].[UserInfo]";

            // IC Card ID
            string strICCardID = string.Empty;
            // Login Name
            string strLoginName = string.Empty;
            // Password
            string strPassword = string.Empty;

            DataTable data = ExecuteDataTable(strSql);

            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    // Get the IC Card ID
                    strICCardID = row["ICCardID"].ToString().Trim();
                    // Get the Login Name.
                    strLoginName = row["LoginName"].ToString().Trim();
                    // Get the Password.
                    strPassword = row["Password"].ToString().Trim();

                    // While the IC Card ID is null.
                    if ((!string.IsNullOrEmpty(strICCardID)) && (!"admin".Equals(strLoginName)))
                    {
                        int returnVal = ICCardData.AddICCardInfo(strICCardID, strLoginName, strPassword, filepath);
                        if (returnVal != 0)
                        {
                            throw new Exception("Error on Add IccardInfor");
                        }
                    }
                }
            }

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

        }
        #endregion

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

            using (SqlConnection con = new SqlConnection(ConnectionString))
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

    }
}
