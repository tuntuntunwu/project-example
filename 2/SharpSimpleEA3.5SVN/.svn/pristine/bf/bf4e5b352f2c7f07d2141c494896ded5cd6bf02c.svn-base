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
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using Microsoft.Win32;

namespace SimpleEACommon
{
    public class IIServicesVersion
    {
        internal const string IISRegKeyName = "Software\\Microsoft\\InetStp";
        internal const string IISRegKeyValue = "MajorVersion";

        #region "Get IIS Server Type"
        /// <summary>
        /// Get IIS Server Type
        /// </summary>
        /// <returns></returns>
        /// <Date>2011.01.18</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        public static bool GetIISServerType(out int regValue)
        {
            if (GetRegistryValue(RegistryHive.LocalMachine, IISRegKeyName, IISRegKeyValue, RegistryValueKind.DWord, out regValue))
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }
        #endregion

        #region "Get Registry Value"
        /// <summary>
        /// Get Registry Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hive"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="kind"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <Date>2011.01.18</Date>
        /// <Author>SES Ji JianXiong</Author>
        /// <Version>0.01</Version>
        private static bool GetRegistryValue<T>(RegistryHive hive, string key, string value, RegistryValueKind kind, out T data)
        {
            bool success = false;
            data = default(T);

            using (RegistryKey baseKey = RegistryKey.OpenRemoteBaseKey(hive, String.Empty))
            {
                if (baseKey != null)
                {
                    using (RegistryKey registryKey = baseKey.OpenSubKey(key, RegistryKeyPermissionCheck.ReadSubTree))
                    {
                        if (registryKey != null)
                        {
                            try
                            {
                                // If the key was opened, try to retrieve the value.
                                RegistryValueKind kindFound = registryKey.GetValueKind(value);
                                if (kindFound == kind)
                                {
                                    object regValue = registryKey.GetValue(value, null);
                                    if (regValue != null)
                                    {
                                        data = (T)Convert.ChangeType(regValue, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
                                        success = true;
                                    }
                                }
                            }
                            catch (System.IO.IOException)
                            {
                                // The registry value doesn't exist. Since the
                                // value doesn't exist we have to assume that
                                // the component isn't installed and return
                                // false and leave the data param as the
                                // default value.
                            }
                        }
                    }
                }
            }
            return success;
        }

        #endregion
    }

}
