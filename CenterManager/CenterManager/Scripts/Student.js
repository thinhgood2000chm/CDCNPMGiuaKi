function UpdateDialog(student_id, name, birthYear) {// phải sử dụng onclick trong th render html từ jquery ==> ko gọi được jquery 
    let label = $('#student-to-edit');
    let param = $('#student-to-edit-input');
    label.html(student_id + " - " + name);
    param.attr("data-id", student_id);// set attribute vào button cập nhật để gửi được id đi
    $("#edit-student-name").val(name);
    $("#edit-student-year").val(birthYear);
  
}
function DeleteDialog(student_id, student_name) {
    let label = $('#student-to-delete');
    let param = $('#student-to-delete-input');
    label.html(student_id + " - " + student_name);
    param.attr("data-id", student_id);// set attribute vào button cập nhật để gửi được id đi
}
loadPage(1)
function loadPage(page) {
    fetch("https://localhost:44368/api/Student?page="+ page, {
        method: "GET",
        headers: {
            'content-type': 'application/json'
        },
    })
        .then(res => res.json())
        .then(a => {
            var data = a.data
            $("#bodyTable").empty()
            for (var i = 0; i < data.length; i++) {
                var htmlLoad = $(
                    `<tr id = ${data[i].student_id}>
                            <td class ="index">${data[i].id}</td>
                            <td>${data[i].student_id}</td>
                            <td>${data[i].name}</td>
                            <td>${data[i].birthYear}</td>
                            <td> <a href="#" onclick="DeleteDialog('${data[i].student_id}','${data[i].name}')" class="text-danger" data-toggle="modal" onclick="DeleteDialog(this)" data-target="#confirm-delete">xóa</a>
                                |
                                <a href="#" data-toggle="modal" onclick="UpdateDialog('${data[i].student_id}','${data[i].name}', '${data[i].birthYear}')" data-target="#confirm-edit">sửa</a></td>
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

$("#CreateStudent").click(e => {


    var student_id = $("#student_id").val()
    var student_name = $("#student_name").val()
    var student_birthYear = $("#student_birthYear").val()
    //console.log(student_id, student_name, student_birthYear)
    var data = {
        "student_id": student_id,
        "name": student_name,
        "birthYear": student_birthYear
    }
    fetch("https://localhost:44368/api/Student", {
        method: "POST",
        headers: {
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

                $("#edit-error").empty();
                $("#edit-error").append(`<div class="alert alert-danger" style="margin-top:5px; padding:10px">Lỗi: ${data.message}</div>`);
            }
            else {

                var row = $(
                    `<tr  id = ${data.student_id}>
                                    <td class ="index">${data.id}</td>
                                   <td>${data.student_id}</td>
                                   <td>${data.name}</td>
                                 <td>${data.year}</td>
                                <td> <a href="#"  onclick="DeleteDialog('${data.student_id}','${data.name}')" class="text-danger" data-toggle="modal" onclick="DeleteDialog(this)" data-target="#confirm-delete">xóa</a>
                                |
                                <a href="#" data-toggle="modal" onclick="UpdateDialog('${data.student_id}','${data.name}', '${data.year}')" data-target="#confirm-edit">sửa</a>
                               </td>
                             </tr>`
                )
                $("#bodyTable").append(row)
                window.$("#CreateModal").modal("hide");
                $("#student_id").val("")
                $("#student_name").val("")
                $("#student_birthYear").val("")
            }
        })

})

$("#student-to-edit-input").click(e => {
    const btn = e.target
    const student_id = btn.dataset.id
    const name = $("#edit-student-name").val()
    const birthYear = $("#edit-student-year").val()
    //console.log(name, birthYear)
    var data = {
        "name": name,
        "birthYear": birthYear
    }
    fetch('https://localhost:44368/api/Student/' + student_id, {
        method: "PUT",
        headers: {
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
                var indexRow = $("#" + data.student_id).find(".index").html(); // lấy ra giá trị của stt dựa vào class = index
                let row = $("#" + data.student_id).empty()
                
                console.log(indexRow)
                let data_in_row = $(`
                                <td class="index">${indexRow}</td>
                                <td>${data.student_id}</td>
                                <td>${data.name}</td>
                                <td>${data.year}</td>
                            <td> <a href="#"  onclick="DeleteDialog('${data.student_id}','${data.name}')" class="text-danger" data-toggle="modal" onclick="DeleteDialog(this)" data-target="#confirm-delete">xóa</a>
                            |
                            <a href="#" data-toggle="modal" onclick="UpdateDialog('${data.student_id}','${data.name}', '${data.year}')" data-target="#confirm-edit">sửa</a>
                            </td>
                            `)
                row.append(data_in_row)
            }
        })
    $("#confirm-edit").modal('hide');
})

$("#student-to-delete-input").click(e => {
    const btn = e.target
    const student_id = btn.dataset.id
    fetch("https://localhost:44368/api/Student/" + student_id, {
        method: "DELETE",
        headers: {
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
            }
        })
    $("#" + student_id).empty()
    $("#confirm-delete").modal('hide');
})



// search when key up
$("#searchInput").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});