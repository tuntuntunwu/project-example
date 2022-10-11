// ==============================================================================
// File Name           : common.js
// Description         : common
// Author(s)           : 
// Date created        : 
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================

// Administrator Guide
var AdminGuide = "Simple EA Administrator Guide(V2.0).pdf";
// Normail User Guide
var NormalUserGuide = "Simple EA User Guide(V2.0).pdf";

var msgdisp = false;

String.prototype.Trim = function() { 
    // 2010.11.25 Update By SES Jijianxiong Ver.1.1 Update ST
    // Java Script Speed Up
    // return this.replace(/(^\s*)|(\s*$)/g, ""); 
    var str = this.replace(/^\s+/,""),
        end = str.length - 1,
        ws = /\s/;
    while (ws.test(str.charAt(end))) {
        end--;
    }
    
    return str.slice(0, end + 1);
    // 2010.11.25 Update By SES Jijianxiong Ver.1.1 Update ED
}  
String.prototype.LTrim = function() { return this.replace(/(^\s*)/g, ""); }  
String.prototype.RTrim = function() { return this.replace(/(\s*$)/g, ""); } 

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
// 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ST
//MFP SERIAL NUMBER
var PARAM_SERIAL_NUMBER = "SERIAL_NUMBER";
// 2010.12.13 Add By SES zhoumiao Ver.1.1 Update ED

var onEnterKeyDown = null;
/********************************************
 * FUNCTION : onkeydown
 * SUMMARY  : onKeydown Event
 *            focus in Item , press EnterKey
 *            Move next item.
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.06.18
 * VERSION  : 0.01
 *******************************************/
document.onkeydown=function()
{
    var typ = event.type;
    var ctl = event.srcElement;
    code = event.keyCode;
    var prop = ctl.type;
    if (prop == "file")
    {
        return false;
    }
    if (code == 123)
    {
        window.event.keyCode = 0;            
        return false;
    }
    
      // 2011.03.23 Add By SES Jijianxiong ST
      if ( msgdisp == true ) {
        window.event.keyCode = 0;            
        event.returnvalue=false;
        return false;
      }
      // 2011.03.23 Add By SES Jijianxiong ED

    
    if ( prop == "text" || prop == "checkbox" || prop == "password" || prop == "radio" || prop == "select-one" ) {
        if ( code == 13 ) {
            window.event.keyCode = 9;
//         //2011.3.23 Add By SES zhoumiao Ver.1.1 Update ST
//           var txtSearchName = $(".SearchName")[0];
//           if(!(txtSearchName==null || typeof(txtSearchName) == "undefined"))
//           {     
//              if(!execReg(txtSearchName.value))
//              {
//                  return false;
//              }      
//           }

//          // 2011.3.23 Add By SES zhoumiao Ver.1.1 Update ED
        }
        return true;
    }

    if ((event.altKey)&&
        ((code==37)||   //Alt+right
        (code==39)))    //Alt+left
    {
        event.returnvalue=false;
        return false;
    }
    if ((code == 8) && 
        (prop != "text" && 
            prop != "textarea" && 
            prop != "password") || 
            (code==116)|| //F5
            (event.ctrlKey && code==82))
    {
        event.keyCode=0;
        event.returnvalue=false;
        return false;
    }
    if ((code == 8 || code==116 || (event.ctrlKey && code==82)) && 
        (prop == "text" || 
         prop == "textarea" || 
         prop == "password") && ctl.readOnly)
    {
        event.keyCode=0;
        event.returnvalue=false;
        return false;
    }
    if ((event.ctrlKey)&&(code==78))
    {
        event.returnvalue=false;
        return false;
    }
    if ((event.shiftKey)&&(code==121))
    {
        event.returnvalue=false;
        return false;
    }
    if (window.event.srcElement.tagName == "A" && window.event.shiftKey)
    {
        window.event.returnvalue = false;
        return false;
    }
};

/********************************************
 * FUNCTION : oncontextmenu
 * SUMMARY  : onConTextMenu Event
 *            Can not Use Mouse left Key Except
 *            In text , textarea.
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.06.18
 * VERSION  : 0.01
 *******************************************/
document.oncontextmenu = function()
{ 
    var ctl = event.srcElement;    
    var prop = ctl.type;

    if ( !(ctl.disabled) && (prop == "text" || prop == "textarea")) 
    {
        return true;
    }
    return false; 
};

window.onhelp = function(){return false}; //F1

function popShowPassword() {
    var addr = "";
    addr = "../Password/password.aspx";
    var strOptions = "";
    var dialogWidth = "700";
    var dialogHeight = "400";
    strOptions = "dialogWidth = " + dialogWidth + "px;" + 
        "dialogHeight = " + dialogHeight + "px;";

    window.showModalDialog(addr, null , strOptions);
}

poplock = false;
/********************************************
 * FUNCTION : popWindow
 * SUMMARY  : window.open
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.06.24
 * VERSION  : 0.01
 *******************************************/
function popWindow(addr , strStartTime , strEndTime , strReportType , strIDList ) {

        if ( poplock ){
            return false;
        }
        poplock = true;

        addr = addr + "?" + PARAM_REPORT_TYPE + "=" + strReportType + "&"
                          + PARAM_START_TIME  + "=" + strStartTime + "&"
                          + PARAM_END_TIME    + "=" + strEndTime + "&"
                          + PARAM_ID_LIST     + "=" + strIDList;
                    

        var intwidth=1014;
        var intheight=900;
        
        var strOptions = '';
        if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) {
            strOptions = 'width =' + intwidth + "px" 
                       + ',height ='+ intheight + "px" 
                       + ',left ='+ "0px "
                       + ',top = '+ "0px"
                       + ',resizable = no'
                       + ',menubar=no'
                       + ',toolbar=no'
                       + ',scrollbars=yes';
        } else {
            strOptions = 'width =' + intwidth + "px" 
                       + ',height ='+ intheight + "px" 
                       + ',left ='+ "0px "
                       + ',top = '+ "0px"
                       + ',resizable = no'
                       + ',menubar=no'
                       + ',toolbar=no'
                       + ',scrollbars=yes';
        }
        var pop = window.open(addr,'openWindows',strOptions);

        if ( pop ) {
            pop.focus();
        }
        poplock = false;

        return false;
}

