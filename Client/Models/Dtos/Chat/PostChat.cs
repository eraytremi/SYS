namespace Client.Models.Dtos.Chat
{
    public class PostChat
    {
        public string UserName { get; set; }
        public string MessageText { get; set; }
        public int CreatedBy { get; set; }
    }
}
