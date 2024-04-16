using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.User;

public class DeleteUserQuery : IRequest<Result<UserDto, string>>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteUserQuery, Result<UserDto, string>>
    {
        private readonly IUserService _userService;
       
        public Handler(IUserService service)
        {
            _userService = service;
        }

        public async Task<Result<UserDto, string>> Handle(DeleteUserQuery request,
            CancellationToken cancellationToken)
        {
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
