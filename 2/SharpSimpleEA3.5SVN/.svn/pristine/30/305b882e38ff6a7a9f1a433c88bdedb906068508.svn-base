<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterCardSuccess.aspx.cs" Inherits="MFPScreen_RegisterCardSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta name="Browser" content="NetFront" />
    <title>::Simple EA Application :: Print Details</title>
    <link href="../Css_mfp/style.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" language="javascript">
        setInterval(function () {
            window.location = document.getElementById('<%=hidType.ClientID %>').value;
        }, 2000);
    </script>
</head>
<body class="DetailsBG" leftmargin="0" topmargin="0" scroll="NO" >
    <form id="frmMain" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" 
            class="MFP_table"  style="float: none; height:100%;">
            <tr>
                <td align="left" valign="top" style="height:100px" class="Header_bg">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 83%; height: 48px" align="left" valign="middle" class="Title_Font">
                                <!-- Title -->
                                &nbsp; Simple EA Application
                                <!--End of the Title -->
                            </td>
                            <td style="width: 17%" align="center" valign="middle">
                                <!-- Sharp Logo -->
                                <img src="../Images_mfp/Sharp.png" width="112" height="21" alt="Sharp" />
                                <!--End of the Sharp Logo -->
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" style="height:85%">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" runat="server" id="mainTable">
                        <tr>
                            <td align="left" runat="server" id="leftTd" valign="top" style="width: 15%; border-right:MFPVR_line; height:100%;">
                                
                            </td>
                            <%--<td align="center" valign="top" class="VR_line" style="width: 2px; border:2px;">
                            </td>--%>
                            <td align="left" valign="top" style="width: 70%; height:100%;">
                                <asp:TextBox ID="InputID" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="span_msg" runat="server" Style="display: none;" Text=""></asp:TextBox>
                                
                                <table style="width: 100%; height: auto; table-layout: fixed;" cellpadding="0" cellspacing="0"
                                    class="Invalid_License">
                                    <tr style="height: 280px">
                                        <td style="width: 98%;" align="center" valign="middle" class="TableGrid_bg">
                                            <table>
                                                <tr>
                                                    <td style="width: 98%;" align="center" valign="middle">
                                                        <asp:Label ID="lblTitle" Text="IC卡注册成功！" runat="server" Font-Size="Medium">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 65px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 98%;" align="right" valign="middle" class="TableGrid_bg">
                                                        <asp:ImageButton runat="server" ID="imgBtnFlash" OnClick="imgBtnBack_Click" ImageUrl="../Images_mfp/9.png"
                                                            Height="66px" Width="160px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 98%; height: auto;" align="right" valign="middle" class="TableGrid_bg">
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hidType" Value="" runat="server" />
                                <!-- end of the Main table-->
                            </td>
                            <%--<td align="center" valign="top" class="VR_line" style="width: 2px; border:2px;">
                            </td>--%>
                            <td id="rigthTd" runat="server" style="width: 15%; height:100%; border-left:MFPVR_line;" align="left" valign="top">
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
