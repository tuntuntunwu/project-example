<%@ Page Language="C#" Inherits="RegisterICCardForOSA3" CodeFile="RegisterICCardForOSA3.aspx.cs" ContentType="text/xml"  ResponseEncoding="GB2312"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<script type="text/javascript" src="Js/httpRequest.js"></script>
<script type="text/javascript">
var iccard = <%=cardId %>;

    function bindICCard() {

        //--
        var paramList;
        paramList["ICCard"]=iccard;
        paramList["UserId"]=document.getElementById('id_Login').value;
        paramList["Password"]=document.getElementById('id_password').value;
        CreateXmlHttpRequest(paramList, "ICCardBinding.aspx", "ErrMFP.aspx", 5000, GetResult);

    }
    function GetResult(reParam) {
        if (reParam == 1) {
            alert("IC卡绑定成功!请重新登录！");
            document.location = "OsaMain3.aspx";
        }
        else
            if (reParam == 2) {
                alert("用户名或密码错误，请重试！");
            }
            else
                alert("系统出现错误，请重试或联系管理员！");
    }

</script>
<body>
<form class="osa_login" title="IC卡注册" action="OsaMain.aspx">
        <img id='id_logo' height='32px' width='32px' src='Images_mfp/Fullcolor/id_logo.GIF' />
        <img id='id_footer' height='48px' width='180px' src='Images_mfp/Fullcolor/id_footer.GIF' />
        <p>
            <%=div_error%>
        </p>
       <img id='id_background' height='170px' width='634px' src="Images_mfp/Fullcolor/MFP_INPUT.GIF" />
        <fieldset title="IC卡绑定登录名和密码：">
            <input name="id_Login" type="text" title="登录名：" keyboard="normal" format="text" value=''
                maxlength="10" />
            <input name='id_password' type="password" title='密码：' keyboard="normal" value='' format='password' />
        </fieldset>
        <input id="id_ok" value="注册" onclick="bindICCard()" />
        <input type="submit" id="btnReturn" value="退出" />
        <input type="hidden" id="hidICCardFlg" value="false" />
        <input id="status" name="status" type="hidden" value="validate" />
    </form>
</body>
</html>
