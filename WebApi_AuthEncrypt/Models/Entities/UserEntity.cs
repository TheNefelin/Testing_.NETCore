namespace WebApi_AuthEncrypt.Models.Entities
{
    public class UserEntity
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string E_Hash { get; set; } = string.Empty;
        public string E_Salt { get; set; } = string.Empty;
        public string E_Key { get; set; } = string.Empty;
        public string E_IV { get; set; } = string.Empty;
        public string SessionToken { get; set; } = string.Empty;
    }
}
