using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class cpu_socketsController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();

        public ActionResult Cpu_sockets_Index(string sockets_name)
        {
            ViewBag.Title = "处理器接口管理";
            ViewBag.Sockets_name = sockets_name;
            var b = db.cpu_sockets
                .Where(t=>string.IsNullOrEmpty(sockets_name)||t.sockets_name.Contains(sockets_name))
                .ToList();
            return View(b);
        }
        public ActionResult Cpu_sockets_Edit(int id=0)
        {
            var m = new cpu_sockets();
            if (id == 0)
            {
                //新建
                ViewBag.Title = "添加处理器接口";
                var g = db.cpu_sockets.Count();
                m = new cpu_sockets { sockets_id = g + 1 };
            }
            else
            {
                //修改
                ViewBag.Title = "修改处理器接口";
                m = db.cpu_sockets.Find(id);

            }
            return View(m);
        }

        [HttpPost]
        public ActionResult Cpu_sockets_Edit(cpu_sockets m)
        {
            if (m.sockets_id == 0)
            {
                //新建
                db.cpu_sockets.Add(m);
                db.SaveChanges();
            }
            else
            {
                //修改
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Cpu_sockets_Index");
        }
        public ActionResult Cpu_sockets_Delete(int id, FormCollection collection)
        {
            try
            {
                if (id != 0)
                {
                    db.cpu_sockets.Remove(db.cpu_sockets.Find(id));
                    db.SaveChanges();
                }
                return RedirectToAction("Cpu_sockets_Index");
            }
            catch
            {
                ViewBag.Erroy = "删除失败！";
                return RedirectToAction("Cpu_sockets_Index");
            }
        }
    }
}
