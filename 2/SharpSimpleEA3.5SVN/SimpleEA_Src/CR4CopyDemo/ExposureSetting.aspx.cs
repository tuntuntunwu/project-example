
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
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonOSA;

public partial class ExposureSetting : BasePage
{
    private bool fAuto = false;
    private const string SESSION_EXPOSURE_MODE = "SESSION_EXPOSURE_MODE";
    protected DeviceSession currentSession = null;
    protected string defExposureMode = null;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        id_Label_Title.Text = Resources.OSAStrings.TITLE_LABEL_MAIN + OSACopy.PropName.SEPARATOR +
            Resources.OSAStrings.TITLE_LABEL_EXPOSURE;
        if (null == prop)
        {
            // No session information is found. Try to load from a file.
            Helper.Load(Request);
            currentSession = Helper.GetSession(Request);
            if (null == currentSession)
            {
                return; // Unrecoverable 
            }
            prop = new JobSettableProp(currentSession);
            activeStyleSheet.Text = "<link rel='stylesheet' type='text/css' href='" + currentSession.cssFile + ".css'>";
        }

        CreateControls();

        defExposureMode = prop.GetDefaultPropValue(OSACopy.PropName.EXPOSURE_MODE.value);

        base.OnInit(e);
    }

    protected void CreateControls()
    {
        string strVal = prop.GetPropValue(OSACopy.PropName.EXPOSURE_LEVEL.value);

        if (0 == string.Compare("0", strVal, true))
        {
            // exposure-level is AUTO.
            this.fAuto = true;
        }
        else
        {
            this.fAuto = false;
        }
        HiddenExposureLevel.Value = strVal;

        // make [AUTO] Button.
        CreateAutoButton();
        // make [MANUAL] Button.
        CreateManualButton();
        // make [EXPOSURE_LEVEL] DropDownList.
        CreateLevelList();
        CreateExposure();

    }

    private void CreateAutoButton()
    {
        string strDefVal = string.Empty;
        if (null != prop)
        {
            strDefVal = prop.GetPropValue(OSACopy.PropName.EXPOSURE_LEVEL.value);
        }
        id_exposure_auto.Value = Resources.OSAStrings.AUTO_STRING;

        if (0 == string.Compare("0", strDefVal, true))
        {
            id_exposure_auto.Style["background-color"] = SELECTED_COLOR;
            if (string.IsNullOrEmpty(HiddenExposureLevel.Value))
            {
                HiddenExposureLevel.Value = "0";
            }
        }
    }

    private void CreateManualButton()
    {
        string strDefVal = string.Empty;
        if (null != prop)
        {
            strDefVal = prop.GetPropValue(OSACopy.PropName.EXPOSURE_LEVEL.value);
        }
        id_exposure_manual.Value = Resources.OSAStrings.MANUAL_STRING;

        if (0 != string.Compare("0", strDefVal, true))
        {
            id_exposure_manual.Style["background-color"] = SELECTED_COLOR;
        }
    }

    private void CreateLevelList()
    {
        string strValue = string.Empty;

        int max = 0;
        int min = 0;

        //--- Get EXPOSURE_LEVEL's minimum value and maximum value ---
        GetJob.PropertyAppInfo[] appInfos = (GetJob.PropertyAppInfo[])prop.GetPropList(OSACopy.PropName.EXPOSURE_LEVEL.value);
        foreach (GetJob.PropertyAppInfo appInfo in appInfos)
        { 
            switch(appInfo.name)
            {
                case OSACopy.PropValue.EXPOSURE_LEVEL.MIN_VALUE:
                    min = int.Parse(appInfo.Value);
                    break;
                case OSACopy.PropValue.EXPOSURE_LEVEL.MAX_VALUE:
                    max = int.Parse(appInfo.Value);
                    break;
                default:
                    break;
            }
        }

        //--- Create list item ---
        for (int i = min; i <= max; i++)
        {
            if (0 != i)
            {
                id_level_list.Items.Add(i.ToString());
            }
        }

        if (this.fAuto)
        {
            id_level_list.Enabled = false;
        }

        string strDefVal = string.Empty;
        if (null != prop)
        {
            id_level_list.SelectedValue = prop.GetPropValue(OSACopy.PropName.EXPOSURE_LEVEL.value);
        }

        id_level_list.Attributes["onchange"] = "return SelectExposureLevel()";
    }

    private void CreateExposure()
    {
        // Display a label for exposure mode.
        id_div_exp_right.Controls.Add(new LiteralControl((Resources.OSAStrings.EXPOSURE_MODE_STRING + "<br /><br />")));
            
        List<WebControl> btnExposureMode = null;
        btnExposureMode = this.CreateListButtons(this.id_div_exp_right, ControlType.ImageButton, OSACopy.PropName.EXPOSURE_MODE, "btn_expmode_set", HiddenExposureMode);
        //--- The property value corresponding to control ID is set ---
        currentSession.SetControlValue(SESSION_EXPOSURE_MODE, btnExposureMode);
        //--- Set session information ---
        Helper.SetSession(Request, currentSession);
        //--- Save session information to file ---
        Helper.Save(Request);
    }

    protected override void CreateListButtonsBefore(Control targetFrom, WebControl targetControl, string[] targetList, CommonOSA.PropNameValue propName, string propValue)
    {
        ((ImageButton)targetControl).AlternateText = prop.GetResourcePropVal(OSACopy.PropName.EXPOSURE_MODE, propValue);
        targetControl.BorderStyle = BorderStyle.Double;
        targetControl.BorderWidth = 3;
    }

    protected override void CreateListButtonsAfter(Control targetFrom, WebControl targetControl, string[] targetList, CommonOSA.PropNameValue propName, string propValue)
    {
        if (propName.Equals(OSACopy.PropName.EXPOSURE_MODE))
        {
            if (this.fAuto)
            {
                targetControl.Style["background-color"] = string.Empty;
            }
        
        }

        targetFrom.Controls.Add(new LiteralControl("<label id=\"id_label_ImageExposure\">" + prop.GetResourcePropVal(OSACopy.PropName.EXPOSURE_MODE, propValue) + "</label>"));
    }

    protected override StringBuilder CreateOnClientClick(Control targetFrom, WebControl targetControl, CommonOSA.PropNameValue propName, string propValue, string hiddenName)
    {
        StringBuilder javaScript = new StringBuilder();
        string gropName = GetGroupName(targetFrom, propName);
        javaScript.Append("SelectedControl('" + targetControl.ID + "', '" + gropName + "', '" + hiddenName + "');");
        javaScript.Append("return SelectManualButton();");
        return javaScript;
    }

    protected void Btn_OK_Click(object sender, EventArgs e)
    {
        SaveData();
        Response.Redirect("Main.aspx");
    }

    private void SaveData()
    {
        prop.SetPropValue(OSACopy.PropName.EXPOSURE_LEVEL.value, HiddenExposureLevel.Value);
        //--- The control setting value array that corresponds to the key is gotten ---
        Dictionary<string, string> dicExposureMode = currentSession.GetControlValue(SESSION_EXPOSURE_MODE);
        prop.SetPropValue(OSACopy.PropName.EXPOSURE_MODE.value, dicExposureMode[HiddenExposureMode.Value]);
        //--- Save session information to file ---
        Helper.Save(Request);
    }
}