using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CropSmartAPI.Server.Commands.User;

public class CreateUserQuery : IRequest<Result<int, string>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public class Handler : IRequestHandler<CreateUserQuery, Result<int, string>>
    {
        private readonly IUserService _userService;

        public Handler(IUserService service)
        {
            _userService = service;
        }

        public async Task<Result<int, string>> Handle(CreateUserQuery request,
            CancellationToken cancellationToken)
        {
            var obj = new UserDto
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Password = request.Password
            };

            var id = await _userService.Create(obj);

            return Result.Success<int, string>(id);
        }
    }
}