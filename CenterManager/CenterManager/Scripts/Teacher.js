function UpdateTeacherDialog(teacher_id, name) {// phải sử dụng onclick trong th render html từ jquery ==> ko gọi được jquery 
    let label = $('#teacher-to-edit');
    let param = $('#teacher-to-edit-input');
    label.html(teacher_id + " - " + name);
    param.attr("data-id", teacher_id);// set attribute vào button cập nhật để gửi được id đi
    $("#edit-teacher-name").val(name);

}
function DeleteTeacherDialog(teacher_id, name) {
    let label = $('#teacher-to-delete');
    let param = $('#teacher-to-delete-input');
    label.html(teacher_id + " - " + name);
    param.attr("data-id", teacher_id);// set attribute vào button cập nhật để gửi được id đi
}
loadPage(1)
function loadPage(page) {
    token = document.cookie.slice(6)
    fetch("https://localhost:44368/api/Teacher?page=" + page, {
        method: "GET",
        headers: {
            'token': token,
            'content-type': 'application/json'
        },
    })
        .then(res => res.json())
        .then(a => {
            $("#bodyTable").empty()
            var data = a.data
            for (var i = 0; i < data.length; i++) {
                var htmlLoad = $(
                    `<tr id = ${data[i].teacher_id}>
                        <td class ="index">${data[i].id}</td>
                        <td>${data[i].teacher_id}</td>
                        <td>${data[i].name}</td>
                        <td> <a href="#" onclick="DeleteTeacherDialog('${data[i].teacher_id}','${data[i].name}')" class="text-danger" data-toggle="modal" onclick="DeleteDialog(this)" data-target="#confirm-delete">xóa</a>
                            |
                            <a href="#" data-toggle="modal" onclick="UpdateTeacherDialog('${data[i].teacher_id}','${data[i].name}')" data-target="#confirm-edit">sửa</a></td>
                    </tr>`
                )
                $("#bodyTable").append(htmlLoad)
            }
            // paging
            var maxPage = a.maxPage
            $("#paging").empty();
            for (var i = 1; i <= maxPage; i++) {
                var liPaging;
                if (i == page) {
                    liPaging = `
                        <li class="page-item active">
                            <a class="page-link" href="javascript:void(0)">${i}</a>
                        </li>`
                } else {
                    liPaging = `
                        <li class="page-item">
                            <a class="page-link" href="javascript:void(0)" onclick="loadPage(${i})">${i}</a>
                        </li>`
                }
                $("#paging").append(liPaging)
            }
        })
}

// thêm giáo viên
$("#CreateTeacher").click(e => {
    var teacher_id = $("#teacher_id").val()
    var teacher_name = $("#teacher_name").val()
    //console.log(student_id, student_name, student_birthYear)
    var data = {
        "teacher_id": teacher_id,
        "name": teacher_name,
    }
    token = document.cookie.slice(6)
    fetch("https://localhost:44368/api/Teacher", {
        method: "POST",
        headers: {
            'token': token,
            'content-type': 'application/json'
        },
        body: JSON.stringify(data),

    })
        .then(res =>
            res.json()
        )
        .then(data => {
            console.log(data.code, data.message)
            if (data.code === 400) {
                $("#add-error").empty();
                $("#add-error").append(`<div class="alert alert-danger" style="margin-top:5px; padding:10px">Lỗi: ${data.message}</div>`);
            }
            else {

                var row = $(
                    `<tr id ="${data.teacher_id}">
                            <td class ="index">${data.id}</td>
                            <td>${data.teacher_id}</td>
                            <td>${data.name}</td>
                            <td> <a href="#" onclick="DeleteTeacherDialog('${data.teacher_id}','${data.name}')" class="text-danger" data-toggle="modal" onclick="DeleteDialog(this)" data-target="#confirm-delete">xóa</a>
                            |
                            <a href="#" data-toggle="modal" onclick="UpdateTeacherDialog('${data.teacher_id}','${data.name}')" data-target="#confirm-edit">sửa</a></td>
                        </tr>`
                )
                $("#bodyTable").append(row)
                window.$("#CreateModal").modal("hide");
                $("#teacher_id").val("")
                $("#teacher_name").val("")
            }
        })
    
})

// cập nhật giáo viên
$("#teacher-to-edit-input").click(e => {
    const btn = e.target
    const teacher_id = btn.dataset.id
    const name = $("#edit-teacher-name").val()
    var data = {
        "name": name,
    }
    token = document.cookie.slice(6)
    fetch('https://localhost:44368/api/Teacher/' + teacher_id, {
        method: "PUT",
        headers: {
            'token': token,
            'content-type': 'application/json'
        },
        body: JSON.stringify(data),

    })
        .then(res => res.json())
        .then(data => {
            if (data.code === 400) {

                $("#edit-error").empty();
                $("#edit-error").append(`<div class="alert alert-danger" style="margin-top:5px; padding:10px">Lỗi: ${data.message}</div>`);
            }
            else {
                var indexRow = $("#" + data.teacher_id).find(".index").html(); // lấy ra giá trị của stt dựa vào class = index
                console.log(data.teacher_id)
                let row = $("#"+data.teacher_id).empty()
                console.log(row)
                //console.log(indexRow)
                let data_in_row = $(`
                                <td class="index">${indexRow}</td>
                                <td>${data.teacher_id}</td>
                                <td>${data.name}</td>
                            <td> <a href="#"  onclick="DeleteTeacherDialog('${data.teacher_id}','${data.name}')" class="text-danger" data-toggle="modal" onclick="DeleteDialog(this)" data-target="#confirm-delete">xóa</a>
                            |
                            <a href="#" data-toggle="modal" onclick="UpdateTeacherDialog('${data.teacher_id}','${data.name}')" data-target="#confirm-edit">sửa</a>
                            </td>
                            `)
                row.append(data_in_row)
                window.$("#confirm-edit").modal('hide');
            }
        })

})

// xóa 
$("#teacher-to-delete-input").click(e => {
    const btn = e.target
    const teacher_id = btn.dataset.id
    token = document.cookie.slice(6)
    fetch("https://localhost:44368/api/Teacher/" + teacher_id, {
        method: "DELETE",
        headers: {
            'token': token,
            'content-type': 'application/json'
        },
    }).then(res => res.json())
        .then(data => {
            if (data.code == 400) {
                $("#dalete-error").empty();
                $("#dalete-error").append(`<div class="alert alert-danger" style="margin-top:5px; padding:10px">Lỗi: ${data.message}</div>`);
            }
            else {
                alert(data.message)
                $("#" + teacher_id).empty()
                window.$("#confirm-delete").modal('hide');
            }
        })

})


// search when key up
$("#searchInput").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});