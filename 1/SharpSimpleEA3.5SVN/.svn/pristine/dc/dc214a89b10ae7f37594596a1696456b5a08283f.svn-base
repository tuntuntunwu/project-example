
#region Copyright SHARP Corporation
//	Copyright (c) 2010-2014 SHARP CORPORATION. All rights reserved.
//
//	Extended Sharp OSA SDK
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
using System.Web.UI;

namespace CommonOSA
{
    public class JobSettableProp : Page
    {
        //--- MFP information ---
        private GetJob.Xmldocout xmlDocDsc = null;
        //--- MFP execution parameter set on screen ---
        private Dictionary<string, List<string>> currentData = null;

        //--- Constructor ---
        public JobSettableProp(BaseDeviceSession session)
        {
            if (null == session.xmlDocDsc)
            {
                return;
            }
            if (null == xmlDocDsc)
            {
                xmlDocDsc = session.xmlDocDsc;
            }

            if (null == session.currentData)
            {
                return;
            }
            if (null == currentData)
            {
                currentData = session.currentData;
            }
        }

        //--- Get parameter list from MFP information ---
        public object[] GetPropList(string strSysname)
        {
            if (string.IsNullOrEmpty(strSysname))
            {
                return null;
            }
            if ((null == this.xmlDocDsc) || (null == this.xmlDocDsc.complex[0].property))
            {
                return null;
            }

            GetJob.Property[] ret = Array.FindAll(this.xmlDocDsc.complex[0].property, delegate(GetJob.Property pro) { return pro.sysname == strSysname; });

            if (1 == ret.Length)
            {
                switch (ret[0].isType)
                {
                    case CommonConst.isType.typeList:
                        return ret[0].allowedValueList;
                    case CommonConst.isType.typeString:
                    case CommonConst.isType.typeInteger:
                        return ret[0].appInfo;
                    default:
                        break;
                }
            }

            return null;
        }
        //--- Get property from MFP information ---
        public object GetProperty(string strSysname)
        {
            if (string.IsNullOrEmpty(strSysname))
            {
                return null;
            }
            if ((null == this.xmlDocDsc) || (null == this.xmlDocDsc.complex[0].property))
            {
                return null;
            }

            GetJob.Property[] ret = Array.FindAll(this.xmlDocDsc.complex[0].property, delegate(GetJob.Property pro) { return pro.sysname == strSysname; });

            if (1 == ret.Length)
            {
                switch (ret[0].isType)
                {
                    case CommonConst.isType.typeList:
                    case CommonConst.isType.typeString:
                    case CommonConst.isType.typeInteger:
                        return ret[0];
                    default:
                        break;
                }
            }

            return null;
        }

        // Get parameter value from MFP information
        public string GetDefaultPropValue(string strSysname)
        {

            if ((null == this.xmlDocDsc) || (null == this.xmlDocDsc.complex[0].property))
            {
                return null;
            }

            GetJob.Property[] ret = Array.FindAll(this.xmlDocDsc.complex[0].property, delegate(GetJob.Property pro) { return pro.sysname == strSysname; });
            if (1 == ret.Length)
            {
                return ret[0].value;
            }
            
            return null;
        }

        //--- Get execution parameter value ---
        public string GetPropValue(string strSysname)
        {

            if ((null == this.currentData) ||  !this.currentData.ContainsKey(strSysname) || (null == this.currentData[strSysname]))
            {
                return null;
            }
 
            if (1 == this.currentData[strSysname].Count)
            {
                return currentData[strSysname][0];
            }
            
            return null;
        }

        //--- Get execution parameter value (specified multiple) ---
        public List<string> GetMultiPropValue(string strSysname)
        {

            if ((null == this.currentData) || !this.currentData.ContainsKey(strSysname) || (null == this.currentData[strSysname]))
            {
                return null;
            }

            if (this.currentData[strSysname].Count > 0)
            {
                return currentData[strSysname];
            }

            return null;
        }

        //--- Set execution parameter value ---
        public void SetPropValue(string strSysname, string strValue)
        {
            if ((null == this.currentData) || (!this.currentData.ContainsKey(strSysname)))
            {
                return;
            }

            if (1 == this.currentData[strSysname].Count)
            {
                this.currentData[strSysname].Clear();
                this.currentData[strSysname].Add(strValue);
            }
        }

        //--- Set execution parameter value (specified multiple) ---
        public void SetMultiPropValue(string strSysname, List<string> listValue)
        {
            if ((null == this.currentData) || !this.currentData.ContainsKey(strSysname) || (null == this.currentData[strSysname]))
            {
                return;
            }

            this.currentData[strSysname] = listValue;
        }

        //--- Localization ---
        public string GetResourcePropSetttingVal(CommonOSA.PropNameValue propkey)
        {
            return GetResourcePropVal(propkey, GetPropValue(propkey.value));
        }

        //--- Localization ---
        public string GetResourcePropVal(CommonOSA.PropNameValue propkey, string proVal)
        {
            string strPropSrckey = string.Empty;
            string strResult = string.Empty;
            if (null == proVal)
            {
                proVal = string.Empty;
            }
            strPropSrckey = propkey.displyName + "_" + proVal.Replace("/", "_");

            try
            {
                strResult = GetGlobalResourceObject("OSAStrings", strPropSrckey).ToString();
            }
            catch (NullReferenceException)
            {
                strResult = proVal;
            }

            return (string.Empty == strResult) ? proVal : strResult;
        }
    }
}
