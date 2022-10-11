<%@ Page Language="C#" ContentType="text/html" CodeFile="Default.aspx.cs" Inherits="_Default"
    UICulture="Auto" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <script type="text/javascript" src="JavaScript/common.js"></script>
    <script type="text/javascript" src="JavaScript/json2.js"></script>
    <script type="text/javascript">

        window.onload = WindowLoad;

        function WindowLoad() {
            CallGetDeviceSettings();
        }

        function CallGetDeviceSettings() {
            var target = document.createElement('script');
            target.charset = 'utf-8';
            // Callback function name : GetDeviceSettingsCallback
            target.src = "<%=requestGetDeviceSettingsURL%>";
            target.onload = function () { document.getElementsByTagName("head")[0].removeChild(this); };
            document.getElementsByTagName("head")[0].appendChild(target);
        }

        function GetDeviceSettingsCallback(_retParam) {
            if (CheckJsonpResponseError(_retParam, "<%=GET_RESOURCES_STRING_URL%>", "<%=FATAL_ERROR_URL%>", "<%=AJAX_TIME_OUT%>")) {
                if ("enabled" != _retParam["result"]["GetDeviceSettingsResponse"]["device-settings"]["osa-info"]["copy"]) {
                    document.location = "FatalError.aspx?code=INITIALIZATION";
                }
                else {
                    var OSAVersion = document.getElementById('OSAVersion');
                    function NullThenDisabled(OSAVersion) {
                        if (OSAVersion == null) {
                            return "disabled";
                        }
                        else {
                            return OSAVersion
                        }
                    }
                    OSAVersion.value = NullThenDisabled(_retParam["result"]["GetDeviceSettingsResponse"]["device-info"]["osaversion"]);
                    var target = document.createElement('script');
                    target.charset = 'utf-8';
                    // Callback function name : GetJobSettableElementsCallback
                    target.src = "<%=requestGetJobSettableElementsURL%>";
                    target.onload = function () { document.getElementsByTagName("head")[0].removeChild(this); };
                    document.getElementsByTagName("head")[0].appendChild(target);
                }
            }
        }

        function GetJobSettableElementsCallback(_retParam) {
            if (CheckJsonpResponseError(_retParam, "<%=GET_RESOURCES_STRING_URL%>", "<%=FATAL_ERROR_URL%>", "<%=AJAX_TIME_OUT%>")) {
                var JobSettableElements = document.getElementById('JobSettableElements');
                JobSettableElements.value = JSON.stringify(_retParam["result"]["GetJobSettableElementsResponse"], null, " "); ;
                document.forms[0].submit();
            }
        }

    </script>
</head>
<body>
    <form id="exposure_form" runat="server" method="post" action="Default.aspx">
    <asp:HiddenField ID="JobSettableElements" runat="server" />
    <asp:HiddenField ID="OSAVersion" runat="server" Value="disabled" />
    </form>
</body>
</html>
