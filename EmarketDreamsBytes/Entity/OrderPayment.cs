using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmarketDreamsBytes.Entity
{
    public class OrderPayment
    {
        [Key]
        public int OrderPaymentId { get; set; }
        public _OrderType OrderType { get; set; }
        public decimal Price { get; set; }

        public enum _OrderType
        {
            Havale = 0,
            KrediKarti = 1
        }
    }

}