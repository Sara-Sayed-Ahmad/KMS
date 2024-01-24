using Knowledge_Managment_System2.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge_Managment_System2.Model
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Track> Tracks { get; set; }
    }
}
