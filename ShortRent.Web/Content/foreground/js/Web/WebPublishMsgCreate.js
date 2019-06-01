$(function () {
    var E = window.wangEditor;
    var editor = new E('#Editor');
    var $text1 = $('#Detail');
    editor.customConfig.onchange = function (html) {
        // 监控变化，同步更新到 textarea
        $text1.val(html);
    }
    editor.create();
    // 初始化 textarea 的值
    $text1.val(editor.txt.html());
    $("input[type='submit']").click(function () {
        Pace.restart();
    });
});
function SuccessPublishMsg(data) {
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
function BeginPublishMsg() {
    $("input[type='submit']").prop("disabled", true);
}