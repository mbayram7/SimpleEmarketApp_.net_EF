using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmarketDreamsBytes.Entity
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserAdressID { get; set; }
        public decimal TotalPrice { get; set; }
        //public string OProducts { get; set; }

        public User User { get; set; }
        public UserAddress UserAddress { get; set; }
        public Product Product { get; set; }

        public virtual List<OrderProduct> OrderProduct { get; set; }
        public virtual List<OrderPayment> OrderPayment { get; set; }
        


    }
}