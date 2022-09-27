<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SourceSetting00.aspx.cs" Inherits="SourceSetting00" UICulture="Auto"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <asp:Literal runat="server" id="activeStyleSheet"></asp:Literal>
</head>    
<body>
    <form id="input_tray_form" runat="server" method="post">
        <asp:HiddenField ID = "HiddenInputTray" runat="server" />
	    <div id="header" style="left: 0px; position: absolute; top: 0px;">
    	    <asp:Label ID="id_Label_Title" runat="server"></asp:Label>
	    </div>
	    <div style="position: absolute; top: 0%" >
            <asp:Button ID="id_btn_OK" runat="server" Text="OK" OnClick="Btn_OK_Click"  CssClass="btn_header"/>
        </div>
        <br /><br /><br /><br />
        <div id="id_div_InputTray_left" runat="server">
        </div>
    </form> 
</body>
</html>
