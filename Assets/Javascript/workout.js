$(document).ready(function () {
    function collapse(activity, data) {
        var heading = activity.find("h3");
        var details = activity.children(".details");

        activity.find("h3").text(activity.find("h3").text() + " (Complete)");

        activity.css("background", "#FFFFFF");

        activity.animate({
            color: "#555",
            backgroundColor: "#212121"
        }, 1000);

        details.animate({
            opacity: 0,
            height: 0
        }, 1000, function () {
            $(this).css("display", "none");

            if (data == "workout complete") {
                location.reload(true);
            }
        });
    }

    $(".success").click(function () {
        var activity = $(this).parents(".activity");
        var form = $(this).siblings("form");
        form.children("input[name='isFailure']").val("False");

        $.post(completeActivityUrl, form.serialize(), function (data) {
            collapse(activity, data);
        });

        return false;
    });

    $(".failure").click(function () {
        var activity = $(this).parents(".activity");
        var form = $(this).siblings("form");
        form.children("input[name='isFailure']").val("True");

        $.post(completeActivityUrl, form.serialize(), function (data) {
            collapse(activity, data);
        });

        return false;
    });
});