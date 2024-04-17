using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Fertilizer;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Field;

public class GetFieldsByUserQuery : IRequest<Result<List<FieldDto>, string>>
{

    public string? SearchQuery { get; set; } 


    public class Handler : IRequestHandler<GetFieldsByUserQuery, Result<List<FieldDto>, string>>
    {
        private readonly IFieldService _fieldService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public Handler(IFieldService service, IHttpContextAccessor httpContextAccessor)
        {
            _fieldService = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<List<FieldDto>, string>> Handle(GetFieldsByUserQuery request,
            CancellationToken cancellationToken)
        {
            var item = _httpContextAccessor.HttpContext.Items.FirstOrDefault(i=> i.Key == "UserId").Value.ToString();
            var userId = int.Parse(item);
            var field = await _fieldService.Fields(userId, request.SearchQuery);

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
                SoilType = p.SoilType,
                CoordinateX = p.CoordinateX,
                CoordinateY = p.CoordinateY,
                Userid = p.Userid
            }).ToList();

            return Result.Success<List<FieldDto>, string>(result);
        }
    }
}
