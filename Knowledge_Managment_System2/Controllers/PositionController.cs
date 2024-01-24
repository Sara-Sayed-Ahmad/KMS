using Knowledge_Managment_System2.Repository;
using Knowledge_Managment_System2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Knowledge_Managment_System2.Model.DTOs;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;

        public PositionController(KMS_IRepository Repository)
        {
            _Repository = Repository;
        }

        //Get all Position
        [HttpGet]
        public async Task<IActionResult> GetAllPosition()
        {
            try
            {
                var positions = await _Repository.GetAllPosition();

                return Ok(positions);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get position by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositionById(int id)
        {
            try
            {
                var position = await _Repository.GetPosition(id);

                if (position == null)
                {
                    return NotFound();
                }
                return Ok(position);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}