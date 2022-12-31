using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "API",
                url: "API/{authId}/{action}",
                defaults: new { controller = "API", action = "Index" }
            );
            // using multi function area defined id's
            routes.MapRoute(
                name: "Economy",
                url: "Economy/{action}/{id}/{view}",
                defaults: new { controller = "Economy", action = "Index", view = "Edit", id = UrlParameter.Optional }
            );

            #region Developer AutoRegister
            routes.MapRoute(
                name: "Developer PoC Layout",
                url: "Developer/PoC/{area}/{view}/{id}",
                defaults: new
                {
                    controller = "Developer",
                    action = "POC",
                    id = UrlParameter.Optional
                }
            );
            routes.MapRoute(
                 name: "Developer ToDo",
                 url: "Developer/ToDo/{area}/{view}/{id}",
                 defaults: new
                 {
                     controller = "Developer",
                     action = "ToDo",
                     id = UrlParameter.Optional
                 }
             );
            routes.MapRoute(
                 name: "Developer WiP",
                 url: "Developer/WiP/{view}/{id}",
                 defaults: new
                 {
                     controller = "Developer",
                     action = "WiP",
                     id = UrlParameter.Optional
                 }
             );
            #endregion

            routes.MapRoute(
                name: "DefaultTwoIds",
                url: "{controller}/{action}/{id}/{idSecond}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, idSecond = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
