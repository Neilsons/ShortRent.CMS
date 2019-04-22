function ShowImage() {
    var preview = document.getElementById("headPhoto");
    var file = document.querySelector('input[type=file]').files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }
    if (file) {
        reader.readAsDataURL(file);
    }
}
function SuccessEditPerson(data) {
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
    $(document).on("click", "input[type='button']", function () {
        $("#frmSave").ajaxSubmit({
            dataType: 'json',
            beforeSend: function (xhr) {
                if ($("#Name").val() == $("#hidName").val()
                    && $("#Position").val() == $("#hidPosition").val()
                    && $("#Qq").val() == $("#hidQQ").val()
                    && $("#WeChat").val() == $("#hidWeChat").val()
                    && $("#PersonDetail").val() == $("#hidDetail").val()
                    && $("#hidImage").val() == $("#headPhoto").attr("src")) {
                    layer.alert("请修改后再提交！");
                    return;
                }
                $("input[type='button']").prop("disabled", true);
            },
            success: function (data) {
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
        });

    });
});
