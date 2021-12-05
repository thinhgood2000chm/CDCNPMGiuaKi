var currPage
loadPage(1)
// load all classes
function loadPage(page) {
    currPage = page
    token = document.cookie.slice(6)
    fetch("https://localhost:44368/api/class?page=" + page, {
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
                document.location.href = '/'
            } else {
                $("tbody").empty();
                for (var i = 0; i < data.data.length; i++) {
                    var htmlLoad = $(
                        // index = (i + 1) + ((page - 1) * sizeOfPage)
                        `<tr id="${data.data[i].class_id}">
                            <td class="text-center index">${(i + 1) + ((page - 1) * 10)}</td>
                            <td>${data.data[i].class_id}</td>
                            <td>${data.data[i].class_name}</td>
                            <td>${data.data[i].subject_id}</td>
                            <td>${data.data[i].subject_name}</td>
                            <td>${data.data[i].teacher_id}</td>
                            <td>${data.data[i].teacher_name}</td>
                            <td>
                                <a href="#" class="text-danger" onclick="updateDeleteDialog('${data.data[i].class_id}', '${data.data[i].class_name}')" data-toggle="modal" data-target="#confirm-delete">xóa</a>
                                |
                                <a href="#" data-toggle="modal" onclick="updateEditDialog('${data.data[i].class_id}', '${data.data[i].class_name}', '${data.data[i].subject_id}', '${data.data[i].teacher_id}')" data-target="#confirm-edit">sửa</a>
                                |
                                <a href="ClassDetails/${data.data[i].class_id}" class="text-info">danh sách học viên</a>
                            </td>
                        </tr>`
                    )
                    $("tbody").append(htmlLoad)
                }
                // paging
                var maxPage = data.maxPage
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
                //
            }

        })
}
//------ ADD ------
function addClass() {
    // get data
    var id = $("#add-class-id").val();
    var name = $("#add-class-name").val();
    var s_id = $("#add-subject-id").val();
    var t_id = $("#add-teacher-id").val();
    var data = {
        "class_id": id,
        "name": name,
        "subject_id": s_id,
        "teacher_id": t_id
    }
    token = document.cookie.slice(6)
    // request add: POST - api/Class
    fetch("https://localhost:44368/api/Class/", {
        method: "POST",
        headers: {
            'token': token,
            'content-type': 'application/json'
        },
        body: JSON.stringify(data),
    })
        .then(res => res.json())
        .then(data => {
            if (data.code != 200) {
                // add fail
                $("#add-error").empty();
                $("#add-error").append(`<div class="alert alert-danger" style="margin-top:5px; padding:10px">Lỗi: ${data.message}</div>`);
            }
            else {
                // add success
                var rowNumber = $('tbody tr').length + 1;
                var newRow = $(
                    `<tr id="${data.data.class_id}">
                        <td class="text-center index">${rowNumber}</td>
                        <td>${data.data.class_id}</td>
                        <td>${data.data.class_name}</td>
                        <td>${data.data.subject_id}</td>
                        <td>${data.data.subject_name}</td>
                        <td>${data.data.teacher_id}</td>
                        <td>${data.data.teacher_name}</td>
                        <td>
                            <a href="#" class="text-danger" onclick="updateDeleteDialog('${data.data.class_id}', '${data.data.class_name}')" data-toggle="modal" data-target="#confirm-delete">xóa</a>
                            |
                            <a href="#" data-toggle="modal" onclick="updateEditDialog('${data.data.class_id}', '${data.data.class_name}', '${data.data.subject_id}', '${data.data.teacher_id}')" data-target="#confirm-edit">sửa</a>
                            |
                            <a href="ClassDetails/${data.data.class_id}" class="text-info">danh sách học viên</a>
                        </td>
                    </tr>`
                )
                $("tbody").append(newRow)
                updateRowIndex();
                // reset form
                $("#add-class-id").val("");
                $("#add-class-name").val("");
                $("#add-subject-id").val("");
                $("add-teacher-id").val("");
                $("#add-error").empty();
                // close dialog
                window.$("#confirm-add").modal('hide');
            }
        })
}


