﻿using System;
using System.Web.Mvc;

namespace V308CMS.Common
{

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(
                            AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || filterContext.HttpContext.Session["UserId"] == null || filterContext.HttpContext.Session["Role"] == null || filterContext.HttpContext.Session["Admin"] == null)
            {
                string loginUrl = "/Home/Login";
                filterContext.Result = new RedirectResult(loginUrl);
            }
        }
    }

    public class AffiliateAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization( AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || filterContext.HttpContext.Session["UserId"] == null || filterContext.HttpContext.Session["Role"] == null )
            {
                string loginUrl = "/dang-nhap";
                filterContext.Result = new RedirectResult(loginUrl);
            }
        }
    }

    public class CustomAuthorize2Attribute : AuthorizeAttribute
    {
        public override void OnAuthorization(
                            AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string loginUrl = "/";
                filterContext.Result = new RedirectResult(loginUrl);
            }
        }
    }
}