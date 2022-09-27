
var elmHeight = "25";	// should be specified based on image size

// Extend JQuery Functionality For Custom Radio Button Functionality
jQuery.fn.extend({
dgStyle: function()
{
	// Initialize with initial load time control state
	$.each($(this), function(){
		var elm	=	$(this).children().get(0);
		elmType = $(elm).attr("type");
		$(this).data('type',elmType);
		$(this).data('checked',$(elm).attr("checked"));
		$(this).dgClear();
	});
	$(this).mousedown(function() { $(this).dgEffect(); });
	$(this).mouseup(function() { $(this).dgHandle(); });
// 2010.09.16 Add By SES.JiJianXiong ST
	$(this).click(function() { $(this).dgHandle(); });

// 2010.09.16 Add By SES.JiJianXiong ED
	
},
dgClear: function()
{
	if($(this).data("checked") == true)
	{
		$(this).css("backgroundPosition","center -"+(elmHeight*2)+"px");
		}
	else
	{
		$(this).css("backgroundPosition","center 0");
		}	
},
dgEffect: function()
{
// 2010.09.15 Update By SES.JiJianXiong ST
// for the radio button,while checked is true, is always true.
//	if($(this).data("checked") == true)
//		$(this).css({backgroundPosition:"center -"+(elmHeight*3)+"px"});
//	else
//		$(this).css({backgroundPosition:"center -"+(elmHeight)+"px"});

	if($(this).data("checked") != true) {
		$(this).css({backgroundPosition:"center -"+(elmHeight)+"px"});
    }

// 2010.09.15 Update By SES.JiJianXiong ED
},
dgHandle: function()
{
// 2010.09.15 Update By SES.JiJianXiong ST
// for the radio button,while checked is true, is always true.
//                      while checked is false, is turn to true , and group item turn to false.
	var elm	=	$(this).children().get(0);
//	if($(this).data("checked") == true)
//		$(elm).dgUncheck(this);
//	else
//		$(elm).dgCheck(this);
	if($(this).data("checked") != true) {
		$(elm).dgCheck(this);
    }
    
    
	
	if($(this).data('type') == 'radio')
	{
		$.each($("input[name='"+$(elm).attr("name")+"']"),function()
		{
			if(elm!=this)
				$(this).dgUncheck(-1);
		});
	}
// 2010.09.15 Update By SES.JiJianXiong ED
},
dgCheck: function(div)
{
	$(this).attr("checked",true);
	$(div).data('checked',true).css({backgroundPosition:"center -"+(elmHeight*2)+"px"});
},
dgUncheck: function(div)
{
	$(this).attr("checked",false);
	if(div != -1)
		$(div).data('checked',false).css({backgroundPosition:"center 0"});
	else
		$(this).parent().data("checked",false).css({backgroundPosition:"center 0"});
}
});