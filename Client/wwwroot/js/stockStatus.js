$(document).ready(function () {
    $('.btnApprove').click(function () {
        var id = $(this).attr('stockMovementId');
        approveStatu(id);
    });

    $('.btnReject').click(function () {
        var id = $(this).attr('stockMovementId');
        rejectStatu(id);
    });
});

function approveStatu(id) {
    Swal.fire({
        text: "Girilen stoğu kabul etmek istiyor musunuz?",
        icon: 'success',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Onayla!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/StockMovement/Approve/' + id,
                method: 'get',
                success: function (response) {
                    if (response.IsSuccess) {
                        Swal.fire({
                            text: response.Message,
                            icon: 'success',
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'Tamam',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.href = "/StockMovement/Index";
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


function rejectStatu(id) {
    Swal.fire({
        text: "Girilen stoğu reddetmek istiyor musunuz?",
        icon: 'success',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Onayla!',
        cancelButtonText: 'Vazgeç!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/StockMovement/Reject/' + id,
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
                                window.location.href = "/StockMovement/Index";
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

