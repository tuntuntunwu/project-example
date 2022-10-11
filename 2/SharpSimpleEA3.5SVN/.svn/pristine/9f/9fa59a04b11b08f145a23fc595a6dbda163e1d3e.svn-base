<%--
// ==============================================================================
// File Name           : GroupInfoAdd.aspx
// Description         : GroupInfoAdd Page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.06.15
// Date updated        : 2010.09.07
//                       Build No: 1.0.3.2: UI Update.
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

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="GroupInfoAdd.aspx.cs" Inherits="GroupInfoAdd" Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>
<asp:Content ID="ContentGroupEdit" ContentPlaceHolderID="cphbody" runat="Server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_left">
            </td>
            <td style="width: 98%;" align="left" valign="middle" class="TableGrid_bg">
                &nbsp; 用户情报
            </td>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_Right">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" class="Left_VRLine">
                &nbsp;
            </td>
            <td align="left" valign="top" style="height: 390px">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="Normalfont_black">
                    <tr class="Light_GrayFont">
                        <td style="width: 13%; height: 45px" align="left" valign="middle">
                            &nbsp; 用户组名称
                        </td>
                        <td style="width: 1%;" align="left" valign="top" class="VR_line">
                            &nbsp;
                        </td>
                        <td style="width: 86%;" align="left" valign="middle">
                            &nbsp;<asp:TextBox ID="txtGroupName" runat="server" CssClass="Inputtextbox" MaxLength="30"
                                Width="400px"></asp:TextBox><img alt="Req" src="../Images/Req_star.png" width="5"
                                    height="5" style="vertical-align: top" /><asp:CustomValidator ID="valGroupName" ControlToValidate="txtGroupName"
                                        runat="server" ValidationGroup="UpdateGroupName" OnServerValidate="valGroupName_ServerValidate"
                                        Display="Dynamic"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="rfvGroupName" ControlToValidate="txtGroupName" runat="server"
                                Display="Dynamic" ValidationGroup="UpdateGroupName"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revGroupName" ControlToValidate="txtGroupName"
                                Display="Dynamic" runat="server" ValidationGroup="UpdateGroupName" ValidationExpression="[^\\/:*?&quot;&lt;&gt;|]*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr class="Light_GrayFont">
                        <td align="left" valign="middle" style="height: 45px">
                            &nbsp; 配额选择
                               </td>
                        <td align="left" valign="top" class="VR_line">
                            &nbsp;</td>
                        <td align="left" valign="top">
                            <div style="padding-left: 8px; padding-top: 9px; float:left; margin-right:15px;">
                                <asp:DropDownList ID="lstRestriction" runat="server" 
                                    DataTextField="RestrictionName" DataValueField="Id" ie6check="true" Width="400px">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="RefreshRestrictionSet" runat="server" Text="RefreshRestrictionSet" OnClick="btnRefreshRestrictionSet_click" Style="display:none;"/>
                            <asp:Button ID="BtnMoeney" runat="server" Text="自定义" Visible="false" OnClick="btnSetRestrictionName_click" CssClass="Select_button" Style="margin-top:7px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 33px" colspan="3" align="left" valign="middle" class="TableGrid_bg">
                            &nbsp; 所属用户
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="HR_Line" colspan="3" style="height: 2px;" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 259px" colspan="3" align="center" valign="middle">
                            <table width="95%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 45%; height: 21px" align="left" valign="middle">
                                        用户一览
                                    </td>
                                    <td style="width: 10%;" align="left" valign="top">
                                        &nbsp;</td>
                                    <td style="width: 45%;" align="left" valign="middle">
                                        所属用户一览
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="2" align="left" valign="top">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="UserInfoTableborder">
                                            <tr>
                                                <td style="height: 31px" align="left" valign="middle" class="UserInfo_tablebg">
                                                    &nbsp; 用户名(登录名)[所属组名]
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 2px" align="left" valign="top" class="HR_Line">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 187px" align="left" valign="top" class="Normalfont_withoutbold">
                                                    <asp:ListBox ID="lstNoBelong" SelectionMode="Multiple" runat="server" DataTextField="UserList"
                                                        CssClass="group_list selectlist" Rows="10" DataValueField="LoginName"></asp:ListBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="height: 31px" align="left" valign="top">
                                        &nbsp;</td>
                                    <td rowspan="2" align="left" valign="top">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="UserInfoTableborder">
                                            <tr>
                                                <td style="height: 31px" align="left" valign="middle" class="UserInfo_tablebg">
                                                    &nbsp; 用户名(登录名)[所属组名]</td>
                                            </tr>
                                            <tr>
                                                <td style="height: 2px" align="left" valign="top" class="HR_Line">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 187px" align="left" valign="top">
                                                    <asp:ListBox ID="lstBelong" SelectionMode="Multiple" runat="server" DataTextField="UserList"
                                                        Rows="10" DataValueField="LoginName" CssClass="group_list selectlist"></asp:ListBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 205px" align="left" valign="top">
                                        <div style="z-index: 1; position: static; padding-left: 10px; padding-right: 10px;">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 178px">
                                                <tr>
                                                    <td align="left" style="height: 30px" valign="top">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="height: 30px" valign="middle">
                                                        <asp:Button runat="server" ID="btnToBelong" OnClick="btnToBelong_Click" Text=">>"
                                                            CssClass="Addbutton" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="height: 30px" valign="middle">
                                                        <asp:Button runat="server" ID="btnToNoBelong" OnClick="btnToNoBelong_Click" Text="<<"
                                                            CssClass="Addbutton" /></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="height: 50px" valign="top">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
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
</asp:Content>
<asp:Content ID="contentfoot" ContentPlaceHolderID="cphfoot" runat="server">
    <asp:Button ID="btnUpdate" runat="server" Text="用户组追加" OnClick="btnUpdate_Click"
        ValidationGroup="UpdateGroupName" CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
            ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" CssClass="Login_Button_bg" />
</asp:Content>
