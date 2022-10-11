
<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="LDAPUser.aspx.cs" Inherits="Settings_LDAPUser" Title="Settings" %>

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
    /<span id="Span7"><a id="A7" runat="server" class="small_link" href="../Settings/LDAPGroup.aspx">LDAP组设置</a></span>
    /<span id="Span8"><a id="A8" runat="server" class="small_link_select" href="../Settings/LDAPUser.aspx">LDAP用户设置</a></span>
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
                                            
                                             <tr class="Light_GrayFont">
                                                <td align="left" valign="middle" style="width: 403px">
                                                    搜索默认DN设置：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    
                                                    <asp:DropDownList ID="DropDownList1" runat="server" Height="25px"  
                                                        Width="100px">

                                                    </asp:DropDownList>

                                                    <asp:ImageButton ID="ImageBtn_Add1" runat="server" 
                                                        ImageUrl="~/Images/plus_imagebtn.png" Height="25px"  
                                                        Width="25px" ImageAlign="AbsMiddle" onclick="ImageBtn_Add1_Click"/>

                                                    <asp:TextBox ID="TB_DN" runat="server" Height="21px"  
                                                        Width="120px" Visible="False" ></asp:TextBox>

                                                    <asp:Button ID="AddDNBtn" runat="server" Text="添加" Height="25px"  
                                                        Width="45px" onclick="AddDNBtn_Click" Visible="False"  />


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
                                                    搜索范围：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                                      <asp:ListItem>Base</asp:ListItem>
                                                      <asp:ListItem>One Level</asp:ListItem>
                                                      <asp:ListItem>Subtree</asp:ListItem>                                               
                                                    </asp:DropDownList>
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
                                                    使用以下设置获取“用户名”：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                   
                                                    <asp:DropDownList ID="DropDownList3" runat="server" Height="25px"  
                                                        Width="100px">

                                                    </asp:DropDownList>

                                                    <asp:ImageButton ID="ImageBtn_Add2" runat="server" 
                                                        ImageUrl="~/Images/plus_imagebtn.png" Height="25px"  
                                                        Width="25px" ImageAlign="AbsMiddle" onclick="ImageBtn_Add2_Click"/>

                                                    <asp:TextBox ID="TB_Name" runat="server" Height="21px"  
                                                        Width="120px" Visible="False" ></asp:TextBox>

                                                    <asp:Button ID="AddNameBtn" runat="server" Text="添加" Height="25px"  
                                                        Width="45px" onclick="AddNameBtn_Click" Visible="False"  />


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
                                                    使用以下设置获取“Email”：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                             
                                                    <asp:DropDownList ID="DropDownList4" runat="server" Height="25px"  
                                                        Width="100px">

                                                    </asp:DropDownList>

                                                    <asp:ImageButton ID="ImageBtn_Add3" runat="server" 
                                                        ImageUrl="~/Images/plus_imagebtn.png" Height="25px"  
                                                        Width="25px" ImageAlign="AbsMiddle" onclick="ImageBtn_Add3_Click"/>

                                                    <asp:TextBox ID="TB_Email" runat="server" Height="21px"  
                                                        Width="120px" Visible="False" ></asp:TextBox>

                                                    <asp:Button ID="AddEmailBtn" runat="server" Text="添加" Height="25px"  
                                                        Width="45px" onclick="AddEmailBtn_Click" Visible="False"  />



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
                                                    使用以下设置获取“IC卡编号”：&nbsp;&nbsp;       

                                                 <asp:DropDownList ID="DropDownList6" runat="server" Height="25px"  
                                                        Width="100px">

                                                    </asp:DropDownList>

                                                    <asp:ImageButton ID="ImageBtn_Add6" runat="server" 
                                                        ImageUrl="~/Images/plus_imagebtn.png" Height="25px"  
                                                        Width="25px" ImageAlign="AbsMiddle" onclick="ImageBtn_Add6_Click"/>

                                                    <asp:TextBox ID="TB_IC" runat="server" Height="21px"  
                                                        Width="120px" Visible="False" ></asp:TextBox>

                                                    <asp:Button ID="AddICBtn" runat="server" Text="添加" Height="25px"  
                                                        Width="45px" onclick="AddICBtn_Click" Visible="False"  />

                                                <td align="left" class="VR_line" valign="top">
                                                </td>
                                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                                        </div>
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
                                                    Test User Account
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
                                                    LDAP User Name：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                   <asp:TextBox ID="TB_UserName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                    LDAP User Password：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                   <asp:TextBox ID="TB_UserPassword" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnTest" runat="server" Text="测试"  Height="30px"  Width="60px"/>
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
