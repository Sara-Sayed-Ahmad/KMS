using Knowledge_Managment_System2.Model;
using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Model.UpdateDTOs;
using Knowledge_Managment_System2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;

        public AchievementController(KMS_IRepository Repository)
        {
            _Repository = Repository;
        }

        //Get achievements
        [HttpGet]
        public async Task<IActionResult> GetAchievement()
        {
            try
            {
                var achievement = await _Repository.GetAchievement();
                return Ok(achievement);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get Achievement by id
        [HttpGet("byId")]
        public async Task<IActionResult> GetAchievementId(string idemp)
        {
            try
            {
                var achieve = await _Repository.GetAchievementById(idemp);

                return Ok(achieve);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //When user saved course
        [HttpPost("Saved")]
        public async Task<IActionResult> SaveCourses([FromBody] SaveCourse course)
        {
            try
            {
                await _Repository.SaveCourse(course);

                return Ok("Save Course");
            }
            catch(Exception)
            {
                throw;
            }
        }

        //When user finished course 
        [HttpPut("Finished")]
        public async Task<IActionResult> FinishCourse([FromBody] AchieveDate data) 
        {
            try
            {
                await _Repository.FinishCourse(data);

                return Ok("The course has been completed successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}