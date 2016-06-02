using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMovie.Model;
using MyMovie.BLL;

namespace MyMovie.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            HttpCookie aCookie = Request.Cookies["MyMovie_UserID"];
            if (aCookie == null)
            {
                ViewBag.username = "";
            }
            else
            {
                int id = Convert.ToInt32(aCookie.Value);
                UserDB db = new UserDB();
                string userName = db.GetUserName(id);
                ViewBag.userName = userName;
            }

            MovieDB mdb = new MovieDB();
            List<MovieDetailModel> newlist = mdb.GetNewMovies();
            List<MovieDetailModel> popularlist = mdb.GetPopularMovies();

            ViewBag.newlist = newlist;
            ViewBag.popularlist = popularlist;

            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            HttpCookie aCookie = Response.Cookies["MyMovie_UserID"];
            aCookie.Expires = DateTime.Now.AddDays(-1);
            return new RedirectResult("/home/index");
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult MovieList(string name="")
        {
            //去数据库数据，把name传进去
            MovieDB db = new MovieDB();
            List<MovieDetailModel> list = db.GetListByTypeName(name);

            //MovieDetailModel m = new MovieDetailModel();
            //m.ID = 1;
            //m.MovieImg = "/Upload/Images/23423423.jpg";
            //m.Name = "疯狂动物城";
            //list.Add(m);

            //MovieDetailModel m1 = new MovieDetailModel();
            //m1.ID = 1;
            //m1.MovieImg = "/Upload/Images/23423423.jpg";
            //m1.Name = "疯狂动物城1";
            //list.Add(m1);

            ViewBag.MovieList = list;
            ViewBag.now = name;

            HttpCookie aCookie = Request.Cookies["MyMovie_UserID"];
            if (aCookie == null)
            {
                ViewBag.username = "";
            }
            else
            {
                int id = Convert.ToInt32(aCookie.Value);
                UserDB udb = new UserDB();
                string userName = udb.GetUserName(id);
                ViewBag.userName = userName;
            }
            return View();
        }

        /// <summary>
        /// 详情页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MovieDetail(int id=0)
        {
            //去数据库数据，把id传进去
            MovieDB db = new MovieDB();
            MovieDetailModel m = db.GetDetail(id);

            ViewBag.movieModel = m;

            HttpCookie aCookie = Request.Cookies["MyMovie_UserID"];
            if (aCookie == null)
            {
                ViewBag.username = "";
            }
            else
            {
                int id1 = Convert.ToInt32(aCookie.Value);
                UserDB idb = new UserDB();
                string userName = idb.GetUserName(id1);
                ViewBag.userName = userName;
            }

            return View();
        }


        public ActionResult SignUpAjax(string userName, string password)
        {
            //验证空字符串
            if (userName == "" || password == "")
            {
                return Json(new { result=0,error="用户名或密码不能为空" });
            }
            //验证用户名是否重复,获取数据库中用户名为ddd的数据
            UserDB db = new UserDB();
            int id = db.CheckUserName(userName);
            if (id > 0)
            {
                return Json(new { result = 0, error = "该用户名已存在" });
            }
            //保存到数据库
            int result = db.SaveUser(userName, password);
            string error = "";
            if (result != 1)
            {
                error = "数据库错误";
            }
            return Json(new { result = result, error = error });
        }

        public ActionResult SignInAjax(string userName, string password)
        {
            if (userName == "" || password == "")
            {
                return Json(new { result = 0, error = "用户名或密码不能为空" });
            }
            UserDB db = new UserDB();
            int id = db.Check(userName, password);
            if (id == 0)
            {
                return Json(new { result = 0, error = "用户名或密码错误" });
            }
            else
            {
                HttpCookie aCookie = new HttpCookie("MyMovie_UserID");
                aCookie.Value = id.ToString();
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(aCookie);
                return Json(new { result = 1, error = "" });
            }
        }
    }
}
