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
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

namespace SimpleEACommon
{

    public class INIClass
    {
        public string inipath;
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// INIClass
        /// </summary>
        /// <param name="INIPath">filepath</param>
        public INIClass(string INIPath)
        {
            inipath = INIPath;
        }

        /// <summary>
        /// Get Value
        /// </summary>
        /// <param name="Section">Item Name</param>
        /// <param name="Key">Key</param>
        public string GetValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(500);
            int i = GetPrivateProfileString(Section, Key, "", temp, 500, this.inipath);
            return temp.ToString();
        }

        /// <summary>
        /// SetValue
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public void SetValue(string Section, string Key, string val)
        {
            long i = WritePrivateProfileString(Section, Key, val, this.inipath);
        }
        /// <summary>
        /// isExist
        /// </summary>
        /// <returns>bool</returns>
        public bool isExist()
        {
            return File.Exists(inipath);
        }
    }
}