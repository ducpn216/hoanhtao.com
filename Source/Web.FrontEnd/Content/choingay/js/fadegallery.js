
	jQuery(document).ready(function(){

							
									if(jQuery("#img").length>0){
										new FadeGallery(jQuery("#img"),
											{
												control_event:"mouseover",
												auto_play:true,
												control:jQuery("ul#imgControl"),delay:3
											}
										);
									}
								});
									
											


/* fadegallery.js */ 

FadeGallery=function(a,d){var c="ActiveBanner";var b=this;this.setup=function(){this.options={control_event:d!=undefined&&d.control_event!=undefined?d.control_event:"click",auto_play:d!=undefined&&d.auto_play!=undefined?d.auto_play:false,delay:d!=undefined&&d.delay!=undefined?d.delay*1000:2*1000,control:d!=undefined&&d.control!=undefined?d.control:undefined,next_btn:d!=undefined&&d.next_btn!=undefined?d.next_btn:undefined,prev_btn:d!=undefined&&d.prev_btn!=undefined?d.prev_btn:undefined,play_backward:d!=undefined&&d.play_backward!=undefined?d.play_backward:false};this.list=a;this.list_items=this.list.find("> li");this.control_items=this.options.control.find("li");this.current_active_index;this.onAnimate=false;this.onHover=false;this.list_items.each(function(e){var f=jQuery(this);if(f.hasClass(c)){b.current_active_index=e}});b.control_items.eq(b.current_active_index).addClass(c);if(this.options.control!=undefined){this.list_items.hover(function(){b.clearTimer()},function(){if(b.options.auto_play){b.autoPlay()}});this.control_items.each(function(e){var f=jQuery(this);f.bind(b.options.control_event,function(){if(!f.hasClass(c)){b.clearTimer();b.onAnimate=true;b.gotoSlide(e)}b.clearTimer();return false});f.bind("mouseout",function(){if(b.options.auto_play){b.autoPlay()}})})}if(this.options.next_btn!=undefined){this.options.next_btn.bind("click",function(){if(!b.onAnimate){b.clearTimer();b.onAnimate=true;b.next()}return false})}if(this.options.prev_btn!=undefined){this.options.prev_btn.bind("click",function(){if(!b.onAnimate){b.clearTimer();b.onAnimate=true;b.prev()}return false})}if(this.options.auto_play){b.autoPlay()}return b};this.clear=function(){this.list_items.eq(this.current_active_index).removeClass(c);this.control_items.eq(this.current_active_index).removeClass(c);this.current_active_index=undefined};this.autoPlay=function(){this.timer=setInterval(function(){b.onAnimate=true;if(!b.options.play_backward){if(b.current_active_index==b.list_items.length-1){b.gotoSlide(0)}else{b.gotoSlide(b.current_active_index+1)}}else{if(b.current_active_index==0){b.gotoSlide(b.list_items.length-1)}else{b.gotoSlide(b.current_active_index-1)}}},this.options.delay)};this.next=function(){if(this.current_active_index==this.list_items.length-1){this.gotoSlide(0)}else{this.gotoSlide(this.current_active_index+1)}};this.prev=function(){if(this.current_active_index==0){this.gotoSlide(this.list_items.length-1)}else{this.gotoSlide(this.current_active_index-1)}};this.gotoSlide=function(e){this.swapSlides(this.current_active_index,e,function(){b.current_active_index=e})};this.swapSlides=function(g,e,h){if(g!=undefined){b.control_items.eq(g).removeClass(c)}if((/MSIE 6\.0/).test(navigator.userAgent)){try{DD_belatedPNG.applyVML(b.control_items.eq(g).find("a").get(0))}catch(f){}}b.control_items.eq(e).addClass(c);if((/MSIE 6\.0/).test(navigator.userAgent)){try{DD_belatedPNG.applyVML(b.control_items.eq(e).find("a").get(0))}catch(f){}}this.list_items.eq(e).stop().animate({opacity:1});if(g!=undefined){this.list_items.eq(g).stop().animate({opacity:0},"normal","swing",function(){});b.list_items.eq(g).removeClass(c);b.list_items.eq(e).addClass(c);b.internal_callback();if(h!=undefined){h(b)}}else{b.list_items.eq(e).addClass(c);b.internal_callback();if(h!=undefined){h(b)}}};this.internal_callback=function(){this.onAnimate=false;if(this.timer==null){if(this.options.auto_play){this.autoPlay()}}};this.clearTimer=function(){clearInterval(this.timer);this.timer=null};this.setup()}; 
