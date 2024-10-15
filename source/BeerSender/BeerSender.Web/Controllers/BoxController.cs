using BeerSender.Domain.Boxes;
using Microsoft.AspNetCore.Mvc;

namespace BeerSender.Web.Controllers
{
    [Route("api/command/[controller]")]
    [ApiController]
    public class BoxController(CommandRouter router) : ControllerBase
    {
        [HttpPost]
        [Route("create")]
        public IActionResult CreateBox([FromBody] CreateBox command)
        {
            router.HandleCommand(command);
            return Accepted();
        }

        [HttpPost]
        [Route("addbottle")]
        public IActionResult AddBottle([FromBody] AddBeerBottle command)
        {
            router.HandleCommand(command);
            return Accepted();
        }
    }
}
