$(function () {
    $("input[type='checkbox']").iCheck({
        cursor: true,
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });
    if($("input[type='hidden']").val()=="False")
    {
        $(".custom-select").html("找工作");
        $("select[name='Type']").val("False");
    }
    else
    {
        $(".custom-select").html("招聘");
        $("select[name='Type']").val("True");
    }
    $("input[type='submit']").click(function () {
        Pace.restart();
    });
});
function BeginLogin() {
    $("input[type='submit']").prop("disabled",true)
}
function SuccessLogin(data) {
    if (data.httpCodeResult == 200) {
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