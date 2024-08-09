namespace Client.Models.Dtos.GroupMessages
{
    public class UpdateGroupMessage
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }
        public long? SenderId { get; set; }
        public string MessageText { get; set; }
        public bool IsSeen { get; set; }
    }
}
