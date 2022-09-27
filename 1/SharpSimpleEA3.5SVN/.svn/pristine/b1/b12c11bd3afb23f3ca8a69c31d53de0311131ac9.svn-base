<%--
// ==============================================================================
// File Name           :  Log View Report Screen.
// Description         : Log View Report Screen. for SimpleEA
// Author(s)           : ESE)Zhou Miao
// Date created        : 2010.12.14
//                       2010.12.21
//                       Ver.1.1 Update
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

<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEALogMasterPage.master"
    AutoEventWireup="true" CodeFile="LogView.aspx.cs" Inherits="LogInfo_LogView"
    Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleEALogMasterPage" %>
<%-- the List Head Item ST--%>
<asp:Content ID="ContentListHead" ContentPlaceHolderID="cphlisthead" runat="server">

    <script type="text/javascript" language="javascript">
        window.onload = function() {
            var doc = document;
            if ( doc.getElementById("Scroller-Total") ) {
                loadScriptScroller(function () {
                    var scroller = null;
                    var scrollbar = null;
                    scroller = new jsScroller(doc.getElementById("Scroller-Total"), 400, 300);
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

    <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
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
                    AutoPostBack="True" OnSelectedIndexChanged="ddlNumPerPage_SelectedIndexChanged">
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
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
<%-- the List Body Item ST--%>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" runat="server">
    &nbsp; &nbsp;&nbsp;
    <asp:SqlDataSource ID="SqlDataListSource" runat="server" SelectCommand="SELECT * FROM [UserInfo]">
    </asp:SqlDataSource>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
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
                <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="top" style="width: 1024px; height: 300px" class="Scroll_td">
                            <div class="Container">
                                <div id="Scroller-Total" class="Scroll_div">
                                    <div class="Scroller-Container">
                                        <asp:GridView ID="CustomersGridView" runat="server" DataSourceID="SqlDataListSource"
                                            PageSize="10" OnRowDataBound="CustomView_RowDataBound" GridLines="None" maxrow="8"
                                         
                                            CellClick = ""
                                            CellPadding="0" CellSpacing="0" CssClass="Table_dataFont GridViewCSS">
                                            <PagerSettings Position="Top" PageButtonCount="5" Visible="False" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" Text="" Width="20px" CssClass="Min_check"
                                                            Style="display: none" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="35px" Height="31px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemStyle Width="35px" CssClass="Uncheck_box" Height="33px" HorizontalAlign="Center"
                                                        VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProcessTime" HeaderText="操作时间" SortExpression="ProcessTime"
                                                    DataFormatString="{0:yyyy/MM/dd HH:mm:ss}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="UserName" HeaderText="用户名" SortExpression="UserName">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="139px" />
                                                    <ItemStyle Wrap="true" CssClass="Automatic_Wrap" Width="139px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MFPModelName" HeaderText="MFP型号" SortExpression="MFPModelName">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="MFPSerialNumber" HeaderText="MFP序列号" SortExpression="MFPSerialNumber">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="JobType" HeaderText="操作类型" SortExpression="JobType">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                                </asp:BoundField>
                                                  <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PageID" HeaderText="纸张" SortExpression="PageID">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ColorMode" HeaderText="色彩" SortExpression="ColorMode">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Duplex" HeaderText="单双面" SortExpression="Duplex">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SheetCount" HeaderText="面数" SortExpression="SheetCount"
                                                    DataFormatString="{0:N0}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PapeCount" HeaderText="张数" SortExpression="PapeCount"
                                                    DataFormatString="{0:N0}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" CssClass="VR_line" />
                                                    <HeaderStyle Width="1%" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CopyCount" HeaderText="份数" SortExpression="CopyCount"
                                                    DataFormatString="{0:N0}">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Cost" HeaderText="总计" SortExpression="Cost">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                </asp:BoundField>
                                               
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" CssClass="VR_line" />
                                                    <HeaderStyle Width="1px" CssClass="VR_line" />
                                                </asp:TemplateField>

                                                <%--
                                                <asp:HyperLinkField  DataTextField="CheckCopy" HeaderText="查看" 
                                                 datatextformatstring="{0:c}"                             
                                                NavigateUrl = "D:\SimpleEAMonitor\1.pdf"
                                                DataNavigateUrlFields="FileName"
                                                 DataNavigateUrlFormatString="~\CopyFilePDF\{0}.pdf"
                                                 target="_blank">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" />  
                                                 </asp:HyperLinkField>
                                                                 
                                                 ---%>
                                                 <asp:TemplateField   HeaderText="查看">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnLoolPdf" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                                   onclick="btnLookPdf_Click" Text="查看" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" CssClass="VR_line" />
                                                    <HeaderStyle Width="1px" CssClass="VR_line" />
                                                </asp:TemplateField>

                                               <%--<asp:TemplateField>                           
                                                        <ItemTemplate>
                                                        <asp:Button ID="Button1" Text="查看" runat="server" CssClass="Select_button" 
                                                        Onclick="window.open()"
                                                         />
                                                    </ItemTemplate>
                                                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="31px" Width="100px" />
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="33px" Width="100px"/>
                                                </asp:TemplateField>  

                                                ---%>

                                                <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" CssClass="VR_line" />
                                                    <HeaderStyle Width="1%" CssClass="VR_line" />
                                                </asp:TemplateField>--%>
                                                <%--<asp:HyperLinkField HeaderText="下载" Text="下载" Visible="false"
                                                     DataNavigateUrlFields="MFPPrintTaskID"  DataNavigateUrlFormatString="ViewPrintContentResult.aspx?MFPPrintTaskID={0}" Target="_blank">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%"/>
                                                    <ItemStyle Wrap="true" CssClass="Automatic_Wrap" Font-Underline="true" />
                                                </asp:HyperLinkField>--%>
                                                <asp:BoundField DataField="CheckCopy">
                                                    <ItemStyle CssClass="viewDisplay" />
                                                    <HeaderStyle CssClass="viewDisplay" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Id">
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
                                <img alt="Down" class="Scrollbar-Down-Total" src="../js/Down_arrow.gif" title="Down" />
                                <div class="Scrollbar-Track-Total">
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
<%-- the List Body Item ED--%>
<%-- the Button Item ST--%>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" runat="server">

    <script type="text/javascript" language="javascript">

         $(document).ready(function(){
            Close_light();
        });
    </script>

</asp:Content>
<%-- the Button Item ED--%>
