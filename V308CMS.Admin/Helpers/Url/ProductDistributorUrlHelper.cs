﻿using System.Web.Mvc;

namespace V308CMS.Admin.Helpers.Url
{
    public static class ProductDistributorUrlHelper
    {
        public static string ProductDistributorIndexUrl(this UrlHelper helper, object routeValue = null, string controller = "productdistributor",
         string action = "index")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductDistributorCreateUrl(this UrlHelper helper, object routeValue = null, string controller = "productdistributor",
         string action = "create")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductDistributorEditUrl(this UrlHelper helper, object routeValue = null, string controller = "productdistributor",
        string action = "edit")
        {
            return helper.Action(action, controller, routeValue);
        }
        public static string ProductDistributorDeleteUrl(this UrlHelper helper, object routeValue = null, string controller = "productdistributor",
       string action = "delete")
        {
            return helper.Action(action, controller, routeValue);
        }

    }
}