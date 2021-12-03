using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CenterManager.Controllers
{
    public class TeacherController : ApiController
    {
        teacherDao tcDao = new teacherDao();
        // GET: api/Teacher
        public IHttpActionResult Get()
        {
            return Ok(tcDao.GetAllTeacher());
        }

        // GET: api/Teacher/5
        public IHttpActionResult Get(string id)
        {
            var st = tcDao.GetTeacherByID(id);
            if (st != null)
            {
                return Ok(st);
            }
            return BadRequest("không tìm thấy giáo viên với id = " + id);
        }

        // POST: api/Teacher
        public IHttpActionResult Post([FromBody]teacher model)
        {
            if (string.IsNullOrEmpty(model.teacher_id))
            {
                return Json(new { code = 400, message = "chưa nhập id" });
            }
            if (string.IsNullOrEmpty(model.name))
            {
                return Json(new { code = 400, message = "chưa nhập tên" });
            }
            var teacher = tcDao.GetTeacherByID(model.teacher_id);
            if (teacher != null)
            {
                return Json(new { code = 400, message = "id giáo viên đã tồn tại" });
            }
            if (tcDao.AddTeacher(model))
            {
                return Json(new { code = 200, id = model.id, teacher_id = model.teacher_id, name = model.name });
            }
            return Json(new { code = 400, message = "có lỗi xảy ra" });
        }

        // PUT: api/Teacher/5
        public IHttpActionResult Put(string id, [FromBody] teacher model)
        {
            var oldTeacher = tcDao.GetTeacherByID(id);
            if (oldTeacher == null)
            {
                return Json(new { code = 400, message = "Gíao viên không tồn tại" });
            }
            if (string.IsNullOrEmpty(model.name))
            {
                return Json(new { code = 400, message = "chưa nhập tên" });
            }
            oldTeacher.name = model.name;
            if (tcDao.UpdateTeacher(oldTeacher))
            {
                return Json(new { code = 200, teacher_id = id, name = model.name });
            }
            return Json(new { code = 400, message = "có lỗi xảy ra" });
        }

        // DELETE: api/Teacher/5
        public IHttpActionResult Delete(string id)
        {
            var tc = tcDao.GetTeacherByID(id);
            if (tc == null)
            {
                return Json(new { code = 200, message = "không tìm thấy giáo viên" });
            }
            if (tcDao.DeleteTeacher(tc))
            {
                return Json(new { code = 200, message = "XÓA THÀNH CÔNG" });
            }
            return Json(new { code = 400, message = "có lỗi xảy ra" });
        }
    }
}
