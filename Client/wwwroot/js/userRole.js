

$(document).ready(function () {
    $("#postUserRole").click(function () {
        var userId = $("#userId").val();
        var roleId = $("#roleId").val();

        if (userId.length > 0 && roleId.length > 0) {
            var formDataObject = {
                UserId: userId,
                RoleId: roleId
            };
            console.log(formDataObject);

            $.ajax({
                url: "/UserRole/Post",
                method: "POST",
                data: formDataObject,
                success: function (response) {
                    if (response.isSuccess) {
                        window.location.href = "/UserRole/Index";
                    } else {
                        Swal.fire({
                            title: 'İşlem Başarısız',
                            text: response.messages,
                            icon: 'error',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam'
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        title: 'Hata',
                        text: 'Sunucu ile bağlantı kurulurken bir hata oluştu.',
                        icon: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Tamam'
                    });
                }
            });
        } else {
            Swal.fire({
                title: 'Uyarı',
                text: "Alanlar boş bırakılamaz.",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Tamam'
            });
        }
    });


    $("#updateUserRole").click(function () {
        var userRoleId = $("#updatedUserRoleId").val();
        var userId = $("#updatedUserId").val();
        var roleId = $("#updatedRoleId").val(); 

        
        if (userId.length > 0 && roleId.length>0) {
            
            var formDataObject = {
                Id: userRoleId,
                UserId: userId,
                RoleId: roleId
            };
            // AJAX isteği gönder
            $.ajax({
                url: "/UserRole/Update", 
                method: "POST",
                data: formDataObject, 
                success: function (response) {
                  
                    if (response.isSuccess) {
                      
                        window.location.href = "/UserRole/Index";
                    } else {
                        // Başarısız ise hata mesajını göster
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
        } else {
            // Boş bir ad girdiyse uyarı ver
            Swal.fire({
                title: 'Uyarı',
                text: "Alanları boş bırakamazsın.",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Tamam'
            });
        }
    });

    $('.btnDelete').click(function () {
        var id = $(this).attr('userRoleId');
        deleteUserRole(id);
    });

});
function deleteUserRole(id) {

    Swal.fire({
        text: "Rolü silmek istiyor musunuz?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/UserRole/Delete/' + id,
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
                                window.location.href = "/UserRole/Index";
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