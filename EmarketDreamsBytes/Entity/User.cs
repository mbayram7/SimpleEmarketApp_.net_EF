using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmarketDreamsBytes.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhoneNumber { get; set; }
        public bool IsAdmin { get; set; }

        public virtual IEnumerable<UserAddress> userAddress { get; set; }
        public virtual List<Order> Order { get; set; }
        public virtual List<Basket> Basket { get; set; }


    }
}