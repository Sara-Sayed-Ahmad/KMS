using Knowledge_Managment_System2.Repository;
using Knowledge_Managment_System2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;

        public DepartmentController(KMS_IRepository Repository)
        {
            _Repository = Repository;
        }

        //Get Department
        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var departments = await _Repository.GetAllDepartment();

                return Ok(departments);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get Department By id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            try
            {
                var department = await _Repository.GetDepartmentById(id);

                if (department == null)
                {
                    return NotFound();
                }

                return Ok(department);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}