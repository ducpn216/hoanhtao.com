function AddLoading() {
    var img = $("<div class='bg_loading'><img src='/Content/images/loading.gif' /></div>");
    $("#main-content form").append(img);
}

function RemoveLoading() {
    $(".bg_loading").remove();
}

function SetMessage(message, isValid) {
    var html = "";

    if (isValid) {
        html = "<div class=\"alert alert-success fade in\">" +
                      "<button data-dismiss=\"alert\" class=\"close close-sm\" type=\"button\">" +
                            "<i class=\"icon-remove\"></i>" +
                        "</button>" + message +
                    "</div>";
    }
    else {
        html = "<div class=\"alert alert-block alert-danger fade in\">" +
                      "<button data-dismiss=\"alert\" class=\"close close-sm\" type=\"button\">" +
                            "<i class=\"icon-remove\"></i>" +
                        "</button>" + message +
                    "</div>";
    }

    $("#error_message").html(html);
}

function AddCaptchaToForm(form, UserInput) {
    //$('<input />').attr('type', 'hidden')
    //        .attr('name', "CaptchaId")
    //        .attr('value', CaptchaId)
    //        .appendTo(form);

    //$('<input />').attr('type', 'hidden')
    //    .attr('name', "InstanceId")
    //    .attr('value', InstanceId)
    //    .appendTo(form);

    $('<input />').attr('type', 'hidden')
        .attr('name', "UserInput")
        .attr('value', UserInput)
        .appendTo(form);
}

function Redirect(link) {
    window.location.href = link;
}

function ReloadImage(id, source) {
    var element = document.getElementById(id);
    var date = new Date();
    element.src = source + "?date=" + date;
    return false;
}

function SetDefaultButton(objTextBox, objButton) {
    var ButtonKeys = { 'EnterKey': 13 };
    $(function () {
        $('#' + objTextBox).keypress(function (e) {
            if ((e.which && e.which == ButtonKeys.EnterKey) || (e.keycode && e.keycode == ButtonKeys.EnterKey)) {
                $('#' + objButton).click();
                return false;
            }
        });
        $('#' + objTextBox).focus(function (e) {
            if ((e.which && e.which == ButtonKeys.EnterKey) || (e.keycode && e.keycode == ButtonKeys.EnterKey)) {
                $('#' + objButton).click();
                return false;
            }
        });
    });
}

function ReloadCaptcha() {
    var date = new Date();
    $("#img-captcha").attr("src", "/Captcha?date=" + date);
}

$(document).ready(function () {
    $(".ddlServer").change(function () {
        var accountName = $(".txtAccount").val();
        var serverId = $(this).val();
        var ddlRole = $(".ddlRole");

        $.ajax({
            type: "GET",
            url: "/Game/GetRoleListApi/?accountName=" + accountName + "&serverId=" + serverId,
            dataType: "json",
            beforeSend: function () {
                AddLoading();
            },
            success: function (response) {
                var html = "";

                $.each(response, function (i) {
                    html += "<option value='" + response[i].dwRoleID + "'>" + response[i].szRoleName + "</option>";
                });

                $(ddlRole).html(html);
                RemoveLoading();
            }
        });
    });

    $(".btn-captcha").click(function () {
        ReloadCaptcha();
    });
});