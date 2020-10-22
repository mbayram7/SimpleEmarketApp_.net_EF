using EmarketDreamsBytes.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EmarketDreamsBytes
{
    public class MarketDB : DbContext
    {

        public MarketDB()
            :base(@"Data Source=.\SQLEXPRESS;Initial Catalog=EmarketDreamsBytesDB;Integrated Security=True")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }       
        public DbSet<Basket> Baskets { get; set; }   
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

      


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
          
        }

        internal object GetALlModelFromDB()
        {
            throw new NotImplementedException();
        }


        
     
    }
}