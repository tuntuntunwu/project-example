
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

namespace CommonOSA
{
    public class BasePage : Page
    {

        //--- Color when button is selected ---
        protected string SELECTED_COLOR = BaseHelper.GetAppSettingString("SelectedColor");
        protected const string FATAL_ERROR_URL = "FatalError.aspx";
        protected const string GET_RESOURCES_STRING_URL = "GetResourcesString.aspx";
        protected string AJAX_TIME_OUT = BaseHelper.GetAppSettingString("AjaxTimeOut");
        protected JobSettableProp prop = null;


        //--- Type of control to create ---
        protected enum ControlType
        {
            Button,
            ImageButton
        }

        //--- Create controls from MFP information parameter list ---
        protected List<WebControl> CreateListButtons(Control targetFrom, ControlType type, CommonOSA.PropNameValue propName, string cssName, Control selectedValue)
        {
            return CreateListButtons(targetFrom, type, propName, cssName, selectedValue, null);
        }

        //--- Create controls from MFP information parameter list ---
        protected List<WebControl> CreateListButtons(Control targetFrom, ControlType type, CommonOSA.PropNameValue propName, string cssName, Control selectedValue, PageStateControl pageCtrl)
        {

            //--- Get parameter list ---
            string[] targetList = null;
            targetList = (string[])prop.GetPropList(propName.value);

            //--- Control array ---
            List<WebControl> targetControls = new List<WebControl>();

            //--- Parameter list doesn't exist ---
            if (null == targetList)
            {
                return targetControls;
            }

            //--- Parameter list doesn't exist ---
            if (0 == targetList.Length)
            {
                return targetControls;
            }

            //--- When the field that maintains selected control ID is HiddenField ---
            if (selectedValue is HiddenField)
            {
                //--- Only one selection in the control group has been created JavaScript ---
                CreateSelectedJavaScript(targetFrom, propName, selectedValue.ClientID);
            }
            else
            {
                //--- Multiple selection in the control group has been created JavaScript ---
                CreateMultiSelectedJavaScriptSpMode(targetFrom, propName, selectedValue.ClientID);
            }

            //--- Div control group get the name tag ---
            string gropName = GetGroupName(targetFrom, propName);

            //--- Div group to add a tag ---
            targetFrom.Controls.Add(new LiteralControl("<div id=" + gropName + ">"));

            //--- Get parameter value ---
            List<string> defValue = prop.GetMultiPropValue(propName.value);
            WebControl targetControl = null;

            int i = 0;
            int startIndex = 0;
            int lastIndex = targetList.Length - 1;

            //--- If the page control is set to get the start and end position control from the page to display page ---
            if (null != pageCtrl)
            {
                pageCtrl.CalcCurrentPageInfo(targetList.Length);
                startIndex = pageCtrl.GetStartIndex();
                lastIndex = pageCtrl.GetLastIndex();
            }

            string strValue = string.Empty; 

            for (i = startIndex; i <= lastIndex; i++)
            {

                strValue = targetList[i];
                
                if (IsLoopContinue(targetFrom, targetList, propName, strValue))
                {
                    continue;
                }

                switch (type)
                {
                    case ControlType.Button:
                        //--- Create button ---
                        targetControl = new Button();
                        ((Button)targetControl).Text = prop.GetResourcePropVal(propName, strValue);
                        break;
                    case ControlType.ImageButton:
                        //--- Create image button ---
                        targetControl = new ImageButton();
                        ((ImageButton)targetControl).ImageUrl = "Images/" + strValue + ".png";
                        break;
                    default:
                        break;
                }

                //--- Set parameter list value ---
                targetControl.ToolTip = strValue;
                //--- Set control ID ---
                targetControl.ID = GetControlID(targetFrom, targetControl, propName, strValue);
                targetControl.CssClass = cssName;

                //--- If the parameter contains the value of the parameter list (selected) ---
                if (defValue.Contains(strValue))
                {
                    //--- Set color ---
                    targetControl.Style["background-color"] = SELECTED_COLOR;

                    //--- ID of the control been selected by specified HiddenField is set ---
                    if (selectedValue is HiddenField)
                    {
                        ((HiddenField)selectedValue).Value = targetControl.ID;
                    }
                    else
                    {
                        //--- ID of the control been selected by HiddenField in the Div tag is set at multiple selections ---
                        foreach (Control multiSelectValue in selectedValue.Controls)
                        {
                            if (multiSelectValue is HiddenField && string.IsNullOrEmpty(((HiddenField)multiSelectValue).Value))
                            {
                                ((HiddenField)multiSelectValue).Value = targetControl.ID;
                                break;
                            }
                        }
                    }

                }

                StringBuilder javaScript = null;

                if (selectedValue is HiddenField)
                {
                    //--- Call only one selection JavaScript ---
                    javaScript = CreateOnClientClick(targetFrom, targetControl, propName, strValue, selectedValue.ID);
                }
                else
                {
                    //--- Call multiple selection JavaScript ---
                    javaScript = CreateMultiOnClientClick(targetFrom, targetControl, propName, strValue, selectedValue.ID);
                }

                switch (type)
                {
                    case ControlType.Button:
                        ((Button)targetControl).OnClientClick = javaScript.ToString();
                        break;
                    case ControlType.ImageButton:
                        ((ImageButton)targetControl).OnClientClick = javaScript.ToString();
                        break;
                    default:
                        break;
                }

                //--- Add method is called before the control ---
                CreateListButtonsBefore(targetFrom, targetControl, targetList, propName, strValue);
                targetFrom.Controls.Add(targetControl);
                //--- Add method is called after the control ---
                CreateListButtonsAfter(targetFrom, targetControl, targetList, propName, strValue);

                //--- JavaScript reference value of the parameter list for additional HiddenField ---
                HiddenField hiddenPropValue = new HiddenField();
                hiddenPropValue.ID = "hiddenPropValue_" + targetControl.ID;
                hiddenPropValue.Value = strValue;
                targetFrom.Controls.Add(hiddenPropValue);

                targetControls.Add(targetControl);

            }

            //--- Div group to add a tag (termination) ---
            targetFrom.Controls.Add(new LiteralControl("</div>"));
            return targetControls;
        }

