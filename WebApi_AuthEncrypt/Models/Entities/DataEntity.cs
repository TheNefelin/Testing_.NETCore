namespace WebApi_AuthEncrypt.Models.Entities
{
    public class DataEntity
    {
        public int Id { get; set; }
        public string Data1 { get; set; } = string.Empty;
        public string Data2 { get; set; } = string.Empty;
        public string Data3 { get; set; } = string.Empty;
        public int Id_User { get; set; }
    }
}
