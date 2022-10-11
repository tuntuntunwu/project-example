<%--
// ==============================================================================
// File Name           : Password 
// Description         : Password Modify in  SimpleEA while the user is admin
// Author(s)           : Ji Jianxiong
// Date created        : 2011.03.28
// Date updated        : 2011.03.28
//                       Html Standerd Modify
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2011, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Password.aspx.cs" Inherits="Password_Password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Language" content="zh-cn" />
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <meta http-equiv="Pragma" content="no-cache" />
    <base target="_self" />
    <title>::Simple EA Application :: 管理员密码修改</title>
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Css/common.css" />
    <!--[if IE 6]>
		<link rel="stylesheet" href="../Css/IE.css" type="text/css" />
		
   <![endif]-->
    <link rel="stylesheet" type="text/css" href="../Css/popstyle.css" />

    <script language="javascript" src="../Js/common.js" type="text/javascript"></script>

    <script type="text/javascript" src="../JavaScript/jquery.min.js"></script>
</head>
<body leftmargin="0" topmargin="0" style="min-width: auto; min-height: auto;">
    <form id="frmMain" runat="server" >

        <script language="javascript" type="text/javascript">bgLoad();</script>

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
                        <input type="button" id="AlertMessageOK" class="Login_Button_bg" value="确定" onclick="close_Alert();" />
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
                                                                    管理员密码编辑
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
                                                                                                        &nbsp;原密码
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" class="VR_line">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" style="width: 450px">
                                                                                                        &nbsp;<asp:TextBox ID="txtOldPassword" runat="server" Width="140px" MaxLength="10"
                                                                                                            TextMode="Password" CssClass="Inputtextbox"></asp:TextBox><img alt="Req" src="../Images/Req_star.png"
                                                                                                                width="5px" height="5px" style="vertical-align: top;" /><asp:CustomValidator ID="valOldPassword"
                                                                                                                    ControlToValidate="txtOldPassword" runat="server" ValidationGroup="UpdateSet"
                                                                                                                    OnServerValidate="valOldPassword_ServerValidate" Display="Dynamic"></asp:CustomValidator><asp:RequiredFieldValidator
                                                                                                                        ID="rfvOldPassword" ControlToValidate="txtOldPassword" runat="server" Display="Dynamic"
                                                                                                                        ValidationGroup="UpdateSet"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                                                            ID="revOldPassword" ControlToValidate="txtOldPassword" Display="Dynamic" runat="server"
                                                                                                                            ValidationGroup="UpdateSet" ValidationExpression="^[a-zA-Z0-9]{4,10}$"></asp:RegularExpressionValidator></td>
                                                                                                </tr>
                                                                                                <tr class="Light_GrayFont">
                                                                                                    <td align="left" valign="middle" style="height: 35px;">
                                                                                                        &nbsp;新密码
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" class="VR_line">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td align="left" valign="top">
                                                                                                        &nbsp;<asp:TextBox ID="txtPassword" runat="server" Width="140px" MaxLength="10" TextMode="Password"
                                                                                                            CssClass="Inputtextbox"></asp:TextBox><img alt="Req" src="../Images/Req_star.png"
                                                                                                                width="5px" height="5px" style="vertical-align: top;" /><asp:RequiredFieldValidator
                                                                                                                    ID="rfvPassword" ControlToValidate="txtPassword" runat="server" Display="Dynamic"
                                                                                                                    ValidationGroup="UpdateSet"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                                                                                        ID="revPassword" ControlToValidate="txtPassword" Display="Dynamic" runat="server"
                                                                                                                        ValidationGroup="UpdateSet" ValidationExpression="^[a-zA-Z0-9]{4,10}$"></asp:RegularExpressionValidator></td>
                                                                                                </tr>
                                                                                                <tr class="Light_GrayFont">
                                                                                                    <td align="left" valign="middle" style="height: 35px;">
                                                                                                        &nbsp;确认新密码
                                                                                                    </td>
                                                                                                    <td align="left" valign="top" class="VR_line">
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td align="left" valign="top">
                                                                                                        &nbsp;<asp:TextBox ID="txtPasswordConfirm" runat="server" Width="140px" MaxLength="10"
                                                                                                            CssClass="Inputtextbox" TextMode="Password"></asp:TextBox><img alt="Req" src="../Images/Req_star.png"
                                                                                                                width="5px" height="5px" style="vertical-align: top;" /><asp:RequiredFieldValidator
                                                                                                                    ID="rfvPasswordConfirm" ControlToValidate="txtPasswordConfirm" runat="server"
                                                                                                                    Display="Dynamic" ValidationGroup="UpdateSet"></asp:RequiredFieldValidator><asp:CompareValidator
                                                                                                                        ID="cpvPasswordConfirm" runat="server" ValidationGroup="UpdateSet" ControlToCompare="txtPassword" Display="Dynamic"
                                                                                                                        ControlToValidate="txtPasswordConfirm" Operator="Equal"></asp:CompareValidator></td>
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
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="设定" ValidationGroup="UpdateSet" CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                                                        ID="btnCancel" runat="server" Text="关闭" CssClass="Login_Button_bg" OnClientClick="window.close(); return false" />
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
       if ( window.attachEvent ) 
       {            
         document.forms[0].attachEvent("onsubmit" , onsubmitscreen);
       }
       else
       {
         document.forms[0].addEventListener("onsubmit",onsubmitscreen ,true);
       }
        if ( window.attachEvent)  { 
            window.attachEvent("onload",function() {checkErrorInfo();});
        }
        else {
            window.addEventListener("load",function() {checkErrorInfo();},true);
        }

        if ( window.attachEvent)  { 
            window.attachEvent("onload",function() {Close_light();});
        }
        else {
            window.addEventListener("load",function() {Close_light();},true);
        }
        
          /********************************************
           * FUNCTION : close_Alert
           * SUMMARY  : close alert message.
           * AUTHOR   : SES Ji JianXiong
           * DATE     : 2010.08.19
           * VERSION  : 0.01
           *******************************************/
          function close_Alert() 
          {
              var id = "AlertMessage";
              if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
              {
                  // for the group select list item in IE 6.0
                  $(".selectlist").each(function()
                  {
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
