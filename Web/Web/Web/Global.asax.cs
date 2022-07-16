using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace InfoMan
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Get system application information
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            Application["systemVersion"] = fvi.FileVersion;
            FileInfo fileInfo = new FileInfo(assembly.Location);
            fileInfo.Refresh();
            Application["systemDateTime"] = fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm");
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
        }

        protected void Session_End(Object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
