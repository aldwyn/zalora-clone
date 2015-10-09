using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using ZaloraClone101.Models;

namespace ZaloraClone101.Controllers
{
    [Authorize]
    public class CartsController : ApiController
    {
        private ItemContext db = new ItemContext();

        // GET: api/Carts
        public IQueryable<Cart> GetCarts()
        {
            return db.Carts.Include("Item");
        }

        // POST: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult PostCart(int id)
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
            return Ok(item);
        }

        // DELETE: api/Carts/5
        [ResponseType(typeof(Cart))]
        public IHttpActionResult DeleteCart(int id)
        {
            string user_id = User.Identity.GetUserId();
            Cart cart = (Cart) db.Carts.Where(o => o.item_id == id && o.user_id == user_id).Single();
            if (cart == null)
            {
                return NotFound();
            }

            db.Carts.Remove(cart);
            db.SaveChanges();

            return Ok(cart);
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