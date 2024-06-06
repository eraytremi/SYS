$(document).ready(function () {
    // Event handler for posting stock
    $("#postProduct").click(function () {

        var formData = new FormData();
        formData.append("Name", $("#productName").val());
        formData.append("Unit", $("#unit").val());
        formData.append("WareHouseId", $("#wareHouseId").val());
        formData.append("SupplierId", $("#supplierId").val());
        formData.append("CategoryId", $("#categoryId").val());
        formData.append("Description", $("#description").val());
        formData.append("Price", $("#price").val());


        var fileInput = document.getElementById('productImage');
        if (fileInput.files.length > 0) {
            formData.append("Picture", fileInput.files[0]);
            console.log("File appended: ", fileInput.files[0]);
        }

        $.ajax({
            url: "/Product/Post",
            method: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.isSuccess) {
                    window.location.href = "/Product/Index";
                } else {
                    Swal.fire({
                        title: 'Ýþlem Baþarýsýz',
                        text: response.messages,
                        icon: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Tamam'
                    });
                }
            }
        });

        Swal.fire({
            title: 'Baþarýlý',
            text: "Ýþlem baþarýlý",
            icon: 'success',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Tamam'
        });
    });

    $("#updateProduct").click(function () {

        var formData = new FormData();
        formData.append("Id", $("#updatedProductId").val());
        formData.append("Name", $("#updatedProductName").val());
        formData.append("WareHouseId", $("#updatedWareHouseId").val());
        formData.append("CategoryId", $("#updatedCategoryId").val());
        formData.append("SupplierId", $("#updatedSupplierId").val());
        formData.append("Description", $("#updatedDescription").val());
        formData.append("Price", $("#updatedPrice").val());
        formData.append("Unit", $("#updatedUnit").val());


        var fileInput = document.getElementById('productImageUpdate');
        if (fileInput.files.length > 0) {
            formData.append("Picture", fileInput.files[0]);
            console.log("File appended: ", fileInput.files[0]);
        }



        $.ajax({
            url: "/Product/Update",
            method: "POST",
            data: formData,
            processData: false, // Ensure jQuery doesn't process the data
            contentType: false, // Ensure jQuery sets the content type correctly
            success: function (response) {
                if (response.isSuccess) {
                    window.location.href = "/Product/Index";
                } else {
                    Swal.fire({
                        title: 'Ýþlem Baþarýsýz',
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
                    text: 'Güncelleme sýrasýnda bir hata oluþtu.',
                    icon: 'error',
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Tamam'
                });
            }
        });
    });

});


// Event handler for deleting supplier
$('.btnDelete').click(function () {
    var id = $(this).attr('productId');
    deleteSupplier(id);
});

// Function to delete supplier
function deleteSupplier(id) {
    Swal.fire({
        text: "Stoðu silmek istiyor musunuz?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Product/Delete/' + id,
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
                                window.location.href = "/Product/Index";
                            }
                        });
                    } else {
                        Swal.fire({
                            title: 'Ýþlem Baþarýsýz',
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
