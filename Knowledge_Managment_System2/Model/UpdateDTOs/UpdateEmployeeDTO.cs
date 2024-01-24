using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.UpdateDTOs
{
    public class UpdateEmployeeDTOs
    {
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(18, MinimumLength =10)]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}