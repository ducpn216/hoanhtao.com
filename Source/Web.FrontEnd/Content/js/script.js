$(document).ready(function () {
    var isLoaded = 1;
    var more = 0;
    //if (!CheckIPLocal()) redirectURL("/");
    var tb = $("#toolbar_profile");
    //var objLogin = $.login();
    //if (objLogin.check) {

    //    tb.html(
    //       "Xin chào, <span style='color: red;'>" + objLogin.u_name + "</span> | " +
    //       "<a href='javascript:;' onclick='FOConnectConfig.updateaccount();'>Tài khoản</a> | " +
    //       "<a target='_blank' href='http://id.fpt.net/thay-doi-mat-khau.html'>Đổi mật khẩu</a> | " +
    //       "<a onclick='FOConnect.logout();' href='javascript:;'>Thoát</a>");
    //} else {

    //    tb.html(
    //        "<a href='javascript:;' onclick='FOConnect.login();'>Đăng nhập</a> | " +
    //        "<a href='javascript:;' onclick='FOConnectConfig.signup();'>Đăng ký</a> | " +
    //        "<a target='_blank' href='http://id.fpt.net/khoi-phuc-mat-khau.html'>Quên mật khẩu</a>");
    //}

    $(".link_event").click(function () {
        window.location = "TinTuc/DanhSach/SuKien/index.html";
    });
    $(".link_news").click(function () {
        window.location = "TinTuc/DanhSach/TinTuc/index-2.html";
    });
    $(".link_event").hover(function () {
        more = 0;
    });
    $(".link_news").hover(function () {
        more = 0;
    });
    setLastNews();
    function setLastNews() {
        newsCount = $("#content_tintuc .tinconlist").length - 1;
        $("#content_tintuc .tinconlist:eq(" + newsCount + ")").addClass("last_news");
        eventCount = $("#content_event .tinconlist").length - 1;
        $("#content_event  .tinconlist:eq(" + eventCount + ")").addClass("last_news");
    }
    $(window).scroll(function () {
        if (isLoaded == 1) {
            if (more <= 1) {
                wTop = $(this).scrollTop() + 500;
                loadMore(wTop);
            }
        }
    });
    $(".loadmore_news").click(function () {
        more = 0;
        if ($("#content_tintuc").is(":visible")) {
            newsCount = $("#content_tintuc .tinconlist").length;
            getArticles(newsCount, 1);
        }
        else if ($("#content_event").is(":visible")) {
            eventCount = $("#content_event .tinconlist").length;
            getArticles(eventCount, 2);
        }
    });
    function loadMore(wTop) {

        var newsOffset = $(".loadNews").offset();
        var eventOffset = $(".loadEvent").offset();
        if ($("#content_tintuc").is(":visible")) {
            if (wTop >= newsOffset.top) {
                newsCount = $("#content_tintuc .tinconlist").length;
                getArticles(newsCount, 1);
                more++;
            }
        }
        else if ($("#content_event").is(":visible")) {
            if (wTop >= eventOffset.top) {
                eventCount = $("#content_event .tinconlist").length;
                getArticles(eventCount, 2);
                more++;
            }
        }



    }

    function getArticles(index, type) {
        isLoaded = 0;
        $.ajax({
            type: 'post',
            url: '/Event/Ajax/Default.aspx?ajaxType=loadMore&&articleType=' + type + '&&pageIndex=' + index,
            dataType: 'html',
            success: function (result) {
                if (type == 1) {
                    $("#content_tintuc .listtin .post-wrapper").append(result);
                }
                else if (type == 2) {
                    $("#content_event .listtin .post-wrapper").append(result);
                }
                isLoaded = 1;
                setLastNews();
            }
        });

    }


});
$(document).ready(function () {


    //var objJSON = polls_get();
    //if (objJSON != "error!") {
    //    var pollId = objJSON.ID, question = objJSON.Text, multi = objJSON.MultiOptions, options = objJSON.Options;

    //    var objHTML = "<p class='question'>" + question + "</p>";
    //    var isSellect;
    //    objHTML += "<ul>";
    //    if (multi == "True") {
    //        $.each(options, function (i, v) {
    //            if (i == 0) { isSelect = 'checked="checked"' }
    //            objHTML +=
    //                "<li><input " + isSellect + " type='checkbox' id='" + v.id + "' class='bc' /><span style='padding-left:10px'>" + v.Text + "</span></</li>";
    //        });
    //    } else {
    //        $.each(options, function (i, v) {
    //            if (i == 0) { isSelect = 'checked="checked"' }
    //            objHTML +=
    //                "<li><input " + isSellect + "  type='radio' id='" + v.ID + "' class='bc' name='rd' /><span>" + v.Text + "</span></li>";
    //        });
    //    }
    //    objHTML += "<p style='text-align: center; padding-top: 8px;'><button id='btn_vote' class='submit_xn' value='Bình chọn'></button> <button id='btn_viewresult' class='submit_kq' value='Bình chọn'></button></p>";
    //    objHTML += "</ul>";

    //    $("#vote").html(objHTML);

    //    $("#btn_vote").click(function () {

    //        if ($("#vote").is(":visible")) {

    //            if ($(".bc").filter(":checked").length != 0) {
    //                var optionsId = $(".bc:checked"), ops = "";
    //                $.each(optionsId, function () {
    //                    ops += $(this).attr("id") + ",";
    //                });
    //                ops = ops.substr(0, ops.length - 1);
    //                var flag = polls_check();
    //                if (!flag) {
    //                    polls_vote(pollId, ops);
    //                    $(".bc").attr("checked", false);
    //                    vote_result();
    //                } else {
    //                    alert("Bạn đã bình chọn rồi!");
    //                }
    //            }
    //            else alert("Bạn chưa chọn câu trả lời!");
    //        } else {

    //            $("#vote_result").hide(), $("#vote").show();
    //        }
    //        return false

    //    });
    //}
    //$(".back").live('click', function () { $("#vote_result").hide(), $(".poll-vote").show(); return false; });
    //$("#btn_viewresult").live('click', function () { vote_result(); $("#vote_result").show(), $(".poll-vote").hide(); return false; });
    function vote_result() {

        if ($("#vote").is(":visible")) {
            objJSON = polls_result();
            if (objJSON != "error") {
                options = objJSON.Options, sum = objJSON.Sum;
                objHTML = "<p style='font-weight:bold;margin-left:10px'>" + question + "<p/>";
                $.each(options, function (i, v) {

                    objHTML += "<div style='font-weight:normal;margin-left:10px'>" + v.Text + "</div><div class='chart'><div class='percent' style='width:" + v.VoteCount / sum * 420 + "px'></div>" + $.htmlVote(v.VoteCount, sum) + "</div>";
                });
                objHTML += "<p style='margin-left:10px'>Tổng số lượt bình chọn: " + sum + "</p>";
                objHTML += "<p style='text-align: center; padding-top: 8px;'><div style='cursor:pointer;width:90px;margin:auto'  class='submit_back back'  value='Bình chọn''></div></p>";
                $("#vote_result").html(objHTML);
                $(".poll-vote").hide(), $("#vote_result").show();
            }
        }
        return false
    }
});

