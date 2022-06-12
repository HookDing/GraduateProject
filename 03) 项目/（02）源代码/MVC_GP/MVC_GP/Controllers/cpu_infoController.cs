using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class cpu_infoController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();

        public ActionResult Cpu_Info_Index(string brand_name,int cpu_sockets_id=0)
        {
            ViewBag.Title = "处理器管理";
            ViewBag.brand_name = brand_name;
            ViewBag.cpu_sockets_id=new  SelectList(db.cpu_sockets.ToList(), "sockets_id", "sockets_name", cpu_sockets_id);
            var b = db.cpu_info
                .Where(t=>string.IsNullOrEmpty(brand_name)||t.brands.brand_name.Contains(brand_name))
                .Where(t=> cpu_sockets_id == 0||t.cpu_sockets_id== cpu_sockets_id)
                .ToList();
            return View(b);
        }

        public ActionResult Cpu_Info_Edit(int id=0)
        {
            var m = new cpu_info();
            if (id == 0)
            {
                //新建
                ViewBag.Title = "添加处理器";
                //var g = db.cpu_info.Count();
                //m = new cpu_info { cpu_id = g + 1 };
            }
            else
            {
                //修改
                ViewBag.Title = "修改处理器";
                m = db.cpu_info.Find(id);

            }
            ViewBag.cpu_brandId = new SelectList(db.brands.ToList(), "brand_id", "brand_name", m.cpu_brandId);
            ViewBag.cpu_sockets_id=new  SelectList(db.cpu_sockets.ToList(), "sockets_id", "sockets_name", m.cpu_sockets_id);
            return View(m);
        }

        [HttpPost]
        public ActionResult Cpu_Info_Edit(cpu_info m)
        {
            try
            {
                if (m.cpu_id == 0)
                {
                    //新建
                    db.cpu_info.Add(m);
                    db.SaveChanges();
                }
                else
                {
                    //修改
                    db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Cpu_Info_Index");
            }
            catch
            {
                ViewBag.Erroy = "出错！";
                return RedirectToAction("Cpu_Info_Edit");
            }
        }

        public ActionResult Cpu_Info_Delete(int id=0)
        {
            try
            {
                if (id != 0)
                {
                    db.cpu_info.Remove(db.cpu_info.Find(id));
                    db.SaveChanges();
                }
                return RedirectToAction("Cpu_Info_Index");
            }
            catch
            {
                ViewBag.Erroy = "删除失败！";
                return RedirectToAction("Cpu_Info_Index");
            }
        }
    }
}
