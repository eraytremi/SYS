

$(document).ready(function () {
    $("#postSupplier").click(function () {
        var formDataObject =
        {
            Name: $("#supplierName").val(),
            Description: $("#description").val()
        };
        console.log(formDataObject)
        if ($("#supplierName").val().length > 0) {
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
        // Güncellenecek tedarikçinin Id'sini ve yeni adýný al
        var supplierId = $("#updatedSupplierId").val();
        var newName = $("#updatedSupplierName").val();
        var description = $("updatedDescription").val();
        // Form verilerini kontrol et
        if (newName.length > 0) {
            // Form verilerini bir JavaScript nesnesi olarak oluþtur
            var formDataObject = {
                Id: supplierId,
                Name: newName,
                Description: description
            };
            // AJAX isteði gönder
            $.ajax({
                url: "/Supplier/UpdateSupplier", // Güncelleme endpoint'i
                method: "POST",
                data: formDataObject, // Form verileri
                success: function (response) {
                    // Sunucudan gelen yanýtý iþle
                    if (response.isSuccess) {
                        // Baþarýlý ise sayfayý yenile
                        window.location.href = "/Supplier/Index";
                    } else {
                        // Baþarýsýz ise hata mesajýný göster
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
            // Boþ bir ad girdiyse uyarý ver
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