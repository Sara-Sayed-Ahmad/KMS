using Realms;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Realms.RequiredAttribute;

namespace Knowledge_Managment_System2.Model.Password
{
    public class ResetPassword
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}