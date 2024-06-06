$(document).ready(function () {


    $("#postCategory").click(function () {
        var formData = new FormData();
        formData.append("Name", $("#categoryName").val());
        var fileInput = document.getElementById('categoryImage');
        if (fileInput.files.length > 0) {
            formData.append("Picture", fileInput.files[0]);
            console.log("File appended: ", fileInput.files[0]);
        }
        
        $.ajax({
            url: "/Category/Post",
            method: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.isSuccess) {
                    window.location.href = "/Category/Index";
                } else {
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

        Swal.fire({
            title: 'Başarılı',
            text: "İşlem başarılı",
            icon: 'success',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Tamam'
        });
    });

    $("#updateCategory").click(function () {
        var formData = new FormData();
        formData.append("Id", $("#updatedCategoryId").val());
        formData.append("Name", $("#updatedCategoryName").val());
        var fileInput = document.getElementById('categoryImageUpdate');
        if (fileInput.files.length > 0) {
            formData.append("Picture", fileInput.files[0]);
            console.log("File appended: ", fileInput.files[0]);
        }
        $.ajax({
            url: "/Category/Update",
            method: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.isSuccess) {
                    window.location.href = "/Category/Index";
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
            error: function (error) {
                Swal.fire({
                    title: 'Hata',
                    text: 'Güncelleme sırasında bir hata oluştu.',
                    icon: 'error',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Tamam'
                });
            }
        });
    });

    $('.btnDelete').click(function () {
        var id = $(this).attr('categoryId');
        deleteSupplier(id);
    });

    function deleteSupplier(id) {
        Swal.fire({
            text: "Stoğu silmek istiyor musunuz?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sil!',
            cancelButtonText: 'Vazgeç!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Category/Delete/' + id,
                    method: 'DELETE',
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
                                    window.location.href = "/Category/Index";
                                }
                            });
                        } else {
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
});