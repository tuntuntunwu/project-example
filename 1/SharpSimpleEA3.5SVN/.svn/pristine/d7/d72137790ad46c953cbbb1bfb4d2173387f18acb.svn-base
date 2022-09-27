
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

public partial class SourceSetting : BasePage
{
    protected string strRatioVal = string.Empty;
    protected int min = 0;
    protected int max = 0;
    protected const int UP_VALUE = 25;
    protected const int DOWN_VALUE = -25;

    protected string errMsg = null;

    private const string SESSION_DUPLEX_MODE = "DuplexMode";

    protected DeviceSession currentSession = null;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        id_Label_Title.Text = Resources.OSAStrings.TITLE_LABEL_MAIN + OSACopy.PropName.SEPARATOR +
            Resources.OSAStrings.TITLE_LABEL_RATIO;
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

    protected void OkButton_Click(object sender, EventArgs e)
    {
        SaveData();
        Response.Redirect("Main.aspx");
    }

    private void CreateControls()
    {
        CreateCopyRatio();

        // CreateDuplexMode
        //List<WebControl> btnDuplexs = null;
        //btnDuplexs = this.CreateListButtons(this.id_div_srcset_left, ControlType.ImageButton, OSACopy.PropName.DUPLEX_MODE, "btn_dup_set", HiddenDuplexMode);

        //--- The property value corresponding to control ID is set ---
        //currentSession.SetControlValue(SESSION_DUPLEX_MODE, btnDuplexs);
        //--- Set session information ---
        Helper.SetSession(Request, currentSession);
        //--- Save session information to file ---
        Helper.Save(Request);
    }

    private void CreateCopyRatio()
    {
        GetJob.PropertyAppInfo[] CopyRatioVals = (GetJob.PropertyAppInfo[])prop.GetPropList(OSACopy.PropName.COPYRATIO);
        foreach (GetJob.PropertyAppInfo ratioVal in CopyRatioVals)
        {
            switch (ratioVal.name)
            {
                case OSACopy.PropValue.COPYRATIO.MIN_VALUE:
                    min = int.Parse(ratioVal.Value);
                    break;
                case OSACopy.PropValue.COPYRATIO.MAX_VALUE:
                    max = int.Parse(ratioVal.Value);
                    break;
                default:
                    break;
            }
        }
        errMsg = string.Format( Resources.OSAStrings.COPY_ERROR_MESSAGE, min, max );

        id_Label_Range.Text = "%" + string.Format(OSACopy.PropName.FROM_TO, min, max); 
        id_text_Ratio.Text = prop.GetPropValue(OSACopy.PropName.COPYRATIO);
        strRatioVal = id_text_Ratio.Text;
    }

    protected override void CreateListButtonsBefore(Control targetFrom, WebControl targetControl, string[] targetList, CommonOSA.PropNameValue propName, string propValue)
    {
        //if (propName.Equals(OSACopy.PropName.DUPLEX_MODE))
        //{
        //    targetControl.BorderStyle = BorderStyle.Double;
        //    targetControl.BorderWidth = 3;
        //}
    }

    protected override void CreateListButtonsAfter(Control targetFrom, WebControl targetControl, string[] targetList, CommonOSA.PropNameValue propName, string propValue)
    {
        //if (propName.Equals(OSACopy.PropName.DUPLEX_MODE))
        //{
        //    targetFrom.Controls.Add(new LiteralControl("<label id=\"id_label_ImageExplanation\">" + prop.GetResourcePropVal(OSACopy.PropName.DUPLEX_MODE, propValue) + "</label><br />"));
        //}
    }

    private void SaveData()
    {
        //--- The control setting value array that corresponds to the key is gotten ---
        //Dictionary<string, string> dicDuplexMode = currentSession.GetControlValue(SESSION_DUPLEX_MODE);
        //prop.SetPropValue(OSACopy.PropName.DUPLEX_MODE.value, dicDuplexMode[HiddenDuplexMode.Value]);
        prop.SetPropValue(OSACopy.PropName.COPYRATIO, id_text_Ratio.Text);

        //prop.SetPropValue(OSACopy.PropName.DUPLEX_BINDING_CHANGE.value, OSACopy.PropValue.FALSE);

        //--- Save session information to file ---
        Helper.Save(Request);
    }
}
