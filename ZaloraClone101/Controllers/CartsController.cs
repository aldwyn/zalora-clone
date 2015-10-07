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
    public class dbController : ApiController
    {
        private ItemContext db = new ItemContext();

        // GET: api/db
        public IQueryable<Cart> Getdb()
        {
            return db.Carts.Include("Item");
        }

        // GET: api/db/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult GetCart(int id)
        {
            Cart cart = new Cart();
            cart.item_id = id;
            cart.user_id = User.Identity.GetUserId();
            cart.cart_date = DateTime.Now;
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            var findCart = db.Carts.Where(o => o.item_id.Equals(cart.item_id) && o.user_id.Equals(cart.user_id));
            if (findCart.Any())
            {
                return NotFound();
            }
            db.Carts.Add(cart);
            db.SaveChanges();
            return Ok(db);
        }

        // DELETE: api/db/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult DeleteCart(int id)
        {
            Cart cart = db.Carts.Find(id);
            if (db == null)
            {
                return NotFound();
            }

            db.Carts.Remove(cart);
            db.SaveChanges();

            return Ok(db);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartExists(int id)
        {
            return db.Carts.Count(e => e.item_id == id) > 0;
        }
    }
}