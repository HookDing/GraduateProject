using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class powersController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();
        // Powers_Index
        public ActionResult Powers_Index(string powers_size, int powers_brands=0)
        {
            ViewBag.Title = "电源管理";
            ViewBag.Powers_size = powers_size;
            ViewBag.powers_brands = new SelectList(db.brands.ToList(), "brand_id", "brand_name", powers_brands);
            var b = db.powers
                .Where(x =>string.IsNullOrEmpty(powers_size)|| x.powers_size.Contains(powers_size))
                .Where(x => powers_brands == 0 || x.powers_brands == powers_brands)
                .ToList();
            return View(b);
        }

        // Edit
        public ActionResult Powers_Edit(int id=0)
        {
            var m = new powers();
            if (id == 0)
            {
                //添加
                ViewBag.Title = "添加电源";
                var g = db.mem.Count();
                m = new powers { powers_id = g + 1 };
            }
            else
            {
                //编辑
                ViewBag.Title = "修改电源";
                m = db.powers.Find(id);
            }
            ViewBag.powers_brands = new SelectList(db.brands.ToList(), "brand_id", "brand_name", m.powers_brands);
            return View(m);
        }

        // DoEdit
        [HttpPost]
        public ActionResult Powers_Edit(powers m)
        {
            if (m.powers_id > db.powers.Count())
            {
                //添加
                db.powers.Add(m);
                db.SaveChanges();
            }
            else
            {
                //编辑
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Powers_Index");
        }
        // Delete
        public ActionResult Powers_Delete(int id=0)
        {
            if (id != 0)
            {
                db.powers.Remove(db.powers.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Powers_Index");
        }
    }
}
