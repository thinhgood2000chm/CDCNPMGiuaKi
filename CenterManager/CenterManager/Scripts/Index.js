﻿token = document.cookie.slice(6)
fetch("https://localhost:44368/api/Student", {
    method: "GET",
    headers: {
        'token': token,
        'content-type': 'application/json'
    },
})
    .then(res => res.json())
    .then(data => {
        if (data.code != 200) {
            alert("Code 500. Lỗi không xác định");
        } else {
            console.log(data.count)
            $("#numberStudent").html(data.count)
            //
        }

    })
