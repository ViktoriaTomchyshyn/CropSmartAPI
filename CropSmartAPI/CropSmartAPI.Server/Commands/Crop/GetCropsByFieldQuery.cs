using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Field;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Crop;

public class GetCropsByFieldQuery : IRequest<Result<List<CropDto>, string>>
{
    public int FieldId { get; set; }
    public string? SearchQuery { get; set; }

    public class Handler : IRequestHandler<GetCropsByFieldQuery, Result<List<CropDto>, string>>
    {
        private readonly ICropService _cropService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public Handler(ICropService service, IHttpContextAccessor httpContextAccessor)
        {
            _cropService = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<List<CropDto>, string>> Handle(GetCropsByFieldQuery request,
            CancellationToken cancellationToken)
        {
            var item = _httpContextAccessor.HttpContext.Items.FirstOrDefault(i => i.Key == "UserId").Value.ToString();
            var userId = int.Parse(item);
            var crop = await _cropService.Crops(userId, request.FieldId, request.SearchQuery);

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

