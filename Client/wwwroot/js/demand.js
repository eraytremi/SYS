$(document).ready(function () {
    // Event handler for posting stock
    $('.btnApprove').click(function () {
        var id = $(this).attr('demandId');
        approveDemand(id);
    });

    $('.btnReject').click(function () {
        var id = $(this).attr('demandId');
        rejectDemand(id);
    });
    $("#sendDemand").click(function () {
      
        var formDataObject = {
            ProductName: $("#productName").val(),
            Quantity: $("#quantity").val(),
            Description: $("#description").val()   
        };
        console.log(formDataObject)
        $.ajax({
            url: "/Demand/Post",
            method: "POST",
            data: formDataObject,
            success: function (response) {
                if (response.isSuccess) {
                    window.location.href = "/Demand/Index";
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

});
function approveDemand(id) {
    Swal.fire({
        text: "Girilen talebi kabul etmek istiyor musunuz?",
        icon: 'success',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Onayla!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Demand/Approve/' + id,
                method: 'get',
                success: function (response) {
                    if (response.isSuccess) {
                        Swal.fire({
                            text: response.message[0],
                            icon: 'success',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.href = "/Demand/WaitingDemands";
                            }
                        });
                    } else {
                        Swal.fire({
                            title: 'İşlem Başarısız',
                            text: response.messages[0],
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam',
                            icon: 'error',
                        });
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error: ' + error);
                    Swal.fire({
                        title: 'Hata',
                        text: 'Bir hata oluştu: ' + error,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Tamam',
                        icon: 'error',
                    });
                }
            });
        }
    });
}


function rejectDemand(id) {
    Swal.fire({
        text: "Girilen talebi reddetmek istiyor musunuz?",
        icon: 'success',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Onayla!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Demand/Reject/' + id,
                method: 'get',
                dataType: 'json',
                success: function (response) {
                    if (response.IsSuccess) {
                        Swal.fire({
                            text: response.Message,
                            icon: 'success',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.href = "/Demand/WaitingDemands";
                            }
                        });
                    } else {
                        Swal.fire({
                            title: 'İşlem Başarısız',
                            text: response.Messages.join('\n'),
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam',
                            icon: 'error',
                        });
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error: ' + error);
                    Swal.fire({
                        title: 'Hata',
                        text: 'Bir hata oluştu: ' + error,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Tamam',
                        icon: 'error',
                    });
                }
            });
        }
    });
}