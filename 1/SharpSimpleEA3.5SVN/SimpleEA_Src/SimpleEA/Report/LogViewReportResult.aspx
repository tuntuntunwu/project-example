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
            <asp:TableHeaderCell  Width="150px" Wrap="false" height="50px">操作时间</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="140px" Wrap="false" >用户名</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="150px" Wrap="false" >用户组名</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false" >MFP型号</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false" >MFP序列号</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false">MFP IP地址</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="150px" Wrap="false">操作类型</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="200px" Wrap="false" >文件名</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="80px"  Wrap="false" >色彩</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="80px"  Wrap="false" >纸张</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="50px"  Wrap="false" >面数</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="50px"  Wrap="false" >张数</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="50px"  Wrap="false" >份数</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px"  Wrap="false" >总计</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false">单/双面模式</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="100px" Wrap="false" >状态</asp:TableHeaderCell>
            <asp:TableHeaderCell  Width="200px" Wrap="false" >异常信息</asp:TableHeaderCell>
         </asp:TableHeaderRow>        
    </asp:Table>
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" Runat="Server">
    <asp:Button ID="btnCSVOutPut" runat="server" Text="CSV文件导出" OnClick="btnCSVOutPut_Click" 
        CssClass="Login_Button_bg" />
</asp:Content>
