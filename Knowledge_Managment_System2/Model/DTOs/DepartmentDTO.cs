using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.DTOs
{
    public class DepartmentDTO 

    {
        [Key]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public List<PositionDTO> Positions { get; set; }

        public List<RecordDTO> Records { get; set; }
    }
}