/********************************************
 * FUNCTION : popWindowAvailable
 * SUMMARY  : window.open
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.07.13
 * VERSION  : 0.01
 *******************************************/
function popWindowAvailable(addr , strIDList ) {

        if ( poplock ){
            return false;
        }
        poplock = true;

        addr = addr + "?" + PARAM_ID_LIST     + "=" + strIDList;
                    

        var intwidth=1014;
        var intheight=900;
        

        var strOptions = '';
        if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) {
            strOptions = 'width =' + intwidth + "px" 
                       + ',height ='+ intheight + "px" 
                       + ',left ='+ "0px "
                       + ',top = '+ "0px"
                       + ',resizable = no'
                       + ',menubar=no'
                       + ',toolbar=no'
                       + ',scrollbars=yes';
        } else {
            strOptions = 'width =' + intwidth + "px" 
                       + ',height ='+ intheight + "px" 
                       + ',left ='+ "0px "
                       + ',top = '+ "0px"
                       + ',resizable = no'
                       + ',menubar=no'
                       + ',toolbar=no'
                       + ',scrollbars=yes';
        }

        var pop = window.open(addr,'openWindows',strOptions);
        if ( pop ) {
            pop.focus();
        }
        poplock = false;

        return false;
}
/********************************************
 * FUNCTION : popWindowJob
 * SUMMARY  : window.open
 * AUTHOR   : SES Zhou Miao
 * DATE     : 2010.12.13
 * VERSION  : 1.1
 *******************************************/
function popWindowJob(addr , strStartTime , strEndTime , strReportType , strIDList ,strSerialNumber) {

        if ( poplock ){
            return false;
        }
        poplock = true;
        
       
        addr = addr + "?" + PARAM_REPORT_TYPE + "=" + strReportType + "&"
                          + PARAM_START_TIME  + "=" + strStartTime + "&"
                          + PARAM_END_TIME    + "=" + strEndTime + "&"
                          + PARAM_ID_LIST     + "=" + strIDList +"&"
                          + PARAM_SERIAL_NUMBER + "=" + strSerialNumber;
        

        var intwidth=1014;
        var intheight=900;
        
        var strOptions = '';
        if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) {
            strOptions = 'width =' + intwidth + "px" 
                       + ',height ='+ intheight + "px" 
                       + ',left ='+ "0px "
                       + ',top = '+ "0px"
                       + ',resizable = no'
                       + ',menubar=no'
                       + ',toolbar=no'
                       + ',scrollbars=yes';
        } else {
            strOptions = 'width =' + intwidth + "px" 
                       + ',height ='+ intheight + "px" 
                       + ',left ='+ "0px "
                       + ',top = '+ "0px"
                       + ',resizable = no'
                       + ',menubar=no'
                       + ',toolbar=no'
                       + ',scrollbars=yes';
        }
        var pop = window.open(addr,'openWindows',strOptions);

        if ( pop ) {
            pop.focus();
        }
        poplock = false;

        return false;
}

/********************************************
 * FUNCTION : Trim
 * SUMMARY  : Trim
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.08.18
 * VERSION  : 0.01
 *******************************************/
function Trim( strInput ) {
    
    // 2010.11.25 Update By SES Jijianxiong Ver.1.1 Update ST
    // JavaScript Speed Up
//    return strInput.replace(/^\s+|\s+$/g,"");
    var str = strInput.replace(/^\s+/,""),
        end = str.length - 1,
        ws = /\s/;
    while (ws.test(str.charAt(end))) {
        end--;
    }
    
    return str.slice(0, end + 1);
    // 2010.11.25 Update By SES Jijianxiong Ver.1.1 Update ED

}

function Page_Chang(){ 
	if(document.getElementById("Lightbox").style.display == "inline")
	{
	  // 2011.01.13 Update By SES zhoumiao Ver.1.1 Update ST
	  //document.getElementById("Lightbox").style.width =window.document.body.scrollWidth;       
      document.getElementById("Lightbox").style.width =document.body.clientWidth;

      // 2011.01.13 Update By SES zhoumiao Ver.1.1 Update ED
     document.getElementById("Lightbox").style.Height =window.document.body.scrollHeight;
    }       
        
}

 /********************************************
  * FUNCTION : loadScript
  * SUMMARY  : loadScript
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.11.23
  * VERSION  : 1.10
  *******************************************/
 function loadScript( url , callback ) 
 {
     var script = document.createElement("script");
     // set type
     script.type = "text/javascript";

     // IE
     if ( script.readyState) 
     {
         script.onreadystatechange = function() 
         {
             if ( script.readyState == "loaded" ||  script.readyState == "complete" ) 
             {
                 script.onreadystechange = null;
                 callback();
             } 
         }
     }
      else 
      {
         script.onload = function() 
         {
             callbacke();
         };
     }
     
     // set src
     script.src = url;
     
     document.getElementsByTagName("head")[0].appendChild(script);
 }

/********************************************
 * FUNCTION : loadScript
 * SUMMARY  : loadScript
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.11.23
 * VERSION  : 1.10
 *******************************************/
function loadScript( url ) 
{
    var script = document.createElement("script");
    // set type
    script.type = "text/javascript";

    // set src
    script.src = url;
    
    document.getElementsByTagName("head")[0].appendChild(script);
}
   /********************************************
 * FUNCTION : loadScriptScroller
 * SUMMARY  : load Scroller Bar Setting
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.11.23
 * VERSION  : 1.10
 *******************************************/
