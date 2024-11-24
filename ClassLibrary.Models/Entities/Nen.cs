namespace ClassLibrary.Models.Entities
{
    public class Nen
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Hunter_Nen> Hunter_Nen { get; set; }
    }
}
