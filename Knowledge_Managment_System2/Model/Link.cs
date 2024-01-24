using Knowledge_Managment_System2.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge_Managment_System2.Model
{
    public class Link
    {
        [Key]
        public int LinkId { get; set; }

        public string LinkName { get; set; }

        public string LinkData { get; set; }

        public int RecordId { get; set; }
        public Record Record { get; set; }
    }
}