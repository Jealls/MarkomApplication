using System.Web;
using System.Web.Optimization;

namespace MarkomApplication
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
                      "~/Content/font-awesome.css",
                      "~/Content/site.css",
                      "~/Content/modal.css",
                      "~/Content/alert-style.css"
                      ));

            //new style
            bundles.Add(new StyleBundle("~/Content/css-validate").Include(
            "~/Content/validateStyle.css"
            ));

            bundles.Add(new StyleBundle("~/Content/dropdown-autocomplete").Include(
                       "~/Content/autocomplete-dropdown-style.css"
            ));

            //new script
            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                        "~/Scripts/validation/validate.*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
            "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/alert").Include(
           "~/Scripts/alert.option.js"));

            

        }
    }
}
