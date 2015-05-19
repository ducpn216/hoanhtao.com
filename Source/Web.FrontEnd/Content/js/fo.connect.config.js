if (!window.FOConnectConfig)
    window.FOConnectConfig = FOConnectConfig = {
    popupSignup: !1,
    popupUpdate: !1,
    urlFPTID: 'http://id.fpt.net',
    popupCenter: function (a, b, c, d, e, f) {
        "undefined" ===
        typeof e && (e = screen.width / 2 - b / 2);
        "undefined" === typeof d && (d = screen.height / 2 - c / 2);
        120 < d && (d -= 70);
        a = window.open(a, "_blank", "menubar=0,resizable=1,toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,copyhistory=no," + ("width=" + b + ", height=" + c + ", top=" + d + ", left=" + e));
        "function" === typeof a.focus && !f && a.focus();
        return a
    },
    signup: function () {
        try {
            var b = location.protocol + '//' + location.host + '/Close/Close/';
            var d = FOConnectConfig.urlFPTID + '/dang-ky-popup.html?referersp=' + encodeURIComponent(b);
            if (!1 === FOConnectConfig.popupSignup) {
                if (FOConnectConfig.popupSignup = FOConnectConfig.popupCenter(d, 605, 540, 50), 'boolean' === typeof FOConnectConfig.popupSignup.closed)
                    var e = setInterval(function () {
                        FOConnectConfig.popupSignup.closed && (FOConnectConfig.popupSignup = !1, clearInterval(e))
                    }, 500)

            } else 'function' === typeof FOConnectConfig.popupSignup.focus && FOConnectConfig.popupSignup.focus()
        } catch (e) { }
    },
    updateaccount: function () {
        try {
            var b = location.protocol + '//' + location.host + '/Close/CloseUpdate/';
            var d = FOConnectConfig.urlFPTID + '/cap-nhat-popup.html?referersp=' + encodeURIComponent(b);
            if (!1 === FOConnectConfig.popupUpdate) {
                if (FOConnectConfig.popupUpdate = FOConnectConfig.popupCenter(d, 620, 800, 50), 'boolean' === typeof FOConnectConfig.popupUpdate.closed)
                    var e = setInterval(function () {
                        FOConnectConfig.popupUpdate.closed && (FOConnectConfig.popupUpdate = !1, clearInterval(e))
                    }, 500)

            } else 'function' === typeof FOConnectConfig.popupUpdate.focus && FOConnectConfig.popupUpdate.focus()
        } catch (e) { }
    }
};
