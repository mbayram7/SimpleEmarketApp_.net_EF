using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmarketDreamsBytes.Entity
{
    public class UserAddress
    {
        [Key]
        public int UserAdressID { get; set; }
        public int UserID { get; set; }
        public string UAddress { get; set; }

        public User User { get; set; }
        public virtual List<Order> Order { get; set; }
    }
}