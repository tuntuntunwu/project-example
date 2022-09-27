﻿<%--
// ==============================================================================
// File Name           : SettingGroupSetting.aspx
// Description         : SectionGroupSetting Page
// Author(s)           : WangZiyang
// Date created        : 2019.09.26
//                       IC Card Must Input Delete
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="SectionGroupSetting.aspx.cs" Inherits="Settings_SectionGroupSetting" Title="SectionGroupSetting" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>


<asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
     <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link"  href="../Settings/Settings.aspx">系统设置</a></span> 
    /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
    /<span id="Span1"><a id="A1" runat="server" class="small_link" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
    /<span id="Span2"><a id="A2" runat="server" class="small_link" href="../Settings/ImportUserInfor.aspx">用户信息导入</a></span>
    /<span id="Span3"><a id="A3" runat="server" class="small_link" href="../Settings/ServerIPSetting.aspx">ServerIP设置</a></span>
    /<span id="Span4"><a id="A4" runat="server" class="small_link" href="../Settings/ContentBackupSetting.aspx">内容留底设置</a></span>
    /<span id="Span10"><a id="A10" runat="server" class="small_link" href="../Settings/LDAPSetting.aspx">LDAP认证设置</a></span>
    /<span id="Span11"><a id="A11" runat="server" class="small_link" href="../Settings/DBAuthSetting.aspx">第三方认证设置</a></span>
    /<span id="Span12"><a id="A12" runat="server" class="small_link" href="../Settings/LDAPSynchronization.aspx">LDAP同步</a></span>
     /<span id="Span5"><a id="A13" runat="server" class="small_link" href="../Settings/SectionSetting.aspx">集团设置</a></span>
    /<span id="Span6"><a id="A14" runat="server" class="small_link_select" href="../Settings/SectionGroupSetting.aspx">集团组设置</a></span>

</asp:Content>



<asp:Content ID="ContentUserEdit" ContentPlaceHolderID="cphbody" runat="Server">
    <!-- Main table-->
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_left">
            </td>
            <td style="width: 98%; height: 2px;" align="left" valign="middle" class="bottom_HR_line">
            </td>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_Right">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" class="Left_VRLine">
                &nbsp;
            </td>
            <td align="left" valign="top" style="height: 385px;">
                <table style="width: 100%; border: 0; height: 385px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 97%; height: 385px;" align="left" valign="top">
                            <table style="width: 100%; height: 385px; border: 0;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 27%;" align="left" valign="middle" class="Add_User_Titlebg">
                                        &nbsp;集团组
                                    </td>
                                    <td style="width: 0%;" align="left" valign="top" class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                    <td style="width: 73%;" align="left" valign="top" class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr id="TR_ID1" class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;用户组
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle" style="padding-left: 10px;">
                                        <div>
                                             <asp:DropDownList ID="ddl1"  OnSelectedIndexChanged="uGonSelectedLebelChanged" runat="server" AutoPostBack="true"
                                                CssClass="changeMe" Width="400px">
                                            </asp:DropDownList>
                                        </div></td>
                                </tr>
                                <tr id="TR_ID2" class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;集团级别
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" style="padding-left: 10px;padding-top:30px">
                                        <div style="z-index: 2; position: absolute; padding-top: 5px">
                                            <asp:DropDownList ID="ddl2"  OnSelectedIndexChanged="sGonSelectedLebelChanged" runat="server" AutoPostBack="true"
                                                CssClass="changeMe" Width="400px">
                                                <asp:ListItem> 1 </asp:ListItem>
                                                <asp:ListItem> 2 </asp:ListItem>
                                                <asp:ListItem> 3 </asp:ListItem>
                                                <asp:ListItem> 4 </asp:ListItem>
                                                <asp:ListItem> 5 </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                
                                <tr id="TR_ID3" class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;集团
                                    </td>
                                    <td align="left" valign="middle" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle" style="padding-left: 10px;padding-top:10px">
                                             <asp:DropDownList ID="ddl3"  OnSelectedIndexChanged="sonSelectedLebelChanged" runat="server" 
                                                CssClass="changeMe" Width="400px">
                                            </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                   
                            </table>
                        </td>
                        <td style="width: 3%;" align="left" valign="top" class="VR_line">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" valign="top" class="VR_line">
            </td>
        </tr>
        <tr>
            <td style="height: 2px;" align="right" valign="bottom" class="Bottom_login_bg_new">
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
    <asp:Button ID="btnUpdate" runat="server" Text="设定" OnClick="btnUpdate_Click" ValidationGroup="UpdateSection"  OnClientClick="javascript:alert('设定成功!')" onserverclick="btnSubmit_ServerClick" class="but_01" value="确定提交"
        CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
