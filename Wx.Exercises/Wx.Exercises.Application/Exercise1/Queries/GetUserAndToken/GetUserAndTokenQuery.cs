using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Wx.Exercises.Application.Exercise1.Models;

namespace Wx.Exercises.Application.Exercise1.Queries.GetUserAndToken
{
    /// <summary>
    /// Request object
    /// </summary>
    public class GetUserAndTokenQuery : IRequest<BasicResponseModel>
    {
    }

    /// <summary>
    /// Request Handler
    /// </summary>
    public class GetUserAndTokenQueryHandler : IRequestHandler<GetUserAndTokenQuery, BasicResponseModel>
    {
        private readonly ILogger<GetUserAndTokenQueryHandler> _logger;

        public GetUserAndTokenQueryHandler(ILogger<GetUserAndTokenQueryHandler> logger)
        {
            _logger = logger;
        }

        public async Task<BasicResponseModel> Handle(GetUserAndTokenQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(100);

            return new BasicResponseModel()
            {
                Name = "Hello! 👋 It's me Reggie",
                Token = ""
            };
        }
    }
}
