

$(document).ready(function () {
    $("#postSupplier").click(function () {
        console.log($("#description").val())
        var formDataObject =
        {
            Name: $("#supplierName").val(),
            Description: $("#description").val(),
            Category: $("#category").val()
        };
        console.log(formDataObject)
        if ($("#supplierName").val().length > 0 && $("#category").val().length > 0) {
            $.ajax({
                url: "/Supplier/PostSupplier",
                method: "POST",
                data: formDataObject,
                success: function (response) {
                    if (response.isSuccess) {
                        window.location.href = "/Supplier/Index";
                    }
                    else {
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
        }
        else {
            Swal.fire({
                title: 'Uyarý',
                text: "Tedarikçi ismi giriniz.",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Tamam'
            });
        }
    });


    $("#updateSupplier").click(function () {
        var supplierId = $("#updatedSupplierId").val();
        var newName = $("#updatedSupplierName").val();
        var description = $("#updatedDescription").val(); 
        var  category= $("#updatedCategory").val()


        if (newName.length > 0) {
         
            var formDataObject = {
                Id: supplierId,
                Name: newName,
                Description: description,
                Category: category
            }
            $.ajax({
                url: "/Supplier/UpdateSupplier", 
                method: "POST",
                data: formDataObject,
                success: function (response) {
                   
                    if (response.isSuccess) {
                      
                        window.location.href = "/Supplier/Index";
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
        } else {
   -
            Swal.fire({
                title: 'Uyarý',
                text: "Tedarikçi ismi giriniz.",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Tamam'
            });
        }
    });

    $('.btnDelete').click(function () {
        var id = $(this).attr('supplierId');

        deleteSupplier(id);
    });

});
function deleteSupplier(id) {

    Swal.fire({
        text: "Tedarikçiyi silmek istiyor musunuz?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Supplier/DeleteSupplier/' + id,
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
                                window.location.href = "/Supplier/Index";
                            };
                        });
                    }
                    else {
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