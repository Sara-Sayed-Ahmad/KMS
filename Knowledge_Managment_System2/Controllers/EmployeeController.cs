using Azure;
using Knowledge_Managment_System2.Authorization;
using Knowledge_Managment_System2.Helpers;
using Knowledge_Managment_System2.Repository;
using Knowledge_Managment_System2.Model.Password;
using Knowledge_Managment_System2.Model.UpdateDTOs;
using Knowledge_Managment_System2.Model.UserDTOs;
using Knowledge_Managment_System2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Knowledge_Managment_System2.Model.AddDTO;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;

        public EmployeeController(KMS_IRepository Repository)
        {
            _Repository = Repository;
        }

        //Get Employee
        [HttpGet("All")]
        public async Task<IActionResult> GetEmplyees()
        {
            try
            {
                var employees = await _Repository.GetEmployee();

                return Ok(employees);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get employee By id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeId(string id)
        {
            try
            {
                var employee = await _Repository.GetEmployeeById(id);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Edit information employee
        [HttpPut("UpdateEmp{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromRoute] string id, [FromBody] UpdateEmployeeDTOs employee)
        {
            try
            {
                await _Repository.UpdateEmployee(id, employee);

                return Ok("Employee information is updated");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Add Experiences
        [HttpPost("AddExperence")]
        public async Task<ActionResult> AddExperience([FromBody] AddExperienceDto data)
        {
            try
            {
                await _Repository.addExperience(data);

                return Ok("Experience is added");

            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get experiences for employee by id
        [HttpGet("ExperienceByid")]
        public async Task<IActionResult> GetExperience(string id)
        {
            try
            {
                var data = await _Repository.GetExperience(id);

                if(data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Chamge password
        [HttpPut("ChangePasswprd")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePassword password)
        {
            try
            {
                await _Repository.ChangePassword(password);

                return Ok("Password Changed successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}