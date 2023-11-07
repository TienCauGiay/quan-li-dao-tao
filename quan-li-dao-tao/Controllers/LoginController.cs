using quan_li_dao_tao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_li_dao_tao.Controllers
{
    public class LoginController : Controller
    {
        QLSVDbContext _context = null;

        public LoginController()
        {
            _context = new QLSVDbContext(); 
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(users u)
        {
            var user = _context.Users.FirstOrDefault(x => x.user_name == u.user_name && x.pass_word == u.pass_word);
            if (user != null)
            {
                if (user.status == 1)
                {
                    Session["User"] = user;
                    if (user.role_id == "admin")
                        return RedirectToAction("Index", "Admin");
                    if (user.role_id == "teacher")
                        return RedirectToAction("Index", "Teacher");
                    if (user.role_id == "student")
                        return RedirectToAction("Index", "Student");
                }
                else
                {
                    TempData["AlertMessage"] = "Tài khoản không hoạt động";
                }
            }
            else
            {
                TempData["AlertMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index");
        }
    }
}