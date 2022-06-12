using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class UserInfoController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();
        // UserInfo_Index
        public ActionResult UserInfo_Index(string user_uname)
        {
            ViewBag.Title = "用户管理";
            ViewBag.user_uname = user_uname; 
            var b = db.UserInfo
                .Where(t => string.IsNullOrEmpty(user_uname) || t.user_uname.Contains(user_uname))
                .ToList();
            return View(b);
        }

        // Edit
        public ActionResult UserInfo_Edit(int id=0)
        {
            var m = new UserInfo();
            if (id == 0)
            {
                //添加
                ViewBag.Title = "添加用户";
                var g = db.UserInfo.Count();
                m = new UserInfo { user_uid = g + 1 };
            }
            else
            {
                //编辑
                ViewBag.Title = "修改用户";
                m = db.UserInfo.Find(id);
            }
            return View(m);
        }

        // DoEdit
        [HttpPost]
        public ActionResult UserInfo_Edit(UserInfo m)
        {
            if (m.user_uid > db.UserInfo.Count())
            {
                //添加
                db.UserInfo.Add(m);
                db.SaveChanges();
            }
            else
            {
                //编辑
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("UserInfo_Index");
        }
         
        // Delete
        public ActionResult UserInfo_Delete(int id=0)
        {
            if (id != 0)
            {
                db.UserInfo.Remove(db.UserInfo.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("UserInfo_Index");
        }
    }
}
