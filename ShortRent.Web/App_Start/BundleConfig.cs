using System.Web;
using System.Web.Optimization;

namespace ShortRent.Web.App_Start
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //公共的引用CSS开始
            bundles.Add(new StyleBundle("~/Styles/AdminLTE").Include("~/Content/css/AdminLTE.min.css"));
            bundles.Add(new StyleBundle("~/Styles/AllSkins").Include("~/Content/css/_all-skins.min.css"));
            bundles.Add(new StyleBundle("~/Styles/FontAwesome").Include("~/Content/css/font-awesome.css"));
            bundles.Add(new StyleBundle("~/Styles/IonIcons").Include("~/Content/css/ionicons.css"));
            bundles.Add(new StyleBundle("~/Styles/AdminCore").Include("~/Content/css/AdminCore.css"));
            bundles.Add(new StyleBundle("~/Styles/BootStrap", "https://cdn.bootcss.com/twitter-bootstrap/3.3.7/css/bootstrap.min.css").Include("~/Content/css/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Styles/BootstraTable", "https://cdn.bootcss.com/bootstrap-table/1.14.2/bootstrap-table.css").Include("~/Content/css/bootstrap-table.css"));
            bundles.Add(new StyleBundle("~/Styles/LayAlert").Include("~/Content/layer/theme/default/layer.css"));
            bundles.Add(new StyleBundle("~/Styles/LayerDate").Include("~/Content/laydate/theme/default/laydate.css"));
            bundles.Add(new StyleBundle("~/Styles/jqueryTreeGrid").Include("~/Content/bootstrapTableTreeGrid/jquery.treegrid.min.css"));
            bundles.Add(new StyleBundle("~/Styles/ColorPicker").Include("~/Content/bootstrapColorPicker/css/bootstrap-colorpicker.min.css"));
            bundles.Add(new StyleBundle("~/Styles/IcheckSkins").Include("~/Content/Icheck/skins/square/_all.css"));
            //异步加载进度指示条
            bundles.Add(new StyleBundle("~/Styles/Pace").Include("~/Content/pace/pace.min.css"));
            //日志详情的自定义css
            bundles.Add(new StyleBundle("~/Styles/LogDetail").Include("~/Content/css/LogInfo/LogDetail.css"));
            //历史操作详情的自定义css
            bundles.Add(new StyleBundle("~/Styles/HistoryDetail").Include("~/Content/css/History/HistoryDetail.css"));
            //登陆自定义css
            bundles.Add(new StyleBundle("~/Styles/animat").Include("~/Content/Login/animate.css"));
            bundles.Add(new StyleBundle("~/Styles/login").Include("~/Content/Login/login.css"));
            bundles.Add(new StyleBundle("~/Styles/Loginstyle").Include("~/Content/Login/style.css"));
            //个人资料自定义css
            bundles.Add(new StyleBundle("~/Styles/PersonData").Include("~/Content/css/Person/PersonData.css"));
            //公共的引用CSS结束
            //公共的JS库的引用开始
            bundles.Add(new ScriptBundle("~/Scripts/Jquery", "https://cdn.bootcss.com/jquery/3.3.1/jquery.min.js").Include("~/Scripts/jquery-3.3.1.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquery-Validate", "https://cdn.bootcss.com/jquery-validate/1.19.0/jquery.validate.min.js").Include("~/Scripts/jquery.validate.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquery-Validate-unobtrusive", "https://cdn.bootcss.com/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js").Include("~/Scripts/jquery.validate.unobtrusive.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquery-Ajax-unobtrusive").Include("~/Scripts/jquery.unobtrusive-ajax.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquery-Form").Include("~/Scripts/jquery.form.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap", "https://cdn.bootcss.com/twitter-bootstrap/3.3.7/js/bootstrap.min.js").Include("~/Scripts/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/FastClick", "https://cdn.bootcss.com/fastclick/1.0.5/fastclick.min.js").Include("~/Scripts/fastclick.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Adminlte").Include("~/Scripts/AdminMaster/adminlte.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Demo").Include("~/Scripts/AdminMaster/demo.js"));
            bundles.Add(new ScriptBundle("~/Scripts/BootstrapTable", "https://cdn.bootcss.com/bootstrap-table/1.14.2/bootstrap-table.js").Include("~/Scripts/bootstrap-table.js"));
            bundles.Add(new ScriptBundle("~/Scripts/BootstrapTable_zh","https://cdn.bootcss.com/bootstrap-table/1.14.2/locale/bootstrap-table-zh-CN.js").Include("~/Scripts/locale/bootstrap-table-zh-CN.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ValidationExtention-Bootstrap").Include("~/Scripts/extensions/jquery.unobtrusive.table.js"));
            bundles.Add(new ScriptBundle("~/Scripts/LayAlert").Include("~/Content/layer/layer.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Pace").Include("~/Content/pace/pace.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/LayDate").Include("~/Content/laydate/laydate.js"));
            bundles.Add(new ScriptBundle("~/Scripts/bootstrapTableTreeGrid").Include("~/Content/bootstrapTableTreeGrid/bootstrap-table-treegrid.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquerytreegrid").Include("~/Content/bootstrapTableTreeGrid/jquery.treegrid.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ColorPicker").Include("~/Content/bootstrapColorPicker/js/bootstrap-colorpicker.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Icheck").Include("~/Content/Icheck/icheck.min.js"));
            //公共的JS库的引用结束

            //role用到的js 自定义的开始
            bundles.Add(new ScriptBundle("~/Scripts/RoleIndex").Include("~/Scripts/Role/RoleIndex.js"));
            bundles.Add(new ScriptBundle("~/Scripts/RoleCreateOrEdit").Include("~/Scripts/Role/RoleCreateOrEdit.js"));
            bundles.Add(new ScriptBundle("~/Scripts/PrivilegesIndex").Include("~/Scripts/Role/PrivilegesIndex.js"));
            //表格导出
            bundles.Add(new ScriptBundle("~/Scripts/BootstrapTableExport", "https://cdn.bootcss.com/bootstrap-table/1.14.2/extensions/export/bootstrap-table-export.js").Include("~/Scripts/extensions/export/bootstrap-table-export.js"));
            bundles.Add(new ScriptBundle("~/Scripts/TableExport").Include("~/Scripts/tableexport-1.10.3.js"));
            //loginfo用到的js库自定义的开始
            bundles.Add(new ScriptBundle("~/Scripts/LogInfoIndex").Include("~/Scripts/LogInfo/LogIndex.js"));
            //manager用到的js库自定义开始
            bundles.Add(new ScriptBundle("~/Scripts/ManagerIndex").Include("~/Scripts/Manager/ManagerIndex.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ManagerCreateOrEdit").Include("~/Scripts/Manager/ManagerCreateOrEdit.js"));
            //IconInfo用到的js自定义开始
            bundles.Add(new ScriptBundle("~/Scripts/IconIndex").Include("~/Scripts/IconsInfo/IconInfo.js"));
            bundles.Add(new ScriptBundle("~/Scripts/IconCreate").Include("~/Scripts/IconsInfo/IconCreateOrUpdate.js"));
            //历史操作要引用的js自定义开始
            bundles.Add(new ScriptBundle("~/Scripts/HistoryIndex").Include("~/Scripts/HistoryOperator/HistoryIndex.js"));
            //用户登陆要引用的js自定义开始
            bundles.Add(new ScriptBundle("~/Scripts/PersonLogin").Include("~/Scripts/Person/PersonLogin.js"));
            //后台用户编辑用到的js
            bundles.Add(new ScriptBundle("~/Scripts/AdminEdit").Include("~/Scripts/Person/AdminEdit.js"));
            //后台 用户列表用到的js
            bundles.Add(new ScriptBundle("~/Scripts/AdminIndex").Include("~/Scripts/Person/PersonIndex.js"));
            bundles.Add(new ScriptBundle("~/Scripts/AssignRolesIndex").Include("~/Scripts/Person/AssignRolesIndex.js"));
            //后台 修改用户密码用到的js
            bundles.Add(new ScriptBundle("~/Scripts/PersonEditPassWord").Include("~/Scripts/Person/EditPersonPassWord.js"));
            //后台 用户创建或者编辑用到的js
            bundles.Add(new ScriptBundle("~/Scripts/PersonCreateOrEdit").Include("~/Scripts/Person/PersonCreateOrEdit.js"));

            //后台 用户或公司介绍问题列表用到的js
            bundles.Add(new ScriptBundle("~/Scripts/PerOrComIntroGuidanceIndex").Include("~/Scripts/PerOrComIntroGuidance/PerOrComIntroGuidanceIndex.js"));
            bundles.Add(new ScriptBundle("~/Scripts/PerOrComIntroGuidanceCreateOrEdit").Include("~/Scripts/PerOrComIntroGuidance/PerOrComIntroGuidanceCreteOrEdit.js"));

            //后台 发布内容的标签列表
            bundles.Add(new ScriptBundle("~/Scripts/PublishTags").Include("~/Scripts/CompanyPerTags/PublishTags.js"));
            bundles.Add(new ScriptBundle("~/Scripts/PublishCreateOrEdit").Include("~/Scripts/CompanyPerTags/PublishTagsCreateOrEdit.js"));

            //后台 公司内容的列表
            bundles.Add(new ScriptBundle("~/Scripts/CompanyIndex").Include("~/Scripts/Company/CompanyIndex.js"));
            bundles.Add(new ScriptBundle("~/Scripts/CompanyAudit").Include("~/Scripts/Company/CompanyAudit.js"));
            //后台 被招聘者列表
            bundles.Add(new ScriptBundle("~/Scripts/UserTypeIndex").Include("~/Scripts/UserType/UserTypeIndex.js"));
            bundles.Add(new ScriptBundle("~/Scripts/UserTypeAudit").Include("~/Scripts/UserType/UserTypeAudit.js"));

            //后台 招聘者列表
            bundles.Add(new ScriptBundle("~/Scripts/ReduitUserType").Include("~/Scripts/UserType/ReduitUserType.js"));

            //后台 联系我们
            bundles.Add(new ScriptBundle("~/Scripts/ContactIndex").Include("~/Scripts/Contact/ContactIndex.js"));

            //后台 行业
            bundles.Add(new ScriptBundle("~/Scritps/BussinessIndex").Include("~/Scripts/Bussiness/BussinessIndex.js"));
            bundles.Add(new ScriptBundle("~/Scripts/BussinessCreate").Include("~/Scripts/Bussiness/BussinessCreate.js"));

            //后台 发布消息
            bundles.Add(new ScriptBundle("~/Scripts/RecruiterByListIndex").Include("~/Scripts/PublishMsg/RecruiterByListIndex.js"));
            bundles.Add(new ScriptBundle("~/Scripts/RecruiterListIndex").Include("~/Scripts/PublishMsg/RecruiterListIndex.js"));



            //jqueryUpload上传插件的绑定
            bundles.Add(new StyleBundle("~/Content/jQuery-File-Upload").Include(
                 "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
                "~/Content/jQuery.FileUpload/css/jquery.fileupload-ui.css"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jQuery-File-Upload").Include(
                   //<!-- The Templates plugin is included to render the upload/download listings -->
                   "~/Content/jQuery.FileUpload/vendor/jquery.ui.widget.js",
                     "~/Content/jQuery.FileUpload/tmpl.min.js",
                    //<!-- The Load Image plugin is included for the preview images and image resizing functionality -->
                    "~/Content/jQuery.FileUpload/load-image.all.min.js",
                    //<!-- The Canvas to Blob plugin is included for image resizing functionality -->
                    "~/Content/jQuery.FileUpload/canvas-to-blob.min.js",
                    //"~/Content/file-upload/jquery.blueimp-gallery.min.js",
                    //<!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
                    "~/Content/jQuery.FileUpload/jquery.iframe-transport.js",
                    //<!-- The basic File Upload plugin -->
                    "~/Content/jQuery.FileUpload/jquery.fileupload.js",
                    //<!-- The File Upload processing plugin -->
                    "~/Content/jQuery.FileUpload/jquery.fileupload-process.js",
                    //<!-- The File Upload image preview & resize plugin -->
                    "~/Content/jQuery.FileUpload/jquery.fileupload-image.js",
                    //<!-- The File Upload validation plugin -->
                    "~/Content/jQuery.FileUpload/jquery.fileupload-validate.js",
                    //!-- The File Upload user interface plugin -->
                    "~/Content/jQuery.FileUpload/jquery.fileupload-ui.js"
            ));













            //前台要捆绑的css
            bundles.Add(new StyleBundle("~/Content/Foreground/Custom").Include("~/Content/foreground/css/custom.css"));
            bundles.Add(new StyleBundle("~/Content/Foreground/bootstrap").Include("~/Content/foreground/css/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/Foreground/Color").Include("~/Content/foreground/css/color.css"));
            bundles.Add(new StyleBundle("~/Content/Foreground/Responsive").Include("~/Content/foreground/css/responsive.css"));
            bundles.Add(new StyleBundle("~/Content/Foreground/Carousel").Include("~/Content/foreground/css/owl.carousel.css"));
            bundles.Add(new StyleBundle("~/Content/Foreground/Font-Awesome").Include("~/Content/foreground/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/Content/Foreground/MCustomScrollbar").Include("~/Content/foreground/css/jquery.mCustomScrollbar.css"));
            bundles.Add(new StyleBundle("~/Content/Foreground/FontGoogle", "https://fonts.googleapis.com/css?family=Roboto:400,300,300italic,500,700,900"));
            //登录引用的表单想到css
            bundles.Add(new StyleBundle("~/Content/Foreground/JquerySteps").Include("~/Content/foreground/css/jquery.steps.css"));

            //前台要捆绑的js
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/Jquery").Include("~/Content/foreground/js/jquery-1.11.3.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/BootStrap").Include("~/Content/foreground/js/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/Carousel").Include("~/Content/foreground/js/owl.carousel.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/Jquery-Velocity").Include("~/Content/foreground/js/jquery.velocity.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/Jquery-Kenburnsy").Include("~/Content/foreground/js/jquery.kenburnsy.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/Jquery-CustomScrollbar").Include("~/Content/foreground/js/jquery.mCustomScrollbar.concat.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/Custom").Include("~/Content/foreground/js/custom.js"));
            //登陆使用的js
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/Theme-Scripts").Include("~/Content/foreground/js/theme-scripts.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/Jquery-Noconflict").Include("~/Content/foreground/js/jquery.noconflict.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/WebLogin").Include("~/Content/foreground/js/Login/WebLogin.js"));
            //注册使用的js 
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/JquerySteps").Include("~/Content/foreground/js/jquery.steps.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/WebRegister").Include("~/Content/foreground/js/Login/WebRegister.js"));

            //联系我们使用的js
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/WebContact").Include("~/Content/foreground/js/Web/WebContact.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Foreground/WebPublishMsgCreate").Include("~/Content/foreground/js/Web/WebPublishMsgCreate.js"));


            //富文本编辑器的用法
            bundles.Add(new ScriptBundle("~/Scripts/wangEditor").Include("~/Content/wangEditor-3.1.1/release/wangEditor.min.js"));
        }
    }
}