        //--- Get the name of the group Div tag (It is possible to change by override) ---
        protected virtual string GetGroupName(Control targetFrom, CommonOSA.PropNameValue propName)
        {
            return "id_div_group_" + propName.value;
        }

        //--- Get control ID (It is possible to change by override) ---
        protected virtual string GetControlID(Control targetFrom, WebControl targetControl, CommonOSA.PropNameValue propName, string propValue)
        {
            return propName.value + "_" + propValue;
        }

        //--- Continue the loop when judging create the control (It is possible to change by override) ---
        protected virtual bool IsLoopContinue(Control targetFrom, string[] targetList, CommonOSA.PropNameValue propName, string propValue)
        {
            return false;
        }

        //--- Add method is called before the control (It is possible to add by override) ---
        protected virtual void CreateListButtonsBefore(Control targetFrom, WebControl targetControl, string[] targetList, CommonOSA.PropNameValue propName, string propValue)
        {
        }

        //--- Add method is called after the control (It is possible to add by override) ---
        protected virtual void CreateListButtonsAfter(Control targetFrom, WebControl targetControl, string[] targetList, CommonOSA.PropNameValue propName, string propValue)
        {
        }

        //--- Choose not only one in the control group that is constantly creating JavaScript ---
        protected virtual void CreateSelectedJavaScript(Control targetFrom, CommonOSA.PropNameValue propName, string hiddenName)
        {

            String csName = "SelectedJavaScript";
            Type csType = this.GetType();

            ClientScriptManager cs = this.ClientScript;

            if (!cs.IsStartupScriptRegistered(csType, csName))
            {

                StringBuilder javaScript = new StringBuilder();
                javaScript.AppendLine("function SelectedControl(ControlID, GropName, HiddenName) {");
                javaScript.AppendLine("    var groupDiv = document.getElementById(GropName);");
                javaScript.AppendLine("    var buttons = groupDiv.getElementsByTagName('input');");
                javaScript.AppendLine("    for (i = 0; i < buttons.length; i++) {");
                javaScript.AppendLine("        if (buttons[i].id == ControlID) {");
                javaScript.AppendLine("            buttons[i].style.backgroundColor = '" + SELECTED_COLOR + "';");
                javaScript.AppendLine("            var select = document.getElementById(HiddenName);");
                javaScript.AppendLine("            select.value = ControlID;");
                javaScript.AppendLine("        } else {");
                javaScript.AppendLine("            buttons[i].style.backgroundColor = '';");
                javaScript.AppendLine("        }");
                javaScript.AppendLine("    }");
                javaScript.AppendLine("    return false;");
                javaScript.AppendLine("}");

                cs.RegisterStartupScript(csType, csName, javaScript.ToString(), true);

            }

        }

