<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FatalError.aspx.cs" Inherits="FatalError" UICulture="Auto"%>
<html xmlns="http://www.w3.org/1999/xhtml" > 
    <head>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <asp:Literal runat="server" id="activeStyleSheet"></asp:Literal>
        <script type="text/javascript">

            function ShowScreen() {
                var target = document.createElement('script');
                target.charset = 'utf-8';
                // Callback function name : ShowScreenCallback
                // noCache : A unique request key is generated for the cash measures
                target.src = "<%=requestShowScreenURL%>" + "&noCache=" + new Date().getTime();
                target.onload = function() { document.getElementsByTagName("head")[0].removeChild(this); };
                document.getElementsByTagName("head")[0].appendChild(target);
            }

            function ShowScreenCallback(_retParam) {
            }
        </script>

    </head>
    <body>
		<p><%= detail %></p>
        <input id="id_exit" type="button" value="OK" class="btn_bottom" onclick="ShowScreen();"/>
    </body>
</html>
