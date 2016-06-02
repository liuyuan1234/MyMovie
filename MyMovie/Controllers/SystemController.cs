using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMovie.BLL;
using MyMovie.Model;
using System.IO;

namespace MyMovie.Controllers
{
    public class SystemController : Controller
    {
        //
        // GET: /System/

        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult Index()
        {
            HttpCookie aCookie = Request.Cookies["MyMovie_sysUserID"];
            if (aCookie == null)
            {
                return new RedirectResult("/System/SignIn");
            }
            else
            {
                int id = Convert.ToInt32(aCookie.Value);
                SystemDB db = new SystemDB();
                string userName = db.GetUserName(id);
                ViewBag.userName = userName;
            }
            return View();
        }

        public ActionResult Detail(int id = 0)
        {
            HttpCookie aCookie = Request.Cookies["MyMovie_sysUserID"];
            if (aCookie == null)
            {
                return new RedirectResult("/System/SignIn");
            }
            else
            {
                int uid = Convert.ToInt32(aCookie.Value);
                SystemDB udb = new SystemDB();
                string userName = udb.GetUserName(uid);
                ViewBag.userName = userName;
                
            }

            SystemDB db = new SystemDB();

            MovieDetailModel model;
            if (id > 0)
            {
                model = db.GetDetail(id);
                ViewBag.Submit = "/system/update";
            }
            else
            {
                model = new MovieDetailModel();
                ViewBag.Submit = "/system/insert";
            }
            ViewBag.Item = model;
            return View();
        }

        //登录验证
        public ActionResult SignInAjax(string userName, string password)
        {
            if (userName == "" || password == "")
            {
                return Json(new { result = 0, error = "用户名或密码不能为空" });
            }
            SystemDB db = new SystemDB();
            int id = db.Check(userName, password);
            if (id == 0)
            {
                return Json(new { result = 0, error = "用户名或密码错误" });
            }
            else
            {
                HttpCookie aCookie = new HttpCookie("MyMovie_sysUserID");
                aCookie.Value = id.ToString();
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(aCookie);
                return Json(new { result = 1, error = "" });
            }
        }

        public ActionResult ListAjax(string type,int pageindex,int pagesize)
        {
            int resordcount, pagecount;
            SystemDB db = new SystemDB();
            List<MovieDetailModel> list = db.GetListByTypeName(type, pagesize, pageindex, out resordcount, out pagecount);
            return Json(new { list = list, resordcount = resordcount, pagecount = pagecount });
        }

        public ActionResult DeleteAjax(int id)
        {
            SystemDB db = new SystemDB();
            int result = db.DeleteOne(id);
            return Json(new { result = result });
        }

        [HttpPost] 
        public ActionResult Insert(FormCollection form)
        {
            MovieDetailModel model = new MovieDetailModel();
            model.Name = form["Name"];
            model.typename = form["TypeName"];
            model.Actors = form["Actors"];
            model.Introduce = form["Introduce"];
            model.Score = Convert.ToDecimal(form["Score"]);


            //model.MovieImg = String.Empty;
            //model.MovieUrl = String.Empty;

            HttpPostedFileBase imgFileBase = Request.Files["Img"];
            string baseUrl = Server.MapPath("/");
            string uploadPath = baseUrl + @"Upload\img\";
            string exten = Path.GetExtension(imgFileBase.FileName);
            model.MovieImg = DateTime.Now.ToString("yyyyMMddHHmmss") + exten;
            imgFileBase.SaveAs(uploadPath + model.MovieImg);

            HttpPostedFileBase movieFileBase = Request.Files["movie"];
            uploadPath = baseUrl + @"Upload\movies\";
            exten = Path.GetExtension(movieFileBase.FileName);
            model.MovieUrl = DateTime.Now.ToString("yyyyMMddHHmmss") + exten;
            movieFileBase.SaveAs(uploadPath + model.MovieUrl);

            // /Upload/img/+
            SystemDB db = new SystemDB();
            int result = db.AddOne(model);

            return new RedirectResult("/System/index");

        }

        public ActionResult Update(FormCollection form)
        {
            SystemDB db = new SystemDB();

            MovieDetailModel model = db.GetDetail(Convert.ToInt32(form["ID"]));
            model.Name = form["Name"];
            model.typename = form["TypeName"];
            model.Actors = form["Actors"];
            model.Introduce = form["Introduce"];
            model.Score = Convert.ToDecimal( form["Score"]);
            int result = db.UpdOne(model);

            return new RedirectResult("/System/index");

        }

        public ActionResult LogOut()
        {
            HttpCookie aCookie = Response.Cookies["MyMovie_sysUserID"];
            aCookie.Expires = DateTime.Now.AddDays(-1);
            return new RedirectResult("/system/SignIn");
        }
    }
}
