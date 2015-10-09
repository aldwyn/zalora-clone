using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ZaloraClone101.Models;

namespace ZaloraClone101.Controllers
{
    public class ItemsController : ApiController
    {
        private ItemContext db = new ItemContext();

        // GET: api/Items
        public IQueryable<MinimalItem> GetItems(int offset = 0, string sortBy = "", string searchFor = "")
        {
            if (String.IsNullOrEmpty(sortBy))
            {
                sortBy = "";
            }
            var items = MinimizeItemResult(db.Items.OrderBy(item => item.id_catalog_config), offset);
            if (!String.IsNullOrEmpty(searchFor))
            {
                items = MinimizeItemResult(db.Items
                    .Where(item => item.name.Contains(searchFor) || item.brand.Contains(searchFor))
                    .OrderBy(item => item.id_catalog_config), offset);
            }
            if (sortBy.Equals("price_desc"))
            {
                items = MinimizeItemResult(db.Items.OrderByDescending(item => item.price), offset);
            }
            else if (sortBy.Equals("price_asc"))
            {
                items = MinimizeItemResult(db.Items.OrderBy(item => item.price), offset);
            }
            else if (sortBy.Equals("activated_asc"))
            {
                items = MinimizeItemResult(db.Items.OrderBy(item => item.ActivatedAt()), offset);
            }
            else if (sortBy.Equals("activated_desc"))
            {
                items = MinimizeItemResult(db.Items.OrderByDescending(item => item.ActivatedAt()), offset);
            }
            else if (sortBy.Equals("alphanum"))
            {
                items = MinimizeItemResult(db.Items.OrderBy(item => item.name), offset);
            }
            return items;
        }

        public IQueryable<MinimalItem> MinimizeItemResult(IQueryable<Item> partialContext, int offset)
        {
            return partialContext
                .Skip(offset).Take(48)
                .Select(item =>
                new MinimalItem {
                    id_catalog_config = item.id_catalog_config,
                    name = item.name,
                    brand = item.brand,
                    link = item.link,
                    price = item.price,
                    available_sizes = item.available_sizes
                });
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            Item item = (Item) db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.id_catalog_config)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Items
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ItemExists(item.id_catalog_config))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = item.id_catalog_config }, item);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(int id)
        {
            Item item = (Item) db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.id_catalog_config == id) > 0;
        }
    }
}