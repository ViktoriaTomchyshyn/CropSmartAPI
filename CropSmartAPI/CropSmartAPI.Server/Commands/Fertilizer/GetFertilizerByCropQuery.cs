using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Fertilizer;

public class GetFertilizerByCropQuery : IRequest<Result<List<FertilizerDto>, string>>
{
    public int CropId { get; set; }
    public string? SearchQuery { get; set; }


    public class Handler : IRequestHandler<GetFertilizerByCropQuery, Result<List<FertilizerDto>, string>>
    {
        private readonly IFertilizerService _fertilizerService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public Handler(IFertilizerService service, IHttpContextAccessor httpContextAccessor)
        {
            _fertilizerService = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<List<FertilizerDto>, string>> Handle(GetFertilizerByCropQuery request,
            CancellationToken cancellationToken)
        {
            var item = _httpContextAccessor.HttpContext.Items.FirstOrDefault(i => i.Key == "UserId").Value.ToString();
            var userId = int.Parse(item);
            var fertilizer = await _fertilizerService.Fertilizers(userId, request.CropId, request.SearchQuery);

            if (!fertilizer?.Any() ?? true)
            {
                return Result.Failure<List<FertilizerDto>, string>("Fertilizer not found");
            }

            var result = fertilizer.Select(p => new FertilizerDto
            {
                Id = p.Id,
                Name = p.Name,
                CropId = p.CropId,
                Quantity = p.Quantity,
            }).ToList();

            return Result.Success<List<FertilizerDto>, string>(result);
        }
    }
}
