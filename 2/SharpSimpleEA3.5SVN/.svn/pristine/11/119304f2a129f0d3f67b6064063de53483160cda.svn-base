<%@ Page Language="C#" ContentType="text/html" CodeFile="SourceSetting02.aspx.cs"
    Inherits="SourceSetting" UICulture="Auto" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <asp:Literal runat="server" ID="activeStyleSheet"></asp:Literal>
    <script type="text/javascript" src="JavaScript/common.js"></script>
    <script type="text/javascript">

        function GetChangeRatioVal(value) {
            var ratio = document.getElementById('id_text_Ratio');
            var curValue = parseInt(ratio.value);
            var min = parseInt("<%=min%>");
            var max = parseInt("<%=max%>");
            value = parseInt(value);

            if (min <= value + curValue && value + curValue <= max) {
                curValue = curValue + value;
            } else {
                if (min > value + curValue) {
                    curValue = min;
                } else {
                    curValue = max;
                }
            }
            ratio.value = curValue;
        }

        function CheckRatio() {
            var result = true;
            var ratio = document.getElementById("id_text_Ratio");

            if (CheckIsNumber(ratio.value)) {
                var value = parseInt(ratio.value, 10);
                if (value < parseInt("<%=min%>") || parseInt("<%=max%>") < value) {
                    result = false;
                }
                else {
                    ratio.value = value;
                }
            }
            else {
                result = false;
            }
            if (!result) {
                alert("<%=errMsg %>");
                ratio.value = parseInt("<%=strRatioVal%>", 10);
                ratio.focus();
            }

            return result;
        }

    </script>
    <style type="text/css">
        .btn_sourceSetting
        {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="src_form" runat="server">
    <asp:HiddenField ID="HiddenDuplexMode" runat="server" />
    <br />
    <br />
    <div id="header" style="left: 0px; position: absolute; top: 0px;">
        <asp:Label ID="id_Label_Title" runat="server"></asp:Label>
    </div>
    <div style="position: absolute; top: 0%">
        <asp:Button ID="id_btn_OK" runat="server" Text="OK" OnClientClick="return CheckRatio();"
            OnClick="OkButton_Click" CssClass="btn_header" />
    </div>
    <br />
    <br />
    <table style="width: 80%; top: 10px; position: relative;" border="0">
        <tr>
            <td style="width: 45%" align="left">
                <asp:Label ID="id_Label_Ratio" runat="server" Text="<%$ Resources:OSAStrings, COPY_RATIO %>"></asp:Label>
                <asp:TextBox ID="id_text_Ratio" runat="server" istyle="4" MaxLength="3" onblur="CheckRatio();">0</asp:TextBox>
                <asp:Label ID="id_Label_Range" runat="server"></asp:Label>
                <input type="button" id="id_ratio_down" value="<%= Resources.OSAStrings.BTN_DOWN %>"
                    onclick="GetChangeRatioVal('<%=DOWN_VALUE%>');" />
                <input type="button" id="id_ratio_up" value="<%= Resources.OSAStrings.BTN_UP %>"
                    onclick="GetChangeRatioVal('<%=UP_VALUE%>');" />
            </td>
        </tr>
    </table>
    <br />
    <hr id="id_hr" />
    <div id="id_div_srcset_left" runat="server" style="margin-top: 5px">
    </div>
    </form>
</body>
</html>
