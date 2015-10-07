using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZaloraClone101.Models;

namespace ZaloraClone101.Controllers
{
    public class HomeController : Controller
    {
        private ItemContext db = new ItemContext();
        public ActionResult Index()
        {
            var user_id = User.Identity.GetUserId();
            ViewBag.CartCount = db.Carts.Where(item => item.user_id == user_id).Count();
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}