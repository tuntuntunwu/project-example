#region Copyright SHARP Corporation
//	Copyright (c) 2011 SHARP CORPORATION. All rights reserved.
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
using System.IO;
using System.Text;

namespace SimpleEACommon
{
    public class CustomLog
    {
        // Log File Name;
        private const string FileName = "SimpleEALog";
        // Log File Extensions
        private const string FileExt = ".log";
        // All File Name
        private static string FileAllName = string.Empty;
        // All File Path
        private static string FileAllPath = string.Empty;
        // ClassName
        private static string ClassName = string.Empty;
        // FunctionName
        private static string FunctionName = string.Empty;

        public enum Level
        {
            DEBUG,
            INFO,
            PROCESS,
            WARNING,
            ERROR,
            NONE           
        }

        public enum Status 
        {
            BEGIN,
            END
        }


        #region "RecordLog"
        /// <summary>
        /// RecordLog
        /// </summary>
        /// <param name="strInformation"></param>
        /// <param name="UserLevel"></param>
        /// <Date>2011.01.18</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static void RecordLog(string strInformation, Level UserLevel)
        {
            // Get the File All Name with the DateTime.
            if (string.IsNullOrEmpty(FileAllName))
            {
                FileAllName = FileName + DateTime.Now.ToString("yyyyMMddHHmmss") + FileExt;
            }

            // Get the File All Path
            if (string.IsNullOrEmpty(FileAllPath))
            {
                //TODO: Check the path C:\Documents and Settings\<User>\Local Settings\Temp
                FileAllPath = Environment.GetEnvironmentVariable("TEMP");
                FileAllPath = Path.Combine(FileAllPath, FileAllName);
            }

            string strLog = string.Empty;
            // 01:30:12 127.0.0.1 GET /SimpleEA3/Default.aspx 500
            strLog = DateTime.Now.ToString("HH:mm:ss") + "   " + "::Simple EA::" + "   " + UserLevel.ToString() + ":" + strInformation;

            string _classname = string.Empty;
            if ( ! string.IsNullOrEmpty(ClassName) ) {
                _classname = ClassName;
            }

            string _functionname = string.Empty;
            if ( ! string.IsNullOrEmpty(FunctionName) ) {
                _functionname = FunctionName;
            }

            string _information = string.Empty;

            _information = _classname + " " + _functionname;
            _information = _information.Trim();
            if (! string.IsNullOrEmpty(_information.Trim())){
                _information =  _information + " ";
            }

            strLog = DateTime.Now.ToString("HH:mm:ss") + "   " + "::Simple EA::" + "   " + UserLevel.ToString() + ":" + _information + strInformation;

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(FileAllPath, true);
                sw.WriteLine(strLog);
            }
            catch
            {
                ;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
        #endregion

        #region "RecordProcessLog"
        /// <summary>
        /// RecordProcessLog
        /// </summary>
        /// <param name="_className"></param>
        /// <param name="_functionName"></param>
        /// <param name="_status"></param>
        /// <Date>2011.01.18</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static void RecordProcessLog(string _className, string _functionName, Status _status)
        {
            ClassName = _className;
            FunctionName = _functionName;

            RecordLog(_status.ToString(), Level.PROCESS);

            if (_status.Equals(Status.END))
            {
                ClassName = string.Empty;
                FunctionName = string.Empty;
            }
        }
        #endregion

    }
}
