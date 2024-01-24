using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Knowledge_Managment_System2.Model.DTOs;

namespace Knowledge_Managment_System2.Model.DTOs
{
    public class PositionDTO 
       
    {
        [Key]
        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public int DepartmentId { get; set; }

        public List<EmployeeDTO> Employees { get; set; }

        public List<TrackDTO> Tracks { get; set; }
    }
}