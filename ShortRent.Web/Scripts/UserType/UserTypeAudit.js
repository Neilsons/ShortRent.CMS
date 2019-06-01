function BeginCreateAudit() {
    if ($("#TypeUser").val() == "0") {
        layer.alert("请选择审核状态");
        return false;
    }
    $("input[type='submit']").prop("disabled", true);
}
function SuccessCreateAudit(data) {
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
function InitDateInput(value) {
    laydate.render({
        elem: value
        , format: 'yyyy-MM-dd'
    });
}
$(function () {
    $("input[type='submit']").click(function () {
        Pace.restart();
    });
    //初始化注册时间的输入框
    InitDateInput("#Birthday");
})

