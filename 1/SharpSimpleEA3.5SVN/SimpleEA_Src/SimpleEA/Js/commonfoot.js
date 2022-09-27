// ==============================================================================
// File Name           : commonfoot.js
// Description         : commonfoot
// Author(s)           : 
// Date created        : 
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================
 /********************************************
 * FUNCTION : ValidatorUpdateDisplay
 * SUMMARY  : ValidatorUpdateDisplay
 *
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.06.24
 * VERSION  : 0.01
 *******************************************/
function ValidatorUpdateDisplay(val) 
{
    // Get the tagName = "TR"
    var obj = val;
    var k = 0;
    // For all Validator Control is in the Table rows.
    for (var i = 0 ; i == 0 ; i = i+ 0 ) 
    {
        obj = obj.parentNode;
        if ( obj == null ) 
        {
            break;
        }
        if ( obj.tagName == "TR" && ( obj.className.indexOf("Light_GrayFont") > -1 ||  obj.className.indexOf("Req_Filed_red") > -1 ) ) 
        {
            break;
        }
        // Safe mode loop the for.
        k = k + 1;
        if ( k == 10 ) 
        {
            break;
        }
    }
    
    if (typeof(val.display) == "string") 
    {
        if (val.display == "None") 
        {
            return;
        }
        if (val.display == "Dynamic") 
        {
            val.style.display = val.isvalid ? "none" : "inline";
            
            // Get the classname
            var cssname = Trim(obj.className);

            if ( val.isvalid ) 
            {
                // The Css has be changed by check control.
                if ( cssname.indexOf( "Req_Filed_red" ) >= 0 ) 
                {
                    // In the Css has this check control's id
                    if ( cssname.indexOf( val.id ) >= 0 ) 
                    {
                        cssname = cssname.replace( val.id , "" );
                        cssname = Trim( cssname );
                        // other id is not existed.
                        if ( cssname == "Req_Filed_red" ) 
                        {
                            cssname = "Light_GrayFont";
                        }
                    }
                }

                
            } 
            else 
            {
                // The Css does not be changed by other check control.
                if ( cssname.indexOf( "Light_GrayFont" ) >= 0 ) 
                {
                    cssname = "Req_Filed_red" + " " + val.id;
                } 
                else if ( cssname.indexOf( "Req_Filed_red" ) >= 0 )
                {
                    // The Css has be changed by other check control.
                    // the check control hasnot checked.
                    if ( cssname.indexOf( val.id ) < 0 ) 
                    {
                        cssname = cssname  + " " + val.id;
                    }
                }
            }
            
            obj.className = Trim(cssname);
            val.style.color = "White";
            return;
        }
    }
    if ((navigator.userAgent.indexOf("Mac") > -1) &&
        (navigator.userAgent.indexOf("MSIE") > -1)) 
    {
        val.style.display = "inline";
    }
    val.style.visibility = val.isvalid ? "hidden" : "visible";
    // Get the classname
    var cssname = Trim(obj.className);

    if ( val.isvalid ) 
    {
        // The Css has be changed by check control.
        if ( cssname.indexOf( "Req_Filed_red" ) >= 0 ) 
        {
            // In the Css has this check control's id
            if ( cssname.indexOf( val.id ) >= 0 ) 
            {
                cssname = cssname.replace( val.id , "" );
                cssname = Trim( cssname );
                // other id is not existed.
                if ( cssname == "Req_Filed_red" ) 
                {
                    cssname = "Light_GrayFont";
                }
            }
        }

        
    } 
    else 
    {
        // The Css does not be changed by other check control.
        if ( cssname.indexOf( "Light_GrayFont" ) >= 0 ) 
        {
            cssname = "Req_Filed_red" + " " + val.id;
        } 
        else if ( cssname.indexOf( "Req_Filed_red" ) >= 0 )
        {
            // The Css has be changed by other check control.
            // the check control hasnot checked.
            if ( cssname.indexOf( val.id ) < 0 ) 
            {
                cssname = cssname  + " " + val.id;
            }
        }
    }
    
    obj.className = Trim(cssname);
    val.style.color = "White";
}



  /********************************************
   * FUNCTION : __doPostBack
   * SUMMARY  : __doPostBack
   * AUTHOR   : SES Ji JianXiong
   * DATE     : 2010.08.20
   * VERSION  : 0.01
  *******************************************/
  function __doPostBack(eventTarget, eventArgument) 
  {
     if (!theForm.onsubmit || (theForm.onsubmit() != false)) 
     {         
         Showbox();
         theForm.__EVENTTARGET.value = eventTarget;
         theForm.__EVENTARGUMENT.value = eventArgument;
         theForm.submit();
      }
 }
        
//        document.onreadystatechange = function () {
//            if ( document.readyState == "complete" ) {
//                Close_light();
//            }
//        }

  var onsubmitscreen = function() 
  {
  // before do showbox function. check the form status.
     if ( event ) 
     {
        if ( event.returnValue == false ) 
        {
            return false;
        }
     }
            
      Showbox();
            
      return true;
   };        
   
    
 function checkErrorInfo() 
 {
     if (typeof(Page_Validators) == "undefined") 
     {
         return true;
     }
     
     for (var item = 0; item < Page_Validators.length; item++) 
     {
         var ctrl = Page_Validators[item];
         
         if((typeof(ctrl.display) == "string" && ctrl.style.display == "")||( typeof(ctrl.display) != "string" && ctrl.style.visibility != "hidden")) 
         {
             // Get the tagName = "TR"
             var obj = ctrl;
             var k = 0;
             // For all Validator Control is in the Table rows.
             for (var i = 0 ; i == 0 ; i = i+ 0 ) 
             {
                 obj = obj.parentNode;
                 if ( obj == null ) 
                 {
                     break;
                 }
                 if ( obj.tagName == "TR" ) 
                 {
                     break;
                 }
                 // Safe mode loop the for.
                 k = k + 1;
                 if ( k == 10 ) 
                 {
                     break;
                 }
              }
              // Get the classname
             var cssname = Trim(obj.className);

             // The Css does not be changed by other check control.
             if ( cssname.indexOf( "Light_GrayFont" ) >= 0 ) 
             {
             cssname = "Req_Filed_red" + " " + ctrl.id;
              } 
             else if ( cssname.indexOf( "Req_Filed_red" ) >= 0 )
             {
                // The Css has be changed by other check control.
                // the check control hasnot checked.
                if ( cssname.indexOf( ctrl.id ) < 0 ) 
                {
                 cssname = cssname  + " " + ctrl.id;
                }
             }
         
            obj.className = Trim(cssname);
            ctrl.style.color = "White";
        }
    }

}    

   function CheckDispItem() 
   {
        var classname = "checkdispitem";
        var objlist = $("." + classname);
        var isCheck = false;
        objlist.each(
            function() 
            {
                var span = this;
                // Get the Check box
                var checkbox = span.childNodes[0];
                
                if ( checkbox.checked )
                 {
                    isCheck = true;
                    return true;
                }
            }
        );
        
        if ( ! isCheck ) 
        {
            window.event.returnValue = false;
            error_alert("初始显示项目设定中请至少选择一个初始显示项目。");
            return false;
        }
    }
    
$(document).ready(function(){
  try {
      window.moveTo(0,0);   
      window.resizeTo(clientScreenWidth-10,clientScreenHeight-10);
      window.resizeTo(clientScreenWidth,clientScreenHeight);
  } 
  catch (e)    
  {
  }
});

