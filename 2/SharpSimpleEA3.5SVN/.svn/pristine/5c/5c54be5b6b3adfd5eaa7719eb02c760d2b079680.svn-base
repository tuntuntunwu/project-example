<%@ Page Language="C#" ContentType="text/html" CodeFile="SourceSetting01.aspx.cs" Inherits="SourceSetting01" UICulture="Auto"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
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

                if (min <= value + curValue && value + curValue <= max)
                {
                    curValue = curValue + value;
                }else {
                    if ( min > value + curValue ) {
                        curValue = min;
                    }else {
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
                    if (value < parseInt("<%=min%>") || parseInt("<%=max%>") < value)
                    {
                        result = false;
                    }
                    else 
                    {
                        ratio.value = value;
                    }
                }
                else
                {
                    result = false;
                }
                if(!result) {
                    alert( "<%=errMsg %>" );
                    ratio.value = parseInt("<%=strRatioVal%>", 10);
                    ratio.focus();
                }
                
                return result;
            }

        </script>
        <style type="text/css">
            .btn_sourceSetting {
                margin-bottom: 0px;
            }
        </style>
    </head>
    <body>
        <form id="src_form" runat="server">
            <asp:HiddenField ID = "HiddenDuplexMode" runat="server" />
	        <div id="header" style="left: 0px; position: absolute; top: 0px;">
    	        <asp:Label ID="id_Label_Title" runat="server"></asp:Label>
	        </div>
            <div style="position: absolute; top: 0%" >
                <asp:Button ID="id_btn_OK" runat="server" Text="OK" OnClientClick="return CheckRatio();"  OnClick="OkButton_Click"  CssClass="btn_header"/>
            </div>
            <br /><br /><br />
            <table style="width: 80%; top: 10px; position: relative;" border="0">
                <tr>
                    <td style="WIDTH: 45%" align="left">
                    </td>
                </tr>
            </table>
            <table style="top: 10px; position: relative;margin-bottom:20px">
            </table>
            <div id="id_div_srcset_left" runat="server" style="margin-top:5px">
            </div>
        </form>
    </body>
</html>
