using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CenterManager.Models
{
    public class SubjectDAO
    {
        CenterManagerEntities db = new CenterManagerEntities();
        public bool AddSubject(subject s)
        {
            try
            {
                db.subjects.Add(s);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public IEnumerable<subject> GetAllSubjects()
        {
            return db.subjects;
        }
        public subject GetSubjectByID(string subject_id)
        {
            return db.subjects.FirstOrDefault(s => s.subject_id == subject_id);
        }
        public bool UpdateSubject(subject s)
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
        public bool DeleteSubject(string subject_id)
        {
            try
            {
                subject s = db.subjects.FirstOrDefault(i => i.subject_id == subject_id);
                if (s == null)
                    return false;

                db.subjects.Remove(s);
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