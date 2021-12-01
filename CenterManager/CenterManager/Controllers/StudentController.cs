using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CenterManager.Controllers
{
    public class StudentController : ApiController
    {
        StudentDao stDao = new StudentDao();
        // GET: api/Student
        public IHttpActionResult Get()
        {
            return Ok(stDao.GetAllStudents());
        }
            // GET: api/Student/5
            public IHttpActionResult Get(string id)
        { 
            var st = stDao.GetstudentByID(id);
            if(st != null)
            {
                return Ok(st);
            }
            return BadRequest("ko tìm thấy học viên với id =" + id);
        }

        // POST: api/Student
        public IHttpActionResult Post([FromBody] student model)
        {
            var student = stDao.GetstudentByID(model.student_id);
     
            if (string.IsNullOrEmpty(model.student_id))
            {
                return BadRequest("chưa nhập id");
            }
            if (string.IsNullOrEmpty(model.name))
            {
                return BadRequest("chưa nhập tên");
            }
            if (model.birthYear.ToString().Equals(""))
            {
                return BadRequest("chưa nhập năm sinh");
            }
            if (student != null)
            {
                return BadRequest("id học viên đã tồn tại");
            }
            if (stDao.AddStudent(model))
            {
                return Ok("Thêm học viên thành công");
            }
            return BadRequest("có lỗi xảy ra");
        }

        // PUT: api/Student/5
        public IHttpActionResult Put(string id,  student model)
        {
            var oldStudent = stDao.GetstudentByID(id);
            if(oldStudent == null)
            {
                return BadRequest("học viên không tồn tại");
            }
            if (string.IsNullOrEmpty(model.name))
            {
                return BadRequest("chưa nhập tên");
            }
            if (model.birthYear.ToString().Equals(""))
            {
                return BadRequest("chưa nhập năm sinh");
            }
            oldStudent.name = model.name;
            oldStudent.birthYear = model.birthYear;
            if (stDao.UpdateStudent(oldStudent))
            {
                return Ok("cập nhật học viên thành công");
            }
            return BadRequest("có lỗi xảy ra");


        }

        // DELETE: api/Student/5
        public IHttpActionResult Delete(string id)
        {
            var st = stDao.GetstudentByID(id);
            if (st == null)
            {
                return BadRequest("không tìm thấy học viên");
            }
            if (stDao.DeleteStudent(st))
            {
                return Ok("XÓA THÀNH CÔNG");
            }
            return BadRequest("XÓA BỊ LỖI");
        }
    }
}
