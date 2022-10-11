<%--
// ==============================================================================
// File Name           : UserInfoEdit.aspx
// Description         : UserInfoEdit Page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.06.15
// Date updated        : 2010.08.18
//                       Build No: 1.0.3.2: UI Update.
//                       2010.12.27
//                       Html Standerd Modify
//                       2010.12.28
//                       Html Standerd Modify
//                       2011.01.10
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
    CodeFile="UserInfoEdit.aspx.cs" Inherits="UserInfo_UserInfoEdit" Title="UserInfoEdit" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>
<asp:Content ID="ContentUserEdit" ContentPlaceHolderID="cphbody" runat="Server">
    <!-- Main table-->
    <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
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
                            <table style="width: 100%; height: 385px;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 27%;" align="left" valign="middle" class="Add_User_Titlebg">
                                        &nbsp;用户情报
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
                                        &nbsp;用户名(全名等)
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle">
                                        &nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="300px" MaxLength="30" CssClass="Inputtextbox"
                                            Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr id="TR_ID2" class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;登录名
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle">
                                        &nbsp;<asp:TextBox ID="txtLoginName" runat="server" Width="300px" MaxLength="30"
                                            CssClass="Inputtextbox" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="left" valign="middle" style="height: 35px;" class="TableGrid_bg">
                                        &nbsp;用户密码
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;密码
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top">
                                        &nbsp;<asp:TextBox ID="txtPassword" runat="server" Width="140px" MaxLength="30" 
                                            CssClass="Inputtextbox"></asp:TextBox><img alt="Req" src="../Images/Req_star.png"
                                                width="5px" height="5px" style="vertical-align: top;" /><asp:RequiredFieldValidator
                                                    ID="rfvPassword" ControlToValidate="txtPassword" runat="server" Display="Dynamic"
                                                    ValidationGroup="UpdateUser"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revPassword" ControlToValidate="txtPassword" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateUser" ValidationExpression="^[a-zA-Z0-9]{4,30}$"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;确认密码
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top">
                                        &nbsp;<asp:TextBox ID="txtPasswordConfirm" runat="server" Width="140px" MaxLength="10"
                                            CssClass="Inputtextbox" ></asp:TextBox><img alt="Req" src="../Images/Req_star.png"
                                                width="5px" height="5px" style="vertical-align: top;" /><asp:RequiredFieldValidator
                                                    ID="rfvPasswordConfirm" ControlToValidate="txtPasswordConfirm" runat="server"
                                                    Display="Dynamic" ValidationGroup="UpdateUser"></asp:RequiredFieldValidator><asp:CompareValidator
                                                        ID="cpvPasswordConfirm" runat="server" ValidationGroup="UpdateUser" ControlToCompare="txtPassword"
                                                        ControlToValidate="txtPasswordConfirm" Operator="Equal"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="left" valign="middle" style="height: 35px;" class="TableGrid_bg">
                                        &nbsp;IC卡信息
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;IC卡编号
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top">
                                        &nbsp;<asp:TextBox ID="txtCardID" runat="server" Width="250px" MaxLength="20" CssClass="Inputtextbox"></asp:TextBox><asp:CustomValidator
                                            ID="valCardID" ControlToValidate="txtCardID" runat="server" ValidationGroup="UpdateUser"
                                            OnServerValidate="valCardID_ServerValidate" Display="Dynamic"></asp:CustomValidator><asp:RegularExpressionValidator
                                                ID="revCardID" ControlToValidate="txtCardID" Display="Dynamic" runat="server"
                                                ValidationGroup="UpdateUser" ValidationExpression="^[a-zA-Z0-9]{0,20}$"></asp:RegularExpressionValidator></td>
                                </tr>
								
                                <tr>
                                    <td colspan="3" align="left" valign="middle" style="height: 35px;" class="TableGrid_bg">
                                        &nbsp;Pin码信息
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;Pin码编号
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top">
                                        &nbsp;<asp:TextBox ID="txtPinCode" runat="server" Width="250px" MaxLength="20" CssClass="Inputtextbox"></asp:TextBox><asp:CustomValidator
                                                ID="valPinCode" ControlToValidate="txtPinCode" runat="server" ValidationGroup="UpdateUser"
                                                OnServerValidate="valPincode_ServerValidate" Display="Dynamic"></asp:CustomValidator><asp:RegularExpressionValidator
                                                        ID="revPinCode" ControlToValidate="txtPinCode" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateUser" ValidationExpression="^[0-9]{4,20}$"></asp:RegularExpressionValidator></td>
                                </tr>								
								
								
                                <tr>
                                    <td colspan="3" align="left" valign="middle" style="height: 35px;" class="TableGrid_bg">
                                        &nbsp;Email信息
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;Email
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top">
                                        &nbsp;<asp:TextBox ID="txtEmail" runat="server" Width="400px" MaxLength="50" CssClass="Inputtextbox"></asp:TextBox><asp:CustomValidator
                                            ID="CustomValidator1" ControlToValidate="txtEmail" runat="server" ValidationGroup="UpdateUser"
                                            Display="Dynamic"></asp:CustomValidator><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator1" ControlToValidate="txtEmail" 
                                            Display="Dynamic" runat="server"
                                                ValidationGroup="UpdateUser" 
                                            ValidationExpression="^([\w-\.]+)@([-\dA-Za-z]+\.)+[a-zA-Z]{2,}"
                                            ErrorMessage="Email地址不合法"></asp:RegularExpressionValidator></td>
                                </tr>     
                                <tr>
                                    <td colspan="3" align="left" valign="middle" style="height: 35px;" class="TableGrid_bg">
                                        &nbsp;用户组信息
                                    </td>
                                </tr>
                                <tr id="TR_ID3" class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;所属组
                                    </td>
                                    <td align="left" valign="middle" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" style="padding-left: 10px;">
                                        <div style="z-index: 2; position: absolute; padding-top: 5px">
                                            <asp:DropDownList ID="ddlGroupName" AutoPostBack=true OnSelectedIndexChanged="onSelectedGroupChanged" runat="server" DataTextField="GroupName" DataValueField="id"
                                                CssClass="changeMe" Width="400px">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="left" valign="middle" style="height: 35px;" class="TableGrid_bg">
                                        &nbsp;配额方案
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;" class="Light_GrayFont">
                                        &nbsp;配额选择
                                    </td>
                                    <td align="left" valign="middle" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" style="padding-left: 10px;">
                                        <div style="padding-top: 5px;float:left; margin-right:15px;">
                                            <asp:DropDownList ID="ddlRestrictionSet" runat="server" DataTextField="RestrictionName"
                                                DataValueField="id" CssClass="changeMe" Width="400px">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Button ID="RefreshRestrictionSet" runat="server" Text="RefreshRestrictionSet" OnClick="btnRefreshRestrictionSet_click" Style="display:none;"/>
                                        <asp:Button ID="btnAutoDefine" runat="server" Text="自定义" Visible="false" OnClick="btnSetRestrictionName_click"  CssClass="Select_button" Style="margin-top:3px;"/>
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
    <asp:HiddenField ID="hidICId" runat="server" />
    <!-- end of the Main table-->
</asp:Content>
<asp:Content ID="contentfoot" ContentPlaceHolderID="cphfoot" runat="server">
    <asp:Button ID="btnMFPRes" runat="server" Text="MFP限制设定" CssClass="Login_Button_bg"  OnClick="btnMFPRes_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnUpdate" runat="server" Text="更新" OnClick="btnUpdate_Click" ValidationGroup="UpdateUser"
        CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server"
            Text="取消" OnClick="btnCancel_Click" CssClass="Login_Button_bg" />
</asp:Content>
