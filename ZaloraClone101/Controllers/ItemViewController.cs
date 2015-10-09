using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ZaloraClone101.Models;

namespace ZaloraClone101.Controllers
{
    public class ItemViewController : Controller
    {
        private ItemContext db = new ItemContext();

        // GET: ItemView
        public ActionResult Index(int offset = 0)
        {
            return View(db.Items.OrderBy(item => item.id_catalog_config).Skip(offset).Take(48));
        }

        // GET: ItemView/Details/5
        public ActionResult Details(int? id)
        {
            var user_id = User.Identity.GetUserId();
            ViewBag.CartCount = db.Carts.Where(o => o.user_id == user_id).Count();
            ViewBag.CartMatch = db.Carts.Where(o => o.item_id == id && o.user_id == user_id).Count();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: ItemView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemView/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_catalog_config,sku,max_saving_percentage,brand,name,special_price,price,activated_at,max_price,grouped_products,sizesystembrand,sizesystembrand_position,sub_cat_type_id,gender_id,attribute_set_id,categories,max_special_price,link,image,is_new,available_sizes")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: ItemView/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: ItemView/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_catalog_config,sku,max_saving_percentage,brand,name,special_price,price,activated_at,max_price,grouped_products,sizesystembrand,sizesystembrand_position,sub_cat_type_id,gender_id,attribute_set_id,categories,max_special_price,link,image,is_new,available_sizes")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: ItemView/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: ItemView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
