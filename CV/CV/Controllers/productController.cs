using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CV.Models;

namespace CV.Controllers
{
    public class productController : Controller
    {
        // GET: product
        public ActionResult Index()
        {
            List<product> products = new List<product>();
            for(int i=1; i<11;i++)
            {
                var pl = new product()
                {
                    Id = i.ToString(),
                    Name = "Product-" + i.ToString()
                };
                products.Add(pl);
            }
            
            return View(products);
        }
    }
}