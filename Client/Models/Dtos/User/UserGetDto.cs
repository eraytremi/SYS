namespace Client.Models.Dtos.User
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Mail { get; set; }
        public string Token { get; set; }
    }
}
