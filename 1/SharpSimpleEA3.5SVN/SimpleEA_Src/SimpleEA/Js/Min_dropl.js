// ==============================================================================
// File Name           : Min_dropl.js
// Description         : Min_dropl
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
 * FUNCTION : GetDesSeries
 * SUMMARY  : GetDesSeries
 * AUTHOR   : SES Ji JianXiong
 * DATE     : 2010.11.30
 * VERSION  : 0.01
 *******************************************/


/********************************************
 * FUNCTION : Min_dropl.js
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
 *  (5).2010.11.30
 *  SES Ji JianXiong Add:
 *      Long list to short
 *  
 *******************************************/
$(document).ready(function(){
// first locate all of the select tags on the page and hide them
 $("select.changeMe").css('display','none');
// Add By SES.zhoumiao 2011.03.22 ST
var btnie6=false;
        if (($.browser.version == "6.0"))
        {        
          btnie6=true;
        }
        else
        {      
          btnie6=false;
        }
// Add By SES.zhoumiao 2011.03.22 ED
//now, for each select box, run this function
$("select.changeMe").each(function(){   
	
	var intline = 6;
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
	
// Add By SES.zhoumiao 2011.03.22 ST
     if(btnie6)
     {
       var ie6check = this.getAttribute("ie6check");
       if (! ( typeof(ie6check) == "undefined" || ie6check == null ) ) 
       {
          if ( ie6check == "true" ) 
          {
             $("select.changeMe").css('display','block');
             var optionlist = $(curSel).children('option');
             optionlist.each( function(){           
		     var text = $(this).text(); 
		     if ( text == "" ) {$(this).attr("text","£¨Î´Ñ¡Ôñ£©");}});             
             return;
          }
       }
     }
// Add By SES.zhoumiao 2011.03.22 ED

	
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
	var gddFirst = '<div class="first"><span class="selectME gselected" style="width:'+ gddWidth2 +  'px;">'+ whatSelected +'</span><span id="arrowImg"></span><div class="clears"></div></div>';
	
	// create a new array of div options from the original's options
	var addItems = new Array();
	var optionlist = $(curSel).children('option');
	var _optionlistLength = optionlist.length;
	
	// Get Special Descending series
	var attriDate = this.getAttribute("dateday");
    var isdate = false;
    if (! ( typeof(attriDate) == "undefined" || attriDate == null ) ) {
        if ( attriDate == "dateday" ) {
            isdate = true;
            intline = 7;
        }
    }
    
    var _listNumItems = null;
    
 	var urlblock = '';
    var littleurlFirst = '<ul class="selectME">';
    var littleurlBottom ='</ul>'
    if ( isdate != true ) {
        var GetDesSeries = function ( count , splitNum) {

            var _retList = new Array();
            var intModValue = 0;
            
            intModValue = Math.floor(count/splitNum);
            
            if ( count/splitNum > intModValue ) {
                intModValue = intModValue + 1;
            } 

            for ( var i = 0; i < intModValue; i++ ) {
                _retList.push(splitNum);
            }

            
            if ( intModValue == 0 ) {
                return _retList;
            }
            
            // build series list
            var jseries = _retList.length - 1;
            var allcount = intModValue * splitNum;

            var flg = 1;
            var sameflg = intModValue - 1;
            if ( sameflg > 3 ) {
                sameflg = 3
            }
            
            for ( var i = 0; i < 20 ; i++ ) {
                if ( count >= allcount ) {
                    break;
                }

                if ( jseries < 0 ) {
                    jseries = _retList.length - 1;
                }
                
                _retList[jseries] = _retList[jseries] - 1;
                count = count + 1;
                
                if ( flg == sameflg ) {
                    jseries = jseries - 1;
                    flg =1;
                } else {
                    flg = flg + 1
                }
            }
            
            return _retList;
        }

        // array: each row's item count.
    	_listNumItems = GetDesSeries(_optionlistLength , intline);
    	    // first value.
	    var _intListid = 0;
    	
	    optionlist.each( function() {           
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
		    if ( addItems.length == 1 && urlblock == '' ) {
    		    addItems.push(before + text + after);
		    }
    	
	        var splitNum = _listNumItems[_intListid];
	        if ( urlblock == '' ) {
	            splitNum = splitNum + 1;
	        }
    	    
		    if ( addItems.length >= splitNum && urlblock == '' && gddWidthL < 300) {
	            //hide the default from the list of options 
	            var removeFirst = addItems.shift();
        	    urlblock = urlblock + littleurlFirst + addItems.join('') + littleurlBottom;
    		    addItems = new Array();
    		    _intListid = _intListid + 1;
		    } else if ( addItems.length >= splitNum && urlblock != ''  && gddWidthL < 300  ) {
    		
        	    urlblock = urlblock + littleurlFirst + addItems.join('') + littleurlBottom;

    		    addItems = new Array();      
    		    _intListid = _intListid + 1;
		    }
	        // Add By SES JiJianxiong 2010.08.25 ED
	    });
    	

        var itemslegnth = addItems.length;
	    if ( itemslegnth > 0 && urlblock == '' ) {
            //hide the default from the list of options 
            var removeFirst = addItems.shift();
	    }
    	
	    if ( itemslegnth > 0 ) {
    	    urlblock = urlblock + littleurlFirst + addItems.join('') + littleurlBottom;
	    }

    } else {
        // array: each row's item count.
        var urlblocklist = new Array();
        // for 1-7
    	urlblocklist.push(new Array());
    	urlblocklist.push(new Array());
    	urlblocklist.push(new Array());
    	urlblocklist.push(new Array());
    	urlblocklist.push(new Array());
    	urlblocklist.push(new Array());
    	urlblocklist.push(new Array());
    	// for cursor
    	var iCursor = 0;
	    optionlist.each( function() {           
		    if ( iCursor >= urlblocklist.length ) {
		        iCursor = 0;
		    }

		    var text = $(this).text();  
		    var selVal = $(this).attr('value'); 
		    var before =  '<li style="width:' + gddWidthL + 'px;"><a href="#" rel="' + selVal + '" tabindex="0"  style="width:' + gddWidth3 + 'px;">';
		    var after = '</a></li>';           

	        if ( text == "" ) {
	            text = "£¨Î´Ñ¡Ôñ£©";
	        }

		    urlblocklist[iCursor].push(before + text + after);
		    
            iCursor++;    		
	    });
	    
	    // array to string
	    for ( var i =0 ; i < urlblocklist.length ; i++ ) {
	        urlblock = urlblock + littleurlFirst + urlblocklist[i].join('') + littleurlBottom;
	    }
	    
    }

	// create the end of the div selectbox and close everything off
	var gddBottom ='</div></div></div></div><div class="cornersbottom"><div><div></div></div></div></div>'
	//write everything after each selectbox
	var GDD = gddTop + gddFirst + urlblock + gddBottom;
	
	$(curSel).after(GDD);
	
	//this var selects the div select box directly after each of the origials
	var nGDD = $(curSel).next('div.selectME');
	
	var nUL = $(nGDD).find('ul');
	
	for ( var i = 0 ; i < nUL.length ; i++ ) {
	    var nULItem = nUL[i]
	    $(nULItem).find('li:first').addClass("first");
	}
	
    $(nUL[0]).find('li:last').addClass('last');
	
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
					
					// Set the ul Location
					var ullist = $(nGDD).find('ul');
					if ( ullist.length <= 1 ) {
					    return false;
					}
					
					var urlfirst = ullist[0];
					var urlleft = urlfirst.offsetLeft;
					var urlwidth = this.offsetWidth;
					
					for ( var i = 1; i <= ullist.length - 1 ; i++ ) {
					    var urlobj = ullist[i];
					    if ( i == 1 ) {
					        urlleft = urlleft + urlwidth + 1;
					    } else {
					        urlleft = urlleft + urlwidth - 1;
					    }
					    
					    urlobj.style.left = urlleft;
					}
					
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
								// 2011.01.27 Update By SES Jijianxiong ST
								// $(curSel).val(myTarA);
								selectval($(curSel)[0] , myTarA);
								// 2011.01.27 Update By SES Jijianxiong ED
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
					// 2011.01.27 Update By SES Jijianxiong ST
					// $(curSel).val(curVal);
					selectval($(curSel)[0] , curVal);
					// 2011.01.27 Update By SES Jijianxiong ED

					$(nGDD).find('span.gselected').text(curText);
					moveDown();
					return false;
					break;
				case 38:
					// 2011.01.27 Update By SES Jijianxiong ST
					// $(curSel).val(curVal);
					selectval($(curSel)[0] , curVal);
					// 2011.01.27 Update By SES Jijianxiong ED
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


function selectval(obj, value ) {
    var checks = obj;
    for (var i = 0; i < checks.length; i++) {
        if ( checks[i].value == value) {
            checks.selectedIndex = i;
            break;
        }
    }
}
