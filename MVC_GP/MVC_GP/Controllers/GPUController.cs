using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class GPUController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();

        // GPU_Index
        public ActionResult GPU_Index(string gpu_model)
        {
            ViewBag.Title = "显卡管理";
            ViewBag.gpu_model = gpu_model;
            var b = db.GPU
                .Where(t => string.IsNullOrEmpty(gpu_model) || t.gpu_model.Contains(gpu_model))
                .ToList();
            return View(b);
        }
        // Edit
        public ActionResult GPU_Edit(int id=0)
        {
            var m = new GPU();
            if (id == 0)
            {
                //添加
                ViewBag.Title = "添加显卡";
                var g = db.disks.Count();
                m = new GPU { gpu_id = g + 1 };
            }
            else
            {
                //编辑
                ViewBag.Title = "修改显卡";
                m = db.GPU.Find(id);
            }
            ViewBag.gpu_brand = new SelectList(db.brands.ToList(), "brand_id", "brand_name", m.gpu_brand);
            return View(m);
        }

        // DoEdit
        [HttpPost]
        public ActionResult GPU_Edit(GPU m)
        {
            if (m.gpu_id > db.GPU.Count())
            {
                //添加
                db.GPU.Add(m);
                db.SaveChanges();
            }
            else
            {
                //编辑
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("GPU_Index");
        }

        

        // Delete
        public ActionResult GPU_Delete(int id, FormCollection collection)
        {
            try
            {
                if (id != 0)
                {
                    db.GPU.Remove(db.GPU.Find(id));
                    db.SaveChanges();
                }
                return RedirectToAction("GPU_Index");
            }
            catch
            {
                return RedirectToAction("GPU_Index");
            }
        }
    }
}
