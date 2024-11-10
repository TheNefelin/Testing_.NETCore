using ClassLibrary_SignalIR.DTOs;
using ClassLibrary_SignalIR.Models;

namespace ClassLibrary_SignalIR.Repositories
{
    public class UserRepository
    {
        private readonly List<User> _dbUsers = new()
        {
            new User() { Id = Guid.NewGuid().ToString(), Email = "Netero", Password = "Password", },
            new User() { Id = Guid.NewGuid().ToString(), Email = "Hisoka", Password = "Password", },
            new User() { Id = Guid.NewGuid().ToString(), Email = "Killua", Password = "Password", },
        };

        public User? Login(LoginDTO user)
        {
            return _dbUsers.FirstOrDefault(e => e.Email.Equals(user.Email) && e.Password.Equals(user.Password));
        }

        public List<User> GetUsers() => _dbUsers;
    }
}
