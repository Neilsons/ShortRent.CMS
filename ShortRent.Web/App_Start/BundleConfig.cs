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
            bundles.Add(new StyleBundle("~/Styles/BootStrap", "https://cdn.bootcss.com/twitter-bootstrap/3.3.7/css/bootstrap.min.css").Include("~/Content/css/bootstrap.css"));
             //公共的引用CSS结束
            //公共的JS库的引用开始
            bundles.Add(new ScriptBundle("~/Scripts/Jquery", "https://cdn.bootcss.com/jquery/3.3.1/jquery.min.js").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquery-Validate", "https://cdn.bootcss.com/jquery-validate/1.19.0/jquery.validate.min.js").Include("~/Scripts/jquery.validate.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Jquery-Validate-unobtrusive", "https://cdn.bootcss.com/jquery-validation-unobtrusive/3.2.11/jquery.validate.unobtrusive.min.js").Include("~/Scripts/jquery.validate.unobtrusive.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap", "https://cdn.bootcss.com/twitter-bootstrap/3.3.7/js/bootstrap.min.js").Include("~/Scripts/bootstrap*"));
            bundles.Add(new ScriptBundle("~/Scripts/FastClick", "https://cdn.bootcss.com/fastclick/1.0.5/fastclick.min.js").Include("~/Scripts/fastclick.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Adminlte").Include("~/Scripts/AdminMaster/adminlte.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/Demo").Include("~/Scripts/AdminMaster/demo.js"));
            //公共的JS库的引用结束

        }
    }
}
