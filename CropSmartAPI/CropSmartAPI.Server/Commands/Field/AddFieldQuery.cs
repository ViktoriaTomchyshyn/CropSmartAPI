using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.DAL.Entities;
using CropSmartAPI.Server.Commands.User;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Field;

public class AddFieldQuery : IRequest<Result<int, string>>
{
    public string CadastralNumber { get; set; }
    public string Name { get; set; }
    public double Area { get; set; }
    public PropertyRight PropertyRight { get; set; }
    public double CoordinateX { get; set; }
    public double CoordinateY { get; set; }
    public int Userid { get; set; }

    public class Handler : IRequestHandler<AddFieldQuery, Result<int, string>>
    {
        private readonly IFieldService _fieldService;

        public Handler(IFieldService service)
        {
            _fieldService = service;
        }

        public async Task<Result<int, string>> Handle(AddFieldQuery request,
            CancellationToken cancellationToken)
        {
            var obj = new FieldDto
            {
                Name = request.Name,
                CadastralNumber = request.CadastralNumber,
                Area = request.Area,
                PropertyRight = request.PropertyRight,
                CoordinateX = request.CoordinateX,
                CoordinateY = request.CoordinateY,
                Userid = request.Userid,
            };

            var id = await _fieldService.Create(obj);

            return Result.Success<int, string>(id);
        }
    }
}