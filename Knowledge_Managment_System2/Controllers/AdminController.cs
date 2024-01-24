using Knowledge_Managment_System2.Model.UserDTOs;
using Knowledge_Managment_System2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knowledge_Managment_System2.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;

        public AdminController(KMS_IRepository Repository)
        {
            _Repository = Repository;
        }

        //When the admin approve the record added by employee
        [HttpPost("ApproveRecord{id}")]
        public async Task<IActionResult> ApproveRecord(int id)
        {
            try
            {
                await _Repository.ApproveRecord(id);

                return Ok("Approved :)");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //When the admin reject the record added by employee
        [HttpPost("RejectRecord{id}")]
        public async Task<IActionResult> RejectRecord(int id)
        {
            try
            {
                await _Repository.RejectRecord(id);

                return Ok("Rejected :(");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //When admin approve the record updated by employee
        [HttpPost("ApproveUpdate{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                await _Repository.ApproveUpdateRecord(id);

                return Ok("Approved :)");
            }
            catch (Exception)
            {
                throw;
            }
        }


        //When admin reject the record updated by employee
        [HttpPost("RejectUpdate{id}")]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                await _Repository.RejectUpdateRercord(id);

                return Ok("Rejected :(");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get number of employee for each role (position)
        [HttpGet("nEmployees{id}")]
        public async Task<ActionResult> GetEmployeeofPosition(int id)
        {
            try
            {
                var number = await _Repository.GetEmployeePosition(id);

                return Ok("Number: " + number);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get number of record for each department
        [HttpGet("nRecord")]
        public async Task<ActionResult> GetNumberRecordOfDepartment(int id)
        {
            try
            {
                var records = await _Repository.GetNumberRecord(id);


                return Ok(records);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Delete Track by id
        [HttpDelete("DeleteTrack{id}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            try
            {
                await _Repository.DeleteTrackById(id);

                return Ok("Track deleted successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Delete record by id
        [HttpDelete("DeleteRecord{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            try
            {
                await _Repository.DeleteRecordById(id);

                return Ok("Record deleted successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Delete link by id 
        [HttpDelete("DeleteLink{id}")]
        public async Task<IActionResult> DeleteLinkById(int id)
        {
            try
            {
                await _Repository.DeleteLinkById(id);

                return Ok("Link deleted successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Delete course by id
        [HttpDelete("DeleteCourse{id}")]
        public async Task<IActionResult> DeleteCourseById(int id)
        {
            try
            {
                await _Repository.DeleteCourseById(id);

                return Ok("Course deleted successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}