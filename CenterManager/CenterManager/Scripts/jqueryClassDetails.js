
// load class details by id
var url = window.location.pathname;
var classID = url.substring(url.lastIndexOf('/') + 1);
if (classID.length == 0)
    document.location.href = '/Home/Class'
var className;
// get class name
fetch("https://localhost:44368/api/Class/" + classID, {
    method: "GET",
    headers: {
        'content-type': 'application/json'
    },
})
    .then(res => res.json())
    .then(data => {
        if (data.code != 200) {
            document.location.href = '/Home/Class'
            alert("Không tìm thấy mã lớp học " + classID)
        }
        else {
            className = data.data.class_name;
            $("#class-id-name").html(classID + " - " + className);
        }

    })

// GET - /api/ClassDetails/5
var currPage
loadPage(1)
function loadPage(page) {
    currPage = page
    fetch("https://localhost:44368/api/ClassDetails/" + classID + "?page=" + page, {
        method: "GET",
        headers: {
            'content-type': 'application/json'
        },
    })
        .then(res => res.json())
        .then(data => {
            $("tbody").empty()
            for (var i = 0; i < data.data.length; i++) {
                var htmlLoad = $(
                    `<tr id="${data.data[i].student_id}">
                        <td class="text-center index">${(i + 1) + ((page - 1) * 10)}</td>
                        <td>${data.data[i].student_id}</td>
                        <td>${data.data[i].student_name}</td>
                        <td>${data.data[i].student_birthYear}</td>
                        <td>
                            <a href="#" class="text-danger" onclick="updateDeleteDialog('${data.data[i].student_id}', '${data.data[i].student_name}')" data-toggle="modal" data-target="#confirm-delete">xóa</a>
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
        })

}
//------ ADD ------
function addStudent() {
    // get data
    var s_id = $("#add-student-id").val();
    var data = {
        "class_id": classID,
        "student_id": s_id
    }

    // request add: POST - api/ClassDetails
    fetch("https://localhost:44368/api/ClassDetails/", {
        method: "POST",
        headers: {
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
                        <td>${data.data.student_id}</td>
                        <td>${data.data.student_name}</td>
                        <td>${data.data.student_birthYear}</td>
                        <td>
                            <a href="#" class="text-danger" onclick="updateDeleteDialog('${data.data.student_id}', '${data.data.student_name}')" data-toggle="modal" data-target="#confirm-delete">xóa</a>
                        </td>
                    </tr>`
                )
                $("tbody").append(newRow)
                updateRowIndex();
                // reset form
                $("#add-student-id").val("");
                $("#add-error").empty();
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
function deleteClassDetails() {
    //get id
    var s_id = $("#class-to-delete-input").attr("data-id");
    var data = {
        "class_id": classID,
        "student_id": s_id
    }
    // request delete: DELETE - api/ClassDetails/
    fetch("https://localhost:44368/api/ClassDetails/", {
        method: "DELETE",
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(data),
    })
        .then(res => res.json())
        .then(data => {
            if (data.code === 200) {
                // delete success
                $("#" + s_id).remove();
                $("#confirm-delete").modal('hide');
                //  update row index
                updateRowIndex();
            } else {
                // delete failed
                $("#confirm-delete").modal('hide');
                alert("Xóa không thành công: " + data.message);
            }
        })
}


//------ SUPPORT
// update row index
function updateRowIndex() {
    $("table tbody tr").each(function () {
        $(this).find(".index").html(($(this).index() + 1) + ((currPage - 1) * 5));
    });
}

// search when key up
$("#searchInput").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});

// search in option
/*$('select').selectize({
    sortField: 'text'
});*/