using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MvcCms.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/vendor/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/vendor/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/extras").Include(
                        "~/vendor/extras/extras.css"));

//#if DEBUG
//            BundleTable.EnableOptimizations = false;
//#else
//        BundleTable.EnableOptimizations = true;
//#endif
        }
    }
}