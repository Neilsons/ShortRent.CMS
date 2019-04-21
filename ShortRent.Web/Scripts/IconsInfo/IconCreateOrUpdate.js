$(function () {
    $("input[type='submit']").click(function () {
        Pace.restart();
    });
})
function BeginCreateIcon() {
    $("input[type='submit']").prop("disabled", true);
    return true;
}
function BeginEditIcon() {
    if ($("#hidPrefix").val() == $("#Prefix").val() && $("#hidContent").val() == $("#Content").val()) {
        layer.alert("请修改后提交！");
        return false;
    }
    $("input[type='submit']").prop("disabled", true);
    return true;
}
function SuccessCreateIcon(data) {
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