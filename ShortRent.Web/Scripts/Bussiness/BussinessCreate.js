$(function () {
    $("input[type='submit']").click(function () {
        Pace.restart();
    });
})
function BeginCreateBussiness() {
    $("input[type='submit']").prop("disabled", true);
    return true;
}
function BeginEditBussiness() {
    if ($("#hidName").val() == $("#Name").val()) {
        layer.alert("请修改后提交！");
        return false;
    }
    $("input[type='submit']").prop("disabled", true);
    return true;
}
function SuccessCreateBussiness(data) {
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