<%@ Page Language="C#" ContentType="text/html" CodeFile="Main.aspx.cs" Inherits="Main"
    UICulture="Auto" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <asp:Literal runat="server" ID="activeStyleSheet"></asp:Literal>
    <script type="text/javascript" src="JavaScript/common.js"></script>
    <script type="text/javascript">
        window.onload = WindowLoad;

        var curCopiesVal = "<%=strCopiesVal%>";
        var curPreviewVal = "<%=strPreviewVal%>";
        var curCollate = "<%=strCollate%>";

        function WindowLoad() {
            if (null == curPreviewVal || "" == curPreviewVal) {
                document.getElementById("id_div_preview").style.display = "none";
            } else {
                document.getElementById("id_div_preview").style.display = "block";
                if ("true" == curPreviewVal) {
                    document.getElementById("id_check_preview").checked = true;
                } else {
                    document.getElementById("id_check_preview").checked = false;
                }
            }

            if (null == "<%=strColorModeVal%>" || "" == "<%=strColorModeVal%>") {
                document.getElementById("id_div_execute_bottom_p1").style.display = "block";
            }
            else {
                document.getElementById("id_div_execute_bottom_p2_left").style.display = "block";
                document.getElementById("id_div_execute_bottom_p2_right").style.display = "block";
            }
        }

        var exeParameters = null;
        var orgParameters = null;
        function ExecCopy(isMonochrome) {

            if (!CheckCopies()) {
                return;
            }

            exeParameters = {};
            orgParameters = {};

            if (curCopiesVal != document.getElementById('id_text_Copies').value) {
                exeParameters["Copies"] = document.getElementById('id_text_Copies').value;
                curCopiesVal = document.getElementById('id_text_Copies').value;
            }

            if ("block" == document.getElementById("id_div_preview").style.display &&
                    curPreviewVal != document.getElementById('id_check_preview').checked.toString()) {
                exeParameters["Preview"] = document.getElementById('id_check_preview').checked;
                curPreviewVal = document.getElementById('id_check_preview').checked.toString();
            }

            if ("block" == document.getElementById("id_div_preview").style.display &&
                    document.getElementById('id_check_preview').checked) {
                if (curCollate != "" && curCollate != "<%=OSACopy.PropValue.COLLATE.SORT%>") {
                    exeParameters["Collate"] = "<%=OSACopy.PropValue.COLLATE.SORT%>";
                    orgParameters["Collate"] = "<%=strCollate%>";
                }
            }

            if (null != "<%=strColorModeVal %>" && "" != "<%=strColorModeVal %>") {
                if (isMonochrome) {
                    if ("<%=OSACopy.PropValue.COLOR_MODE.MONOCHROME%>" != "<%=strColorModeVal%>") {
                        exeParameters["ColorMode"] = "<%=OSACopy.PropValue.COLOR_MODE.MONOCHROME%>";
                        orgParameters["ColorMode"] = "<%=strColorModeVal%>";
                    }
                    if ("<%=defColorSelectVal%>" != "<%=strColorSelectVal%>") {
                        exeParameters["ColorSelect"] = "<%=defColorSelectVal%>";
                        orgParameters["ColorSelect"] = "<%=strColorSelectVal%>";
                    }
                    if ("<%=defColorReplaceVal%>" != "<%=strColorReplaceVal%>") {
                        exeParameters["ColorReplace"] = "<%=defColorReplaceVal%>";
                        orgParameters["ColorReplace"] = "<%=strColorReplaceVal%>";
                    }
                } else {
                    if ("<%=defColorModeVal %>" == "<%=OSACopy.PropValue.COLOR_MODE.MONOCHROME%>" && "<%=strColorModeVal %>" == "<%=OSACopy.PropValue.COLOR_MODE.MONOCHROME%>") {
                        exeParameters["ColorMode"] = "<%=OSACopy.PropValue.COLOR_MODE.FULL_COLOR%>";
                        orgParameters["ColorMode"] = "<%=OSACopy.PropValue.COLOR_MODE.FULL_COLOR%>";
                    }
                }
            }

            if (IsParameters(exeParameters)) {
                CreateXmlHttpRequest(exeParameters, "SetParameter.aspx", "<%=FATAL_ERROR_URL%>", "<%=AJAX_TIME_OUT%>", ExecuteJob);
            }
            else {
                ExecuteJob();
            }
        }

        function ExecuteJob() {

            var setJobCount = parseInt("<%=setJobCount%>", 10);

            if (document.getElementById('id_text_Copies').value != "<%=defCopiesVal%>") {
                setJobCount++;
            }

            if ("block" == document.getElementById('id_div_preview').style.display) {
                if (document.getElementById('id_check_preview').checked.toString() != "<%=defPreviewVal%>") {
                    setJobCount++;
                }
            }

            if (null != "<%=strColorModeVal %>" && "" != "<%=strColorModeVal %>") {

                var colorMode = "<%=strColorModeVal%>";
                if (null != exeParameters["ColorMode"]) {
                    colorMode = exeParameters["ColorMode"];
                }

                if (colorMode != "<%=defColorModeVal%>") {
                    setJobCount++;
                }

                var colorSelect = "<%=strColorSelectVal%>";
                if (null != exeParameters["ColorSelect"]) {
                    colorSelect = exeParameters["ColorSelect"];
                }

                if (colorSelect != "<%=defColorSelectVal%>") {
                    setJobCount++;
                }

                var colorReplace = "<%=strColorReplaceVal%>";
                if (null != exeParameters["ColorReplace"]) {
                    colorReplace = exeParameters["ColorReplace"];
                }

                if (colorReplace != "<%=defColorReplaceVal%>") {
                    setJobCount++;
                }
            }

            var url = "";

            if (setJobCount > 0) {
                // Callback function name : ExecuteJobCallback
                // noCache : A unique request key is generated for the cash measures
                url = "<%=requestExecuteJobParamsURL%>" + "&noCache=" + new Date().getTime();
            }
            else {
                // Callback function name : ExecuteJobCallback
                // noCache : A unique request key is generated for the cash measures
                url = "<%=requestExecuteJobURL%>" + "&noCache=" + new Date().getTime();
            }

            var target = document.createElement('script');
            target.charset = 'utf-8';
            target.src = url;
            target.onload = function () { document.getElementsByTagName("head")[0].removeChild(this); };
            document.getElementsByTagName("head")[0].appendChild(target);

        }

        function ExecuteJobCallback(_retParam) {

            var submitFnc = function () {
                if (CheckJsonpResponseError(_retParam, "<%=GET_RESOURCES_STRING_URL%>", "<%=FATAL_ERROR_URL%>", "<%=AJAX_TIME_OUT%>")) {
                    var JobId = document.getElementById('JobId');
                    JobId.value = _retParam["result"]["ExecuteJobResponse"]["jobId"];
                    document.forms[0].submit();
                }
            };

            if (IsParameters(orgParameters)) {
                CreateXmlHttpRequest(orgParameters, "SetParameter.aspx", "<%=FATAL_ERROR_URL%>", "<%=AJAX_TIME_OUT%>", submitFnc);
            }
            else {
                submitFnc();
            }
        }

        function Dispatch(el) {

            if (!CheckCopies()) {
                return;
            }

            var pageChangeFnc = function () {
                switch (el.id) {
                    case "id_InputTray":
                        document.location = "SourceSetting00.aspx";
                        break;
                    case "id_SingleDouble":
                        document.location = "SourceSetting01.aspx";
                        break;
                    case "id_Ratio":
                        document.location = "SourceSetting02.aspx";
                        break;
                    case "id_Exposure_Level":
                        document.location = "ExposureSetting.aspx";
                        break;
                    default:
                        break;
                }
            };

            var parameters = {};
            if (curCopiesVal != document.getElementById('id_text_Copies').value) {
                parameters["Copies"] = document.getElementById('id_text_Copies').value;
            }

            if ("block" == document.getElementById("id_div_preview").style.display &&
                    curPreviewVal != document.getElementById('id_check_preview').checked.toString()) {
                parameters["Preview"] = document.getElementById('id_check_preview').checked;
            }

            if (IsParameters(parameters)) {
                CreateXmlHttpRequest(parameters, "SetParameter.aspx", "<%=FATAL_ERROR_URL%>", "<%=AJAX_TIME_OUT%>", pageChangeFnc);
            }
            else {
                pageChangeFnc();
            }

        }

        function CopyValChange(val) {

            if (!CheckCopies()) {
                return;
            }

            var copyVal = document.getElementById('id_text_Copies');
            var iVal = parseInt(copyVal.value, 10);
            if (parseInt("<%=min%>", 10) <= parseInt(iVal + val, 10) && parseInt(iVal + val, 10) <= parseInt("<%=max%>", 10)) {
                copyVal.value = iVal + val;
            }
        }

        function CheckCopies() {

            var result = true;
            var copies = document.getElementById("id_text_Copies");

            if (CheckIsNumber(copies.value)) {
                var value = parseInt(copies.value, 10);
                if (value < parseInt("<%=min%>", 10) || parseInt("<%=max%>", 10) < value) {
                    result = false;
                }
                else {
                    copies.value = value;
                }
            }
            else {
                result = false;
            }

            if (!result) {
                alert("<%=string.Format(Resources.OSAStrings.COPIES_ERROR_MESSAGE, min, max) %>");
                copies.value = parseInt("<%=strCopiesVal%>", 10);
                copies.focus();
            }

            return result;

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 6px;
        }
        .style2
        {
            width: 7%;
        }
    </style>
