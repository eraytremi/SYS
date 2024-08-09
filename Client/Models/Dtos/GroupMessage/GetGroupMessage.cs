using Client.Models.Dtos.GroupChat;
using Client.Models.Dtos.User;

namespace Client.Models.Dtos.GroupMessages
{
    public class GetGroupMessage
    {
        public long Id { get; set; }
        public long? GroupId { get; set; }
        public long? SenderId { get; set; }
        public string MessageText { get; set; }
        public bool IsSeen { get; set; }
        public virtual GetGroupChat Group { get; set; }
        public virtual UserGetDto Sender { get; set; }
    }
}
