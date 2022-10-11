

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="DBAuthSetting.aspx.cs" Inherits="Settings_DBAuthSetting" Title="" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>

<asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
     <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link"  href="../Settings/Settings.aspx">系统设置</a></span> 
    /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
    /<span id="Span1"><a id="A1" runat="server" class="small_link" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
    /<span id="Span2"><a id="A2" runat="server" class="small_link" href="../Settings/ImportUserInfor.aspx">用户信息导入</a></span>
    /<span id="Span3"><a id="A3" runat="server" class="small_link" href="../Settings/ServerIPSetting.aspx">ServerIP设置</a></span>
    /<span id="Span4"><a id="A4" runat="server" class="small_link" href="../Settings/ContentBackupSetting.aspx">内容留底设置</a></span>
    /<span id="Span10"><a id="A10" runat="server" class="small_link" href="../Settings/LDAPSetting.aspx">LDAP认证设置</a></span>
   /<span id="Span11"><a id="A11" runat="server" class="small_link_select" href="../Settings/DBAuthSetting.aspx">第三方认证设置</a></span>
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
                                <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                    数据库连接设置
                                </td>
                            </tr>    
                            <tr class="Light_GrayFont">
                              
                                <!--控制两行之间的大小-->
                                    <td align="left" valign="top" style="padding-left: 90px; height: 20px;">
                                        <div style="z-index: 2; position: absolute; padding-top: 2px">
                                        </div>
                                    </td>
                                  <td align="left" class="VR_line" valign="middle"> 
                                </td>
                           </tr>

                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle">连接数据库配置信息：
                                        <td align="left" class="VR_line" valign="middle"> &nbsp; &nbsp; &nbsp;                                        
                                            <asp:TextBox ID="TB_ConnectStr" runat="server" TextMode="MultiLine" MaxLength="500" style="width: 600px; height: 100px;"></asp:TextBox>                                                 
                                        </td>
                                    </td>                                       
                                </tr>
                                 
      
                          <tr class="Light_GrayFont">

                                <td align="left" valign="middle">
                                    测试数据库：
                                    <td align="left" class="VR_line" valign="middle">&nbsp; &nbsp; &nbsp;  
                                       <asp:Button ID="btnTestCon" runat="server" Text="测试" Height="30px"  Width="60px" 
                                        onclick="btnTest_Click" />  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Label ID="lbl_con_status" runat="server" Text="  "></asp:Label>   
                                       </td>
                                </td>

                                <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                    <div style="z-index: 8; position: absolute; padding-top: 5px">
                                        </div>
                                </td>
                                                
                          </tr>
                          <tr>
                            <!--画一条横线-->
                            <td colspan="5" class="HR_Line" style="height: 2px">
                            </td>
                        </tr> 

      <!----------------------------------两个界面的分割线----------------------------------------------->
                        <tr>            
                            <td align="left" valign="middle" class="Add_User_Titlebg" colspan="5">
                                选择SQL语句
                            </td>
                        </tr>    
                           
                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle" style="width: 403px">
                                SQL语句：
                                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_SearchSql" runat="server" TextMode="MultiLine" MaxLength="500" style="width: 600px; height: 100px;">
                                    </asp:TextBox>
                                </td>
                            </td>
                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                    </div>
                            </td>
                        </tr>
                    <td class="Light_GrayFont" >
                         测试检索语句:
                         <td align="left" class="VR_line" valign="middle"> &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnTestSel" runat="server" Text="测试" Height="30px"  Width="60px" 
                                        onclick="btnTestSel_Click" /> 
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                            <asp:Label ID="lbl_testsearch" runat="server" Text="  "></asp:Label>   

                         </td>     
                    </td>               
                              
                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle" style="width: 403px">
                                数据认证条件：
                                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="TB_WhereSql" runat="server" TextMode="MultiLine" MaxLength="500" style="width: 600px; height: 20px;">
                                    </asp:TextBox>
                                </td>
                            </td>
                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                    </div>
                            </td>
                        </tr>   
                        
                         <tr class="Light_GrayFont">
                            <td align="left" valign="middle" style="width: 403px">
                                组1条件值：
                                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="MTGrpVal1" runat="server" style="width: 150px; height: 19px;">
                                    </asp:TextBox>
                                </td>
                            </td>
                            <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                                <div style="z-index: 8; position: absolute; padding-top: 5px">
                                    </div>
                            </td>
                        </tr>             
                        <tr class="Light_GrayFont">
                            <td align="left" valign="middle" style="width: 403px">
                                组2条件值：
                                <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="MTGrpVal2" runat="server" style="width: 150px; height: 19px;">
                                    </asp:TextBox>
                                </td>
                            </td>     
                        <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                            <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                        </td>
                                                
                    </tr>                                            
                                                                    
                   <tr  class="Light_GrayFont">
                        <td align="left" valign="middle" style="width: 403px">
                            组1
                           <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlGroupName1" AutoPostBack=true OnSelectedIndexChanged="onSelectedGroup1Changed" runat="server" DataTextField="GroupName" DataValueField="id"
                                   Width="200px">
                                </asp:DropDownList>
                        </td>
                        </td>
                        <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                            <div style="z-index: 8; position: absolute; padding-top: 5px">
                                </div>
                        </td>

                    </tr>
                   <tr  class="Light_GrayFont">
                        <td align="left" valign="middle" style="height: 35px;">
                            组2
                        </td>
                        <td align="left" valign="middle" class="VR_line"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlGroupName2" AutoPostBack=true OnSelectedIndexChanged="onSelectedGroup2Changed" runat="server" DataTextField="GroupName" DataValueField="id"
                                   Width="200px">
                                </asp:DropDownList>
                        </td>
                        </td>
                    </tr>
 
                         
    <!----------------------------------两个界面的分割线----------------------------------------------->
                 
                <tr>
                <!--画一条横线-->
                <td colspan="5" class="HR_Line" style="height: 2px">
                </td>
            </tr>       
                   
                   <tr  class="Light_GrayFont">
                        <td align="left" valign="middle" style="width: 403px">
                            认证规则：
                            <td align="left" class="VR_line" valign="middle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="AuthDBFlg" runat="server" style="width: 150px; height: 19px;">
                                </asp:TextBox>
                                0：认证不通过不可使用 1：认证不通过允许使用
                            </td>
                        </td>     
                    <td align="left" valign="top" style="padding-left: 90px; height: 35px;">
                        <div style="z-index: 8; position: absolute; padding-top: 5px">
                            </div>
                    </td>
                    </tr>
                <tr>
                <!--画一条横线-->
                <td colspan="5" class="HR_Line" style="height: 2px">
                </td>
                         <!--
            </tr>       
                    <td class="Light_GrayFont">
                         测试条件:
                            <asp:Button ID="btnAuthICCard" runat="server" Text="测试ICCardAuth" Height="30px"  Width="200px" 
                                        onclick="btnAuthICCard_Click" /> 
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                            <asp:Label ID="Label2" runat="server" Text="  "></asp:Label>   

                    </td>               
                   </tr>
                            -->
                             
        <tr class="Light_GrayFont">
           
            <td>
                <td align="left"  valign="middle">  
                    &nbsp; &nbsp;&nbsp;&nbsp;                                    
                    <asp:Button ID="btnSetting" runat="server" Text="保存" Height="30px"  
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
            <td colspan="5" class="HR_Line" style="height: 2px">
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
