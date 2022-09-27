// ==============================================================================
// File Name           : Min_res_grid.js
// Description         : GridView item in Restriction set page
// Author(s)           : Ji Jianxiong
// Date created        : 2010.09.16
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
    $(".Res_Grid").each(function(){
    	
	    var curgrid = $(this);

	    if ( curgrid.length > 0 ) {
	        // Get Checkbox
	        var grid = curgrid[0];
    	    
	        // Add Empty Row as border bottom style.
	        if ( grid.rows.length >= 1 ) {
    	        var colCount = grid.rows[0].childNodes.length;
        	    
    	        var rowcount = grid.rows.length;
        	    
    	        var lastline = 0;
        	    
	            for ( var i = rowcount - 1 ; i > lastline ; i-- ) {
	                // Insert row after even row.
    	            var newTr = grid.insertRow(grid.rows.length - i)
    	            var newTd = newTr.insertCell();
    	            newTd.colSpan = colCount;
    	            newTd.className = "HR_Line";
    	            newTd.height = 2;
	            }
	        }
	    }
    });
});
