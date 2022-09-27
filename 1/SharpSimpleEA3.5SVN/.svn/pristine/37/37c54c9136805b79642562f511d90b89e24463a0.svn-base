

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="LDAPSetting.aspx.cs" Inherits="Settings_LDAPSetting" Title="" %>

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
    /<span id="Span12"><a id="A12" runat="server" class="small_link" href="../Settings/LDAPSynchronization.aspx">LDAP同步</a></span>
     /<span id="Span5"><a id="A13" runat="server" class="small_link" href="../Settings/SectionSetting.aspx">集团设置</a></span>
    /<span id="Span6"><a id="A14" runat="server" class="small_link" href="../Settings/SectionGroupSetting.aspx">集团组设置</a></span>

   <!--
    /<span id="Span5"><a id="A5" runat="server" class="small_link_select" href="../Settings/LDAPConnection.aspx">LDAP连接设置</a></span>
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
        <!--标记表格在界面的位置-->
            <td align="left" valign="top" class="Table_header_left">
            </td>
            
            <td align="left" valign="middle" class="bottom_HR_line" style="height: 2px; width: 98%">
            </td>
         
            <td align="left" valign="top" class="Table_header_Right" style="width: 1%">
            </td>
                     
        </tr>

        <tr>
            <td align="left" valign="top" class="Left_VRLine">
            </td>
            <td align="left" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="top" style="height: 394px; width: 97%" class="Scroll_td">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">  

                            <tr>            
                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="3">
                                    LDAP连接设置
                                </td>
                            </tr>    
                                                 
                                <tr class="Light_GrayFont">                                   
                                    <td align="left" valign="middle" style="width: 200px; height: 40px;">LDAP服务器
                                        <td align="left" class="VR_line" valign="middle" style="height: 40px">  
                                        </td>
                                    </td>

                                    <!--控制两行之间的大小-->
                                    <td align="left" valign="top" style="padding-left: 90px; height: 40px;">
                                        <div style="z-index: 8; position: absolute; padding-top: 5px">
                                        </div>
                                    </td> 
                                </tr>

                                <tr class="Light_GrayFont">                                   
                                    <td align="left" valign="middle" style="width: 200px; height: 40px;">LDAP服务器无法连接时，是否允许使用DB信息
                                        <td align="left" class="VR_line" valign="middle" style="height: 40px">  
                                            &nbsp;&nbsp; &nbsp;&nbsp;  
                                            <asp:CheckBox ID="DBAllowed" runat="server"  Text="允许" />
                                        </td>
                                    </td> 
                                </tr>

                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle">服务器主机名/IP地址：
                                        <td align="left" class="VR_line" valign="middle"> &nbsp; &nbsp; &nbsp;                                        
                                            <asp:TextBox ID="TB_IP" runat="server" style="width: 150px; height: 19px;"></asp:TextBox>                                                 
                                        </td>
                                    </td>                                       
                                </tr>
                                 

                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle">服务器端口：
                                        <td align="left" class="VR_line" valign="middle">&nbsp; &nbsp; &nbsp;
                                            <asp:TextBox ID="TB_Port" runat="server" style="width: 150px; height: 19px;"></asp:TextBox>
                                                 &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:Button ID="Button_PortTest" runat="server" Text="测试"  Height="30px"  Width="60px" OnClick="PingTest_Click"/>
                                             <asp:Label ID="label1" runat="server" Text="  "></asp:Label>
                                        </td>
                                        <!--控制两行之间的大小-->
                                        <td align="left" valign="top" style="padding-left: 90px; height: 40px;">
                                            <div style="z-index: 8; position: absolute; padding-top: 5px">
                                            </div>
                                        </td>
                                    </td> 
                                </tr>                                        
                                <tr>
                                    <!--画一条横线-->
                                    <td colspan="3" class="HR_Line" style="height: 2px">
                                    </td>
                                </tr>


                                <tr class="Light_GrayFont">

                                    <td align="left" valign="middle">连接认证设置
                                    </td>
                                               
                                    <td align="left" class="VR_line" valign="top">
                                    </td>

                                    <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                        <div style="z-index: 8; position: absolute; padding-top: 5px">
                                        </div>
                                    </td>                                                
                                </tr>

                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle">认证方式类型：
                                    </td>
                                        <td align="left" class="VR_line" valign ="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList ID="Drop_VerType1" runat="server" style="width: 150px; height: 19px;">
                                            <asp:ListItem>none</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                   
                               

                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                    </div>
                                </td>
                                                
                                </tr>

                             <tr>
                                <!--画一条横线-->
                                <td colspan="3" class="HR_Line" style="height: 2px">
                                </td>
                             </tr>


                            <tr class="Light_GrayFont">

                                <td align="left" valign="middle">
                                    请提供一个LDAP账户来执行用户搜索和账户状态检查
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
                                    连接认证账号：
                                    <td align="left" class="VR_line" valign="middle">&nbsp; &nbsp; &nbsp;  
                                       <asp:TextBox ID="TB_Account" runat="server" style="width: 150px; height: 19px;"></asp:TextBox>
                                       </td>
                                </td>

                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                        </div>
                                </td>
                                                
                          </tr>


                            <tr class="Light_GrayFont">

                                <td align="left" valign="middle">
                                    连接认证密码：
                                    <td align="left" class="VR_line" valign="middle"> &nbsp; &nbsp; &nbsp;    
                                        <asp:TextBox ID="TB_Password" runat="server" style="width: 150px; height: 19px;"></asp:TextBox>
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                                    <asp:Button ID="btnTest" runat="server" Text="测试" Height="30px"  Width="60px" 
                                        onclick="btnTest_Click" />   
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                     
                                    </td> 
                                </td>                                
                            </tr>

                        
                        <tr class="Light_GrayFont">
                        <!--
                        <td align="left" class="VR_line" valign="top">
                        </td>
                        -->
                        <!--控制两行之间的大小-->
                        <td align="left"  valign="top" style="padding-left: 90px; height: 30px;">
                            <div style="z-index: 8; position: absolute; padding-top: 5px">
                            </div>
                        </td> 
                            <td align="left"  class="VR_line" valign="middle">                                                   
                                &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="label" runat="server" Text="  "></asp:Label>
                            </td>
                                                
                        </tr>
                 
                        <tr>
                            <!--画一条横线-->
                            <td colspan="3" class="HR_Line" style="height: 2px">
                            </td>
                        </tr>     
                        
      <!----------------------------------两个界面的分割线----------------------------------------------->
                        <tr>            
                            <td align="left" valign="middle" class="Add_User_Titlebg" colspan="3">
                                LDAP认证设置
                            </td>
                        </tr>    
                                             
                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle">
                                LDAP认证选项
                                <td align="left" class="VR_line" valign="top">
                                </td>                                                
                            </td>

                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                            </td>          
                        </tr>

                        <!--

                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle">
                                认证方式类型：
                                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="Drop_VerType2" runat="server" style="width: 150px; height: 19px;">
                                        <asp:ListItem>none</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </td>
                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                            </td>
                                                
                        </tr>
                        -->
                        <!-- 
                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle">
                                用户登录属性：
                                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_UserLoginAttri" runat="server" style="width: 150px; height: 19px;"></asp:TextBox>                                                 
                                    例如:sAMAccountName
                                </td>  
                            </td>
                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                            </td>
                                                
                        </tr>
                         -->
                        <tr>
                            <!--画一条横线-->
                            <td colspan="3" class="HR_Line" style="height: 2px">
                            </td>
                        </tr>  

                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle">
                                针对Active Directory使用者
                            </td>
                            <td align="left" class="VR_line" valign="middle">
                            </td>  
                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                            </td>
                                                
                        </tr>


                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle">
                                登录形式：
                                 <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="Drop_LoginType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="myListDropDown_Change" 
                                        style="width: 150px; height: 19px;">
                                        <asp:ListItem>NT Domain Name</asp:ListItem>
                                        <asp:ListItem>User Prinicipal</asp:ListItem>
                                        </asp:DropDownList>
                                </td>
                            </td>
                                       

                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                            </td>
                                                
                        </tr>
                           
                                           
                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle">
                                NT或DNS域名：
                                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_NTorDNS" runat="server" style="width: 150px; height: 19px;"></asp:TextBox>
                               
                                </td>
                            </td>  
                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                            </td>
                                                
                        </tr>

                         <tr>
                            <!--画一条横线-->
                            <td colspan="3" class="HR_Line" style="height: 2px">
                            </td>
                        </tr> 
                  

      <!----------------------------------两个界面的分割线----------------------------------------------->
                        <tr>            
                            <td align="left" valign="middle" class="Add_User_Titlebg" colspan="3">
                                LDAP组设置
                            </td>
                        </tr>  
                        <tr class="Light_GrayFont">
                        <td align="left" valign="middle" style="height: 45px">
                            用户组默认配额方案
                            <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:DropDownList ID="lstRestriction" runat="server" 
                                    DataTextField="RestrictionName" DataValueField="Id" ie6check="true" Width="200px">
                                </asp:DropDownList>
                            <asp:Button ID="RefreshRestrictionSet" runat="server" Text="RefreshRestrictionSet" OnClick="btnRefreshRestrictionSet_click" Style="display:none;"/>
                              </td>
                        </td>
                         <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                    </div>
                         </td>     
                    </tr>

                           
                                                   
                        <tr>
                            <!--画一条横线-->
                            <td colspan="3" class="HR_Line" style="height: 2px">
                            </td>
                        </tr> 

                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle" style="width: 403px">
                                LDAP用户组
                            </td>
                            <td align="left" class="VR_line" valign="middle">
                            </td>   
                                                
                        </tr>
                                                                                    
                             
                                            
                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle" style="width: 403px">
                                获取用户组信息：
                                 <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="Drop_AttriName" runat="server" style="width: 150px; height: 19px;">                                        
                                        <asp:ListItem>Department</asp:ListItem>
                                        <asp:ListItem>OU</asp:ListItem>
                                    </asp:DropDownList>
                                 </td>                              
                            </td>
                                   
                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                    </div>
                            </td>                                                
                        </tr>
                        <tr>
                            <!--画一条横线-->
                            <td colspan="3" class="HR_Line" style="height: 2px">
                            </td>
                        </tr>                  

                                             
                     
      <!----------------------------------两个界面的分割线----------------------------------------------->
                        <tr>            
                            <td align="left" valign="middle" class="Add_User_Titlebg" colspan="3">
                                LDAP用户设置
                            </td>
                        </tr>    
                           
                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle" style="width: 403px">
                                搜索默认DN设置：
                                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_DefaultDN" runat="server" style="width: 400px; height: 19px;">
                                    </asp:TextBox>
                                    例如:DC=example,DC=com
                                </td>
                            </td>
                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                    </div>
                            </td>
                        </tr>
                                            
                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle" style="width: 403px">
                                搜索范围：
                                <td align="left"  class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="Drop_Search" runat="server" style="width: 150px; height: 19px;">
                                        <asp:ListItem>Base</asp:ListItem>
                                        <asp:ListItem>OneLevel</asp:ListItem>
                                        <asp:ListItem>Subtree</asp:ListItem>                                               
                                    </asp:DropDownList>
                                </td>
                            </td>     
                        <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                            <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                        </td>
                                                
                    </tr>                                            
                      
                    <tr>
                        <!--画一条横线-->
                        <td colspan="3" class="HR_Line" style="height: 2px">
                        </td>
                    </tr>        
                                            
                    <tr class="Light_GrayFont">
                        <td align="left" valign="middle" style="width: 403px">
                        使用以下设置获取“用户名”：
                            <td align="left" class="VR_line" valign="middle">  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                         
                                <asp:TextBox ID="TB_ObtainUserName" runat="server" style="width: 150px; height: 19px;">
                                </asp:TextBox>
                                例如:displayName或cn
                            </td>
                        </td>    
                        <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                            <div style="z-index: 8; position: absolute; padding-top: 5px">
                            </div>
                        </td>
                                                
                    </tr>                                         
                           
                                            
                    <tr class="Light_GrayFont">
                        <td align="left" valign="middle" style="width: 403px">
                            使用以下设置获取“Email”:
                            <td align="left" class="VR_line" valign="middle"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;               
                                <asp:TextBox ID="TB_Email" runat="server" style="width: 150px; height: 19px;">
                                </asp:TextBox>
                                例如:mail
                            </td>
                        </td>    

                        <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                            <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                        </td>
                                                
                </tr>                                          
                                            
                <tr class="Light_GrayFont">
                    <td align="left" valign="middle" style="width: 403px">
                        使用以下设置获取“IC卡编号”:
                         <td align="left" class="VR_line" valign="middle"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;      
                            <asp:TextBox ID="TB_ICCard" runat="server" style="width: 150px; height: 19px;">
                            </asp:TextBox>
                            例如:telephoneNumber
                         </td>     
                    </td>               
                 

                    <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                        <div style="z-index: 8; position: absolute; padding-top: 5px">
                            </div>
                    </td>
                </tr>
                      
                <tr>
                    <!--画一条横线-->
                    <td colspan="3" class="HR_Line" style="height: 2px">
                    </td>
                </tr>                     
                                              
                <tr class="Light_GrayFont">
                    <td align="left" valign="middle" style="width: 403px">
                        测试用用户帐号
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
                用户名称:
                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:TextBox ID="TB_UserName" runat="server" style="width: 150px; height: 19px;">
                     </asp:TextBox>
                </td>
            </td>                                  
       
            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                <div style="z-index: 8; position: absolute; padding-top: 5px">
                    </div>
            </td>
                                                
        </tr>

            <tr class="Light_GrayFont">
            <td align="left" valign="middle">
                用户密码:
                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TB_UserPassword" runat="server" style="width: 150px; height: 19px;"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Bt_Test" runat="server" Text="测试"  Height="30px"  Width="60px" 
                        onclick="Bt_Test_Click"/>
                </td>
            </td>                                  
      
            <td align="left" valign="top" style="padding-left: 90px; height: 50px;">
                <div style="z-index: 8; position: absolute; padding-top: 5px">
                    </div>
            </td>
                                                
        </tr>
                                                                                       
        <tr>
            <!--画一条横线-->
            <td colspan="3" class="HR_Line" style="height: 2px">
            </td>
        </tr>                                

                             
        <tr class="Light_GrayFont">
           
            <td>
                <td align="left"  valign="middle">  
                    &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;                                  
                    <asp:Button ID="btnSetting" runat="server" Text="设定" Height="30px"  
                        Width="150px" onclick="btnSetting_Click" />
                </td>
            </td>                                 
         
            <td align="left" valign="middle" style="padding-left: 90px; height: 35px;">
                <div style="z-index: 50; position: absolute; padding-top: 10px">
                    </div>
            </td>
                                                
        </tr>

        <tr>
            <!--画一条横线-->
            <td colspan="3" class="HR_Line" style="height: 2px">
            </td>
        </tr> 

                           
                           
                           
                                                                             
                                </table>
                            </td>
                        </tr>
                    </table>
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
