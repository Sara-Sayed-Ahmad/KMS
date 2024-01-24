using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.Password
{
    public class ForgetPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DefaultValue(false)]
        public bool SendEmail { get; set; }
    }
}
