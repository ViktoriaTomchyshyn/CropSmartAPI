using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Fertilizer;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Field;

public class GetFieldsByUserQuery : IRequest<Result<List<FieldDto>, string>>
{
    public int UserId { get; set; }

    public class Handler : IRequestHandler<GetFieldsByUserQuery, Result<List<FieldDto>, string>>
    {
        private readonly IFieldService _fieldService;

        public Handler(IFieldService service)
        {
            _fieldService = service;
        }

        public async Task<Result<List<FieldDto>, string>> Handle(GetFieldsByUserQuery request,
            CancellationToken cancellationToken)
        {
            var field = await _fieldService.GetByUser(request.UserId);

            if (!field?.Any() ?? true)
            {
                return Result.Failure<List<FieldDto>, string>("Field not found");
            }

            var result = field.Select(p => new FieldDto
            {
                Id = p.Id,
                CadastralNumber = p.CadastralNumber,
                Name = p.Name,
                Area = p.Area,
                PropertyRight = p.PropertyRight,
                Userid = p.Userid
            }).ToList();

            return Result.Success<List<FieldDto>, string>(result);
        }
    }
}
