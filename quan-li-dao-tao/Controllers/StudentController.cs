using PagedList;
using quan_li_dao_tao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_li_dao_tao.Controllers
{
    public class StudentController : BaseController
    {
        QLSVDbContext _context = null;
        public StudentController()
        {
            _context = new QLSVDbContext();
        }
        public ActionResult Index()
        {
            var user = (users)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.student_id == user.user_name);
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "classes_id", "classes_name");
            return View(student);
        }

        public ActionResult UpdateStudent(student student, HttpPostedFileBase file)
        {
            var res = _context.STUDENTs.FirstOrDefault(x => x.student_id == student.student_id);
            res.classes_id = student.classes_id;
            res.student_name = student.student_name;
            res.birthday = student.birthday;
            res.gender = student.gender;
            res.address = student.address;
            res.phone_number = student.phone_number;
            res.email = student.email;
            res.image = student.image;
            res.year_admission = student.year_admission;
            var path = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                         || Path.GetExtension(file.FileName).ToLower() == ".png"
                        || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {
                        path = Path.Combine(Server.MapPath("~/assets/img/student"), file.FileName);
                        file.SaveAs(path);
                        res.image = file.FileName;
                    }
                }
            }
            _context.STUDENTs.AddOrUpdate(res);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Cập nhật thông tin thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Score(int? page)
        {
            var user = (users)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.student_id == user.user_name);
            int pageSize = 15;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listScore = _context.SCOREs.Where(x => x.student_id == student.student_id).ToList();
            double? diem = 0.0;
            int? stc = 0;
            foreach (var item in listScore)
            {
                diem += item.score_tb * item.SUBJECT.number_tc;
                stc += item.SUBJECT.number_tc;
            }
            ViewBag.DiemHeMuoi = diem / stc;
            ViewBag.Semester = _context.SEMESTERs.ToList();
            return View(listScore.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ScoreBySemester(string semester)
        {
            var user = (users)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.student_id == user.user_name);
            var listScore = _context.SCOREs.Where(x => x.SUBJECT.semester_id.Contains(semester) && x.student_id == student.student_id).ToList();
            ViewBag.Semester = _context.SEMESTERs.ToList();
            return View(listScore);
        }

        public ActionResult Register()
        {
            var user = (users)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.student_id == user.user_name);
            var listSubject = (from s in _context.SUBJECTs
                               where !(from sc in _context.SCOREs
                                       where sc.student_id == student.student_id
                                       select sc.subject_id)
                               .Contains(s.subject_id)
                               || (from sc in _context.SCOREs
                                   where sc.student_id == student.student_id && sc.score_tb < 4
                                   select sc.subject_id)
                               .Contains(s.subject_id)
                               select s).ToList();
            ViewBag.Subjects = new SelectList(listSubject, "subject_id", "subject_name");
            return View();
        }

        [HttpPost]
        public ActionResult Register(register_subject register)
        {
            var user = (users)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.student_id == user.user_name);
            // ??
            var subject = _context.SUBJECTs.FirstOrDefault(x => x.subject_id == register.subject_name);
            var idRegsiter = student.student_id + "_" + subject.subject_id;
            register_subject r = new register_subject();
            r.register_subject_id = idRegsiter;
            r.student_id = student.student_id;
            r.subject_name = subject.subject_name;
            r.status = 0;
            try
            {
                _context.REGISTERSUBJECTs.Add(r);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Bạn đã đăng kí môn " + subject.subject_name + ", vui lòng kiểm tra lịch học";
                var listSubject = (from s in _context.SUBJECTs
                                   where !(from sc in _context.SCOREs
                                           where sc.student_id == student.student_id
                                           select sc.subject_id)
                                   .Contains(s.subject_id)
                                   || (from sc in _context.SCOREs
                                       where sc.student_id == student.student_id && sc.score_tb < 4
                                       select sc.subject_id)
                                   .Contains(s.subject_id)
                                   select s).ToList();
                ViewBag.Subjects = new SelectList(listSubject, "subject_id", "subject_name");
                return View(r);
            }
            TempData["AlertMessage"] = "Bạn đã đăng kí học môn " + subject.subject_name + " thành công";
            return RedirectToAction("Schedule");
        }

        public ActionResult Schedule()
        {
            var user = (users)Session["User"];
            var student = _context.STUDENTs.FirstOrDefault(x => x.student_id == user.user_name);
            var res = _context.REGISTERSUBJECTs.Where(x => x.student_id == student.student_id).ToList();
            return View(res);
        }

        public ActionResult ChangePassword()
        {
            var user = (users)Session["User"];
            return View(user);
        }

        [HttpPost]
        public ActionResult ChangePassword(users user)
        {
            var res = _context.Users.FirstOrDefault(x => x.user_name == user.user_name);
            res.pass_word = user.pass_word;
            _context.SaveChanges();
            TempData["AlertMessage"] = "Bạn đã đổi mật khẩu thành công";
            return RedirectToAction("Index");
        }
    }
}