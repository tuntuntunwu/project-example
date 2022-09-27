// JScript File
// ==============================================================================
// File Name           : Min_radiobox.js
// Description         : for the setting screen only.
// Author(s)           : Ji Jianxiong
// Date created        : 2010.09.01
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================

$(document).ready(function(){
    //now, for each select box, run this function
    $(".radio").each(function(){
    	
	    var curbox = $(this);
    	
	    if ( curbox.length > 0 ) {
	        // Get Checkbox
	        var radio = getRadio(curbox[0]);
    	    
            // Get TD
            var td = curbox[0].parentNode;
            // Get TR
            var tr = td.parentNode;
            
            // Add onclick event to cell.
            $(td).click( function(e) {
                radio.click();
                
                check_radio_status()
                
            });
           
	    }
    });
});


function check_radio_status() {
    $(".radio").each(function(){
    	
	    var curbox = $(this);
    	
	    if ( curbox.length > 0 ) {
	        // Get Checkbox
	        var radio = getRadio(curbox[0]);
    	    
            // Get TD
            var td = curbox[0].parentNode;
            // Get TR
            var tr = td.parentNode;

	    }
    });
}

function getRadio(o) {
    var radio = $(o).find("input:radio");
    return radio[0];
}