var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/book",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "30%" },
            { "data": "author", "width": "30%" },
            { "data": "isbn", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/BookList/Upsert?id=${data}" class="btn btn-info btn-sm text-white" style="cursor:pointer; width:100px;">Edit<a/>

                        <a class="btn btn-danger btn-sm text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/book?id='+${data})>Delete<a/>
                    <div/>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    console.log("I'm HERE WRYYYY");
    swal({
        title: "Are you shure?",
        text: "Once deleted, you will not be able to recover",
        //icon: "warning",   
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: (data) => {
                    if (data.success) {
                        //library from links in _layout; 
                        //message - because we have return Json(new { success = true, message = "Deleted success" }) in controller;
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }

            });
        }
    });
}