function loadScriptScroller( callback ) 
{
    var jsScroller = document.createElement("script");
    var jsScrollbar = document.createElement("script");
    // set type
    jsScroller.type = "text/javascript";
    jsScrollbar.type = "text/javascript";

    // IE
    if ( jsScrollbar.readyState) 
    {
        jsScrollbar.onreadystatechange = function() 
        {
        
            var _readyState = jsScroller.readyState;
            var _bolready = false;
            if ( _readyState != "loaded" &&  _readyState != "complete" )
            {
                return;
            }

            _readyState = jsScrollbar.readyState;
            if ( _readyState == "loaded" ||  _readyState == "complete" ) 
            {
                jsScrollbar.onreadystechange = null;
                jsScroller.onreadystechange = null;
                callback();
            } 
        }

        jsScroller.onreadystatechange = function()
        {
            var _readyState = jsScrollbar.readyState;
            if ( _readyState != "loaded" &&  _readyState != "complete" )
            {
                return;
            }

            _readyState = jsScroller.readyState;
            if ( _readyState == "loaded" ||  _readyState == "complete" ) 
            {
                jsScrollbar.onreadystechange = null;
                jsScroller.onreadystechange = null;
                callback();
            } 
        }

    }
    
    // set src
    jsScroller.src = "../js/jsScroller.js";
    jsScrollbar.src = "../js/jsScrollbar.js";
    
    document.getElementsByTagName("head")[0].appendChild(jsScroller);
    document.getElementsByTagName("head")[0].appendChild(jsScrollbar);
}

        

