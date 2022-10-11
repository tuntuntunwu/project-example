<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpage/MFPPrintDetailsMasterPage.master" AutoEventWireup="true" 
CodeFile="UserPrintDetails.aspx.cs" Inherits="MFPScreen_UserPrintDetails" EnableSessionState="True" EnableEventValidation ="false" %>

<%@ MasterType TypeName="Masterpage_MFPPrintDetailsMasterPage" %>
<asp:Content ID="ContentListHead" ContentPlaceHolderID="cphlisthead" runat="Server">
    <script language="javascript" src="../Js/MFP_common.js" type="text/javascript"></script>
    <script language ="javascript" type ="text/javascript" >
      function select_all() {
          var a = document.getElementsByTagName("input");
          for (var j = 0; j < a.length; j++) {
              if (a[j].type == "checkbox")
                  a[j].checked = document.all.selectall.checked;
          }
      }

      window.onload = SetTime;

      function SetTime() {
          document.getElementById('<%=hidRecordTime.ClientID %>').value = new Date().getMinutes();
      }

      setInterval(function () {
          var result = record_session(document.getElementById('<%=hidRecordTime.ClientID %>').value, "<%=timeOutPeriod%>");
          if (result == "timeout")
          // Edit by Zhengwei 20150113
          //{
          //    document.location = "../Main.aspx";
         // }

          {
              document.getElementById('<%=ImageButton1.ClientID %>').click();
           //location.reload(true);
           }
          //END
      }, 60000);


      
  </script>

    <table width="100%" cellpadding="0" cellspacing="0" align="center" border="1" style="border-right :none; border-bottom :none; border-left:none; border-top:none">
        <tr>
            <td id="Td1" style="border-style: none; border-color: inherit; border-width: medium; height: 60px; width: 5%; " 
                align="center" valign="middle" runat="server" >
                &nbsp;</td>
            <td style="height: 60px; width: 95%; border:none" align="center" valign="middle" runat="server" id="lblTdName1">
                <asp:Label ID="lblUserName" runat="server" Text="用户">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td class="MFPHR_Line" 
                style="border-style: none; border-color: inherit; border-width: medium; height: 5px; width: 5%;">
                &nbsp;</td>
            <td class="MFPHR_Line" 
                style="height: 5px;border-bottom:none; border-top:none; border-left :none; border-right :none">
            </td>
        </tr>
        <tr>
            <td id="Td2" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server">
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="lblTdCommon1">
                <asp:Label ID="label2" runat="server" Text="总余额: ">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td id="Td12" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server">
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="Td13">
                <asp:Label ID="lblAllMoney" runat="server" Text="a">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td id="Td3" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server">
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="lblTdCommon2">
                <asp:Label ID="label3" runat="server" Text="其中彩色余额: ">
                </asp:Label>
                
            </td>
        </tr>
        <tr>
            <td id="Td4" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server" >
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="lblTdCommon3">
                <asp:Label ID="lblColorMoney" runat="server" Text="a">
                </asp:Label>
           </td>
        </tr>
        <tr>
          <td style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " >&nbsp; </td>
          <td style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " >&nbsp; </td>
        </tr>
        <tr>
            <td id="Td5" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server" >
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align= "left" valign="middle" runat="server" id="lblTdTitle2">
                <asp:Label ID="label4" runat="server" Text="可用功能：">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td id="Td6" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server" >
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="lblTdCommon4">
        
                <asp:Image ID="imgColCopy" runat="server" 
                    ImageUrl="~/Images/Radio_Unselected.png" />
                彩色复印
            </td>
        </tr>
        <tr>
            <td id="Td7" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server">
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="lblTdCommon5">
               <asp:Image ID="imgBWCopy" runat="server" 
                    ImageUrl="~/Images/Radio_Unselected.png" /> 黑白复印
            </td>
        </tr>
        <tr>
            <td id="Td8" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server">
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="lblTdCommon6">
               <asp:Image ID="imgColPrint" runat="server" 
                    ImageUrl="~/Images/Radio_Unselected.png" />  彩色打印
            </td>
        </tr>
        <tr>
            <td id="Td9" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server" >
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="lblTdCommon7">
               <asp:Image ID="imgBWPrint" runat="server" 
                    ImageUrl="~/Images/Radio_Unselected.png" />  黑白打印
            </td>
        </tr>
        <tr>
            <td id="Td10" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server">
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="lblTdCommon8">
              <asp:Image ID="imgScan" runat="server" 
                    ImageUrl="~/Images/Radio_Unselected.png" />  扫描
            </td>
        </tr>
        <tr>
            <td id="Td11" style="border-style: none; border-color: inherit; border-width: medium; width: 5%; height: 20px; " 
                align="center" valign="middle" runat="server">
                &nbsp;</td>
            <td style="width: 95%; height: 20px; border:none" align="left" valign="middle" runat="server" id="lblTdCommon9">
                <asp:Image ID="imgFax" runat="server" 
                    ImageUrl="~/Images/Radio_Unselected.png" />  传真
                <asp:HiddenField ID="hidUID" runat="server" Value="" />
                <asp:HiddenField ID="hidSN" runat="server" Value="" />
                <asp:HiddenField ID="hidRemoteIP" runat="server" Value="" />
                <asp:HiddenField ID="hidLoginName" runat="server" Value="" />
                <asp:HiddenField ID="hidRecordTime" runat="server" Value="" />
                <asp:HiddenField ID="hidIsTimeout" runat="server" Value="0" />
            </td>
        </tr>      
      <!--
        <tr>
            <td style="width: 95%; height: 5px; border:none" align="center" valign="middle">
            </td>
        </tr>
        <tr>
            <td class="MFPHR_Line" colspan="5" style="height: 5px;border-bottom:none; border-top:none; border-left :none; border-right :none">
            </td>
        </tr>
        <tr style="height:20%">
            <td style="height: 100px; width: 95%; border:none" align="center" valign="middle" runat="server"  id="btnTdEnter3" >
                <asp:ImageButton runat="server" ID="imgBtnRegister" OnClick ="imgBtnRegister_OnClick" ImageUrl="../Images_mfp/screen4/6.png" />
            </td>
        </tr>
       -->
    </table>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" runat="Server">
    <table style="width: 100%; height:auto;table-layout:fixed;" cellpadding="0" cellspacing="0">
        <tr style ="height:auto">
            <td style="width: 98%; height:30px;" align="center" valign="middle" class="TableGrid_bg">
            <asp:Label ID= "lblTitle" Text="作业列表" runat="server" Font-Size="Large">
            </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style=" height: 260px;">
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="top" style="width: 97%;" class="Scroll_td">
                            <asp:GridView ID="CustomersGridView" runat="server" PageSize="5" BorderStyle="Solid"
                                DataSourceID="SqlDataListSource" OnRowDataBound="CustomView_RowDataBound" 
                                GridLines="None" CellPadding="4"
                                DataKeyNames="MFPPrintTaskID" EnableModelValidation="True" 
                                ForeColor="#333333" Width="100%" AutoGenerateColumns="False"  >
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate >
                                            <asp:CheckBox ID="chkSelect" runat="server" Text="" Width="10%"/>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <input type="checkbox" id="selectall" onclick="select_all()">
                                        </HeaderTemplate>
                                        <HeaderStyle Width="10%" Height="15px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Width="10%"
                                            Height="12px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CreateTime"   HeaderText="提交任务时间" >
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40%" />
                                    <HeaderStyle Height="15px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField DataField="PrintFileName" HeaderText="打印文件名称">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%" />
                                    </asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="打印文件名称" SortExpression="FileName">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl" runat="server" Text='<%# PartSubString(Eval("PrintFileName").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%" />
                                        <ItemStyle Wrap="true" CssClass="Automatic_Wrap" Width="50%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <PagerSettings Visible ="false" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#0033FF" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#C4C4C4" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height:48px">
                <table style="width: 100%; height:48px" cellpadding="0" cellspacing="0">
                    <tr >
                    <td style="width: 40%;"></td>
                        <td style="width: 10px;" align="center" valign="middle">
                            <asp:ImageButton ID="lbtnPrePage" runat="server" OnClick="IndexOfPage_Pre" ImageUrl="../Images_mfp/Arrow_Pre.png"
                                Width="16px" Height="16px"></asp:ImageButton>
                        </td>
                        <td style="width: 15px;" align="center" valign="middle">
                            <asp:Label ID="lblCurrentPage" runat="server" Text="1"></asp:Label>
                        </td>
                        <td style="width: 1px;" align="center" valign="middle">
                            <asp:Label ID="Label18" runat="server" Text="|"></asp:Label>
                        </td>
                        <td style="width: 15px;" align="center" valign="middle">
                            <asp:Label ID="lblTotalPage" runat="server" Text="10"></asp:Label>
                        </td>
                        <td style="width: 10px;" align="center" valign="middle">
                            <asp:ImageButton ID="lbtnNextPage" runat="server" OnClick="IndexOfPage_Next" ImageUrl="../Images_mfp/Arrow_Next.png"
                                Width="16px" Height="16px"></asp:ImageButton>
                        </td>
                        <td style="width: 40%;"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataListSource" runat="server" SelectCommand="">
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" runat="Server">
    <table width="100%" border="1" style="height:auto; border:1; border-right :none; border-bottom :none; border-left:none; border-top:none" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td style="height:55px ; width: 95%; border:none" align="center" valign="middle" runat="server" id="btnTdEnter1">
            <asp:ImageButton runat="server" ID="BtnEntry" OnClick="btnEntry_OnClick" 
                    ImageUrl="../Images_mfp/screen3/1-1.png" />
            </td>
        </tr>
        <tr>
            <td class="MFPHR_Line" colspan="5" style="height: 5px;border-bottom:none; border-top:none; border-left :none; border-right :none">
            </td>
        </tr>
        <tr >
            <td style="height: 55px; width: 95%; border:none" align="center" valign="middle" runat="server" id="BtnTdCommon2">
            <asp:ImageButton runat="server" ID="btnPrintAndDelete" ImageUrl="../Images_mfp/screen3/2.png" OnClick="btnPrintAndDelete_Click" OnClientClick="SetTime();"/>
            </td>
        </tr>
        <tr>
            <td style="height: 50px; width: 95%; border:none" align="center" valign="middle" runat="server" id="BtnTdCommon3">
            <asp:ImageButton runat="server" ID="btnPrint" OnClick="btnPrint_Click" ImageUrl="../Images_mfp/screen3/5.png" OnClientClick="SetTime();" />
            </td>
        </tr>
        <tr>
            <td style="height: 50px; width: 95%; border:none" align="center" valign="middle" runat="server" id="BtnTdCommon4">
            <asp:ImageButton runat="server" ID="btnDelete" OnClick="btnDelete_Click"  ImageUrl="../Images_mfp/screen3/3.png" OnClientClick="SetTime();" />
            </td>
        </tr>
        <tr>
            <td style="height:60px; width: 95%; border:none" align="center" valign="middle" runat="server" id="BtnTdCommon5">
            <asp:ImageButton runat="server" ID="btnFlash" ImageUrl="../Images_mfp/screen3/4.png" OnClick="btnFlash_Click" OnClientClick="SetTime();"/>
            </td>
        </tr>
        <tr>
            <td class="MFPHR_Line" colspan="5" style="height: 5px; border-bottom:none; border-top:none; border-left :none; border-right :none">
            </td>
        </tr>
        <tr >
            <td style="height: 60px; width: 95%; border-top:MFPHR_Line; border :none" align="center" valign="middle" runat="server" id="btnTdEnter2">
                <asp:ImageButton runat="server" ID="ImageButton1" OnClick="btnExit_OnClick"  ImageUrl="../Images_mfp/screen3/7.png" />
            </td>
        </tr>
    </table>
</asp:Content>

