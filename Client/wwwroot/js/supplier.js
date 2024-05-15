

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
                            title: '��lem Ba�ar�s�z',
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
                title: 'Uyar�',
                text: "Tedarik�i ismi giriniz.",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Tamam'
            });
        }
    });


    $("#updateSupplier").click(function () {
        // G�ncellenecek tedarik�inin Id'sini ve yeni ad�n� al
        var supplierId = $("#updatedSupplierId").val();
        var newName = $("#updatedSupplierName").val();
        var description = $("updatedDescription").val();
        // Form verilerini kontrol et
        if (newName.length > 0) {
            // Form verilerini bir JavaScript nesnesi olarak olu�tur
            var formDataObject = {
                Id: supplierId,
                Name: newName,
                Description: description
            };
            // AJAX iste�i g�nder
            $.ajax({
                url: "/Supplier/UpdateSupplier", // G�ncelleme endpoint'i
                method: "POST",
                data: formDataObject, // Form verileri
                success: function (response) {
                    // Sunucudan gelen yan�t� i�le
                    if (response.isSuccess) {
                        // Ba�ar�l� ise sayfay� yenile
                        window.location.href = "/Supplier/Index";
                    } else {
                        // Ba�ar�s�z ise hata mesaj�n� g�ster
                        Swal.fire({
                            title: '��lem Ba�ar�s�z',
                            text: response.messages,
                            icon: 'error',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam'
                        });
                    }
                }
            });
        } else {
            // Bo� bir ad girdiyse uyar� ver
            Swal.fire({
                title: 'Uyar�',
                text: "Tedarik�i ismi giriniz.",
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
        text: "Tedarik�iyi silmek istiyor musunuz?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sil!',
        cancelButtonText: 'Vazge�!'
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
                            title: '��lem Ba�ar�s�z',
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