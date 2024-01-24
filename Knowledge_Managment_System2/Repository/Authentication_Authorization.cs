using Knowledge_Managment_System2.Model.UserDTOs;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Knowledge_Managment_System2.Model;
using Microsoft.Win32;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.WebUtilities;
using Knowledge_Managment_System2.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Knowledge_Managment_System2.Repository
{
    public class Authentication_Authorization : IAuthentication_IAuthorization
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<Permission> _roleManager;
        private readonly SystemDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public Authentication_Authorization(UserManager<Employee> userManager, RoleManager<Permission> roleManager, SystemDbContext context, IConfiguration configuration, IMapper mapper, IMailService mailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _mailService = mailService;
        }

        //Login
        public async Task<string> Authenticate(AuthenticateRequest authenticate)
        {
            var user = _context.Employees.Include(r => r.Permissions)
                .SingleOrDefault(x => x.Email == authenticate.Email);

            var userRoles = await _userManager.GetRolesAsync(user);
          
            //if user exist in the system and password is correct
            if (user == null || !await _userManager.CheckPasswordAsync(user, authenticate.Password))
            {
                throw new ApplicationException("Wrong email or password");
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(ClaimTypes.Role, authenticate.Role),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            //Generate Token
            var token = GetToken(authClaims);

            var tokenUser = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            //return token.EncodedPayload;
            return tokenUser.token;
        }

        //Registration user
        public async Task<bool> Register(RegisterRequest register)
        {
            var userExist = await _userManager.FindByEmailAsync(register.Email);

            //if user exist in the system 
            if (userExist != null)
            {
                throw new ApplicationException("Email '" + register.Email + "' is already taken");
            }

            if(register.Password != register.VerifyPassword)
            {
                throw new ApplicationException("Password is incorrect");
            }

            var user = new Employee()
            {
                UserName = register.FirstName + register.PositionId,
                Email = register.Email,
                LastName = register.LastName,
                FirstName = register.FirstName,
                PhoneNumber = register.PhoneNumber,
                Address = register.Address,
                PositionId = register.PositionId,
                SecurityStamp = Guid.NewGuid().ToString(),
                //EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                //Token confirm email
                var confirmaEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //Encoded
                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmaEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                //string url1 = $"{_configuration["AppUrl"]}/api/Account/ConfirmEmail?userid={user.Id}&token={validEmailToken}";

                string url = $"https://localhost:7061/api/Account/ConfirmEmail?userid={user.Id}&token={validEmailToken}";

                await _mailService.SendEmail(user.Email, "Confirm your email", $"<h1>Welcome to Vital</h1>" +
                        $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
            }

            //if role = user
            if (await _roleManager.RoleExistsAsync(Roles.User))
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
            }
            //create role user
            //if (!await _roleManager.RoleExistsAsync(Roles.User))
            //{
            //    await _roleManager.CreateAsync(new Permission()
            //    {
            //        Name = Roles.User
            //    });
            //}
            //else //create role admin
            //{
            //    await _roleManager.CreateAsync(new Permission()
            //    {
            //        Name = Roles.Admin
            //    });
            //}

            if (!result.Succeeded)
            {
                throw new ApplicationException("User creation failed! Please check user details and try again.");
            }
            return result.Succeeded;
        }

        //Registration Admin
        public async Task<bool> RegisterAdmin(RegisterRequest register)
        {
            var userExist = await _userManager.FindByEmailAsync(register.Email);

            //if user exist in the system 
            if (userExist != null)
            {
                throw new ApplicationException("Email '" + register.Email + "' is already taken");
            }

            var user = new Employee()
            {
                UserName = register.FirstName + register.PositionId,
                Email = register.Email,
                LastName = register.LastName,
                FirstName = register.FirstName,
                PhoneNumber = register.PhoneNumber,
                Address = register.Address,
                PositionId = register.PositionId,
                SecurityStamp = Guid.NewGuid().ToString(),
                //EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                //Token confirm email
                var confirmaEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                //Encoded
                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmaEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                //string url1 = $"{_configuration["AppUrl"]}/api/Account/ConfirmEmail?userid={user.Id}&token={validEmailToken}";

                string url = $"https://localhost:7061/api/Account/ConfirmEmail?userid={user.Id}&token={validEmailToken}";

                await _mailService.SendEmail(user.Email, "Confirm your email", $"<h1>Welcome to Vital</h1>" +
                        $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");
            }

            //create role user
            if (!await _roleManager.RoleExistsAsync(Roles.User))
            {
                await _roleManager.CreateAsync(new Permission()
                {
                    Name = Roles.User
                });
            }
            else //create role admin
            {
                await _roleManager.CreateAsync(new Permission()
                {
                    Name = Roles.Admin
                });
            }

            //if role = admin
            if (await _roleManager.RoleExistsAsync(Roles.Admin))
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin);
            }

            if (!result.Succeeded)
            {
                throw new ApplicationException("User creation failed! Please check user details and try again.");
            }

            return result.Succeeded;
        }

        //Confirm Email
        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            //Decoded
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            return result.Succeeded;
        }

        //List<Claim> authClaims
        private JwtSecurityToken GetToken(List<Claim> authClaim)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //var authClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Email, user.Email),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim(ClaimTypes.Role, user.Role),
            //};

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
