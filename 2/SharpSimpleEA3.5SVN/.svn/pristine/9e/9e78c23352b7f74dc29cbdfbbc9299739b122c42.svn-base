<%--
// ==============================================================================
// File Name           : RestrictList.aspx
// Description         : Restriction Set List Information Page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.06.07
// Date updated        : 2010.09.07
//                       Build No: 1.0.3.2: UI Update.
//                     : 2010.12.16
//                       Ver.1.1 Update
//                       2010.12.29
//                       Html Standerd Modify
//                       2011.01.05
//                       Html Standerd Modify
//                       2011.01.07
//                       Html Standerd Modify
//                       2011.01.20
//                       Ln90: width:100%->width:110%

// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="Settings.aspx.cs" Inherits="Settings_Settings" Title="Settings" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>

<asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
    <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link_select"  href="../Settings/Settings.aspx">系统设置</a></span> 
    /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
    /<span id="Span1"><a id="A1" runat="server" class="small_link" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
    /<span id="Span2"><a id="A2" runat="server" class="small_link" href="../Settings/ImportUserInfor.aspx">用户信息导入</a></span>
    /<span id="Span3"><a id="A3" runat="server" class="small_link" href="../Settings/ServerIPSetting.aspx">ServerIP设置</a></span>

    /<span id="Span4"><a id="A4" runat="server" class="small_link" href="../Settings/ContentBackupSetting.aspx">内容留底设置</a></span>
    /<span id="Span10"><a id="A10" runat="server" class="small_link" href="../Settings/LDAPSetting.aspx">LDAP认证设置</a></span>
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

        window.onload = function() {
            var doc = document;
            if ( doc.getElementById("Scroller-Set") ) {
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
        $(document).ready(function(){
            $(".radio").dgStyle();
             grid_resize();
        });
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
                        <td align="left" valign="top" style="height: 394px; width: 97%" class="Scroll_td">
                        
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="7">
                                                    &nbsp;配额周期
                                                </td>
                                            </tr>
                                            <tr id="tr_date" class="Light_GrayFont" style="height: 109px">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    日期</td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td colspan="3" valign="middle" style="width: 890px">
                                                    <table width="110%" border="0" cellpadding="0" cellspacing="0" style="height: 109px">
                                                        <tr>
                                                            <td align="center" valign="middle" style="width: 50px">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoMonth" GroupName="ResetGroup" Checked="true" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle" style="width: 30px">
                                                                每月</td>
                                                            <td align="left" valign="top" style="padding-left: 10px; height: 35px; width: auto;">
                                                                <div style="z-index: 14;  padding-top: 5px">
                                                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="changeMe" Width="120px">
                                                                    </asp:DropDownList></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" class="HR_Line" style="height: 2px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="middle">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoWeek" GroupName="ResetGroup" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                每周</td>
                                                            <td align="left" valign="top" style="padding-left: 10px; height: 35px;">
                                                                <div style="z-index: 12;  padding-top: 5px">
                                                                    <asp:DropDownList ID="ddlWeek" runat="server" CssClass="changeMe" Width="120px">
                                                                    </asp:DropDownList></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" class="HR_Line" style="height: 2px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" style="height: 35px">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoDay" GroupName="ResetGroup" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                每天</td>
                                                            <td align="left" valign="middle">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" class="HR_Line" style="height: 2px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" style="height: 35px">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoNoLimit" GroupName="ResetGroup" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle">配额用完为止</td>
                                                            <td align="left" valign="middle">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" class="HR_Line" style="height: 2px">
                                                </td>
                                            </tr>
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle">
                                                    时间</td>
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        <asp:DropDownList ID="ddlHour" runat="server" CssClass="changeMe" Width="120px">
                                                        </asp:DropDownList></div>
                                                </td>
                                                <td align="left" valign="middle" style="width: 10px;">
                                                    &nbsp;
                                                </td>
                                                <td align="left" valign="middle">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="HR_Line" colspan="5" style="height: 2px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                                    &nbsp;关于统计方式的设定
                                                </td>
                                            </tr>
                                            <tr id="TR6" class="Light_GrayFont" style="height: 70px">
                                                <td align="left" valign="middle">
                                                    统计方式</td>
                                                <td align="left" valign="top" class="VR_line">
                                                </td>
                                                <td colspan="3" valign="middle">
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="center" valign="middle" style="width: 50px">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoMoneyCount" GroupName="BorrowGroup" 
                                                                        Checked="true" AutoPostBack="True" 
                                                                        oncheckedchanged="rdoMoneyCount_CheckedChanged" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle" style="width: 90px">
                                                                金额</td>
                                                            <td align="left" valign="middle" style="padding-left: 10px; width: auto; height: 35px">
                                                                <asp:CheckBox ID="chk_Money" runat="server" Text="A3以A4两张记" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" class="HR_Line" style="height: 2px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" valign="middle">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoPaperCount" GroupName="BorrowGroup" 
                                                                        AutoPostBack="True" oncheckedchanged="rdoPaperCount_CheckedChanged" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                面数</td>
                                                            <td align="left" valign="middle" style="padding-left: 10px; height: 35px">
                                                                <asp:CheckBox ID="chk_Paper" runat="server" Text="A3以A4两张记" />
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
                                                    &nbsp;日志查询显示记录数设定
                                                </td>
                                            </tr>
                                            <tr id="TR7" class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    最多显示</td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px">
                                                </td>
                                                <td style="padding-left: 10px; height: 35px;" valign="top" align="left">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        <asp:DropDownList ID="ddlAvailBorrow" runat="server" CssClass="changeMe" Width="200px">
                                                            <asp:ListItem Value="0" Text="无限制" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="1000" Text="1000条记录"></asp:ListItem>
                                                            <asp:ListItem Value="2000" Text="2000条记录"></asp:ListItem>
                                                            <asp:ListItem Value="5000" Text="5000条记录"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                                <td valign="middle" align="left">
                                                    &nbsp;
                                                </td>
                                                <td valign="middle" align="left">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <!-- 认证方式 -->
                                            <tr>
                                                <td colspan="5" class="HR_Line" style="height: 2px">
                                                </td>
                                            </tr>

                                            <tr id="TR8" class="Light_GrayFont" style="height: 109px">
                                                <td align="left" valign="middle" style="width: 200px">
                                                    认证方式</td>
                                                <td align="left" valign="top" class="VR_line" style="width: 2px;">
                                                </td>
                                                <td colspan="3" valign="middle" style="width: 890px">
                                                    <table width="110%" border="0" cellpadding="0" cellspacing="0" style="height: 109px">
                                                        <tr>
                                                            <td align="right" valign="middle" style="height: 35px">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoSystem" GroupName="AuthGroup" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle">
                                                                本系统</td>
                                                            <td align="left" valign="middle">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" class="HR_Line" style="height: 2px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" style="height: 35px">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoLDAP" GroupName="AuthGroup" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle">域认证</td>
                                                            <td align="left" valign="middle">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" class="HR_Line" style="height: 2px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" valign="middle" style="height: 35px">
                                                                <div class="radio">
                                                                    <asp:RadioButton runat="server" ID="rdoDBAuth" GroupName="AuthGroup" />
                                                                </div>
                                                            </td>
                                                            <td align="left" valign="middle">第三方数据库认证</td>
                                                            <td align="left" valign="middle">&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" class="HR_Line" style="height: 2px">
                                                </td>
                                            </tr>
                                            <!-- -->

                                        </table>
                                
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
    <asp:Button ID="btnApply" runat="server" Text="设定" CssClass="Login_Button_bg" OnClick="btnApply_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnReset"    runat="server" Text="重新设定" CssClass="Login_Button_bg" OnClick="btnReset_Click" />

    <script type="text/javascript" src="../js/Min_radio.js"></script>

    <script type="text/javascript" src="../js/Min_checkbox.js"></script>

    <script src="../js/commonfoot.js" type="text/javascript">
       
    </script>

</asp:Content>
