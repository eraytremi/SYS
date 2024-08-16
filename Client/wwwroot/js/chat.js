// SignalR bağlantısını oluştur
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7220/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

// Bağlantıyı başlat
async function startConnection() {
    try {
        await connection.start();
        console.log("SignalR bağlantısı kuruldu.");
    } catch (err) {
        console.log("Bağlantı hatası: ", err);
        setTimeout(startConnection, 5000);
    }
}

// Bağlantıyı başlatma çağrısı
startConnection();

// Mesaj alındığında çalışacak event
connection.on("ReceiveMessage", function (userId, message) {
    // Mesajı ekranda göster
    const messageArea = $("#mesajAlani");
    const newMessage = `<div class="alert alert-dark" role="alert">
                            <p><b>Kullanıcı ${userId}</b>: ${message}</p>
                        </div>`;
    messageArea.append(newMessage);
    messageArea.scrollTop(messageArea[0].scrollHeight);
});

// Gönder butonuna tıklama eventi
$(document).on("click", ".sendButton", function () {
    const groupId = $(this).data("group-id");
    const message = $("#mesaj").val();
    const currentUserId = $("#currentUserId").val();

    if (message.trim() !== "") {

        var formData = {
            MessageText: message,
            GroupId: groupId
        };

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
                var currentUserIdx = currentUserId;
                console.log(currentUserIdx);
                console.log(groupId);
                console.log(message);
                // Mesaj alanını temizle
                $('#group-' + groupId + ' #mesaj').val('');

                connection.invoke("SendMessageToGroup", groupId, message, currentUserId)
                    .then(function () {
                        // Mesaj gönderildikten sonra input'u temizle
                        $("#mesaj").val("");
                    })
                    .catch(function (err) {
                        console.error("Mesaj gönderilemedi: ", err);
                    });
               
            },
            error: function (error) {
                console.log("Hata: ", error);
            }
        });

        // Mesajı gruba gönder
      
    }
});

// Gruba tıklayınca mesajları göster
$(document).on("click", ".clickable-row", function () {
    const groupId = $(this).data("group-id");
    $(`#group-${groupId}`).toggle();
});
