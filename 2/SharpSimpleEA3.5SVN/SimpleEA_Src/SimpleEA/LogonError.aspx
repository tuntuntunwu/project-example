<%--
// ==============================================================================
// File Name           : ErrMFP.aspx
// Description         : ErrMFP.aspx
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


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogonError.aspx.cs" Inherits="LogonError"  UICulture="Auto"  ResponseEncoding="GB2312"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta name="Browser" content="NetFront" />
    <title>Logon Error</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="ErrorMsg" runat="server" Text="Label" > </asp:Label>
        <br>
        <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="btnOK_Click"  > </asp:Button>
    </form>
</body>
</html>
