using System.ComponentModel.DataAnnotations;

namespace ConsoleFetchAPI.Models
{
    internal class CoreRequest<T>
    {
        [Required]
        public string Id_User { get; set; } = string.Empty;
        [Required]
        public string Sql_Token { get; set; }
        public string Password { get; set; } = string.Empty;
        public T? CoreData { get; set; }
    }
}
