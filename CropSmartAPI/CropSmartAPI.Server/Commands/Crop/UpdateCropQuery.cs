using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.DAL.Entities;
using CropSmartAPI.Server.Commands.Field;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Crop;

public class UpdateCropQuery : IRequest<Result<int, string>>
{
    public string Key { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime SowingDate { get; set; }
    public DateTime HarverstDate { get; set; }
    public double Fertility { get; set; }
    public string Notes { get; set; }
    public int FieldId { get; set; }

    public class Handler : IRequestHandler<UpdateCropQuery, Result<int, string>>
    {
        private readonly ICropService _cropService;
        private readonly ISessionControlService _sessionControlService;

        public Handler(ICropService service, ISessionControlService sessionControlService)
        {
            _cropService = service;
            _sessionControlService = sessionControlService;
        }

        public async Task<Result<int, string>> Handle(UpdateCropQuery request,
            CancellationToken cancellationToken)
        {
            _sessionControlService.IsLoggedIn(request.Key);
            var obj = new CropDto
            {
                Name = request.Name,
                SowingDate = request.SowingDate,
                HarverstDate = request.HarverstDate,
                Fertility = request.Fertility,
                Notes = request.Notes,
                FieldId = request.FieldId,
            };

            var id = await _cropService.Update(request.Id, obj);

            return Result.Success<int, string>(id);
        }
    }
}