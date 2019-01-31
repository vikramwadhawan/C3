using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Colligo.O365Product.RM.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",                          
                           "~/Scripts/bootstrap.min.js"
                            //,"~/Scripts/popper.min.js"
                          ));            
            
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                      "~/Scripts/Chart.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                     "~/Scripts/libs/runtime.*",
                     "~/Scripts/libs/polyfills.*",
                     "~/Scripts/libs/vendor.*",
                     "~/Scripts/libs/main.*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/styles.css"
                       ));
        }
    }
}