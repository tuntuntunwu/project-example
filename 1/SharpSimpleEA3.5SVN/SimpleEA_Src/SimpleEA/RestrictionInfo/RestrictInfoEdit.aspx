<%--
// ==============================================================================
// File Name           : RestrictInfoEdit.aspx
// Description         : Edit Restriction Set Information Page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.06.15
// Date updated        : 2010.09.07
//                       Build No: 1.0.3.2: UI Update.
//                       2010.12.20
//                       Build No: 1.1 Update.
//                       2010.12.27
//                       Html Standerd Modify
//                       2010.12.28
//                       Html Standerd Modify
//                       2011.01.06
//                       Html Standerd Modify
//                       2011.01.07
//                       Html Standerd Modify
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="RestrictInfoEdit.aspx.cs" Inherits="RestrictionInfo_RestrictInfoEdit"
    Title="RestrictInfoEdit" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="cphSmallbody" runat="server">

    <script type="text/javascript" src="../JavaScript/jquery.custom_radio_checkbox.js"></script>

    <script type="text/javascript" language="javascript">
        window.onload = function() {
            var doc = document;
            if ( doc.getElementById("Scroller-Res") ) {
                loadScriptScroller(function () {
                    var scroller = null;
                    var scrollbar = null;
                    // 2011.01.07 Update By SES Zhou Miao Ver.1.1 Update ST
//                    // 2011.01.06 Update By SES Zhou Miao Ver.1.1 Update ST
//                    //scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400, -100);
//                     var version = navigator.appVersion;
//                     if ((version.indexOf('MSIE 7.0') != -1) ||(version.indexOf('MSIE 6.0') != -1) || (version.indexOf('MSIE 5.5') != -1))
//                      {
//                           scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400,  -138);
//                      }
//                     else
//                     {
//                           scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400,  -100);
//                     }
//                    // 2011.01.06  Update By SES Zhou Miao Ver.1.1 Update ED
                    var version = navigator.appVersion;
                    if (version.indexOf('MSIE 7.0') != -1)
                    {
                        scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400,  -135);
                    }
                    else if((version.indexOf('MSIE 6.0') != -1) || (version.indexOf('MSIE 5.5') != -1))
                    {
                        scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400,  -118);
                    }
                   else
                    {
                        scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400,  -89);
                    }

                    // 2011.01.07 Update By SES Zhou Miao Ver.1.1 Update ED
                    
                    scrollbar = new jsScrollbar(doc.getElementById("Scrollbar-Container"), scroller, false);
                    scroller = null;
                    scrollbar = null;
                });
            }
        }
        
        $(document).ready(function(){
            // 2010.11.23 Add By SES Jijianxiong Ver.1.1 Update ST
            // Bug in the IE 6
            var version = navigator.appVersion;
            if ((version.indexOf('MSIE 6.0') != -1) || (version.indexOf('MSIE 5.5') != -1)) {
                // $(".radio").dgStyle();
                $(".toggle_container1").show();
                $("h2.trigger").addClass("active"); 
            }
            // 2010.11.23 Add By SES Jijianxiong Ver.1.1 Update ED

            // setting radio button
            $(".radio").dgStyle();
            // 2011.1.5 Add By SES zhoumiao Ver.1.1 Update ST
            grid_resize();
            // 2011.1.5 Add By SES zhoumiao Ver.1.1 Update ED
        });
    </script>

    <script type="text/javascript" language="javascript">
        loadScript("../js/Collaped_expand.js");
        loadScript("../js/Min_res_grid.js");
        loadScript("../js/Min_radio_res.js");
        loadScript("../js/Min_resize.js");
    </script>

   
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" runat="server">
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
            <td align="left" valign="top">
                <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 97%;" align="left" valign="top">
                            <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 27%; height: 14px;" align="left" valign="middle" 
                                        class="Add_User_Titlebg">
                                        &nbsp;配额信息
                                    </td>
                                    <td style="width: 0%; height: 14px;" align="left" valign="top" 
                                        class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                    <td style="width: 73%; height: 14px;" align="left" valign="top" 
                                        class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                </tr>


                                <tr id="TR_ID1" class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;名称
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle">
                                    <asp:TextBox ID="txtResName" runat="server" MaxLength="30" CssClass="Inputtextbox"
                    Width="430px"></asp:TextBox><span style="height: 35px"><img alt="Req" src="../Images/Req_star.png"
                        width="5px" height="5px" style="vertical-align: top;" /></span><asp:CustomValidator
                            ID="valResName" ControlToValidate="txtResName" runat="server" ValidationGroup="UpdateRes"
                            OnServerValidate="valResName_ServerValidate" Display="Dynamic"></asp:CustomValidator><asp:RequiredFieldValidator
                                ID="rfvResName" ControlToValidate="txtResName" runat="server" Display="Dynamic"
                                ValidationGroup="UpdateRes"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                    ID="revResName" ControlToValidate="txtResName" Display="Dynamic" runat="server"
                                    ValidationGroup="UpdateRes"></asp:RegularExpressionValidator></td>
                                </tr>
                                  <tr>
                                    <td class="HR_Line" colspan="3" style="height: 2px">
                                    </td>
                                </tr>
                                <tr id="TR_ID2" class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;配额
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle">
                                      <asp:TextBox ID="txtAllQuota" runat="server" Width="250px" MaxLength="8" CssClass="Inputtextbox"></asp:TextBox><span style="height: 5px"><img alt="Req" src="../Images/Req_star.png"
                                            width="5px" height="5px" style="vertical-align: top;" /></span>
                                                <asp:RequiredFieldValidator  ID="rfv_AllQuota" ControlToValidate="txtAllQuota" runat="server" Display="Dynamic"
                                                    ValidationGroup="UpdateRes"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator
                                                        ID="rev_AllQuota" ControlToValidate="txtAllQuota" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateRes" ValidationExpression="^([0-9]\d{0,4})$|^(0|[1-9]\d{0,4})\.(\d{1,2})$"></asp:RegularExpressionValidator></td>
                                </tr>
                                  <tr>
                                    <td class="HR_Line" colspan="3" style="height: 2px">
                                    </td>
                                </tr>
                                 <tr id="TR1" class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;其中彩色配额
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle">
                                      <asp:TextBox ID="txtColorQuota" runat="server" Width="250px" MaxLength="8" CssClass="Inputtextbox"></asp:TextBox><span style="height: 5px"><img alt="Req" src="../Images/Req_star.png"
                                            width="5px" height="5px" style="vertical-align: top;" /></span>
                                      <asp:CustomValidator
                                                ID="cv_ColorQuota" ControlToValidate="txtColorQuota" runat="server" ValidationGroup="UpdateRes"
                                                Display="Dynamic" onservervalidate="cv_ColorQuota_ServerValidate"></asp:CustomValidator>
                                                 <asp:RequiredFieldValidator  ID="rfv_ColorQuota" ControlToValidate="txtColorQuota" runat="server" Display="Dynamic"
                                                    ValidationGroup="UpdateRes"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator
                                                        ID="rev_ColorQuota" ControlToValidate="txtColorQuota" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateRes" ValidationExpression="^([0-9]\d{0,4})$|^(0|[1-9]\d{0,4})\.(\d{1,2})$"></asp:RegularExpressionValidator></td>
                                </tr>
                                  <tr>
                                    <td class="HR_Line" colspan="3" style="height: 2px">
                                    </td>
                                </tr>
                                 <tr id="TR2" class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;">
                                        &nbsp;透支上限
                                    </td>
                                    <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle">
                                      <asp:TextBox ID="txtOverLimit" runat="server" Width="250px" MaxLength="8" CssClass="Inputtextbox"></asp:TextBox><span style="height: 5px"><img alt="Req" src="../Images/Req_star.png"
                                            width="5px" height="5px" style="vertical-align: top;" /></span>
                                      <asp:CustomValidator
                                                ID="cv_OverLimit" ControlToValidate="txtOverLimit" runat="server" ValidationGroup="UpdateRes"
                                                Display="Dynamic" onservervalidate="cv_OverLimit_ServerValidate"></asp:CustomValidator>
                                                
                                                 <asp:RequiredFieldValidator  ID="rfv_OverLimit" ControlToValidate="txtOverLimit" runat="server" Display="Dynamic"
                                                    ValidationGroup="UpdateRes"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator
                                                        ID="rev_OverLimit" ControlToValidate="txtOverLimit" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateRes" ValidationExpression="^([0-9]\d{0,4})$|^(0|[1-9]\d{0,4})\.(\d{1,2})$"></asp:RegularExpressionValidator></td>
                                </tr>
                                  <tr>
                                    <td class="HR_Line" colspan="3" style="height: 2px">
                                    </td>
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;" class="Light_GrayFont">
                                        &nbsp;可用功能
                                    </td>
                                    <td align="left" valign="middle" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle"  valign="top" style="padding-left: 1px; height: 35px">
                                        <div  style="margin-left:1px;">
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
                                  <tr>
                                    <td class="HR_Line" colspan="3" style="height: 2px">
                                    </td>
                                </tr>

                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="height: 35px;" class="Light_GrayFont">
                                        &nbsp;打印策略
                                    </td>
                                    <td align="left" valign="middle" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle" style="padding-left: 1px; height: 35px">
                                    <div  style="margin-left:2px;">
                                        <asp:CheckBox ID="chk_PrintBW" runat="server" Text="强制黑白" />
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
 
    <asp:HiddenField ID="hidResId" runat="server" />
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" runat="server">
    <asp:Button ID="btnUpdate" runat="server" Text="更新" OnClick="btnUpdate_Click" ValidationGroup="UpdateRes"
        CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server"
            Text="取消" OnClick="btnCancel_Click" CssClass="Login_Button_bg" />
</asp:Content>