//get poll 
$(document).ready(function () {
    //var objJSON = polls_get();
    //if (objJSON != "error!") {
    //    var pollId = objJSON.ID, question = objJSON.Text, multi = objJSON.MultiOptions, options = objJSON.Options;
    //    $('.poll-question').html(question);
    //}
});


//SignUP
var error = "";
function resetPopup() {

    $("#SigninPopup_wrap").height(130);
    $("#SigninPopup_wrap").width(300);
    $("#SigninPopup_wrap").css("border", "8px solid rgba(0,0,0,0.4)");
    $("#SigninPopup_wrap").css("z-index", "10000");
    $(".popup_content").height(85);
    $("#SigninPopup_wrap").css("margin-top", "-65px");
    $("#SigninPopup_wrap").css("margin-left", "-150px");
    $(".popup_content").css("padding", "25px 20px 20px 20px");
    $(".close_wrap").css("margin-top", "-126px");
    $(".close_wrap").css("margin-left", "278px");
}
$(document).ready(function () {

    //$(".submit_dk").click(function () {
    //    resetPopup();
    //    if (verify()) {
    //        var username = $(".signup_username").val(), password = $(".signup_pass").val(), verifyCode = $(".signup_verifycode").val()
    //        signUp(verifyCode, username, password);
    //    }
    //    else {
    //        showSigninPopUp();
    //        $(".popup_content").html(error);

    //    }

    //});
    //$(".submit_dkleft").click(function () {
    //    resetPopup();
    //    if (verify_left()) {
    //        var username = $(".signup_usernameleft").val(), password = $(".signup_passleft").val(), verifyCode = $(".signup_verifycodeleft").val()
    //        signUp(verifyCode, username, password);
    //    }
    //    else {
    //        showSigninPopUp();
    //        $(".popup_content").html(error);

    //    }

    //});
});
function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function signUp(verifyCode, username, password) {

    $.ajax({
        type: 'post',
        url: '/Event/Ajax/Default.aspx?ajaxType=signUp&&verifyCode=' + verifyCode + '&&username=' + username + '&&password=' + password,
        dataType: 'html',
        success: function (response) {
            result_description = response.split('|')[0];
            result = response.split('|')[1];
            showSigninPopUp();
            $(".popup_content").html(result_description);
            if (result == 1) {
                _gaq.push(['_trackEvent', 'NRU', 'Register', 'Hoanh-DK nhanh']);
                $(".popup_content").append("<div class='close_dn' style='padding-top:10px;color:#222'>Ấn vào <a style='font-weight:bold' href='javascript:;' onclick='FOConnect.login();'>Đây</a> để đăng nhập</div>");
                ReloadImage('imageverifier', '/Library/VerifyImage/ImageVerifier.aspx');
                $(".signup_username").val('');
                $(".signup_pass").val('');
                $(".signup_repass").val('');
                $(".signup_verifycode").val('');




            }
        }
    });
}
function verify() {

    flag = true;
    error = "";
    if ($(".signup_username").val() == '') {

        flag = false;
        error += "(*) Bạn chưa nhập tên tài khoản. <br/>";

    }
    else if ($(".signup_username").val().length < 6 || $(".signup_username").val().length > 51) {
        flag = false;
        error += "(*) Tên tài khoản phải từ 6-50 kí tự. <br/>";
    }
    if ($(".signup_pass").val() == '') {
        flag = false;
        error += "(*) Bạn chưa nhập mật khẩu. <br/>";
    }
    else if ($(".signup_pass").val().length < 6 || $(".signup_pass").val().length > 64) {
        flag = false;
        error += "(*) Mật khẩu phải từ 6-64 kí tự. <br/>";
    }
    else
        if ($(".signup_pass").val() != $(".signup_repass").val()) {
            flag = false;
            error += "(*) Mật khẩu nhập lại không chính xác. <br/>";
        }

    if ($(".signup_verifycode").val() == '') {
        flag = false;
        error += "(*) Bạn chưa nhập mã xác nhận. <br/>";
    }

    return flag;
}
function verify_left() {

    flag = true;
    error = "";
    if ($(".signup_usernameleft").val() == '') {

        flag = false;
        error += "(*) Bạn chưa nhập tên tài khoản. \n";

    }
    else if ($(".signup_usernameleft").val().length < 6 || $(".signup_usernameleft").val().length > 51) {
        flag = false;
        error += "(*) Tên tài khoản phải từ 6-50 kí tự. \n";
    }
    if ($(".signup_passleft").val() == '') {
        flag = false;
        error += "(*) Bạn chưa nhập mật khẩu. \n";
    }
    else if ($(".signup_passleft").val().length < 6 || $(".signup_passleft").val().length > 64) {
        flag = false;
        error += "(*) Mật khẩu phải từ 6-64 kí tự. \n";
    }
    else
        if ($(".signup_passleft").val() != $(".signup_repassleft").val()) {
            flag = false;
            error += "(*) Mật khẩu nhập lại không chính xác. \n";
        }

    if ($(".signup_verifycodeleft").val() == '') {
        flag = false;
        error += "(*) Bạn chưa nhập mã xác nhận. \n";
    }
    

    return flag;
}

