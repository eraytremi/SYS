$(document).ready(function () {
    // Event handler for posting stock
    $("#postProduct").click(function () {
       
        var formDataObject = {
            Name: $("#productName").val(),
            Unit: $("#unit").val(),
            WareHouseId: $("#wareHouseId").val(),
            SupplierId: $("#supplierId").val(),
            CategoryId: $("#categoryId").val(),
            Description: $("#description").val(),
            Price: $("#price").val()
        };

        $.ajax({
            url: "/Product/Post",
            method: "POST",
            data: formDataObject,
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

        var productId = $("#updatedProductId").val();
        var updatedWareHouseId = $("#updatedWareHouseId").val();
        var updatedProductName = $("#updatedProductName").val();
        var updatedCategoryId = $("#updatedCategoryId").val();
        var updatedSupplierId = $("#updatedSupplierId").val();
        var updatedDescription = $("#updatedDescription").val();
        var updatedPrice = $("#updatedPrice").val();
        var updatedUnit = $("#updatedUnit").val();

        var formDataObject = {
            Id: productId,
            Name: updatedProductName,
            WareHouseId: updatedWareHouseId,
            CategoryId:updatedCategoryId,
            SupplierId: updatedSupplierId,
            Description: updatedDescription,
            Price: updatedPrice,
            Unit: updatedUnit
        };
        $.ajax({
            url: "/Product/Update",
            method: "POST",
            data: formDataObject,
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
