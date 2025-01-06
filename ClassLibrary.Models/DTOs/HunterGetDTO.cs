namespace ClassLibrary.Models.DTOs
{
    public class HunterGetDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required int Age { get; set; }
        public List<NenDTO> Nen { get; set; }
    }
}
