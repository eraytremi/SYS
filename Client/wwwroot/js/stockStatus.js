
$('.btnApprove').click(function () {
    var id = $(this).attr('stockMovementId');
    approveStatu(id);
});

function approveStatu(id) {
    Swal.fire({
        text: "Girilen stoğu kabul etmek  istiyor musunuz?",
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
