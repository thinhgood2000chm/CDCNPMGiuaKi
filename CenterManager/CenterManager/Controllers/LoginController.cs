using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CenterManager.Controllers
{
    public class LoginController : ApiController
    {
        LoginRegis lg = new LoginRegis();
        public IHttpActionResult Post(userLogin userLogin)
        {
            if(!lg.CheckLogin(userLogin.username, userLogin.password))
            {
                return Json(new { code = 400, message = "Sai tên đăng nhập hoặc mật khẩu" });
            }
            var Token = System.Guid.NewGuid().ToString();
            return Ok(new {code = 200, token = Token });
        }

     

    }
}
