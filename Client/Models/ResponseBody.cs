namespace Client.Models
{
    public class ResponseBody<T>
    {
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public string? StatusMessage { get; set; }
        public List<string>? ErrorMessages { get; set; }
    }
}
