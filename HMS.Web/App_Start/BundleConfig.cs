using System.Web;
using System.Web.Optimization;

namespace HMS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/dashboard/css").Include(
                "~/Content/kendo/2019.2.619/kendo.common.min.css",
                "~/Content/kendo/2019.2.619/kendo.flat.min.css",
                "~/Scripts/kendo/2019.2.619/kendo.all.min.js",
                "~/Scripts/kendo/PersianKendo.js",
                "~/Dashboard/dist/css/style.min.css",
                "~/Content/iziModal.min.css",
                "~/Content/iziToast.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/dashboard/js").Include(
                "~/Dashboard/assets/bootstrap.min.js",
                "~/Dashboard/dist/js/app.min.js",
                "~/Dashboard/dist/js/app.init.js",
                "~/Dashboard/dist/js/app-style-switcher.js",
                "~/Dashboard/assets/perfect-scrollbar.jquery.min.js",
                "~/Dashboard/dist/js/waves.js",
                "~/Dashboard/dist/js/sidebarmenu.js",
                "~/Dashboard/dist/js/custom.js",
                "~/Scripts/iziModal.min.js",
                "~/Scripts/iziToast.min.js"

                ));
        }
    }
}
