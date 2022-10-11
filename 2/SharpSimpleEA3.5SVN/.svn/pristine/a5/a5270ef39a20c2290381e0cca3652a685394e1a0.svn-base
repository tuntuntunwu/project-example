// ==============================================================================
// File Name           : Min_drop.js
// Description         : Min_drop
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
 * FUNCTION : Min_drop.js
 * HISTROY  : 
 *  (1).2010.08.18
 *  SES Ji JianXiong Add:
 *        onchange event
 *  (2).2010.08.24
 *  SES Ji JianXiong Add:
 *      Enter Key -> Tab Key
 *  (3).2010.08.25
 *  SES Ji JianXiong Add:
 *      For the list copy the first item.
 *  (4).2010.08.25
 *  SES Ji JianXiong Add:
 *      For disabled attribite.
 *******************************************/
$(document).ready(function(){
// first locate all of the select tags on the page and hide them
$("select.changeMe").css('display','none');
//now, for each select box, run this function
$("select.changeMe").each(function(){
	
	var curSel = $(this);
	
	// Add By SES.JiJianxiong 2010.08.25 ST
	// Get the disable attribites
	var disable = curSel[0].disabled;
	// Add By SES.JiJianxiong 2010.08.25 ED
	
	// get the CSS width from the original select box
	var gddWidth = $(curSel).css('width');
	var gddWidthL = gddWidth.slice(0,-2);
	var gddWidth2 = gddWidthL - 28;
	var gddWidth3 = gddWidthL - 16;

	
	// build the new div structure
	var gddTop = '<div style="width:' + gddWidthL + 'px" class="selectME" tabindex="0"><div class="cornerstop"><div><div></div></div></div><div class="middle"><div><div><div>';

	//get the default selected option
	var whatSelected = $(curSel).children('option:selected').text();
	// Add By SES JiJianxiong 2010.08.24 ST
	if ( whatSelected == "" ) {
	    whatSelected = "£¨Î´Ñ¡Ôñ£©";
	}
	// Add By SES JiJianxiong 2010.08.24 ED
	//write the default
	var gddFirst = '<div class="first"><span class="selectME gselected" style="width:'+ gddWidth2 +  'px;">'+ whatSelected +'</span><span id="arrowImg"></span><div class="clears"></div></div><ul class="selectME">';
	// create a new array of div options from the original's options
	var addItems = new Array();      
	$(curSel).children('option').each( function() {           
		var text = $(this).text();  
		var selVal = $(this).attr('value'); 
		var before =  '<li style="width:' + gddWidthL + 'px;"><a href="#" rel="' + selVal + '" tabindex="0"  style="width:' + gddWidth3 + 'px;">';
		var after = '</a></li>';           
	    // Add By SES JiJianxiong 2010.08.24 ST
	    if ( text == "" ) {
	        text = "£¨Î´Ñ¡Ôñ£©";
	    }
	    // Add By SES JiJianxiong 2010.08.24 ED
		addItems.push(before + text + after);
		
	    // Add By SES JiJianxiong 2010.08.25 ST
	    // for the list copy the first item.
		if ( addItems.length == 1 ) {
    		addItems.push(before + text + after);
		}
	    // Add By SES JiJianxiong 2010.08.25 ED
	});
	//hide the default from the list of options 
	var removeFirst = addItems.shift();
	// create the end of the div selectbox and close everything off
	var gddBottom ='</ul></div></div></div></div><div class="cornersbottom"><div><div></div></div></div></div>'
	//write everything after each selectbox
	var GDD = gddTop + gddFirst + addItems.join('') + gddBottom;
	
	$(curSel).after(GDD);
	
	//this var selects the div select box directly after each of the origials
	var nGDD = $(curSel).next('div.selectME');
	
	$(nGDD).find('li:first').addClass("first");
	
	$(nGDD).find('li:last').addClass('last');
	//handle the on click functions - push results back to old text box
	$(nGDD).click( function(e) {
	    	// Add By SES.JiJianxiong 2010.08.25 ST
            // while it's disabled.
            if ( disable == true ) {
                return;
            }
            // Add By SES.JiJianxiong 2010.08.25 ED

		 var myTarA = $(e.target).attr('rel');
		 var myTarT = $(e.target).text();
		 var myTar = $(e.target);
		 //if closed, then open
		 if( $(nGDD).find('li').css('display') == 'none')
			{
					//this next line closes any other selectboxes that might be open
					$('div.selectME').find('li').css('display','none');
					$(nGDD).find('li').css('display','block');
					
					//if user clicks off of the div select box, then shut the whole thing down
					$(document.window || 'body').click( function(f) {
							var myTar2 = $(f.target);
							if (myTar2 !== nGDD) {$(nGDD).find('li').css('display','none');}
					});
							return false;
			}
			else
			{      
					if (myTarA == null){
						$(nGDD).find('li').css('display','none');
								return false;
							}
							else {
                                // Add By SES.JiJianXiong 2010.08.18 ST
                                var oldValue = curSel[0].value;
                                // Add By SES.JiJianXiong 2010.08.18 ED

	                            // Add By SES JiJianxiong 2010.08.24 ST
	                            if ( myTarA == "£¨Î´Ñ¡Ôñ£©" ) {
	                                myTarA = "";
	                            }
	                            // Add By SES JiJianxiong 2010.08.24 ED

								//set the value of the old select box
								$(curSel).val(myTarA);
								//set the text of the new one
								 $(nGDD).find('span.gselected').text(myTarT);
								 
								 $(nGDD).find('li').css('display','none');

                                // Add By SES.JiJianXiong 2010.08.18 ST
                                if ( oldValue != myTarA ) {
                                    $(curSel).change();
                                }
                                // Add By SES.JiJianXiong 2010.08.18 ED
								 return false;
							}
			}
	//handle the tab index functions
	 }).focus( function(e) {        
	 	        

		 $(nGDD).find('li:first').addClass('currentDD');
		 $(nGDD).find('li:last').addClass('lastDD');
		 function checkKey(e){
			//on keypress handle functions
			function moveDown() {
	    	    // Add By SES.JiJianxiong 2010.08.25 ST
                // while it's disabled.
                if ( disable == true ) {
                    return;
                }
                // Add By SES.JiJianxiong 2010.08.25 ED
				var current = $(nGDD).find('.currentDD:first');
				var next = $(nGDD).find('.currentDD').next();
				if ($(current).is('.lastDD')){
				return false;
				} else {
					$(next).addClass('currentDD');
					$(current).removeClass('currentDD');
				}
			}
			function moveUp() {
	    	    // Add By SES.JiJianxiong 2010.08.25 ST
                // while it's disabled.
                if ( disable == true ) {
                    return;
                }
                // Add By SES.JiJianxiong 2010.08.25 ED
				var current = $(nGDD).find('.currentDD:first');
				var prev = $(nGDD).find('.currentDD').prev();
				if ($(current).is('.first')){
				return false;
				} else {
					$(prev).addClass('currentDD');
					$(current).removeClass('currentDD');
				}
			}
			var curText = $(nGDD).find('.currentDD:first').text();
			var curVal = $(nGDD).find('.currentDD:first a').attr('rel');
			// Add By SES.Jijianxiong 2010.08.24 ST
			// Enter Key -> Tab Key
            if ( e.keyCode == 13 ) {
                window.event.keyCode = 9
            }
			// Add By SES.Jijianxiong 2010.08.24 ED
		   switch (e.keyCode) {
				case 40:
					$(curSel).val(curVal);
					$(nGDD).find('span.gselected').text(curText);
					moveDown();
					return false;
					break;
				case 38:
					$(curSel).val(curVal);
					$(nGDD).find('span.gselected').text(curText);
					moveUp();
					return false;
					break;
				case 13:
					$(nGDD).find('li').css('display','none');
					}     
		}
		$(document).keydown(checkKey);	
	}).blur( function() {
			$(document).unbind('keydown');
	});
});
});