
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

public partial class SourceSetting00 : CommonOSA.BasePage
{

    protected string strBypassTrayType = string.Empty;
    protected string strMainBtnSetting = string.Empty;
    private const string SESSION_INPUT_TRAY = "SESSION_INPUT_TRAY";
    protected DeviceSession currentSession = null;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        // No session information is found. Try to load from a file.
        id_Label_Title.Text = Resources.OSAStrings.TITLE_LABEL_MAIN + OSACopy.PropName.SEPARATOR +
            Resources.OSAStrings.TITLE_LABEL_ORIGINAL_INPUT_TRAY;
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

        base.OnInit(e);
    }

    private void CreateControls()
    {
        CreateTrayControls();
    }

    private void CreateTrayControls()
    {
        List<WebControl> tray_btns = null;
        tray_btns = this.CreateListButtons(this.id_div_InputTray_left, ControlType.Button, OSACopy.PropName.INPUT_TRAY, "btn_tray_set", HiddenInputTray);
        //--- The property value corresponding to control ID is set ---
        currentSession.SetControlValue(SESSION_INPUT_TRAY, tray_btns);
        //--- Set session information ---
        Helper.SetSession(Request, currentSession);
        //--- Save session information to file ---
        Helper.Save(Request);
    }

    protected void Btn_OK_Click(object sender, EventArgs e)
    {
        SaveData();
//        Response.Redirect("SourceSetting.aspx", false);
        Response.Redirect("Main.aspx");
    }

    private void SaveData()
    {
        //--- The control setting value array that corresponds to the key is gotten ---
        Dictionary<string, string> dicInputTray = currentSession.GetControlValue(SESSION_INPUT_TRAY);
        prop.SetPropValue(OSACopy.PropName.INPUT_TRAY.value, dicInputTray[HiddenInputTray.Value]);

        // Combination of 'InputTray=auto' and 'SpecialMode=MultiShot' is not allowed, so we unset MultiShot if the value is auto.
        if (OSACopy.PropValue.INPUT_TRAY.AUTO == dicInputTray[HiddenInputTray.Value])
        {
            List<string> listSpMode = prop.GetMultiPropValue(OSACopy.PropName.SPECIAL_MODE.value);
            if (listSpMode.Contains(OSACopy.PropValue.SPECIAL_MODE.MULTI_SHOT))
            {
                listSpMode.Remove(OSACopy.PropValue.SPECIAL_MODE.MULTI_SHOT);
                if (0 == listSpMode.Count)
                {
                    listSpMode.Add(OSACopy.PropValue.SPECIAL_MODE.NONE);
                }
                prop.SetMultiPropValue(OSACopy.PropName.SPECIAL_MODE.value, listSpMode);
            }
            //--- MultiShot is default. ---
            prop.SetPropValue(OSACopy.PropName.MULTI_SHOT_MODE.value, prop.GetDefaultPropValue(OSACopy.PropName.MULTI_SHOT_MODE.value));
            prop.SetPropValue(OSACopy.PropName.MULTI_SHOT_ORDER.value, prop.GetDefaultPropValue(OSACopy.PropName.MULTI_SHOT_ORDER.value));
            prop.SetPropValue(OSACopy.PropName.MULTI_SHOT_BORDER.value, prop.GetDefaultPropValue(OSACopy.PropName.MULTI_SHOT_BORDER.value));
        }
        
        //--- Save session information to file ---
        Helper.Save(Request);
    }
}
