using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Knowledge_Managment_System2.Model.DTOs;
using Knowledge_Managment_System2.Model;

namespace Knowledge_Managment_System2.Model.DTOs
{
    public class CourseDTO 

    {
        [Key]
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string Link_course { get; set; }

        public string RequiredSkills { get; set; }

        public bool Status { get; set; }

        public bool Wait { get; set; }

        public bool Mandantory { get; set; }

        public int PositionId { get; set; }

        public int TrackId { get; set; }

        public List<AchievementDTO> Achievements { get; set; }
    }
}