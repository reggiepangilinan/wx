using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Wx.Exercises.Application.Common.Enums;
using Wx.Exercises.Application.Exercise1.Models;
using Wx.Exercises.Application.Exercise1.Queries.GetUser;
using Wx.Exercises.Application.Exercise3.Commands.CalculateTrolley;

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

        [HttpGet("user")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser()
        {
            var response = await _mediator.Send(new GetUserQuery());
            return Ok(response);
        }

        [HttpGet("sort")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery(Name ="sortOption")] SortOption sortOption)
        {
            var response = await _mediator.Send(new GetProductsQuery()
            {
                SortOption = sortOption
            });
            return Ok(response);
        }

        [HttpPost("trolleyTotal")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalculateTrolley([FromBody] CalculateTrolleyCommand command)
            => Ok(await _mediator.Send(command));
    }
}
