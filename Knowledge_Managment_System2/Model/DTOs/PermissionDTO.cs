using Knowledge_Managment_System2.Model.DTOs;
using Knowledge_Managment_System2.Model;
using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.DTOs
{

    public class PermissionDTO
    {
        public string Id { get; set; }

        public IList<EmployeeDTO> Employees { get; set; }
    }
}
