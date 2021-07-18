using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Wx.Exercises.Application.Common.Configurations;
using Wx.Exercises.Application.Exercise1.Models;

namespace Wx.Exercises.Application.Exercise1.Queries.GetUser
{
    /// <summary>
    /// Request object
    /// </summary>
    public class GetUserQuery : IRequest<UserModel>
    {
    }

    /// <summary>
    /// Request Handler
    /// </summary>
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserModel>
    {
        private readonly ILogger<GetUserHandler> _logger;
        private readonly WxApiOptions _wxApiOptions;

        public GetUserHandler(ILogger<GetUserHandler> logger, IOptions<WxApiOptions> wxApiOptions)
        {
            _logger = logger;
            _wxApiOptions = wxApiOptions?.Value;
        }

        public Task<UserModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new UserModel()
            {
                Name = _wxApiOptions.User,
                Token = _wxApiOptions.Token
            });
        }
    }
}
