<%--
// ==============================================================================
// File Name           : MFP Job Report Screen
// Description         : MFP Job Report Screen for SimpleEA
// Author(s)           : Ji Jianxiong
// Date created        : 2010.07.07
// Date updated        : 2010.09.10
//                       Build No: 1.0.3.2: UI Update.
//                       2010.11.23
//                       Build No: 1.1 Update.
//                       2010.12.9
//                       Build No: 1.1 Update.
//                       2010.12.27
//                       Html Standerd Modify
//                       2010.12.28
//                       Html Standerd Modify
//                       2011.01.05
//                       Html Standerd Modify
//                       2011.01.07
//                       Html Standerd Modify
//                       2011.01.12
//                       Html Standerd Modify
//                       2011.03.22
//                       Html Standerd Modify
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAReportMasterPage.master"
    AutoEventWireup="true" CodeFile="MFPJobReport.aspx.cs" Inherits="Report_MFPJobReport"
    Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleEAReportMasterPage" %>
<%-- the List Head Item ST--%>
<asp:Content ID="ContentHead" ContentPlaceHolderID="cphlisthead" runat="server">

    <script type="text/javascript" language="javascript">
        window.onload = function() {
            var doc = document;
            if ( doc.getElementById("Scroller-Reprot") ) {
                loadScriptScroller(function () {
                    var scroller = null;
                    var scrollbar = null;
                    scroller = new jsScroller(doc.getElementById("Scroller-Reprot"), 400, 330);
                    scrollbar = new jsScrollbar(doc.getElementById("Scrollbar-Container"), scroller, false);
                    scroller = null;
                    scrollbar = null;
                });
            }
        }
        // For the Report page , resize is needed.
        // do while page load. and size reize
        $(document).ready(function(){
            grid_resize();
        });
        
    </script>

    <script type="text/javascript" src="../js/Min_checkbox.js"></script>

    <script type="text/javascript" src="../js/Min_resize.js"></script>

    <script type="text/javascript" src="../js/Min_grid.js"></script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 13%; height: 30px;" align="center" valign="middle">
                <div style="z-index: 3;">
                    <asp:Button ID="btnSelectAll" runat="server" Text="全部选中" OnClientClick="btnSelectAll_Click(this);"
                        CssClass="Select_button" />
                </div>
            </td>
            <td style="width: 51%;" align="left" valign="middle">
                &nbsp;</td>
            <td style="width: 17%;" align="right" valign="middle" class="Black_Font_bold">
                每页项数 &nbsp;
            </td>
            <td style="width: 6%;" align="center" valign="middle">
                <!-- Select Drop down Menu: No of Rows -->
                <asp:DropDownList ID="ddlNumPerPage" CssClass="changeMe" Style="width: 50px" runat="server"
                    AutoPostBack="False" OnSelectedIndexChanged="ddlNumPerPage_SelectedIndexChanged" onchange="CheckListItem(this)">
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                    <asp:ListItem Value="">全部</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 2%;" align="center" valign="middle">
                <asp:ImageButton ID="lbtnPrePage" runat="server" OnClick="IndexOfPage_Pre" ImageUrl="../Images/Arrow_Pre.gif"
                    Width="7px" Height="13px"></asp:ImageButton>
            </td>
            <td style="width: 5%;" align="center" valign="middle">
                <!-- Select Drop down Menu: No of Pages -->
                <asp:DropDownList ID="ddlIndexOfPage" runat="server" CssClass="changeMe" Style="width: 50px;"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlIndexOfPage_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width: 1%;" align="center" valign="middle">
                <img alt="Split" src="../Images/Split_Fixed.png" width="2px" height="22px" />
            </td>
            <td style="width: 2%;" align="center" valign="middle" class="Black_Font_bold">
                <asp:Label ID="lblTotalPage" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width: 3%;" align="center" valign="middle">
                <asp:ImageButton ID="lbtnNextPage" runat="server" OnClick="IndexOfPage_Next" ImageUrl="../Images/Arrow_Next.gif"
                    Width="7px" Height="13px"></asp:ImageButton>
            </td>
        </tr>
    </table>
</asp:Content>
<%-- the List Head Item ED--%>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" runat="server">
    &nbsp; &nbsp;&nbsp;
    <asp:SqlDataSource ID="SqlDataListSource" runat="server" SelectCommand="SELECT * FROM [UserInfo]">
    </asp:SqlDataSource>
    <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_left">
            </td>
            <td style="width: 98%;" align="left" valign="middle" class="TableGrid_bg">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="height: 35px"
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
                        <td align="left" valign="top" style="width: 97%; height: 330px" class="Scroll_td">
                            <div class="Container">
                                <div id="Scroller-Reprot" class="Scroll_div">
                                    <div class="Scroller-Container">
                                        <asp:GridView ID="CustomersGridView" runat="server" DataSourceID="SqlDataListSource"
                                            PageSize="10" OnRowDataBound="CustomView_RowDataBound" GridLines="None" maxrow="9"
                                            CellPadding="0" CellSpacing="0" CssClass="Table_dataFont GridViewCSS">
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
                                                <asp:BoundField DataField="ModelName" HeaderText="MFP型号" SortExpression="ModelName">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="94px" />
                                                    <ItemStyle Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SerialNumber" HeaderText="MFP序列号" SortExpression="SerialNumber">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                                    <ItemStyle Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CopyTotal" HeaderText="彩色复印合计" SortExpression="CopyTotal"
                                                    DataFormatString="{0:N0}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="95px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="GrayCopyTotal" DataFormatString="{0:N0}" HeaderText="黑白复印合计"
                                                    SortExpression="GrayCopyTotal">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="95px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PrintTotal" HeaderText="彩色打印合计" SortExpression="PrintTotal"
                                                    DataFormatString="{0:N0}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="95px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="GrayPrintTotal" HeaderText="黑白打印合计" SortExpression="GrayPrintTotal"
                                                    DataFormatString="{0:N0}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="95px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ScanTotal" HeaderText="扫描使用合计" SortExpression="ScanTotal"
                                                    DataFormatString="{0:N0}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="95px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FaxTotal" HeaderText="传真使用合计" SortExpression="FaxTotal"
                                                    DataFormatString="{0:N0}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="95px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="OtherTotal" DataFormatString="{0:N0}" HeaderText="其它使用合计"
                                                    SortExpression="OtherTotal">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="95px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Total" HeaderText="总使用合计" SortExpression="Total" DataFormatString="{0:N0}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="95px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" CssClass="VR_line" />
                                                    <HeaderStyle Width="1px" CssClass="VR_line" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="SerialNumber">
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
                                <img alt="Down" class="Scrollbar-Down-Reprot" src="../js/Down_arrow.gif" title="Down" />
                                <div class="Scrollbar-Track-Reprot">
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
            <td style="height: 2px;" align="right" valign="bottom" class="Bottom_login_bg_new">
            </td>
            <td align="left" valign="top" class="HR_line_New">
            </td>
            <td align="right" valign="bottom" class="Bottom_right_bg_Newupdated">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" runat="server">
    <div style="z-index: 4;">
        <asp:Button ID="btnItemCount" runat="server" Text="统计" CssClass="Login_Button_bg"
            OnClick="btnItemCount_Click" />
    </div>
</asp:Content>
