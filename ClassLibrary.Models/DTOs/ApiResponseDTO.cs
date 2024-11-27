namespace ClassLibrary.Models.DTOs
{
    public class ApiResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
