using quan_li_dao_tao.Models;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace quan_li_dao_tao.Controllers
{
    public class TeacherController : BaseController
    {
        QLSVDbContext _context = null;

        public TeacherController()
        {
            _context = new QLSVDbContext();
        }
        // GET: Teacher
        public ActionResult Index()
        {
            var user = (users)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == user.user_name);
            ViewBag.Faculty = new SelectList(_context.FACULTies.ToList(), "faculty_id", "faculty_name");
            return View(teacher);
        }

        [HttpPost]
        public ActionResult UpdateTeacher(teacher teacher, HttpPostedFileBase file)
        {
            var res = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == teacher.teacher_id);
            res.faculty_id = teacher.faculty_id;
            res.teacher_name = teacher.teacher_name;
            res.birthday = teacher.birthday;
            res.gender = teacher.gender;
            res.address = teacher.address;
            res.phone_number = teacher.phone_number;
            res.email = teacher.email;
            var path = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                         || Path.GetExtension(file.FileName).ToLower() == ".png"
                        || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {
                        path = Path.Combine(Server.MapPath("~/assets/img/teacher"), file.FileName);
                        file.SaveAs(path);
                        res.image = file.FileName;
                    }
                }
            }
            _context.TEACHERs.AddOrUpdate(res);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Cập nhật thông tin thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Student(int? page)
        {
            var user = (users)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == user.user_name);
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listStudent = _context.STUDENTs.Where(x => x.CLASS.faculty_id == teacher.faculty_id).ToList();
            ViewBag.ClassList = _context.CLASSes.Where(x => x.FACULTY.faculty_id == teacher.faculty_id).DistinctBy(c => c.classes_name).ToList();
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult SearchStudent(int? page, string searchStudent, string classlist)
        {
            var user = (users)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == user.user_name);
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listStudent = _context.STUDENTs.Where(x => x.classes_id.Contains(classlist) && x.student_name.Contains(searchStudent)).ToList();
            ViewBag.SearchStudent = searchStudent;
            ViewBag.Classes = classlist;
            ViewBag.ClassList = _context.CLASSes.Where(x => x.FACULTY.faculty_id == teacher.faculty_id).DistinctBy(c => c.classes_name).ToList();
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Score()
        {
            var user = (users)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == user.user_name);
            ViewBag.ClassList = _context.CLASSes.Where(x => x.FACULTY.faculty_id == teacher.faculty_id).DistinctBy(c => c.classes_name).ToList();
            ViewBag.SubjectList = _context.SUBJECTs.ToList();
            return View();
        }

        public ActionResult ScoreStudentByClass(int? page, string classlist, string subjectlist)
        {
            var user = (users)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == user.user_name);
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listScoreStudent = _context.SCOREs.Where(x => x.STUDENT.classes_id.Contains(classlist) && x.subject_id.Contains(subjectlist)).ToList();
            ViewBag.ClassList = _context.CLASSes.Where(x => x.FACULTY.faculty_id == teacher.faculty_id).DistinctBy(c => c.classes_name).ToList();
            ViewBag.SubjectList = _context.SUBJECTs.ToList();
            ViewBag.ClassListSearch = classlist;
            ViewBag.SubjectListSearch = subjectlist;
            return View(listScoreStudent.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult InputScore(HttpPostedFileBase excelFile)
        {
            if (excelFile == null || excelFile.ContentLength == 0)
            {
                TempData["AlertMessage"] = "Vui lòng chọn một file Excel";
                return RedirectToAction("Score");
            }

            if (!excelFile.FileName.EndsWith(".xlsx") && !excelFile.FileName.EndsWith(".xls"))
            {
                TempData["AlertMessage"] = "File không đúng định dạng";
                return RedirectToAction("Score");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Đọc dữ liệu từ file Excel và lưu vào cơ sở dữ liệu
            using (var package = new ExcelPackage(excelFile.InputStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet != null)
                {
                    try
                    {
                        int rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            string studentId = worksheet.Cells[row, 1].Value?.ToString();
                            string subjectId = worksheet.Cells[row, 2].Value?.ToString();
                            double diemcc = double.Parse(worksheet.Cells[row, 3].Value?.ToString());
                            double diemkt = double.Parse(worksheet.Cells[row, 4].Value?.ToString());
                            double diemThi = double.Parse(worksheet.Cells[row, 5].Value?.ToString());
                            double diemTB = double.Parse(worksheet.Cells[row, 6].Value?.ToString());

                            // Lưu điểm của sinh viên vào cơ sở dữ liệu
                            var score = new score
                            {
                                student_id = studentId,
                                subject_id = subjectId,
                                score_cc = diemcc,
                                score_kt = diemkt,
                                score_test = diemThi,
                                score_tb = diemTB
                            };
                            _context.SCOREs.Add(score);
                            _context.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["AlertMessage"] = "Nhập điểm sinh viên không thành công, vui lòng kiểm tra lại file excel";
                        return RedirectToAction("Score");
                    }
                }
            }

            TempData["AlertMessage"] = "Nhập điểm sinh viên thành công";
            return RedirectToAction("Score");
        }

        public ActionResult UpdateScore(string studentid, string subjectid)
        {
            var score = _context.SCOREs.FirstOrDefault(x => x.student_id == studentid && x.subject_id == subjectid);
            return View(score);
        }

        [HttpPost]
        public ActionResult UpdateScore(score score)
        {
            var res = _context.SCOREs.FirstOrDefault(x => x.student_id == score.student_id && x.subject_id == score.subject_id);
            res.score_cc = score.score_cc;
            res.score_kt = score.score_kt;
            res.score_test = score.score_test;
            res.score_tb = (score.score_cc + score.score_kt * 2 + score.score_test * 3) / 6;
            _context.SaveChanges();
            return RedirectToAction("Score");
        }

        public ActionResult Schedule()
        {
            var user = (users)Session["User"];
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.teacher_id == user.user_name);
            var res = _context.REGISTERSUBJECTs.Where(x => x.teacher_name.Contains(teacher.teacher_name)).DistinctBy(x => x.time_learning).ToList();
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