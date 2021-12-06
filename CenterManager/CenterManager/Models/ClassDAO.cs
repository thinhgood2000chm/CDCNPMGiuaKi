using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CenterManager.Models
{
    public class ClassDAO
    {
        CenterManagerEntities db = new CenterManagerEntities();
        // add
        public bool AddClass(@class c)
        {
            try
            {
                db.classes.Add(c);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
    
        }
        // get all
        public IEnumerable<object> GetAllClasses()
        {
            // join class - subject - teacher; get class + subjeb name + teacher name
            var classes = (from c in db.classes
                              join s in db.subjects on c.subject_id equals s.subject_id
                              join t in db.teachers on c.teacher_id equals t.teacher_id
                              select new
                              {
                                  class_id = c.class_id,
                                  class_name = c.name,
                                  subject_id = c.subject_id,
                                  subject_name = s.name,
                                  teacher_id = c.teacher_id,
                                  teacher_name = t.name
                              }).OrderBy(e => e.class_id);
            return classes;
        }
        // get by id
        public @class GetClassByID(string class_id)
        {
            return db.classes.FirstOrDefault(i => i.class_id == class_id);
        }
        // get all info by id 
        public object GetClassInfoByID(string class_id)
        {
            var myClass = (from c in db.classes
                           join s in db.subjects on c.subject_id equals s.subject_id
                           join t in db.teachers on c.teacher_id equals t.teacher_id
                           where c.class_id == class_id
                           select new
                           {
                               class_id = c.class_id,
                               class_name = c.name,
                               subject_id = c.subject_id,
                               subject_name = s.name,
                               teacher_id = c.teacher_id,
                               teacher_name = t.name
                           }).FirstOrDefault();
            return myClass;
        }
        // update
        public bool UpdateClass(@class c)
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
        // delete
        public bool DeleteClass(string class_id)
        {
            try
            {
                @class c = db.classes.FirstOrDefault(i => i.class_id == class_id);
                if (c == null)
                    return false;

                db.classes.Remove(c);
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