// ==============================================================================
// File Name           : commonlogin.js
// Description         : commonlogin
// Author(s)           : 
// Date created        : 
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================

  // page's width and height
        var nPageWidth = screen.width - 20;
        var nPageHeight = screen.height - 60;
        if ( screen.width <= 1024 ) {
            nPageWidth = screen.width - 10;
            nPageHeight = screen.height - 30;
        }

   function InitLoginWindow()
  {
            
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
  function AdjustHeight() // Dyamic height
  {
       var heightFIND = screen.height;
       var val = 1024;
       document.getElementById("txtLoginID").focus();
       imageObject = document.getElementById('HeightAdjustImage');
       
       if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
       {
           var bodyHeight = document.getElementById("tabBody").offsetHeight;
           if ( document.body.offsetHeight < bodyHeight) 
           {
               document.body.style.height = ( bodyHeight + 15 ) + "px";
           }
       }
   }

   function Login_ErrorShow()//Showing Error Meesgae
   {
       document.getElementById("Error_meesageID").style.display = "inline"; // showing error message box
       setTimeout("hidediv('Error_meesageID')", 4000); // Time for closeing error message
   } 

   function hidediv(arg) 
   {
       document.getElementById(arg).style.display = 'none'; // Hidden error message after 4 sec
   }
   function bgLoad() {

       var detected_os = navigator.userAgent.toLowerCase();
       if ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1)) 
       {
          // document.write("<div id='page-background' style='position:absolute; top:0; left:0; width:100%; height:100%;'><img src='../Images/BG.jpg' width='100%' height='100%'></div>");
            document.write(" <div id='content' style='padding:10px;'>");

       }
       else 
       {
           document.write("<div id='page-background' style='position:fixed; top:0; left:0; width:100%; height:100%;'><img src='../Images/BG.jpg' width='100%' height='100%'></div>");
           document.write(" <div id='content' style='position:relative; z-index:1; padding:10px;'>");

       }
   }
		
   /********************************************
    * FUNCTION : SetFocusInFirstItemTxt
    * SUMMARY  : Set Focus In First textbox
    * AUTHOR   : SES Ji JianXiong
    * DATE     : 2010.06.18
    * VERSION  : 0.01
    *******************************************/
   function SetFocusInFirstItemTxt() {
       var objList = document.getElementsByTagName("input");
       for ( var i = 0 ; i< objList.length ; i++ ) {
           if ( objList[i].type == "text" ||  objList[i].type == "textarea"  ) {
               objList[i].focus();
               return false;
           }
       }

       return false;
   }
        
   /********************************************
    * FUNCTION : Login
    * SUMMARY  : Set Focus In First textbox
    * AUTHOR   : SES Ji JianXiong
    * DATE     : 2010.08.18
    * VERSION  : 0.01
    *******************************************/
   function Login() 
   {
       var returnVal = true;
       var txtLoginID = document.getElementById("txtLoginID");
       var txtPassword = document.getElementById("txtPassword");
       var objfocus = null;

       if ( Trim( txtLoginID.value ) == "" ) 
       {
           document.getElementById("TR_txtLoginID").className = "Req_Filed_red";
           
           if ( objfocus == null ) objfocus = txtLoginID;
           returnVal = false;
       } else {
           document.getElementById("TR_txtLoginID").className = "";
       }
       
       if ( Trim( txtPassword.value ) == "" )
        {
           document.getElementById("TR_txtPassword").className = "Req_Filed_red";
           if ( objfocus == null ) objfocus = txtPassword;
           returnVal = false;
       } else {
           document.getElementById("TR_txtPassword").className = "";
       }

       if ( objfocus != null ) {
            setTimeout("document.getElementById('" + objfocus.id + "').focus()" , 1 );
       }

// 2011.01.26 Update By SES Jijianxiong ST
       if (! ((navigator.appVersion.indexOf('MSIE 6.0') != -1) || (navigator.appVersion.indexOf('MSIE 5.5') != -1))) 
       {
           window.event.returnValue = returnVal;
       } else {
           if ( returnVal == false && typeof( window.event ) != "undefined" ) {
                window.event.returnValue = returnVal;
           }
       }
// 2011.01.26 Update By SES Jijianxiong ED

       return returnVal;
       
   }
        
   function Login_In()
    {
        if (event.keyCode == 13) 
        {
            document.getElementById("btnLogin").click();
        } //end if
   }
        
