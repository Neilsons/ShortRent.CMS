function BeginContact() {
    $("input[type='submit']").prop("disabled", true);
}
function SuccessContact(data) {
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
$(function () {
    $("input[type='submit']").click(function () {
        Pace.restart();
    });
})