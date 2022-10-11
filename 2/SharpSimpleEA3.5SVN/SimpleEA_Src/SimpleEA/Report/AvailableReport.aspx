<%--
// ==============================================================================
// File Name           : Available Report Screen.
// Description         : Available Report Screen. for SimpleEA
// Author(s)           : Ji Jianxiong
// Date created        : 2010.07.01
// Date updated        : 2010.09.10
//                       Build No: 1.0.3.2: UI Update.
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
    CodeFile="AvailableReport.aspx.cs" Inherits="Report_AvailableReport" Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>
<%-- the List Head Item ST--%>
<asp:Content ID="ContentListHead" ContentPlaceHolderID="cphlisthead" runat="server">
    <script type="text/javascript" src="../js/Min_checkbox.js"></script>
   <script language="javascript" type="text/javascript">       
        // do while page load. and size reize
        $(document).ready(function(){
            grid_resize();
        });       
    

       
        function onAddValueKeyUp(txtbx) {
            if (!$(txtbx).attr("lastValue")) {
                $(txtbx).attr("lastValue", "");
            }
            if ($(txtbx).val() == "") {
                $(txtbx).attr("lastValue", "");

            }else if (/^((-?\d+(\.\d{1,2})?)|(-?\d+\.)|(-))$/.test($(txtbx).val())) {
                $(txtbx).attr("lastValue", $(txtbx).val());

            } else {
                $(txtbx).val($(txtbx).attr("lastValue"));
            }
        }
    </script>

   


    <script type="text/javascript" src="../js/Min_resize.js"></script>

    <script type="text/javascript" src="../js/Min_grid.js"></script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 13%; height: 35px;" align="center" valign="middle" class="Black_Font_bold">
            时间段:
                          
                <asp:Label ID="lbl_period" runat="server" Text="lbl_period"></asp:Label>
                          
            </td>
            <td style="width: 51%;" align="left" valign="middle">
                &nbsp;</td>
            <td style="width: 17%;" align="right" valign="middle" class="Black_Font_bold">
                每页项数 &nbsp;
            </td>
            <td style="width: 6%;" align="center" valign="middle">
                <!-- Select Drop down Menu: No of Rows -->
                <asp:DropDownList ID="ddlNumPerPage" CssClass="changeMe" Style="width: 50px" runat="server"
                    AutoPostBack="false" OnSelectedIndexChanged="ddlNumPerPage_SelectedIndexChanged"  onchange="CheckListItem(this)">
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                    <asp:ListItem Value="">全部</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 2%;" align="center" valign="middle">
                <asp:ImageButton ID="lbtnPrePage" runat="server" OnClick="IndexOfPage_Pre" ImageUrl="../Images/Arrow_Pre.gif"
                    Width="7" Height="13"></asp:ImageButton>
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
    <table style="width: 100%; border: 0;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_left">
            </td>
            <td style="width: 98%" align="left" valign="middle" class="TableGrid_bg">
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
                                        <asp:GridView  ID="CustomersGridView" runat="server"  
                                            DataSourceID="SqlDataListSource" OnRowDataBound="CustomView_RowDataBound" 
                                            GridLines="None" CellPadding="0" CssClass="Table_dataFont GridViewCSS" 
                                            EnableModelValidation="True" AutoGenerateColumns="False" 
                                            onrowupdating="CustomersGridView_RowUpdating" RowStyle-Height="33px">
                                            <PagerSettings Position="Top" PageButtonCount="5" Visible="False" />
                                            <Columns>
                                            
                                              <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>



                                                <asp:BoundField DataField="UserName" HeaderText="" SortExpression="UserName">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                                                    <ItemStyle VerticalAlign="Middle" Width="200px" HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="GroupName" HeaderText="用户组名称" SortExpression="GroupName">
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                                    <ItemStyle VerticalAlign="Middle" Width="150px" HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Id">
                                                    <ItemStyle CssClass="viewDisplay" />
                                                    <HeaderStyle CssClass="viewDisplay" />
                                                </asp:BoundField>


                                                  <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderText="总余额" >
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" Width="100px" />
                                                    <ItemTemplate  > 
                                                    <asp:Label  ID="lblRemain" runat="server" text='<%#Eval("Id")!=DBNull.Value?getRemainByUserID(int.Parse(Eval("Id").ToString())):"0"%>' ></asp:Label>
                                                    </ItemTemplate> 
                                                </asp:TemplateField>

                                                  <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="添加" >
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"  Width="140px"/>
                                                    <ItemStyle  VerticalAlign="Middle" HorizontalAlign="Center" Width="140px" />
                                                
                                                    <ItemTemplate> 
                                                      <asp:TextBox size="9"  ID="txtRemain" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" MaxLength="9" Width="120px" Text="0" />
                                                      <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="txtRemain" ErrorMessage="必须输入" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </ItemTemplate> 

                                                 </asp:TemplateField>

                                                   <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="可使用彩色余额" >
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                  <ItemStyle VerticalAlign="Middle" HorizontalAlign="Right" Width="100px" />
                                                
                                                <ItemTemplate> 
                                             
                                                 <asp:Label ID="lblRemainColor" runat="server" text='<%#Eval("Id")!=DBNull.Value?getRemainColorByUserID(int.Parse(Eval("Id").ToString())):"0" %>'></asp:Label>
                                                </ItemTemplate> 
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="添加" >
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                                                 <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Width="140px" />
                                                <ItemTemplate> 
                                                  <asp:TextBox size="9"  onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);"  ID="txtRemainColor" runat="server" MaxLength="9" Width="120px" Text="0" />
                                                    <asp:RequiredFieldValidator Display="Dynamic"   ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="txtRemainColor" ErrorMessage="必须输入" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </ItemTemplate> 


                                                </asp:TemplateField>


                                                  <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" CssClass="VR_line" />
                                                    <HeaderStyle Width="10px" CssClass="VR_line" />
                                                </asp:TemplateField>


                                      
                                      
                                                   <asp:TemplateField>
                                   


                                                    <ItemTemplate>
                                                        <asp:Button ID="Button1" Text="追加" runat="server" CssClass="Select_button" CommandName="Update" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id")%>' 
                                                         />
                                                    </ItemTemplate>
                                                    <HeaderStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="31px" Width="100px" />
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Height="33px" Width="100px"/>
                                                </asp:TemplateField>

                                                  <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1px" CssClass="VR_line" />
                                                    <HeaderStyle Width="1px" CssClass="VR_line" />
                                                </asp:TemplateField>

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
        <asp:Button ID="btnSearch" runat="server" Text="导出" CssClass="Login_Button_bg" OnClick="btnSearch_Click" OnClientClick="flgbtnCSV = true;"  />
    </div>
</asp:Content>
<%-- the Button Item ED--%>
