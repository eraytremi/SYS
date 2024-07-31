namespace Client.Models.Dtos.GroupMember
{
    public class UpdateGroupMember
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long UserId { get; set; }
    }
}
