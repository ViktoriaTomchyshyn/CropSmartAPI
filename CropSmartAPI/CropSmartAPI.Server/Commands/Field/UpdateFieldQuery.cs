using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.DAL.Entities;
using CropSmartAPI.Server.Commands.User;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Field;

public class UpdateFieldQuery : IRequest<Result<int, string>>
{
    public int Id { get; set; }
    public string CadastralNumber { get; set; }
    public string Name { get; set; }
    public double Area { get; set; }
    public PropertyRight PropertyRight { get; set; }
    public SoilType SoilType { get; set; }
    public double CoordinateX { get; set; }
    public double CoordinateY { get; set; }

    public class Handler : IRequestHandler<UpdateFieldQuery, Result<int, string>>
    {
        private readonly IFieldService _fieldService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(IFieldService service, IHttpContextAccessor httpContextAccessor)
        {
            _fieldService = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<int, string>> Handle(UpdateFieldQuery request,
            CancellationToken cancellationToken)
        {
            var item = _httpContextAccessor.HttpContext.Items.FirstOrDefault(i => i.Key == "UserId").Value.ToString();
            var userId = int.Parse(item);
            var obj = new FieldDto
            {
                Name = request.Name,
                CadastralNumber = request.CadastralNumber,
                Area = request.Area,
                PropertyRight = request.PropertyRight,
                SoilType = request.SoilType,
                CoordinateX = request.CoordinateX,
                CoordinateY = request.CoordinateY,
                Userid = userId,
            };

            var id = await _fieldService.Update(request.Id, obj);

            return Result.Success<int, string>(id);
        }
    }
}