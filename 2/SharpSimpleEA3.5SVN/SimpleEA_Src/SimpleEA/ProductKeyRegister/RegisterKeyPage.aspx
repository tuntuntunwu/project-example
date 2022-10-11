<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterKeyPage.aspx.cs" Inherits="RegisterKeyPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Language" content="zh-cn" />
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312" />
    <title>::Simple EA Application :: ErrorPage</title>
    <link href="../Css/style_error.css" rel="stylesheet" type="text/css" />
    <!--[if IE 6]>
		<link rel="stylesheet" href="Css/IE_error.css" type="text/css" />
	<![endif]-->

    <script type="text/javascript" language="javascript">

        function bgLoad()//Background image loading and auto resize
        {

            var detected_os = navigator.userAgent.toLowerCase();
//            if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) {
//                document.write(" <div id='content' style='position:relative; z-index:1;padding:10px;'>");

//            }
//            else {
                document.write("<div id='page-background' style='position:fixed; top:0; left:0; width:100%; height:100%;'><img src='../Images/BG.jpg' width='100%' height='100%'/></div>");
                document.write(" <div id='content' style='position:relative; z-index:1; padding:10px;'>");

//            }
        }
        
    </script>

</head>
<body leftmargin="0" topmargin="0" scroll="no">
    <form id="frmLogin" runat="server">
    <script language="javascript" type="text/javascript">        bgLoad();</script>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 47px;" align="left" valign="top">
                <table border="0" cellpadding="0" cellspacing="0" class="tablefixed" style="height: 25px;"
                    width="100%">
                    <tr>
                        <!-- Application Logo -->
                        <td align="left" valign="bottom" class="TitleFont">
                            <%--<div class="IMG_1" style="width: 375px; height: 41px;">
                                </div>--%>
                            Simple EA Application
                        </td>
                        <td>
                        </td>
                        <td align="center" class="TitleLogo" valign="middle">
                            <!-- Sharp Logo-->
                            <div class="Sharp_Logo" style="width: 106px; height: 16px;">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" class="HR_Line" style="height: 2px">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="height: 20px">
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" style="height: 48px">
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <table width="97%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" valign="top" style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" style="width: 1%">
                                    </td>
                                    <td align="left" valign="top" style="width: 98%">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="right" valign="top" style="width: 1%">
                                                    <div class="Login_left_IMG" style="width: 100%; height: 39px">
                                                    </div>
                                                </td>
                                                <td align="left" valign="middle" class="Login_Topcenter" style="width: 98%">
                                                    注册页面
                                                </td>
                                                <td align="left" valign="top" style="width: 1%">
                                                    <div class="Login_Right_IMG" style="width: 100%; height: 39px">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top" class="Login_left_bg">
                                                </td>
                                                <td align="left" valign="top" class="Login_centerbg" style="height: 500px;">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="left" valign="top">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="top" style="height: 208px">
                                                                <br />
                                                                <asp:Label ID="labelMessage" Text="" runat="server" />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <asp:TextBox ID="txtRegisterKey" runat="server" Width="400px" MaxLength="20" CssClass="Inputtextbox SearchName" />
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="Label1" Height="20px" Width="500px" runat="server" Text="试用注册码为:AAAA-AAAAAA-AAAAAAAA. 使用期限为30天."></asp:Label>
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <asp:Button ID="btnUpdate" runat="server" Text="确认" OnClick="btnUpdate_Click" CssClass="Login_Button_bg" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnCancel" runat="server" Text="取消" OnClick="btnCancel_Click" CssClass="Login_Button_bg" />
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="LabelTips" Visible="false" CssClass="Error_Message" Font-Size="Medium"
                                                                    Height="20px" Width="400px" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <!-- end of the Main table-->
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 50px" align="center" valign="middle">
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
                                    <td align="left" valign="top" style="width: 1%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 55px" align="left" valign="top">
                                    </td>
                                    <td align="left" valign="top">
                                        <!-- Login Box shadows effect -->
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 3%; height: 55px" align="left" valign="top" class="Login_left_shadow">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 94%" align="center" valign="middle" class="Login_bottom_shoadow">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 3%" align="left" valign="top" class="Login_right_shadows">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="left" valign="top">
                                    </td>
                                </tr>
                            </table>
                            <!--End of the Login Box shadows effect -->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="height: 2px" class="HR_Line_copyright">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="height: 40px">
                <!-- Copy right Location -->
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height: 40px; width: 61%" align="left" valign="middle" class="Copyright">
                            &nbsp;&nbsp;&nbsp;&nbsp;&copy; 夏普商贸（中国）有限公司
                        </td>
                        <td style="width: 39%" align="right" valign="bottom" class="WhiteFont">
                            <div class="SharpLogo_Insidepage" style="width: 109px; height: 33px;">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
