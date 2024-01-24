using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.AddDTO
{
    public class AddCourseDTO
    {
        [Required]
        public string CourseName { get; set; }

        [Required]
        public string RequiredSkills { get; set; }

        [Required]
        public bool Mandantory { get; set; }

        [Required]
        public string Link_course { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public int TrackId { get; set; }

    }
}