        //--- Choose not only one in the control group is always able to de-select it again by selecting Create JavaScript ---
        protected void CreateSelectedJavaScriptSpMode(Control targetFrom, CommonOSA.PropNameValue propName, string hiddenName)
        {

            String csName = "SelectedJavaScriptSpMode";
            Type csType = this.GetType();

            ClientScriptManager cs = this.ClientScript;

            if (!cs.IsStartupScriptRegistered(csType, csName))
            {

                StringBuilder javaScript = new StringBuilder();

                javaScript.AppendLine("function SelectedControlSpMode(ControlID, GropName, HiddenName) {");
                javaScript.AppendLine("    var groupDiv = document.getElementById(GropName);");
                javaScript.AppendLine("    var buttons = groupDiv.getElementsByTagName('input');");
                javaScript.AppendLine("    for (i = 0; i < buttons.length; i++) {");
                javaScript.AppendLine("        if (buttons[i].id == ControlID) {");
                javaScript.AppendLine("            if (buttons[i].style.backgroundColor == '') {");
                javaScript.AppendLine("                buttons[i].style.backgroundColor = '" + SELECTED_COLOR + "';");
                javaScript.AppendLine("                var select = document.getElementById(HiddenName);");
                javaScript.AppendLine("                select.value = ControlID;");
                javaScript.AppendLine("            }");
                javaScript.AppendLine("            else {");
                javaScript.AppendLine("                buttons[i].style.backgroundColor = '';");
                javaScript.AppendLine("                var select = document.getElementById(HiddenName);");
                javaScript.AppendLine("                select.value = '';");
                javaScript.AppendLine("            }");
                javaScript.AppendLine("        } else {");
                javaScript.AppendLine("            buttons[i].style.backgroundColor = '';");
                javaScript.AppendLine("        }");
                javaScript.AppendLine("    }");
                javaScript.AppendLine("    return false;");
                javaScript.AppendLine("}");

                cs.RegisterStartupScript(csType, csName, javaScript.ToString(), true);

            }

        }

        //--- Multiple selection in the control group has been created JavaScript ---
        protected void CreateMultiSelectedJavaScriptSpMode(Control targetFrom, CommonOSA.PropNameValue propName, string hiddenName)
        {

            String csName = "MultiSelectedJavaScriptSpMode";
            Type csType = this.GetType();

            ClientScriptManager cs = this.ClientScript;

            if (!cs.IsStartupScriptRegistered(csType, csName))
            {

                StringBuilder javaScript = new StringBuilder();

                javaScript.AppendLine("function MultiSelectedControlSpMode(ControlID, GropName, MultiDivName, ActiveOnly) {");
                javaScript.AppendLine("    var continueFlg = false;");
                javaScript.AppendLine("    var groupDiv = document.getElementById(GropName);");
                javaScript.AppendLine("    var buttons = groupDiv.getElementsByTagName('input');");
                javaScript.AppendLine("    for (i = 0; i < buttons.length; i++) {");
                javaScript.AppendLine("        if (buttons[i].id == ControlID) {");
                javaScript.AppendLine("            var multiDiv = document.getElementById(MultiDivName);");
                javaScript.AppendLine("            var multiSelectedValue = multiDiv.getElementsByTagName('input');");
                javaScript.AppendLine("            if(!ActiveOnly) {");
                javaScript.AppendLine("                for (j = 0; j < multiSelectedValue.length; j++) {");
                javaScript.AppendLine("                    if (multiSelectedValue[j].value ==  ControlID) {");
                javaScript.AppendLine("                        buttons[i].style.backgroundColor = '';");
                javaScript.AppendLine("                        multiSelectedValue[j].value = '';");
                javaScript.AppendLine("                        continueFlg = true;");
                javaScript.AppendLine("                        break;");
                javaScript.AppendLine("                    }");
                javaScript.AppendLine("                }");
                javaScript.AppendLine("            }");
                javaScript.AppendLine("            if (!continueFlg) {");
                javaScript.AppendLine("                for (j = 0; j < multiSelectedValue.length; j++) {");
                javaScript.AppendLine("                    if (multiSelectedValue[j].value ==  '') {");
                javaScript.AppendLine("                        buttons[i].style.backgroundColor = '" + SELECTED_COLOR + "';");
                javaScript.AppendLine("                        multiSelectedValue[j].value = ControlID;");
                javaScript.AppendLine("                        break;");
                javaScript.AppendLine("                    }");
                javaScript.AppendLine("                }");
                javaScript.AppendLine("            }");
                javaScript.AppendLine("        }");
                javaScript.AppendLine("    }");
                javaScript.AppendLine("    return false;");
                javaScript.AppendLine("}");

                cs.RegisterStartupScript(csType, csName, javaScript.ToString(), true);

            }

        }

