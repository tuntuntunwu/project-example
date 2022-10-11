<%--
// ==============================================================================
// File Name           : RestrictionInfo 
// Description         : 配额管理  追加
// Author(s)           : MJ Tan
// Date created        : 2014.04.24
// Date updated        : 2014.04.24
//                       Html Standerd Modify
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2011, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/RestrictionInfo/popRestrictInfoAdd.aspx.cs" Inherits="RestrictionInfo_popRestrictInfoAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Language" content="zh-cn" />
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta http-equiv="Pragma" content="no-cache" />
    <base target="_self" />
    <title>::Simple EA Application :: 配额管理  追加</title>
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Css/common.css" />
    <!--[if IE 6]>
		<link rel="stylesheet" href="../Css/IE.css" type="text/css" />
		
   <![endif]-->
    <link rel="stylesheet" type="text/css" href="../Css/popstyle.css" />

    <script language="javascript" src="../Js/common.js" type="text/javascript"></script>

    <script type="text/javascript" src="../JavaScript/jquery.min.js"></script>

    <script type="text/javascript" language="javascript">
        setTimeout(function () { $("#chk_job_function").width(380).css("display", "block"); }, 500);
        function callparent() {
            if (window.opener) {
                window.opener.document.getElementById("ctl00_cphbody_RefreshRestrictionSet").click();
            }
            close_Alert();
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" style="min-width: auto; min-height: auto;" onresize="window.resizeTo(800, 450);">
    <form id="frmMain" runat="server" >

        <script language="javascript" type="text/javascript">            bgLoad();</script>

        <div id="WhiteBoxbg" class="AttentionMSG">
            <!-- light box - show message -->
            <table cellpadding="0" cellspacing="0" border="0" style="width: 400px; height: 175px;">
                <tr>
                    <td class="AttentionMSG_tablebg" style="height: 35px;" align="left" valign="middle">
                        &nbsp;::Simple EA Application :: 请稍候...
                    </td>
                </tr>
                <tr>
                    <td style="height: 100px;" class="Black_Font_bold" align="center" valign="middle">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" valign="middle" style="width: 25%;">
                                    <img alt="" src="../Images/Attention.png" style="border: 0;" /></td>
                                <td align="left" valign="middle">
                                    请不要关闭本页面，操作正在执行中，请稍候...</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="Alert" style="height: 65px;" align="center" valign="middle">
                    </td>
                </tr>
                <!-- End of the light box - show message -->
            </table>
        </div>
        <div id="ErrorMessage" class="ErrorMSG" onkeydown="ErrorMessage_onkeydown();">
            <!-- light box - show message -->
            <table cellpadding="0" cellspacing="0" border="0" style="width: 400px; height: 175px;">
                <tr>
                    <td class="ErrorMSG_tablebg" style="height: 35px;" align="left" valign="middle">
                        &nbsp; <span id="ErrorMessagetitle">Message: Error Files</span>
                    </td>
                </tr>
                <tr>
                    <td style="height: 100px;" class="Black_Font_bold" align="center" valign="middle">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" valign="middle" style="width: 25%;">
                                    <img alt="" src="../Images/Error_msg.png" style="border: 0;" /></td>
                                <td align="left" valign="middle">
                                    <span id="ErrorMessagebody">ERROR MESSAGE: OOps Error message</span></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="ErrorMessageButton" style="height: 65px" align="center" valign="middle">
                        <input id="ErrorMessageOK" class="Login_Button_bg" onclick="close_Error();"
                            type="button" value="确定" />
                    </td>
                </tr>
                <!-- End of the light box - show message -->
            </table>
        </div>
        <div id="AlertMessage" class="OKMSG" onkeydown="AlertMessage_onkeydown();">
            <!-- light box - show message -->
            <table cellpadding="0" cellspacing="0" border="0" style="width: 400px; height: 175px;">
                <tr>
                    <td class="OKMSG_tablebg" style="height: 35px;" align="left" valign="middle">
                        &nbsp; <span id="AlertMessagetitle">Message: Successfully Add</span>
                    </td>
                </tr>
                <tr>
                    <td style="height: 100px;" class="Black_Font_bold" align="center" valign="middle">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" valign="middle" style="width: 25%;">
                                    <img alt="" src="../Images/OK.png" style="border: 0;" /></td>
                                <td align="left" valign="middle">
                                    <span id="AlertMessagebody">OK MESSAGE: Successfully Add</span></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="Td2" style="height: 65px;" align="center" valign="middle">
                        <input type="button" id="AlertMessageOK" class="Login_Button_bg" value="确定" onclick="callparent();" />
                    </td>
                </tr>
                <!-- End of the light box - show message -->
            </table>
        </div>
        <div id="ConfirmMessage" class="WarningnMSG" onkeydown="ConfirmMessage_onkeydown();">
            <!-- light box - show message -->
            <table cellpadding="0" cellspacing="0" style="border: 0; width: 400px; height: 175px;">
                <tr>
                    <td class="WarningnMSG_tablebg" style="height: 35px;" align="left" valign="middle">
                        &nbsp;<span id="ConfirmMessagetitle"> Message: Attention Requied Filed</span>
                    </td>
                </tr>
                <tr>
                    <td style="height: 100px;" class="Black_Font_bold" align="center" valign="middle">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" valign="middle" style="width: 25%;">
                                    <img alt="" src="../Images/warning.png" style="border: 0;" /></td>
                                <td align="left" valign="middle">
                                    <span id="ConfirmMessagebody">ATTENTION MESSAGE: ATTENTION Required Filed</span></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="tdConfirm" style="height: 65px;" align="center" valign="middle">
                        <input type="button" id="ConfirmMessageOK" class="Login_Button_bg" value="确定" onclick="close_Confirm(true);" />
                        <input type="button" id="ConfirmMessageCancel" class="Login_Button_bg" value="取消"
                            onclick="close_Confirm(false);" />
                    </td>
                </tr>
                <!-- End of the light box - show message -->
            </table>
        </div>
        <div id="Lightbox" class="Lightboxbg">
        </div>
        <div id="LightWhiteboxbg" class="LightWhiteboxbg">
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="float: none;">
            <tr>
                <td style="height: 47px;" align="left" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 25px;">
                        <tr>
                            <td style="width: 90%;" align="left" valign="bottom" class="TitleFont">
                                <!-- Application Logo -->
                                <div class="IMG_1" style="width: 375px; height: 41px;">
                                </div>
                            </td>
                            <td style="width: 10%;" align="left" valign="top">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="height: 2px;" class="HR_Line">
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="height: 20px;">
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <table width="97%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="height: 10px;" align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1%;" align="left" valign="top">
                                        </td>
                                        <td style="width: 98%;" align="left" valign="top">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 1%;" align="right" valign="top">
                                                        <div class="Login_left_IMG" style="width: 100%; height: 39px">
                                                        </div>
                                                    </td>
                                                    <td class="Login_Topcenter" style="width: 98%;">
                                                        <table id="tblmust" runat="server" cellpadding="0" cellspacing="0" style="height: 38px;
                                                            width: 100%; border: 0;" visible="true">
                                                            <tr>
                                                                <td align="left"  style="width: 30%;" valign="middle">
                                                                    配额管理  追加
                                                                </td>
                                                                <td align="right" class="Small_Font" valign="middle">
                                                                    以 ''<img alt="" height="5px" src="../Images/Req_star.png" width="5px" />'' 标记的项目为必须输入项目。
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>                                                    
                                                    <td style="width: 1%;" align="left" valign="top">
                                                        <div class="Login_Right_IMG" style="width: 100%; height: 39px">
                                                        </div>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top" class="Login_left_bg">
                                                    </td>
                                                    <td align="left" valign="top" class="Login_centerbg">
                                                        <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="left" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="top">
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
                                                                            <td align="left" valign="top">
                                                                                <table style="width: 572px; border: 0;" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td style="width: 572px;" align="left" valign="top">
                                                                                            <table style="width: 572px;" border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr class="Light_GrayFont">
                                                                                                    <td align="left" valign="middle" style="height: 35px; width: 118px">
                                                                                                        &nbsp;名称
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" class="VR_line">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" style="width: 450px">
                                     <asp:TextBox ID="txtResName" runat="server" MaxLength="30" CssClass="Inputtextbox"  Width="430px"></asp:TextBox><span style="height: 5px"><img alt="Req" src="../Images/Req_star.png"
                                            width="5px" height="5px" style="vertical-align: top;" /></span>
                                            <asp:CustomValidator
                                                ID="valResName" ControlToValidate="txtResName" runat="server" ValidationGroup="UpdateRes"
                                                OnServerValidate="valResName_ServerValidate" Display="Dynamic"></asp:CustomValidator><asp:RequiredFieldValidator
                                                    ID="rfvResName" ControlToValidate="txtResName" runat="server" Display="Dynamic"
                                                    ValidationGroup="UpdateRes"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                        ID="revResName" ControlToValidate="txtResName" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateRes"></asp:RegularExpressionValidator>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr class="Light_GrayFont">
                                                                                                    <td align="left" valign="middle" style="height: 35px;">
                                                                                                        &nbsp;配额
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" class="VR_line">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td align="left" valign="top">
                                      <asp:TextBox ID="txtAllQuota" runat="server" Width="250px" MaxLength="20" CssClass="Inputtextbox"></asp:TextBox><span style="height: 5px"><img alt="Req" src="../Images/Req_star.png"
                                            width="5px" height="5px" style="vertical-align: top;" /></span>
                                                <asp:RequiredFieldValidator  ID="rfv_AllQuota" ControlToValidate="txtAllQuota" runat="server" Display="Dynamic"
                                                    ValidationGroup="UpdateRes"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator
                                                        ID="rev_AllQuota" ControlToValidate="txtAllQuota" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateRes" ValidationExpression="^([1-9]\d{0,4})$|^(0|[1-9]\d{0,4})\.(\d{1,2})$"></asp:RegularExpressionValidator>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr class="Light_GrayFont">
                                                                                                    <td align="left" valign="middle" style="height: 35px;">
                                                                                                        &nbsp;其中彩色配额
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" class="VR_line">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td align="left" valign="top">
                                      <asp:TextBox ID="txtColorQuota" runat="server" Width="250px" MaxLength="20" CssClass="Inputtextbox"></asp:TextBox><span style="height: 5px"><img alt="Req" src="../Images/Req_star.png"
                                            width="5px" height="5px" style="vertical-align: top;" /></span>
                                      <asp:CustomValidator
                                                ID="cv_ColorQuota" ControlToValidate="txtColorQuota" runat="server" ValidationGroup="UpdateRes"
                                                Display="Dynamic" onservervalidate="cv_ColorQuota_ServerValidate"></asp:CustomValidator>
                                                 <asp:RequiredFieldValidator  ID="rfv_ColorQuota" ControlToValidate="txtColorQuota" runat="server" Display="Dynamic"
                                                    ValidationGroup="UpdateRes"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator
                                                        ID="rev_ColorQuota" ControlToValidate="txtColorQuota" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateRes" ValidationExpression="^([1-9]\d{0,4})$|^(0|[1-9]\d{0,4})\.(\d{1,2})$"></asp:RegularExpressionValidator>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr class="Light_GrayFont">
                                                                                                    <td align="left" valign="middle" style="height: 35px;">
                                                                                                        &nbsp;透支上限
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" class="VR_line">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td align="left" valign="top">
                                      <asp:TextBox ID="txtOverLimit" runat="server" Width="250px" MaxLength="20" CssClass="Inputtextbox"></asp:TextBox><span style="height: 5px"><img alt="Req" src="../Images/Req_star.png"
                                            width="5px" height="5px" style="vertical-align: top;" /></span>
                                      <asp:CustomValidator
                                                ID="cv_OverLimit" ControlToValidate="txtOverLimit" runat="server" ValidationGroup="UpdateRes"
                                                Display="Dynamic" onservervalidate="cv_OverLimit_ServerValidate"></asp:CustomValidator>
                                                
                                                 <asp:RequiredFieldValidator  ID="rfv_OverLimit" ControlToValidate="txtOverLimit" runat="server" Display="Dynamic"
                                                    ValidationGroup="UpdateRes"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator
                                                        ID="rev_OverLimit" ControlToValidate="txtOverLimit" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateRes" ValidationExpression="^([1-9]\d{0,4})$|^(0|[1-9]\d{0,4})\.(\d{1,2})$"></asp:RegularExpressionValidator>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr class="Light_GrayFont">
                                                                                                    <td align="left" valign="middle" style="height: 35px;">
                                                                                                        &nbsp;功能限制
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" class="VR_line">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td align="left" valign="top">
                                        <div style="z-index: 1; position: absolute; padding-top: 5px">
                                           
                                            <asp:CheckBoxList ID="chk_job_function" runat="server" 
                                                RepeatDirection="Horizontal" Width="243px">
                                            <asp:ListItem Value="1-2">彩色复印</asp:ListItem>
                                            <asp:ListItem Value="1-1">黑白复印</asp:ListItem>
                                            <asp:ListItem Value="2-2">彩色打印</asp:ListItem>
                                            <asp:ListItem Value="2-1">黑白打印</asp:ListItem>
                                            <asp:ListItem Value="6-1">扫描</asp:ListItem>
                                            <asp:ListItem  Value="8-1">传真</asp:ListItem>
                                        </asp:CheckBoxList>
                                
                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
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
                                                                    <!-- end of the Main table-->
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 50px;" align="center" valign="middle">
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="新配额追加" ValidationGroup="UpdateRes" CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                                                        ID="btnCancel" runat="server" Text="取消" CssClass="Login_Button_bg" OnClientClick="window.close(); return false" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left" valign="top" class="Login_right_bg">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <div class="Login_bottom_left_IMG" style="width: 17px; height: 22px;">
                                                        </div>
                                                    </td>
                                                    <td align="right" valign="top" class="Login_bottom_bg">
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <div class="Login_bottom_right_IMG" style="width: 17px; height: 22px">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 1%;" align="left" valign="top">
                                        </td>
                                    </tr>
                                </table>
                                <!--End of the Login Box shadows effect -->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
    </form>

    <script language="javascript" src="../Js/commonfoot.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        if (window.attachEvent) {
            document.forms[0].attachEvent("onsubmit", onsubmitscreen);
        }
        else {
            document.forms[0].addEventListener("onsubmit", onsubmitscreen, true);
        }
        if (window.attachEvent) {
            window.attachEvent("onload", function () { checkErrorInfo(); });
        }
        else {
            window.addEventListener("load", function () { checkErrorInfo(); }, true);
        }

        if (window.attachEvent) {
            window.attachEvent("onload", function () { Close_light(); });
        }
        else {
            window.addEventListener("load", function () { Close_light(); }, true);
        }

        /********************************************
        * FUNCTION : close_Alert
        * SUMMARY  : close alert message.
        * AUTHOR   : SES Ji JianXiong
        * DATE     : 2010.08.19
        * VERSION  : 0.01
        *******************************************/
        function close_Alert() {
            var id = "AlertMessage";
            if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) {
                // for the group select list item in IE 6.0
                $(".selectlist").each(function () {
                    var box = this;
                    box.style.display = "inline";

                });
            }
            document.getElementById(id).style.display = "none";
            document.getElementById("Lightbox").style.display = "none";
            // 2011.03.23 Add By SES Jijianxiong ST
            msgdisp = false;
            // 2011.03.23 Add By SES Jijianxiong ED
            window.close();
        }
    </script>

</body>

</html>
