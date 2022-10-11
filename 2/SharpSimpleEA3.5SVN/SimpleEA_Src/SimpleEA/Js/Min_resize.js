// ==============================================================================
// File Name           : Min_resize.js
// Description         : For GridView with it's scrollbar, adjuest width for display.
// Author(s)           : Ji Jianxiong
// Date created        : 2010.08.18
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.
//
// ==============================================================================

/********************************************
 * FUNCTION : grid_resize
 * SUMMARY  : resize function.
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.08.24
 * VERSION  : 0.01
 *******************************************/
function grid_resize() {

// 2010.11.23 Update By SES Jijianxiong Ver.1.1 Update ST
// Speed Up
//    $(".Container").each(function(){
//	    var curCt = $(this)[0];
//	    var parent = curCt.parentNode;
//        curCt.width = parent.offsetWidth + 33;
//        curCt.style.width = (parent.offsetWidth + 33) + "px";
//        curCt.childNodes[0].width = parent.offsetWidth;
//        curCt.childNodes[0].style.width = (parent.offsetWidth) + "px";
//    });

//    var Scroll_TD1 = $(".Scroll_div");
//    if ( Scroll_TD1.length == 0 ) { 
//        return false;
//    }
//    var ScrollbarCSS = $(".ScrollbarCSS");
//    if ( ScrollbarCSS.length == 0 ) {
//        return false;
//    }
//    
//    var id = Scroll_TD1.length;
//    
//    for ( var id = 0 ; id < Scroll_TD1.length; id++ ) {
//        // Date
//        var objdiv = Scroll_TD1[id];
//        var objtd = $(".Scroll_td")[id];
//        // tabel width with the scrollbar.
//        var divwidth = objdiv.offsetWidth;
//        var tdwidth = objtd.offsetWidth;

//        //parentNode.
//        var objTable = objtd.parentNode;
//        // Scrollbar
//        var objScrollbarCSS = ScrollbarCSS[id];
//        var scrollwidth = objScrollbarCSS.offsetWidth;
//        
//        if ( scrollwidth < 34 ) {
//            // resize the td
//            objtd.width = objTable.offsetWidth - 33;
//            objtd.style.width = (objTable.offsetWidth - 33) + "px";
//            tdwidth = objTable.offsetWidth - 33;
//            
//            objScrollbarCSS.width = 33;
//            objScrollbarCSS.style.width = "33px";
//        }
//        

//        // while the data td width is floowed
//        if ( divwidth != tdwidth ) {
//            objdiv.width = tdwidth;
//            objdiv.style.width = tdwidth + "px";
//        } 
//    
//    }

    var _Container = $(".Container");
    if ( _Container.length == 0 ) {
        return;
    }
    var curCt = _Container[0];
    var parent = curCt.parentNode;
    var _parent_offsetWidth = parent.offsetWidth;
    var curCt_child = curCt.childNodes[0];
    
    curCt.width = _parent_offsetWidth + 33;
    curCt.style.width = (_parent_offsetWidth + 33) + "px";
    curCt_child.width =_parent_offsetWidth;
    curCt_child.style.width = (_parent_offsetWidth) + "px";

    var Scroll_div = $(".Scroll_div");
    if ( Scroll_div.length == 0 ) { 
        return false;
    }
    
    var ScrollbarCSS = $(".ScrollbarCSS");
    if ( ScrollbarCSS.length == 0 ) {
        return false;
    }
    
    var Scroll_td = $(".Scroll_td");
    if ( Scroll_td.length == 0 ) {
        return false;
    }
    
    
    var _length = Scroll_div.length;
    
    for ( var id = 0 ; id < _length; id++ ) {
        // Date
        var objdiv = Scroll_div[id];
        var objtd = Scroll_td[id];
        // Scrollbar
        var objScrollbarCSS = ScrollbarCSS[id];

        // tabel width with the scrollbar.
        var divwidth = objdiv.offsetWidth;
        var tdwidth = objtd.offsetWidth;

        //parentNode.
        var objTable = objtd.parentNode;
        var scrollwidth = objScrollbarCSS.offsetWidth;
       // 2010.12.30 Delete By SES Zhoumiao Ver.1.1 Update ST 
      //  if ( scrollwidth < 34 ) {
      // 2010.12.30 Delete By SES Zhoumiao Ver.1.1 Update ED
      // 2010..12.30 Update By SES Zhoumiao Ver.1.1 Update ST 
          if ( scrollwidth != 34 ) {
      // 2010..12.30 Update By SES Zhoumiao Ver.1.1 Update ED
      
            // resize the td
            var _objTable_offsetWidth = objTable.offsetWidth;
            tdwidth = _objTable_offsetWidth - 33;
            objtd.width = tdwidth;
            objtd.style.width = tdwidth + "px";
            
            objScrollbarCSS.width = 33;
            objScrollbarCSS.style.width = "33px";
     
       }
    

        // while the data td width is floowed
        if ( divwidth != tdwidth ) {
            objdiv.width = tdwidth;
            objdiv.style.width = tdwidth + "px";
        } 
    
    }

// 2010.11.23 Update By SES Jijianxiong Ver.1.1 Update ED
}

// do while page load. and size reize
$(document).ready(function(){
    grid_resize();
});


if ( window.attachEvent)  window.attachEvent("onresize",function() {grid_resize();});
else window.addEventListener("onresize",function() {grid_resize();},true);

