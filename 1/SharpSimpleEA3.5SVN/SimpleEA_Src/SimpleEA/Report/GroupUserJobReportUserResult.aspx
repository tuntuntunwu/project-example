<%--
// ==============================================================================
// File Name           : UserJobReportResult.aspx
// Description         : UserJobReportResult Page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.07.02
// Date updated        : 2010.09.07
//                       Build No: 1.0.3.2: UI Update.
//                       2010.11.24
//                       Build No: 1.1 Update.
//                       2010.12.27
//                       Html Standerd Modify
//                       2010.12.28
//                       Html Standerd Modify
//                       2011.03.22
//                       Html Standerd Modify
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleDetailPage.master" AutoEventWireup="true"
    CodeFile="GroupUserJobReportUserResult.aspx.cs" Inherits="Report_GroupUserJobReportUserResult"
    Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleDetailPage" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" runat="Server">
    <asp:Table ID="tblDetail" runat="server" CellPadding="1" CellSpacing="1" 
         BackColor="#019cff" CssClass="Normalfont_Blacknormal">
        <asp:TableHeaderRow ID="tblHRBigTitle" runat="server" CssClass="Table_Top_bg">
            <asp:TableHeaderCell Width="140px" Wrap="false" Height="55px">用户名</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="140px" Wrap="false">彩色复印合计</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="140px" Wrap="false">黑白复印合计</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="140px" Wrap="false">彩色打印合计</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="140px" Wrap="false">黑白打印合计</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="140px" Wrap="false">扫描使用合计</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="140px" Wrap="false">传真使用合计</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="140px" Wrap="false">其它使用合计</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="140px" Wrap="false">总使用合计</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow ID="tblHRSmallTitle" runat="server" CssClass="Table_Top_bg">
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" runat="Server">
    <asp:Button ID="btnCSVOutPut" runat="server" Text="CSV文件导出" OnClick="btnCSVOutPut_Click"
        CssClass="Login_Button_bg" />
</asp:Content>
