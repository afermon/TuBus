using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/Views/ControlActions.js",
                        "~/Scripts/Views/Auth.js"));

            bundles.Add(new ScriptBundle("~/Scripts/clean").Include(
                "~/Scripts/jquery.min.js",
                "~/Scripts/Views/ControlActionsWithoutAuth.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js-body").Include(
                "~/Scripts/popper.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery.easing.min.js",
                "~/Scripts/tubus.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/tubus.css"));

            bundles.Add(new ScriptBundle("~/Scripts/datatable").Include(
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap4.min.js"));

            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                "~/Content/dataTables.bootstrap4.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/form").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/tempusdominus.min.js"));

            bundles.Add(new StyleBundle("~/Content/form").Include(
                "~/Content/tempusdominus.min.css"));
        }
    }
}
