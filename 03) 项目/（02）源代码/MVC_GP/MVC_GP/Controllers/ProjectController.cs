using MVC_GP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_GP.Controllers
{
    public class ProjectController : Controller
    {
        ProjectDBEntities db = new ProjectDBEntities();
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Project_Home()
        {
            ViewBag.Title = "主页";
            ViewBag.UserID = Session["UserID"] == null ? 0 : Session["UserID"];
            return View(db.Hot.ToList());
        }
        /// <summary>
        /// 了解更多
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Project_LearnMore(int id = 0)
        {
            ViewBag.Title = "了解更多";
            ViewBag.UserID = Session["UserID"] == null ? 0 : Session["UserID"];
            var b = new Hot();
            if (id != 0)
            {
                b = db.Hot.Find(id);
            }
            else
            {
                b.rid = db.Hot.Count() + 1;
            }
            return View(b);
        }
        /// <summary>
        /// 论坛
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Project_Talk(int id = 0)
        {
            ViewBag.Title = "论坛";
            ViewBag.UserID = Session["UserID"] == null ? 0 : Session["UserID"];
            //var b = new Talk();
            //if (id != 0)
            //{
            //    b = db.Talk.Find(id);
            //}
            return View(db.Talk.ToList());
        }
        /// <summary>
        /// 发表讨论
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Project_Talk(Talk m)
        {
            m.user_uid =int.Parse( Session["UserID"].ToString());
            db.Talk.Add(m);
            db.SaveChanges();
            return RedirectToAction("Project_Talk");
        }
        /// <summary>
        /// 个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Project_UserDetail(int id=0)
        {
            ViewBag.Title = "个人信息";
            ViewBag.UserID = Session["UserID"] == null ? 0 : Session["UserID"];
            var uid = Session["UserID"] == null ? 0 : Session["UserID"];
            return View(db.UserInfo.Find(uid));
        }
        /// <summary>
        /// 编辑个人信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Project_UserEdit(int id=0)
        {
            ViewBag.Title = "编辑个人信息";
            ViewBag.UserID = Session["UserID"] == null ? 0 : Session["UserID"];
            var uid = Session["UserID"] == null ? 0 : Session["UserID"];
            return View(db.UserInfo.Find(uid));
        }
        /// <summary>
        /// 提交个人信息更改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Project_UserEdit(UserInfo m)
        {
            db.Entry(m).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Project_UserDetail");
        }
        /// <summary>
        /// 用户讨论查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Project_UserTalk()
        {
            ViewBag.Title = "我的讨论";
            ViewBag.UserID = Session["UserID"] == null ? 0 : Session["UserID"];
            var uid = Session["UserID"] == null ? 0 : Session["UserID"];
            return View(db.Talk.Find(uid));
        }
        /// <summary>
        /// 管理页面入口
        /// </summary>
        /// <returns></returns>
        public ActionResult Project_Manage()
        {
                return Redirect("/Manage/Manage_Index");
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Project_Login(UserInfo m)
        {
            var i = db.UserInfo.Where(x => x.user_uname == m.user_uname && x.user_password == m.user_password).ToArray();
            if (i.Count() > 0)
            {
                Session["UserID"] = i[0].user_uid;
                TempData["msg"] = "登陆成功！";
                return RedirectToAction("Project_Home");
            }
            else
            {
                TempData["msg"] = "用户名或密码输入有误！";
                return RedirectToAction("Project_Home");
            }
        }
        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Project_Logout()
        {
            Session["UserID"] = 0;
                TempData["msg"] = "注销成功！";
            return RedirectToAction("Project_Home");
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public ActionResult Project_Regist(UserInfo m)
        {
            db.UserInfo.Add(m);
            if (db.SaveChanges() > 0)
            {
                TempData["msg"] = "注册成功";
                return RedirectToAction("Project_Home");
            }
            TempData["msg"] = "注册失败";
            return RedirectToAction("Project_Home");
        }
        //分部视图
        public ActionResult Navbar()
        {
            ViewBag.UserID = Session["UserID"] == null ? 0 : Session["UserID"];
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Regist()
        {
            return View();
        }
        public ActionResult Footer()
        {
            return View();
        }
    }
}