using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Field;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Crop;

public class DeleteCropQuery : IRequest<Result<CropDto, string>>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteCropQuery, Result<CropDto, string>>
    {
        private readonly ICropService _cropService;
      
        public Handler(ICropService service)
        {
            _cropService = service;
        }

        public async Task<Result<CropDto, string>> Handle(DeleteCropQuery request,
            CancellationToken cancellationToken)
        {
            var obj = await _cropService.Delete(request.Id);

            if (obj == null)
            {
                return Result.Failure<CropDto, string>("Crop not found");
            }

            var result = new CropDto
            {
                Id = obj.Id,
                Name = obj.Name,
                SowingDate = obj.SowingDate,
                HarvestDate = obj.HarverstDate,
                Fertility = obj.Fertility,
                Notes = obj.Notes,
                FieldId = obj.FieldId
            };

            return Result.Success<CropDto, string>(result);
        }
    }
}

