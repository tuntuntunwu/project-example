<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MFPPrintDetailsMasterPage.master" AutoEventWireup="true"
 CodeFile="ProductKeyInvalid.aspx.cs" Inherits="MFPScreen_ProductKeyInvalid" %>


 <%@ MasterType TypeName="Masterpage_MFPPrintDetailsMasterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphlisthead" Runat="Server" Visible="false">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbody" Runat="Server">
    <table style="width: 100%; height:400px; table-layout: fixed;" cellpadding="0" cellspacing="0"
        class="Invalid_License">
        <tr style="height: 380px">
            <td style="width: 98%;" align="center" valign="middle" class="TableGrid_bg">
                <table>
                    <tr>
                        <td class="Invalid_Licens" style="width: 20%;" align="left">
                        </td>
                        <td style="width: 80%;" align="center" valign="middle">
                            <asp:Label ID="lblTitle" Text="系统未注册或注册码到期，请联系管理员！" runat="server" Font-Size="Medium">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 45px">
                        </td>
                        <td style="height: 45px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 98%; padding-left: 40px" align="right" valign="middle" class="TableGrid_bg">
                            <asp:ImageButton runat="server" ID="imgBtnFlash" OnClick="imgBtnFlash_Click" ImageUrl="../Images_mfp/8.png"
                                Height="66px" Width="160px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 98%; height: auto;" align="right" valign="middle" class="TableGrid_bg">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" Runat="Server" Visible="false">
</asp:Content>

