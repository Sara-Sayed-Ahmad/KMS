using Knowledge_Managment_System2.Model.DTOs;
using Knowledge_Managment_System2.Model;
using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.DTOs
{
    public class EmployeeDTO 

    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int PositionId { get; set; }

        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }

        public string Address { get; set; }

        public List<ExperienceDTO> Experiences { get; set; }

        public List<TrackDTO> Tracks { get; set; }

        public List<RecordDTO> Records { get; set; }

        public List<AchievementDTO> Achievements { get; set; }
    }
}
