function BeginCreateIntro() {
    var hidQuestionMsg = $("#hidQuestionMsg").val();
    var hidType = $("#hidType").val();
    var questionMsg = $("#QuestionMsg").val();
    var type = $("#Type").val();
    if (hidQuestionMsg) {
        if (hidQuestionMsg == questionMsg.trim() && type.trim() == hidType.trim()) {
            layer.alert("未发生改变，请修改后再提交");
            return false;
        }
    }
    $("input[type='submit']").prop("disabled", true);

}
function SucessCreateIntro(data) {
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

