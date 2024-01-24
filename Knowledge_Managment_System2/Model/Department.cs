using Knowledge_Managment_System2.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge_Managment_System2.Model
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public List<Record> Records { get; set; }

        public List<Position> Positions { get; set; }
    }
}
