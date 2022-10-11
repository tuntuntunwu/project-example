
<%--
// ==============================================================================
// File Name           : ContentBackupSetting.aspx
// Description         : ContentBackupSetting
// Author(s)           : Tu Jinyu
// Date created        : 2017.06.05
// Date updated        : 2017.06.05
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true" 
CodeFile="ContentBackupSetting.aspx.cs" Inherits="Settings_ContentBackupSetting" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>
    <asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
         <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link" href="../Settings/Settings.aspx">系统设置</a></span> 
        /<span id="Span0"><a id="A0" runat="server" class="small_link" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
        /<span id="Span1"><a id="A1" runat="server" class="small_link" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
        /<span id="Span2"><a id="A2" runat="server" class="small_link" href="../Settings/ImportUserInfor.aspx">用户信息导入</a></span>
        /<span id="Span3"><a id="A3" runat="server" class="small_link" href="../Settings/ServerIPSetting.aspx">ServerIP设置</a></span>
        /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link_select" href="../Settings/ContentBackupSetting.aspx">内容留底设置</a></span>
        /<span id="Span10"><a id="A10" runat="server" class="small_link" href="../Settings/LDAPSetting.aspx">LDAP认证设置</a></span>
       /<span id="Span11"><a id="A11" runat="server" class="small_link" href="../Settings/DBAuthSetting.aspx">第三方认证设置</a></span>
     /<span id="Span9"><a id="A9" runat="server" class="small_link" href="../Settings/LDAPSynchronization.aspx">LDAP同步</a></span>
    /<span id="Span5"><a id="A13" runat="server" class="small_link" href="../Settings/SectionSetting.aspx">集团设置</a></span>
    /<span id="Span6"><a id="A14" runat="server" class="small_link" href="../Settings/SectionGroupSetting.aspx">集团组设置</a></span>

    </asp:Content>
