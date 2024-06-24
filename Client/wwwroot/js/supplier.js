

$(document).ready(function () {
    $("#postSupplier").click(function () {


        var name = $("#supplierName").val();
        var description = $("#description").val();
        var category = $("#category").val();
        var mail= $("#email").val();
       
        if (name.length > 0 && description.length > 0 && mail.val().length > 0 && category.val().length>0)  {

            var formDataObject =
            {
                Name: name,
                Description: description,
                Category: category,
                Mail:mail
            }
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

    $("#sendOfferSupplier").click(function () {

        var toEmails = $("#toEmails").val();
        var subject = $("#subject").val();
        var body = $("#body").val();
        
        if (toEmails.length > 0 && subject.length > 0 && body.length > 0) {
            var emailArray = toEmails.split(',').map(email => email.trim());
            var formDataObject = {
                To: emailArray,
                Subject: subject,
                Body: body
            };
            console.log(formDataObject)
            $.ajax({
                url: "/Supplier/SendEmail",
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify(formDataObject), 
                success: function (response) {
                    if (response.isSuccess) {
                        window.location.href = "/Supplier/Index";
                    } else {
                        Swal.fire({
                            title: '��lem Ba�ar�s�z',
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
                        text: 'Sunucu ile ba�lant� kurulurken bir hata olu�tu.',
                        icon: 'error',
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Tamam'
                    });
                }
            });
        } else {
            Swal.fire({
                title: 'Uyar�',
                text: 'Alanlar bo� b�rak�lamaz.',
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
        var category = $("#updatedCategory").val();
        var mail = $("#updatedMail").val();

        if (newName.length > 0) {

            var formDataObject = {
                Id: supplierId,
                Name: newName,
                Description: description,
                Category: category,
                Mail: mail
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
            -
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