        //--- When the Redirect to check whether a button has been selected to create a JavaScript ---
        protected void CreateRedirectCheckJavaScript()
        {

            String csName = "RedirectCheckJavaScript";
            Type csType = this.GetType();

            ClientScriptManager cs = this.ClientScript;

            if (!cs.IsStartupScriptRegistered(csType, csName))
            {

                StringBuilder javaScript = new StringBuilder();

                javaScript.AppendLine("function RedirectCheck(ControlID, MultiDivName) {");
                javaScript.AppendLine("    var multiDiv = document.getElementById(MultiDivName);");
                javaScript.AppendLine("    var multiSelectedValue = multiDiv.getElementsByTagName('input');");
                javaScript.AppendLine("    for (j = 0; j < multiSelectedValue.length; j++) {");
                javaScript.AppendLine("        if (multiSelectedValue[j].value ==  ControlID) {");
                javaScript.AppendLine("            return true;");
                javaScript.AppendLine("        }");
                javaScript.AppendLine("    }");
                javaScript.AppendLine("    return false;");
                javaScript.AppendLine("}");
                cs.RegisterStartupScript(csType, csName, javaScript.ToString(), true);

            }
        }

        //--- JavaScript embedded in the button click event (only one selection) ---
        protected virtual StringBuilder CreateOnClientClick(Control targetFrom, WebControl targetControl, CommonOSA.PropNameValue propName, string propValue, string hiddenName)
        { 
        
            StringBuilder javaScript = new StringBuilder();
            string gropName = GetGroupName(targetFrom, propName);
            javaScript.Append("return SelectedControl('" + targetControl.ID + "', '" + gropName + "', '" + hiddenName + "');");
            return javaScript;

        }

        //--- JavaScript embedded in the button click event (multiple selection) ---
        protected virtual StringBuilder CreateMultiOnClientClick(Control targetFrom, WebControl targetControl, CommonOSA.PropNameValue propName, string propValue, string multiItemValueName)
        {

            StringBuilder javaScript = new StringBuilder();
            string gropName = GetGroupName(targetFrom, propName);
            javaScript.Append("return MultiSelectedControlSpMode('" + targetControl.ID + "', '" + gropName + "', '" + multiItemValueName + "',false);");
            return javaScript;

        }

        //--- Css for the screen display is set to the specified control ---
        protected void SetControlCssClass(WebControl ctrl)
        {
            SetControlCssClass(ctrl, "id_parameter_select_value");
        }

        //--- Specified Css is set, except when the text of the specified control is NULL ---
        protected void SetControlCssClass(WebControl ctrl, string cssName)
        {
            if (null == ctrl)
            {
                return;
            }

            if (ctrl is ITextControl)
            {
                ITextControl val = (ITextControl)ctrl;
                if (!string.IsNullOrEmpty(val.Text))
                {
                    ctrl.CssClass = cssName;
                }
            }
        }
    
    }
}
