var dataTable;
var deleteBtn;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/GuestBooks/GetAll"
        },
        "columns": [
            { "data": "guestBookFirstName", "width": "15%" },
            { "data": "guestBookLastName", "width": "15%" },
            { "data": "guestBookEmail", "width": "15%" },
            { "data": "guestType.guestTypeName", "width": "15%" },
            { "data": "alienType.alienTypeName", "width": "15%" },
            { "data": "guestBookDescription", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-100 align-middle btn-group" style="text-align: right" role="group">
                            <a href="/Admin/GuestBooks/Upsert?id=${data}"
                                <button type="button" class="btn btn-primary m-0" role="button">
                                    <i class="bi bi-pencil-square"></i> Edit
                                </button>
                            </a>
                        </div>
                        `
                },
                "width":  "15%"
            },
        ]
    });
   
   
};

//function Delete(url) {
//    Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: url,
//                type: 'DELETE',
//                success: function (data) {
//                    if (data.error) {
//                        toastr.error(data.message);
//                    }
                    
//                }
//            })
//        }
//    })
//};


//{ "data": [{ "id": 1, "firstName": "Eric", "lastName": "Tingler", "email": "erictingler@gmail.com", "guestTypeId": 4, "guestType": { "id": 4, "guestTypeName": "Archnemesis", "guestTypeDisplayOrder": 4, "guestTypeCreatedDateTime": "2022-06-04T21:58:06.2638304" }, "alienTypeId": 2, "alienType": { "id": 2, "alienTypeName": "I want to believe", "alienTypeDateTimeCreated": "0001-01-01T00:00:00" } }] }