//function showSigninPopUp() {
//    if (!$("#overcontain").length) {
//        $("body").append("<div id='overcontain'></div>");
//        $("#overcontain").height($(document).height());
//    }
//    if (!$("#SigninPopup_wrap").length) {
//        $("body").append("<div id='SigninPopup_wrap'><div class='popup_title'></div><div class='popup_content'></div><div class='close_wrap'><img src='http://toolbar.gate.vn/PopUp/Images/close_gray.png' alt=''/></div></div>");
//    }
//    $("#overcontain").fadeIn();
//    $("#SigninPopup_wrap").fadeIn();

//    h = $("#SigninPopup_wrap").height();
//    w = $("#SigninPopup_wrap").width();
//    $("#SigninPopup_wrap").css({ "margin-top": -h / 2, "margin-left": -w / 2 });
//    $(".close_wrap").hover(function () { $('img', this).attr('src', '../toolbar.gate.vn/PopUp/Images/close.png'); }, function () { $('img', this).attr('src', '../toolbar.gate.vn/PopUp/Images/close_gray.png'); });
//    $(".close_wrap").live('click', (function () { $("#SigninPopup_wrap").fadeOut(); $("#overcontain").fadeOut(); }));
//    $(".close_dn").live('click', (function () { $("#SigninPopup_wrap").fadeOut(); $("#overcontain").fadeOut(); }));
//    $("#overcontain").live('click', function () {
//        $("#SigninPopup_wrap").fadeOut();
//        $(this).fadeOut();
//    });

//}

//Survey




