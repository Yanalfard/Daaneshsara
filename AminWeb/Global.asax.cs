using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AminWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            HttpContext.Current.Application["Online"] = 0;
        }

        protected void Session_Start()
        {
            int online = int.Parse(HttpContext.Current.Application["Online"].ToString());
            online += 1;
            HttpContext.Current.Application["Online"] = online;
            Session["IpOnline"] = online;

            DateTime dtNow = DateTime.Now.Date;
            string ip = Request.UserHostAddress;
            //using (DataLayer.MyEshop_DBEntities db = new DataLayer.MyEshop_DBEntities())
            //{

            //    if (!db.SiteVisit.Any(v => v.IP == ip && v.Date == dtNow))
            //    {
            //        db.SiteVisit.Add(new DataLayer.SiteVisit()
            //        {
            //            Date = DateTime.Now,
            //            IP = ip
            //        });
            //        db.SaveChanges();
            //    }
            //}

        }
    }
}
