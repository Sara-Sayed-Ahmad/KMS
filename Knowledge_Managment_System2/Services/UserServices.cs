using System.Security.Claims;

namespace Knowledge_Managment_System2.Services
{
    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _httpContext;
        public UserServices(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetUserId()
        {
            return _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
