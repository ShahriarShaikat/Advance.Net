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
            pr.qty = 1;
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
                bool flag = true;
                foreach(var datas in d)
                {
                    if(datas.id == pr.id)
                    {
                        datas.qty = datas.qty + 1;
                        flag = false;
                    }
                }
                if(flag)
                {
                    d.Add(pr);
                }
                var listP = new JavaScriptSerializer().Serialize(d);
                Session["cart"] = listP.ToString();
            }
            Session["msg"] = "Product added to the cart!";
            //TempData["msg"] = "Temporay message";
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
            /*eCommerceEntities db = new eCommerceEntities();
            var matchObj = (from data in db.products
                      where data.id == id
                      select data).FirstOrDefault();*/
            string json = Session["cart"].ToString();
            var d = new JavaScriptSerializer().Deserialize<List<product>>(json);
            var matchObj = (from data in d
                            where data.id == id
                            select data).FirstOrDefault();
            //d.Remove(d.FirstOrDefault(e => e.id == matchObj.id));
            d.Remove(matchObj);
            var listP = new JavaScriptSerializer().Serialize(d);
            Session["cart"] = listP.ToString();
            TempData["msg"] = "Product deleted!";
            return RedirectToAction("ViewCart");
        }
    }


}