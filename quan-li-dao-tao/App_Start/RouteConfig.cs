﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace quan_li_dao_tao
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "admin",
                url: "quan-tri-vien",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin setup schedule",
                url: "quan-li-lich-giang-day-hoc-tap",
                defaults: new { controller = "Admin", action = "SetupSchedule", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin add faculty",
                url: "them-khoa",
                defaults: new { controller = "Faculty", action = "AddFaculty", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin manage faculty",
                url: "quan-li-khoa",
                defaults: new { controller = "Faculty", action = "ManageFaculty", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin update faculty",
                url: "quan-li-khoa/sua-{id}",
                defaults: new { controller = "Faculty", action = "UpdateFaculty", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin delete faculty",
                url: "quan-li-khoa/xoa-{id}",
                defaults: new { controller = "Faculty", action = "DeleteFaculty", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin add teacher",
                url: "them-giang-vien",
                defaults: new { controller = "Admin", action = "AddTeacher", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin update teacher",
                url: "quan-li-giang-vien/sua-{id}",
                defaults: new { controller = "Admin", action = "UpdateTeacher", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin delete teacher",
                url: "quan-li-giang-vien/xoa-{id}",
                defaults: new { controller = "Admin", action = "DeleteTeacher", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin search teacher",
                url: "quan-li-giang-vien/tim-kiem",
                defaults: new { controller = "Admin", action = "SearchTeacher", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin manage teacher",
                url: "quan-li-giang-vien",
                defaults: new { controller = "Admin", action = "ManageTeacher", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin add student",
                url: "them-sinh-vien",
                defaults: new { controller = "Admin", action = "AddStudent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "admin update student",
               url: "quan-li-sinh-vien/sua-{id}",
               defaults: new { controller = "Admin", action = "UpdateStudent", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "admin delete student",
                url: "quan-li-sinh-vien/xoa-{id}",
                defaults: new { controller = "Admin", action = "DeleteStudent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin manage student",
                url: "quan-li-sinh-vien",
                defaults: new { controller = "Admin", action = "ManageStudent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin search student",
                url: "quan-li-sinh-vien/tim-kiem",
                defaults: new { controller = "Admin", action = "SearchStudent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin manage user",
                url: "quan-li-tai-khoan",
                defaults: new { controller = "Admin", action = "ManageUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin search user",
                url: "tim-kiem-tai-khoan",
                defaults: new { controller = "Admin", action = "SearchUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin update user",
                url: "quan-li-tai-khoan/sua-{id}",
                defaults: new { controller = "Admin", action = "UpdateUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "admin delete user",
                url: "quan-li-tai-khoan/xoa-{id}",
                defaults: new { controller = "Admin", action = "DeleteUser", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "teacher",
                url: "giang-vien",
                defaults: new { controller = "Teacher", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "teacher manage student",
                url: "giang-vien/quan-li-sinh-vien",
                defaults: new { controller = "Teacher", action = "Student", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "teacher manage score",
                url: "giang-vien/quan-li-diem-sinh-vien",
                defaults: new { controller = "Teacher", action = "Score", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "teacher change password",
                url: "giang-vien/doi-mat-khau",
                defaults: new { controller = "Teacher", action = "ChangePassword", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "teacher manage schedule",
                url: "giang-vien/lich-giang-day",
                defaults: new { controller = "Teacher", action = "Schedule", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "teacher search student",
                url: "giang-vien/tim-kiem-sinh-vien",
                defaults: new { controller = "Teacher", action = "SearchStudent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "teacher manage score student",
                url: "giang-vien/quan-li-diem-sinh-vien-theo-mon",
                defaults: new { controller = "Teacher", action = "ScoreStudentByClass", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "teacher update score student",
                url: "giang-vien/cap-nhat-diem-sinh-vien",
                defaults: new { controller = "Teacher", action = "UpdateScore", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "student",
                url: "sinh-vien",
                defaults: new { controller = "Student", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "student change password",
                url: "sinh-vien/doi-mat-khau",
                defaults: new { controller = "Student", action = "ChangePassword", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "student score",
                url: "sinh-vien/diem",
                defaults: new { controller = "Student", action = "Score", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "student score by semester",
                url: "sinh-vien/diem-theo-hoc-ky",
                defaults: new { controller = "Student", action = "ScoreBySemester", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "student register subject",
                url: "sinh-vien/dang-ki-tin",
                defaults: new { controller = "Student", action = "Register", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "student schedule",
                url: "sinh-vien/lich",
                defaults: new { controller = "Student", action = "Schedule", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
