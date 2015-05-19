$(document).ready(function () {
    //$("#accordion li .sub-nav").hide();
    //if ($("#accordion .active").length <= 0) { $("#accordion li .sub-nav:eq(0)").show(); }
    //$("#accordion").on("click", "#accordion li", function () {
    //    var a = document.getElementById("accordion").getElementsByTagName("li");
    //    for (i = 0; i < a.length; i++) a.item(i).className = ""; !1 == $(this).children(".sub-nav").is(":visible") && $("#accordion li .sub-nav").slideUp(300);
    //    $(this).addClass("active");
    //    $(this).children(".sub-nav").slideToggle(300)
    //});
    //$("#accordion .active .sub-nav").show();

    $('a#linkTop').click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 300);
        return false;
    });

    url = window.location.href;

    //jQuery(function(a){
    //var b=a("#top_btn"),
    //c=b.offset().top,d=a(window);
    //d.bind("scroll resize",function(){
    //	var a=d.scrollTop();
    //	a>c&&!b.is(".top_btn_fixed")?b.removeClass("top_btn").addClass("top_btn_fixed"):a<=c&&b.is(".top_btn_fixed")&&b.removeClass("top_btn_fixed").addClass("top_btn")})});


    //if (url == 'http://hoanh.gate.vn/') {
    //    $('.dk_nhanh_block').show();
    //}
    //else {
    //    //$('.dk-nhanh').hover(function () {
    //    //    $('.dk_nhanh_block').slideDown();
    //    //    $('.form_signin').slideUp();

    //    //});

    //    //$('.dn').hover(function () {
    //    //    $('.form_signin').slideDown();
    //    //    $(".dk-nhanh").removeClass("active");

    //    //});

    //    //$('.dn').hover(function(){
    //    //    $('.dk_nhanh_block').slideUp();
    //    //    $('.form_signin').slideDown();
    //    //	$(".dk-nhanh").removeClass("active");
    //    //});

    //    $('.dk_taigame_napthe').hover(function () {
    //        $('.dk_nhanh_block').slideUp();
    //        $('.form_signin').slideUp();
    //        $(".dk-nhanh").removeClass("active");
    //    });

    //    $('.ContentLeft').hover(function () {
    //        $('.dk_nhanh_block').slideUp();
    //        $('.form_signin').slideUp();
    //        $(".dk-nhanh").removeClass("active");
    //    });

    //    $('.server-block').hover(function () {
    //        $('.dk_nhanh_block').slideUp();
    //        $('.form_signin').slideUp();
    //        $(".dk-nhanh").removeClass("active");
    //    });

    //    $('.nv_container').hover(function () {
    //        $('.dk_nhanh_block').slideUp();
    //        $('.form_signin').slideUp();
    //        $(".dk-nhanh").removeClass("active");
    //    });

    //    $('.block-video').hover(function () {
    //        $('.dk_nhanh_block').slideUp();
    //        $('.form_signin').slideUp();
    //        $(".dk-nhanh").removeClass("active");
    //    });

    //    $(".dk_nhanh_block").hover(function () {
    //        $(".dk-nhanh").addClass("active");
    //    });

    //}


    // hide #back-top first
    $("#back-top").hide();
    // fade in #back-top
    $(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 1150) {
                $('#back-top').fadeIn();
            } else {
                $('#back-top').fadeOut();
            }
        });

        // scroll body to 0px on click
        $('#back-top a').click(function () {
            $('body,html').animate({
                scrollTop: 0
            }, 300);
            return false;
        });
    });
});// JavaScript Document

$(document).ready(function () {
    var windowWidth = jQuery(window).width();
    var windowHeight = jQuery(window).height();
    var popupWidth = jQuery('.popup_block').width();
    var popupHeight = jQuery('.popup_block').height();
    $('.popup_block').css('left', (windowWidth - popupWidth) / 2);

    $(window).resize(function () {
        windowWidth = jQuery(window).width();
        windowHeight = jQuery(window).height();
        $('.popup_block').css('left', (windowWidth - popupWidth) / 2);
    });
    $('#thewindowbackground').css({ 'height': $(document).height(), 'width': $(document).width() });

    $('#pop-poll').click(function () {
        $('#pop_poll').css('visibility', 'visible');
        $('#thewindowbackground').show();
    });


    $('.close-poll').click(function () {
        $('#pop_poll').css('visibility', 'hidden');
        $('#thewindowbackground').hide();
    });
    $('#survey-poll').click(function () {
        $('#pop_survey').css('visibility', 'visible');
        $('#thewindowbackground').show();
    });


    $('.close-poll').click(function () {
        $('#pop_survey').css('visibility', 'hidden');
        $('#thewindowbackground').hide();
    });

});

$(document).ready(function () {
    $('.pop_open').show();
    /*$('.pop_open').delay(5000).hide("slide", {direction: "right"}, "500");
	
	if($('.pop_open').css('display') == 'block')
		{
			$('.pop_open').mouseover(function () {
				$(this).css('display','block')
			});	
		}*/
    if ($(".pop_open").hasmouseover) {
        $(this).show();
    }
    else {
        $('.pop_open').delay(5000).hide("slide", { direction: "right" }, "500");
    };



    $('.pop_close').click(function () {
        $('.pop_open').show("slide", { direction: "right" }, "500");
    });

    $('.close').click(function () {
        $('.pop_open').hide("slide", { direction: "right" }, "500");
    });
});