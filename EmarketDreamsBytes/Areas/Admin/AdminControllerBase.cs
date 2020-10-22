using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmarketDreamsBytes.Areas.Admin
{
    public class AdminControllerBase : Controller
    {

        protected override void Initialize(RequestContext requestContext)
        {
            var IsLogin = false;

            if(requestContext.HttpContext.Session["AdminLoginUser"] == null)
            {
                //admin girişi yapılmadı
                requestContext.HttpContext.Response.Redirect("AdminLogin/Index");
            }
            else
            {
                //admin giriş yaptı, sayfayı çalıştır
                base.Initialize(requestContext);
            }
        }
    }
}