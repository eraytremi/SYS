namespace Client.Models.Dtos.User
{
    public class UpdateUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
