using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.Password
{
    public class ChangePassword
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required, DataType(DataType.Password)]
        public string currentPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string newPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("newPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}