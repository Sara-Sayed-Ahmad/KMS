using Knowledge_Managment_System2.Model.AddDTO;
using Knowledge_Managment_System2.Model.UpdateDTOs;
using Knowledge_Managment_System2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Knowledge_Managment_System2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly KMS_IRepository _Repository;

        public TracksController(KMS_IRepository repository)
        {
            _Repository = repository;
        }

        //Add Track
        [HttpPost("AddTrack")]
        public async Task<IActionResult> AddTrack([FromBody] AddTrackDTO track)
        {
            try
            {
                await _Repository.AddTrack(track);

                return Ok("Track added successfully");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get All Tracks
        [HttpGet]
        public async Task<IActionResult> GetTracks()
        {
            try
            {
                var tracks = await _Repository.GetAllTrack();

                return Ok(tracks);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Get Track by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrackId(int id)
        {
            try
            {
                var trackId = await _Repository.GetTrackById(id);

                return Ok(trackId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Edit Track by id
        [HttpPut("UpdateTrack{id}")]
        public async Task<ActionResult> UpdateTrack([FromRoute] int id, [FromBody] UpdatetrackDTO track, string employeeId)
        {
            try
            {
                await _Repository.UpdateTrack(id, track, employeeId);

                return Ok("Track is updated");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}