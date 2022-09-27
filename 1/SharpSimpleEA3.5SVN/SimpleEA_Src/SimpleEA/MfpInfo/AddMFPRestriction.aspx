<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="AddMFPRestriction.aspx.cs" Inherits="MfpInfo_AddMFPRestriction" Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>
<asp:Content ID="ContentGroupEdit" ContentPlaceHolderID="cphbody" runat="Server">
    <script language="javascript" type="text/javascript">
        /********************************************
        * FUNCTION : IpReqular
        * SUMMARY  : Reqular check for IP
        * AUTHOR   : SES Zheng Wei
        * DATE     : 2012.01.19
        * VERSION  : 0.01
        *******************************************/
        function IpReqular(sender, args) {
            var txtIP1 = document.getElementById("<%=txtIP1.ClientID%>");
            var txtIP2 = document.getElementById("<%=txtIP2.ClientID%>");
            var txtIP3 = document.getElementById("<%=txtIP3.ClientID%>");
            var txtIP4 = document.getElementById("<%=txtIP4.ClientID%>");
            var checkValue = txtIP1.value;
            if (IsIpCode(checkValue)) {
                args.IsValid = false;
                return false;
            }
            checkValue = txtIP2.value;
            if (IsIpCode(checkValue)) {
                args.IsValid = false;
                return false;
            }
            checkValue = txtIP3.value;
            if (IsIpCode(checkValue)) {
                args.IsValid = false;
                return false;
            }
            checkValue = txtIP4.value;
            if (IsIpCode(checkValue)) {
                args.IsValid = false;
                return false;
            }
            args.IsValid = true;
            return true;
        }

        /********************************************
        * FUNCTION : IsIpCode
        * SUMMARY  : IpCode check for IP
        * AUTHOR   : SES Zheng Wei
        * DATE     : 2012.01.19
        * VERSION  : 0.01
        *******************************************/
        function IsIpCode(o) {
            o = o.Trim();

            if (o == "") {
                return false;
            }

            var Letters = "1234567890";
            var i;
            var c;
            for (i = 0; i < o.length; i++) {
                c = o.charAt(i);
                if (Letters.indexOf(c) == -1) {
                    return true;
                }
            }

            // between 0 to 255
            if (Number(o) < 0 || Number(o) > 255) {
                return true;
            }

            return false;
        }
                                   
    </script>
    <script language="javascript" type="text/javascript">
        /********************************************
        * FUNCTION : IpRequire
        * SUMMARY  : Require check for IP
        * AUTHOR   : SES Zheng Wei
        * DATE     : 2012.01.19
        * VERSION  : 0.01
        *******************************************/
        function IpRequire(sender, args) {
            var txtIP1 = document.getElementById("<%=txtIP1.ClientID%>");
            var txtIP2 = document.getElementById("<%=txtIP2.ClientID%>");
            var txtIP3 = document.getElementById("<%=txtIP3.ClientID%>");
            var txtIP4 = document.getElementById("<%=txtIP4.ClientID%>");

            var checkValue = txtIP1.value;


            if (checkValue.Trim() == "") {
                args.IsValid = false;
                return false;
            }
            checkValue = txtIP2.value;
            if (checkValue.Trim() == "") {
                args.IsValid = false;
                return false;
            }
            checkValue = txtIP3.value;
            if (checkValue.Trim() == "") {
                args.IsValid = false;
                return false;
            }
            checkValue = txtIP4.value;
            if (checkValue.Trim() == "") {
                args.IsValid = false;
                return false;
            }
            args.IsValid = true;
            return true;
        }
    </script>
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
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 27%;" align="left" valign="middle" class="Add_User_Titlebg">
                                        &nbsp;MFP情报
                                    </td>
                                    <td style="width: 0%;" align="left" valign="top" class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                    <td style="width: 73%;" align="left" valign="top" class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="width: 200px; height: 45px;">
                                        MFP型号
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txtModel" runat="server" CssClass="Inputtextbox" MaxLength="30"
                                            Width="200px"></asp:TextBox><img alt="Req" src="../Images/Req_star.png" width="5"
                                                height="5" style="vertical-align: top" /><asp:RequiredFieldValidator ID="rfvModelName"
                                                    ControlToValidate="txtModel" runat="server" Display="Dynamic" ValidationGroup="UpdateGroupName"> </asp:RequiredFieldValidator> <asp:RegularExpressionValidator
                                                        ID="revModelName" ControlToValidate="txtModel" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateGroupName"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" class="HR_Line" style="height: 2px">
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle">
                                        MFP 序列号
                                    </td>
                                    <td align="left" class="VR_line" valign="top">
                                    </td>
                                    <td style="width: 86%; height: 45px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txtSev" runat="server" CssClass="Inputtextbox" MaxLength="10" Enabled="true"
                                            Width="200px"></asp:TextBox><img alt="Req" src="../Images/Req_star.png" width="5"
                                                height="5" style="vertical-align: top" /><asp:RequiredFieldValidator ID="ReqularMFPSN"
                                                    ControlToValidate="txtSev" runat="server" Display="Dynamic" ValidationGroup="UpdateGroupName"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="RegularMFPSN" ControlToValidate="txtSev" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateGroupName"></asp:RegularExpressionValidator><asp:CustomValidator
                                                            ID="valMFPSN" ControlToValidate="txtSev" runat="server" ValidationGroup="UpdateGroupName"
                                                            Display="Dynamic" OnServerValidate="valMFPSN_ServerValidate"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="HR_Line" colspan="5" style="height: 2px">
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="width: 200px; height: 45px;">
                                        MFP IP 地址
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 2px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txtIP1" runat="server" CssClass="Inputtextbox" MaxLength="3" Enabled="true"
                                            Width="32"></asp:TextBox>.<asp:TextBox ID="txtIP2" runat="server" CssClass="Inputtextbox"
                                                MaxLength="3" Enabled="true" Width="32"></asp:TextBox>.<asp:TextBox ID="txtIP3" runat="server"
                                                    CssClass="Inputtextbox" MaxLength="3" Enabled="true" Width="32"></asp:TextBox>.<asp:TextBox
                                                        ID="txtIP4" runat="server" CssClass="Inputtextbox" MaxLength="3" Enabled="true"
                                                        Width="32"></asp:TextBox><img alt="Req" src="../Images/Req_star.png" width="5" height="5"
                                                            style="vertical-align: top" />
                                        <asp:CustomValidator ID="CusIPRequire" runat="server" ValidationGroup="UpdateGroupName"
                                            Display="Dynamic" OnServerValidate="CusIPRequire_ServerValidate" ClientValidationFunction="IpRequire">
                                        </asp:CustomValidator>
                                        <asp:CustomValidator ID="IpReqular" runat="server" ValidationGroup="UpdateGroupName"
                                            Display="Dynamic" ClientValidationFunction="IpReqular"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" class="HR_Line" style="height: 2px">
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="width: 200px; height: 45px;">
                                        管理员账号
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 2px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txtAdministrator" runat="server" CssClass="Inputtextbox" MaxLength="10" Enabled="false"
                                            Width="200px"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" class="HR_Line" style="height: 2px">
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="width: 200px; height: 45px;">
                                        管理员密码
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 2px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="Inputtextbox" MaxLength="50" Enabled="true"
                                            Width="200px"></asp:TextBox><img alt="Req" src="../Images/Req_star.png" width="5"
                                                height="5" style="vertical-align: top" /><asp:RequiredFieldValidator ID="rfvPSWD"  ControlToValidate="txtPassword" runat="server" Display="Dynamic" ValidationGroup="UpdateGroupName">  </asp:RequiredFieldValidator>

                                                        


                                    </td>
                                </tr>
                                  <tr>
                                    <td colspan="5" class="HR_Line" style="height: 2px">
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;" class="Light_GrayFont">
                                        &nbsp;价格选择
                                    </td>
                                    <td align="left" valign="middle" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" style="padding-left: 20px;">
                                        <div style="z-index: 1; position: absolute; padding-top: 5px">
                                            <asp:DropDownList ID="ddlPriceSet" runat="server" DataTextField="PriceNM"
                                                DataValueField="PriceID" CssClass="changeMe" Width="400px">
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>



                                <tr>
                                    <td colspan="5" class="HR_Line" style="height: 2px">
                                    </td>
                                </tr>


                                <!--监控设置 -->
                             <tr class="Light_GrayFont">
                                <td align="left" valign="middle" style="height: 35px;" class="Light_GrayFont">
                                        &nbsp;监控设置
                                    </td>
                                    <td align="left" valign="middle" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="top" style="padding-left: 20px;">
                                        <div style="z-index: 1; position: absolute; padding-top: 8px">
                                        
                                       
                                        <asp:CheckBox ID="Monitor1" runat="server" />&nbsp;打印内容留底
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="Monitor2" runat="server" />&nbsp;复印内容留底
                                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="Monitor3" runat="server" />&nbsp;强制黑白

                                        </div>
                                     </td>
                                </tr>

                                
                                <tr>
                                    <td colspan="5" class="HR_Line" style="height: 2px">
                                    </td>
                                </tr>

                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="width: 200px; height: 275px;">
                                        描述
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 10px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txtDes" runat="server" TextMode="MultiLine" class="Textarea" CssClass="Inputtextbox"
                                            Height="200px" Width="300px" MaxLength="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="txtConclusionValidator1" ControlToValidate="txtDes" ErrorMessage ="超过50字"
                                           Display="Dynamic" runat="server" ValidationGroup="UpdateGroupName" ValidationExpression="^[\s\S]{0,50}$"/>
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
    <asp:Button ID="btnAdd" runat="server" Text="追加" OnClick="btnAdd_Click" ValidationGroup="UpdateGroupName"
        CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server"
            Text="取消" CssClass="Login_Button_bg" OnClick="btnCancel_Click" />
</asp:Content>
