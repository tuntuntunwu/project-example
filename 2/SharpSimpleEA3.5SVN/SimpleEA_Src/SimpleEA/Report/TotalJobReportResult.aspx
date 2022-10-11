<%--
// ==============================================================================
// File Name           : TotalJobReportResult.aspx
// Description         : TotalJobReportResult Page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.06.24
// Date updated        : 2010.09.01
//                       Build No: 1.0.3.2: UI Update.
//                       2010.12.27
//                       Html Standerd Modify
//                       2010.12.28
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
    CodeFile="TotalJobReportResult.aspx.cs" Inherits="Report_TotalJobReportResult"
    Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleDetailPage" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" runat="Server">
    <asp:Table ID="tblDetail" runat="server" CellPadding="1" CellSpacing="1" BackColor="#019cff"
        CssClass="Normalfont_Blacknormal">
        <asp:TableHeaderRow ID="tblHRBigTitle" runat="server" CssClass="Table_Top_bg">
            <asp:TableHeaderCell RowSpan="2" Height="55px" Width="200px">
                &nbsp;
            </asp:TableHeaderCell>
            <asp:TableHeaderCell RowSpan="2" Width="140px" Wrap="false">总使用量</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow ID="tblHRSmallTitle" runat="server" CssClass="Table_Top_bg">
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" runat="Server">
    <asp:Button ID="btnCSVOutPut" runat="server" Text="CSV文件导出" OnClick="btnCSVOutPut_Click"
        CssClass="Login_Button_bg" />
</asp:Content>
