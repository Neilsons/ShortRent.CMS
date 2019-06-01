$(function () {
    $("input[type='checkbox']").iCheck({
        cursor: true,
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });
    $("input.check-all").on("ifChecked", function () {
        $(this).parents("tr").find("input:checkbox").iCheck("check");
    });
    $("input.check-all").on("ifUnchecked", function () {
        $(this).parents("tr").find("input:checkbox").iCheck("uncheck");
    });
    $("tr").each(function () {
        var cache = true;
        $(this).find("input[name='permissionIds']").each(function () {
            if (!$(this).is(':checked')) {
                cache = false;
            }
        });
        if (cache) {
            $(this).iCheck("check");
        }
    })

});
function SuccessCreateEntity(data) {
    if (data.httpCodeResult == 200) {
        layer.alert(data.message, {
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