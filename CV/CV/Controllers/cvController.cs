using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CV.Controllers
{
    public class cvController : Controller
    {
        // GET: cv
        public ActionResult Home()
        {
            return View();
        }
    }
}