using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.UserDTOs
{
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}