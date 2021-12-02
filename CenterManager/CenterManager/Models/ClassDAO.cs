using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CenterManager.Models
{
    public class ClassDAO
    {
        CenterManagerEntities db = new CenterManagerEntities();
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
        public IEnumerable<@class> GetAllClasses()
        {
            return db.classes; 
        }
        public @class GetClassByID(string class_id)
        {
            return db.classes.FirstOrDefault(i => i.class_id == class_id);
        }
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