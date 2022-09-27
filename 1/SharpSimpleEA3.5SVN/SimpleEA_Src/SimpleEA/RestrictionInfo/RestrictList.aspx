<%--
// ==============================================================================
// File Name           : RestrictList.aspx
// Description         : Restriction Set List Information Page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.06.14
// Date updated        : 2010.09.07
//                       Build No: 1.0.3.2: UI Update.
//                       2010.11.19
//                       Build No: 1.1 Update.
//                       2010.12.27
//                       Html Standerd Modify
//                       2010.12.28
//                       Html Standerd Modify
//                       2010.12.29
//                       Html Standerd Modify
//                       2011.01.05
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
    CodeFile="RestrictList.aspx.cs" Inherits="RestrictionInfo_RestrictList" Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>
<%-- the List Head Item ST--%>
<asp:Content ID="ContentListHead" ContentPlaceHolderID="cphlisthead" runat="server">

    <script type="text/javascript" src="../js/Min_checkbox.js"></script>

    <script language="javascript" type="text/javascript">       
        // do while page load. and size reize
        $(document).ready(function(){
            grid_resize();
        });       
        
    </script>
    
    <script type="text/javascript" src="../js/Min_resize.js"></script>

    <script type="text/javascript" src="../js/Min_grid.js"></script>



    <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 13%; height: 35px;" align="center" valign="middle">
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
    <asp:SqlDataSource ID="SqlDataListSource" runat="server"></asp:SqlDataSource>
    <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_left">
            </td>
            <td style="width: 98%;" align="left" valign="middle" class="TableGrid_bg">
                <table cellpadding="0" cellspacing="0" style="height: 35px; width: 100%; border: 0;"
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
                        <td align="left" valign="top" style="width: 97%; height: 390px" class="Scroll_td">
                            <div class="Container">
                                <div id="Scroller-1" class="Scroll_div">
                                    <div class="Scroller-Container">
                                        <asp:GridView ID="CustomersGridView" runat="server" DataSourceID="SqlDataListSource"
                                            PageSize="10" OnRowDataBound="CustomView_RowDataBound" GridLines="None" CellPadding="0"
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
                                                <asp:HyperLinkField DataTextField="RestrictionName" HeaderText="配额名称（设定内容概要）"
                                                    Text="配额名称（设定内容概要）" SortExpression="RestrictionName" DataNavigateUrlFields="Id"
                                                    DataNavigateUrlFormatString="RestrictInfoEdit.aspx?RestrictionId={0}" ShowHeader="False">
                                                    <HeaderStyle Width="250px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemStyle Width="250px" Wrap="true" CssClass="Automatic_Wrap" />
                                                </asp:HyperLinkField>
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>

                                                  <asp:HyperLinkField DataTextField="AllQuota" HeaderText="配额"
                                                    Text="配额" SortExpression="AllQuota" DataNavigateUrlFields="Id"
                                                    DataNavigateUrlFormatString="RestrictInfoEdit.aspx?RestrictionId={0}" ShowHeader="False">
                                                    <HeaderStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemStyle Width="100px" Wrap="true" CssClass="Automatic_Wrap" HorizontalAlign="Right" />
                                                </asp:HyperLinkField>

                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                 <asp:HyperLinkField DataTextField="colorQuota" HeaderText="彩色配额"
                                                    Text="彩色配额" SortExpression="colorQuota" DataNavigateUrlFields="Id"
                                                    DataNavigateUrlFormatString="RestrictInfoEdit.aspx?RestrictionId={0}" ShowHeader="False">
                                                    <HeaderStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemStyle Width="100px" Wrap="true" CssClass="Automatic_Wrap"  HorizontalAlign="Right" />
                                                </asp:HyperLinkField>

                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                  <asp:HyperLinkField DataTextField="OverLimit" HeaderText="透支上限"
                                                    Text="透支上限" SortExpression="OverLimit" DataNavigateUrlFields="Id"
                                                    DataNavigateUrlFormatString="RestrictInfoEdit.aspx?RestrictionId={0}" ShowHeader="False">
                                                    <HeaderStyle Width="100px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                                    <ItemStyle Width="100px" Wrap="true" CssClass="Automatic_Wrap" HorizontalAlign="Right" />
                                                </asp:HyperLinkField>
                                    
                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="可用功能" InsertVisible="False">
                                                    <HeaderStyle Width="320px" />
                                                    <ItemStyle Width="320px"  />

                                                <ItemTemplate>
                                                <a href="RestrictInfoEdit.aspx?RestrictionId=<%# Eval("Id")%>"> <%#getFunctionList(Eval("Id").ToString())%></a>  
                                                </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="打印策略" InsertVisible="False">
                                                    <HeaderStyle Width="320px" />
                                                    <ItemStyle Width="320px"  />

                                                <ItemTemplate>
                                                <a href="RestrictInfoEdit.aspx?RestrictionId=<%# Eval("Id")%>"> <%#getPrintBW(Eval("Id").ToString())%></a>  
                                                </ItemTemplate>
                                                </asp:TemplateField>


                                                 <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" CssClass="VR_line" />
                                                    <HeaderStyle Width="1px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                 
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
    <div style="z-index: 4;">
        <asp:Button ID="btnAddNewGrp" runat="server" Text="新配额方案追加" OnClick="btnAddNewRes_Click"
            CssClass="Button_Medium" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDeleteGrp"
                runat="server" Text="删除选择的配额方案" OnClick="btnDeleteRes_Click" CssClass="Button_Larger" />
    </div>
</asp:Content>
<%-- the Button Item ED--%>
