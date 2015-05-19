$(document).ready(function () {
    $("#partnerHeader").css("height","39px");
});

(function ($) {
   
    jQuery.extend
    (
        {
            getValues: function (data) {
                var result = null;
                $.ajax(
                {
                    url: "/Event/ajax/get.ashx",
                    type: 'get',
                    data: data,
                    //contentType: 'application/json; charset=utf-8',
                    dataType: 'text',
                    async: false,
                    cache: false,
                    success: function (data) {
                        result = data;
                    }
                });
                return result;
            }
        }
    );
})(jQuery);

(function ($) {
    jQuery.extend
    (
        {
            callAjax: function (url) {
                var result = null;
                $.ajax(
                {
                    url: url,
                    type: 'post',
                    dataType: 'html',
                    async: false,
                    cache: false,
                    success: function (data) {
                        result = data;
                    }
                });
                return result;
            }
        }
    );
})(jQuery);

//$.login = function () {
//    var obj = $.parseJSON($.callAjax("/Event/ajax/checklogin.ashx"));
//    return { "check": obj.check == "true", "u_name": obj.username }
//}

$.urlParam = function (name) {
    var results = new RegExp('[\\?&amp;]' + name + '=([^&amp;#]*)').exec(window.location.href);
    return results[1] || 0;
}

function redirectURL(url) {
    window.location.href = url;
}

function redirectURLNew(url) {
    var newWindow = window.open(url, '_blank');
    newWindow.focus();
    return false;
}

jQuery.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});

jQuery.extend({
    random: function (X) {
        return Math.floor(X * (Math.random() % 1));
    },
    randomBetween: function (MinV, MaxV) {
        return MinV + jQuery.random(MaxV - MinV + 1);
    }
});

jQuery.extend({
    htmlVote: function (vote, sum) {
        var w = ((vote / sum) * 100).toFixed(1);
        var objHTML = "<span>" + vote + " lượt - " + w + "%</span>";
        return objHTML;
    }
});

jQuery.extend({
    processTitle: function (string, num_char) {
        if (string.length <= num_char) return string;
        else return string.substr(0, num_char) + "...";
    }
});

function Set_Cookie(name, value, expires, path, domain, secure) {
    // set time, it's in milliseconds
    var today = new Date();
    today.setTime(today.getTime());

    /*
	if the expires variable is set, make the correct
	expires time, the current script below will set
	it for x number of days, to make it for hours,
	delete * 24, for minutes, delete * 60 * 24
	*/
    if (expires) {
        //expires = expires * 1000 * 60 * 60 * 24;
        expires = expires * 1000 * 60;
    }
    var expires_date = new Date(today.getTime() + (expires));

    document.cookie = name + "=" + escape(value) +
((expires) ? ";expires=" + expires_date.toGMTString() : "") +
((path) ? ";path=" + path : "") +
((domain) ? ";domain=" + domain : "") +
((secure) ? ";secure" : "");
}

function Get_CookieValue(check_name) {
    // first we'll split this cookie up into name/value pairs
    // note: document.cookie only returns name=value, not the other components
    var a_all_cookies = document.cookie.split(';');
    var a_temp_cookie = '';
    var cookie_name = '';
    var cookie_value = '';
    var b_cookie_found = false; // set boolean t/f default f

    for (i = 0; i < a_all_cookies.length; i++) {
        // now we'll split apart each name=value pair
        a_temp_cookie = a_all_cookies[i].split('=');


        // and trim left/right whitespace while we're at it
        cookie_name = a_temp_cookie[0].replace(/^\s+|\s+$/g, '');

        // if the extracted name matches passed check_name
        if (cookie_name == check_name) {
            b_cookie_found = true;
            // we need to handle case where cookie has no value but exists (no = sign, that is):
            if (a_temp_cookie.length > 1) {
                cookie_value = unescape(a_temp_cookie[1].replace(/^\s+|\s+$/g, ''));
            }
            // note that in cases where cookie is initialized but no value, null is returned
            return cookie_value;
            break;
        }
        a_temp_cookie = null;
        cookie_name = '';
    }
    if (!b_cookie_found) {
        return null;
    }
}

function Delete_Cookie(name, path, domain) {
    if (Get_Cookie(name)) document.cookie = name + "=" +
((path) ? ";path=" + path : "") +
((domain) ? ";domain=" + domain : "") +
";expires=Thu, 01-Jan-1970 00:00:01 GMT";
}

function Get_Cookie(name) {

    var start = document.cookie.indexOf(name + "=");
    var len = start + name.length + 1;
    if ((!start) && (name != document.cookie.substring(0, name.length))) {
        return null;
    }
    if (start == -1) return null;
    var end = document.cookie.indexOf(";", len);
    if (end == -1) end = document.cookie.length;
    return unescape(document.cookie.substring(len, end));
}

