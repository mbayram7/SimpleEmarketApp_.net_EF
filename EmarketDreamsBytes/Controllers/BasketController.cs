using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmarketDreamsBytes.Controllers
{
    public class BasketController : MarketControllerBase
    {
        [HttpPost]
        public JsonResult AddProduct(int productId, int quantity)
        {

            var db = new MarketDB();
            db.Baskets.Add(new Entity.Basket
            {
                ProductId = productId,
                Quantity = quantity,
                UserId = LoginUserID
                //ProductName = productName
            });

            var rt =  db.SaveChanges();
            return Json(rt, JsonRequestBehavior.AllowGet);
        }

        [Route("Basket")]
        public ActionResult Index()
        {
            var db = new MarketDB();
            var data = db.Baskets.Include("Product").Where(x => x.UserId == LoginUserID).ToList();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var db = new MarketDB();
            var deleteitem = db.Baskets.Where(x => x.BasketId == id).FirstOrDefault();
            db.Baskets.Remove(deleteitem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}