using Microsoft.AspNetCore.Mvc;

namespace WebApi_DapperService.Classes
{
    public class ApiActionResult<T> : IApiActionResult<T>, IActionResult
    {
        public int StatusCode { get; }
        public string Message { get; }
        public T? Data { get; }

        private readonly ActionResult _result;

        public ApiActionResult(int statusCode, string message, T? data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;

            var responseObj = new
            {
                StatusCode,
                Message,
                Data,
            };

            _result = statusCode switch
            {
                200 => new OkObjectResult(responseObj),                             // OK()
                201 => new ObjectResult(responseObj) { StatusCode = statusCode },   // Created()
                202 => new ObjectResult(responseObj) { StatusCode = statusCode },   // AcceptedResult()
                204 => new ObjectResult(responseObj) { StatusCode = statusCode },   // NoContent()
                400 => new BadRequestObjectResult(responseObj),                     // BadRequest()
                401 => new UnauthorizedObjectResult(responseObj),                   // Unauthorized()
                404 => new NotFoundObjectResult(responseObj),                       // NotFound()
                500 => new ObjectResult(responseObj) { StatusCode = statusCode },   // Server Error
                _ => new ObjectResult(responseObj) { StatusCode = statusCode }      // Sin Definir
            };
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return _result.ExecuteResultAsync(context);
        }
    }

    public class ApiActionResult : ApiActionResult<object>, IApiActionResult
    {
        public ApiActionResult(int statusCode, string message) : base(statusCode, message, null) 
        { 
        }

        public ApiActionResult(int statusCode, string message, object data) : base(statusCode, message, data) 
        { 
        }
    }
}
