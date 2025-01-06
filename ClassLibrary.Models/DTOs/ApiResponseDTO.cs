namespace ClassLibrary.Models.DTOs
{
    public class ApiResponseDTO<T>
    {
        public required int StatusCode { get; set; }
        public required string Message { get; set; }
        public T? Data { get; set; }
    }
}