// <!CDATA[
  // The Size for the IE.( Width )
  var clientScreenWidth = screen.width;
  // The Size for the IE.( Height )
  var clientScreenHeight = screen.height-30;
  
  try {
      window.moveTo(0,0);   
      window.resizeTo(clientScreenWidth,clientScreenHeight);
  } 
  catch (e)    
  {
  }
  
  // confirm message, select true, then do.
  var confirmfunction = null;

  /********************************************
   * FUNCTION : close_Confirm
   * SUMMARY  : close confirm message.
   * AUTHOR   : SES Ji JianXiong
   * DATE     : 2010.08.19
   * VERSION  : 0.01
   *******************************************/
  function close_Confirm(retVal) 
  {
      var id = "ConfirmMessage";
      document.getElementById(id).style.display = "none";
      document.getElementById("Lightbox").style.display = "none";
      if ( retVal ) 
      {
          if ( confirmfunction != null ) 
          {
              confirmfunction();
          }
      } 
      else
       {
          if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
          {
              // for the group select list item in IE 6.0
              $(".selectlist").each(function() 
              {
                  var box = this;
                  box.style.display = "inline";
                  
              });
            // for the group select list item in IE 6.0
            $(".Restrictionie6").css('display','inline');
          }
      }
      // 2011.03.23 Add By SES Jijianxiong ST
      msgdisp = false;
      // 2011.03.23 Add By SES Jijianxiong ED
      return retVal;
  }
  
  
  
  /********************************************
   * FUNCTION : confirm
   * SUMMARY  : open confirm message.
   * AUTHOR   : SES Ji JianXiong
   * DATE     : 2010.08.19
   * VERSION  : 0.01
   *******************************************/
  function confirm(message , title, fun) 
  {
      // While OK , run this function.
      if ( fun ) 
      {
          confirmfunction = fun;
      }
      var id = "ConfirmMessage";
      if ( title )
      {
          document.getElementById("ConfirmMessagetitle").innerText = "::Simple EA Application :: " + title;
      } 
      else 
      {
          document.getElementById("ConfirmMessagetitle").innerText = "::Simple EA Application :: 信息确认";
      }
      // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ST
      if ( document.getElementById("Lightbox").style.display != "inline" )
       {
           document.getElementById("WhiteBoxbg").style.display = "none";
           document.getElementById("LightWhiteboxbg").style.display = "none";
          // 2011.01.13 Delete By SES zhoumiao Ver.1.1 Update ST
           //AdjustDIVSize("WhiteBoxbg" , "LightWhiteboxbg");
           // 2011.01.13 Delete By SES zhoumiao Ver.1.1 Update ED
              
        }
       // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ED
      document.getElementById("ConfirmMessagebody").innerText = message;
      
      if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
      {
          // for the group select list item in IE 6.0
          $(".selectlist").each(function() 
          {
              var box = this;
              box.style.display = "none";
              
          });
          // for the group select list item in IE 6.0
          $(".Restrictionie6").css('display','none');
      }       
      document.getElementById(id).style.display = "inline";
      document.getElementById("Lightbox").style.display = "inline";        
      // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ST
      document.getElementById("Lightbox").style.width =document.body.clientWidth;
      // 2011.01.13 Update By SES zhoumiao Ver.1.1 Update ST
      //document.getElementById("Lightbox").style.Height =document.body.clientHeight;
      document.getElementById("Lightbox").style.Height =window.document.body.scrollHeight;
      // 2011.01.13 Update By SES zhoumiao Ver.1.1 Update ED
     
     
      // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ED
      
      document.getElementById("ConfirmMessageCancel").focus();
      // 2011.03.23 Add By SES Jijianxiong ST
      msgdisp = true;
      // 2011.03.23 Add By SES Jijianxiong ED
      
      return false;
  }
  
  /********************************************
   * FUNCTION : ConfirmMessage_onkeydown
   * SUMMARY  : ConfirmMessage_onkeydown
   * AUTHOR   : SES Ji JianXiong
   * DATE     : 2010.08.20
   * VERSION  : 0.01
   *******************************************/
  function ConfirmMessage_onkeydown() 
  {
      var code = event.keyCode;
      if ( code == 9 || code == 39 || code == 37 ) 
      {
          // change focus with OK button and Cancel button.
          var obj = document.activeElement;
          if ( obj ) 
          {
              if ( obj.id == "ConfirmMessageOK" ) 
              {
                  document.getElementById("ConfirmMessageCancel").focus();
                  event.returnValue=false;
                  return false;
              } 
              else if (  obj.id == "ConfirmMessageCancel" ) 
              {
                  document.getElementById("ConfirmMessageOK").focus();
                  event.returnValue=false;
                  return false;
              }
          }
          
          // Defulat: focus on the Cancel button.
          document.getElementById("ConfirmMessageCancel").focus();
          event.returnValue=false;
          return false;
      }
      
      if ( code == 13 || code == 32 ) 
      {
          // while focus not on the OK button, Cancel button.click.
          var obj = document.activeElement;
          if ( obj ) 
          {
              if ( obj.id != "ConfirmMessageOK" ) 
              {
                  document.getElementById("ConfirmMessageCancel").onclick();
              }
          }
          return true;
      }
       else 
      {
          event.returnValue=false;
          return false;
      }

  }
  
  /********************************************
   * FUNCTION : alert
   * SUMMARY  : alert
   * AUTHOR   : SES Ji JianXiong
   * DATE     : 2010.08.20
   * VERSION  : 0.01
   *******************************************/
  function alert(message , title)
   {
      var id = "AlertMessage";
      if ( title ) 
      {
          document.getElementById("AlertMessagetitle").innerText = "::Simple EA Application :: " + title;
      } 
      else      
      {
          document.getElementById("AlertMessagetitle").innerText = "::Simple EA Application :: 信息确认";
      }
      // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ST
      if ( document.getElementById("Lightbox").style.display != "inline" ) 
      {
           document.getElementById("WhiteBoxbg").style.display = "none";
           document.getElementById("LightWhiteboxbg").style.display = "none";
           // 2011.01.13 Delete By SES zhoumiao Ver.1.1 Update ST
           //AdjustDIVSize("WhiteBoxbg" , "LightWhiteboxbg");
           // 2011.01.13 Delete By SES zhoumiao Ver.1.1 Update ED
      }
       // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ED
      if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
      {
          // for the group select list item in IE 6.0
          $(".selectlist").each(function() 
          {
              var box = this;
              box.style.display = "none";
              
          });
          
      }       
      document.getElementById("AlertMessagebody").innerText = message;
      document.getElementById(id).style.display = "inline";
      document.getElementById("Lightbox").style.display = "inline";
      // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ST
      document.getElementById("Lightbox").style.width =document.body.clientWidth;
      // 2011.01.13 Update By SES zhoumiao Ver.1.1 Update ST
      //document.getElementById("Lightbox").style.Height =document.body.clientHeight;
      document.getElementById("Lightbox").style.Height =window.document.body.scrollHeight;
      // 2011.01.13 Update By SES zhoumiao Ver.1.1 Update ED
      // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ED
      document.getElementById("AlertMessageOK").focus();
      // 2011.03.23 Add By SES Jijianxiong ST
      msgdisp = true;
      // 2011.03.23 Add By SES Jijianxiong ED
      return false;
  }
  
  /********************************************
   * FUNCTION : AlertMessage_onkeydown
   * SUMMARY  : AlertMessage_onkeydown
   * AUTHOR   : SES Ji JianXiong
   * DATE     : 2010.08.20
   * VERSION  : 0.01
   *******************************************/
  function AlertMessage_onkeydown() 
  {
      var code = event.keyCode;
      if ( code == 9 || code == 39 || code == 37 ) 
      {
          document.getElementById("AlertMessageOK").focus();
          event.returnValue=false;
          return false;
      }
      
      if ( code == 13 || code == 32 ) 
      {
          document.getElementById("AlertMessageOK").focus();
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
  function close_Alert() 
  {
      var id = "AlertMessage";
      if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
      {
          // for the group select list item in IE 6.0
          $(".selectlist").each(function()
          {
              var box = this;
              box.style.display = "inline";
              
          });
       }
      document.getElementById(id).style.display = "none";
      document.getElementById("Lightbox").style.display = "none";
      // 2011.03.23 Add By SES Jijianxiong ST
      msgdisp = false;
      // 2011.03.23 Add By SES Jijianxiong ED
      return false;
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
      if ( title ) {
          document.getElementById("ErrorMessagetitle").innerText = "::Simple EA Application :: " + title;
      } 
      else 
      {
          document.getElementById("ErrorMessagetitle").innerText = "::Simple EA Application :: 错误信息确认";
      }
      // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ST
      if ( document.getElementById("Lightbox").style.display != "inline" ) 
      {
           document.getElementById("WhiteBoxbg").style.display = "none";
           document.getElementById("LightWhiteboxbg").style.display = "none";
          // 2011.01.13 Delete By SES zhoumiao Ver.1.1 Update ST
           //AdjustDIVSize("WhiteBoxbg" , "LightWhiteboxbg");
           // 2011.01.13 Delete By SES zhoumiao Ver.1.1 Update ED
           
      }
       // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ED    
      if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1))
      {
          // for the group select list item in IE 6.0
          $(".selectlist").each(function() 
          {
              var box = this;
              box.style.display = "none";
              
          });
          // for the group select list item in IE 6.0
          $(".Restrictionie6").css('display','none');
       }
      document.getElementById("ErrorMessagebody").innerText = message;
      document.getElementById(id).style.display = "inline";
      document.getElementById("Lightbox").style.display = "inline";
      // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ST
      document.getElementById("Lightbox").style.width =document.body.clientWidth;
      // 2011.01.13 Update By SES zhoumiao Ver.1.1 Update ST
      //document.getElementById("Lightbox").style.Height =document.body.clientHeight;
      document.getElementById("Lightbox").style.Height =window.document.body.scrollHeight;
      // 2011.01.13 Update By SES zhoumiao Ver.1.1 Update ED
      // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ED
      document.getElementById("ErrorMessageOK").focus();
      // 2011.03.23 Add By SES Jijianxiong ST
      msgdisp = true;
      // 2011.03.23 Add By SES Jijianxiong ED
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
          // for the group select list item in IE 6.0
          $(".Restrictionie6").css('display','inline');
     }
      // 2011.03.23 Add By SES Jijianxiong ST
      msgdisp = false;
      // 2011.03.23 Add By SES Jijianxiong ED
      
      return false;
  }
  /********************************************
   * FUNCTION : ResDivTitleDispAllControl
   * SUMMARY  : display and hidden All Job DIV
   *               in Restrict Edit Screen.
   * PARAM    : obj   Display/Hidden Button
   * AUTHOR   : SES Ji JianXiong
   * DATE     : 2010.06.11
   * VERSION  : 0.01
   *******************************************/
  function ResDivTitleDispAllControl( obj ) 
  {
      var name;
      var objContent = obj.parentNode;
      var divlist;
      var displayname = "全部表示";
      var hiddenname = "全部隐藏";
      
      var dispcss= ".disp_show";
      var hidcss = ".disp_hid";        
      if ( obj.value == displayname ) 
      {
// 2010.09.16 Update By SES.JiJianXiong ST
//            // display process
//            $(dispcss).each(function(){
//	
//	            var curSel = $(this);
//	            var td = curSel[0];
//	            td.style.display = "block";
//	            });
//            $(hidcss).each(function(){
//	
//	            var curSel = $(this);
//	            var td = curSel[0];
//	            td.style.display = "none";
//	            });

        $(".toggle_container1").show();

        $("h2.trigger").addClass("active"); 

        // 2011.01.05 Add By SES Jijianxiong ST
        // Simulator the slideToggle event
        $("h2.trigger").each(function()
        {
            this.lastToggle = 1;
        });
        // 2011.01.05 Add By SES Jijianxiong ED

// 2010.09.16 Update By SES.JiJianXiong ED
         // Button's Name
         obj.value = hiddenname;
     }
      else if ( obj.value == hiddenname ) 
     {
 // 2010.09.16 Update By SES.JiJianXiong ST
//           // display process
//            $(hidcss).each(function(){
//	
//	            var curSel = $(this);
//	            var td = curSel[0];
//	            td.style.display = "block";
//	            });
//            $(dispcss).each(function(){
//	
//	            var curSel = $(this);
//	            var td = curSel[0];
//	            td.style.display = "none";
//	            });
         $(".toggle_container1").hide();

         $("h2.trigger").removeClass("active");
         // 2011.01.05 Add By SES Jijianxiong ST
         // Simulator the slideToggle event
         $("h2.trigger").each(function()
         {
             this.lastToggle = 2;
         });
         // 2011.01.05 Add By SES Jijianxiong ED
// 2010.09.16 Update By SES.JiJianXiong ED
            // Button's Name
         obj.value = displayname;
     }
     return false;
 }

 /********************************************
  * FUNCTION : ResDivTitleDispControl
  * SUMMARY  : display and hidden Job DIV in
  *               Restrict Edit Screen.
  * PARAM    : obj   DIV
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.06.11
  * VERSION  : 0.01
  *******************************************/
 function ResDivTitleDispControl(obj , dispflg) 
 {

// Update By SES.JiJianXiong 2010.09.16 ST
//        // Get the ParentNode.
//        if ( obj.id == "" ) {
//            var i = 0;
//            var k = 0;
//            for ( i =0 ; i >= 0 ; i= i + 0 ) {
//                obj = obj.parentNode;
//                if ( obj.id != "" & obj.tagName == "TR" ) {
//                    i = -1;
//                }
//                k = k + 1;
//                if ( k == 5 ) {
//                    return false;
//                }
//            }
//        } 
//        var display_id = obj.id;
//        // ParentNode ( table )
//        var objParentNode = obj.parentNode;

//        var div_Antonym ; 

//      
//        if ( objParentNode.childNodes[0].id == display_id ) {
//            div_Antonym = objParentNode.childNodes[1];
//        } else if ( objParentNode.childNodes[1].id == display_id ) {
//            div_Antonym = objParentNode.childNodes[0];
//        }

//        if (typeof(dispflg) == "boolean" && dispflg ) {
//            div_Antonym.style.display = "none";
//            obj.style.display = "block";
//            return;
//        } 

//        if ( div_Antonym ) {
//            div_Antonym.style.display = "block";
//            obj.style.display = "none";
//            return;
//        }

       // Get All toggle_container1
     var trigger = obj;
     for ( var i=0 ; i < 10 ; i++ ) {
          trigger = trigger.parentNode;
          if ( trigger.className == "toggle_container1" ) 
          {
              break;
          }
     }
      if ( trigger.className == "toggle_container1" ) 
      {
          var h2trigger = trigger.parentNode.childNodes[0];
          $(trigger).show();

          $(h2trigger).addClass("active"); 
      }
// Update By SES.JiJianXiong 2010.09.16 ED
 }

 /********************************************
  * FUNCTION : LimitControlEnable
  * SUMMARY  : enable Limit to in
  *               Restrict Edit Screen.
  * PARAM    : obj   obj
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.06.11
  * VERSION  : 0.01
  *******************************************/
 function LimitControlEnable(obj) 
 {
     var objParentNode = obj.parentNode.parentNode.parentNode;
     var objList = objParentNode.getElementsByTagName("input");
     var  txtLimit;
     for ( var i = 0 ; i< objList.length ; i++ )
     {
         if ( objList[i].id.indexOf("txtLimitNum") > 0 ) 
         {
             txtLimit =  objList[i];            
         }
     }
     
     if ( txtLimit ) 
     {
         txtLimit.tabIndex = "0";
         txtLimit.focus();
     }
 }

 /********************************************
  * FUNCTION : LimitControlDisable
  * SUMMARY  : disable Limit to in
  *               Restrict Edit Screen.
  * PARAM    : obj   obj
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.06.11
  * VERSION  : 0.01
  *******************************************/
 function LimitControlDisable(obj) 
 {
     var objParentNode = obj.parentNode.parentNode.parentNode;
     var objList = objParentNode.getElementsByTagName("input");
     var  txtLimit;
     for ( var i = 0 ; i< objList.length ; i++ ) 
     {
         if ( objList[i].id.indexOf("txtLimitNum") > 0 ) 
         {
             txtLimit =  objList[i];            
             break;
         }
     }
     
     if ( txtLimit ) 
     {
         txtLimit.value = "";
         txtLimit.tabIndex = "-1";
         var val_items = txtLimit.Validators;
         for (i = 0; i < val_items.length; i++) 
         {
             ValidatorValidate(val_items[i], null, null);
             ValidatorUpdateDisplay(val_items[i]);
         }
     }
 }

 /********************************************
  * FUNCTION : LimitTxtControlClick
  * SUMMARY  : enable Limit to in
  *               Restrict Edit Screen.
  * PARAM    : obj   obj
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.06.11
  * VERSION  : 0.01
  *******************************************/
 function LimitTxtControlClick(obj) 
 {
// Update By SES.JiJianXiong 2010.09.16 ST
//// Update By SES.JiJianXiong 2010.09.01 ST
////        var objParentNode = obj.parentNode.parentNode.parentNode;
////        var objList = objParentNode.getElementsByTagName("input");
//        var objParentNode = obj.parentNode.parentNode;
//        var objList = $(".Min_radio" , objParentNode);
//// Update By SES.JiJianXiong 2010.09.01 ED

     var objParentNode = obj.parentNode.parentNode;
     var objList = $("input:radio" , objParentNode);

// Update By SES.JiJianXiong 2010.09.16 ED
     var  rdoLimit;
     for ( var i = 0 ; i< objList.length ; i++ ) 
     {
// Update By SES.JiJianXiong 2010.09.16 ST
//// Update By SES.JiJianXiong 2010.09.01 ST
////           if ( objList[i].id.indexOf("rdoLimite") > 0 ) {
////                rdoLimit =  objList[i];            
////            }
//            if ( objList[i].childNodes[0].value.indexOf("rdoLimite") > -1 ) {
//                rdoLimit =  objList[i].childNodes[0];
//                break;
//            }
//        }
//// Update By SES.JiJianXiong 2010.09.01 ED
        if ( objList[i].value.indexOf("rdoLimite") > -1 )
         {
            rdoLimit =  objList[i];
            break;
         }
     }
// Update By SES.JiJianXiong 2010.09.16 ED
        
     obj.readOnly = ""
     if ( rdoLimit ) 
     {
// Update By SES.JiJianXiong 2010.09.16 ST
//            rdoLimit.checked = true;
        rdoLimit.click();
// Update By SES.JiJianXiong 2010.09.16 ED
        obj.tabIndex = "0";
     }
     
     check_radio_status(objParentNode);
     return false;
}
    
