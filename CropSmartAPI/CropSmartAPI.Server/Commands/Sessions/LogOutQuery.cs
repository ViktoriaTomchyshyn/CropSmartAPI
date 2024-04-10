using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Sessions;

public class LogOutQuery : IRequest<Result<string, string>>
{
    public string Key { get; set; }

    public class Handler : IRequestHandler<LogOutQuery, Result<string, string>>
    {
        private readonly ISessionControlService _sessionControlService;

        public Handler(ISessionControlService service)
        {
            _sessionControlService = service;
        }

        public async Task<Result<string, string>> Handle(LogOutQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _sessionControlService.LogOut(request.Key);

            return Result.Success<string, string>(result.ToString());
        }
    }
}