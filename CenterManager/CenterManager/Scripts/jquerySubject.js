var currPage
loadPage(1)
// load all subjects on load
function loadPage(page) {
    currPage = page
    token = document.cookie.slice(6)
    fetch("https://localhost:44368/api/subject?page=" + page, {
        method: "GET",
        headers: {
            'token': token,
            'content-type': 'application/json'
        },
    })
        .then(res => res.json())
        .then(data => {
            $("tbody").empty()
            for (var i = 0; i < data.data.length; i++) {
                var htmlLoad = $(
                    `<tr id="${data.data[i].subject_id}">
                        <td class="text-center index">${(i + 1) + ((page-1)*10)}</td>
                        <td>${data.data[i].subject_id}</td>
                        <td>${data.data[i].name}</td>
                        <td>
                            <a href="#" class="text-danger" onclick="updateDeleteDialog('${data.data[i].subject_id}', '${data.data[i].name}')" data-toggle="modal" data-target="#confirm-delete">xóa</a>
                            |
                            <a href="#" data-toggle="modal" onclick="updateEditDialog('${data.data[i].subject_id}', '${data.data[i].name}')" data-target="#confirm-edit">sửa</a>
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

        })
}

//------ ADD ------
function addSubject() {
    // get data
    var id = $("#add-subject-id").val();
    var name = $("#add-subject-name").val();
    var data = {
        "subject_id": id,
        "name": name
    }
    token = document.cookie.slice(6)
    // request add: POST - api/Subject
    fetch("https://localhost:44368/api/Subject/", {
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
                var rowNumber = $('tbody tr').length+1;
                var newRow = $(
                    `<tr id="${data.data.subject_id}">
                        <td class="text-center index">${rowNumber}</td>
                        <td>${data.data.subject_id}</td>
                        <td>${data.data.name}</td>
                        <td>
                            <a href="#" class="text-danger" onclick="updateDeleteDialog('${data.data.subject_id}', '${data.data.name}')" data-toggle="modal" data-target="#confirm-delete">xóa</a>
                            |
                            <a href="#" data-toggle="modal" onclick="updateEditDialog('${data.data.subject_id}', '${data.data.name}')" data-target="#confirm-edit">sửa</a>
                        </td>
                    </tr>`
                )
                $("tbody").append(newRow)
                updateRowIndex();
                // reset form
                $("#add-subject-id").val("");
                $("#add-subject-name").val("");
                $("#add-error").empty();
                // close dialog
                window.$("#confirm-add").modal('hide');
            }
        })
}


//------ DELETE ------
//update delete dialog
function updateDeleteDialog(id, name) {
    let label = $('#subject-to-delete');
    let param = $('#subject-to-delete-input');
    label.html(id + " - " + name);
    param.attr("data-id", id);
}
// delete subject
function deleteSubject() {
    //get id
    var id = $("#subject-to-delete-input").attr("data-id");
    token = document.cookie.slice(6)
    // request delete: DELETE - api/subject/5
    fetch("https://localhost:44368/api/subject/"+id, {
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
        }
        else {
            // delete failed
            window.$("#confirm-delete").modal('hide');
            alert("Xóa không thành công: " + data.message);
        }
    })
}


//------ EDIT ------
// update edit dialog
function updateEditDialog(id, name) {
    let label = $('#subject-to-edit');
    let param = $('#subject-to-edit-input');
    label.html(id + " - " + name);
    param.attr("data-id", id);
    $("#edit-subject-name").val(name);
}

// edit subject
function editSubject() {
    // get data
    var id = $("#subject-to-edit-input").attr("data-id");
    var name = $("#edit-subject-name").val();
    var data = {
        "subject_id": id,
        "name": name
    }
    token = document.cookie.slice(6)
    // request edit: PUT - api/Subject/5
    fetch("https://localhost:44368/api/Subject/"+id, {
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
                <td>${data.data.subject_id}</td>
                <td>${data.data.name}</td>
                <td>
                    <a href="#" class="text-danger" onclick="updateDeleteDialog('${data.data.subject_id}', '${data.data.name}')" data-toggle="modal" data-target="#confirm-delete">xóa</a>
                    |
                    <a href="#" data-toggle="modal" onclick="updateEditDialog('${data.data.subject_id}', '${data.data.name}')" data-target="#confirm-edit">sửa</a>
                </td>
                `
            )

            window. $("#confirm-edit").modal('hide');
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

