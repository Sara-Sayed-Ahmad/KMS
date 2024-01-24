using Knowledge_Managment_System2.Repository;
using Microsoft.AspNetCore.Http;

namespace Knowledge_Managment_System2.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        //HttpContext: HTTP-specific information about an individual HTTP request
        //KMS_IRepository: retrieval for method (GetEmployeeById)
        //IJwtUtils: Generate token and Validate
        public async Task Invoke(HttpContext context, KMS_IRepository repository, IJwtUtils jwtUits)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            //validate token
            var userId = jwtUits.ValidateToken(token);

            //if (userId != null)
            //{
            //    // attach user to context on successful jwt validation
            //    context.Items["Employee"] = repository.GetEmployeeById(userId.Value);
            //}

            await _next(context);
        }
    }
}
