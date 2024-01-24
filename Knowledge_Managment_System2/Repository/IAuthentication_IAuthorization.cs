using Knowledge_Managment_System2.Model.UserDTOs;
using System.IdentityModel.Tokens.Jwt;

namespace Knowledge_Managment_System2.Repository
{
    public interface IAuthentication_IAuthorization
    {
        Task<string> Authenticate(AuthenticateRequest authenticate);

        Task<bool> Register(RegisterRequest register);

        Task<bool> RegisterAdmin(RegisterRequest register);

        Task<bool> ConfirmEmail(string userId, string token);
    }

}
