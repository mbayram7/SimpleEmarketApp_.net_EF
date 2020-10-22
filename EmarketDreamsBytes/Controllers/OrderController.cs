using EmarketDreamsBytes.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmarketDreamsBytes.Controllers
{
    public class OrderController : MarketControllerBase
    {

        [Route("Orders")]
        public ActionResult Index()
        {
            var db = new MarketDB();
            var data = db.Orders.Where(x => x.UserId == LoginUserID).ToList();
            return View(data);

        }


        public ActionResult AddressList()
        {
            var db = new MarketDB();
            var data = db.UserAddresses.Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }



        public ActionResult CreateAddress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAddress(UserAddress entity)
        {
            entity.UserID = LoginUserID;
            var db = new MarketDB();
            db.UserAddresses.Add(entity);
            db.SaveChanges();
            return RedirectToAction("/AddressList");
        }

        public ActionResult CreateOrder(int id)
        {
            var db = new MarketDB();

            var basket = db.Baskets.Include("Product").Where(x => x.UserId == LoginUserID).ToList();
            Order order = new Order();

            order.TotalPrice = basket.Sum(x => x.Product.Price);
            order.UserAdressID = id;
            order.UserId = LoginUserID;

            order.OrderProduct = new List<OrderProduct>();

            foreach (var item in basket)
            {
                order.OrderProduct.Add(new OrderProduct
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity

                });

                db.Baskets.Remove(item);

            }

            db.Orders.Add(order);
            db.SaveChanges();
            var orderid = db.Orders.Where(x => x.UserId == LoginUserID).AsEnumerable().LastOrDefault().OrderId;
            return RedirectToAction("OrderDone", new { id = orderid });
        }

        public ActionResult Detail(int id)
        {
            var db = new MarketDB();
            var data = db.Orders.Include("OrderProduct")
                .Include("OrderProduct.Product")
                .Include("OrderPayment")
                .Include("UserAddress")
                .Where(x => x.OrderId == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult DeleteAddress(int id)
        {
            var db = new MarketDB();
            var deleteitem = db.UserAddresses.Where(x => x.UserAdressID == id).FirstOrDefault();
            db.UserAddresses.Remove(deleteitem);
            db.SaveChanges();
            return RedirectToAction("/AddressList");
        }

        //public ActionResult Pay(int id)
        //{
        //    var db = new MarketDB();
        //    var order = db.Orders.Where(x => x.OrderId == id).FirstOrDefault();
        //    db.SaveChanges();
        //    return View("Pay", new { id = order.OrderId });
        //}

        public ActionResult OrderDone(int id)
        {
            var db = new MarketDB();
            var order = db.Orders.AsEnumerable().LastOrDefault();
            db.SaveChanges();
            return View(order);

        }





    }
}