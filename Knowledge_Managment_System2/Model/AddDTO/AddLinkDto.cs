using Realms;

namespace Knowledge_Managment_System2.Model.AddDTO
{
    public class AddLinkDTO
    {
        [Required]
        public string LinkName { get; set; }

        [Required]
        public string LinkData { get; set; }

        [Required]
        public int RecordId { get; set; }
    }
}
