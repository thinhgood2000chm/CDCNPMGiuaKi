using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CenterManager.Controllers
{
    public class ClassDetailsController : ApiController
    {
        ClassDetailsDAO dao = new ClassDetailsDAO();
        // GET api/ClassDetails
        public IHttpActionResult Get()
        {
            return Json(new { code = 200, data = dao.GetAll() });
        }

        // GET api/ClassDetails/5
        public IHttpActionResult Get(string id)
        {
            ClassDAO classDAO = new ClassDAO();
            var c = classDAO.GetClassByID(id);
            if (c == null)
                return Json(new { code = 400, message = "không tìm thấy lớp học" });
            var cs = dao.GetInfoByClassID(id);
            return Json(new { code = 200, data = cs });
        }

        // POST api/ClassDetails
        public IHttpActionResult Post([FromBody] class_student model)
        {
            // kiểm tra thông tin
            if (string.IsNullOrEmpty(model.class_id))
            {
                return Json(new { code = 400, message = "chưa nhập mã lớp học" });
            }
            if (string.IsNullOrEmpty(model.student_id))
            {
                return Json(new { code = 400, message = "chưa nhập mã học viên" });
            }


            // kiểm tra mã lớp học có tồn tại không
            ClassDAO classDAO = new ClassDAO();
            var c = classDAO.GetClassByID(model.class_id);
            if (c == null)
            {
                return Json(new { code = 400, message = "mã lớp học không tồn tại" });
            }

            // kiểm tra mã học viên có tồn tại không
            StudentDao studentDao = new StudentDao();
            var s = studentDao.GetstudentByID(model.student_id);
            if (s == null)
            {
                return Json(new { code = 400, message = "mã học viên không tồn tại" });
            }


            // kiểm tra học viên đã có trong lớp đó chưa
            var cs = dao.GetBy2ID(model.class_id, model.student_id);
            if (cs != null)
            {
                return Json(new { code = 400, message = "học viên đã có trong lớp học" });
            }


            // Thêm
            if (dao.Add(model))
            {
                // return class with all info
                var rs = dao.GetInfoBy2ID(model.class_id, model.student_id);
                return Json(new { code = 200, data = rs });
            }
            return Json(new { code = 500, message = "có lỗi xảy ra" });

        }

        // PUT api/ClassDetails/5
        public IHttpActionResult Put(string id, [FromBody] class_student model)
        {
            return Json(new { code = 400, message = "phương thức không được hỗ trợ" });
        }

        // DELETE api/ClassDetails/
        public IHttpActionResult Delete([FromBody] class_student cs)
        {
            var c = dao.GetBy2ID(cs.class_id, cs.student_id);
            if (c == null)
            {
                return Json(new { code = 400, message = "không tìm thấy học viên trong lớp" });
            }
            var oldClass = dao.GetInfoBy2ID(cs.class_id, cs.student_id);
            if (dao.Delete(cs))
            {
                return Json(new { code = 200, data = oldClass });
            }
            return Json(new { code = 500, message = "có lỗi khi xóa" });
        }
    }
}