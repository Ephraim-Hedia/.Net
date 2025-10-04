
using DataAccessLayer.Entites.Identity;

namespace ServicesLayer.Services.TokenService
{
    public interface ITokenService
    {
        public string GenerateToken(ApplicationUser applicationUser);
    }
}
