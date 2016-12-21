using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mikro
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "UserProfile",
               "user/{id}",
               new { controller = "UserProfile", action = "Index", id = UrlParameter.Optional }
               );

            routes.MapRoute(
                "EditPost",
                "editpost/{id}",
                new { controller = "Post", action = "Post", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                "Post",
                "post/{id}",
                new {controller = "Post", action = "Post", id = UrlParameter.Optional}
                )
            ;

            routes.MapRoute(
                "Tag",
                "tag/{id}",
                new { controller = "Tag", action = "DisplayTagContent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
