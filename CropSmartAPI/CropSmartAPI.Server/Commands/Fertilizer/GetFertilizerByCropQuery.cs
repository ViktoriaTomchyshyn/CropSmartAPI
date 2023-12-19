using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Fertilizer;

public class GetFertilizerByCropQuery : IRequest<Result<List<FertilizerDto>, string>>
{
    public int CropId { get; set; }

    public class Handler : IRequestHandler<GetFertilizerByCropQuery, Result<List<FertilizerDto>, string>>
    {
        private readonly IFertilizerService _fertilizerService;

        public Handler(IFertilizerService service)
        {
            _fertilizerService = service;
        }

        public async Task<Result<List<FertilizerDto>, string>> Handle(GetFertilizerByCropQuery request,
            CancellationToken cancellationToken)
        {
            var fertilizer = await _fertilizerService.GetByCrop(request.CropId);

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
