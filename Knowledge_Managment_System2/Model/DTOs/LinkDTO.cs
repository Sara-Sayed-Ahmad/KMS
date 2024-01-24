using Knowledge_Managment_System2.Model.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model.DTOs
{
    public class LinkDTO 

    {
        [Key]
        public int LinkId { get; set; }

        public string LinkName { get; set; }

        public string LinkData { get; set; }

        public int RecordId { get; set; }
    }
}