function IntroCookie(IntroURL) {
    //Detected Cookie Browser
    var cookieEnabled = (navigator.cookieEnabled) ? true : false;
    //if not IE4+ nor NS6+
    if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
        document.cookie = "checkcookie"
        cookieEnabled = (document.cookie.indexOf("checkcookie") != -1) ? true : false
    }

    if (cookieEnabled) {
        if (!Get_Cookie('IntroHT')) {
            //Write Cookie
            Set_Cookie('IntroHT', 'Intro Actived', 15, 'index.html', '', '');
            window.location.href = IntroURL;
        }
    }
}
function checkCookieSurveys()
{
    b = false;
    var cookieEnabled = (navigator.cookieEnabled) ? true : false;
    //if not IE4+ nor NS6+
    if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
        document.cookie = "checkcookie"
        cookieEnabled = (document.cookie.indexOf("checkcookie") != -1) ? true : false
    }

    if (cookieEnabled) {
        if (Get_Cookie('surveyHT')) {
            b = true;
        }
    }
    return b;
}
function CheckCookie(IntroURL, Value) {
    //Detected Cookie Browser
    var cookieEnabled = (navigator.cookieEnabled) ? true : false;
    //if not IE4+ nor NS6+
    if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
        document.cookie = "checkcookie"
        cookieEnabled = (document.cookie.indexOf("checkcookie") != -1) ? true : false
    }

    if (!Get_Cookie(Value)) {
        //Write Cookie
        Set_Cookie(Value, 'RegisterX1', 5, 'index.html', '', '');
        window.location.href = IntroURL;
    }
}

function CheckIPLocal() {
    return $.callAjax("/Event/ajax/checkIPlocal.ashx") == "True";
}

function CheckExpires(beginDate, endDate) {

}
function checkActiveCode()
{
   
    result = ($.callAjax("/Event/ajax/Default.aspx?ajaxType=checkActiveCode"))
    //result = 1;
   // alert(result);
    if(result==1) return true;
    else return false;
}
function checkToken(serverId)
{
    result = ($.callAjax("/Event/ajax/Default.aspx?ajaxType=countToken&&serverId=" + serverId))
    //alert(result);
    return result;
}
function playgame(serverid) {
   
    if (!Date.now) {
        Date.now = function () { return new Date().valueOf(); } // Ie7
    }
    var date = new Date;
        if (!$.login().check) {
            FOConnect.init({ receiver: 'http://hoanh.gate.vn/DangNhap/', loginurl: 'http://hoanh.gate.vn/ChoiNgay/Play/' + serverid + "/"}); FOConnect.login();
            //FOConnect.init({ receiver: 'http://hoanh.gate.vn/DangNhap/', loginurl: 'http://hoanh.gate.vn/ChoiNgay/' }); FOConnect.login();
        }
        else {
            if (serverid != -1) {
                if (checkToken(serverid) == "0") {

                        redirectURL("http://hoanh.gate.vn/ChoiNgay/Play/" + serverid + "/");
                   
                }
                else {
                    if (checkToken(serverid) == "1") {
                        alert('Server đang quá đông, xin vui lòng chọn server khác');
                    }
                    else {
                        alert('Số lượng truy cập đang quá đông, xin vui lòng đợi hoặc chọn server khác');
                    }
                }
            }
            else {
                redirectURL("http://hoanh.gate.vn/ChoiNgay/Play/" + serverid + "/");
            }




            ////if (checkActiveCode()) {
            //if (checkToken(serverid) == "0") {
            //    redirectURL("http://hoanh.gate.vn/ChoiNgay/Play/" + serverid + "/");
            //}
            //else {
            //    if (checkToken(serverid) == "1") {
            //        alert('Server đang quá đông, xin vui lòng chọn server khác');
            //    }
            //    else {
            //        alert('Số lượng truy cập đang quá đông, xin vui lòng đợi hoặc chọn server khác');
            //    }
            //}
            ////}
            ////else {
            ////    alert('Cần kích hoạt code alpha, xem chi tiết tại hoanh.gate.vn. Kích hoạt trước 12 giờ ngày 9/7');
            ////}
        }
    
};
function playgameGate(serverid) {

    if (!Date.now) {
        Date.now = function () { return new Date().valueOf(); } // Ie7
    }
    var date = new Date;
    if (!$.login().check) {
        FOConnect.init({ receiver: 'http://hoanh.gate.vn/DangNhap/', loginurl: 'http://hoanh.gate.vn/ChoiNgayGate/Play/' + serverid + "/" }); FOConnect.login();
    }
    else {
        if (serverid != -1) {
            if (checkToken(serverid) == "0") {

                redirectURL("http://hoanh.gate.vn/ChoiNgayGate/Play/" + serverid + "/");

            }
            else {
                if (checkToken(serverid) == "1") {
                    alert('Server đang quá đông, xin vui lòng chọn server khác');
                }
                else {
                    alert('Số lượng truy cập đang quá đông, xin vui lòng đợi hoặc chọn server khác');
                }
            }
        }
        else {
            redirectURL("http://hoanh.gate.vn/ChoiNgayGate/Play/" + serverid + "/");
        }
    }

};
function launcherplaygame(serverid) {
   
    if (!Date.now) {
        Date.now = function () { return new Date().valueOf(); } //ie7
    }
    var date = new Date;
   
    //if (checkActiveCode()) {
        if (serverid != -1)
        {
            if (checkToken(serverid) == "0") {
               
                    redirectURL("http://hoanh.gate.vn/ChoiNgay/LauncherPlay.aspx?ServerID=" + serverid);
              
            }
            else {
                if (checkToken(serverid) == "1")
                {
                    alert('Server đang quá đông, xin vui lòng chọn server khác');
                }
                else
                {
                    alert('Số lượng truy cập đang quá đông, xin vui lòng đợi hoặc chọn server khác');
                }
            }
        }
        else
        {
            redirectURL("http://hoanh.gate.vn/ChoiNgay/LauncherPlay.aspx?ServerID=" + serverid);
        }
    //}
    //else {
    //    alert('Cần kích hoạt code alpha, xem chi tiết tại hoanh.gate.vn. Kích hoạt trước 12 giờ ngày 9/7');
    //}
    
   
};

