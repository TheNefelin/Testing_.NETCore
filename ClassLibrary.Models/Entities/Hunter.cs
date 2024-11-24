namespace ClassLibrary.Models.Entities
{
    public class Hunter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public List<Hunter_Nen> Hunter_Nen { get; set; }
    }
}
