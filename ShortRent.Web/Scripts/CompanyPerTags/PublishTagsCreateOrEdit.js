$(function () {
    $('#Color').colorpicker();
    $("input[type='submit']").click(function () {
        Pace.restart();
    });
})
function BeginCreateTags() {
    $("input[type='submit']").prop("disabled", true);
    return true;
}
function BeginEditTags() {
    if ($("#hidName").val() == $("#Name").val()
        && $("#hidColor").val() == $("#Color").val()
        && $("#hidTagOrder").val() == $("#TagOrder").val()) {
        layer.alert("请修改后提交！");
        return false;
    }
    $("input[type='submit']").prop("disabled", true);
    return true;
}
function SuccessCreateTags(data) {
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