using System.Web;
using System.Web.Optimization;

namespace BinTracking
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

            bundles.Add(new ScriptBundle("~/assets/js/js").Include(
                    "~/assets/js/libs/jquery-3.1.1.min.js",
                    "~/assets/js/libs/jquery-3.7.1.min.js",
                    "~/assets/js/app.js",
                    "~/assets/js/darkmode.js",
                    "~/assets/js/loader.js",
                    "~/assets/js/toastr.min.js",
                    "~/assets/js/inputvalidations.js"));


            bundles.Add(new ScriptBundle("~/Layout/datatable/js").Include(
                "~/plugins/table/datatable/datatables.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Login/css").Include(
                      "~/bootstrap5/css/bootstrap.min.css",
                      "~/assets/css/main.css",
                      "~/assets/css/structure.css",
                      "~/line-awesome-1.3.0/1.3.0/css/line-awesome.min.css",
                      "~/assets/css/login.css",
                      "~/bootstrap/css/bootstrap.min.css",
                      "~/assets/css/main.css",
                      "~/assets/css/structure.css",
                      "~/assets/css/toastr.min.css",
                      "~/assets/css/loader.css"));

            bundles.Add(new StyleBundle("~/Layout/css/css").Include(
                    "~/assets/css/poppins_font.css",
                    "~/bootstrap5/css/bootstrap.min.css",
                    "~/assets/css/main.css",
                    "~/assets/css/structure.css",
                    "~/assets/css/elements/tooltip.css",
                    "~/line-awesome-1.3.0/1.3.0/css/line-awesome.min.css",
                    "~/assets/css/forms/radio-theme.css",
                    "~/plugins/table/datatable/datatables.css",
                    "~/plugins/table/datatable/dt-global_style.css",
                    "~/assets/css/toastr.min.css",
                    "~/assets/css/loader.css"));
        }
    }
}