//------ DELETE ------
//update delete dialog
function updateDeleteDialog(id, name) {
    let label = $('#class-to-delete');
    let param = $('#class-to-delete-input');
    label.html(id + " - " + name);
    param.attr("data-id", id);
}
// delete class
function deleteSubject() {
    //get id
    var id = $("#class-to-delete-input").attr("data-id");
    token = document.cookie.slice(6)
    // request delete: DELETE - api/class/5
    fetch("https://localhost:44368/api/class/" + id, {
        method: "DELETE",
        headers: {
            'token': token,
            'content-type': 'application/json'

        },
    })
        .then(res => res.json())
        .then(data => {
            if (data.code === 200) {
                // delete success
                $("#" + id).remove();
                $("#confirm-delete").modal('hide');
                //  update row index
                updateRowIndex();
            } else {
                // delete failed
                window.$("#confirm-delete").modal('hide');
                alert("Xóa không thành công: " + data.message);
            }
        })
}


//------ EDIT ------
// update edit dialog
function updateEditDialog(id, name, s_id, t_id) {
    let label = $('#class-to-edit');
    let param = $('#class-to-edit-input');
    label.html(id + " - " + name);
    param.attr("data-id", id);

    $("#edit-class-name").val(name);
    $("#edit-subject-id").val(s_id);
    $("#edit-teacher-id").val(t_id);
}

// edit class
function editClass() {
    // get data
    var id = $("#class-to-edit-input").attr("data-id");
    var name = $("#edit-class-name").val();
    var s_id = $("#edit-subject-id").val();
    var t_id = $("#edit-teacher-id").val();
    var data = {
        "class_id": id,
        "name": name,
        "subject_id": s_id,
        "teacher_id": t_id
    }
    token = document.cookie.slice(6)
    // request edit: PUT - api/Class/5
    fetch("https://localhost:44368/api/Class/" + id, {
        method: "PUT",
        headers: {
            'token': token,
            'content-type': 'application/json'
        },
        body: JSON.stringify(data),
    })
        .then(res => res.json())
        .then(data => {
            if (data.code != 200) {
                // edit fail
                $("#edit-error").empty();
                $("#edit-error").append(`<div class="alert alert-danger" style="margin-top:5px; padding:10px">Lỗi: ${data.message}</div>`);
            }
            else {
                // edit success
                // update row
                var indexRow = $("#" + id).find(".index").html();
                var updateRow = $(
                    `
                    <td class="text-center index">${indexRow}</td>
                    <td>${data.data.class_id}</td>
                    <td>${data.data.class_name}</td>
                    <td>${data.data.subject_id}</td>
                    <td>${data.data.subject_name}</td>
                    <td>${data.data.teacher_id}</td>
                    <td>${data.data.teacher_name}</td>
                    <td>
                        <a href="#" class="text-danger" onclick="updateDeleteDialog('${data.data.class_id}', '${data.data.class_name}')" data-toggle="modal" data-target="#confirm-delete">xóa</a>
                        |
                        <a href="#" data-toggle="modal" onclick="updateEditDialog('${data.data.class_id}', '${data.data.class_name}', '${data.data.subject_id}', '${data.data.teacher_id}')" data-target="#confirm-edit">sửa</a>
                        |
                        <a href="ClassDetails/${data.data.class_id}" class="text-info">danh sách học viên</a>
                    </td>
                    `
                )

                window.$("#confirm-edit").modal('hide');
                $("#edit-error").empty();
                $("#" + id).empty();
                $("#" + id).append(updateRow);
            }
        })
}


//------ SUPPORT
// update row index
function updateRowIndex() {
    $("table tbody tr").each(function () {
        $(this).find(".index").html(($(this).index() + 1) + ((currPage - 1) * 10));
    });
}

// search when key up
$("#searchInput").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});

function getAllSubjects() {
    
}

function getAllTeachers() {

}

// search in option
/*$('select').selectize({
    sortField: 'text'
});*/