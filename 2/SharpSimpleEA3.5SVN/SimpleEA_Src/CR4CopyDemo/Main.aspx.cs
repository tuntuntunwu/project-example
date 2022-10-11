
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
using System.Web.UI.WebControls;
using CommonOSA;

public partial class Main : CommonOSA.BasePage
{
    protected DeviceSession currentSession = null;

    protected string min = string.Empty;
    protected string max = string.Empty;

    protected string strRatio = string.Empty;

    protected string strFeederLabel = string.Empty;

    protected string strCopiesVal = string.Empty;
    protected string defCopiesVal = string.Empty;

    protected string strColorModeVal = string.Empty;
    protected string defColorModeVal = string.Empty;

    protected string strColorSelectVal = string.Empty;
    protected string defColorSelectVal = string.Empty;

    protected string strColorReplaceVal = string.Empty;
    protected string defColorReplaceVal = string.Empty;

    protected string strPreviewVal = string.Empty;
    protected string defPreviewVal = string.Empty;

    protected string deviceId = string.Empty;
    protected string requestExecuteJobURL = string.Empty;
    protected string requestExecuteJobParamsURL = string.Empty;

    protected string strCollate = string.Empty;

    protected int setJobCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        id_Label_Title.Text = Resources.OSAStrings.TITLE_LABEL_MAIN;
        strRatio = Resources.OSAStrings.MAIN_BTN_RATIO;

