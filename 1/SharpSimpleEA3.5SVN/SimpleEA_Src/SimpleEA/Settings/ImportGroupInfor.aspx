<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true" 
CodeFile="ImportGroupInfor.aspx.cs" Inherits="Settings_ImportGroupInfor" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>

<asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
    <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link"  href="../Settings/Settings.aspx">系统设置</a></span> 
    /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
    /<span id="Span1"><a id="A1" runat="server" class="small_link_select" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
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


<asp:Content ID="ContentListHead" ContentPlaceHolderID="cphlisthead" runat="server">
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
                <asp:Label ID="lblGroup" Text="请选择组信息文件:" runat="server"></asp:Label>
                <asp:FileUpload ID="fileUploadGroup" runat="server" CssClass="Inputtextbox" />
            </td>
            <td align="center" style="width: 150px">
                <asp:HyperLink ID="hLink" NavigateUrl="~/Img/SampleGroupImport.csv" runat="server"
                    Text="点击下载csv示例" Font-Size="Small"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="上传文件" CssClass="Login_Button_bg" />
            </td>
        </tr>
    </table>
</asp:Content>
<%-- the List Head Item ED--%>
<%-- the List Body Item ST--%>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" runat="server">
    &nbsp; &nbsp;&nbsp;
    <asp:SqlDataSource ID="SqlDataListSource" runat="server"></asp:SqlDataSource>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" runat ="server" visible ="false" id ="gridTable">
        <tr>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_left">
            </td>
            <td style="width: 98%;" align="left" valign="middle" class="TableGrid_bg">
                <table border="0" cellpadding="0" cellspacing="0" style="height: 35px; width: 100%;"
                    class="GridViewCSS_H">
                </table>
            </td>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_Right">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" class="Left_VRLine">
                &nbsp;
            </td>
            <td align="left" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="top" style="width: 97%; height: 390px" class="Scroll_td">
                            <div class="Container">
                                <div id="Scroller-1" class="Scroll_div">
                                    <div class="Scroller-Container">
                                        <asp:GridView ID="CustomersGridView" runat="server" 
                                            DataSourceID="SqlDataListSource" GridLines="None" CellPadding="0" 
                                            CssClass="Table_dataFont GridViewCSS" AutoGenerateColumns="False" 
                                            EnableModelValidation="True">
                                            <PagerSettings Position="Top" PageButtonCount="5" Visible="False" />
                                            <Columns>
                                            <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" Visible="false" runat="server" Text="" Width="20px" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="100px" Height="31px" VerticalAlign="Middle" HorizontalAlign="Center"/>
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center"
                                                        Height="33px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" CssClass="VR_line" />
                                                    <HeaderStyle Width="1%" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:HyperLinkField DataTextField="GroupName" HeaderText="用户组名称" 
                                                    ShowHeader="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="38%" />
                                                    <ItemStyle Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:HyperLinkField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" CssClass="VR_line" />
                                                    <HeaderStyle Width="1%" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="UserCount" HeaderText="所属用户数" SortExpression="UserCount">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                    <ItemStyle Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" CssClass="VR_line" />
                                                    <HeaderStyle Width="1%" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:HyperLinkField DataTextField="RestrictionName" HeaderText="限制条件" 
                                                    ShowHeader="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                                                    <ItemStyle Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:HyperLinkField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" CssClass="VR_line" />
                                                    <HeaderStyle Width="1%" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:HyperLinkField DataTextField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime"
                                                    ShowHeader="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15%" />
                                                    <ItemStyle Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:HyperLinkField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td style="width: 3%;" align="left" valign="top" class="VR_line ScrollbarCSS ">
                            <div id="Scrollbar-Container" style="visibility: visible;">
                                <img alt="Up" class="Scrollbar-Up" src="../js/UP_arrow.gif" title="Up" />
                                <img alt="Down" class="Scrollbar-Down" src="../js/Down_arrow.gif" title="Down" />
                                <div class="Scrollbar-Track">
                                    <img alt="Handle" class="Scrollbar-Handle" src="../js/Scrollbar_handle.gif" title="Handle" />
                                </div>
                            </div>
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
</asp:Content>
<%-- the List Body Item ED--%>
<%-- the Button Item ST--%>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" runat="server">
    <div style="z-index: 4;">
        <asp:Button ID="btnOverride" runat="server" Text="覆盖导入" CssClass="Login_Button_bg" OnClick="btnOverride_OnClick" Visible="false"  />&nbsp;&nbsp;&nbsp;<asp:Button
            ID="btnIgnore" runat="server" Text="不覆盖导入" CssClass="Login_Button_bg" OnClick="btnIgnore_OnClick"  Visible="false" />
        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="取消导入" CssClass="Login_Button_bg"
            OnClick="btnCancel_OnClick" Visible="false"  />
    </div>
</asp:Content>
