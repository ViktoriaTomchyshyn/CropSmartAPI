using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Core.SessionObjects;
using CropSmartAPI.DAL.Entities;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Sessions;

public class LogInQuery: IRequest<Result<LoginResult, string>>
{
    public string Login { get; set; }
    public string Password { get; set; }
    

    public class Handler : IRequestHandler<LogInQuery, Result<LoginResult, string>>
    {
        private readonly ISessionControlService _sessionControlService;

        public Handler(ISessionControlService service)
        {
            _sessionControlService = service;
        }

        public async Task<Result<LoginResult, string>> Handle(LogInQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _sessionControlService.LogIn(request.Login, request.Password);

            return Result.Success<LoginResult, string>(result);
        }
    }
}
