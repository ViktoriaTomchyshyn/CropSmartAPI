using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Crop;

public class AddCropQuery : IRequest<Result<int, string>>
{
    public string Name { get; set; }
    public DateTime SowingDate { get; set; }
    public DateTime HarvestDate { get; set; }
    public double? Fertility { get; set; }
    public string Notes { get; set; }
    public int FieldId { get; set; }

    public class Handler : IRequestHandler<AddCropQuery, Result<int, string>>
    {
        private readonly ICropService _cropService;
       
        public Handler(ICropService service)
        {
            _cropService = service;
        }

        public async Task<Result<int, string>> Handle(AddCropQuery request,
            CancellationToken cancellationToken)
        {
           
            var obj = new CropDto
            {
                Name = request.Name,
                SowingDate = request.SowingDate,
                HarvestDate = request.HarvestDate,
                Fertility = request.Fertility,
                Notes = request.Notes,
                FieldId = request.FieldId,
            };

            var id = await _cropService.Create(obj);

            return Result.Success<int, string>(id);
        }
    }
}
