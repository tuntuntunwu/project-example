
<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="LDAPGroup.aspx.cs" Inherits="Settings_LDAPGroup" Title="Settings" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>

<asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
    <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link"  href="../Settings/Settings.aspx">系统设置</a></span> 
    /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
    /<span id="Span1"><a id="A1" runat="server" class="small_link" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
    /<span id="Span2"><a id="A2" runat="server" class="small_link" href="../Settings/ImportUserInfor.aspx">用户信息导入</a></span>
    /<span id="Span3"><a id="A3" runat="server" class="small_link" href="../Settings/ServerIPSetting.aspx">ServerIP设置</a></span>
    /<span id="Span4"><a id="A4" runat="server" class="small_link" href="../Settings/ContentBackupSetting.aspx">内容留底设置</a></span>
    /<span id="Span12"><a id="A12" runat="server" class="small_link" href="../Settings/LDAPSynchronization.aspx">LDAP同步</a></span>
    /<span id="Span5"><a id="A13" runat="server" class="small_link" href="../Settings/SectionSetting.aspx">集团设置</a></span>
    /<span id="Span6"><a id="A14" runat="server" class="small_link" href="../Settings/SectionGroupSetting.aspx">集团组设置</a></span>
    <!--
    /<span id="Span5"><a id="A5" runat="server" class="small_link" href="../Settings/LDAPConnection.aspx">LDAP连接设置</a></span>
    /<span id="Span6"><a id="A6" runat="server" class="small_link" href="../Settings/LDAPVerification.aspx">LDAP认证设置</a></span>
    /<span id="Span7"><a id="A7" runat="server" class="small_link_select" href="../Settings/LDAPGroup.aspx">LDAP组设置</a></span>
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

                                             <tr class="Light_GrayFont">
                                                   <td align="left" valign="middle" style="width: 403px">
                                                        &nbsp;
                                                   </td>
                                             </tr>    
                                            <!--
                                             <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 403px">
                                                    用户组默认配额方案：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="DropDownList1" runat="server">                                                                                                       
                                                    </asp:DropDownList>

                                                 </td>
                                                
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>
                                            -->
                                                                <tr class="Light_GrayFont">
                        <td align="left" valign="middle" style="height: 45px">
                            &nbsp; 配额选择
                               </td>
                        <td align="left" valign="top" class="VR_line">
                            &nbsp;</td>
                        <td align="left" valign="top">
                            <div style="padding-left: 8px; padding-top: 9px; float:left; margin-right:15px;">
                                <asp:DropDownList ID="lstRestriction" runat="server" 
                                    DataTextField="RestrictionName" DataValueField="Id" ie6check="true" Width="400px">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="RefreshRestrictionSet" runat="server" Text="RefreshRestrictionSet" OnClick="btnRefreshRestrictionSet_click" Style="display:none;"/>
                            <asp:Button ID="BtnMoeney" runat="server" Text="自定义" Visible="false" OnClick="btnSetRestrictionName_click" CssClass="Select_button" Style="margin-top:7px;" />
                        </td>
                    </tr>

                                            
                                              <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 403px">
                                                   <!--此段代码作用：空1行-->
                                                 </td>
                                             
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>
                                                   


                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 403px">
                                                    LDAP用户组
                                                </td>
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                                                                
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>
                                                                                    


                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 403px">
                                                   使用以下设置获取用户组信息：
                                                 </td>                                       
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>


                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 403px">
                                                   <!--空1行---> 
                                                    <asp:RadioButtonList ID="RadioButton1" runat="server" AutoPostBack="True" OnSelectedIndexChanged = "onAckTypeChanged" >
                                                        <asp:ListItem Selected="True">User Attribute</asp:ListItem>
                                                        <asp:ListItem>User DN</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                     
                                                    <br />
                                                    </td>
                                                
                                                <td align="left" class="VR_line" valign="top">
                                                    &nbsp;</td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>

                                            
                                            
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 403px">
                                                    Group Attribute Name：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="DropDownList2" runat="server" Height="25px"  
                                                        Width="100px">

                                                    </asp:DropDownList>

                                                    <asp:ImageButton ID="ImageBtn_Add" runat="server" 
                                                        ImageUrl="~/Images/plus_imagebtn.png" Height="25px"  
                                                        Width="25px" ImageAlign="AbsMiddle" onclick="ImageBtn_Add_Click"/>

                                                    <asp:TextBox ID="TB_AttName" runat="server" Height="21px"  
                                                        Width="120px" Visible="False" ></asp:TextBox>

                                                    <asp:Button ID="AddBtn" runat="server" Text="添加" Height="25px"  
                                                        Width="45px" onclick="AddBtn_Click" Visible="False"  />

                                                 </td>
                                                
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>                                                
                                            </tr>
                                             

                                             
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 403px">
                                                   <!--空1行--->                                                 
                                                    
                                                </td>
                                                
                                                <td align="left" class="VR_line" valign="top">
                                                    &nbsp;</td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>

                                          
                                            <tr class="Light_GrayFont">
                                                <td align="middle" valign="middle" style="width: 403px">                                                   
                                                    <asp:Button ID="btnSetting" runat="server" Text="设定" Height="30px"  
                                                        Width="150px" onclick="btnSetting_Click" />
                                                 </td>
                                             
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
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
    
    <script type="text/javascript" src="../js/Min_radio.js"></script>

    <script type="text/javascript" src="../js/Min_checkbox.js"></script>

    <script src="../js/commonfoot.js" type="text/javascript">
       
    </script>

</asp:Content>
