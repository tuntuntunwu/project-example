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
using System.Collections.Generic;
using System.Text;
using System.Security.AccessControl;
using System.Security.Principal;

namespace SimpleEACommon
{
    public class CustomFileSecurity
    {
        // Class Name.
        internal const string ClassName = "CustomAction";
       
        #region "Directory Information AccessControl"
        /// <summary>
        /// Directory Information AccessControl
        /// </summary>
        /// <param name="path"></param>
        /// <param name="right"></param>
        /// <param name="access"></param>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static void DirAccessControl(string path, FileSystemRights right, AccessControlType access)
        {
            string FunctionName = "DirAccessControl";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);


            DirectoryInfo dirinfo = new DirectoryInfo(path);
            if (!dirinfo.Exists)
            {
                return;
            }

            FileInfo[] finfo = dirinfo.GetFiles();

            // Get NTAccount.
            string user = GetAspNetUser;
            NTAccount account = new NTAccount(user);
            // Get the rule.
            FileSystemAccessRule rule = new FileSystemAccessRule(account, right, access);

            foreach (FileInfo file in finfo)
            {
                SetRule(file, rule);
            }

            // --> Log
            CustomLog.RecordLog(string.Format("Added Access Control for the {0}.",user ), CustomLog.Level.DEBUG);

            // For the IIS 7.0 and above, need to allow anonynous access to the folder / files with the Internet Guest Account.
            int intIISVersion;
            IIServicesVersion.GetIISServerType(out intIISVersion);
            if (intIISVersion >= 7)
            {
                // IIS_IUSRS
                user = GetIIS_IUSRS;
                NTAccount accountIIS_IUSRS = new NTAccount(user);
                // Get the rule
                FileSystemAccessRule ruleIIS_IUSRS = new FileSystemAccessRule(accountIIS_IUSRS, right, access);
                foreach (FileInfo file in finfo)
                {
                    SetRule(file, ruleIIS_IUSRS);
                }
                // --> Log
                CustomLog.RecordLog(string.Format("Added Access Control for the {0}.", user), CustomLog.Level.DEBUG);
            }


            // Get Access Control.
            DirectorySecurity dirsecurity = dirinfo.GetAccessControl();
            // Add Access Control.
            dirsecurity.AddAccessRule(rule);
            // Set the Access Control.
            dirinfo.SetAccessControl(dirsecurity);

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

        }
        #endregion

        #region "File Information AccessControl"
        /// <summary>
        /// File Information AccessControl
        /// </summary>
        /// <param name="path"></param>
        /// <param name="right"></param>
        /// <param name="access"></param>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static void FileAccessControl(string path, FileSystemRights right, AccessControlType access)
        {
            string FunctionName = "FileAccessControl";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);


            FileInfo fs = new FileInfo(path);
            if (!fs.Exists)
            {
                return;
            }

            string user = GetAspNetUser;
            NTAccount account = new NTAccount(user);

            // Get the rule.
            FileSystemAccessRule rule = new FileSystemAccessRule(account, right, access);

            SetRule(fs, rule);

            // --> Log
            CustomLog.RecordLog(string.Format("Added Access Control for the {0}.", user), CustomLog.Level.DEBUG);


            // For the IIS 7.0 and above, need to allow anonynous access to the folder / files with the Internet Guest Account.
            int intIISVersion;
            IIServicesVersion.GetIISServerType(out intIISVersion);
            if (intIISVersion >= 7)
            {
                user = GetIIS_IUSRS;

                // IIS_IUSRS
                NTAccount accountIIS_IUSRS = new NTAccount(user);
                // Get the rule
                FileSystemAccessRule ruleIIS_IUSRS = new FileSystemAccessRule(accountIIS_IUSRS, right, access);
                SetRule(fs, ruleIIS_IUSRS);
                // --> Log
                CustomLog.RecordLog(string.Format("Added Access Control for the {0}.", user), CustomLog.Level.DEBUG);
            }


            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);

        }
        #endregion

        #region "Set rule on the file"
        /// <summary>
        /// Set rule on the file
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="rule"></param>
        /// <Date>2011.01.14</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        internal static void SetRule(FileInfo fs, FileSystemAccessRule rule)
        {
            string FunctionName = "SetRule";

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.BEGIN);

            // Get Access Control.
            FileSecurity dirsecurity = fs.GetAccessControl();
            // --> Log
            CustomLog.RecordLog("Get Access Control.", CustomLog.Level.DEBUG);

            // Add Access Control.
            dirsecurity.AddAccessRule(rule);
            // --> Log
            CustomLog.RecordLog("Add Access Control.", CustomLog.Level.DEBUG);

            // Set the Access Control.
            fs.SetAccessControl(dirsecurity);
            // --> Log
            CustomLog.RecordLog("Set Access Control.", CustomLog.Level.DEBUG);

            // --> Log
            CustomLog.RecordProcessLog(ClassName, FunctionName, CustomLog.Status.END);
        }
        #endregion

        #region "Get AspNet User"
        /// <summary>
        /// Get AspNet User
        /// </summary>
        /// <Date>2011.01.18</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        internal static string GetAspNetUser
        {
            get
            {
                string user = string.Empty;
                // IIS:
                // Before the IIS Version 6, the Asp.Net User is "ASPNET"
                // For the IIS Version 6 or latert, the Asp.Net User is "NETWORK SERVICE"
                int intIISVersion;
                IIServicesVersion.GetIISServerType(out intIISVersion);
                if (intIISVersion < 6)
                {
                    user = string.Format("{0}\\ASPNET", Environment.MachineName);
                }
                else
                {
                    user = "NETWORK SERVICE";
                }
                return user;
            }
        }
        #endregion

        #region "GetIIS_IUSRS"
        /// <summary>
        /// GetIIS_IUSRS
        /// </summary>
        /// <Date>2011.01.18</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        internal static string GetIIS_IUSRS
        {
            get
            {
                string user = string.Empty;
                // IIS_IUSRS
                user = "IIS_IUSRS";
                return user;
            }
        }
        #endregion


    }
}
