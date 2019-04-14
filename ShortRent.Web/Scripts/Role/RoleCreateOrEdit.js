function BeginCreateRole() {
    var hidName = $("#hidName").val();
    var hidType = $("#hidType").val();
    var name = $("#Name").val();
    var type = $("#Type").val();
    if (hidName) {
        if (hidName == name.trim() && type.trim() == hidType) {
            layer.alert("未发生改变，请修改后再提交");
            return false;
        }
    }
    $("input[type='submit']").prop("disabled", true);
  
}
function SuccessCreateRole(data) {
    if (data.httpCodeResult == 200) {
        Pace.on("done", function () {
            layer.alert(data.message,{
                closeBtn: 1    // 是否显示关闭按钮
                ,yes: function () {
                    window.location.href = data.url;
                }
                , cancel: function () {
                    window.location.href = data.url;
                }
               
            });
        })
    }
}
$(function () {
    $("input[type='submit']").click(function () {
        Pace.restart();
    });
})

