using CenterManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CenterManager.Controllers
{
    public class RegisterController : ApiController
    {
        public IHttpActionResult POST(userLogin userLogin)
        {
            LoginRegis lg = new LoginRegis();

            if (string.IsNullOrEmpty(userLogin.username))
            {
                return Json(new { code = 400, message = "Chưa nhập username" });
            }
            if (string.IsNullOrEmpty(userLogin.password))
            {
                return Json(new { code = 400, message = "Chưa nhập password" });
            }
            var account = lg.GetAccountByUsername(userLogin.username);
            if (account != null)
            {
                return Json(new { code = 400, message = "Tài khoản đã tồn tại" });
            }
            if (lg.Register(userLogin))
            {
                return Ok(new { code = 200, message = "Đăng kí thành công" });
            }
            return Json(new { code = 400, message = "Có lỗi xảy ra" });

        }
    }
}
