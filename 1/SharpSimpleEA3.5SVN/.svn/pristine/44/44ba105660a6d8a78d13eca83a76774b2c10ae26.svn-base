
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Principal;
using System.Security.AccessControl;
using System.Web.Hosting;
using System.Web;
using CommonOSA;

public class Helper : BaseHelper
{
    public string[] tmpList = new string[3];

    // Create session information.
    public static bool Create(OsaRequestInfo req, string physicalApplicationPath, ref string errPage, out DeviceSession s)
    {
        s = new DeviceSession();
        return BaseHelper.Create(req, physicalApplicationPath, ref errPage, s);
    }

    // Get session information
    public static DeviceSession GetSession(HttpRequest request)
    {
        string deviceId = OsaRequestInfo.GetDeviceId(request);
        return (DeviceSession)BaseHelper.GetSession(deviceId);
    }

    // Set session information
    public static void SetSession(HttpRequest request, DeviceSession Session)
    {
        string deviceId = OsaRequestInfo.GetDeviceId(request);
        BaseHelper.SetSession(deviceId, Session);
    }

    public static SetJob.SetJobElements Copy(DeviceSession s)
    {
        SetJob.SetJobElements doc = ConvertToSetJobElements(s);
        return doc;
    }

    public static string CutString(string value, int length)
    {
        string ret = value;
        if (value.Length > length)
        {
            ret = value.Substring(0, length - 1) + "...";
        }
        return ret;
    }

    public static void AddPamphletCopy(JobSettableProp prop)
    {
        List<string> listSpMode = prop.GetMultiPropValue(OSACopy.PropName.SPECIAL_MODE.value);

        List<string> listNew = new List<string>();
        listNew.Add(OSACopy.PropValue.SPECIAL_MODE.PAMPHLET_COPY);

        foreach (string specialMode in listSpMode)
        {
            if (0 != string.Compare(specialMode, OSACopy.PropValue.SPECIAL_MODE.NONE))
            {
                listNew.Add(specialMode);
            }
        }
        prop.SetMultiPropValue(OSACopy.PropName.SPECIAL_MODE.value, listNew);

    }

    public static void RemovePamphletCopy(JobSettableProp prop)
    {
        List<string> listValue = prop.GetMultiPropValue(OSACopy.PropName.SPECIAL_MODE.value);
        if (listValue.Contains(OSACopy.PropValue.SPECIAL_MODE.PAMPHLET_COPY))
        {
            listValue.Remove(OSACopy.PropValue.SPECIAL_MODE.PAMPHLET_COPY);
            //--- None is set when there is no control that has been selected ---
            if (0 == listValue.Count)
            {
                listValue.Add(OSACopy.PropValue.SPECIAL_MODE.NONE);
            }
            //--- Set multiple states are selected ---
            prop.SetMultiPropValue(OSACopy.PropName.SPECIAL_MODE.value, listValue);
        }
    }

    public static void SaddleStitchClear(JobSettableProp prop)
    {
        prop.SetPropValue(OSACopy.PropName.SADDLE_STITCH_BINDING.value, OSACopy.PropValue.SADDLE_STITCH_BINDING.NONE);
        prop.SetPropValue(OSACopy.PropName.SADDLE_STITCH_ORIGINAL.value, prop.GetDefaultPropValue(OSACopy.PropName.SADDLE_STITCH_ORIGINAL.value));
        prop.SetPropValue(OSACopy.PropName.STAPLE.value, prop.GetDefaultPropValue(OSACopy.PropName.STAPLE.value));

        RemovePamphletCopy(prop);
    }

    public static void AddNoneToSaddleStitchBinding(JobSettableProp prop)
    {
        GetJob.Property propSaddleStitchBinding = (GetJob.Property)prop.GetProperty(OSACopy.PropName.SADDLE_STITCH_BINDING.value);
        if (propSaddleStitchBinding == null)
        {
            return;
        }
        ArrayList arrList = new ArrayList();
        arrList.Add(OSACopy.PropValue.SADDLE_STITCH_BINDING.NONE);
        for (int i = 0; i < propSaddleStitchBinding.allowedValueList.Length; i++)
        {
            arrList.Add(propSaddleStitchBinding.allowedValueList[i]);
        }
        propSaddleStitchBinding.allowedValueList = (string[])arrList.ToArray(typeof(string));
        propSaddleStitchBinding.value = OSACopy.PropValue.SADDLE_STITCH_BINDING.NONE;
    }

    public static void RemoveNoneFromSaddleStitchBinding(DeviceSession s, JobSettableProp prop)
    {
        GetJob.Property propSaddleStitchBinding = (GetJob.Property)prop.GetProperty(OSACopy.PropName.SADDLE_STITCH_BINDING.value);
        if (propSaddleStitchBinding == null)
        {
            return;
        }
        ArrayList arrList = new ArrayList();
        for (int i = 0; i < propSaddleStitchBinding.allowedValueList.Length; i++)
        {
            if (0 != string.Compare(propSaddleStitchBinding.allowedValueList[i], OSACopy.PropValue.SADDLE_STITCH_BINDING.NONE))
            {
                arrList.Add(propSaddleStitchBinding.allowedValueList[i]);
            }
        }
        propSaddleStitchBinding.allowedValueList = (string[])arrList.ToArray(typeof(string));
        propSaddleStitchBinding.value = s.originalSaddleStitchBinding;
    }

    public static void SaddleStitchInit(DeviceSession s, JobSettableProp prop)
    {
        s.originalSaddleStitchBinding = prop.GetDefaultPropValue(OSACopy.PropName.SADDLE_STITCH_BINDING.value);
        SaddleStitchClear(prop);
        AddNoneToSaddleStitchBinding(prop);
    }
}
