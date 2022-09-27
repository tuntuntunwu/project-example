<%--
// ==============================================================================
// File Name           : GroupJobReportResult.aspx
// Description         : GroupJobReportResult Page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.07.06
// Date updated        : 2010.09.07
//                       Build No: 1.0.3.2: UI Update.
//                       2010.11.24
//                       Build No: 1.1 Update.
//                       2010.12.27
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

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleDetailPage.master" AutoEventWireup="true" CodeFile="GroupJobReportResult.aspx.cs" Inherits="Report_GroupJobReportResult" Title="Untitled Page" %>
<%@ MasterType TypeName="Masterpage_SimpleDetailPage" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" Runat="Server">
    <asp:Table ID="tblDetail" runat="server" cellpadding="1" cellspacing="1" BackColor="#019cff" CssClass="Normalfont_Blacknormal">
        <asp:TableHeaderRow ID="tblHRBigTitle" runat="server" CssClass="Table_Top_bg">
            <asp:TableHeaderCell RowSpan="2" Width="280px" Wrap="false" Height="55px">�û�����</asp:TableHeaderCell>
            <asp:TableHeaderCell RowSpan="2" Width="140px" Wrap="false" Height="55px">��ʹ����</asp:TableHeaderCell>
            <asp:TableHeaderCell RowSpan="2" Width="140px" Wrap="false" Height="55px">�ڰ����ϼ�</asp:TableHeaderCell>
            <asp:TableHeaderCell RowSpan="2" Width="140px" Wrap="false" Height="55px">��ɫ���ϼ�</asp:TableHeaderCell>
         </asp:TableHeaderRow>
        <asp:TableHeaderRow ID="tblHRSmallTitle" runat="server"  CssClass="Table_Top_bg">
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" Runat="Server">
    <asp:Button ID="btnCSVOutPut" runat="server" Text="CSV�ļ�����" OnClick="btnCSVOutPut_Click" 
        CssClass="Login_Button_bg" />
</asp:Content>

