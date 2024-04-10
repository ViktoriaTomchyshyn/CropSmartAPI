using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.User;

public class DeleteUserQuery : IRequest<Result<UserDto, string>>
{
    public string Key { get; set; }
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteUserQuery, Result<UserDto, string>>
    {
        private readonly IUserService _userService;
        private readonly ISessionControlService _sessionControlService;

        public Handler(IUserService service, ISessionControlService sessionControlService)
        {
            _userService = service;
            _sessionControlService = sessionControlService;
        }

        public async Task<Result<UserDto, string>> Handle(DeleteUserQuery request,
            CancellationToken cancellationToken)
        {
            _sessionControlService.IsLoggedIn(request.Key);
            var obj = await _userService.Delete(request.Id);

            if (obj == null)
            {
                return Result.Failure<UserDto, string>("User not found");
            }

            var result = new UserDto
            {
                Id = obj.Id,
                Name = obj.Name,
                Surname = obj.Surname,
                Email = obj.Email,
                Password = obj.Password
            };

            return Result.Success<UserDto, string>(result);
        }
    }
}
