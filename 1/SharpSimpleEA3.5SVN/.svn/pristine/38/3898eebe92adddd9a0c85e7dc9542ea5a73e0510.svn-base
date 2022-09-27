
<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="LDAPSynchronization.aspx.cs" Inherits="Settings_LDAPSynchronization" Title="Settings" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>

<asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
    <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link"  href="../Settings/Settings.aspx">系统设置</a></span> 
    /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
    /<span id="Span1"><a id="A1" runat="server" class="small_link" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
    /<span id="Span2"><a id="A2" runat="server" class="small_link" href="../Settings/ImportUserInfor.aspx">用户信息导入</a></span>
    /<span id="Span3"><a id="A3" runat="server" class="small_link" href="../Settings/ServerIPSetting.aspx">ServerIP设置</a></span>
    /<span id="Span4"><a id="A4" runat="server" class="small_link" href="../Settings/ContentBackupSetting.aspx">内容留底设置</a></span>
    /<span id="Span10"><a id="A10" runat="server" class="small_link_select" href="../Settings/LDAPSetting.aspx">LDAP认证设置</a></span>
    /<span id="Span11"><a id="A11" runat="server" class="small_link" href="../Settings/DBAuthSetting.aspx">第三方认证设置</a></span>
    /<span id="Span12"><a id="A12" runat="server" class="small_link_select" href="../Settings/LDAPSynchronization.aspx">LDAP同步</a></span>
    /<span id="Span5"><a id="A13" runat="server" class="small_link" href="../Settings/SectionSetting.aspx">集团设置</a></span>
    /<span id="Span6"><a id="A14" runat="server" class="small_link" href="../Settings/SectionGroupSetting.aspx">集团组设置</a></span>
   <!--
    /<span id="Span5"><a id="A5" runat="server" class="small_link" href="../Settings/LDAPConnection.aspx">LDAP连接设置</a></span>
    /<span id="Span6"><a id="A6" runat="server" class="small_link" href="../Settings/LDAPVerification.aspx">LDAP认证设置</a></span>
    /<span id="Span7"><a id="A7" runat="server" class="small_link" href="../Settings/LDAPGroup.aspx">LDAP组设置</a></span>
    /<span id="Span8"><a id="A8" runat="server" class="small_link" href="../Settings/LDAPUser.aspx">LDAP用户设置</a></span>
    /<span id="Span9"><a id="A9" runat="server" class="small_link_select" href="../Settings/LDAPSynchronization.aspx">LDAP同步</a></span>
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
        <!--
        <tr>
            <td align="left" valign="top" class="Table_header_left" style="width: 1%">
            </td>
            <td align="left" valign="middle" class="bottom_HR_line" style="height: 2px; width: 98%">
            </td>
            <td align="left" valign="top" class="Table_header_Right" style="width: 1%">
            </td>
        </tr>
      
      -->
                                            <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" width="150">                                                   
                                                    <asp:Button ID="btnSyncnow" runat="server" Text="立即同步" Height="30px"  
                                                        Width="150px" onclick="btnSyncnow_Click" />
                                                 </td>
                                             
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>

                                               <tr class="Light_GrayFont">
                                                <td align="left" valign="middle">
                                                   <!--此段代码作用：结果反馈文本框-->
                                                      <asp:TextBox ID="TB_Feedback" runat="server" TextMode="MultiLine" style="width: 800px; height: 80px; max-width: 800px; max-height: 100px;"></asp:TextBox>
                                                </td>
                                             
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                       </tr>

                                            
                                           <tr class="Light_GrayFont">
                                                <td align="left" valign="middle">
                                                   <!--此段代码作用：空1行-->
                                                </td>
                                             
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>
                            <!--
                                           <tr class="Light_GrayFont">
                                                <td align="left" valign="middle">
                                                   IC卡号批量导入：
                                                </td>
                                             
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>



                                           <script type="text/javascript" src="../js/Min_checkbox.js"></script>
                                            <script language="javascript" type="text/javascript">
                                                // do while page load. and size reize
                                                $(document).ready(function () {
                                                    grid_resize();
                                                });       
        
                                            </script>
                                            <script type="text/javascript" src="../js/Min_resize.js"></script>
                                            <script type="text/javascript" src="../js/Min_grid.js"></script>
                                            <table cellpadding="0" cellspacing="0" align="center">
                                              
                                               <tr valign="middle">
                                                  <td align="center" style="height: 50px">
                                                    <asp:Button ID="download" runat="server" Text="导出ICCard信息" Height="30px"  Width="150px" 
                                                             onclick="download_Click" />
                                                            &nbsp; &nbsp; &nbsp; &nbsp;
                                                    <asp:Label ID="lblGroup" Text="请选择组信息文件:" runat="server">
                                                    </asp:Label>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="Inputtextbox" />
                                                   
                                                    </td>
                                                    <td align="center" style="width: 150px">
                                                         &nbsp; &nbsp; &nbsp; &nbsp;
                                                        <asp:Button ID="Button4" runat="server" Text="上传文件" Height="30px"  Width="150px" 
                                                             onclick="Button4_Click" />
                                                        <asp:Label ID="label" runat="server" Text="  "></asp:Label>
                                                        
                                                    </td>
                                                </tr>
                                               
                                            </table>

                                      
                                        -->

                                           <tr class="Light_GrayFont">
                                                <td align="left" valign="middle">
                                                   <!--此段代码作用：空1行-->
                                                </td>
                                             
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>
    
                                       <!--设定可能不需要了
                                           <tr class="Light_GrayFont">
                                                <td align="middle" valign="middle" width="150">
                                                   
                                                    <asp:Button ID="btnSetting" runat="server" Text="设定" Height="30px"  Width="150px" 
                                                        onclick="btnSetting_Click" />
                                                 </td>
                                             
                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
                                                </td>
                                                
                                            </tr>
                                      -->
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
