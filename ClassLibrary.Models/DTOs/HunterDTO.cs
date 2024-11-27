using ClassLibrary.Models.Entities;

namespace ClassLibrary.Models.DTOs
{
    public class HunterDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required int Age { get; set; }
    }
}
