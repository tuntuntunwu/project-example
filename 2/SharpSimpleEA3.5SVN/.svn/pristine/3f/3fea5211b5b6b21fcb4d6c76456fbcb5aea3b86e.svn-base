// ==============================================================================
// File Name           : commonreport.js
// Description         : commonreport
// Author(s)           : 
// Date created        : 
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================

  // FROM [XXX JOB REPORT SCREEN] TO [REPORT SCREEN]
  // PARAM'S NAME
  // REPORT TYPE
 var PARAM_REPORT_TYPE = "REPORT_TYPE";
 // START TIME
 var PARAM_START_TIME = "START_TIME";
 // END TIME
 var PARAM_END_TIME = "END_TIME";
 // ID LIST
 var PARAM_ID_LIST = "ID_LIST";
 
 function btnClose_Click() 
 {
     window.close();
 }

 function InitWindow()
 {
     // page's width and height
     var nPageWidth = screen.width - 50;
     var nPageHeight = screen.height - 100;
     
     var nStartWinLeft = (screen.width-nPageWidth)/2;
     var nStartWinTop = (screen.height-nPageHeight)/2;
     try 
     {
        moveTo(nStartWinLeft,nStartWinTop);
        resizeTo(nPageWidth,nPageHeight);
     } 
     catch (e) 
     {
     }
 }
  /********************************************
  * FUNCTION : error_alert
  * SUMMARY  : error_alert
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.08.23
  * VERSION  : 0.01
  *******************************************/
 function error_alert(message , title) 
 {
     var id = "ErrorMessage";
     if ( title )
      {
         document.getElementById("ErrorMessagetitle").innerText = "::Simple EA Application :: " + title;
      }
      else 
      {
         document.getElementById("ErrorMessagetitle").innerText = "::Simple EA Application :: 错误信息确认";
      }
     if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
     {
         // for the group select list item in IE 6.0
         $(".selectlist").each(function() 
         {
             var box = this;
             box.style.display = "none";
             
         });
      }
     document.getElementById("ErrorMessagebody").innerText = message;
     document.getElementById(id).style.display = "inline";
     document.getElementById("Lightbox").style.display = "inline";
     document.getElementById("ErrorMessageOK").focus();
     return false;
 }
 
 /********************************************
  * FUNCTION : ErrorMessage_onkeydown
  * SUMMARY  : ErrorMessage_onkeydown
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.08.23
  * VERSION  : 0.01
  *******************************************/
 function ErrorMessage_onkeydown() 
 {
     var code = event.keyCode;
     if ( code == 9 || code == 39 || code == 37 ) 
     {
         document.getElementById("ErrorMessageOK").focus();
         event.returnValue=false;
         return false;
     }
     
     if ( code == 13 || code == 32 ) 
     {
         document.getElementById("ErrorMessageOK").focus();
         return true;
     }
      else 
     {
         event.returnValue=false;
         return false;
     }
 }

 /********************************************
  * FUNCTION : close_Alert
  * SUMMARY  : close alert message.
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.08.19
  * VERSION  : 0.01
  *******************************************/
 function close_Error() 
 {
     var id = "ErrorMessage";
     document.getElementById(id).style.display = "none";
     document.getElementById("Lightbox").style.display = "none";
     if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
     {
         // for the group select list item in IE 6.0
         $(".selectlist").each(function()
         {
             var box = this;
             box.style.display = "inline";
             
         });
      }
     
     return false;
 }

  function bgLoad()//Background image loading and auto resize
  {
      var detected_os = navigator.userAgent.toLowerCase();
      if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
      {
//          document.write("<div id='page-background' style='position:absolute; top:0; left:0; width:100%; height:100%;'><img src='../Images/BG.jpg' width='100%' height='100%'/></div>");
          document.write(" <div id='content' style='position:relative; z-index:1;padding:10px;'>");

      }
      else 
      {
          document.write("<div id='page-background' style='position:fixed; top:0; left:0; width:100%; height:99%;'><img src='../Images/BG.jpg' width='100%' height='100%'/></div>");
          document.write(" <div id='content' style='position:relative; z-index:1; padding:10px;'>");
          
      }
  }
  function Close_light() //light box: close
  {
      var WhiteBoxbg =  document.getElementById("WhiteBoxbg").style;
      var Lightbox =  document.getElementById("Lightbox").style;
      var ErrorMessage = document.getElementById("ErrorMessage").style;
      if ( WhiteBoxbg.display == "inline" ) {
          WhiteBoxbg.display = "none";
          if ( ErrorMessage.display != "inline" ) {
              Lightbox.display = "none";
          }
      }
      return true;
  }
 function Showbox() //light box: show
 {
     document.getElementById("WhiteBoxbg").style.display = "inline";
     document.getElementById("Lightbox").style.display = "inline";
     return true;
 }
 /********************************************
 * FUNCTION : __doPostBack
 * SUMMARY  : __doPostBack
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.11.24
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

