using Knowledge_Managment_System2.Repository;
using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Model.UserDTOs;
using Knowledge_Managment_System2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;

        public CoursesController(KMS_IRepository Repository)
        {
            _Repository = Repository;
        }

        //Add Courses
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourses([FromBody] AddCourseDTO course)
        {
            try
            {
                await _Repository.AddCourse(course);
                return Ok("Course added successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get Courses
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var course = await _Repository.GetAllCourses();

                return Ok(course);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get course by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            try
            {
                var courseId = await _Repository.GetCourse(id);

                if (courseId == null)
                {
                    return NotFound();
                }

                return Ok(courseId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}