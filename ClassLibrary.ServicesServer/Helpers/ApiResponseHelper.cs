using ClassLibrary.Models.DTOs;

namespace ClassLibrary.ServicesServer.Helpers
{
    public static class ApiResponseHelper
    {
        public static ApiResponseDTO<T> Success<T>(T? data = default, string message = "Ok.")
        {
            return new ApiResponseDTO<T>
            {
                StatusCode = 200,
                Message = message,
                Data = data
            };
        }

        public static ApiResponseDTO<T> BadRequest<T>(string message = "Bad Request.")
        {
            return new ApiResponseDTO<T>
            {
                StatusCode = 400,
                Message = message,
                Data = default
            };
        }

        public static ApiResponseDTO<T> NotFound<T>(string message = "Not Found.")
        {
            return new ApiResponseDTO<T>
            {
                StatusCode = 404,
                Message = message,
                Data = default
            };
        }

        public static ApiResponseDTO<T> Conflict<T>(string message = "Conflict.")
        {
            return new ApiResponseDTO<T>
            {
                StatusCode = 409,
                Message = message,
                Data = default
            };
        }

        public static ApiResponseDTO<T> Error<T>(string message = "Error.")
        {
            return new ApiResponseDTO<T>
            {
                StatusCode = 500,
                Message = message,
                Data = default
            };
        }
    }
}
