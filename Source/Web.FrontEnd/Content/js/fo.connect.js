if (!window.FOConnect) window.FOConnect = FOConnect = {
    redirect: null,
    popupLogin: !1,
    urlFPTID: 'http://id.fpt.net',
    loadFospScript: function (a, b, c) {
        var d = document.createElement("script");
        d.type = "text/javascript";
        d.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + a;
        if (!!b) {
            d.defer = "defer";
            d.async = true;
        };
        if (c instanceof Function) d.onreadystatechange = function () {
            "loaded" === this.readyState && c.apply({}, [])
        }, d.onload = c;
        a = document.getElementsByTagName("script")[0];
        a.parentNode.insertBefore(d, a);
    },
    cookie: function (a, b, c) {
        if (1 < arguments.length && "[object Object]" !== "" + b) {
            c = "object" === typeof c ? c : {};

            if (null === b || void 0 === b) c.expires = -1;
            if ("number" === typeof c.expires) {
                var d = c.expires, e = c.expires = new Date;
                e.setDate(e.getDate() +
                    d)
            }
            b = "" + b;
            return document.cookie = [encodeURIComponent(a), "=", c.raw ? b : encodeURIComponent(b), c.expires ? "; expires=" + c.expires.toUTCString() : "", c.path ? "; path=" + c.path : "", c.domain ? "; domain=" + c.domain : "", c.secure ? "; secure" : ""].join("")
        }
        c = b || {};

        e = c.raw ? function (a) {
            return a
        } : decodeURIComponent;
        return (d = RegExp("(?:^|; )" + encodeURIComponent(a) + "=([^;]*)").exec(document.cookie)) ? e(d[1]) : null
    },
    addListen: function (a, b, c) {
        Ed = function (a) {
            a.preventDefault ? a.preventDefault() : a.returnValue = false;
            return false;
        };
        El = window.addEventListener ? function (a, b, c) {
            var d = function (a) {
                var b = c.call(this, a);
                false === b && Ed(a);
                return b;
            }
            a.addEventListener(b, d, false);
            return d;
        } : window.attachEvent ? function (a, b, c) {
            var d = function () {
                var b = window.event, d = c.call(a, b);
                false === d && Ed(b);
                return d;
            };
            a.attachEvent("on" + b, d);
            return d;
        } : void 0;
        El(a, b, c);
    },
    isGoodBrowser: function () {
        var a = navigator.userAgent, b = a.indexOf("MSIE ");
        return -1 != b && 8 > parseFloat(a.substring(b + 5, a.indexOf(";", b))) ? !1 : !0
    },
    popupCenter: function (a, b, c, d, e, f) {
        "undefined" ===
        typeof e && (e = screen.width / 2 - b / 2);
        "undefined" === typeof d && (d = screen.height / 2 - c / 2);
        120 < d && (d -= 70);
        a = window.open(a, "_blank", "menubar=0,resizable=1,toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=no,resizable=yes,copyhistory=no," + ("width=" + b + ", height=" + c + ", top=" + d + ", left=" + e));
        "function" === typeof a.focus && !f && a.focus();
        return a
    },
    doLogin: function (a) {
        FOConnect.redirect = a || FOConnect.redirect;
        //window.location.href = FOConnect.redirect;
        window.location.href = window.location.href;
        //window.reload(!0);
    },
    logout: function (a) {
        a = a || FOConnect.logouturl;
        //window.location.href = FOConnect.urlFPTID + '/logout/?referersp=' + encodeURIComponent(a);
        window.location.href = FOConnect.urlFPTID + '/logout/?referersp=' + encodeURIComponent(window.location.protocol + '//' + window.location.host + '/Thoat/?r=' + window.location.href);
    },

    login: function (a) {
        FOConnect.redirect = a || FOConnect.loginurl;
        try {
            if ("function" === typeof FOConnect.redirect) {
                FOConnect.doLogin = function () {
                    try {
                        FOConnect.redirect.apply({}, arguments);
                    } catch (c) {
                        alert(c)
                    }
                };
            } else {
                if ("string" === typeof FOConnect.redirect) {
                    if (0 === FOConnect.redirect.indexOf(location.protocol + "//" + location.host)) {
                        FOConnect.redirect = FOConnect.redirect.replace(/#.*$/, "");
                    } else {
                        FOConnect.redirect = FOConnect.loginurl;
                    }
                }
                FOConnect.doLogin = function () {
                    window.location.href = FOConnect.redirect;
                };
            }
            d = FOConnect.urlFPTID + "/?display=connect&receiver=" + encodeURIComponent(FOConnect.receiver) + "&referersp=" + encodeURIComponent(FOConnect.receiver + '?action=foconnect');
            
            if (!1 === FOConnect.popupLogin) {
                if (FOConnect.popupLogin = FOConnect.popupCenter(d, 600, 560, 50), "boolean" === typeof FOConnect.popupLogin.closed)
                    var e = setInterval(function () {
                        FOConnect.popupLogin.closed && (FOConnect.popupLogin = !1, clearInterval(e))
                    }, 500)

            } else 'function' === typeof FOConnect.popupLogin.focus && FOConnect.popupLogin.focus()
        } catch (f) { }
    },
    fptIdCheck: function () {
        id = FOConnect.cookie('Fid') || 0;
        FOConnect.loadFospScript('id.fpt.net/index/foconnect/id/' + id + '/foconnect.js?receiver=' + encodeURIComponent(FOConnect.receiver) + '&layout=' + FOConnect.raw, !0);
    },
    doSync: function (a) {
        if (FOConnect.syncsesion) {
            window.location.href = FOConnect.loginurl;
        }
        if (FOConnect.raw) {
            FOConnect.layout.apply({}, a);
        }
    },
    init: function (a) {
        var href = window.location.href;
        FOConnect.receiver = a.receiver || location.protocol + '//' + location.host + '/sso.php';
        FOConnect.logouturl = a.logouturl || href;
        FOConnect.loginurl = a.loginurl || href;
        FOConnect.raw = a.raw || !0;
        FOConnect.layout = "function" === typeof a.layout ? a.layout : function () { };
        FOConnect.syncsesion = a.syncsesion || !0;
        FOConnect.fptIdCheck();
    }
};