using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Wx.Exercises.Application.Exercise1.Models;
using Wx.Exercises.Application.Exercise1.Queries.GetUserAndToken;

namespace Wx.Exercises.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly ILogger<ExercisesController> _logger;
        private IMediator _mediator;

        public ExercisesController(ILogger<ExercisesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("/user")]
        [ProducesResponseType(typeof(BasicResponseModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser()
        {
            var response = await _mediator.Send(new GetUserAndTokenQuery());
            return Ok(response);
        }
    }
}
