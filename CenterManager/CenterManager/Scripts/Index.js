token = document.cookie.slice(6)
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
        } else {
            console.log(data.count)
            $("#numberStudent").html(data.count)
            //
        }

    })


fetch("https://localhost:44368/api/Teacher", {
    method: "GET",
    headers: {
        'token': token,
        'content-type': 'application/json'
    },
})
    .then(res => res.json())
    .then(data => {
        console.log(data)
        if (data.code != 200) {
        } else {
            console.log(data.count)
            $("#numberTeacher").html(data.count)
            //
        }

    })
fetch("https://localhost:44368/api/Subject", {
    method: "GET",
    headers: {
        'token': token,
        'content-type': 'application/json'
    },
})
    .then(res => res.json())
    .then(data => {
        if (data.code != 200) {
        } else {
            $("#numberSubject").html(data.count)
            //
        }

    })

fetch("https://localhost:44368/api/Class", {
    method: "GET",
    headers: {
        'token': token,
        'content-type': 'application/json'
    },
})
    .then(res => res.json())
    .then(data => {
        if (data.code != 200) {
        } else {
            $("#numberClass").html(data.count)
            //
        }

    })