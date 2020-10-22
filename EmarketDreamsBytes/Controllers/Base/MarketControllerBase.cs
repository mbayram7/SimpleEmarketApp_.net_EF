using EmarketDreamsBytes.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmarketDreamsBytes
{
    public class MarketControllerBase : Controller
    {
        public bool IsLogin { get; private set; }

        public int LoginUserID { get; private set; } //giriş yapan user id'si

        public User LoginUserEntity { get; private set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if(requestContext.HttpContext.Session["LoginUserID"] != null)
            {
                IsLogin = true;
                LoginUserID = (int)requestContext.HttpContext.Session["LoginUserID"];
                LoginUserEntity = (EmarketDreamsBytes.Entity.User)requestContext.HttpContext.Session["LoginUser"];
            }
            base.Initialize(requestContext);
        }
    }
}