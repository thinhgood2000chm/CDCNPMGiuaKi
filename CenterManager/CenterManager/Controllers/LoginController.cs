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
            var account = lg.GetAccountByUsername(userLogin.username);
            if(account != null)
            {
                account.token = Token;
                lg.add_Token(account);
                return Ok(new { code = 200, token = Token });
            }
            return Json(new { code = 400, message = "Có lỗi xảy ra" });
          
        }

        public IHttpActionResult Put()
        {
            if (!Request.Headers.Contains("token"))
            {
                return Unauthorized();
            }
            var token = Request.Headers.GetValues("token").First();
          var account = lg.GetAccountByToken(token);
            if (account != null)
            {

                account.token = null;
                lg.Logout(account);
                return Json(new { code = 200, message = "logout thành công" });

            }
            return Json(new { code = 400, message = "Có lỗi xảy ra" });

        }

     

    }
}
