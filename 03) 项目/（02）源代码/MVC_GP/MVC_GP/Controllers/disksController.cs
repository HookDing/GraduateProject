using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class disksController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();
        // GET: disks
        public ActionResult Disks_Index(string disks_model)
        {
            ViewBag.Title = "硬盘管理";
            ViewBag.disks_model = disks_model;
            var b = db.disks
                .Where(t => string.IsNullOrEmpty(disks_model) || t.disks_model.Contains(disks_model))
                .ToList();
            return View(b);
        }

        // GET: disks/Edit/5
        public ActionResult Disks_Edit(int id=0)
        {
            var m = new disks();
            if (id==0)
            {
                //添加
                ViewBag.Title = "添加硬盘";
                var g = db.disks.Count();
                m = new disks { disks_id = g + 1 };
            }
            else
            {
                //编辑
                ViewBag.Title = "修改硬盘";
                m = db.disks.Find(id);
            }
            ViewBag.disks_brand_id = new SelectList(db.brands.ToList(), "brand_id", "brand_name", m.disks_brand_id);
            return View(m);
        }

        // POST: disks/Edit/5
        [HttpPost]
        public ActionResult Disks_Edit(disks m)
        {
            try
            {
                if (m.disks_id > db.disks.Count())
                {
                    //添加
                    db.disks.Add(m);
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Disks_Index");
            }
            catch
            {
                return RedirectToAction("Disks_Edit");
            }
        }

        // POST: disks/Delete/5
        public ActionResult Disks_Delete(int id=0)
        {
            try
            {
                if (id != 0)
                {
                    db.disks.Remove(db.disks.Find(id));
                    db.SaveChanges();
                }
                return RedirectToAction("Disks_Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
