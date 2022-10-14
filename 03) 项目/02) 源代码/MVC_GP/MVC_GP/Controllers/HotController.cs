using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class HotController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();
        // Hot_Index
        public ActionResult Hot_Index(string imgtext)
        {
            ViewBag.Title = "热门管理";
            ViewBag.imgtext = imgtext;
            var b = db.Hot
                .Where(t => string.IsNullOrEmpty(imgtext) || t.imgtext.Contains(imgtext))
                .ToList();
            return View(b);
        }

        // Edit
        public ActionResult Hot_Edit(int id=0)
        {
            var m = new Hot();
            if (id == 0)
            {
                //添加
                ViewBag.Title = "添加热门";
                var g = db.Hot.Count();
                m = new Hot { rid = g + 1 };
            }
            else
            {
                //编辑
                ViewBag.Title = "修改热门";
                m = db.Hot.Find(id);
            }
            return View(m);
        }

        // DoEdit
        [HttpPost]
        public ActionResult Hot_Edit(Hot m)
        {
            if (m.rid > db.Hot.Count())
            {
                //添加
                db.Hot.Add(m);
                db.SaveChanges();
            }
            else
            {
                //编辑
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Hot_Index");
        }
        // Delete
        public ActionResult Hot_Delete(int id=0)
        {
            if (id != 0)
            {
                db.Hot.Remove(db.Hot.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Hot_Index");
        }
    }
}
