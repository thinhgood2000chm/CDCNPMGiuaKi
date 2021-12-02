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
        public IEnumerable<subject> Get()
        {
            return dao.GetAllSubjects();
        }

        // GET api/Subject/5
        public IHttpActionResult Get(string id)
        {
            var s = dao.GetSubjectByID(id);
            if (s == null)
            {
                return BadRequest("ko tìm thấy môn học với id =" + id);
            }

            return Ok(s);
        }

        // POST api/Subject
        public IHttpActionResult Post([FromBody] subject model)
        {
            // kiểm tra thông tin
            var c = dao.GetSubjectByID(model.subject_id);

            if (string.IsNullOrEmpty(model.subject_id))
            {
                return BadRequest("chưa nhập mã môn học");
            }
            if (string.IsNullOrEmpty(model.name))
            {
                return BadRequest("chưa nhập tên");
            }
            if (c != null)
            {
                return BadRequest("id môn học đã tồn tại");
            }

            // thêm
            if (dao.AddSubject(model))
            {
                return Ok("Thêm môn học thành công");
            }
            return BadRequest("có lỗi xảy ra");
        }

        // PUT api/Subject/5
        // edit
        public IHttpActionResult Put(string id, [FromBody] subject s)
        {
            // kiểm tra thông tin
            var old_s = dao.GetSubjectByID(id);
            if (old_s == null)
            {
                return BadRequest("môn học không tồn tại");
            }
            if (string.IsNullOrEmpty(s.name))
            {
                return BadRequest("chưa nhập tên");
            }

            // cập nhật
            old_s.name = s.name;

            if (dao.UpdateSubject(old_s))
            {
                return Ok("cập nhật môn học thành công");
            }
            return BadRequest("có lỗi xảy ra");
        }

        // DELETE api/Subject/5
        public IHttpActionResult Delete(string id)
        {
            var c = dao.GetSubjectByID(id);
            if (c == null)
            {
                return BadRequest("không tìm thấy môn học");
            }
            if (dao.DeleteSubject(id))
            {
                return Ok("XÓA THÀNH CÔNG");
            }
            return BadRequest("XÓA BỊ LỖI");
        }
    }
}