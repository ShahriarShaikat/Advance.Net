using dbPractice.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace dbPractice.Controllers
{
    public class studentController : Controller
    {
        // GET: student
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(student s)
        {
            if(ModelState.IsValid)
            {
                int currentYear = DateTime.Now.Year;
                s.age = currentYear - Int16.Parse((Request["ag"].ToString()).Substring(0, 4));
                //s.cgpa = Request["ag"].Substring(3);
                DotNetL3Entities1 db = new DotNetL3Entities1();
                db.students.Add(s);
                db.SaveChanges();
                return RedirectToAction("showList");
            }
            return View();
        }

        public ActionResult showList()
        {
            DotNetL3Entities1 data = new DotNetL3Entities1();
            var d = data.students.ToList();
            return View(d);
        }

        public ActionResult Scholarship()
        {
            DotNetL3Entities1 data = new DotNetL3Entities1();
            var d = (from obj in data.students
                    where obj.cgpa > 3.75
                    select obj).ToList();
            return View(d);
        }
    }
}