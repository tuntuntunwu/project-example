// ==============================================================================
// File Name           : Min_grid.js
// Description         : All GridView item in SimpleEA's datatable ( CSS : GridViewCSS)
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


$(document).ready(function(){

//now, for each GridView, run this function
$(".GridViewCSS").each(function(){
	
	var curgrid = $(this);

	if ( curgrid.length > 0 ) {
	    // Get Checkbox
	    var grid = curgrid[0];
	    
	    // Get Head row
	    var title = grid.rows[0].cloneNode(true);
	    
        // Copy Head into HeadTable which table's css is GridViewCSS_H.
        // Get Head Table 
        var head = $(".GridViewCSS_H")[0];
        
        // Date Row
        var rowdate = null;
        if ( grid.rows.length > 1 ) {
            rowdate = grid.rows[1];
            // Redesign the date row
            for ( var k = 0 ; k < title.childNodes.length ; k++ ) {
// 2011.03.23 Update By SES jijianxiong ST
//                if ( rowdate.childNodes[k].width ) {
//                    rowdate.childNodes[k].width = grid.rows[0].childNodes[k].width;
//                }
//                rowdate.childNodes[k].style.width = grid.rows[0].childNodes[k].style.width;
                var rowcell = rowdate.childNodes[k];
                var gridcell = grid.rows[0].childNodes[k];
                $(rowcell).width(gridcell.style.width);
// 2011.03.23 Update By SES jijianxiong ED
            }
        }


        
//        // Save the offsetWidth
//        var Loadwidth = 0;

	    // Redesign the table head's width.
	    var objResiz = function (e) {

// 2010.11.23 Update By SES Jijianxiong Ver.1.1 Update ST
// JavaScript Speed Up
//	        if ( rowdate == null ) {
//	            return;
//	        }
//	        
////	        if ( Loadwidth == rowdate.parentNode.offsetWidth ) {
////	            return;
////	        } else {
////                Loadwidth = rowdate.parentNode.offsetWidth;
////	        }

//// 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ST
//            // while set width, for last item set auto.
////            var lastitem = title.childNodes.length - 1 ;
//            var lastobject = null;
//// 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ED
//	        // Visible Width
//	        // Set Title width and data width.
//	        for ( var j = 0 ; j < title.childNodes.length; j++ ) {
//	            if ( title.childNodes[j].width ) {
//    	            title.childNodes[j].width = rowdate.childNodes[j].offsetWidth;
//	            }
//	            title.childNodes[j].style.width = rowdate.childNodes[j].offsetWidth + "px";
//    	        
//	            // Except undisplay col.
//// 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ST
////	            if (title.childNodes[j].className == "viewDisplay" || title.childNodes[j].style.display == "none" ) {
////	                lastitem = lastitem - 1;
////	            }
//	            if (title.childNodes[j].className != "viewDisplay" && title.childNodes[j].style.display != "none" ) {
//                    lastobject = title.childNodes[j];
//	            }
//// 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ED
//    	        
//	        }
//// 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ST
////	        title.childNodes[lastitem].style.width = "auto";
////            if ( title.childNodes[lastitem].width ) {
////                title.childNodes[lastitem].width = "";
////            }
//	        lastobject.style.width = "auto";
//            if ( lastobject.width ) {
//                lastobject.width = "";
//            }
//// 2010.11.16 Update By SES Jijianxiong Ver 1.1 Update ED

            if ( rowdate == null ) {
                return;
            }
            var _rowdate = rowdate.children;
            var _title = title.children;
            // while set width, for last item set auto.
            var lastobject = null;
// 2011.03.29 Delete By SES jijianxiong ST
//// 2011.03.23 Add By SES jijianxiong ST
//            var lastwidth = 0;
//// 2011.03.23 Add By SES jijianxiong ED
// 2011.03.29 Delete By SES jijianxiong ED
            
	        // Visible Width
	        // Set Title width and data width.
	        for ( var j = 0 ; j < _title.length; j++ ) {
	            var _rowdate_child = _rowdate[j];
	            var _title_child =  _title[j];
	            var _offsetWidth = _rowdate_child.offsetWidth;
	            
// 2011.03.23 Update By SES jijianxiong ST
//	            if ( _title_child.width ) {
//    	            _title_child.width = _offsetWidth;
//	            }
//	            _title_child.style.width = _offsetWidth + "px";
                $(_title_child).width(_offsetWidth);
// 2011.03.23 Update By SES jijianxiong ED
    	        
	            // Except undisplay col.
	            if ( _title_child.className != "viewDisplay" && _title_child.style.display != "none" ) {
                    lastobject = _title_child;
// 2011.03.29 Delete By SES jijianxiong ST
//// 2011.03.23 Add By SES jijianxiong ST
//                    lastwidth = _offsetWidth;
//// 2011.03.23 Add By SES jijianxiong ED
// 2011.03.29 Delete By SES jijianxiong ED

	            }
    	        
	        }
	        
// 2011.03.29 Update By SES jijianxiong ST
//// 2011.03.23 Update By SES jijianxiong ST
////	        lastobject.style.width = "auto";
////            if ( lastobject.width ) {
////                lastobject.width = "";
////            }
//            // Get the Scrollbar width
//            var scrollbar = $(".ScrollbarCSS");
//            if ( scrollbar.length > 0 ) {
//                lastwidth = lastwidth + scrollbar[0].offsetWidth;
//	            $(lastobject).width( lastwidth );
//            } else {
//	            $(lastobject).width( "auto" );
//            }
//// 2011.03.23 Update By SES jijianxiong ED
            $(lastobject).width( "auto" );
// 2011.03.29 Update By SES jijianxiong ED

// 2010.11.23 Update By SES Jijianxiong Ver.1.1 Update ED


        };
        
        objResiz();
        
	    grid.deleteRow(0);


	    // Add Empty Row as border bottom style.
	    if ( grid.rows.length >= 1 ) {
    	    var colCount = grid.rows[0].childNodes.length;
    	    
    	    var rowcount = grid.rows.length;
    	    
    	    var lastline = 0;
    	    
    	    var maxrow = grid.getAttribute("maxrow");
    	    if (typeof(maxrow) == "undefined" || maxrow == null ) {
    	        maxrow = "11";
    	    }

    	    if ( rowcount <= parseInt(maxrow) ) {
    	        lastline = -1;
    	    }
    	    
	        for ( var i = rowcount - 1 ; i > lastline ; i-- ) {
	            // Insert row after even row.
    	        var newTr = grid.insertRow(grid.rows.length - i)
    	        var newTd = newTr.insertCell();
    	        newTd.colSpan = colCount;
    	        newTd.className = "HR_Line";
    	        newTd.height = 2;
	        }
	        
	    }
	    
	    // Add event to GridView resize.
        grid.onresize = objResiz;
        if ( head.childNodes.length == 0 ) {
    	    head.appendChild(title);
        } else {
    	    head.childNodes[0].appendChild(title);
        }

	    
	}
});
// 2011.04.20 Add By SES zhoumiao ST
if(CheckIE()=="7")
{
   grid_resize();
}
// 2011.04.20 Add By SES zhoumiao ED
});
