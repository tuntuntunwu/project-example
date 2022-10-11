<%--
// ==============================================================================
// File Name           : OsaMain3.aspx
// Description         : OsaMain3.aspx for OSA 2.0 (256 Color)
// Author(s)           : Ji Jianxiong
//                       Build No: 1.1
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>

<%@ Page Language="c#" Inherits="OsaMain3" CodeFile="OsaMain3.aspx.cs" ContentType="text/xml"
    ResponseEncoding="GB2312" %>

<html>
<body>
    <form class="osa_login" title="µÇÂ¼" action="OsaMain3.aspx">
        <img id='id_logo' height='32px' width='32px' src='Images_mfp/256color/id_logo.gif' />
        <img id='id_footer' height='48px' width='180px' src='Images_mfp/256color/id_footer.gif' />
        <img id='id_background' height='170px' width='634px' src='Images_mfp/256color/MFP_INPUT.gif' />
        <p>
            <%=div_error%>
        </p>
            <input id="id_ok" value="µÇÂ¼" />
        <fieldset title="ÇëÊäÈëµÇÂ¼ÃûºÍÃÜÂë£º">
            <input name="id_Login" type="text" title="µÇÂ¼Ãû£º" keyboard="normal" format="text" value=''
                maxlength="30" />
            <input name='id_password' type="password" title='ÃÜÂë£º' keyboard="normal" value=''
                format='password' />
        </fieldset>
        <input id="status" name="status" type="hidden" value="validate" />
    </form>
</body>
</html>
