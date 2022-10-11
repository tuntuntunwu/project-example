<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecordCard.aspx.cs" Inherits="MFPScreen_RecordCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta name="Browser" content="NetFront" />
    <title>::Simple EA Application :: Print Details</title>
    <link href="../Css_mfp/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../Js/MFP_common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Login_ErrorShow()//Showing Error Meesgae
        {
            var spanmsg = document.getElementById("span_msg").value;
            if (spanmsg != "") {
                document.getElementById("Error_meesageID").style.display = "inline";
                document.getElementById("span_msg_disp").innerText = spanmsg;
                setTimeout("hidediv('Error_meesageID')", 4000); // Time for closeing error message
            }
        } //end if

        function hidediv(arg) {
            document.getElementById(arg).style.display = 'none'; // Hidden error message after 4 sec
        }

        setInterval(function () {
            var result = record_session(document.getElementById('<%=hidRecordTime.ClientID %>').value, "<%=timeOutPeriod%>");
            if (result == "timeout")
                document.location = document.getElementById('<%=hidUrlType.ClientID %>').value;
        }, 60000);

      
    </script>
    <script type="text/javascript" language="javascript">
                    
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
                    
    </script>
</head>
<body class="DetailsBG" onkeydown="keydown(event);" onload="Login_ErrorShow();" leftmargin="0" topmargin="0" scroll="NO" >
    <form id="frmMain" runat="server">
    <asp:HiddenField ID="OSAType" runat="server" />
    <asp:HiddenField ID="hidRecordTime" runat="server" Value="" />
    <asp:HiddenField ID="hidUrlType" runat="server" Value="" />
        <table width="100%" border="0" cellpadding="0" cellspacing="0"
             style="float: none; height:100%;">
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
                            <td align="left" runat="server" id="leftTd" valign="top" style="width: 15%; border-right:MFPVR_line; height:50px;">
                                
                            </td>
                            <%--<td align="center" valign="top" class="VR_line" style="width: 2px; border:2px;">
                            </td>--%>
                            <td align="left" valign="top" style="width: 70%; height:400px;">
                                <asp:TextBox ID="InputID" runat="server" Style="display: none;"></asp:TextBox>
                                <input type="button" value="登录" id="Login" runat="server" onclick="Login_Click()"
                                    style="display: none;" />
                                <asp:TextBox ID="span_msg" runat="server" Style="display: none;" Text=""></asp:TextBox>
                                <table style="width: 100%; height: auto; table-layout: fixed;" cellpadding="0" cellspacing="0" class="Invalid_License">
                                    <tr style="height: 280px">
                                        <td style="width: 98%;" align="center" valign="middle" class="TableGrid_bg">
                                            <table>
                                                <tr>
                                                    <td  class="Record_Card">
                                                    </td>
                                                    <td style="width:auto;" align="center" valign="middle">
                                                        <asp:Label ID="lblTitle" Text="请刷您需要进行注册的IC卡" runat="server" Font-Size="Large">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 35px">
                                                    </td>
                                                    <td style="height: 35px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 65px;">
                                                    </td>
                                                    <td style="width:auto" align="right" valign="middle" class="TableGrid_bg">
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
                                <asp:HiddenField ID="status" Value="" runat="server" />
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
