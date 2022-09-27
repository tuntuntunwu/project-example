﻿// ==============================================================================
// File Name           : Min_checkbox.js
// Description         : checkbox item in SimpleEA's report screen
// Author(s)           : Ji Jianxiong
// Date created        : 2010.11.24
// Last reviewed by    : 
// Last date of review : 
// Copyright           : (c) 2010, SHARP CORPORATION., All rights reserved.
//                       All rights are reserved. Reproduction or transmission in whole or in part, in
//                       any form or by an means, electronic, mechanical or otherwise, is prohibited
//                       without the prior written consent of the copyright owner.

// ==============================================================================

$(document).ready(function(){
// first locate all of the select tags on the page and hide them
$(".Min_check").css('display','none');

//now, for each select box, run this function
    $(".Min_check").each(function(){
    	
	    var curbox = $(this);
    	
	    if ( curbox.length > 0 ) {
	        // Get Checkbox
	        var chk = curbox[0].childNodes[0];
    	    
            // Get TD
            var td = curbox[0].parentNode;
            // While the check box has onclick event, copy it to the td.
            try {
                if ( chk.onclick != null && td.onclick == null) {
                    td.onclick = chk.onclick;
                }
            } catch (e) {
            }

            // Get TR
            var tr = td.parentNode;
	        if ( chk.disabled == false ) {
                if ( chk.checked ) {
                    // Set TD's css -> Checked
                    td.className = "Checkbox_box";
                    
                } else {
                    // Set TR's css -> UnChecked
                    td.className = "Uncheck_box";
                }
                
                // Add onclick event to Row.
                $(td).click( function(e) {
                    if ( chk.checked ) {
                        chk.checked = false;
                    } else {
                        chk.checked = true;
                    }
                    
                    if ( chk.checked ) {
                        // Set TD's css -> Checked
                        td.className = "Checkbox_box";
                    } else {
                        // Set TR's css -> UnChecked
                        td.className = "Uncheck_box";
                    }
                });
	        } else {
                td.className = "";
	        }

            
	    }
    });
});
