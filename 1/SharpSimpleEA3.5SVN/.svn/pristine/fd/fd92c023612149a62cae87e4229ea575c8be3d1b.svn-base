<!--
// ==============================================================================
// File Name           : Main.aspx
// Description         : IC card Login Page
// Author(s)           : SES JiJianXiong
// Date created        : 2010.09.03
// Date updated        : 2010.12.28
//                       Html Standerd Modify
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
-->

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main"
    UICulture="Auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="Browser" content="NetFront" />
    <title>::Simple EA Application::</title>
    <link href="Css_mfp/style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function Login_ErrorShow()//Showing Error Meesgae
        {
            var spanmsg = document.getElementById("span_msg").value;
            if ( spanmsg != "" ) {
                document.getElementById("Error_meesageID").style.display = "inline";
                document.getElementById("span_msg_disp").innerText = spanmsg;
                setTimeout("hidediv('Error_meesageID')", 4000); // Time for closeing error message
            }
        } //end if
 
        function hidediv(arg) {
            document.getElementById(arg).style.display = 'none'; // Hidden error message after 4 sec
        }
    </script>

</head>
<body onkeydown="keydown(event);" onload="Login_ErrorShow();" leftmargin="0" topmargin="0"
    scroll="NO" class="bg">
    <form id="MFPForm" runat="server" class="osa_login">
        <asp:TextBox ID="InputID" runat="server" Style="display: none;"></asp:TextBox>
        <input type="button" value="µÇÂ¼" id="Login" runat="server" onclick="Login_Click()"
            style="display: none;" />
        <asp:TextBox ID="span_msg" runat="server" Style="display: none;" Text=""></asp:TextBox>
        <table width="800" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="left" valign="top" style="height: 48px" class="Header_bg">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 83%; height: 48px" align="left" valign="middle" class="Title_Font">
                                <!-- Title -->
                                &nbsp; Simple EA Application
                                <!--End of the Title -->
                            </td>
                            <td style="width: 17%" align="center" valign="middle">
                                <!-- Sharp Logo -->
                                <img src="Images_mfp/Sharp.png" width="112" height="21" alt="Sharp" />
                                <!--End of the Sharp Logo -->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="height: 2px" class="HR_Line">
                </td>
            </tr>
            <tr>
                <td style="height: 318px" align="left" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 318px">
                        <tr>
                            <td style="height: 30px" align="left" valign="top">
                                <table runat="server" id="Input_disp" width="100%" style="height: 75px" border="0"
                                    cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 78%; height: 40px" align="left" valign="top">
                                        </td>
                                        <td style="width: 22%" align="left" valign="bottom">
                                            <!-- Direct Login Button -->
                                            <asp:ImageButton runat="server" ID="btnInput" OnClick="btnInput_Click" 
                                                ImageUrl="Images_mfp/DirectLogin.png" TabIndex="1" />
                                            <!--End of the Direct Login Button -->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 35px" align="left" valign="top">
                                        </td>
                                        <td align="left" valign="top">
                                            <!-- Direct Button glass effect -->
                                            <img src="Images_mfp/DirectLogin_showdow.png" width="142" alt="DirectLogin" />
                                            <!--End of the Direct Button glass effect -->
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 247px" align="left" valign="top">
                                <div runat="server" id="div_ICCard" class="MFPCSS">

                                    <script type="text/javascript" language="javascript">
                    <!--
                    function keydown(e)
                    {
                        var c = e.keyCode;
                                                //2013.10.15
                          if(String.fromCharCode(e.keyCode).toString() =="\r\n")
                        {
                            c="";
                        }
                       
                        document.getElementById("InputID").value += String.fromCharCode(c);
                        
                        var strInput = document.getElementById("InputID").value;
                        document.getElementById("InputID").value = strInput.substring(0, <%=UtilCommon.GetICCardLen %>);
                         //END
                        if ( strInput.length == <%=UtilCommon.GetICCardLen %> ){
                            var btn = document.getElementById("Login");
                            btn.click();
                        }
                    }

                    function Login_Click() {
                        document.getElementById("status").value = "Login_Click";
                        document.forms[0].submit();
                    }
                    //-->
                                    </script>

                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 223px">
                                        <tr>
                                            <td style="width: 4%; height: 223px" align="left" valign="top">
                                            </td>
                                            <td style="width: 90%" align="center" valign="bottom" class="IC_cardbg">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 223px">
                                                    <tr>
                                                        <td style="height: 27px" align="right" valign="top">
                                                            <!--Error Message Div tag -->
                                                            <div style="display: none; vertical-align: bottom;" align="center" id="Error_meesageID">
                                                                <table width="50%" border="0" cellpadding="0" cellspacing="0" style="height: 27px">
                                                                    <tr>
                                                                        <td style="height: 27px; width: 9px">
                                                                            <img src="Images_mfp/Error_left.png" width="9px" height="27px" alt="Error" />
                                                                        </td>
                                                                        <td align="center" valign="middle" class="Error_Message">
                                                                            <!--Error Icon and Error Message -->
                                                                            <img src="Images_mfp/error_icon1.gif" width="13" height="13" alt="error" /><span
                                                                                id="span_msg_disp"></span>
                                                                            <!--End of the Error Icon and Error Message -->
                                                                        </td>
                                                                        <td style="height: 27px; width: 9px">
                                                                            <img src="Images_mfp/Error_right.png" width="9" height="27" alt="error" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <!--End of the Error Message Div tag -->
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 168px" align="center" valign="bottom">
                                                            <!--IC Crad Image -->
                                                            <img src="Images_mfp/IC_cardIMG.png" width="505" height="168" vspace="25" alt="IC Card" />
                                                            <!--End of the IC Crad Image -->
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%" align="left" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left" valign="top">
                                            </td>
                                            <td align="left" valign="top" class="IC_cardbg_showdow">
                                                <!--IC Crad Background glass effect -->
                                            </td>
                                            <td align="left" valign="top">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div runat="server" id="div_Input" class="MFPCSS">

                                    <script type="text/javascript" language="javascript">
                    <!--
                    function keydown(e)
                    {
                    }
                    //-->
                                    </script>

                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 4%; height: 223px" align="left" valign="top">
                                            </td>
                                            <td style="width: 90%" align="center" valign="bottom" class="IC_cardbg">
                                                <div class="Login_div">
                                                    <!-- Main Login Div  -->
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 219px">
                                                        <tr>
                                                            <td style="height: 40px" align="left" valign="top">
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 40px">
                                                                    <tr>
                                                                        <td align="center" valign="middle" style="width: 10%">
                                                                            <!-- Top Login User Icon -->
                                                                            <img src="Images_mfp/Login_user.png" width="32" height="32" vspace="5" alt="Loginuser" />
                                                                            <!-- End of the Top Login User Icon -->
                                                                        </td>
                                                                        <td style="width: 24%" class="Login_whiteFont">
                                                                            <!-- Login Text -->
                                                                            µÇÂ¼
                                                                        </td>
                                                                        <td style="width: 66%" align="right" class="Login_MsgFont">
                                                                            <!-- Enter User Name and Password Text -->
                                                                            ÇëÊäÈëµÇÂ¼ÃûºÍÃÜÂë &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top" style="height: 2px" class="HR_Line">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 179px">
                                                                    <tr>
                                                                        <td style="width: 23%" align="right" valign="middle">
                                                                            <!-- Login User Icon big size -->
                                                                            <img src="Images_mfp/User_IMG.png" width="113" height="151px" alt="User" />
                                                                            <!--End of the Login User Icon big size -->
                                                                        </td>
                                                                        <td style="width: 77%" align="left" valign="top">
                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 179px">
                                                                                <tr>
                                                                                    <td colspan="2" align="center" valign="bottom" style="height: 35px">
                                                                                        <!--Error Message Div tag -->
                                                                                        <div style="display: none; vertical-align: bottom;" id="Error_meesageID">
                                                                                            <table width="50%" border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td style="height: 27px; width: 9px">
                                                                                                        <img src="Images_mfp/Error_left.png" width="9" height="27" alt="" />
                                                                                                    </td>
                                                                                                    <td align="center" valign="middle" class="Error_Message">
                                                                                                        <!--Error Icon and Error Message -->
                                                                                                        <img src="Images_mfp/error_icon1.gif" width="13" height="13" alt="error" /><span
                                                                                                            id="span_msg_disp"></span>
                                                                                                        <!--End of the Error Icon and Error Message -->
                                                                                                    </td>
                                                                                                    <td style="height: 27px; width: 9px">
                                                                                                        <img src="Images_mfp/Error_right.png" style="height: 27px; width: 9px" alt="" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                        <!--End of the Error Message Div tag -->
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" valign="middle" class="Login_whiteFont" style="height: 43px; width: 33%">
                                                                                        <!--User Name Text -->
                                                                                        µÇÂ¼Ãû &nbsp;
                                                                                    </td>
                                                                                    <td style="height: 43px; width: 67%" align="left" valign="middle">
                                                                                        <!--User Name Input box -->
                                                                                        <asp:TextBox ID="txtLoginName" runat="server" MaxLength="30" TabIndex="0" 
                                                                                            CssClass="Inputtextbox" Height="35px"></asp:TextBox>
                                                                                        <!--End of the User Name Input box -->
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" valign="middle" class="Login_whiteFont" style="height: 43px;">
                                                                                        <!--Password Text -->
                                                                                        ÃÜÂë &nbsp;
                                                                                    </td>
                                                                                    <td align="left" valign="middle">
                                                                                        <!--Password Input box -->
                                                                                        <asp:TextBox ID="txtPassword" TextMode="Password" keyboard="password" format='password'
                                                                                            runat="server" MaxLength="30" TabIndex="1" CssClass="Inputtextbox" Height="35px"></asp:TextBox>
                                                                                        <!--End of the Password Input box -->
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" align="center" valign="middle" style="height: 48px;">
                                                                                        <!--Login Button -->
                                                                                        <asp:ImageButton runat="server" ID="btnLogIn" OnClick="btnLogIn_Click" ImageUrl="Images_mfp/CMD_login.png" />
                                                                                        <!--End of the Login Button -->
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                            <td style="width: 6%" align="left" valign="top">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left" valign="top">
                                            </td>
                                            <td align="left" valign="top" class="IC_cardbg_showdow">
                                                <!--IC Crad Background glass effect -->
                                            </td>
                                            <td align="left" valign="top">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="status" Value="" runat="server" />
    </form>
</body>
</html>
