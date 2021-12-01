using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CenterManager.Models
{
    public class StudentDao
    {
        CenterManagerEntities db = new CenterManagerEntities();
        public bool AddStudent(student st)
        {
            try
            {
                db.students.Add(st);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
    
        }
        public IEnumerable<student> GetAllStudents()
        {
            return db.students; 
        }
        public student GetstudentByID(string student_id)
        {
            return db.students.FirstOrDefault(i => i.student_id == student_id);
        }
        public bool UpdateStudent(student st)
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
        public bool DeleteStudent(student st)
        {
            try
            {
                db.students.Remove(st);
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