<asp:Content ID="ContentUserEdit" ContentPlaceHolderID="cphbody" runat="Server">
    <script type="text/javascript" language="javascript">
        loadScript("../js/Collaped_expand.js");
        loadScript("../js/Min_resize.js");
        loadScript("../JavaScript/jquery.custom_radio_checkbox.js");

        window.onload = function () {
            var doc = document;
            if (doc.getElementById("Scroller-Set")) {
                loadScriptScroller(function () {
                    var scroller = null;
                    var scrollbar = null;
                    scroller = new jsScroller(doc.getElementById("Scroller-Set"), 400, 448);
                    scrollbar = new jsScrollbar(doc.getElementById("Scrollbar-Container"), scroller, false);
                    scroller = null;
                    scrollbar = null;
                });
            }
        }
        $(document).ready(function () {
            $(".radio").dgStyle();
            grid_resize();
        });

        function noticeUser() {
            AlertMessage("设置完成后，请到SimpleEAFollowME文件夹重启服务。");
        }
    </script>

    <!-- Main table-->
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" valign="top" class="Table_header_left" style="width: 1%">
            </td>
            <td align="left" valign="middle" class="bottom_HR_line" style="height: 2px; width: 98%">
            </td>
            <td align="left" valign="top" class="Table_header_Right" style="width: 1%">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" class="Left_VRLine">
                &nbsp;
            </td>
            <td align="left" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="top" style="height: 448px; width: 97%" class="Scroll_td">
                            <div class="Container">
                                <div id="Scroller-Set" class="Scroll_div">
                                    <div class="Scroller-Container">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                                    &nbsp;留底文件设置
                                                </td>
                                            </tr>
                                      
                                            <tr>
                                                <td colspan="5" class="HR_Line" style="height: 2px">
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="HR_Line" colspan="5" style="height: 2px">
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    文件存放地址
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        <asp:TextBox ID="txtFileLocation" runat="server" Width="200px" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFileLocation" runat="server"
                                                            Display="Dynamic" ErrorMessage ="本项不能为空，请重新填写。">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtFileLocation"
                                                            Display="Dynamic" runat="server" ValidationExpression="[a-zA-Z]:\\((\w)*\\)*" ErrorMessage="存在非法字符，请依照给出的格式填写。">
                                                        </asp:RegularExpressionValidator>
                                                        <asp:Label ID="Label2" runat="server" Text="eg:D:\SimpleEA\"></asp:Label>
                                                          <asp:Button ID="Button0" runat="server" Text="更改" CssClass="Button_JobReport" OnClick="btnModify_Click" />
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                          
                                            <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                                    &nbsp;留底文件清理设置
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    留底文件删除
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                 
                                <!-- calenda -->
                            <asp:TableRow >
            <asp:TableCell ColumnSpan="10" CssClass="Light_GrayFont">
                <asp:Table ID="Table1" runat="server" Width="100%">
                    <asp:TableRow CssClass="Light_GrayFont">
                        <asp:TableHeaderCell CssClass="td_head_time" HorizontalAlign="Left" Width="100px"
                            Wrap="false">&nbsp;时间段</asp:TableHeaderCell>
                        <asp:TableCell VerticalAlign="Top" Width="56px" CssClass="year_width">
                            <div style="z-index: 9; position: absolute; padding-top: 5px">
                                <asp:DropDownList ID="ddlBeginYear" runat="server" CssClass="changeMe" Width="56px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged" ValidationGroup="None">
                                </asp:DropDownList>
                            </div>
                        </asp:TableCell>
                        <asp:TableCell Width="10px">年</asp:TableCell>
                        <asp:TableCell VerticalAlign="Top" CssClass="day_width">
                            <div style="z-index: 8; position: absolute; padding-top: 5px">
                                <asp:DropDownList ID="ddlBeginMonth" runat="server" CssClass="changeMe" Width="45px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged" ValidationGroup="None">
                                </asp:DropDownList>
                            </div>
                        </asp:TableCell>
                        <asp:TableCell Width="10px">月</asp:TableCell>
                        <asp:TableCell VerticalAlign="Top" CssClass="day_width">
                            <div style="z-index: 7; position: absolute; padding-top: 5px">
                                <asp:DropDownList ID="ddlBeginDay" runat="server" CssClass="changeMe" Width="45px"
                                    dateday="dateday" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"
                                    ValidationGroup="None">
                                </asp:DropDownList>
                            </div>
                        </asp:TableCell>
                        <asp:TableCell Width="10px">日</asp:TableCell>
                        <asp:TableCell VerticalAlign="Top" CssClass="day_width">
                            <div style="z-index: 6; position: absolute; padding-top: 5px">
                                <asp:DropDownList ID="ddlBeginHour" runat="server" CssClass="changeMe" Width="45px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged" ValidationGroup="None">
                                </asp:DropDownList>
                            </div>
                        </asp:TableCell>
                        <asp:TableCell Width="70px">时00分～</asp:TableCell>
                        <asp:TableCell ColumnSpan="3">
                            <asp:TextBox ID="StartDateTime" runat="server" CssClass="viewDisplay"></asp:TextBox>
                            <asp:CompareValidator ID="valDateTime" ControlToValidate="StartDateTime" runat="server"
                                Operator="LessThan" Display="Dynamic" ControlToCompare="EndDateTime"></asp:CompareValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow CssClass="Light_GrayFont">
                        <asp:TableHeaderCell Height="29px"></asp:TableHeaderCell>
                        <asp:TableCell VerticalAlign="Top">
                            <div style="z-index: 5; position: absolute; padding-top: 5px">
                                <asp:DropDownList ID="ddlEndYear" runat="server" CssClass="changeMe" Width="56px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged" ValidationGroup="None">
                                </asp:DropDownList>
                            </div>
                        </asp:TableCell>
                        <asp:TableCell>年</asp:TableCell>
                        <asp:TableCell VerticalAlign="Top">
                            <div style="z-index: 4; position: absolute; padding-top: 5px">
                                <asp:DropDownList ID="ddlEndMonth" runat="server" CssClass="changeMe" Width="45px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged" ValidationGroup="None">
                                </asp:DropDownList>
                            </div>
                        </asp:TableCell>
                        <asp:TableCell>月</asp:TableCell>
                        <asp:TableCell VerticalAlign="Top">
                            <div style="z-index: 3; position: absolute; padding-top: 5px">
                                <asp:DropDownList ID="ddlEndDay" runat="server" CssClass="changeMe" Width="45px"
                                    dateday="dateday" AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged"
                                    ValidationGroup="None">
                                </asp:DropDownList>
                            </div>
                        </asp:TableCell>
                        <asp:TableCell>日</asp:TableCell>
                        <asp:TableCell VerticalAlign="Top">
                            <div style="z-index: 2; position: absolute; padding-top: 5px">
                                <asp:DropDownList ID="ddlEndHour" runat="server" CssClass="changeMe" Width="45px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_SelectedIndexChanged" ValidationGroup="None">
                                </asp:DropDownList>
                            </div>
                        </asp:TableCell>
                        <asp:TableCell>时59分</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left" Width="200px">
                            <asp:TextBox ID="EndDateTime" runat="server" CssClass="viewDisplay"></asp:TextBox>
                            <asp:ImageButton ID="btnPeriodPre" runat="server" OnClick="btnPeriodPre_Click" ImageUrl="../Images/Arrow_Pre.gif"
                                Width="7px" Height="13px"  ValidationGroup="None"></asp:ImageButton>&nbsp;&nbsp;&nbsp;<asp:Image
                                    ID="Image1" Width="24px" Height="24px" runat="server" ImageUrl="~/Images/Calendar.gif" />&nbsp;&nbsp;&nbsp;<asp:ImageButton
                                        ID="btnPeriodNext" runat="server" OnClick="btnPeriodNext_Click" ImageUrl="../Images/Arrow_Next.gif"
                                        Width="7px" Height="13px"  ValidationGroup="None"></asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;
                           
                        </asp:TableCell>
                        <asp:TableCell>   
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>

                              <!---calaenda -->
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px; height:25px;">
                                                        <asp:Button ID="btnDeleteCopy" runat="server"
                                                            Text="删除" CssClass="Button_JobReport" OnClick="btn_DeleteCopy" />
                                                    </div>
                                                </td>
                                               
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td align="left" valign="top" class="VR_line ScrollbarCSS" style="width: 34px; min-width: 34px">
                            <div id="Scrollbar-Container" style="visibility: visible;">
                                <img alt="UP" class="Scrollbar-Up" src="../js/UP_arrow.gif" title="UP" />
                                <img alt="Down" class="Scrollbar-Down-Set" src="../js/Down_arrow.gif" title="Down" />
                                <div class="Scrollbar-Track-Set">
                                    <img alt="Handle" class="Scrollbar-Handle" src="../js/Scrollbar_handle.gif" title="Handle" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" valign="top" class="VR_line">
            </td>
        </tr>
        <tr>
            <td style="height: 2px" align="right" valign="bottom" class="Bottom_login_bg_new">
            </td>
            <td align="left" valign="top" class="HR_line_New">
            </td>
            <td align="right" valign="bottom" class="Bottom_right_bg_Newupdated">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidUserId" runat="server" />
    <!-- end of the Main table-->
</asp:Content>
<asp:Content ID="contentfoot" ContentPlaceHolderID="cphfoot" runat="server">

    &nbsp;&nbsp;&nbsp;&nbsp;
    
    <script type="text/javascript" src="../js/Min_radio.js"></script>
    <script type="text/javascript" src="../js/Min_checkbox.js"></script>
    <script src="../js/commonfoot.js" type="text/javascript">
       
    </script>
</asp:Content>

