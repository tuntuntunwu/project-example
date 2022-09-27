<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true" CodeFile="ServerIPSetting.aspx.cs" Inherits="Settings_ServerIPSetting" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>

<asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
    <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link"  href="../Settings/Settings.aspx">系统设置</a></span> 
    /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
    /<span id="Span1"><a id="A1" runat="server" class="small_link" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
    /<span id="Span2"><a id="A2" runat="server" class="small_link" href="../Settings/ImportUserInfor.aspx">用户信息导入</a></span>
    /<span id="Span3"><a id="A3" runat="server" class="small_link_select" href="../Settings/ServerIPSetting.aspx">ServerIP设置</a></span>
    /<span id="Span4"><a id="A4" runat="server" class="small_link" href="../Settings/ContentBackupSetting.aspx">内容留底设置</a></span>
     /<span id="Span10"><a id="A10" runat="server" class="small_link " href="../Settings/LDAPSetting.aspx">LDAP认证设置</a></span>
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
      //      AlertMessage("设置完成后，请到SimpleEAFollowME文件夹重启服务。");
        }
    </script>
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
                                                    &nbsp;服务器IP地址设定
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    服务器IP地址
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td style="width: 86%; height: 2px; padding-left: 20px;" align="left" valign="middle">
                                                    <asp:TextBox ID="txtIP1" runat="server" CssClass="Inputtextbox" MaxLength="3" Enabled="true"
                                                        Width="32" ></asp:TextBox>.<asp:TextBox ID="txtIP2" runat="server" CssClass="Inputtextbox"
                                                            MaxLength="3" Enabled="true" Width="32" ></asp:TextBox>.<asp:TextBox ID="txtIP3" runat="server"
                                                                CssClass="Inputtextbox" MaxLength="3" Enabled="true" Width="32" ></asp:TextBox>.<asp:TextBox
                                                                    ID="txtIP4" runat="server" CssClass="Inputtextbox" MaxLength="3" Enabled="true"
                                                                    Width="32" ></asp:TextBox><img alt="Req" src="../Images/Req_star.png" width="5" height="5"
                                                                        style="vertical-align: top" />
                                                <!--
                                                    <asp:CustomValidator ID="CusIPRequire" runat="server" ValidationGroup="UpdateGroupName"
                                                        Display="Dynamic" OnServerValidate="CusIPRequire_ServerValidate" 
                                                        ClientValidationFunction="IpRequire" ValidateEmptyText="True"></asp:CustomValidator>
                                                    <asp:CustomValidator ID="IpReqular" runat="server" ValidationGroup="UpdateGroupName"
                                                        Display="Dynamic" ClientValidationFunction="IpReqular" 
                                                        ValidateEmptyText="True"></asp:CustomValidator>
                                                        -->
                                                </td>
                                                <td align="left" valign="middle" style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="HR_Line" style="height: 2px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                                    &nbsp;关于公司信息的设定
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    公司信息
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtCompany" runat="server" Width="400px" MaxLength="400" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtCompany" runat="server"
                                                            Display="Dynamic" ErrorMessage ="本项不能为空，请重新填写。">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                                    &nbsp;登录方式的设定
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    登录方式
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="middle" style="padding-left: 20px; height: 35px;" >
                                                   <!-- <asp:CheckBox ID="chk_Login" runat="server" Text="允许Pin码登录" /> -->
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="left" valign="middle" style="width: 5px">
                                                                  <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoUserPwdLogin" GroupName="LonginGroup"   Checked="true" AutoPostBack="True" /> 
                                                                  </div>
                                                            </td>
                                                            <td align="left" valign="middle" style="width: 90px">
                                                               登录名/密码和刷卡登录</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" class="HR_Line" style="height: 2px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="middle">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoPinCodeLogin" GroupName="LonginGroup"   AutoPostBack="True" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                Pin码登录和刷卡登录
                                                            </td>
                                                        </tr>
                                                    </table>

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
                                                    &nbsp;报警参数设定
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    墨粉阈值
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtColThreshold" runat="server" Width="80px" MaxLength="2" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtColThreshold" runat="server"  ValidationGroup="UpadteSetting"
                                                            Display="Dynamic" ErrorMessage ="本项不能为空，请重新填写。">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtColThreshold"
                                                            Display="Dynamic" runat="server" ValidationGroup="UpadteSetting" ValidationExpression="^[0-9]*$" ErrorMessage="存在非法字符，请依照给出的格式填写。">
                                                        </asp:RegularExpressionValidator>
                                                        <asp:Label ID="Label3" runat="server" Text="eg:50"></asp:Label>
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
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    纸张阈值
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtPaperThreshold" runat="server" Width="80px" MaxLength="2" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPaperThreshold" runat="server"  ValidationGroup="UpadteSetting"
                                                            Display="Dynamic" ErrorMessage ="本项不能为空，请重新填写。">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPaperThreshold" ValidationGroup="UpadteSetting"
                                                            Display="Dynamic" runat="server" ValidationExpression="^[0-9]*$" ErrorMessage="存在非法字符，请依照给出的格式填写。">
                                                        </asp:RegularExpressionValidator>
                                                        <asp:Label ID="Label1" runat="server" Text="eg:50"></asp:Label>
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
                                                    &nbsp;关于报警通信方式的设定
                                                </td>
                                            </tr>
                                            <tr id="TR6" class="Light_GrayFont" style="height: 70px">
                                                <td align="left" valign="middle">
                                                    通信方式</td>
                                                <td align="left" valign="top" class="VR_line">
                                                </td>
                                                <td colspan="3" valign="middle">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="center" valign="middle" style="width: 50px">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoEmail" GroupName="BorrowGroup" 
                                                                        Checked="true" AutoPostBack="True" oncheckedchanged="rdoEmail_CheckedChanged"
                                                                         />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle" style="width: 90px">
                                                                Email</td>
                                                            <td align="left" valign="middle" style="padding-left: 10px; width: auto; height: 35px">
                                                                &nbsp;<asp:TextBox ID="txtEmail" runat="server" Width="400px" MaxLength="50" CssClass="Inputtextbox"></asp:TextBox>
                                                                <asp:RegularExpressionValidator
                                                                        ID="revEmail" ControlToValidate="txtEmail" Display="Dynamic" runat="server"
                                                                        ValidationGroup="UpadteSetting" ValidationExpression="^([\w-\.]+)@([-\dA-Za-z]+\.)+[a-zA-Z]{2,}"  ErrorMessage="Email地址不合法"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" class="HR_Line" style="height: 2px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="middle">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoWC" GroupName="BorrowGroup" 
                                                                        AutoPostBack="True" oncheckedchanged="rdoWc_CheckedChanged" /> 
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                微信</td>
                                                            <td align="left" valign="middle" style="padding-left: 10px; height: 35px">
                                                                &nbsp;<asp:TextBox ID="txtWeixin" runat="server" Width="400px" MaxLength="50" CssClass="Inputtextbox"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" class="HR_Line" style="height: 2px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                                    &nbsp;送信方参数设置
                                                </td>
                                            </tr>

                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    送信方Email地址
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtEmailAddress" runat="server" Width="200px" MaxLength="100" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    送信方信箱服务器地址
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtEmailServerIP" runat="server" Width="200px" MaxLength="50" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>

                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    送信方账户名
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtEmailUserName" runat="server" Width="200px" MaxLength="50" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>

                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    送信方账户密码
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtEmailPassword" runat="server" Width="200px" MaxLength="50" CssClass="Inputtextbox" >
                                                        </asp:TextBox>
                                                        
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>


                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    送信方微信账户
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtCorpID" runat="server" Width="200px" MaxLength="50" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>


                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    送信方微信安全
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtSecret" runat="server" Width="200px" MaxLength="200" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>

                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    送信方微信AgentID
                                                </td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 20px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                    <asp:TextBox ID="txtAgentID" runat="server" Width="200px" MaxLength="10" CssClass="Inputtextbox">
                                                        </asp:TextBox>
                                                        
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
    <asp:Button ID="btnApply" runat="server" Text="设定" CssClass="Login_Button_bg" OnClick="btnApply_Click" ValidationGroup="UpadteSetting" OnClientClick="noticeUser();"
         />&nbsp;&nbsp;&nbsp;&nbsp;
    <script type="text/javascript" src="../js/Min_radio.js"></script>
    <script type="text/javascript" src="../js/Min_checkbox.js"></script>
    <script src="../js/commonfoot.js" type="text/javascript">
       
    </script>
</asp:Content>

