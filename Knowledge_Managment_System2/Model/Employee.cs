using Knowledge_Managment_System2.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge_Managment_System2.Model
{
    public class Employee : IdentityUser
    {
        //[Key]
        //public int EmployeeId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public string? Address { get; set; }

        public List<Experience> Experiences { get; set; }

        public List<Track> Tracks { get; set; }

        public List<Record> Records { get; set; }

        public ICollection<Permission> Permissions { get; set; }

        public ICollection<Course> Courses { get; set; }

        public List<Achievement> Achievements { get; set; }

    }
}
