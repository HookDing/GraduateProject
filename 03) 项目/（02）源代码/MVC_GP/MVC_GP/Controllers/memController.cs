using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class memController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();
        // Mem_Index
        public ActionResult Mem_Index(int mem_brand_id=0)
        {
            ViewBag.Title = "内存管理";
            ViewBag.mem_brand_id = new SelectList(db.brands.ToList(), "brand_id", "brand_name", mem_brand_id);
            var b = db.mem
                .Where(x => mem_brand_id==0|| x.mem_brand_id == mem_brand_id)
                .ToList();
            return View(b);
        }
        // Edit
        public ActionResult Mem_Edit(int id=0)
        {
            var m = new mem();
            if (id == 0)
            {
                //添加
                ViewBag.Title = "添加内存";
                var g = db.mem.Count();
                m = new mem { mem_id = g + 1 };
            }
            else
            {
                //编辑
                ViewBag.Title = "修改内存";
                m = db.mem.Find(id);
            }
            ViewBag.mem_brand_id = new SelectList(db.brands.ToList(), "brand_id", "brand_name", m.mem_brand_id);
            return View(m);
        }

        // DoEdit
        [HttpPost]
        public ActionResult Mem_Edit(mem m)
        {
            if (m.mem_id > db.mem.Count())
            {
                //添加
                db.mem.Add(m);
                db.SaveChanges();
            }
            else
            {
                //编辑
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Mem_Index");
        }


        // Delete
        public ActionResult Mem_Delete(int id=0)
        {
            if (id != 0)
            {
                db.mem.Remove(db.mem.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Mem_Index");
        }
    }
}
