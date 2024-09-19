namespace ReadingBookApi.Model
{
    public class ResponseVM
    {
        public string Message { get; set; } = null!;

        public string Status { get; set; }
        public bool IsSuccess { get; set; }

        public object? Data { get; set; }
    }
}