function LimitTxtKeyDown(obj) 
{
    var objParentNode = obj.parentNode.parentNode.parentNode;
    var objList = objParentNode.getElementsByTagName("input");
    var  rdoLimit;
    for ( var i = 0 ; i< objList.length ; i++ ) 
    {
        if ( objList[i].id.indexOf("rdoLimite") > 0 ) 
        {
            rdoLimit =  objList[i];    
        }        
    }
    
    var code = event.keyCode;
    if ( code == 40 ) 
    {
        //↓key 40
        rdoLimit.focus();
    }
    else if ( code == 38 ) 
    {
        // ↑key 38
        rdoLimit.focus();
    }
    return true;
}


 /********************************************
  * FUNCTION : CheckLimitNum
  * SUMMARY  : Must Be Numberic
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.06.11
  * VERSION  : 0.01
  *******************************************/
 function CheckLimitNum(sender, args) 
 {
     var checkItem = args.Value.Trim();
     // Get ControlToValidate Item.
     var controltovalidate = sender.controltovalidate;
     var objInput = document.getElementById(controltovalidate);
    
// Update By SES.JiJianXiong 2010.09.16 ST
//// Update By SES.JiJianXiong 2010.09.01 ST
////        var objParentNode = objInput.parentNode.parentNode.parentNode;
////        var objList = objParentNode.getElementsByTagName("input");
//        var objParentNode = objInput.parentNode.parentNode;
//        var objList = $(".Min_radio" , objParentNode);
//// Update By SES.JiJianXiong 2010.09.01 ED

    var objParentNode = objInput.parentNode.parentNode;
    var objList = $("input:radio" , objParentNode);

// Update By SES.JiJianXiong 2010.09.16 ED
        
     var  rdoLimit;
     for ( var i = 0 ; i< objList.length ; i++ ) 
     {
// Update By SES.JiJianXiong 2010.09.16 ST
//// Update By SES.JiJianXiong 2010.09.01 ST
////            if ( objList[i].id.indexOf("rdoLimite") > 0 ) {
////                rdoLimit =  objList[i];
////                break;
////            }
//            if ( objList[i].childNodes[0].value.indexOf("rdoLimite") > -1 ) {
//                rdoLimit =  objList[i].childNodes[0];
//                break;
//            }
//// Update By SES.JiJianXiong 2010.09.01 ED
         if ( objList[i].value.indexOf("rdoLimite") > -1 ) 
         {
             rdoLimit =  objList[i];
             break;
         }
// Update By SES.JiJianXiong 2010.09.16 ED
     }
       
     if ( rdoLimit.checked == true ) 
     {
         if (checkItem != "" && /^\d+$/.test(checkItem)) 
         {
             var num = parseFloat( checkItem );
             if ( num  >= 1 && num <= 9999 ) 
             {
                 args.IsValid = true;
                 return true;
             }
         }
     }
     else 
     {
         objInput.value = "";
         args.IsValid = true;
         return true;
     }
        
// Update By SES.JiJianXiong 2010.09.16 ST
//        // Add By SES.JiJianXiong 2010.09.01 ST
//        // while the Validation is false.
//        // Display the row.
//        // disp_show
//        var loop = 0;
//        for ( var i=0 ; i < 12 ; i=i+1 ) {
//            objParentNode = objParentNode.parentNode;
//            if ( objParentNode == null ) {
//                break;
//            }
//            // the show item.
//            if ( objParentNode.innerHTML.indexOf( "__show_row" ) > -1 ) {
//                break;
//            }
//        }
//        
//        if ( objParentNode.innerHTML.indexOf( "__show_row" ) > -1 ) {
//            ResDivTitleDispControl(objParentNode , true);
//        }
//        // Add By SES.JiJianXiong 2010.09.01 ED
     ResDivTitleDispControl(objParentNode , true);
// Update By SES.JiJianXiong 2010.09.16 ED
     args.IsValid = false;
     return false;
 }
    
 /********************************************
  * FUNCTION : SetFocusInFirstItemTxt
  * SUMMARY  : Set Focus In First textbox
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.06.18
  * VERSION  : 0.01
  *******************************************/
 function SetFocusInFirstItemTxt() 
 {
     var objList = document.getElementsByTagName("input");
     for ( var i = 0 ; i< objList.length ; i++ ) 
     {
         if ( objList[i].type == "text" ||  objList[i].type == "textarea" || objList[i].type == "password"  ) 
         {
             if ( objList[i].disabled != true ) 
             {
                 objList[i].focus();
                 return false;
             } 
         }
     }

     return false;
 }

 /********************************************
  * FUNCTION : SetFocusInFirstItemBtn
  * SUMMARY  : Set Focus In First Button
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.06.18
  * VERSION  : 0.01
  *******************************************/
 function SetFocusInFirstItemBtn() 
 {
     var objList = document.getElementsByTagName("input");
     for ( var i = 0 ; i< objList.length ; i++ ) 
     {
         if ( objList[i].className == "btnSelectAll" ) 
         {
             objList[i].focus();
             return false;
         }
     }

     return false;
 }
 
 /********************************************
  * FUNCTION : openPDFWindow
  * SUMMARY  : Download User Guide PDF
  * AUTHOR   : SES Ji JianXiong
  * DATE     : 2010.08.05
  * VERSION  : 0.01
  *******************************************/
 function openPDFWindow(strPDF ) 
 {
     // 2011.1.25 Update By SES Zhoumiao Ver.1.1 Update ST
//     window.open("../Img/Simple EA User's Guide(V1.10).pdf",'','');
//     if ( event ) 
//     {
//         event.returnValue = false;
//     }
//     return false;
    if ( poplock )
     {
            return false;
     }
      poplock = true;    
     // 2011.1.26 Add By SES Zhoumiao Ver.1.1 Update ST
     if(strPDF =="Admin")
     {
        // strPDF="../Img/Simple EA Administrator Guide(V1.10).pdf";
        strPDF = "../Img/" + AdminGuide;
     }
     else
     {
        // strPDF="../Img/Simple EA User's Guide(V1.10).pdf";
        strPDF = "../Img/" + NormalUserGuide;
     }
     // 2010.1.26 Add By SES Zhoumiao Ver.1.1 Update ED
     var pop=window.open(strPDF,'SimpleEAGuide','');
// Delete By SES Jijianxiong ST     
//     if ( pop ) {
//            pop.focus();
//        }
// Delete By SES Jijianxiong ED
        poplock = false;

        return false;
     // 2010.1.25 Update By SES Zhoumiao Ver.1.1 Update ED
 }
 
