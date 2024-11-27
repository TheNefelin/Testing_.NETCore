using ClassLibrary.Models.DTOs;

namespace ClassLibrary.ServicesServer.Interfaces
{
    public interface ITokenGenerator
    {
        public TokenDTO GenerateToken(ApiAuthDTO auth);
    }
}
