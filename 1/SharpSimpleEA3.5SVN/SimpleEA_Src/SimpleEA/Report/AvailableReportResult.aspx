<%--
// ==============================================================================
// File Name           : Available Report Result.
// Description         : Available Report Result Screen for SimpleEA
// Author(s)           : Ji Jianxiong
// Date created        : 2010.07.13
// Date updated        : 2010.09.10
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
    CodeFile="AvailableReportResult.aspx.cs" Inherits="Report_AvailableReportResult"
    Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleDetailPage" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" runat="Server">
    <script language="javascript" type="text/javascript">
        // do while page load. and size reize
        $(document).ready(function () {
            $(window).css("overflow-X", "hidden");
            $(window).resize();
            document.body.scroll = "no";
            $("body").css("overflow-X", "hidden");
            $("body").css("width", "1014px");
            $("body").resize();
            $("input eq(0)").focus();
            $("button eq(0)").focus();
            $("td eq(0)").focus();
            $("body eq(0)").focus();
        });
      
        function btnAddValue_click(btn, UserID) {
            $("#ctl00_cphfoot_hidTxtBxUserId").val(UserID);

            var row = $(btn).parents("tr:first");
            var gv = $(row).find("input[name='txtAddMoney_" + UserID + "']:first").val();
            var cv = $(row).find("input[name='txtAddColorMoney_" + UserID + "']:first").val();
            $("#ctl00_cphfoot_hidTxtBxGrayValue").val(gv);
            $("#ctl00_cphfoot_hidTxtBxColorValue").val(cv);
            if (gv == "") {
                error_alert("请输入追加余额", "ERROR");

            } else if (isNaN(gv)) {
                error_alert("请输入正确的追加余额", "ERROR");

            } else if (cv == "") {
                error_alert("请输入追加余额", "ERROR");

            } else if (isNaN(cv)) {
                error_alert("请输入正确的追加余额", "ERROR");

            } else if (parseInt(cv, 10) > parseInt(gv, 10)) {
                error_alert("彩色余额的追加余额 不能大于 余额的追加余额", "ERROR");

            } else if (parseInt(cv, 10)==0 && parseInt(gv, 10)==0) {
                error_alert("请输入追加余额（>0）", "ERROR");

            } else {
                $("#ctl00_cphfoot_hidBtnAddMoney").click();
            }
        }
        function onAddValueKeyUp(txtbx) {
            if (!$(txtbx).attr("lastValue")) {
                $(txtbx).attr("lastValue", "");
            }
            if ($(txtbx).val() == "") {
                $(txtbx).attr("lastValue", "");

            }else if (/^((-?\d+(\.\d{1,2})?)|(-?\d+\.)|(-))$/.test($(txtbx).val())) {
                $(txtbx).attr("lastValue", $(txtbx).val());

            } else {
                $(txtbx).val($(txtbx).attr("lastValue"));
            }
        }
    </script>
    <asp:Table ID="tblDetail" runat="server" Width="1014px" CellPadding="1" CellSpacing="1"
        BackColor="#019cff" CssClass="Normalfont_Blacknormal">
        <asp:TableHeaderRow ID="tblHRBigTitle" runat="server" CssClass="Table_Top_bg">
            <asp:TableHeaderCell RowSpan="2" Width="340px" Wrap="false" Height="55px">用户名</asp:TableHeaderCell>
            <asp:TableHeaderCell RowSpan="2" Width="340px" Wrap="false" Height="55px">用户组名</asp:TableHeaderCell>
            
            <asp:TableHeaderCell ColumnSpan="2" Width="200px" Wrap="false" Height="23px">余额</asp:TableHeaderCell>
            <asp:TableHeaderCell ColumnSpan="2" Width="300px" Wrap="false" Height="23px">彩色余额</asp:TableHeaderCell>

            <asp:TableHeaderCell RowSpan="2" Width="100px" Wrap="false" Height="23px">追加</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow ID="tblHRSmallTitle" runat="server" CssClass="Table_Top_bg">
            <asp:TableHeaderCell Width="150px" Wrap="false" Height="23px">余额</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="150px" Wrap="false" Height="23px">追加余额</asp:TableHeaderCell>

            <asp:TableHeaderCell Width="150px" Wrap="false" Height="23px">余额</asp:TableHeaderCell>
            <asp:TableHeaderCell Width="150px" Wrap="false" Height="23px">追加余额</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" runat="Server">
    <asp:Button ID="btnCSVOutPut" runat="server" Text="CSV文件导出" OnClick="btnCSVOutPut_Click"
        CssClass="Login_Button_bg" />
    <div Style="display:none;">
        <asp:TextBox ID="hidTxtBxUserId" runat="server"></asp:TextBox>
        <asp:TextBox ID="hidTxtBxGrayValue" runat="server"></asp:TextBox>
        <asp:TextBox ID="hidTxtBxColorValue" runat="server"></asp:TextBox>
        <asp:Button ID="hidBtnAddMoney" runat="server" Text="hidBtnAddMoney" OnClick="hidBtnAddMoney_Click"/>
    </div>
</asp:Content>
