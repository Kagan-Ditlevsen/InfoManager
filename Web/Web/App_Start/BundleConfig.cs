using System.Web;
using System.Web.Optimization;

namespace dk.infomanager
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /* JavaScript */
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/additional-methods.min.js" // jQuery Validation
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-{version}.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/skd").Include(
                "~/Scripts/SkdWebHelper/Skd.js",
                "~/Scripts/SkdWebHelper/Skd.Form.js",
                "~/Scripts/SkdWebHelper/Skd.JavaScriptExtend.js",
                "~/Scripts/SkdWebHelper/Skd.Menu.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/website").Include(
                "~/Scripts/Website.js"
            ));

            /* CSS */
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap.bundle.min.css",
                "~/Content/fontawesome-5.15.3/all.min.css"
            ));

            bundles.Add(new StyleBundle("~/Content/skd").Include(
                "~/Scripts/SkdWebHelper/Skd.css",
                "~/Scripts/SkdWebHelper/Skd.Form.css",
                "~/Scripts/SkdWebHelper/Skd.Menu.css",
                "~/Scripts/SkdWebHelper/Skd.Table.css"
            ));

            bundles.Add(new StyleBundle("~/Content/website").Include(
                "~/Content/Website.css"
            ));
        }
    }
}
