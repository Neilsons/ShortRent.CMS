function BeginEdit() {
    var passWord = $("#PassWord").val();
    var confirmWord = $("#ConfirmPassWord").val();
    if (passWord != confirmWord) {
        layer.alert("两次输入的密码不一致！请重新输入");
        return false;
    }
    $("input[type='submit']").prop("disabled", true);
}
function SuccessEdit(data) {
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
$(function () {
    $("input[type='submit']").click(function () {
        Pace.restart();
    });
})

