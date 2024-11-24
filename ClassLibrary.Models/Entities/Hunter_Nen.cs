namespace ClassLibrary.Models.Entities
{
    public class Hunter_Nen
    {
        public int Hunter_Id { get; set; }
        public int Nen_Id { get; set; }
        public Hunter Hunter { get; set; }
        public Nen Nen { get; set; }
    }
}
