<%--
// ==============================================================================
// File Name           : OsaMain.aspx
// Description         : OsaMain.aspx
// Author(s)           : Ji Jianxiong
//                       Build No: 1.0.3.2: UI Update.
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
--%>
<%@ Page Language="c#" Inherits="OsaMain" CodeFile="OsaMain.aspx.cs" ContentType="text/xml"  ResponseEncoding="GB2312"%>

<html>
<body>
<form class="osa_login" title="µÇÂ¼" action="OsaMain.aspx">
        <img id='id_logo' height='32px' width='32px' src='Images_mfp/Fullcolor/id_logo.GIF' />
        <img id='id_footer' height='48px' width='180px' src='Images_mfp/Fullcolor/id_footer.GIF' />
        <p>
            <%=div_error%>
        </p>
        <%if (ICCardFlg != true)
   {%>
       <img id='id_background' height='170px' width='634px' src="Images_mfp/Fullcolor/MFP_INPUT.GIF" />
        <fieldset title="ÇëÊäÈëµÇÂ¼ÃûºÍÃÜÂë£º">
            <input name="id_Login" type="text" title="µÇÂ¼Ãû£º" keyboard="normal" format="text" value=''
                maxlength="30" />
            <input name='id_password' type="password" title='ÃÜÂë£º' keyboard="normal" value='' format='password' />
        </fieldset>
        <% }
      else
      {%>
        <img id='id_background' height='170px' width='634px' src="Images_mfp/Fullcolor/MFP_IC.GIF"  />
        <fieldset title="ÇëË¢£É£Ã¿¨£º">
            <input name='id_ic' type="password" title='IC¿¨ºÅ£º' keyboard="password" value='' format='password' />
        </fieldset>
        <%} %>
        <input id="id_ok" value="µÇÂ¼" />
        <%if (ICInput == true && ICCardFlg == true)
              { %>
        <input type="submit" id="btnInput" value="Ö±½ÓÊäÈë" />
        <input type="hidden" id="hidICCardFlg" value="true" />
        <%}
              else if ( ICInput == false && ICCardFlg == false)
              { %>
        <input type="submit" id="btnReturn" value="·µ»Ø" />
        <input type="hidden" id="hidICCardFlg" value="false" />
        <%} %>
        <input id="status" name="status" type="hidden" value="validate" />
    </form>
</body>
</html>
