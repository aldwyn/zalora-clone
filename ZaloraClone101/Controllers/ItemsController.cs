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
    public class ItemsController : ApiController
    {
        private ItemContext db = new ItemContext();

        // GET: api/Items
        public IQueryable<Item> GetItems(int offset = 0, string sortBy = "", string searchFor = "")
        {
            if (String.IsNullOrEmpty(sortBy))
            {
                sortBy = "";
            }
            var items = db.Items.OrderBy(item => item.id_catalog_config).Skip(offset).Take(48);
            if (!String.IsNullOrEmpty(searchFor))
            {
                items = db.Items
                    .Where(item => item.name.Contains(searchFor) || item.brand.Contains(searchFor))
                    .OrderBy(item => item.id_catalog_config).Skip(offset).Take(48);
            }
            if (sortBy.Equals("price_desc"))
            {
                items = db.Items.OrderByDescending(item => item.price).Skip(offset).Take(48);
            }
            else if (sortBy.Equals("price_asc"))
            {
                items = db.Items.OrderBy(item => item.price).Skip(offset).Take(48);
            }
            else if (sortBy.Equals("activated_asc"))
            {
                items = db.Items.OrderBy(item => item.activated_at).Skip(offset).Take(48);
            }
            else if (sortBy.Equals("activated_desc"))
            {
                items = db.Items.OrderByDescending(item => item.activated_at).Skip(offset).Take(48);
            }
            else if (sortBy.Equals("alphanum"))
            {
                items = db.Items.OrderBy(item => item.name).Skip(offset).Take(48);
            }
            return items;
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            Item item = db.Items.Find(id);
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
            Item item = db.Items.Find(id);
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