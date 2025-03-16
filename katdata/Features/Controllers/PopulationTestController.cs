using katdata.Features.Entities.Population;
using katdata.Services;
using Microsoft.AspNetCore.Mvc;

namespace katdata.Features.Controllers
{
    [ApiController]
    [Route("api/popTest")]
    public class PopulationTestController(GameSetUp setup) : ControllerBase
    {
        [HttpGet("run")]
        public async Task<IActionResult> RunSimulation()
        {
            await setup.SetUp();
            return Ok("Something happened");
        }


    };
}
