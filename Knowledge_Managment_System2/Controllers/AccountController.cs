using Knowledge_Managment_System2.Model;
using Knowledge_Managment_System2.Repository;
using Knowledge_Managment_System2.Helpers;
using Knowledge_Managment_System2.Model.Password;
using Knowledge_Managment_System2.Model.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;
        private readonly IAuthentication_IAuthorization _User;

        public AccountController(KMS_IRepository Repository, IAuthentication_IAuthorization User)
        {
            _Repository = Repository;
            _User = User;
        }

        //Login User
        [HttpPost]
        [Route("Authentication")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest employee)
        {
            try
            {
                var loginUser = await _User.Authenticate(employee);

                if(loginUser == null)
                {
                   return Unauthorized();
                }

                return Ok(loginUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Register new user
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                var register = await _User.Register(registerRequest);

                if(register)
                    return Ok(new { message = "Registration successful :)" });
                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("Register-Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                var register = await _User.RegisterAdmin(registerRequest);

                if (register)
                    return Ok(new { message = "Registration successful :)" });
                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Registration confirm email
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _User.ConfirmEmail(userId, token);

            if (result == true)
            {
                return RedirectToPage($"https://localhost:7061/confirmEmail.html");
            }

            return BadRequest(result);
        }

        //When user forget password
        [HttpPost("Forget_Password")]
        public async Task<IActionResult> ForgetPassword(ForgetPassword userData)
        {
            try
            {
                await _Repository.ForgetPassword(userData);

                if(userData.SendEmail == true)
                    return Ok("Send Email");

                return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Reset password
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPassword userData)
        {
            try
            {
                await _Repository.ResetPassword(userData);

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}