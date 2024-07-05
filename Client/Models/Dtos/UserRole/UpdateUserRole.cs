namespace Client.Models.Dtos.UserRole
{
    public class UpdateUserRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public long UserId { get; set; }
    }
}
