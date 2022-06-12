using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class brandsController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();
        // GET: brands
        public ActionResult Brands_Index(string brand_name)
        {
            ViewBag.Title = "主板管理";
            ViewBag.Erroy = "";
            ViewBag.Brand_name = brand_name;
            try
            {
                var b = db.brands
                    .Where(t => string.IsNullOrEmpty(brand_name) || t.brand_name.Contains(brand_name))
                    .ToList();
                return View(b);
            }
            catch
            {
                ViewBag.Erroy = "错误";
                return RedirectToAction("Brands_Index");
            }
        }
        public ActionResult Brands_Index_L(string brand_name)
        {
            ViewBag.Title = "主板管理";
            ViewBag.Erroy = "";
            ViewBag.Brand_name = brand_name;
            try
            {
                var b = db.brands
                    .Where(t => string.IsNullOrEmpty(brand_name) || t.brand_name.Contains(brand_name))
                    .ToList();
                return View(b);
            }
            catch
            {
                ViewBag.Erroy = "出现错误";
                return RedirectToAction("Brands_Index");
            }
        }

        // GET: brands/Edit/5
        public ActionResult Brands_Edit(int id = 0)
        {
            var m = new brands();
            if (id == 0)
            {
                //新建
                ViewBag.Title = "添加主板";
                var g = db.brands.Count();
                m = new brands { brand_id = g + 1 };
            }
            else
            {
                //修改
                ViewBag.Title = "修改主板";
                m = db.brands.Find(id);

            }
            return View(m);
        }

        // POST: brands/Edit/5
        [HttpPost]
        public ActionResult Brands_Edit(brands m)
        {
            try
            {
                if (m.brand_id == 0)
                {
                    //新建
                    db.brands.Add(m);
                    db.SaveChanges();
                }
                else
                {
                    //修改
                    db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Brands_Index");
            }
            catch
            {
                ViewBag.Erroy = "出错！";
                return RedirectToAction("Brands_Edit");
            }
        }

        // POST: brands/Delete/5
        public ActionResult Brands_Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    db.brands.Remove(db.brands.Find(id));
                    db.SaveChanges();
                }
                return RedirectToAction("Brands_Index");
            }
            catch
            {
                ViewBag.Erroy = "删除失败！";
                return RedirectToAction("Brands_Index");
            }
        }
    }
}
