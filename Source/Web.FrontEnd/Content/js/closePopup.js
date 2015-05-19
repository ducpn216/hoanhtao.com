FOSP_XD = {
    addListen: function (c, b, a) {
        if (b.addEventListener)
            b.addEventListener(c, a, !1);
        else if (b.attachEvent)
            return b.attachEvent("on" + c, a);
        else
            window.alert("set event failed!")
    }
};
FOSP_XD.closeSignup = function () {
    try {
        window.opener.location.reload(!0);
        self.close();
    } catch (e) { }
    typeof self.close === "function" && self.close()
};
FOSP_XD.closeUpdateAccount = function () {
    try {
        self.close();
    } catch (e) { }
    typeof self.close === "function" && self.close()
};
