//const { cookies } = require("modernizr")

$(document).ready(function () {
    $("#btnSubmit").click(() => {
        username = $("#username").val()
        password = $("#password").val()
        var data = {
            "username": username,
            "password": password
        }
        fetch("https://localhost:44368/api/Login", {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(data),
        })
            .then(res => res.json())
            .then(data => {
                if (data.code == 200) {
                    document.cookie = "token=" + data.token
                    window.location = "https://localhost:44368/Home/Index";
                }
                else {
                    $("#login-error").empty();
                    $("#login-error").append(`<div class="alert alert-danger" style="margin-top:5px; padding:10px">Lỗi: ${data.message}</div>`);
                }
      
            })
    })

    $("#btnRegister").click(() => {
        username = $("#username").val()
        password = $("#password").val()
        confirmpassword = $("#ConfirmPassword").val()
        if (confirmpassword !== password) {
            $("#login-error").empty();
            $("#login-error").append(`<div class="alert alert-danger" style="margin-top:5px; padding:10px">Lỗi: mật khẩu nhập lại không trùng khớp</div>`);
        }
        var data = {
            "username": username,
            "password": password,
        }
        fetch("https://localhost:44368/api/Register", {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(data),
        })
            .then(res => res.json())
            .then(data => {
                if (data.code == 200) {
                    window.location = "https://localhost:44368/Home/Login";
                }
                else {
                    $("#login-error").empty();
                    $("#login-error").append(`<div class="alert alert-danger" style="margin-top:5px; padding:10px">Lỗi: ${data.message}</div>`);
                }
      
            })

    })
    $("#Logout").click(() => {
        console.log(" da vao");
        token = document.cookie.slice(6)
        console.log(token);
        fetch("https://localhost:44368/api/Login", {
            method: "PUT",
            headers: {
                'token': token,
                'content-type': 'application/json'
            
            },

        })
            .then(res => res.json())
            .then(data => {
                console.log(data)
                if (data.code == 200) {
                    document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:01 GMT;"
                    window.location = "https://localhost:44368/Home/Login";
                }
                else {
                    alert(data.message)
                }
            })
     
      
    })

})