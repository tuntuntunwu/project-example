<%--
// ==============================================================================
// File Name           : FatalError.aspx
// Description         : FatalError.aspx
// Author(s)           : Ji Jianxiong
//                       Build No: 1.0.3.2: UI Update.
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FatalError.aspx.cs" Inherits="FatalError" ContentType="text/xml" UICulture="Auto"  ResponseEncoding="GB2312"%>
<%
    string detail = string.Empty;
    detail = Request.Params["msg"];
    
    string[] details = detail.Split('|');
    
    
%>
<html>
	<body>
		<form class="osa_message" title="Fatal Error" action="FatalError.aspx">
			<p>ERROR OCCURRED IN THE APPLICATION</p>
			<% for( int i =0 ; i < details.Length ;i++) { %>
			<p><%= details[i]%></p>
			<% } %>
		</form>
	</body>
</html>