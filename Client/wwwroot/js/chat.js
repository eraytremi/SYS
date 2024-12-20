﻿// SignalR bağlantısını oluştur
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7220/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build();


let connectionStarted = false; // Bağlantının başlatılıp başlatılmadığını kontrol etmek için

// Bağlantıyı başlatma fonksiyonu
async function startConnection(groupId) {
    try {
        if (!connectionStarted) {
            await connection.start();
            console.log("SignalR bağlantısı kuruldu.");
            connectionStarted = true;
        }

        // Grup katılımını bağlantı başladıktan sonra çağır
        await connection.invoke("JoinGroup", connection.connectionId, groupId);
        console.log(`Connection ID: ${connection.connectionId} ile grup ${groupId}'e katıldı.`);
    } catch (err) {
        console.log("Bağlantı hatası: ", err);
        setTimeout(() => startConnection(groupId), 5000);
    }
}


$(document).on("click", ".clickable-row", function () {
    const groupId = $(this).data("group-id");
    console.log(groupId);
    startConnection(groupId);
    $(`#group-${groupId}`).toggle();
});

// Mesaj alındığında çalışacak event
connection.on("ReceiveMessage", function (messageData) {
    const { GroupId, Sender, MessageText, CreatedDate } = messageData;

    const messageArea = $(`#group-${GroupId} #mesajAlani`);
    const newMessage = `
        <div class="alert alert-dark" role="alert">
            <p><b>${Sender}</b>: ${MessageText} <span style="float: right;">${new Date(CreatedDate).toLocaleTimeString()}</span></p>
        </div>`;
    messageArea.append(newMessage);
    messageArea.scrollTop(messageArea[0].scrollHeight);
});

$(document).ready(function () {
    setTimeout(() => {
        $(".mesajAlani").each(function () {
            $(this).scrollTop(this.scrollHeight);
        });
    }, 100); // Kısa bir gecikme verin
});

// Gönder butonuna tıklama eventi
$(document).on("click", ".sendButton", async function () {
    const groupId = $(this).data("group-id");
    const message = $(`#mesaj-${groupId}`).val();
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
        

        } catch (error) {
            console.error("Mesaj gönderilemedi: ", error);
        }
    }
});

connection.onclose(async () => {
    console.log("Bağlantı kapandı, yeniden bağlanılıyor...");
    await startConnection(groupId);
});


