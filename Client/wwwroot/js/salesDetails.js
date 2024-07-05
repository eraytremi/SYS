$(document).ready(function () {
    function calculateTotal(row) {
        var price = parseFloat(row.find('.product-price').val());
        var quantity = parseInt(row.find('.product-quantity').val());
        var total = price * quantity;
        row.find('.product-total').val(total.toFixed(2));
    }

    
    $('#salesTable').on('change', '#productName', function () {
        var row = $(this).closest('tr');
        var productName = $(this).val();
        if (productName) {
            $.ajax({
                url: "/Product/GetProductByName",
                type: 'POST',
                data: productName,
                success: function (response) {
                    if (response.IsSuccess) {
                        row.find('.product-price').val(response.Data.price);
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

    $('#salesTable').on('change', '.product-quantity', function () {
        var row = $(this).closest('tr');
        calculateTotal(row);
    });

    $('#salesTable').on('click', '.add-row', function () {
        var newRow = '<tr>' +
            '<td><select class="form-control" id="productId" name="productId">' +
            '<option value="">Ürünü Seçiniz</option>' +
            '@foreach (var item in Model) { <option value="@item.Name">@item.Name</option> }' +
            '</select></td>' +
            '<td><input type="text" class="form-control product-price" readonly /></td>' +
            '<td><input type="number" class="form-control product-quantity" min="1" value="1" /></td>' +
            '<td><input type="text" class="form-control product-total" readonly /></td>' +
            '<td><button type="button" class="btn btn-danger remove-row">-</button></td>' +
            '</tr>';
        $('#salesTable tbody').append(newRow);
    });

    $('#salesTable').on('click', '.remove-row', function () {
        $(this).closest('tr').remove();
    });

    $('#salesForm').on('submit', function (e) {
        e.preventDefault();


        var saleDetails = [];
        $('#salesTable tbody tr').each(function () {
            var row = $(this);
            var productName = row.find('#productName').val();
            var price = parseFloat(row.find('.product-price').val());
            var quantity = parseInt(row.find('.product-quantity').val());
            var productTotal = parseFloat(row.find('.product-total').val());

            if (productName && price && quantity) {
                saleDetails.push({
                    Name: {  productName },
                    Price: price,
                    Quantity: quantity
                });
            }
        });


        if (saleDetails.length > 0) {
            $.ajax({
                url: '@Url.Action("AddSale")',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({
                    date: new Date(),
                    totalAmount: saleDetails.reduce((sum, item) => sum + (item.price * item.quantity), 0),
                    saleDetails: saleDetails
                }),
                success: function (response) {
                    alert('Satış kaydedildi');
                    $('#salesTable tbody tr').not(':first').remove();
                    $('#salesTable tbody tr:first').find('input').val('');
                },
                error: function (error) {
                    alert('Satış kaydedilirken bir hata oluştu');
                }
            });
        }

    });
});
