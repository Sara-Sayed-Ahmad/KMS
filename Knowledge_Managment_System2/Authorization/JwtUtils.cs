using Knowledge_Managment_System2.Helpers;
using Knowledge_Managment_System2.Model;
using Knowledge_Managment_System2.Model.UserDTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Realms.Sync;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Knowledge_Managment_System2.Authorization
{

    //The JWT utils class contains methods for generating and validating JWT tokens
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;
        private readonly SystemDbContext _context;

        public JwtUtils(IOptions<AppSettings> appSettings, SystemDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        //Generate Token
        public string GenerateToken(Employee employee)
        {
            //Designed for creating and validating JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            //key
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                ////A claim is a statement about an entity made (describes a property, right, or some other quality of that entity)
                //Subject = new ClaimsIdentity(new[] {
                //    new Claim("id", employee.EmployeeId.ToString())
                //}
                //),
                //Expires = DateTime.UtcNow.AddDays(5),
                //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        //Validate Token
        public int? ValidateToken(string token)
        {
            //if token is not exist
            if (token == null)
                return null;

            //Designed for creating and validating JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
