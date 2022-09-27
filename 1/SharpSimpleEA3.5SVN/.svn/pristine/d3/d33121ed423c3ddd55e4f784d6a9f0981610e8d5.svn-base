<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExposureSetting.aspx.cs"
    Inherits="ExposureSetting" UICulture="Auto" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <asp:Literal runat="server" ID="activeStyleSheet"></asp:Literal>
    <script type="text/javascript">
        function SelectAutoButton() {
            id_exposure_auto.style.background = "<%=SELECTED_COLOR%>";
            id_exposure_manual.style.background = "";
            id_level_list.disabled = true;
            HiddenExposureLevel.value = "0";
            NoneSelectedExposureMode();
            return false;
        }

        function SelectManualButton() {
            id_exposure_auto.style.background = "";
            id_exposure_manual.style.background = "<%=SELECTED_COLOR%>";
            id_level_list.disabled = false;
            HiddenExposureLevel.value = id_level_list.value;
            SelectedExposureMode();
            return false;
        }

        function SelectExposureLevel() {
            HiddenExposureLevel.value = id_level_list.value;
            return false;
        }

        function SelectedExposureMode() {
            var exposureModeValue = document.getElementById('hiddenPropValue_' + HiddenExposureMode.value).value;
            var groupDiv = document.getElementById("id_div_group_<%=OSACopy.PropName.EXPOSURE_MODE.value%>");
            var buttons = groupDiv.getElementsByTagName('input');

            for (i = 0; i < buttons.length; i++) {
                var retentionObject = document.getElementById('hiddenPropValue_' + buttons[i].id);
                if (null != retentionObject && retentionObject.value == exposureModeValue) {
                    buttons[i].style.backgroundColor = "<%=SELECTED_COLOR%>";
                }
            }
        }

        function NoneSelectedExposureMode() {

            var groupDiv = document.getElementById("id_div_group_<%=OSACopy.PropName.EXPOSURE_MODE.value%>");
            var buttons = groupDiv.getElementsByTagName('input');
            for (i = 0; i < buttons.length; i++) {
                var retentionObject = document.getElementById('hiddenPropValue_' + buttons[i].id);
                buttons[i].style.backgroundColor = "";
                if (null != retentionObject && retentionObject.value == "<%=defExposureMode%>") {
                    HiddenExposureMode.value = buttons[i].id;
                }
            }
        }
    </script>
</head>
<body>
    <form id="exposure_form" runat="server" method="post" action="exposureSetting.aspx">
    <asp:HiddenField ID="HiddenExposureLevel" runat="server" />
    <asp:HiddenField ID="HiddenExposureMode" runat="server" />
    <br />
    <div id="header" style="left: 0px; position: absolute; top: 0px;">
        <asp:Label ID="id_Label_Title" runat="server"></asp:Label>
    </div>
    <div style="position: absolute; top: 0%">
        <asp:Button ID="id_btn_OK" runat="server" Text="OK" OnClick="Btn_OK_Click" CssClass="btn_header" />
    </div>
    <br />
    <br />
    <input type="button" runat="server" id="id_exposure_auto" onclick="SelectAutoButton()" />
    <hr id="id_hr" />
    <div>
        <div id="id_div_exp_left">
            <input type="button" runat="server" id="id_exposure_manual" onclick="SelectManualButton()" />
            <asp:DropDownList ID="id_level_list" runat="server" />
        </div>
        <div id="id_div_exp_right" runat="server" />
    </div>
    </form>
</body>
</html>
