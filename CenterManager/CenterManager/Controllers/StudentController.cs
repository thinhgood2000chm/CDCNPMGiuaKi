using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PagedList;

namespace CenterManager.Controllers
{
    public class StudentController : ApiController
    {
        StudentDao stDao = new StudentDao();
        // GET: api/Student
        public IHttpActionResult Get(int page = 1)
        {
            int size = 10; // số index tối đa mỗi trang
            var allData = stDao.GetAllStudents();
            int maxPage = allData.Count() / size; // chia lấy nguyên (int/int => int)
            if (allData.Count() % size != 0)    // (11/10 = 1.1 => phải +1)
                maxPage += 1;
            return Ok(new {code = 200, data = allData.OrderBy(e => e.id).ToPagedList(page, size), maxPage });
        }
            // GET: api/Student/5
        public IHttpActionResult Get(string id)
        { 
            var st = stDao.GetstudentByID(id);
            if(st != null)
            {
                return Ok(st);
            }
            return BadRequest("không tìm thấy học viên với id =" + id);
        }

        // POST: api/Student
        public IHttpActionResult Post([FromBody] student model)
        {
            var student = stDao.GetstudentByID(model.student_id);
     
            if (string.IsNullOrEmpty(model.student_id))
            {
                return Json(new {code =400, message = "chưa nhập id" });
                //return BadRequest("chưa nhập id");
            }
            if (string.IsNullOrEmpty(model.name))
            {
                return Json(new { code = 400, message = "chưa nhập tên" });
            }
            if (model.birthYear.ToString().Equals(""))
            {
                return Json(new { code = 400, message = "chưa nhập năm sinh" });
            }
            if (student != null)
            {
                return Json(new { code = 400, message = "id học viên đã tồn tại" });

            }
            if (stDao.AddStudent(model))
            {
                return  Json(new { code = 200, id = model.id, student_id= model.student_id, name = model.name, year = model.birthYear });
            }
            return Json(new { code = 400, message = "có lỗi xảy ra" });

        }

        // PUT: api/Student/5
        public IHttpActionResult Put(string id,  student model)
        {
            var oldStudent = stDao.GetstudentByID(id);
            if(oldStudent == null)
            {
                return Json(new { code = 400, message = "học viên không tồn tại" });
           
            }
            if (string.IsNullOrEmpty(model.name))
            {
                return Json(new { code = 400, message = "chưa nhập tên" });
            }
            if (model.birthYear.ToString().Equals(""))
            {
                return Json(new { code = 400, message = "chưa nhập năm sinh" });
            }
            oldStudent.name = model.name;
            oldStudent.birthYear = model.birthYear;
            if (stDao.UpdateStudent(oldStudent))
            {
                return Json(new { code = 200, student_id = id, name = model.name, year = model.birthYear });
            }
            return Json(new { code = 400, message = "có lỗi xảy ra" });


        }

        // DELETE: api/Student/5
        public IHttpActionResult Delete(string id)
        {
            var st = stDao.GetstudentByID(id);
            if (st == null)
            {
                return Json(new { code = 400, message = "không tìm thấy học viên" });
            }
            if (stDao.DeleteStudent(st))
            {
                return Json(new { code = 200, message = "XÓA THÀNH CÔNG" });

            }
            return Json(new { code = 400, message = "có lỗi xảy ra" });
        }
    }
}
