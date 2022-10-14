using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class TalkController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();
        // Talk_Index
        public ActionResult Talk_Index(string talk_content,int user_uid=0)
        {
            ViewBag.Title = "论坛管理";
            ViewBag.talk_content = talk_content;
            ViewBag.user_uid = new SelectList(db.UserInfo.ToList(), "user_uid", "user_uname", user_uid);
            var b = db.Talk
                .Where(t => string.IsNullOrEmpty(talk_content) || t.talk_content.Contains(talk_content))
                .Where(t => user_uid==0|| t.user_uid == user_uid)
                .ToList();
            return View(b);
        }

        // Edit
        public ActionResult Talk_Edit(int id=0)
        {
            var m = new Talk();
            if (id == 0)
            {
                //添加
                ViewBag.Title = "添加讨论";
                var g = db.Talk.Count();
                m = new Talk { talk_tid = g + 1 };
            }
            else
            {
                //编辑
                ViewBag.Title = "修改讨论";
                m = db.Talk.Find(id);
            }
            ViewBag.user_uid = new SelectList(db.UserInfo.ToList(), "user_uid", "user_uname", m.user_uid);
            return View(m);
        }

        // DoEdit
        [HttpPost]
        public ActionResult Talk_Edit(Talk m)
        {
            if (m.talk_tid > db.Talk.Count())
            {
                //添加
                db.Talk.Add(m);
                db.SaveChanges();
            }
            else
            {
                //编辑
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Talk_Index");
        }

        // Delete
        public ActionResult Talk_Delete(int id=0)
        {
            if (id != 0)
            {
                db.Talk.Remove(db.Talk.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Talk_Index");
        }
    }
}