/********************************************
 * FUNCTION : AdjustDIVSize
 * SUMMARY  : Adjust the Disp Size.
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.08.05
 * VERSION  : 0.01
 *******************************************/
function AdjustDIVSize(msgID,shieldID)
{
	var MsgDIV = window.document.getElementById(msgID);
	var ShieldDIV = window.document.getElementById(shieldID);
	if(MsgDIV != null)
	{
		
		MsgDIV.style.left = (document.body.clientWidth - MsgDIV.offsetWidth)/2;
		MsgDIV.style.top = (document.body.clientHeight - MsgDIV.offsetHeight)/2;
	}
	
	if(ShieldDIV != null)
	{
//		    var ndivWidth = screen.availWidth;
//		    if ( document.body.clientWidth > ndivWidth ) {
//		        ndivWidth = document.body.clientWidth;
//		    }
		    
		var ndivHeight = screen.availHeight;
		if ( window.document.body.scrollHeight > ndivHeight ) 
		{
		    ndivHeight =  window.document.body.scrollHeight;
		}
		
//		ShieldDIV.style.width = ndivWidth;
		ShieldDIV.style.height = ndivHeight;
	}			
}
// ]]>


function bgLoad()//Background image loading and auto resize
{
    var detected_os = navigator.userAgent.toLowerCase();
    if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
    {
//          document.write("<div id='page-background' style='position:absolute; top:0; left:0; width:100%; height:100%;'><img src='../Images/BG.jpg' width='100%' height='100%'/></div>");
       document.write(" <div id='content' style='padding:10px;'>");

    }
   else 
   {
       document.write("<div id='page-background' style='position:fixed; top:0; left:0; width:100%; height:100%;'><img src='../Images/BG.jpg' width='100%' height='100%'/></div>");
       document.write(" <div id='content' style='position:relative; z-index:1; padding:10px;'>");
   }
}        
   
