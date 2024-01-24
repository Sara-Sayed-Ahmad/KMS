using Knowledge_Managment_System2.Model;
using Realms.Sync;

namespace Knowledge_Managment_System2.Authorization
{
    public interface IJwtUtils
    {
        //Generate Token 
        string GenerateToken(Employee employee);

        //Validate Token
        int? ValidateToken(string token);
    }
}
