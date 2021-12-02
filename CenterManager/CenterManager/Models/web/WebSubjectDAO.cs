using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace CenterManager.Models
{
    public class WebSubjectDAO
    {
        private string Base_URL = "https://localhost:44368/api/";

        // get all
        public IEnumerable<subject> GetAllSubjects()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // GET api/subject
                HttpResponseMessage response = client.GetAsync("subject").Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<IEnumerable<subject>>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }

        // add
        public bool AddSubject(subject s)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // POST api/subject
                HttpResponseMessage response = client.PostAsJsonAsync("subject", s).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }

        }
        
        // get by id
        public subject GetSubjectByID(string subject_id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // GET api/subject/5
                HttpResponseMessage response = client.GetAsync("subject/" + subject_id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<subject>().Result;
                return null;
            }
            catch
            {
                return null;
            }
        }
        // update
        public bool UpdateSubject(subject s)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // PUT api/subject/5
                HttpResponseMessage response = client.PutAsJsonAsync("subject/" + s.subject_id, s).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
        // delete
        public bool DeleteSubject(string subject_id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Base_URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // DELETE api/subject/5
                HttpResponseMessage response = client.DeleteAsync("subject/" + subject_id).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}