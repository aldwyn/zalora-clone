using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ZaloraClone101.Models
{
    public class CartContext : DbContext
    {
        public CartContext() : base("name=DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<ZaloraClone101.Models.Cart> Carts { get; set; }
    }
}