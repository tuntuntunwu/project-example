<%@ Page Language="C#" MasterPageFile="~/Masterpage/SimpleEAMasterPage.master" AutoEventWireup="true"
    CodeFile="AddPrice.aspx.cs" Inherits="PirceInfo_AddPrice" Title="Untitled Page" %>

<%@ MasterType TypeName="Masterpage_SimpleEAMasterPage" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="cphSmallbody" runat="server">

    <script type="text/javascript" src="../JavaScript/jquery.custom_radio_checkbox.js"></script>

    <script type="text/javascript" language="javascript">
        window.onload = function () {
            var doc = document;
            if (doc.getElementById("Scroller-Res")) {
                loadScriptScroller(function () {
                    var scroller = null;
                    var scrollbar = null;
                    // 2011.01.07 Update By SES Zhou Miao Ver.1.1 Update ST
                    //                    // 2011.01.06 Update By SES Zhou Miao Ver.1.1 Update ST
                    //                    //scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400, -100);
                    //                     var version = navigator.appVersion;
                    //                     if ((version.indexOf('MSIE 7.0') != -1) ||(version.indexOf('MSIE 6.0') != -1) || (version.indexOf('MSIE 5.5') != -1))
                    //                      {
                    //                           scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400,  -138);
                    //                      }
                    //                     else
                    //                     {
                    //                           scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400,  -100);
                    //                     }
                    //                    // 2011.01.06  Update By SES Zhou Miao Ver.1.1 Update ED
                    var version = navigator.appVersion;
                    if (version.indexOf('MSIE 7.0') != -1) {
                        scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400, -135);
                    }
                    else if ((version.indexOf('MSIE 6.0') != -1) || (version.indexOf('MSIE 5.5') != -1)) {
                        scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400, -118);
                    }
                    else {
                        scroller = new jsScroller(doc.getElementById("Scroller-Res"), 400, -89);
                    }

                    // 2011.01.07 Update By SES Zhou Miao Ver.1.1 Update ED
                    scrollbar = new jsScrollbar(doc.getElementById("Scrollbar-Container"), scroller, false);
                    scroller = null;
                    scrollbar = null;
                });
            }
        }

        $(document).ready(function () {
            // 2010.11.23 Add By SES Jijianxiong Ver.1.1 Update ST
            // Bug in the IE 6
            var version = navigator.appVersion;
            if ((version.indexOf('MSIE 6.0') != -1) || (version.indexOf('MSIE 5.5') != -1)) {
                $(".toggle_container1").show();
                $("h2.trigger").addClass("active");
            }
            // 2010.11.23 Add By SES Jijianxiong Ver.1.1 Update ED

            // setting radio button
            $(".radio").dgStyle();
            grid_resize();
        });
        function onAddValueKeyUp(txtbx) {
            if (!$(txtbx).attr("lastValue")) {
                $(txtbx).attr("lastValue", "");
            }
            if ($(txtbx).val() == "") {
                $(txtbx).attr("lastValue", "");

            } else if (/^((-?\d+(\.\d{1,2})?)|(-?\d+\.)|(-))$/.test($(txtbx).val())) {
                $(txtbx).attr("lastValue", $(txtbx).val());

            } else {
                $(txtbx).val($(txtbx).attr("lastValue"));
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        loadScript("../js/Collaped_expand.js");
        loadScript("../js/Min_res_grid.js");
        loadScript("../js/Min_radio_res.js");
        loadScript("../js/Min_resize.js");
    </script>

    <table style="width: 100%; border-collapse: collapse; border: 0;" class="Table_dataFont"
        cellspacing="0" cellpadding="0">
       
    </table>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cphbody" runat="Server">
    <script language="javascript" type="text/javascript">
    
        $(document).ready(function(){
            $("td.Add_User_Titlebg").each(function(){
                $(this).removeAttr("style");
                $(this).removeAttr("width");
            });
            $("tr.Light_GrayFont").each(function(){
                $(this).find("td").each(function(){
                    $(this).removeAttr("style");
                    $(this).removeAttr("width");
                });
            });
        });

        function HookUpControl(curObj, validatorClientID) {
            var validationControl = document.getElementById(validatorClientID);
            validationControl.controltovalidate = curObj.id;
            // validationControl.clientvalidationfunction = "validatetextbox";
            validationControl.ClientValidationFunction = "validatetextbox";
            //   validationControl.validateemptytext = "true";
            validationControl.ValidateEmptyText = "True";
            ValidatorHookupControl(curObj, validationControl);
        }

        function validatetextbox(sender, args) {
            var btn = document.getElementById("<%=btnAddClientid%>");
            
            if (args.Value == "") 
            {
                sender.errormessage = "<b><font color='red'> 该栏位不能为空。</font></b>";
                sender.innerHTML = "<b><font color='red'> 该栏位不能为空。</font></b>";

                args.IsValid = false;
        
                 return false;
            }
            if (isNaN(args.Value)) {
               // alert(args.Value);
                sender.errormessage = "<b><font color='red'> 该栏位只能是数字。</font></b>";
                sender.innerHTML = "<b><font color='red'> 该栏位只能是数字。</font></b>";
                args.IsValid = false;
         
                return false;
            }
            if (args.Value.indexOf('.') >= 0) {
                var xsd = args.Value.toString().split(".");
                xsd[1].length < 2
                if (xsd.length == 2) {
                    if (xsd[1].length > 2) {
                        sender.errormessage = "<b><font color='red'> 该栏位的值小数点只能保留2位。</font></b>";
                        sender.innerHTML = "<b><font color='red'> 该栏位的值小数点只能保留2位。</font></b>";
                        args.IsValid = false;

                        return false;
                    }
                }
            }
             if (Number(args.Value) < 0) {
                  sender.errormessage = "<b><font color='red'> 该栏位的值不能小于0。</font></b>";
                  sender.innerHTML = "<b><font color='red'> 该栏位的值不能小于0。</font></b>";
                  args.IsValid = false;
               
                  return false;
              } 
             
        }
    
                   
    </script>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 1%;" align="left" valign="top" class="Table_header_left">
            </td>
            <td style="width: 98%; height: 2px;" align="left" valign="middle" class="bottom_HR_line">
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
                        <td style="width: 97%;" align="left" valign="top">


    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr id="TR_ID1" class="Light_GrayFont">
            <td align="left" valign="top" class="Table_header_left" style="width: 1%">
            </td>
            <td align="left" valign="middle" style="height: 2px; width: 98%">
                价格方案名:     <asp:TextBox ID="txt_price_name" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox> 
                                            <img alt="Req" src="../Images/Req_star.png" width="5"  height="5" style="vertical-align: middle" />
                                           
                                            <asp:RequiredFieldValidator ID="rfvModelName"
                                                    ControlToValidate="txt_price_name" runat="server" Display="Dynamic" ValidationGroup="UpdateGroupName"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator
                                                        ID="revModelName" ControlToValidate="txt_price_name" Display="Dynamic" runat="server"
                                                        ValidationGroup="UpdateGroupName"></asp:RegularExpressionValidator>
                                                        
                                                        <asp:CustomValidator
                                                            ID="valPirceSN" ControlToValidate="txt_price_name" runat="server" ValidationGroup="UpdateGroupName"
                                                            Display="Dynamic" OnServerValidate="valPriceSN_ServerValidate"></asp:CustomValidator>
                                                        </td>
            <td align="left" valign="top"  style="width: 1%">
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" class="Left_VRLine">
                &nbsp;
            </td>
            <td align="left" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="top" style="height: 448px;border-right:1px solid #808080;margin-left:1px;" class="Scroll_td">
                            <table name="StupidTable" width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 83px;" align="left" valign="middle" class="Add_User_Titlebg">
                                        功能</td>
                                         <td style="width: 0%;" align="left" valign="top" class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                    <td style="width: 109px;" align="left" valign="middle" class="Add_User_Titlebg">
                                        纸张类型</td>
                                    <td style="width: 16%;" align="left" valign="top" class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                    <td style="width: 49%;" align="left" valign="middle" class="Add_User_Titlebg">
                                        &nbsp;
                                        纸张单价</td>
                                         <td style="width: 0%;" align="left" valign="top" class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                    <td style="width: 73%;" align="left" valign="middle" class="Add_User_Titlebg">
                                        黑白单价</td>
                                         <td style="width: 0%;" align="left" valign="top" class="Add_User_Titlebg">
                                        &nbsp;
                                    </td>
                                    <td style="width: 73%;" align="left" valign="middle" class="Add_User_Titlebg">
                                        彩色单价</td>
                                    
                                </tr>
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="width: 83px; " rowspan="15">
                                        打印或复印</td>
                                     <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        A3 
                                    </td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Print_A3_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                           
                                    </td>
                                     <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_A3_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                     <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_A3_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                
                                </tr>
                                 <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        A4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Print_A4_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_A4_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                        
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_A4_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        A5</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Print_A5_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_A5_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_A5_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                               <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        B4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Print_B4_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_B4_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_B4_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                               <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        B5</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Print_B5_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_B5_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_B5_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        小于A4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Print_SA_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_SA_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_SA_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        大于A4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Print_BA_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_BA_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_BA_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                               
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                           <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        Others</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Print_OT_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_OT_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Print_OT_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                             <!--SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS-->
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="width: 83px; " rowspan="15">
                                        扫描</td>
                                     <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        A3 
                                    </td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Scan_A3_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                           
                                    </td>
                                     <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_A3_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                     <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_A3_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                
                                </tr>
                                 <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        A4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Scan_A4_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_A4_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                        
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_A4_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        A5</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Scan_A5_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_A5_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_A5_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                               <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        B4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Scan_B4_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_B4_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_B4_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                               <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        B5</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Scan_B5_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_B5_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_B5_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        小于A4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Scan_SA_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);"  runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_SA_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_SA_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        大于A4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Scan_BA_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_BA_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_BA_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                               
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                           <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        Others</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Scan_OT_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_OT_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Scan_OT_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                             <!--SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS-->
                                <tr class="Light_GrayFont">
                                    <td align="left" valign="middle" style="width: 83px; " rowspan="15">
                                        传真</td>
                                     <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        A3 
                                    </td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Fax_A3_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                           
                                    </td>
                                     <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_A3_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                     <td align="left" valign="top" class="VR_line">
                                        &nbsp;
                                    </td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_A3_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                
                                </tr>
                                 <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        A4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Fax_A4_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_A4_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                        
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_A4_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        A5</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Fax_A5_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_A5_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_A5_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                               <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        B4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Fax_B4_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_B4_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_B4_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                               <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        B5</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Fax_B5_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_B5_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_B5_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        小于A4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Fax_SA_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_SA_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_SA_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                                <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        大于A4</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Fax_BA_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_BA_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_BA_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                               
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                           <tr class="Light_GrayFont">
                                  <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" style="width: 109px; height: 45px;">
                                        Others</td>
                                    <td align="left" valign="top" class="VR_line" style="width: 16%">
                                        &nbsp;</td>
                                    <td style="width: 49%; height: 37px; padding-left: 20px;" align="left" 
                                        valign="middle">
                                        <asp:TextBox ID="txt_Fax_OT_Paper_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_OT_BW_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                                           <td align="left" valign="top" class="VR_line">
                                        &nbsp;</td>
                                    <td style="width: 86%; height: 37px; padding-left: 20px;" align="left" valign="middle">
                                        <asp:TextBox ID="txt_Fax_OT_FullColor_Price" onkeyup="onAddValueKeyUp(this);" onbeforepaste="onAddValueKeyUp(this);" runat="server" CssClass="Inputtextbox" 
                                            MaxLength="30"></asp:TextBox>
                                            
                                    </td>
                               
                                </tr>
                                   <tr>
                                    <td colspan="9" class="HR_Line" style="height: 2px">
                                    </td>
                                   
                                </tr>
                             <!--SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS-->
                            </table>
                        </td>
                    </tr>
                    <tr class="Light_GrayFont">
                        <td align="left" valign="top" colspan="9" class="" style="border-bottom:1px solid #808080;border-right:1px solid #808080;height: 37px;">
                            &nbsp;金额的计算方法：张数 * 纸张价格 +  面数 * 黑白价格（或彩色价格）
                         <!--  <asp:RadioButton ID="ridMethod1" runat="server" GroupName="price_method" Text="双面=面数（耗材）*单价" Checked="True" />  -->
                         <!--   <asp:RadioButton ID="ridMethod2" GroupName="price_method" runat="server" Text="双面=总张数+面数*单价" />  -->
                        </td>
                    </tr>
                </table>
      <!--   
      <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ClientValidationFunction="validatetextbox" ValidateEmptyText="True" ></asp:CustomValidator>
     -->
      <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    </table>
    </td>
                        <td style="width: 3%;" align="left" valign="top" class="VR_line">
                            &nbsp;
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
    <asp:HiddenField ID="hidPriceId" runat="server" />
</asp:Content>
<asp:Content ID="ContentFoot" ContentPlaceHolderID="cphfoot" runat="server">
    <asp:Button ID="btnAdd" runat="server" Text="追加" OnClick="btnAdd_Click" ValidationGroup="UpdateGroupName"
        CssClass="Login_Button_bg" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server"
            Text="取消" CssClass="Login_Button_bg" OnClick="btnCancel_Click" />
</asp:Content>
