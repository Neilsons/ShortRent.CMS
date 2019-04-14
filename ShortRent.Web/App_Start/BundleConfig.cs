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
            //异步加载进度指示条
            bundles.Add(new StyleBundle("~/Styles/Pace").Include("~/Content/pace/pace.min.css"));
             //公共的引用CSS结束
            //公共的JS库的引用开始
            bundles.Add(new ScriptBundle("~/Scripts/Jquery", "https://cdn.bootcss.com/jquery/3.3.1/jquery.min.js").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquery-Validate", "https://cdn.bootcss.com/jquery-validate/1.19.0/jquery.validate.min.js").Include("~/Scripts/jquery.validate.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquery-Validate-unobtrusive", "https://cdn.bootcss.com/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js").Include("~/Scripts/jquery.validate.unobtrusive.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquery-Ajax-unobtrusive").Include("~/Scripts/jquery.unobtrusive-ajax.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap", "https://cdn.bootcss.com/twitter-bootstrap/3.3.7/js/bootstrap.min.js").Include("~/Scripts/bootstrap*"));
            bundles.Add(new ScriptBundle("~/Scripts/FastClick", "https://cdn.bootcss.com/fastclick/1.0.5/fastclick.min.js").Include("~/Scripts/fastclick.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Adminlte").Include("~/Scripts/AdminMaster/adminlte.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Demo").Include("~/Scripts/AdminMaster/demo.js"));
            bundles.Add(new ScriptBundle("~/Scripts/BootstrapTable", "https://cdn.bootcss.com/bootstrap-table/1.14.2/bootstrap-table.js").Include("~/Scripts/bootstrap-table.js"));
            bundles.Add(new ScriptBundle("~/Scripts/BootstrapTable_zh","https://cdn.bootcss.com/bootstrap-table/1.14.2/locale/bootstrap-table-zh-CN.js").Include("~/Scripts/locale/bootstrap-table-zh-CN.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ValidationExtention-Bootstrap").Include("~/Scripts/extensions/jquery.unobtrusive.table.js"));
            bundles.Add(new ScriptBundle("~/Scripts/LayAlert").Include("~/Content/layer/layer.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Pace").Include("~/Content/pace/pace.min.js"));
            //公共的JS库的引用结束

            //role用到的js 自定义的开始
            bundles.Add(new ScriptBundle("~/Scripts/RoleIndex").Include("~/Scripts/Role/RoleIndex.js"));
            bundles.Add(new ScriptBundle("~/Scripts/RoleCreateOrEdit").Include("~/Scripts/Role/RoleCreateOrEdit.js"));
            //表格导出
            bundles.Add(new ScriptBundle("~/Scripts/BootstrapTableExport", "https://cdn.bootcss.com/bootstrap-table/1.14.2/extensions/export/bootstrap-table-export.js").Include("~/Scripts/extensions/export/bootstrap-table-export.js"));
            bundles.Add(new ScriptBundle("~/Scripts/TableExport").Include("~/Scripts/tableexport-1.10.3.js"));
        }
    }
}
