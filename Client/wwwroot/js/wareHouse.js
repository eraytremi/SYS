

$(document).ready(function () {
    $("#postWareHouse").click(function () {
        var formDataObject =
        {
            Name: $("#name").val(),
        };
        if ($("#name").val().length > 0) {
            $.ajax({
                url: "/WareHouse/Post",
                method: "POST",
                data: formDataObject,
                success: function (response) {
                    if (response.isSuccess) {
                        window.location.href = "/WareHouse/Index";
                    }
                    else {
                        Swal.fire({
                            title: 'İşlem Başarısız',
                            text: response.messages,
                            icon: 'error',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam'
                        });
                    }
                }
            });
        }
        else {
            Swal.fire({
                title: 'Uyarı',
                text: "Depo ismi giriniz.",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Tamam'
            });
        }
    });


    $("#updateWareHouse").click(function () {
        var Id = $("#updatedWareHouseId").val();
        var newName = $("#updatedName").val();

        if (newName.length > 0) {

            var formDataObject = {
                Id: Id,
                Name: newName, 
            };
            // AJAX request
            $.ajax({
                url: "/WareHouse/Update", // Update endpoint
                method: "POST",
                data: formDataObject, // Form data
                success: function (response) {
                    // Process response from server
                    if (response.isSuccess) {
                        // If successful, reload the page
                        window.location.href = "/WareHouse/Index";
                    } else {
                        // If failed, show error message
                        Swal.fire({
                            title: 'İşlem Başarısız',
                            text: response.messages,
                            icon: 'error',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'OK'
                        });
                    }
                }
            });
        } else {
            // If name is empty, show warning
            Swal.fire({
                title: 'Warning',
                text: "İism Giriniz.",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'OK'
            });
        }
    });

    $('.btnDelete').click(function () {
        var id = $(this).attr('wareHouseId');
        deleteWareHouse(id);
    });

});
function deleteWareHouse(id) {

    Swal.fire({
        text: "Depoyu silmek istiyor musunuz?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/WareHouse/Delete/' + id,
                method: 'delete',
                dataType: 'json',
                success: function (response) {
                    if (response.isSuccess) {
                        Swal.fire({

                            text: response.message,
                            icon: 'success',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.href = "/WareHouse/Index";
                            };
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'İşlem Başarısız',
                            text: response.message,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam',
                            icon: 'error',
                        });
                    }
                }
            });
        }
    });
}