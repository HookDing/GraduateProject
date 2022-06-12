using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class mainboard_infoController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();
        // Mainboard_Index
        public ActionResult Mainboard_Index(int mainboard_sockets_id = 0)
        {
            ViewBag.Title = "主板管理";
            ViewBag.mainboard_sockets_id = new SelectList(db.cpu_sockets.ToList(), "sockets_id", "sockets_name", mainboard_sockets_id);
            var b = db.mainboard_info
                .Where(x => mainboard_sockets_id == 0 || x.mainboard_sockets_id == mainboard_sockets_id)
                .ToList();
            return View(b);
        }
        // Edit
        public ActionResult Mainboard_Edit(int id = 0)
        {
            var m = new mainboard_info();
          
            if (id == 0)
            {
                //添加
                ViewBag.Title = "添加主板";
                var g = db.disks.Count();
                m = new mainboard_info { mainboard_id = g + 1 , mainboard_sockets_id =0};
            }
            else
            {
                //编辑
                ViewBag.Title = "修改主板";
                m = db.mainboard_info.Find(id);
            }
            ViewBag.mainboard_sockets_id = new SelectList(db.cpu_sockets.ToList(), "sockets_id", "sockets_name", m.mainboard_sockets_id);
            return View(m);
        }

        // DoEdit
        [HttpPost]
        public ActionResult Mainboard_Edit(mainboard_info m)
        {
            if (m.mainboard_id > db.mainboard_info.Count())
            {
                //添加
                db.mainboard_info.Add(m);
                db.SaveChanges();
            }
            else
            {
                //编辑
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Mainboard_Index");
        }


        // Delete
        public ActionResult Mainboard_Delete(int id = 0)
        {
            if (id != 0)
            {
                db.mainboard_info.Remove(db.mainboard_info.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Mainboard_Index");
        }
    }
}
