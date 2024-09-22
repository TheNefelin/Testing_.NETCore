namespace ConsoleAppTesting.Models.Entities
{
    internal class SecretEntity
    {
        public int Id { get; set; }
        public string Data1 { get; set; } = string.Empty;
        public string IV1 { get; set; } = string.Empty;
        public string Data2 { get; set; } = string.Empty;
        public string IV2 { get; set; } = string.Empty;
        public string Data3 { get; set; } = string.Empty;
        public string IV3 { get; set; } = string.Empty;
        public string Id_User { get; set; } = string.Empty;
    }
}
