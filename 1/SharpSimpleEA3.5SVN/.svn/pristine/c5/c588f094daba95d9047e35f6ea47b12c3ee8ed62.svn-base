<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobStatus.aspx.cs" Inherits="JobStatus" UICulture="Auto"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <asp:Literal runat="server" id="activeStyleSheet"></asp:Literal>
    <script type="text/javascript" src="JavaScript/common.js"></script> 
    <script type="text/javascript">
        window.onload = WindowLoad;
        var Status = "STARTED";

        function WindowLoad() {
            CallGetJobStatus();
        }

        function CallGetJobStatus() {
            if ("FINISHED" != Status && "CANCELED" != Status && "ERROR" != Status && "SUSPENDED" != Status) {

                var element = document.getElementById('JsonScript');
                if (null != element) {
                    document.getElementsByTagName("head")[0].removeChild(element);
                }
            
                var target = document.createElement('script');
                target.id = "JsonScript";
                target.charset = 'utf-8';
                // Callback function name : GetJobStatusCallback
                // noCache : A unique request key is generated for the cash measures
                target.src = "<%=requestGetJobStatusURL%>" + "&noCache=" + new Date().getTime();
                target.onload = function() {document.getElementsByTagName("head")[0].removeChild(this); };
                document.getElementsByTagName("head")[0].appendChild(target);
            }
        }

        function GetJobStatusCallback(_retParam) {
            if (CheckJsonpResponseError(_retParam, "<%=GET_RESOURCES_STRING_URL%>", "<%=FATAL_ERROR_URL%>", "<%=AJAX_TIME_OUT%>")) {
                if (Status != _retParam["result"]["GetJobStatusResponse"]["status"]) {
                    Status = _retParam["result"]["GetJobStatusResponse"]["status"];
                    ChangingStatusDisplay(Status);
                }
                setTimeout(CallGetJobStatus, 1000);
            }
        }

        function OnClickCancelButton() {
            var target = document.createElement('script');
            target.charset = 'utf-8';
            // Callback function name : CancelJobCallback
            target.src = "<%=requestCancelJobURL%>";
            target.onload = function() { document.getElementsByTagName("head")[0].removeChild(this); };
            document.getElementsByTagName("head")[0].appendChild(target);
        }

        function CancelJobCallback(_retParam) {
            CheckJsonpResponseError(_retParam, "<%=GET_RESOURCES_STRING_URL%>", "<%=FATAL_ERROR_URL%>", "<%=AJAX_TIME_OUT%>", "cancel");
        }

        function BackToMainScreen() {
            document.location = "Main.aspx";
        }

        function ChangingStatusDisplay(status) {
            document.getElementById('lbl_status').innerText = status;
            switch (status) {

                case "SUSPENDED":
                    CancelButtonDisplay(false);
                    document.getElementById('lbl_detail').innerHTML = "<%=detailSuspended%>";
                    ImageDisplay(false);
                    break;
                case "STARTED":
                    CancelButtonDisplay(true);
                    document.getElementById('lbl_detail').innerHTML = "<%=detailStarted%>";
                    ImageDisplay(false);
                    break;
                case "QUEUED":
                    CancelButtonDisplay(false);
                    document.getElementById('lbl_detail').innerHTML = "<%=detailQueued%>";
                    ImageDisplay(true);
                    break;
                case "FINISHED":
                    CancelButtonDisplay(false);
                    document.getElementById('lbl_detail').innerHTML = "<%=detailFinished%>";
                    ImageDisplay(false);
                    setTimeout(BackToMainScreen, 5000);
                    break;
                case "CANCELED":
                    CancelButtonDisplay(false);
                    document.getElementById('lbl_detail').innerHTML = "<%=detailCanceled%>";
                    ImageDisplay(false);
                    setTimeout(BackToMainScreen, 5000);
                    break;
                case "ERROR":
                    CancelButtonDisplay(false);
                    document.getElementById('lbl_detail').innerHTML = "<%=detailError%>";
                    ImageDisplay(false);
                    break;
                default:
                    break;
            }
        }

        function ImageDisplay(isVisible) {
            if (isVisible) {
                document.getElementById('id_imgJobStatus').style.visibility = "visible";
            }
            else {
                document.getElementById('id_imgJobStatus').style.visibility = "hidden";
            }
        }

        function CancelButtonDisplay(isCancel) {
            if (isCancel) {
                document.getElementById('id_status_cancel').style.display = "block";
                document.getElementById('id_status_ok').style.display = "none";
            }
            else {
                document.getElementById('id_status_cancel').style.display = "none";
                document.getElementById('id_status_ok').style.display = "block";
            }
        }

    </script>
</head>
	<body>
    	<form id="status_form" runat="server" action="JobStatus.aspx" >
	        <div id="header" style="left: 0px; position: absolute; top: 0px;">
	            <asp:Label ID="id_Label_Title" runat="server"></asp:Label>
	        </div>
	        <div style="position: absolute; top: 0%" >
                <input type="button" runat="server" id="id_status_cancel" value="<%$ Resources:OSAStrings, MAIN_BTN_CANCEL%>" onclick="OnClickCancelButton();" class="btn_header" style="display:block;" />
                <input type="button" runat="server" id="id_status_ok" value="<%$ Resources:OSAStrings, MAIN_BTN_OK%>" onclick="BackToMainScreen();" class="btn_header" style="display:none;" />
            </div>
            <br /><br />
            <div style="width: 98%; height: 50px; text-align: left; padding-left: 10pt;">
                <label id="lbl_status" >STARTED</label>
            </div>	
            <div style="width: 98%; height: 50px; text-align: center; padding-left: 10pt;">
                <label id="lbl_detail"><%=detailStarted%></label>
            </div>	
            <div style="width: 98%; height: 50px; text-align: center; padding-left: 10pt;">
                <img src="Images/copying.gif" id="id_imgJobStatus" alt="Copying" runat="server" />
            </div>
	    </form>
	</body>
</html>
