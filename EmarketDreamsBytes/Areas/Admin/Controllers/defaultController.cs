using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmarketDreamsBytes.Areas.Admin.Controllers
{
    public class defaultController : AdminControllerBase
    {
        // GET: Admin/default
        public ActionResult Index()
        {
            return View();
        }
    }
}