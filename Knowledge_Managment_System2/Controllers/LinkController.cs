using Knowledge_Managment_System2.Repository;
using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Model.UserDTOs;
using Knowledge_Managment_System2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;

        public LinkController(KMS_IRepository Repository)
        {
            _Repository = Repository;
        }

        //Add link
        [HttpPost("AddLink")]
        public async Task<IActionResult> AddLink(List<AddLinkDTO> links)
        {
            try
            {
                await _Repository.AddLink(links);

                return Ok("Success :)");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get Links
        [HttpGet]
        public async Task<IActionResult> GetAllLinks()
        {
            try
            {
                var links = await _Repository.GetAllLinks();

                return Ok(links);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get link By id 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLinkById(int id)
        {
            try
            {
                var linkId = await _Repository.GetLinkId(id);

                if (linkId == null)
                {
                    return NotFound();
                }

                return Ok(linkId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}