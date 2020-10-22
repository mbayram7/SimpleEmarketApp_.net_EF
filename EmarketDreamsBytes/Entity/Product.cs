using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmarketDreamsBytes.Entity
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public int ProductStock { get; set; }
        public int Price { get; set; }
        
        public virtual List<Basket> Basket { get; set; }
        public virtual List<OrderProduct> OrderProduct { get; set; }



    }
}