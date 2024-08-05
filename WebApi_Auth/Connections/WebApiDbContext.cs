using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Auth.Connections
{
    public class WebApiDbContext : IdentityDbContext
    {
        public WebApiDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
