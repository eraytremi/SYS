var groupIdx;
var messagex;
var connection; // Global connection object
var currentUserId; // Global değişken olarak tanımlandı

$(document).ready(function () {

    // currentUserId değerini HTML'den al ve JavaScript'te ata
    currentUserId = $('#currentUserId').val();

    // SignalR bağlantısını başlat
    connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7220/chathub")
        .configureLogging(signalR.LogLevel.Debug)
        .withAutomaticReconnect()
        .build();

    connection.start()
        .then(() => {
            console.log("Connected to the hub");

            // ReceiveMessage fonksiyonunu dinle
            connection.on("ReceiveMessage", function (senderName, message) {
                console.log("ReceiveMessage metodu tetiklendi.");
                console.log("Gönderen:", senderName);
                console.log("Mesaj:", message);

                const groupId = groupIdx;
                const alertDiv = document.createElement("div");
                alertDiv.classList.add("alert", "alert-dark");
                alertDiv.setAttribute("role", "alert");

                const messageParagraph = document.createElement("p");

                const boldUserName = document.createElement("b");
                boldUserName.textContent = senderName;

                messageParagraph.appendChild(boldUserName);
                messageParagraph.appendChild(document.createTextNode(": " + message));

                alertDiv.appendChild(messageParagraph);

                document.querySelector(`#group-${groupId} #mesajAlani`).appendChild(alertDiv);
            });

            connection.onclose(function (error) {
                console.error('Bağlantı kapandı: ', error);
            });

        })
        .catch(err => console.error("Connection failed: ", err));

    $('.clickable-row').on('click', function () {
        var groupId = $(this).data('group-id');
        var $membersRow = $('#group-' + groupId);
        // Alt tabloyu aç/kapa
        $membersRow.toggle();
    });

    $('.sendButton').on('click', function () {
        var groupId = $(this).data('group-id');
        groupIdx = groupId; // Global değişkeni ayarla
        const message = $('#group-' + groupId + ' #mesaj').val();
        messagex = message; // Global değişkeni ayarla
        if (!message) {
            console.error('Mesaj boş olamaz.');
            return;
        }

        var formData = {
            MessageText: message,
            GroupId: groupId
        };

        console.log(formData);

        $.ajax({
            url: '/Messages/Post',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                console.log('Mesaj gönderildi ve kaydedildi.');

                const alertDiv = document.createElement("div");
                alertDiv.classList.add("alert", "alert-dark");
                alertDiv.setAttribute("role", "alert");

                const messageParagraph = document.createElement("p");

                const boldUserName = document.createElement("b");
                boldUserName.textContent = "Ben";

                messageParagraph.appendChild(boldUserName);
                messageParagraph.appendChild(document.createTextNode(": " + message));

                alertDiv.appendChild(messageParagraph);

                document.querySelector(`#group-${groupId} #mesajAlani`).appendChild(alertDiv);

                // Mesaj alanını temizle
                $('#group-' + groupId + ' #mesaj').val('');

                // Mesajı gruba gönder
                connection.invoke("SendMessageToGroup", groupIdx, messagex, currentUserId)
                    .then(() => {
                        console.log("Message sent to group successfully.");
                    })
                    .catch(err => {
                        console.error("Error sending message to group: ", err);
                    });

            },
            error: function (error) {
                console.log("Hata: ", error);
            }
        });
    });

});
