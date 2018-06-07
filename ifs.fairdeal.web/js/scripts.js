// navigation slide-in
//$(window).load(function() {
//  $('.nav_slide_button').click(function() {
//    $('.pull').slideToggle();
//  });
//});

var api = {
    callback: null,
    call: function (method, info, onSuccess, onError) {
        $.ajax({
            url: "/services/api.ashx?method=" + method,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: 'POST',
            data: JSON.stringify(info),
            responseType: "json",
            success: function (result) {
                if (typeof (onSuccess) == "function") {
                    result = result.replace(/\n/g, ' ').replace(/\r/g, ' ');
                    data = $.parseJSON('[' + result + ']');
                    onSuccess(data);
                } else
                    services.OnComplete(result);
            },
            error: function (result) {
                if (typeof (onError) == "function") {
                    onError(result)
                } else
                    services.OnFail(result);
            }
        });
    },
    OnComplete: function (result) {
        consolelog($.parseJSON('{' + result + '}'));
        showmessage('Success');
    },
    OnFail: function (result) {
        consolelog($.parseJSON('{' + result + '}'));
        showmessage('Request Failed');
    }
}