function Close_light() //light box: close
{
    if ( document.getElementById("WhiteBoxbg").style.display == "inline" ) 
    {
        document.getElementById("WhiteBoxbg").style.display = "none";
        document.getElementById("LightWhiteboxbg").style.display = "none";

        if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
        {
            // for the group select list item in IE 6.0
            $(".selectlist").each(function() 
            {
                var box = this;
                box.style.display = "inline";
                
            });
              // for the group select list item in IE 6.0
              $(".Restrictionie6").css('display','inline');
         }
        return true;
    }
}

var flgbtnCSV = false;
function Showbox() //light box: show
{
  
   if ( flgbtnCSV )
   {
        flgbtnCSV = false;
        return true;
   }   
   if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
   {
        // for the group select list item in IE 6.0
        $(".selectlist").each(function() 
        {
            var box = this;
            box.style.display = "none";
            
        });
        // for the group select list item in IE 6.0
        $(".Restrictionie6").css('display','none');
    }
    if ( document.getElementById("LightWhiteboxbg").style.display != "inline" ) 
    {
        // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ST
        document.getElementById("Lightbox").style.display = "none";
        // 2011.01.06 Add By SES zhoumiao Ver.1.1 Update ED
        document.getElementById("WhiteBoxbg").style.display = "inline";
        document.getElementById("LightWhiteboxbg").style.display = "inline";
        AdjustDIVSize("WhiteBoxbg" , "LightWhiteboxbg");
    }
    return true;
}

  //2011.3.23 Add By SES zhoumiao Ver.1.1 Update ST
    //check Search CompareValidator
 /********************************************
 * FUNCTION : execReg
 * SUMMARY  : execReg
 *
 * AUTHOR   : SES zhoumiao
 * DATE     : 2011.03.23
 * VERSION  : 1.1
 *******************************************/
    function  execReg(str){
	
	if ( str != "" ) {
		var rx = new RegExp("[^,<>&/|]+");
		var matches = rx.exec(str);
		if (!( matches != null && str == matches[0] )) 
		{
		   window.event.returnValue = false;
		    //2011.3.24 Delete By SES zhoumiao Ver.1.1 Update ST
			//error_alert("不能包含特殊字符(<,>,&,/,|)以及逗号。");
			 //2011.3.24 Delete By SES zhoumiao Ver.1.1 Update ED
			return false;
		
		}	
	}
	return true;
	
}
 /********************************************
 * FUNCTION : SearchOnClientClick
 * SUMMARY  : SearchOnClientClick
 *
 * AUTHOR   : SES zhoumiao
 * DATE     : 2011.03.23
 * VERSION  : 1.1
 *******************************************/
