<%--
// ==============================================================================
// File Name           : Log View Report Result.
// Description         : Log View  Report Result Screen for SimpleEA
// Author(s)           : ESE)Zhou Miao
// Date created        : 2010.12.14
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
<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleDetailPage.master" AutoEventWireup="true" CodeFile="LogViewReportResult.aspx.cs" Inherits="Report_LogViewReportResult" Title="Untitled Page" %>
<%@ MasterType TypeName="Masterpage_SimpleDetailPage" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" Runat="Server">
    <asp:Table ID="tblDetail" runat="server" Width="98%"  cellpadding="1" cellspacing="1" BackColor="#019cff" CssClass="Normalfont_Blacknormal">
        <asp:TableHeaderRow ID="tblHRBigTitle" runat="server" CssClass="Table_Top_bg">
            <asp:TableHeaderCell  Width="150px" Wrap="false" height="50px">����ʱ��</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="140px" Wrap="false" >�û���</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="150px" Wrap="false" >�û�����</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false" >MFP�ͺ�</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false" >MFP���к�</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false">MFP IP��ַ</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="150px" Wrap="false">��������</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="200px" Wrap="false" >�ļ���</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="80px"  Wrap="false" >ɫ��</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="80px"  Wrap="false" >ֽ��</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="50px"  Wrap="false" >����</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="50px"  Wrap="false" >����</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="50px"  Wrap="false" >����</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px"  Wrap="false" >�ܼ�</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false">��/˫��ģʽ</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false" >״̬</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="200px" Wrap="false" >�쳣��Ϣ</asp:TableHeaderCell>
         </asp:TableHeaderRow>        
    </asp:Table>
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" Runat="Server">
    <asp:Button ID="btnCSVOutPut" runat="server" Text="CSV�ļ�����" OnClick="btnCSVOutPut_Click" 
        CssClass="Login_Button_bg" />
</asp:Content>
