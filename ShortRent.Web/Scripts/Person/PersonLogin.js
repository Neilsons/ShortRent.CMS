if (window.top !== window.self) {
    window.top.location = window.location;
}
$(function () {
    $("input[name='ReadMe']").iCheck({
        cursor: true,
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });
    $("button[type='submit']").click(function () {
        Pace.restart();
    });
});
function SuccessLogin(data) {
    if (data.httpCodeResult == 200) {
        layer.alert("<span style='color:#000000;'>" + data.message+"</span>", {
            closeBtn: 1    // 是否显示关闭按钮
            , yes: function () {
                window.location.href = data.url;
            }
            , cancel: function () {
                window.location.href = data.url;
            }

        });
    }
    else if (data.httpCodeResult == 404) {
        layer.alert("<span style='color:#000000;'>" + data.message + "</span>", {
            closeBtn: 1    // 是否显示关闭按钮
            , yes: function () {
                window.location.href = data.url;
            }
            , cancel: function () {
                window.location.href = data.url;
            }

        });
    }
}