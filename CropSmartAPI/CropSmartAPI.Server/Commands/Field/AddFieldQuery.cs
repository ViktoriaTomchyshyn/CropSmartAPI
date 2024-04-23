using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.DAL.Entities;
using CropSmartAPI.Server.Commands.User;
using CSharpFunctionalExtensions;
using MediatR;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CropSmartAPI.Server.Commands.Field;

public class AddFieldQuery : IRequest<Result<int, string>>
{
    public string CadastralNumber { get; set; }
    public string Name { get; set; }
    public double Area { get; set; }
    public PropertyRight PropertyRight { get; set; }
    public SoilType SoilType { get; set; }
    public double CoordinateX { get; set; }
    public double CoordinateY { get; set; }

    public class Handler : IRequestHandler<AddFieldQuery, Result<int, string>>
    {
        private readonly IFieldService _fieldService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Handler(IFieldService service, IHttpContextAccessor httpContextAccessor)
        {
            _fieldService = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<int, string>> Handle(AddFieldQuery request,
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

            var id = await _fieldService.Create(obj);

            return Result.Success<int, string>(id);
        }
    }
}