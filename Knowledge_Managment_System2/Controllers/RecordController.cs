using Knowledge_Managment_System2.Authorization;
using Knowledge_Managment_System2.Repository;
using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Model.UpdateDTOs;
using Knowledge_Managment_System2.Model.UserDTOs;
using Knowledge_Managment_System2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;

        public RecordController(KMS_IRepository Repository)
        {
            _Repository = Repository;
        }

        //Add record
        [HttpPost("AddRecord")]
        public async Task<IActionResult> AddRecords([FromBody] AddRecordDTO record)
        {
            try
            {
                await _Repository.AddRecord(record);

                return Ok("Record added successfully :)");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get All Records
        [HttpGet]
        public async Task<ActionResult> GetRecords()
        {
            try
            {
                var records = await _Repository.GetAllRecord();

                return Ok(records);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get record by id 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordById(int id)
        {
            try
            {
                var record = await _Repository.GetRecordById(id);

                if (record == null)
                {
                    NotFound();
                }

                return Ok(record);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Edit Record by id
        [HttpPut("UpdateRecord{id}")]
        public async Task<ActionResult<Record>> UpdateRecord([FromRoute] int id, [FromBody] UpdateRecordDTO record, string employeeId)
        {
            try
            {
                await _Repository.UpdateRecord(id, record, employeeId);

                return Ok("Record is updated");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}