namespace Client.Models.Dtos.Email
{
    public class RequestEmail
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
