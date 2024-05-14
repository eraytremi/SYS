$(document).ready(function () {
    $("#login").click(function () {
        var formDataObject =
        {
            Mail: $("#email").val(),
            Password: $("#password").val()
        };

        if ($("#email").val().length > 0 && $("#password").val().length > 0) {
            $.ajax({
                url: "/Auth/Index",
                method: "post",
                dataType: "json",
                data: formDataObject,
                success: function (response) {
                   
                    if (response.IsSuccess) {
                        window.location.href = "/Home/Index";
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
                text: "Lütfen email ve þifrenizi giriniz.",
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Tamam'
            });
        }
    });


});