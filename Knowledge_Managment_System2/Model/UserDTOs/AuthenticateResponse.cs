using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.UserDTOs
{
    public class AuthenticateResponse
    {
        public string EmployeeId { get; set; }

        public string Roles { get; set; }

        public string Token { get; set; }
    }
}