        if (null == prop)
        {
            Helper.Load(Request);
            currentSession = Helper.GetSession(Request);
            if (null == currentSession)
            {
                return; // Unrecoverable
            }
            prop = new JobSettableProp(currentSession);

prop.SetPropValue("Filing", "main");
        prop.SetPropValue("PublicPDF", "true");
        prop.SetPropValue("FileName", "COPY_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            activeStyleSheet.Text = "<link rel='stylesheet' type='text/css' href='" + currentSession.cssFile + ".css'>";
        }

        deviceId = OsaRequestInfo.GetDeviceId(Request);
        requestExecuteJobParamsURL = currentSession.ExecuteJobURL("ExecuteJobCallback", deviceId);
        requestExecuteJobURL = currentSession.ExecuteJobURL("ExecuteJobCallback");

        SetJob.SetJobElements doc = Helper.ConvertToSetJobElements(currentSession);
        setJobCount = 0;

        foreach (SetJob.Property pro in doc.complex[0].property)
        {
            if (OSACopy.PropName.COPIES == pro.sysname)
            {
                continue;
            }
            if (OSACopy.PropName.PREVIEW == pro.sysname)
            {
                continue;
            }
            if (OSACopy.PropName.COLOR_MODE.value == pro.sysname)
            {
                continue;
            }
            if (OSACopy.PropName.COLOR_SELECT.value == pro.sysname)
            {
                continue;
            }
            if (OSACopy.PropName.COLOR_REPLACE.value == pro.sysname)
            {
                continue;
            }
            setJobCount++;
            break;
        }

        #region --- Copies ---
        //--- Copies ---
        GetJob.PropertyAppInfo[] copiesVals = (GetJob.PropertyAppInfo[])prop.GetPropList(OSACopy.PropName.COPIES);
        foreach (GetJob.PropertyAppInfo copiesVal in copiesVals)
        {
            switch (copiesVal.name)
            {
                case OSACopy.PropValue.COPIES.MIN_VALUE:
                    min = copiesVal.Value;
                    break;
                case OSACopy.PropValue.COPIES.MAX_VALUE:
                    max = copiesVal.Value;
                    break;
                default:
                    break;
            }
        }
        strCopiesVal = prop.GetPropValue(OSACopy.PropName.COPIES);
        defCopiesVal = prop.GetDefaultPropValue(OSACopy.PropName.COPIES);
        #endregion

        #region --- Input Tray ---
        //--- Input Tray ---
        if (string.IsNullOrEmpty(currentSession.originalInputTrayLabel))
        {
            currentSession.originalInputTrayLabel = prop.GetPropValue(OSACopy.PropName.INPUT_TRAY.value);
        }
        currentSession.originalInputTrayLabel = prop.GetPropValue(OSACopy.PropName.INPUT_TRAY.value);
        //switch (currentSession.originalInputTrayLabel)
        //{
        //    case "auto":
        //        id_InputTray.Src = "~/Images/01_AUTO.png";
        //        break;
        //    case "bypass":
        //        id_InputTray.Src = "~/Images/01_BYPASS.png";
        //        break;
        //    case "tray1":
        //        id_InputTray.Src = "~/Images/01_P1.png";
        //        break;
        //    case "tray2":
        //        id_InputTray.Src = "~/Images/01_P2.png";
        //        break;
        //    case "tray3":
        //        id_InputTray.Src = "~/Images/01_P3.png";
        //        break;
        //    case "tray4":
        //        id_InputTray.Src = "~/Images/01_P4.png";
        //        break;
        //    case "lcc":
        //        id_InputTray.Src = "~/Images/01_P5.png";
        //        break;
        //    case "lcc2":
        //        id_InputTray.Src = "~/Images/01_P6.png";
        //        break;
        //    case "lcc3":
        //        id_InputTray.Src = "~/Images/01_P7.png";
        //        break;
        //    case "lcc4":
        //        id_InputTray.Src = "~/Images/01_P8.png";
        //        break;
        //    default:
        //        id_InputTray.Src = "~/Images/01_AUTO.png";
        //        break;
        //}
        switch (currentSession.originalInputTrayLabel)
        {
            case "auto":
                id_InputTray.ImageUrl = "~/Images/01_AUTO.png";
                break;
            case "bypass":
                id_InputTray.ImageUrl = "~/Images/01_BYPASS.png";
                break;
            case "tray1":
                id_InputTray.ImageUrl = "~/Images/01_P1.png";
                break;
            case "tray2":
                id_InputTray.ImageUrl = "~/Images/01_P2.png";
                break;
            case "tray3":
                id_InputTray.ImageUrl = "~/Images/01_P3.png";
                break;
            case "tray4":
                id_InputTray.ImageUrl = "~/Images/01_P4.png";
                break;
            case "lcc":
                id_InputTray.ImageUrl = "~/Images/01_P5.png";
                break;
            case "lcc2":
                id_InputTray.ImageUrl = "~/Images/01_P6.png";
                break;
            case "lcc3":
                id_InputTray.ImageUrl = "~/Images/01_P7.png";
                break;
            case "lcc4":
                id_InputTray.ImageUrl = "~/Images/01_P8.png";
                break;
            default:
                id_InputTray.ImageUrl = "~/Images/01_AUTO.png";
                break;
        }

        #endregion

        #region --- Single & Double ---
        if (string.IsNullOrEmpty(currentSession.originalSingleDoubleLabel))
        {
            currentSession.originalSingleDoubleLabel = prop.GetPropValue(OSACopy.PropName.DUPLEX_MODE.value);
        }
        currentSession.originalSingleDoubleLabel = prop.GetPropValue(OSACopy.PropName.DUPLEX_MODE.value);
        //switch (currentSession.originalSingleDoubleLabel)
        //{
        //    case "simplex_to_simplex":
        //        id_SingleDouble.Src = "~/Images/02_11.png";
        //        break;
        //    case "simplex_to_duplex":
        //        id_SingleDouble.Src = "~/Images/02_12.png";
        //        break;
        //    case "duplex_to_simplex":
        //        id_SingleDouble.Src = "~/Images/02_21.png";
        //        break;
        //    case "duplex_to_duplex":
        //        id_SingleDouble.Src = "~/Images/02_22.png";
        //        break;
        //    default:
        //        id_SingleDouble.Src = "~/Images/02_11.png";
        //        break;
        //}
        switch (currentSession.originalSingleDoubleLabel)
        {
            case "simplex_to_simplex":
                id_SingleDouble.ImageUrl = "~/Images/02_11.png";
                break;
            case "simplex_to_duplex":
                id_SingleDouble.ImageUrl = "~/Images/02_12.png";
                break;
            case "duplex_to_simplex":
                id_SingleDouble.ImageUrl = "~/Images/02_21.png";
                break;
            case "duplex_to_duplex":
                id_SingleDouble.ImageUrl = "~/Images/02_22.png";
                break;
            default:
                id_SingleDouble.ImageUrl = "~/Images/02_11.png";
                break;
        }

        #endregion

        #region --- Ratio% ---
        if (string.IsNullOrEmpty(currentSession.originalRatioLabel))
        {
            currentSession.originalRatioLabel = prop.GetPropValue(OSACopy.PropName.COPYRATIO);
        }
        currentSession.originalRatioLabel = prop.GetPropValue(OSACopy.PropName.COPYRATIO);

        strRatio = currentSession.originalRatioLabel + "%";

        #endregion

        #region --- Exposure ---
        if (string.IsNullOrEmpty(currentSession.originalExposureLabel))
        {
            currentSession.originalExposureLabel = prop.GetPropValue(OSACopy.PropName.EXPOSURE_MODE.value);
        }
        currentSession.originalExposureLabel = prop.GetPropValue(OSACopy.PropName.EXPOSURE_MODE.value);
        currentSession.originalExposureLVLLabel = prop.GetPropValue(OSACopy.PropName.EXPOSURE_LEVEL.value);
        //switch (currentSession.originalExposureLVLLabel)
        //{
        //    case "0":
        //        id_Exposure_Level.Src = "~/Images/04_AUTO.png";
        //        break;
        //    case "1":
        //        id_Exposure_Level.Src = "~/Images/04_1.0.png";
        //        break;
        //    case "2":
        //        id_Exposure_Level.Src = "~/Images/04_1.5.png";
        //        break;
        //    case "3":
        //        id_Exposure_Level.Src = "~/Images/04_2.0.png";
        //        break;
        //    case "4":
        //        id_Exposure_Level.Src = "~/Images/04_2.5.png";
        //        break;
        //    case "5":
        //        id_Exposure_Level.Src = "~/Images/04_3.0.png";
        //        break;
        //    case "6":
        //        id_Exposure_Level.Src = "~/Images/04_3.5.png";
        //        break;
        //    case "7":
        //        id_Exposure_Level.Src = "~/Images/04_4.0.png";
        //        break;
        //    case "8":
        //        id_Exposure_Level.Src = "~/Images/04_4.5.png";
        //        break;
        //    case "9":
        //        id_Exposure_Level.Src = "~/Images/04_5.0.png";
        //        break;
        //    default:
        //        id_Exposure_Level.Src = "~/Images/04_AUTO.png";
        //        break;
        //}
        switch (currentSession.originalExposureLVLLabel)
        {
            case "0":
                id_Exposure_Level.ImageUrl = "~/Images/04_AUTO.png";
                break;
            case "1":
                id_Exposure_Level.ImageUrl = "~/Images/04_1.0.png";
                break;
            case "2":
                id_Exposure_Level.ImageUrl = "~/Images/04_1.5.png";
                break;
            case "3":
                id_Exposure_Level.ImageUrl = "~/Images/04_2.0.png";
                break;
            case "4":
                id_Exposure_Level.ImageUrl = "~/Images/04_2.5.png";
                break;
            case "5":
                id_Exposure_Level.ImageUrl = "~/Images/04_3.0.png";
                break;
            case "6":
                id_Exposure_Level.ImageUrl = "~/Images/04_3.5.png";
                break;
            case "7":
                id_Exposure_Level.ImageUrl = "~/Images/04_4.0.png";
                break;
            case "8":
                id_Exposure_Level.ImageUrl = "~/Images/04_4.5.png";
                break;
            case "9":
                id_Exposure_Level.ImageUrl = "~/Images/04_5.0.png";
                break;
            default:
                id_Exposure_Level.ImageUrl = "~/Images/04_AUTO.png";
                break;
        }
        #endregion


        #region --- Resolution ---
        //--- Resolution ---
        string strFeederVal = prop.GetPropValue(OSACopy.PropName.FEEDER_RESOLUTION.value);
        #endregion

        #region --- Color Mode ---
        //--- Color Mode ---
        strColorModeVal = prop.GetPropValue(OSACopy.PropName.COLOR_MODE.value);
        defColorModeVal = prop.GetDefaultPropValue(OSACopy.PropName.COLOR_MODE.value);

        if (defColorModeVal == OSACopy.PropValue.COLOR_MODE.MONOCHROME
            && strColorModeVal == OSACopy.PropValue.COLOR_MODE.MONOCHROME)
        {
            prop.SetPropValue(OSACopy.PropName.COLOR_MODE.value, OSACopy.PropValue.COLOR_MODE.FULL_COLOR);
        }

        strColorSelectVal = prop.GetPropValue(OSACopy.PropName.COLOR_SELECT.value);
        strColorReplaceVal = prop.GetPropValue(OSACopy.PropName.COLOR_REPLACE.value);

        defColorSelectVal = prop.GetDefaultPropValue(OSACopy.PropName.COLOR_SELECT.value);
        defColorReplaceVal = prop.GetDefaultPropValue(OSACopy.PropName.COLOR_REPLACE.value);
        #endregion

        #region --- Filing ---
        //--- Filing ---
        prop.SetPropValue("Filing", "main");
        prop.SetPropValue("PublicPDF", "true");
        prop.SetPropValue("FileName", "COPY_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        #endregion

        #region --- Special Mode ---
        //--- Special Mode ---
        List<string> specialValues = prop.GetMultiPropValue(OSACopy.PropName.SPECIAL_MODE.value);
        #endregion

        #region --- Preview ---
        //--- Preview ---
        strPreviewVal = prop.GetPropValue(OSACopy.PropName.PREVIEW);
        defPreviewVal = prop.GetDefaultPropValue(OSACopy.PropName.PREVIEW);

        strCollate = prop.GetPropValue(OSACopy.PropName.COLLATE.value);
        #endregion

        //--- Save session information to file ---
        Helper.Save(Request);

        base.OnInit(e);
    }

    // --- 选纸 ---
    protected void id_InputTray_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SourceSetting00.aspx");
    }
    // --- 双面复印 ---
    protected void id_SingleDouble_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SourceSetting01.aspx");
    }
    // --- 倍率% ---
    protected void id_Ratio_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SourceSetting02.aspx");
    }
    // --- 复印浓度 ---
    protected void id_Exposure_Level_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ExposureSetting.aspx");
    }
}
