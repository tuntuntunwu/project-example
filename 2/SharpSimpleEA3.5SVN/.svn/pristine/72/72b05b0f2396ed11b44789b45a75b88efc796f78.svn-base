// ==============================================================================
// File Name           : Min_checkbox.js
// Description         : All checkbox item in SimpleEA's datatable
// Author(s)           : Ji Jianxiong
// Date created        : 2010.08.18
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
            // 2010.11.18 Update By SES Jijianxiong Ver.1.1 Update ST
            // While the check box has onclick event, copy it to the td.
            try {
                if ( chk.onclick != null && td.onclick == null) {
                    td.onclick = chk.onclick;
                }
            } catch (e) {
            }
            // 2010.11.18 Update By SES Jijianxiong Ver.1.1 Update ED
            // Get TR
            var tr = td.parentNode;
	        if ( chk.disabled == false ) {
                if ( chk.checked ) {
                    // Set TD's css -> Checked
                    td.className = "Checkbox_box";
                    tr.className = "SelectedTR";
                } else {
                    // Set TR's css -> UnChecked
                    td.className = "Uncheck_box";
                    tr.className = "UnselectedTR";
                }
                
                // Add onclick event to Row.
                $(tr).click( function(e) {
                    if ( chk.checked ) {
                        chk.checked = false;
                    } else {
                        chk.checked = true;
                    }
                    
                    if ( chk.checked ) {
                        // Set TD's css -> Checked
                        td.className = "Checkbox_box";
                        tr.className = "SelectedTR";
                    } else {
                        // Set TR's css -> UnChecked
                        td.className = "Uncheck_box";
                        tr.className = "UnselectedTR";
                    }
                });
	        } else {
                td.className = "";
                tr.className = "UnselectedTR";
	        }

            
	    }
    });
});

function btnSelectAll_Click(obj) {
    var name = obj.value;
    var chkvalue = true;
    
    // checkbox's status.
    if ( name == "全部选中" ) {
        chkvalue = true;
        obj.value = "解除全选";
    } else {
        chkvalue = false;
        obj.value = "全部选中";
    }
    
    check_status(chkvalue);
    
    if ( event ) {
        event.returnValue = false;
    }
    return false;
}

function check_status(obj) {
    $(".Min_check").each(function(){
    	
	    var curbox = $(this);
    	
	    if ( curbox.length > 0 ) {
	        // Get Checkbox
	        var chk = curbox[0].childNodes[0];
    	    
            // Get TD
            var td = curbox[0].parentNode;
            // Get TR
            var tr = td.parentNode;
	        if ( chk.disabled == false ) {
                chk.checked = obj;
                if ( chk.checked ) {
                    // Set TD's css -> Checked
                    td.className = "Checkbox_box";
                    tr.className = "SelectedTR";
                } else {
                    // Set TR's css -> UnChecked
                    td.className = "Uncheck_box";
                    tr.className = "UnselectedTR";
                }
                
	        } else {
                td.className = "";
                tr.className = "UnselectedTR";
	        }

            
	    }
    });
}