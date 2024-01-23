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

        public Handler(IFieldService service)
        {
            _fieldService = service;
        }

        public async Task<Result<FieldDto, string>> Handle(DeleteFieldQuery request,
            CancellationToken cancellationToken)
        {
            var obj = await _fieldService.Delete(request.Id);

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
                CoordinateX = obj.CoordinateX,
                CoordinateY = obj.CoordinateY,
                Userid = obj.Userid
            };

            return Result.Success<FieldDto, string>(result);
        }
    }
}
