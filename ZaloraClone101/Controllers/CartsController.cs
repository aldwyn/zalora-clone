using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ZaloraClone101.Models;

namespace ZaloraClone101.Controllers
{
    public class CartsController : ApiController
    {
        private CartContext carts = new CartContext();
        private ItemContext items = new ItemContext();

        // GET: api/Carts
        public IQueryable<Cart> GetCarts()
        {
            return carts.Carts.Include("Item");
        }

        // GET: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult GetCart(int id)
        {
            Cart cart = new Cart();
            cart.item_id = id;
            cart.user_id = User.Identity.GetUserId();
            cart.cart_date = DateTime.Now;
            Item item = items.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            var findCart = carts.Carts.Where(o => o.item_id.Equals(cart.item_id) && o.user_id.Equals(cart.user_id));
            if (findCart.Any())
            {
                return NotFound();
            }
            carts.Carts.Add(cart);
            carts.SaveChanges();
            return Ok(carts);
        }

        // DELETE: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult DeleteCart(int id)
        {
            Cart cart = carts.Carts.Find(id);
            if (carts == null)
            {
                return NotFound();
            }

            carts.Carts.Remove(cart);
            carts.SaveChanges();

            return Ok(carts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                carts.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartExists(int id)
        {
            return carts.Carts.Count(e => e.item_id == id) > 0;
        }
    }
}