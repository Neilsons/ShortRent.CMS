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
$(function () {
    $(document).on("click", "input[type='button']", function () {
        if (!$("#frmSave").valid()) {
            return false;
        }
        $("#frmSave").ajaxSubmit({
            dataType: 'json',
            beforeSubmit: function () {
                if ($("#hidName")) {
                    if ($("#Name").val() == $("#hidName").val()
                        && $("#Position").val() == $("#hidPosition").val()
                        && $("#Qq").val() == $("#hidQq").val()
                        && $("#WeChat").val() == $("#hidWeChat").val()
                        && $("#PersonDetail").val() == $("#hidPersonDetail").val()
                        && $("#PerImage").val() == $("#headPhoto").attr("src")) {
                        layer.alert("请修改后再提交！");
                        return false;
                    }
                }
                var file = $("input[type='file']").val();
                if (file != "") {
                    //允许上传的文件类型 
                    var typeAllow = ["gif", "jpg", "png"];
                    //获取上传文件类型 
                    var fileType = (file.substring(file.lastIndexOf(".") + 1, file.length)).toLowerCase();
                    //判断是否存在于配置数组
                    if ($.inArray(fileType, typeAllow) < 0) {
                        layer.alert("请传入正确的文件格式，以下为正确后缀：\"gif\",\"jpg\", \"png\" ");
                        return false;
                    }
                }
                else {
                    layer.alert("请上传头像");
                    return false;
                }
                $("input[type='button']").prop("disabled", true);
            },
            success: function (data) {
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
        });
    });
});
