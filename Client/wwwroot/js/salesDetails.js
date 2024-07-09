$(document).ready(function () {
    function calculateTotal(row) {
        var price = parseFloat(row.find('.product-price').val());
        var quantity = parseInt(row.find('.product-quantity').val());
        var total = price * quantity;
        row.find('.product-total').val(total.toFixed(2));
    }

    $('.form-select').select2({
        theme: "bootstrap-5",
        width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-300') ? '300%' : 'style',
        placeholder: $(this).data('placeholder'),
    });

  
    $('#salesForm').on('change', '.form-select', function () {
        var row = $(this).closest('tr');
        var productId = $(this).val();
        
        if (productId) {
            $.ajax({
                url: "/Product/GetProductById",
                type: 'POST',
                contentType: 'application/json',
                data: productId,
                success: function (response) {
                    if (response.isSuccess) {
                        row.find('.product-price').val(response.data.price);
                        calculateTotal(row);
                    } else {
                        alert('Ürün bulunamadı');
                        row.find('.product-price').val('');
                        row.find('.product-total').val('');
                    }
                },
                error: function () {
                    alert('Ürün fiyatı getirilirken bir hata oluştu');
                }
            });
        } else {
            row.find('.product-price').val('');
            row.find('.product-total').val('');
        }
    });

    $('#salesForm').on('change', '.product-quantity', function () {
        var row = $(this).closest('tr');
        calculateTotal(row);
    });

  
    $('#salesForm').on('click', '.remove-row', function () {
        $(this).closest('tr').remove();
    });

    $('#confirmBtn').click(function () {
        var salesDetails = [];

        $('#salesForm tbody tr').each(function () {
            var productId = $(this).find('.form-select').val();
            var quantity = $(this).find('.product-quantity').val();
            var price = $(this).find('.product-price').val();

            if (productId && quantity && price) {
                salesDetails.push({
                    ProductId: parseInt(productId),
                    Quantity: parseInt(quantity),
                    Price: parseFloat(price)
                });
            }
        });
        console.log(salesDetails)
        if (salesDetails.length > 0) {
            $.ajax({
                url: "/SaleDetails/Post",
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(salesDetails),
                success: function (response) {
                    if (response.isSuccess) {
                        alert('Satış işlemi başarılı');
                        window.location.href = "/SaleDetails/Index";
                    } else {
                        alert('Satış işlemi başarısız: ' + response.message);
                    }
                },
                error: function () {
                    alert('Satış işlemi sırasında bir hata oluştu');
                }
            });
        } else {
            alert('Lütfen en az bir ürün seçin');
        }
    });
});
