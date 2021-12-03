using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CenterManager.Controllers
{
    public class SubjectController : ApiController
    {
        SubjectDAO dao = new SubjectDAO();
        // GET api/Subject
        public IHttpActionResult Get()
        {
            return Json(new { code = 200, data = dao.GetAllSubjects() });
        }

        // GET api/Subject/5
        public IHttpActionResult Get(string id)
        {
            var s = dao.GetSubjectByID(id);
            if (s == null)
            {
                return Json(new { code = 400, message = "không tìm thấy môn học với id: " + id });
            }

            return Json(new { code = 200, data = s });
        }

        // POST api/Subject
        public IHttpActionResult Post([FromBody] subject model)
        {
            // kiểm tra thông tin
            var c = dao.GetSubjectByID(model.subject_id);

            if (string.IsNullOrEmpty(model.subject_id))
            {
                return Json(new { code = 400, message = "chưa nhập mã môn học" });
            }
            if (string.IsNullOrEmpty(model.name))
            {
                return Json(new { code = 400, message = "chưa nhập tên" });
            }
            if (c != null)
            {
                return Json(new { code = 400, message = "mã môn học đã tồn tại" });
            }

            // thêm
            if (dao.AddSubject(model))
            {
                return Json(new { code = 200, data = model });
            }
            return Json(new { code = 400, message = "có lỗi xảy ra" });
        }

        // PUT api/Subject/5
        // edit
        public IHttpActionResult Put(string id, [FromBody] subject s)
        {
            // kiểm tra thông tin
            var old_s = dao.GetSubjectByID(id);
            if (old_s == null)
            {
                return Json(new { code = 400, message = "mã môn học không tồn tại" });
            }
            if (string.IsNullOrEmpty(s.name))
            {
                return Json(new { code = 400, message = "chưa nhập tên" });
            }

            // cập nhật
            old_s.name = s.name;

            if (dao.UpdateSubject(old_s))
            {
                return Json(new { code = 200, data = old_s });
            }
            return Json(new { code = 500, message = "có lỗi xảy ra" });
        }

        // DELETE api/Subject/5
        public IHttpActionResult Delete(string id)
        {
            var c = dao.GetSubjectByID(id);
            if (c == null)
            {
                return Json(new { code = 400, message = "không tìm thấy môn học với id: " + id });
            }
            if (dao.DeleteSubject(id))
            {
                return Json(new { code = 200, data = c });
            }
            return Json(new { code = 500, message = "xóa không thành công" });
        }
    }
}