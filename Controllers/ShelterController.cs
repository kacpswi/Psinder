using Microsoft.AspNetCore.Mvc;
using Psinder.Services;

namespace Psinder.Controllers
{
    [ApiController]
    [Route("api/shelter")]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterService = _shelterService;
        public ShelterController(IShelterService shelterService)
        {
            _shelterService = shelterService;
        }

        [HttpGet("id")]
        public ActionResult GetShelter([FromRoute]int id)
        {

        } 
    }
}