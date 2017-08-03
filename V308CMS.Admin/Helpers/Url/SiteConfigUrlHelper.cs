using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace V308CMS.Admin.Helpers.Url
{
    public static class SiteConfigUrlHelper
    {
        public static string SiteConfigIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "siteconfig",
             string action = "index")
        {
            switch (HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString())
            {
                case "affiliateCreate":
                    action = "affiliate";
                    break;
            }
            return helper.Action(action, controller, routeValue);
        }
        public static string SiteConfigCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "siteconfig",
           string action = "create")
        {
            switch (HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString())
            {
                case "affiliate":
                    action = "affiliateCreate";
                    break;
            }
            return helper.Action(action, controller, routeValue);
        }
        public static string SiteConfigEditUrl(this UrlHelper helper, object routeValue = null, string controller = "siteconfig",
          string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string SiteConfigDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "siteconfig",
         string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }

        public static string SiteConfigAffiliate(this UrlHelper helper, object routeValue = null, string controller = "siteconfig",
             string action = "affiliate")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}