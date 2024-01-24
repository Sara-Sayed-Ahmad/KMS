using Knowledge_Managment_System2.Model;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Knowledge_Managment_System2.Model
{
    public class Permission : IdentityRole
    {
        //[Key]
        //public int Permission_Id { get; set; }

        //public string Permission_Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
