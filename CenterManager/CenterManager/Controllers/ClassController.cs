﻿using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CenterManager.Controllers
{
    public class ClassController : ApiController
    {
        ClassDAO dao = new ClassDAO();
        // GET api/Class
        public IHttpActionResult Get()
        {
            return Json(new { code = 200, data = dao.GetAllClasses() });
        }

        // GET api/Class/5
        public IHttpActionResult Get(string id)
        {
            var c = dao.GetClassByID(id);
            if (c == null)
            {
                return Json(new { code = 400, message = "không tìm thấy lớp học với id: " + id });
            }

            return Json(new { code = 200, data = c });
        }

        // POST api/Class
        // add
        public IHttpActionResult Post([FromBody] @class model)
        {
            // kiểm tra thông tin
            var c = dao.GetClassByID(model.class_id);


            if (string.IsNullOrEmpty(model.class_id))
            {
                return Json(new { code = 400, message = "chưa nhập mã lớp học" });
            }
            if (string.IsNullOrEmpty(model.name))
            {
                return Json(new { code = 400, message = "chưa nhập tên" });
            }
            if (string.IsNullOrEmpty(model.subject_id))
            {
                return Json(new { code = 400, message = "chưa nhập mã môn học" });
            }
            if (string.IsNullOrEmpty(model.teacher_id))
            {
                return Json(new { code = 400, message = "chưa nhập mã giáo viên" });
            }
            if (c != null)
            {
                return Json(new { code = 400, message = "mã lớp học đã tồn tại" });
            }

            // kiểm tra mã giáo viên có tồn tại không
            teacherDao teacherDao = new teacherDao();
            var t = teacherDao.GetTeacherByID(model.teacher_id);
            if (t == null)
            {
                return Json(new { code = 400, message = "mã giáo viên không tồn tại" });
            }

            // kiểm tra mã môn học có tồn tại không
            SubjectDAO subjectDAO = new SubjectDAO();
            var s = subjectDAO.GetSubjectByID(model.subject_id);
            if (s == null)
            {
                return Json(new { code = 400, message = "mã môn học không tồn tại" });
            }

            // thêm
            if (dao.AddClass(model))
            {
                return Json(new { code = 200, data = model });
            }
            return Json(new { code = 400, message = "có lỗi xảy ra" });
        }

        // PUT api/Class/5
        // edit
        public IHttpActionResult Put(string id, [FromBody] @class c)
        {
            // kiểm tra thông tin
            var old_c = dao.GetClassByID(id);
            if (old_c == null)
            {
                return Json(new { code = 400, message = "mã lớp học không tồn tại" });
            }
            if (string.IsNullOrEmpty(c.name))
            {
                return Json(new { code = 400, message = "chưa nhập tên" });
            }
            if (string.IsNullOrEmpty(c.subject_id))
            {
                return Json(new { code = 400, message = "chưa nhập mã môn học" });
            }
            if (string.IsNullOrEmpty(c.teacher_id))
            {
                return Json(new { code = 400, message = "chưa nhập mã giáo viên" });
            }

            // kiểm tra mã giáo viên có tồn tại không
            teacherDao teacherDao = new teacherDao();
            var t = teacherDao.GetTeacherByID(c.teacher_id);
            if (t == null)
            {
                return Json(new { code = 400, message = "mã giáo viên không tồn tại" });
            }

            // kiểm tra mã môn học có tồn tại không
            SubjectDAO subjectDAO = new SubjectDAO();
            var s = subjectDAO.GetSubjectByID(c.subject_id);
            if (s == null)
            {
                return Json(new { code = 400, message = "mã môn học không tồn tại" });
            }

            // cập nhật
            old_c.name = c.name;
            old_c.subject_id = c.subject_id;
            old_c.teacher_id = c.teacher_id;

            if (dao.UpdateClass(old_c))
            {
                return Json(new { code = 200, data = old_c });
            }
            return Json(new { code = 400, message = "có lỗi xảy ra" });
        }

        // DELETE api/Class/5
        public IHttpActionResult Delete(string id)
        {
            var c = dao.GetClassByID(id);
            if (c == null)
            {
                return Json(new { code = 400, message = "không tìm thấy lớp học" });
            }
            if (dao.DeleteClass(id))
            {
                return Json(new { code = 200, data = c });
            }
            return Json(new { code = 400, message = "có lỗi khi xóa" });
        }
    }
}