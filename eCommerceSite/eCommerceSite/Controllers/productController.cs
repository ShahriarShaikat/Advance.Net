using eCommerceSite.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace eCommerceSite.Controllers
{
    public class productController : Controller
    {
        // GET: product
        public ActionResult ViewProduct()
        {
            eCommerceEntities db = new eCommerceEntities();
            var d = db.products.ToList();
            return View(d);
        }

        public ActionResult addToCart(int id)
        {
            eCommerceEntities db = new eCommerceEntities();
            var pr = (from data in db.products
                     where data.id == id
                     select data).FirstOrDefault();
            if(Session["cart"] == null)
            {
                List<product> productList = new List<product>();
                productList.Add(pr);
                var json = new JavaScriptSerializer().Serialize(productList);
                Session["cart"] = json.ToString();
            }
            else
            {
                string json = Session["cart"].ToString();
                var d = new JavaScriptSerializer().Deserialize<List<product>>(json);
                d.Add(pr);
                var listP = new JavaScriptSerializer().Serialize(d);
                Session["cart"] = listP.ToString();
            }
            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            
            if(Session["cart"] == null)
            {
                List<product> d = new List<product>();
                return View(d);
            }
            else 
            {
                string json = Session["cart"].ToString();
                var d = new JavaScriptSerializer().Deserialize<List<product>>(json);
                return View(d);
            }
            
        }
        public ActionResult DeleteProduct(int id)
        {
            eCommerceEntities db = new eCommerceEntities();
            var matchObj = (from data in db.products
                      where data.id == id
                      select data).FirstOrDefault();
            string json = Session["cart"].ToString();
            var d = new JavaScriptSerializer().Deserialize<List<product>>(json);
            d.Remove(matchObj);
            var listP = new JavaScriptSerializer().Serialize(d);
            Session["cart"] = listP.ToString();
            return RedirectToAction("ViewCart");
        }
    }


}