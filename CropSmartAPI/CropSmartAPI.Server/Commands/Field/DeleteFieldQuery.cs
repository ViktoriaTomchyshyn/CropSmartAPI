using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.User;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Field;

public class DeleteFieldQuery : IRequest<Result<FieldDto, string>>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteFieldQuery, Result<FieldDto, string>>
    {
        private readonly IFieldService _fieldService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(IFieldService service, IHttpContextAccessor httpContextAccessor)
        {
            _fieldService = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<FieldDto, string>> Handle(DeleteFieldQuery request,
            CancellationToken cancellationToken)
        {
            var item = _httpContextAccessor.HttpContext.Items.FirstOrDefault(i => i.Key == "UserId").Value.ToString();
            var userId = int.Parse(item);
            var obj = await _fieldService.Delete(userId, request.Id);

            if (obj == null)
            {
                return Result.Failure<FieldDto, string>("Field not found");
            }

            var result = new FieldDto
            {
                Id = obj.Id,
                Name = obj.Name,
                CadastralNumber = obj.CadastralNumber,
                Area = obj.Area,
                PropertyRight = obj.PropertyRight,
                SoilType = obj.SoilType,
                CoordinateX = obj.CoordinateX,
                CoordinateY = obj.CoordinateY,
                Userid = obj.Userid
            };

            return Result.Success<FieldDto, string>(result);
        }
    }
}