function getPosts(begin, end) {
    return $.parseJSON($.getValues({ "fn": "news_getposts", "begin": begin, "end": end })).data;
};

function getPostsByType(begin, end, type) {
    return $.parseJSON($.getValues({ "fn": "news_getpostsbytype", "begin": begin, "end": end, "type": type })).data;
};

function getPostById(id) {
    return $.parseJSON($.getValues({ "fn": "news_getpostsbyid", "id": id })).data;
};

function getPostCountByType(type) {
    return $.parseJSON($.getValues({ "fn": "news_getpostcountbytype", "type": type })).data;
};

function getPics(begin, end) {
    return $.parseJSON($.getValues({ "fn": "pictures_getpictures", "begin": begin, "end": end })).data;
};

function getPicsByType(begin, end, type) {
    return $.parseJSON($.getValues({ "fn": "pictures_getpicturesbytype", "begin": begin, "end": end, "type": type })).data;
};

function getPicById(id) {
    return $.parseJSON($.getValues({ "fn": "pictures_getpicturesbyid", "id": id })).data;
};

function getPicCountByType(type) {
    return $.parseJSON($.getValues({ "fn": "pictures_getpicturecountbytype", "type": type })).data;
};

function getMovies(begin, end) {
    return $.parseJSON($.getValues({ "fn": "movies_getmovies", "begin": begin, "end": end })).data;
};

function getMovieCount() {
    return $.parseJSON($.getValues({ "fn": "movies_getmoviecount" })).data;
};

function getEventBanners() {
    return $.parseJSON($.getValues({ "fn": "eventbanners" })).data;
};

//function polls_get() {
//    return $.parseJSON($.getValues({ "fn": "polls_get" })).data;
//};

//function polls_check() {
//    return $.parseJSON($.getValues({ "fn": "polls_check" })).data;
//};

//function polls_result() {
//    return $.parseJSON($.getValues({ "fn": "polls_result" })).data;
//};

//function polls_vote(pollId, options) {
//    return $.parseJSON($.getValues({ "fn": "polls_vote", "pollID": pollId, "options": options })).data;
//};

function gameitems_get() {
    return $.parseJSON($.getValues({ "fn": "serveritems" })).data;
};

function published_gameitems_get() {
    return $.parseJSON($.getValues({ "fn": "published_serveritems" })).data;
};

function gamehistory_get(username) {
    return $.parseJSON($.getValues({ "fn": "gamehistory_get", "username": username })).data;
};

function ReloadImage(id, srcname) {
    var elm = document.getElementById(id);
    var dt = new Date();
    elm.src = srcname + '?t=' + dt;
    return false;
}

/*check exists*/
jQuery.fn.exists = function () { return this.length > 0; };

/*load iframe*/
function loadIFrame() {
    var t = new Date();
    var ticks = t.getTime();
    var url = "/Event/GiftCode/?t=" + ticks;
    var ifr = document.getElementById('iframe');
    ifr.src = url;
    ifr.contentWindow.location.reload();
    //window.frames['iframe'].location = url;
}


function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}


