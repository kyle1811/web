using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PT.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{pParam}",
                defaults: new { controller = "Account", action = "Login", pParam = UrlParameter.Optional },
                namespaces: new[] { "PT.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Page",
                url: "Page/{controller}/{action}/{id}",
                defaults: new { controller = "Page", action = "Index", pParam = UrlParameter.Optional },
                namespaces: new[] { "PT.Web.Controllers" }
            );

            routes.MapRoute(
                name: "File",
                url: "File/{controller}/{action}/{id}",
                defaults: new { controller = "File", action = "Index", pParam = UrlParameter.Optional },
                namespaces: new[] { "PT.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Name",
                url: "Name/{controller}/{action}/{id}",
                defaults: new { controller = "Name", action = "Index", pParam = UrlParameter.Optional },
                namespaces: new[] { "PT.Web.Controllers" }
            );
        }
    }
}