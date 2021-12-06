using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PagedList;

namespace CenterManager.Controllers
{
    public class SubjectController : ApiController
    {
        SubjectDAO dao = new SubjectDAO();
        LoginRegis lg = new LoginRegis();
        // GET api/Subject
        public IHttpActionResult Get(int page = 1, int size = 10)
        {
            if (!Request.Headers.Contains("token"))
            {
                return Unauthorized();
            }
            var token = Request.Headers.GetValues("token").First();
            var account = lg.GetAccountByToken(token);
            if (account == null)
            {
                return Json(new { code = 400, message = "Chưa đăng nhập" });
            }
         
            var allData = dao.GetAllSubjects();
            int maxPage = allData.Count() / size; // chia lấy nguyên (int/int => int)
            if (allData.Count() % size != 0)    // (9/10 = 0 => phải +1)
                maxPage += 1;
            return Json(new { code = 200, data = allData.OrderBy(e => e.subject_id).ToPagedList(page, size), maxPage, count = allData.Count() });
        }

        // GET api/Subject/5
        public IHttpActionResult Get(string id)
        {
            if (!Request.Headers.Contains("token"))
            {
                return Unauthorized();
            }
            var token = Request.Headers.GetValues("token").First();
            var account = lg.GetAccountByToken(token);
            if (account == null)
            {
                return Json(new { code = 400, message = "Chưa đăng nhập" });
            }
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
            if (!Request.Headers.Contains("token"))
            {
                return Unauthorized();
            }
            var token = Request.Headers.GetValues("token").First();
            var account = lg.GetAccountByToken(token);
            if (account == null)
            {
                return Json(new { code = 400, message = "Chưa đăng nhập" });
            }
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
            if (!Request.Headers.Contains("token"))
            {
                return Unauthorized();
            }
            var token = Request.Headers.GetValues("token").First();
            var account = lg.GetAccountByToken(token);
            if (account == null)
            {
                return Json(new { code = 400, message = "Chưa đăng nhập" });
            }
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
            if (!Request.Headers.Contains("token"))
            {
                return Unauthorized();
            }
            var token = Request.Headers.GetValues("token").First();
            var account = lg.GetAccountByToken(token);
            if (account == null)
            {
                return Json(new { code = 400, message = "Chưa đăng nhập" });
            }
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