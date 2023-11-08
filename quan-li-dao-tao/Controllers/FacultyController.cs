using quan_li_dao_tao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quan_li_dao_tao.Controllers
{
    public class FacultyController : BaseController
    {
        QLSVDbContext _context = null;

        public FacultyController()
        {
            _context = new QLSVDbContext();
        }
        public ActionResult AddFaculty()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFaculty(faculty faculty)
        {
            if (ModelState.IsValid)
            {
                if (_context.FACULTies.FirstOrDefault(x => x.faculty_id == faculty.faculty_id) == null)
                {
                    _context.FACULTies.Add(faculty);
                    _context.SaveChanges();
                    ViewBag.ErrorFaculty = "";
                    TempData["AlertMessage"] = "Thêm khoa mới thành công";
                    return RedirectToAction("ManageFaculty");
                }
                else
                {
                    ViewBag.ErrorFaculty = "Mã khoa đã tồn tại";
                }
            }
            return View(faculty);
        }

        public ActionResult ManageFaculty()
        {
            var listFaculty = _context.FACULTies.ToList();
            return View(listFaculty);
        }

        public ActionResult UpdateFaculty(string id)
        {
            var faculty = _context.FACULTies.FirstOrDefault(x => x.faculty_id == id);
            return View(faculty);
        }

        [HttpPost]
        public ActionResult UpdateFaculty(faculty faculty)
        {
            var res = _context.FACULTies.FirstOrDefault(x => x.faculty_id == faculty.faculty_id);
            if (res != null)
            {
                res.faculty_name = faculty.faculty_name;
                _context.FACULTies.AddOrUpdate(res);
                _context.SaveChanges();
            }
            TempData["AlertMessage"] = "Cập nhật thông tin khoa thành công";
            return RedirectToAction("ManageFaculty");
        }

        public ActionResult DeleteFaculty(string id)
        {
            var faculty = _context.FACULTies.FirstOrDefault(x => x.faculty_id == id);
            return View(faculty);
        }

        [HttpPost]
        public ActionResult DeleteFaculty(faculty faculty)
        {
            var teacher = _context.TEACHERs.FirstOrDefault(x => x.faculty_id == faculty.faculty_id);
            var classes = _context.CLASSes.FirstOrDefault(x => x.faculty_id == faculty.faculty_id);
            var res = _context.FACULTies.FirstOrDefault(x => x.faculty_id == faculty.faculty_id);
            if (teacher == null && classes == null)
            {
                _context.FACULTies.Remove(res);
                ViewBag.ErrorFaculty = "";
                _context.SaveChanges();
            }
            else
            {
                ViewBag.ErrorFaculty = "Bạn không thể xóa khoa này";
                return View(faculty);
            }
            TempData["AlertMessage"] = "Xóa khoa thành công";
            return RedirectToAction("ManageFaculty");
        }
    }
}