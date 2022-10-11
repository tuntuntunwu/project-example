<%@ Page Language="c#" Inherits="MFPScreen_UPD2" CodeFile="UserPrintDetailsFor2.aspx.cs" ContentType="text/xml"
    ResponseEncoding="GB2312" %>

<html>
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form class="osa_fields" title="Sharp Simple EA" action="UserPrintDetailsFor2.aspx">
    <input id="id_print" value="打印" type="submit" />
    <input id="id_delete" value="删除" type="submit"  />
    <input id="id_use_mfp" value="使用复合机" type="submit"/>
    <input id="id_logout" value="注销"  type="submit" />
    
    <fieldset title='任务列表'>
        <%=content%>
    </fieldset>
    <input id="status" name="status" type="hidden" value="validate" />
    </form>
</body>
</html>
