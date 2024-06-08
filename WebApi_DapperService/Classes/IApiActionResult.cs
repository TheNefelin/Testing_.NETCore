using System.Text.Json.Serialization;

namespace WebApi_DapperService.Classes
{
    public interface IApiActionResult<T>
    {
        [JsonPropertyOrder(0)]
        public int StatusCode { get; }

        [JsonPropertyOrder(1)]
        public string Message { get; }

        [JsonPropertyOrder(2)]
        public T? Data { get; }
    }

    public interface IApiActionResult
    {
        [JsonPropertyOrder(0)]
        public int StatusCode { get; }

        [JsonPropertyOrder(1)]
        public string Message { get; }
    }
}
