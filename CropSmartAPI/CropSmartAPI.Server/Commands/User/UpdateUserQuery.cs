using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.User;

public class UpdateUserQuery : IRequest<Result<int, string>>
{
    public string Key { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public class Handler : IRequestHandler<UpdateUserQuery, Result<int, string>>
    {
        private readonly IUserService _userService;
        private readonly ISessionControlService _sessionControlService;

        public Handler(IUserService service, ISessionControlService sessionControlService)
        {
            _userService = service;
            _sessionControlService = sessionControlService;
        }

        public async Task<Result<int, string>> Handle(UpdateUserQuery request,
            CancellationToken cancellationToken)
        {
            _sessionControlService.IsLoggedIn(request.Key);
            var obj = new UserDto
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password
            };

            var id = await _userService.Update(request.Id, obj);

            return Result.Success<int, string>(id);
        }
    }
}