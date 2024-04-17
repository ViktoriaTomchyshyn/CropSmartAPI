using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.User;

public class UpdateUserQuery : IRequest<Result<int, string>>
{
    public int Id { get; set; }
    public UpdateUserDto Body { get; set; }

    public class Handler : IRequestHandler<UpdateUserQuery, Result<int, string>>
    {
        private readonly IUserService _userService;

        public Handler(IUserService service)
        {
            _userService = service;
        }

        public async Task<Result<int, string>> Handle(UpdateUserQuery request,
            CancellationToken cancellationToken)
        {
            var obj = new UserDto
            {
                Name = request.Body.Name,
                Surname = request.Body.Surname,
                Email = request.Body.Email,
                Password = request.Body.Password
            };

            var id = await _userService.Update(request.Id, obj);

            return Result.Success<int, string>(id);
        }
    }
}