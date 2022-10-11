<%--
// ==============================================================================
// File Name           : UserInfoAdd.aspx
// Description         : UserInfoAdd Page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.06.15
// Date updated        : 2010.09.07
//                       Build No: 1.0.3.2: UI Update.
//                     : 2010.12.03
//                       Build No: 1.1: Ver.1.1 Update
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
    CodeFile="SectionAdd.aspx.cs" Inherits="Settings_SectionAdd" Title="SectionAdd" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>
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
                                        &nbsp;集团情报
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
                                        &nbsp;集团名称
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle">
                                        &nbsp;&nbsp;<asp:TextBox ID="txtSectionName" runat="server" Width="300px" MaxLength="30" CssClass="Inputtextbox"></asp:TextBox><img
                                            alt="Req" src="../Images/Req_star.png" width="5px" height="5px" style="vertical-align: top;" />
                                            <asp:CustomValidator ID="valUserName" ControlToValidate="txtSectionName" runat="server" ValidationGroup="UpdateSection"
                                                OnServerValidate="valSectionName_ServerValidate" Display="Dynamic"></asp:CustomValidator><asp:RequiredFieldValidator
                                                ID="rfvSectionName" ControlToValidate="txtSectionName" runat="server" Display="Dynamic"  ValidationGroup="UpdateSection">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator  ID="revSectionName" ControlToValidate="txtSectionName" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateSection"></asp:RegularExpressionValidator>
                                 </td>
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
                                            <asp:DropDownList ID="ddlLebel"  OnSelectedIndexChanged="onSelectedLebelChanged" runat="server" 
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
                                        &nbsp;上级集团
                                    </td>
                                    <td align="left" valign="middle" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" style="padding-left: 10px;padding-top:30px">
                                        <div style="z-index: 2; position: absolute; padding-top: 5px">
                                            <asp:DropDownList ID="ddlParentSectionName" AutoPostBack=True OnSelectedIndexChanged="onSelectedParentSectionChanged" runat="server" DataTextField="SectionID" DataValueField="SectionID"
                                                CssClass="changeMe" Width="400px">
                                            </asp:DropDownList>
                                        </div>
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
    <asp:Button ID="btnUpdate" runat="server" Text="用户追加" OnClick="btnUpdate_Click" ValidationGroup="UpdateSection"
        CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server"
            Text="取消" OnClick="btnCancel_Click"  CssClass="Login_Button_bg" />
</asp:Content>
