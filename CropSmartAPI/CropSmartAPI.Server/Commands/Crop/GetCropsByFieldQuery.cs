using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Field;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Crop;

public class GetCropsByFieldQuery : IRequest<Result<List<CropDto>, string>>
{
    public int FieldId { get; set; }

    public class Handler : IRequestHandler<GetCropsByFieldQuery, Result<List<CropDto>, string>>
    {
        private readonly ICropService _cropService;

        public Handler(ICropService service)
        {
            _cropService = service;
        }

        public async Task<Result<List<CropDto>, string>> Handle(GetCropsByFieldQuery request,
            CancellationToken cancellationToken)
        {
            var crop = await _cropService.GetByField(request.FieldId);

            if (!crop?.Any() ?? true)
            {
                return Result.Failure<List<CropDto>, string>("Crop not found");
            }

            var result = crop.Select(p => new CropDto
            {
                Id = p.Id,
                Name = p.Name,
                SowingDate = p.SowingDate,
                HarverstDate = p.HarverstDate,
                Fertility = p.Fertility,
                Notes = p.Notes,
                FieldId = p.FieldId,
            }).ToList();

            return Result.Success<List<CropDto>, string>(result);
        }
    }
}

