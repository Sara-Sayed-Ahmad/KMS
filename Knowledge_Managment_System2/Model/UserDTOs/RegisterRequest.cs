using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.UserDTOs
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Position is Required")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string VerifyPassword { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string Roles { get; set; }
    }
}