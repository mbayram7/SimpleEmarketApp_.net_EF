using EmarketDreamsBytes.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmarketDreamsBytes.Controllers
{
    public class HomeController : MarketControllerBase
    {

        MarketDB db = new MarketDB();

        public ActionResult Index()
        {
            ViewBag.IsLogin = this.IsLogin;
            var data = db.Products.ToList();
            return View(data);
        }


        [Route("User-Login")]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("User-Login")]
        public ActionResult Login(string Username, string Userpassword)
        {
            
            var users = db.Users.Where(x => x.UserName == Username && x.UserPassword == Userpassword && x.IsAdmin == false).ToList();

            if(users.Count == 1)
            {
                //başarılı giriş
                Session["LoginUserID"] = users.FirstOrDefault().UserId;
                Session["LoginUser"] = users.FirstOrDefault();
                return Redirect("~/Home/Index");
            }
            else
            {
                var data = db.Users.Where(x => x.UserName == Username && x.UserPassword == Userpassword && x.IsAdmin == true).ToList();

                if (data.Count == 1)
                {
                    //doğru giriş
                    Session["AdminLoginUser"] = data.FirstOrDefault();
                    return Redirect("/Admin");
                }
   
            }
            ViewBag.Error = "wrong login";
            return View();
        }


        [Route("User-Register")]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Route("User-Register")]
        public ActionResult CreateUser(User entity)
        {
            try
            {
                entity.IsAdmin = false;

                db.Users.Add(entity);
                db.SaveChanges();
                return Redirect("/"); //anasayfaya git
            }
            catch (Exception ex)
            {

                return View();
            }
        }


        [Route("Logout")]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("/");
        }
    }
}