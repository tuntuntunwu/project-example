
<%--
// ==============================================================================
// File Name           : FollowMESetting.aspx
// Description         : Set Follow ME Print para
// Author(s)           : Wei Changye
// Date created        : 2012.02.29
// Date updated        : 2010.09.07
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true" 
CodeFile="FollowMESetting.aspx.cs" Inherits="Settings_FollowMESetting" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>

<asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
    <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link"  href="../Settings/Settings.aspx">系统设置</a></span> 
    /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link_select" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
    /<span id="Span1"><a id="A1" runat="server" class="small_link" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
    /<span id="Span2"><a id="A2" runat="server" class="small_link" href="../Settings/ImportUserInfor.aspx">用户信息导入</a></span>
    /<span id="Span3"><a id="A3" runat="server" class="small_link" href="../Settings/ServerIPSetting.aspx">ServerIP设置</a></span>

    /<span id="Span4"><a id="A4" runat="server" class="small_link" href="../Settings/ContentBackupSetting.aspx">内容留底设置</a></span>
    /<span id="Span10"><a id="A10" runat="server" class="small_link" href="../Settings/LDAPSetting.aspx">LDAP认证设置</a></span>
    /<span id="Span11"><a id="A11" runat="server" class="small_link" href="../Settings/DBAuthSetting.aspx">第三方认证设置</a></span>
    /<span id="Span12"><a id="A12" runat="server" class="small_link" href="../Settings/LDAPSynchronization.aspx">LDAP同步</a></span>
        /<span id="Span5"><a id="A13" runat="server" class="small_link" href="../Settings/SectionSetting.aspx">集团设置</a></span>
    /<span id="Span6"><a id="A14" runat="server" class="small_link" href="../Settings/SectionGroupSetting.aspx">集团组设置</a></span>

   <!--
    /<span id="Span5"><a id="A5" runat="server" class="small_link" href="../Settings/LDAPConnection.aspx">LDAP连接设置</a></span>
    /<span id="Span6"><a id="A6" runat="server" class="small_link" href="../Settings/LDAPVerification.aspx">LDAP认证设置</a></span>
    /<span id="Span7"><a id="A7" runat="server" class="small_link" href="../Settings/LDAPGroup.aspx">LDAP组设置</a></span>
    /<span id="Span8"><a id="A8" runat="server" class="small_link" href="../Settings/LDAPUser.aspx">LDAP用户设置</a></span>
    /<span id="Span9"><a id="A9" runat="server" class="small_link" href="../Settings/LDAPSynchronization.aspx">LDAP同步</a></span>
    -->
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
                                                    &nbsp;打印文件相关设定(单位:天)
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    SPL文件清理周期
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        <asp:TextBox ID="txtSPLPeriod" runat="server" Width="80px" MaxLength="10" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtSPLPeriod" runat="server"
                                                            Display="Dynamic" ErrorMessage ="本项不能为空，请重新填写。">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revPassword" ControlToValidate="txtSPLPeriod"
                                                            Display="Dynamic" runat="server" ValidationExpression="^[0-9]*$" ErrorMessage="存在非法字符，请依照给出的格式填写。">
                                                        </asp:RegularExpressionValidator>
                                                        <asp:Label ID="SPLLabel" runat="server" Text="天"></asp:Label>
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
                                                <td colspan="5" class="HR_Line" style="height: 2px">
                                                </td>
                                            </tr>
<!--
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle">
                                                    用户保留文件清理周期
                                                </td>
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        <asp:TextBox ID="txtUserOper" runat="server" Width="80px" MaxLength="10" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUserOper" runat="server"
                                                            Display="Dynamic" ErrorMessage ="本项不能为空，请重新填写。">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtUserOper"
                                                            Display="Dynamic" runat="server" ValidationExpression="^[0-9]*$" ErrorMessage="存在非法字符，请依照给出的格式填写。">
                                                        </asp:RegularExpressionValidator>
                                                        <asp:Label ID="Label1" runat="server" Text="天"></asp:Label>
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
                                                <td class="HR_Line" colspan="5" style="height: 2px">
                                                </td>
                                            </tr>
-->                      
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
                                                <td colspan="5" class="HR_Line" style="height: 2px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                                    &nbsp;服务器监听端口设定
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    监听端口号
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtPort" runat="server" Width="80px" MaxLength="10" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPort" runat="server"
                                                            Display="Dynamic" ErrorMessage ="本项不能为空，请重新填写。">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPort"
                                                            Display="Dynamic" runat="server" ValidationExpression="^[0-9]*$" ErrorMessage="存在非法字符，请依照给出的格式填写。">
                                                        </asp:RegularExpressionValidator>
                                                        <asp:Label ID="Label3" runat="server" Text="eg:9100"></asp:Label>
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
                                                <td colspan="5" class="HR_Line" style="height: 2px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                                    &nbsp;系统Debug模式设定
                                                </td>
                                            </tr>
                                            <tr id="TR7" class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    是否开启Debug模式
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px">
                                                </td>
                                                <td style="padding-left: 20px; height: 35px;" valign="top" align="left">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        <asp:DropDownList ID="ddlDebug" runat="server" CssClass="changeMe" Width="100px">
                                                            <asp:ListItem Value="0" Text="否" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="是"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                                <td valign="middle" align="left">
                                                    &nbsp;
                                                </td>
                                                <td valign="middle" align="left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                             <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                                    &nbsp;重启Follow ME设定
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    重新启动Follow ME
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px; height:25px;">
                                                        <asp:Button ID="btnRestartIIS" runat="server"
                                                            Text="重新加载" CssClass="Button_JobReport" OnClick="btnRestartIIS_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="HR_Line" colspan="5" style="height: 2px">
                                                </td>
                                            </tr>
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
    <asp:Button ID="btnApply" runat="server" Text="设定" CssClass="Login_Button_bg" OnClick="btnApply_Click" OnClientClick="noticeUser();" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnReset" runat="server" Text="重新设定" CssClass="Login_Button_bg" OnClick="btnReset_Click" />

    &nbsp;&nbsp;&nbsp;&nbsp;
    
    <script type="text/javascript" src="../js/Min_radio.js"></script>
    <script type="text/javascript" src="../js/Min_checkbox.js"></script>
    <script src="../js/commonfoot.js" type="text/javascript">
       
    </script>
</asp:Content>

