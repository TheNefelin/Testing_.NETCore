using ClassLibrary.ServicesServer.DTOs;

namespace ClassLibrary.ServicesServer.Services
{
    public interface ITokenGenerator
    {
        public TokenDTO GenerateToken(AuthDTO auth);
    }
}
