using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CenterManager.Models
{
    public class teacherDao
    {

        CenterManagerEntities db = new CenterManagerEntities();
        public bool AddTeacher(teacher tc)
        {
            try
            {
                db.teachers.Add(tc);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public IEnumerable<teacher> GetAllTeacher()
        {
            return db.teachers;
        }
        public teacher GetTeacherByID(string teacher_id)
        {
            return db.teachers.FirstOrDefault(i => i.teacher_id == teacher_id);
        }
        public bool UpdateTeacher(teacher tc)
        {
            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteTeacher(teacher tc)
        {
            try
            {
                db.teachers.Remove(tc);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}