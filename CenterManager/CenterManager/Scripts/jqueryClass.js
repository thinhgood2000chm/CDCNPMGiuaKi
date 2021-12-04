﻿
// load all classes
fetch("https://localhost:44368/api/class", {
    method: "GET",
    headers: {
        'content-type': 'application/json'
    },
})
    .then(res => res.json())
    .then(data => {
        for (var i = 0; i < data.data.length; i++) {
            var htmlLoad = $(
                `<tr id="${data.data[i].class_id}">
                    <td class="text-center index">${i + 1}</td>
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
                        <a href="#" class="text-info">danh sách học viên</a>
                    </td>
                </tr>`
            )
            $("tbody").append(htmlLoad)
        }

    })


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

    // request add: POST - api/Class
    fetch("https://localhost:44368/api/Class/", {
        method: "POST",
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(data),
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)
            if (data.code === 400) {
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
                            <a href="#" class="text-info">danh sách học viên</a>
                        </td>
                    </tr>`
                )
                $("tbody").append(newRow)
                // reset form
                $("#add-class-id").val("");
                $("#add-class-name").val("");
                $("#add-subject-id").val("");
                $("add-teacher-id").val("");
                // close dialog
                $("#confirm-add").modal('hide');
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

    // request delete: DELETE - api/class/5
    fetch("https://localhost:44368/api/class/" + id, {
        method: "DELETE",
        headers: {
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
            }
            if (data.code === 400) {
                // delete failed
                $("#confirm-delete").modal('hide');
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

    // request edit: PUT - api/Class/5
    fetch("https://localhost:44368/api/Class/" + id, {
        method: "PUT",
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(data),
    })
        .then(res => res.json())
        .then(data => {
            console.log(data)
            if (data.code === 400) {
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
                        <a href="#" class="text-info">danh sách học viên</a>
                    </td>
                    `
                )

                $("#confirm-edit").modal('hide');
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
        $(this).find(".index").html($(this).index() + 1);
    });
}

function getAllSubjectsID() {
    
}

function getAllTeachersID() {

}