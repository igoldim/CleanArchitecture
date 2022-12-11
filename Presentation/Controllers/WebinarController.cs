using Application.Webinars.Commands.CreateWebinar;
using Application.Webinars.Queries.GetWebinarById;
using Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public sealed class WebinarController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public WebinarController(ApplicationDbContext dbContext) => _dbContext = dbContext;

        [HttpGet("{webinar:guid}")]
        [ProducesResponseType(typeof(WebinarResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWebinar(Guid webinarId, CancellationToken cancellationToken)
        {
            var query = new GetWebinarByIdQuery(webinarId);

            var webinar = await Sender.Send(query, cancellationToken);


            //return Ok("Ok");
            return Ok(webinar);
            
            /*return StatusCode(StatusCodes.Status200OK, new
            {
                sucessful = true,
                data = webinar,
            });*/
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateWebinarAsync(
            [FromBody] CreateWebinarRequest request,
            CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateWebinarCommand>();

            //var webinarId = Guid.NewGuid();
            var webinarId = await Sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetWebinar), new { webinarId }, webinarId);
            
            /*return StatusCode(StatusCodes.Status201Created, new
            {
                sucessful = true,
                data = webinar,
            });*/

        }
    }
}
