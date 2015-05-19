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
});