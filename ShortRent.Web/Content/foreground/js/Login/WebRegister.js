$(document).ready(function () {   
    //被招聘者
    $("#Recruit1").prop("checked","True");
    //初始化出生日期的输入框
    InitDateInput("#Birthday");
    //初始化注册时间的输入框
    InitDateInput("#EstablishTime");
    var form = $("#form");
    form.validate({
        errorPlacement: function errorPlacement(error, element) { element.before(error); },
        rules: {
            Confirm: {
                equalTo: "#PassWord"
            }
        }
    });
    form.children("div").steps({
        headerTag: "h3",
        bodyTag: "section",
        transitionEffect: "slideLeft",
        onStepChanging: function (event, currentIndex, newIndex) {
            form.validate().settings.ignore = ":disabled,:hidden";
            if (currentIndex == 1) {
                if (!($("#Recruit1").prop("checked") == true || $("#Recruit2").prop("checked") == true)) {
                    layer.alert("请选择身份招聘者或者被招聘者");
                    return false;
                }
                else {
                    if ($("#Recruit1").prop("checked") == true) {
                        $(".recruiter").hide();
                        $(".recruiterBy").show();
                    }
                    else {
                        $(".recruiterBy").hide();
                        $(".recruiter").show();
                    }
                }
            }
            if (currentIndex == 2) {
                if ($("#Recruit1").prop("checked") == true) {
                    if (!ifFile("idCardFront", "身份证正面")) {
                        return false;
                    }
                    if (!ifFile("idCardBack", "身份证反面")) {
                        return false;
                    }
                }
                else {
                    if (!ifFile("companyImg", "公司LOGO")) {
                        return false;
                    }
                    if (!ifFile("companyLicense", "营业执照")) {
                        return false;
                    }
                }
            }
            if (currentIndex == 0) {
                var isNext = form.valid();
                $.ajaxSetup({
                    async: false
                });         
                $.get("/ShortWeb/Home/GetJsonByName", { name:$("#Name").val().trim() }, function (data) {
                    if (data.httpCodeResult == 500) {
                        window.location.href = data.url;
                    }
                    else if (data.httpCodeResult == 404) {
                        layer.alert(data.message);
                        isNext = false;
                    }
                });
                if (!ifFile("headPhoto", "个人头像")) {
                    isNext = false;
                };
                return isNext;
            }
            else {
                return form.valid();
            }
        },
        onStepChanged: function (event, currentIndex) {
            if (currentIndex == 3) {
                $("a[href='#finish']").parent("li").show();
            }
            else {
                $("a[href='#finish']").parent("li").hide();
            }
        },
        onFinishing: function (event, currentIndex) {
            form.validate().settings.ignore = ":disabled,:hidden";
            if ($("#PassWord").val() != $("#Confirm").val()) {
                layer.alert("两次密码不一致");
                return false;
            }
            return form.valid();
        },
        onFinished: function (event, currentIndex) {
            $("#form").ajaxSubmit({
                dataType: 'json',
                beforeSubmit: function () {
                    $("a[href='#finish']").parent("li").addClass("disabled");
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
        }
    });
    $("a[href='#finish']").parent("li").hide();
    $("a[href='#cancel']").parent("li").hide();
    $("ul[ role='tablist']").find("li").each(function (index) {
        $(this).addClass("col-xs-12 col-sm-6 col-md-4 col-lg-3");
    });
});
function InitDateInput(value) {
    laydate.render({
        elem: value
        ,format: 'yyyy-MM-dd'
    });
}
function ShowImage(name) {
    var preview = document.getElementById(name);
    var file = document.querySelector('input[name="'+name+'"]').files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }
    if (file) {
        reader.readAsDataURL(file);
    }
}
//参数n为休眠时间，单位为毫秒:
function sleep(n) {
    var start = new Date().getTime();
    //  console.log('休眠前：' + start);
    while (true) {
        if (new Date().getTime() - start > n) {
            break;
        }
    }
    // console.log('休眠后：' + new Date().getTime());
}
function ifFile(name,value) {
    var file = $("input[name='"+name+"']").val();
    if (file != "") {
        //允许上传的文件类型 
        var typeAllow = ["gif", "jpg", "png"];
        //获取上传文件类型 
        var fileType = (file.substring(file.lastIndexOf(".") + 1, file.length)).toLowerCase();
        //判断是否存在于配置数组
        if ($.inArray(fileType, typeAllow) < 0) {
            layer.alert(value + "请传入正确的文件格式，以下为正确后缀：\"gif\",\"jpg\", \"png\" ");
            return false;
        }
        else {
            return true;
        }
    }
    else {
        layer.alert("请上传" + value);
        return false;
    }
}
