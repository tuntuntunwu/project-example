

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="SectionSetting.aspx.cs" Inherits="Settings_SectionSetting" Title="" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>

<asp:Content ID="ContentReportTitle" ContentPlaceHolderID="cphreporttitle" runat="server">
     <span id="lblTotalTitle"><a id="linkTotal" runat="server" class="small_link"  href="../Settings/Settings.aspx">系统设置</a></span> 
    /<span id="lblUserTitle"><a id="linkUser" runat="server" class="small_link" href="../Settings/FollowMESetting.aspx">Follow ME参数设置</a></span>
    /<span id="Span1"><a id="A1" runat="server" class="small_link" href="../Settings/ImportGroupInfor.aspx">组信息导入</a></span>
    /<span id="Span2"><a id="A2" runat="server" class="small_link" href="../Settings/ImportUserInfor.aspx">用户信息导入</a></span>
    /<span id="Span3"><a id="A3" runat="server" class="small_link" href="../Settings/ServerIPSetting.aspx">ServerIP设置</a></span>
    /<span id="Span4"><a id="A4" runat="server" class="small_link" href="../Settings/ContentBackupSetting.aspx">内容留底设置</a></span>
    /<span id="Span10"><a id="A10" runat="server" class="small_link" href="../Settings/LDAPSetting.aspx">LDAP认证设置</a></span>
   /<span id="Span11"><a id="A11" runat="server" class="small_link" href="../Settings/DBAuthSetting.aspx">第三方认证设置</a></span>
    /<span id="Span12"><a id="A12" runat="server" class="small_link" href="../Settings/LDAPSynchronization.aspx">LDAP同步</a></span>
     /<span id="Span5"><a id="A13" runat="server" class="small_link_select" href="../Settings/SectionSetting.aspx">集团设置</a></span>
    /<span id="Span6"><a id="A14" runat="server" class="small_link" href="../Settings/SectionGroupSetting.aspx">集团组设置</a></span>

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

    
    &nbsp; &nbsp;&nbsp;
    <asp:SqlDataSource ID="SqlDataListSource" runat="server"></asp:SqlDataSource>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
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
                                        <asp:GridView ID="CustomersGridView" runat="server" DataSourceID="SqlDataListSource"
                                            PageSize="10" OnRowDataBound="CustomView_RowDataBound" AutoGenerateColumns=false GridLines="None" CellPadding="0"
                                            CellSpacing="0" CssClass="Table_dataFont GridViewCSS">
                                            <PagerSettings Position="Top" PageButtonCount="5" Visible="False" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" Text="" Width="20px" CssClass="Min_check"
                                                            Style="display: none" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" Height="31px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemStyle Width="40px" VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Uncheck_box"
                                                        Height="33px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:HyperLinkField DataTextField="SectionName" HeaderText="集团名称" Text="集团名称" SortExpression="SectionName"
                                                    DataNavigateUrlFields="Id" DataNavigateUrlFormatString="SectionEdit.aspx?GroupId={0}"
                                                    ShowHeader="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" />
                                                    <ItemStyle Width="300px" Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:HyperLinkField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:HyperLinkField DataTextField="Lebel" HeaderText="集团级别" Text="集团级别" SortExpression="Lebel"
                                                    DataNavigateUrlFields="Id" DataNavigateUrlFormatString="SectionEdit.aspx?GroupId={0}"
                                                    ShowHeader="False">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" />
                                                    <ItemStyle Width="300px" Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:HyperLinkField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ParentSectionName" HeaderText="上级集团名称" SortExpression="ParentSectionName">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                    <ItemStyle Width="150px" Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:BoundField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" CssClass="VR_line" />
                                                    <HeaderStyle Width="1px" CssClass="VR_line" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="ID">
                                                    <ItemStyle CssClass="viewDisplay" />
                                                    <HeaderStyle CssClass="viewDisplay" />
                                                </asp:BoundField>
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
<asp:Content ID="Content1" ContentPlaceHolderID="cphfoot" runat="server">
    <div style="z-index: 4;">
        <asp:Button ID="btnAddNewGrp" runat="server" Text="新组追加" OnClick="btnAddNewGrp_Click"
            CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDeleteGrp"
                runat="server" Text="删除选择的组" OnClick="btnDeleteGrp_Click" CssClass="Button_Medium" />
    </div>
</asp:Content>
