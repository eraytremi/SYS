// SignalR bağlantısını oluştur
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7220/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

// Bağlantıyı başlatma fonksiyonu
async function startConnection(groupId) {
    try {
        await connection.start();
        console.log("SignalR bağlantısı kuruldu.");

        // ConnectionId'yi al ve gruba katıl
        const connectionId = connection.connectionId;
        await connection.invoke("JoinGroup", connectionId, groupId);
        console.log(`Connection ID: ${connectionId} ile grup ${groupId}'e katıldı.`);

    } catch (err) {
        console.log("Bağlantı hatası: ", err);
        setTimeout(() => startConnection(groupId), 5000);
    }
}

const groupId = 1; 
startConnection(groupId);

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
$(document).on("click", ".sendButton", async function () {
    const groupId = $(this).data("group-id");
    const message = $("#mesaj").val();
    const currentUserId = $("#currentUserId").val();

    if (message.trim() !== "") {
        const formData = {
            MessageText: message,
            GroupId: groupId
        };

        try {
            // Mesajı sunucuya kaydetme işlemi
            await $.ajax({
                url: '/Messages/Post',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(formData)
            });

            console.log('Mesaj gönderildi ve kaydedildi.');

            // Mesajı gruba gönderme işlemi
            console.log(groupId);
            console.log(message);
            console.log(currentUserId);

            //await connection.invoke("SendMessageToGroup", groupId, message, currentUserId);
            console.log("Mesaj gruba gönderildi.");

            // Mesajı arayüzde gösterme
            const alertDiv = $(`
                <div class="alert alert-dark" role="alert">
                    <p><b>Ben</b>: ${message}</p>
                </div>
            `);
            $(`#group-${groupId} #mesajAlani`).append(alertDiv);
            $(`#group-${groupId} #mesajAlani`).scrollTop($(`#group-${groupId} #mesajAlani`)[0].scrollHeight);

            // Mesaj alanını temizle
            $(`#group-${groupId} #mesaj`).val('');

        } catch (error) {
            console.error("Mesaj gönderilemedi: ", error);
        }
    }
});

connection.onclose(async () => {
    console.log("Bağlantı kapandı, yeniden bağlanılıyor...");
    await startConnection(groupId);
});

// Gruba tıklayınca mesajları göster
$(document).on("click", ".clickable-row", function () {
    const groupId = $(this).data("group-id");
    $(`#group-${groupId}`).toggle();
});
