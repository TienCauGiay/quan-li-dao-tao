using PagedList;
using quan_li_dao_tao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_li_dao_tao.Controllers
{
    public class AdminController : BaseController
    {
        QLSVDbContext _context = null;

        public AdminController()
        {
            _context = new QLSVDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddTeacher()
        {
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "faculty_id", "faculty_name");
            return View();
        }

        [HttpPost]
        public ActionResult AddTeacher(teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (_context.TEACHERs.FirstOrDefault(x => x.teacher_id == teacher.teacher_id) == null)
                {
                    _context.TEACHERs.Add(teacher);
                    if (_context.Users.FirstOrDefault(x => x.user_name == teacher.teacher_id) == null)
                    {
                        var user = new users();
                        user.user_name = teacher.teacher_id;
                        user.pass_word = "1";
                        user.role_id = "teacher";
                        user.status = 1;
                        _context.Users.Add(user);
                    }
                    _context.SaveChanges();
                    ViewBag.ErrorTeacher = "";
                    TempData["AlertMessage"] = "Thêm giảng viên thành công";
                    return RedirectToAction("ManageTeacher");
                }
                else
                {
                    ViewBag.ErrorTeacher = "Mã giảng viên đã tồn tại";
                }
            }
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "faculty_id", "faculty_name");
            return View(teacher);
        }

        public ActionResult ManageTeacher(int? page)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listTeacher = _context.TEACHERs.ToList();
            return View(listTeacher.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SearchTeacher(int? page, string searchTeacher)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listTeacher = _context.TEACHERs.Where(x => x.teacher_name.Contains(searchTeacher)).ToList();
            ViewBag.SearchTeacher = searchTeacher;
            return View(listTeacher.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UpdateTeacher(string id)
        {
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "faculty_id", "faculty_name");
            var res = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == id);
            return View(res);
        }

        [HttpPost]
        public ActionResult UpdateTeacher(teacher teacher)
        {
            if (ModelState.IsValid)
            {
                var res = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == teacher.teacher_id);
                res.faculty_id = teacher.faculty_id;
                res.teacher_name = teacher.teacher_name;
                res.birthday = teacher.birthday;
                res.gender = teacher.gender;
                res.address = teacher.address;
                res.phone_number = teacher.phone_number;
                res.email = teacher.email;
                res.image = teacher.image;
                _context.TEACHERs.AddOrUpdate(res);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Cập nhật thông tin giảng viên thành công";
                return RedirectToAction("ManageTeacher");
            }
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "faculty_id", "faculty_name");
            return View(teacher);
        }

        public ActionResult DeleteTeacher(string id)
        {
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "faculty_id", "faculty_name");
            var res = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == id);
            return View(res);
        }

        [HttpPost]
        public ActionResult DeleteTeacher(teacher teacher)
        {
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "faculty_id", "faculty_name");
            var res = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == teacher.teacher_id);
            ViewBag.ErrorTeacher = "";
            var user = _context.Users.FirstOrDefault(x => x.user_name == res.teacher_id);
            _context.Users.Remove(user);
            _context.TEACHERs.Remove(res);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Xóa giảng viên thành công";
            return RedirectToAction("ManageTeacher");
        }

        public ActionResult AddStudent()
        {
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "classes_id", "classes_name");
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(student student)
        {
            if (ModelState.IsValid)
            {
                if (_context.STUDENTs.FirstOrDefault(x => x.student_id == student.student_id) == null)
                {
                    _context.STUDENTs.Add(student);
                    if (_context.Users.FirstOrDefault(x => x.user_name == student.student_id) == null)
                    {
                        var user = new users();
                        user.user_name = student.student_id;
                        user.pass_word = "1";
                        user.role_id = "student";
                        user.status = 1;
                        _context.Users.Add(user);
                    }
                    _context.SaveChanges();
                    ViewBag.ErrorStudent = "";
                    TempData["AlertMessage"] = "Thêm sinh viên thành công";
                    return RedirectToAction("ManageStudent");
                }
                else
                {
                    ViewBag.ErrorStudent = "Mã sinh viên đã tồn tại";
                }
            }
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "classes_id", "classes_name");
            return View(student);
        }

        public ActionResult ManageStudent(int? page)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listStudent = _context.STUDENTs.ToList();
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SearchStudent(int? page, string searchStudent)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listStudent = _context.STUDENTs.Where(x => x.student_name.Contains(searchStudent)).ToList();
            ViewBag.SearchStudent = searchStudent;
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UpdateStudent(string id)
        {
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "classes_id", "classes_name");
            var res = _context.STUDENTs.FirstOrDefault(x => x.student_id == id);
            return View(res);
        }

        [HttpPost]
        public ActionResult UpdateStudent(student student)
        {
            if (ModelState.IsValid)
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
                _context.STUDENTs.AddOrUpdate(res);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Cập nhật thông tin sinh viên thành công";
                return RedirectToAction("ManageStudent");
            }
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "classes_id", "classes_name");
            return View(student);
        }

        public ActionResult DeleteStudent(string id)
        {
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "classes_id", "classes_name");
            var res = _context.STUDENTs.FirstOrDefault(x => x.student_id == id);
            return View(res);
        }

        [HttpPost]
        public ActionResult DeleteStudent(student student)
        {
            ViewBag.Classes = new SelectList(_context.CLASSes.ToList(), "classes_id", "classes_name");
            var resgisterSubject = _context.REGISTERSUBJECTs.FirstOrDefault(x => x.student_id == student.student_id);
            if (resgisterSubject != null)
            {
                ViewBag.ErrorStudent = "Không thể xóa sinh viên này";
                return View(student);
            }
            else
            {
                var scoreStudent = _context.SCOREs.Where(x => x.student_id == student.student_id);
                if (scoreStudent != null)
                {
                    _context.SCOREs.RemoveRange(scoreStudent);
                }
                var res = _context.STUDENTs.FirstOrDefault(x => x.student_id == student.student_id);
                ViewBag.ErrorStudent = "";
                _context.STUDENTs.Remove(res);
                var user = _context.Users.FirstOrDefault(x => x.user_name == student.student_id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Xóa sinh viên thành công";
                return RedirectToAction("ManageStudent");
            }
        }

        public ActionResult ManageUser(int? page)
        {
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listUser = _context.Users.ToList();
            return View(listUser.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SearchUser(int? page, string searchUser)
        {
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listUser = _context.Users.Where(x => x.user_name.Contains(searchUser)).ToList();
            ViewBag.SearchUser = searchUser;
            return View(listUser.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult UpdateUser(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.user_name == id);
            ViewBag.ListRole = new SelectList(_context.Roles.ToList(), "role_id", "description");
            return View(user);
        }

        [HttpPost]
        public ActionResult UpdateUser(users user)
        {
            var res = _context.Users.FirstOrDefault(x => x.user_name == user.user_name);
            res.pass_word = user.pass_word;
            res.role_id = user.role_id;
            res.status = user.status;
            _context.SaveChanges();
            TempData["AlertMessage"] = "Cập nhật tài khoản thành công";
            return RedirectToAction("ManageUser");
        }

        public ActionResult DeleteUser(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.user_name == id);
            return View(user);
        }

        [HttpPost]
        public ActionResult DeleteUser(users user)
        {
            var res = _context.Users.FirstOrDefault(x => x.user_name == user.user_name);
            _context.Users.Remove(res);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Xóa tài khoản thành công";
            return RedirectToAction("ManageUser");
        }

        public ActionResult SetupSchedule()
        {
            var result = (_context.REGISTERSUBJECTs.Where(rs => rs.status == 0).Select(rs => rs.subject_name).Distinct()).ToList();
            var subject = new SelectList(_context.SUBJECTs.Where(x => result.Contains(x.subject_name)).ToList(), "subject_name", "subject_name");
            var teacher = new SelectList(_context.TEACHERs.ToList(), "teacher_name", "teacher_name");
            ViewBag.Subject = subject;
            ViewBag.Teacher = teacher;
            return View();
        }

        [HttpPost]
        public ActionResult SetupSchedule(register_subject resgisterSubject)
        {
            var res = _context.REGISTERSUBJECTs.Where(x => x.subject_name.Contains(resgisterSubject.subject_name) && x.status == 0).ToList();
            foreach (var item in res)
            {
                item.time_learning = resgisterSubject.time_learning;
                item.address_learn = resgisterSubject.address_learn;
                item.teacher_name = resgisterSubject.teacher_name;
                item.status = 1;
            }
            try
            {
                _context.SaveChanges();
                TempData["AlertMessage"] = "Phân công lịch giảng dạy, học tập thành công";
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Phân công lịch giảng dạy, học tập không thành công";
            }
            var result = (_context.REGISTERSUBJECTs.Where(rs => rs.status == 0).Select(rs => rs.subject_name).Distinct()).ToList();
            var subject = new SelectList(_context.SUBJECTs.Where(x => result.Contains(x.subject_name)).ToList(), "subject_name", "subject_name");
            var teacher = new SelectList(_context.TEACHERs.ToList(), "teacher_name", "teacher_name");
            ViewBag.Subject = subject;
            ViewBag.Teacher = teacher;
            return View();
        }
    }
}