</head>
<body>
    <%--Place the img tag here to preload the image.--%>
    <img alt="Dummy" src="Images/monochrome_start_button_down.png" style="display: none" />
    <img alt="Dummy" src="Images/color_start_button_down.png" style="display: none" />
    <img alt="Dummy" src="Images/start_button_down.png" style="display: none" />
    <div id="header" style="left: 0px; position: absolute; top: 0px;">
        <asp:Label ID="id_Label_Title" runat="server"></asp:Label>
    </div>
    <br />
    <br />
    <form id="ExecCopyForm" method="post" action="JobStatus.aspx">
    <input type="hidden" id="JobId" name="JobId" />
    </form>
    <div id="id_div_execute">
        <div id="id_div_execute_top">
            <div id="id_div_checkbox">
                <br />
                <div id="id_div_preview" style="display: none">
                    <input id="id_check_preview" type="checkbox" class="checkbox_css" /><label><%= Resources.OSAStrings.MAIN_CHK_PREVIEW%></label>
                </div>
            </div>
        </div>
        <div id="id_div_execute_bottom">
            <div id="id_div_execute_bottom_p2_left" style="display: none;">
                <input type="image" src="Images/monochrome_start_button.png" id="img_mono_start"
                    name="img_mono_start" onmousedown="this.src='Images/monochrome_start_button_down.png';"
                    onmouseup="this.src='Images/monochrome_start_button.png'; ExecCopy(true);" onmouseout="this.src='Images/monochrome_start_button.png';" />
                <div id="id_div_start_bw_copy" align="center" onmousedown="document.getElementById('img_mono_start').src='Images/monochrome_start_button_down.png';"
                    onmouseup="document.getElementById('img_mono_start').src='Images/monochrome_start_button.png'; ExecCopy(true);"
                    onmouseout="document.getElementById('img_mono_start').src='Images/monochrome_start_button.png';">
                    <table cellpadding="2" cellspacing="2" border="0">
                        <tr align="center">
                            <td>
                                <div class="monochrome_button_font_specify">
                                    <%=Resources.OSAStrings.MAIN_BTN_BLACK_WHITE%>
                                </div>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <div class="monochrome_button_font_specify">
                                    <%=Resources.OSAStrings.MAIN_BTN_START%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="id_div_execute_bottom_p2_right" style="display: none;">
                <input type="image" src="Images/color_start_button.png" id="img_color_start" name="img_color_start"
                    onmousedown="this.src='Images/color_start_button_down.png';" onmouseup="this.src='Images/color_start_button.png'; ExecCopy(false);"
                    onmouseout="this.src='Images/color_start_button.png';" />
                <div id="id_div_start_color_copy" align="center" onmousedown="document.getElementById('img_color_start').src='Images/color_start_button_down.png';"
                    onmouseup="document.getElementById('img_color_start').src='Images/color_start_button.png'; ExecCopy(false);"
                    onmouseout="document.getElementById('img_color_start').src='Images/color_start_button.png';">
                    <table cellpadding="2" cellspacing="2" border="0">
                        <tr align="center">
                            <td>
                                <div class="color_button_font_specify">
                                    <%=Resources.OSAStrings.MAIN_BTN_COLOR%>
                                </div>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <div class="color_button_font_specify">
                                    <%=Resources.OSAStrings.MAIN_BTN_START%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="id_div_execute_bottom_p1" style="display: none;">
                <input type="image" src="Images/start_button.png" id="img_copy_start" name="img_copy_start"
                    onmousedown="this.src='Images/start_button_down.png';" onmouseup="this.src='Images/start_button.png'; ExecCopy(true);"
                    onmouseout="this.src='Images/start_button.png';" />
                <div id="id_div_start" align="center" onmousedown="document.getElementById('img_copy_start').src='Images/start_button_down.png';"
                    onmouseup="document.getElementById('img_copy_start').src='Images/start_button.png'; ExecCopy(true);"
                    onmouseout="document.getElementById('img_copy_start').src='Images/start_button.png';">
                    <%= Resources.OSAStrings.MAIN_BTN_START%>
                </div>
            </div>
        </div>
    </div>
    <form id="Copy_Form" runat="server">
    <br />
    <table border="0" style="width: 100%; height: 20px;">
        <tr>
            <td colspan="5">
                <label>
                    <input type="text" id="id_text_Copies" value="<%=strCopiesVal %>" maxlength="4" istyle="4" />
                    <%= Resources.OSAStrings.COPIES%></label>
                <label>
                    <%= string.Format(OSACopy.PropName.FROM_TO, min, max)%></label>
                <input type="button" id="id_button_copy_down" value="<%= Resources.OSAStrings.BTN_DOWN %>"
                    onclick="CopyValChange(-1);" />
                <input type="button" id="id_button_copy_up" value="<%= Resources.OSAStrings.BTN_UP %>"
                    onclick="CopyValChange(1);" />
            </td>
        </tr>
    </table>
    <div id="id_div_execute_body">
        <div id="id_div_function01">
            <%--            <input id="id_InputTray" name="id_InputTray" type="image" src="Images/01_AUTO.png"
                onclick="Dispatch(this)" runat="server" class="btn_main" style="border-style: outset" />
            --%>
            <asp:ImageButton ID="id_InputTray" runat="server" Height="170px" Width="208px" ImageUrl="~/Images/01_AUTO.png"
                OnClick="id_InputTray_Click" />
            <div id="id_div_inputtray" align="center">
                <%--                <table cellpadding="2" cellspacing="2" border="0">
                    <tr align="center">
                        <td>
                            <div class="fun_inputtray_button_font_specify">
                                <%=Resources.OSAStrings.MAIN_BTN_BLACK_WHITE%>
                            </div>
                        </td>
                    </tr>
                </table>
                --%>
            </div>
        </div>
        <div id="id_div_empty01">
        </div>
        <div id="id_div_function02">
            <%--            <input id="id_SingleDouble" type="image" src="Images/02_11.png" onclick="Dispatch(this)"
                runat="server" class="btn_main" style="border-style: outset" />
            --%>
            <asp:ImageButton ID="id_SingleDouble" runat="server" Height="170px" Width="208px"
                ImageUrl="~/Images/02_11.png" OnClick="id_SingleDouble_Click" />
            <div id="id_div_singledouble" align="center">
                <%--            <div id="id_div_singledouble" align="center" onclick="Dispatch(this)" class="btn_main"';">
                <table cellpadding="2" cellspacing="2" border="0">
                    <tr align="center">
                        <td>
                            <div class="fun_singledouble_button_font_specify">
                                <%=Resources.OSAStrings.MAIN_BTN_COLOR%>
                            </div>
                        </td>
                    </tr>
                </table>
                --%>
            </div>
        </div>
        <div id="id_div_empty02">
        </div>
        <div id="id_div_function03">
            <%--            <input id="id_Ratio" type="image" src="Images/03_AUTO.png" onclick="Dispatch(this)"
                runat="server" class="btn_main" style="border-style: outset" />
            --%>
            <asp:ImageButton ID="id_Ratio" runat="server" Height="170px" Width="208px" ImageUrl="~/Images/03_AUTO.png"
                OnClick="id_Ratio_Click" />
            <div id="id_div_ratio" align="center">
                <table cellpadding="1" cellspacing="1" border="0">
                    <tr align="center">
                        <td>
                            <div class="fun_ratio_button_font_specify">
                                <%=strRatio%>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="id_div_empty03">
        </div>
        <div id="id_div_function04">
            <%--            <input id="id_Exposure_Level" type="image" src="Images/04_AUTO.png" onclick="Dispatch(this)"
                runat="server" class="btn_main" style="border-style: outset" />
            --%>
            <asp:ImageButton ID="id_Exposure_Level" runat="server" Height="170px" Width="208px"
                ImageUrl="~/Images/02_11.png" OnClick="id_Exposure_Level_Click" />
            <div id="id_div_exposure_level" align="center">
                <%--                <table cellpadding="2" cellspacing="2" border="0">
                    <tr align="center">
                        <td>
                            <div class="fun_exposure_button_font_specify">
                                <%=Resources.OSAStrings.MAIN_BTN_COLOR%>
                            </div>
                        </td>
                    </tr>
                </table>
                --%>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