function SearchOnClientClick() {

    var txtSearchName = $(".SearchName")[0];

     if(!(txtSearchName==null || typeof(txtSearchName) == "undefined"))
     {    
      if(!execReg(txtSearchName.value))
      {
        //2011.3.24 Add By SES zhoumiao Ver.1.1 Update ST
		error_alert("不能包含特殊字符(<,>,&,/,|)以及逗号。");
	   //2011.3.24 Add By SES zhoumiao Ver.1.1 Update ED
         return false;
      }      
     }
       
      return true;
    
}

 /********************************************
 * FUNCTION : LogSearchOnClientClick
 * SUMMARY  : LogSearchOnClientClick
 *
 * AUTHOR   : SES zhoumiao
 * DATE     : 2011.03.23
 * VERSION  : 1.1
 *******************************************/
function LogSearchOnClientClick() {

     var txtLogSearchName = $(".LogSearchName")[0];
  
       if(!(txtLogSearchName==null || typeof(txtLogSearchName) == "undefined"))
       {
      
      if(!execReg(txtLogSearchName.value))
      {
        //2011.3.24 Add By SES zhoumiao Ver.1.1 Update ST
		error_alert("[用户名/登录名]中不能输入\r\n包含特殊字符(<,>,&,/,|)以及逗号。");
	   //2011.3.24 Add By SES zhoumiao Ver.1.1 Update ED
         return false;
      }      
      }
      return true;
    
}

    
      //2011.3.29 Add By SES zhoumiao Ver.1.1 Update ST
    // Check IE 
 /********************************************
 * FUNCTION : CheckIE
 * SUMMARY  : CheckIE
 *
 * AUTHOR   : SES zhoumiao
 * DATE     : 2011.03.23
 * VERSION  : 1.1
 *******************************************/
    function  CheckIE(){
     //when IE is under of 6
     if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
     {
        return "6";
     }
     //when IE is 7
     else if(navigator.appVersion.indexOf('MSIE 7.0') != -1) 
     {
        return "7";
     }
     //when IE is 8
     else if(navigator.appVersion.indexOf('MSIE 8.0') != -1) 
     {
       return "8";
     }
    
	
	
	
}
// 2011.3.29 Add By SES zhoumiao Ver.1.1 Update E</>D


/********************************************
* FUNCTION : CheckListItem
* SUMMARY  : CheckListItem
*
* AUTHOR   : SES zhengwei
* DATE     : 2012.02.06
* VERSION  : 1.2
*******************************************/
function CheckListItem(o) {

    var selectValue = o.value;

    // Drop Down List: While Selected Value = "":ALL, Display the Confirm Message

    if (selectValue == "") {

        // Drop Down List: While Value changed, call the doPostBack Event

        var fun = function (e) {

            setTimeout('__doPostBack(\'' + o.id + '\',\'\')', 0);

        };



        if (confirm("可能会花费较多时间,是否继续?", null, fun)) {

            return true;

        }

        return false;

    }



    // Drop Down List: While Selected Value != "":ALL, call the doPostBack Event.

    setTimeout('__doPostBack(\'' + o.id + '\',\'\')', 0)

    return true;



}
