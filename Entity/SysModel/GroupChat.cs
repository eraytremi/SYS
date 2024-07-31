namespace Entity.SysModel
{
    public class GroupChat:BaseEntity<long>
    {
        public string GroupName { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
