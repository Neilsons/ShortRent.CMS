$(function () {
    $('#Color').colorpicker();
});

function btn_edit(v1) {
    var v = $(v1).find("i").attr("class");
    $("#ClassIcons").val(v);
    $("#ClassIcons").focus();
    $("#IconModel").modal("hide");
}
function ManagerBegin() {
    if (!($("select[name='pid'] option:Selected").data("menu")=="True")) {
        layer.alert("父级列表请选择菜单");
        return false;
    }
    $("input[type='submit']").prop("disabled", true);
    return true;
}
function ManagerSuccess(data) {
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