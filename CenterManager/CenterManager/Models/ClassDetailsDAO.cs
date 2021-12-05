using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CenterManager.Models
{
    // Danh sách sinh viên trong lớp học
    public class ClassDetailsDAO
    {
        CenterManagerEntities db = new CenterManagerEntities();
        // add
        public bool Add(class_student c)
        {
            try
            {
                db.class_student.Add(c);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        // get all
        public IEnumerable<object> GetAll()
        {
            // join class - class_student - student; get class + student
            var classesDetails = (from cs in db.class_student
                           join c in db.classes on cs.class_id equals c.class_id
                           join s in db.students on cs.student_id equals s.student_id
                           select new
                           {
                               class_id = cs.class_id,
                               class_name = c.name,
                               student_id = cs.student_id,
                               student_name = s.name,
                               student_birthYear = s.birthYear
                           }).ToList();
            if (classesDetails.Count() == 0)
                return null;
            return classesDetails;
        }
        // get by class id
        public IEnumerable<class_student> GetByClassID(string id)
        {
            var classesDetails = from cs in db.class_student
                        where cs.class_id == id
                        select cs;
            if (classesDetails.Count() == 0)
                return null;
            return classesDetails;
        }
        // get info by class id
        public IEnumerable<object> GetInfoByClassID(string id)
        {
            var classDetails = (from cs in db.class_student
                                join c in db.classes on cs.class_id equals c.class_id
                                join s in db.students on cs.student_id equals s.student_id
                                where cs.class_id == id
                                select new
                                {
                                    class_id = cs.class_id,
                                    class_name = c.name,
                                    student_id = cs.student_id,
                                    student_name = s.name,
                                    student_birthYear = s.birthYear
                                })
                                .OrderBy(s => s.student_name);
            return classDetails;
        }
        // get by student id
        public IEnumerable<class_student> GetByStudentID(string id)
        {
            var classesDetails = from cs in db.class_student
                                 where cs.student_id == id
                                 select cs;
            if (classesDetails.Count() == 0)
                return null;
            return classesDetails;
        }
        // get by class id AND student id
        public class_student GetBy2ID(string class_id, string student_id)
        {
            var classDetails = db.class_student
                    .Where(cs => cs.class_id == class_id && cs.student_id == student_id)
                    .FirstOrDefault();
            return classDetails;
        }

        // get info by class id AND student id
        public object GetInfoBy2ID(string class_id, string student_id)
        {
            var classDetails = (from cs in db.class_student
                                join c in db.classes on cs.class_id equals c.class_id
                                join s in db.students on cs.student_id equals s.student_id
                                where cs.class_id == class_id 
                                   && cs.student_id == student_id
                                select new
                                {
                                    class_id = cs.class_id,
                                    class_name = c.name,
                                    student_id = cs.student_id,
                                    student_name = s.name,
                                    student_birthYear = s.birthYear
                                }).FirstOrDefault();
            return classDetails;
        }
        // update: không hỗ trợ
        public bool Update()
        {
            return false;
        }
        // delete
        public bool Delete(class_student c)
        {
            try
            {
                class_student cs = GetBy2ID(c.class_id, c.student_id);
                if (cs == null)
                    return false;

                db.class_student.Remove(cs);
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