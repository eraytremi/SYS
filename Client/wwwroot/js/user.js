

$(document).ready(function () {
    $("#postUser").click(function () {
        var formDataObject =
        {
            Name: $("#name").val(),
            Mail: $("#mail").val(),
            Password: $("#password").val()
        };
        if ($("#name").val().length > 0) {
            $.ajax({
                url: "/User/RegisterUser",
                method: "POST",
                data: formDataObject,
                success: function (response) {
                    if (response.isSuccess) {
                        window.location.href = "/User/Index";
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
                text: "Kullanıcı ismi giriniz.",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Tamam'
            });
        }
    });


    $("#updateUser").click(function () {
        var userId = $("#updatedUserId").val();
        var newName = $("#updatedUserName").val();
        var mail = $("#updatedMail").val();
        var password = $("#updatedPassword").val();

        if (newName.length > 0) {

            var formDataObject = {
                Id: userId,
                Name: newName,
                Mail: mail,
                Password: password
            };
            // AJAX request
            $.ajax({
                url: "/User/Update", // Update endpoint
                method: "POST",
                data: formDataObject, // Form data
                success: function (response) {
                    // Process response from server
                    if (response.isSuccess) {
                        // If successful, reload the page
                        window.location.href = "/User/Index";
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
        var id = $(this).attr('userId');

        deleteSupplier(id);
    });

});
function deleteSupplier(id) {

    Swal.fire({
        text: "Kullanıcıyı silmek istiyor musunuz?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/User/Delete/' + id,
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
                                window.location.href = "/User/Index";
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