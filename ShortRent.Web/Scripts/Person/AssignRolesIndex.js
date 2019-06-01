$(function () {
    $("input[type='checkbox']").iCheck({
        cursor: true,
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue'
    });
});
function SuccessAssign(data) {
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