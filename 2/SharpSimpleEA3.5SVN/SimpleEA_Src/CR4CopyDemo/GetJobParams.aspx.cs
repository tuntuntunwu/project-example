
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
using CommonOSA;

public partial class GetJobParams : CommonOSA.BasePage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string key = Request.Params["key"];
        List<string> listSpMode;
        //--- Session is restored from file ---
        Helper.Load(key);
        DeviceSession s = (DeviceSession)CommonOSA.BaseHelper.GetSession(key);

        if (null == prop)
        {
            prop = new JobSettableProp(s);
        }
        bool pamphletRem = false;
        bool saddlestitchRem = false;
        if (s.SaddleStitchFinisherExist == true)
        {
            string strStaple = prop.GetPropValue(OSACopy.PropName.STAPLE.value);
            if (0 == string.Compare(strStaple, OSACopy.PropValue.STAPLE.SADDLE_STITCH))
            {
                listSpMode = prop.GetMultiPropValue(OSACopy.PropName.SPECIAL_MODE.value);

                string strSaddleStitchBinding = prop.GetPropValue(OSACopy.PropName.SADDLE_STITCH_BINDING.value);
                if (0 == string.Compare(strSaddleStitchBinding, OSACopy.PropValue.SADDLE_STITCH_BINDING.NONE))
                {
                    if (listSpMode.Contains(OSACopy.PropValue.SPECIAL_MODE.PAMPHLET_COPY))
                    {
                        // remove pamphlet copy
                        listSpMode.Remove(OSACopy.PropValue.SPECIAL_MODE.PAMPHLET_COPY);
                        if (0 == listSpMode.Count)
                        {
                            listSpMode.Add(OSACopy.PropValue.SPECIAL_MODE.NONE);
                        }
                        prop.SetMultiPropValue(OSACopy.PropName.SPECIAL_MODE.value, listSpMode);
                        pamphletRem = true;
                    }
                }
                else
                {
                    if (!listSpMode.Contains(OSACopy.PropValue.SPECIAL_MODE.PAMPHLET_COPY))
                    {
                        Helper.AddPamphletCopy(prop);
                    }
                    Helper.RemoveNoneFromSaddleStitchBinding(s, prop);
                    saddlestitchRem = true;
                }
            }
            else
            {
                if (s.OSAVersion >= 4.5)
                {
                    prop.SetPropValue(OSACopy.PropName.SADDLE_STITCH_BINDING.value, OSACopy.PropValue.SADDLE_STITCH_BINDING.NONE);
                    prop.SetPropValue(OSACopy.PropName.SADDLE_STITCH_ORIGINAL.value, prop.GetDefaultPropValue(OSACopy.PropName.SADDLE_STITCH_ORIGINAL.value));
                }

                Helper.RemovePamphletCopy(prop);
            }
        }


        //--- Get parameter for copy execution ---
        SetJob.SetJobElements xmlDocDsc = Helper.Copy(s);

        if (s.SaddleStitchFinisherExist == true)
        {
            if (pamphletRem)
            {
                Helper.AddPamphletCopy(prop);
            }
            if (saddlestitchRem)
            {
                Helper.AddNoneToSaddleStitchBinding(prop);
            }
        }

        //--- Save session information to file ---
        Helper.Save(key);

        //--- Serialize to XML format ---
        string SerialObject = Helper.Serialize(xmlDocDsc);
        //--- Response XML ---
        Response.Write(SerialObject);
        Response.End();
    }
}
