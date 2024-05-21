$(document).ready(function () {
    // Event handler for posting stock
    $("#postStock").click(function () {
        var stockAction = $('input[name="stockAction"]:checked').val();
        var formDataObject = {
            ProductId: $("#productId").val(),
            Unit: $("#unit").val(),
            Quantity: $("#quantity").val(),
            Destination: $("#destination").val(),
            Source: $("#source").val(),
            IsEntry: stockAction === "true"
        };

        $.ajax({
            url: "/Stock/Post",
            method: "POST",
            data: formDataObject,
            success: function (response) {
                if (response.isSuccess) {
                    window.location.href = "/Stock/Index";
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

    $("#updateStock").click(function () {
       
        var updatedQuantity = $("#updatedQuantity").val();
        var stockId = $("#updatedStockId").val();
        var updatedProductId = $("#updatedProductId").val();


        
            var formDataObject = {
                Id: stockId,
                ProductId: updatedProductId,
                Quantity: updatedQuantity,
            };
            $.ajax({
                url: "/Stock/Update",
                method: "POST",
                data: formDataObject,
                success: function (response) {
                    if (response.isSuccess) {
                        window.location.href = "/Stock/Index";
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
    var id = $(this).attr('stockId');
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
                url: '/Stock/Delete/' + id,
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
                                window.location.href = "/Stock/Index";
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
