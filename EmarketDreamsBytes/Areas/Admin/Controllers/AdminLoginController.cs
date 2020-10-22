using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmarketDreamsBytes.Controllers
{
    public class AdminLoginController : Controller
    {
        MarketDB db = new MarketDB();

    
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Name, string Password)
        {
            var data = db.Users.Where(x => x.UserName == Name && x.UserPassword == Password && x.IsAdmin == true).ToList();

            if(data.Count == 1)
            {
                //doğru giriş
                Session["AdminLoginUser"] = data.FirstOrDefault();
                return Redirect("/Admin");
            }
            else
            {
                return View();
            }
        }
    }
}