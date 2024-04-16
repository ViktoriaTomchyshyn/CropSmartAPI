using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Crop;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Fertilizer;

public class GetFertilizerQuery : IRequest<Result<FertilizerDto, string>>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<GetFertilizerQuery, Result<FertilizerDto, string>>
    {
        private readonly IFertilizerService _fertilizerService;
       
        public Handler(IFertilizerService service)
        {
            _fertilizerService = service;
        }

        public async Task<Result<FertilizerDto, string>> Handle(GetFertilizerQuery request,
            CancellationToken cancellationToken)
        {
            var obj = await _fertilizerService.Get(request.Id);

            if (obj == null)
            {
                return Result.Failure<FertilizerDto, string>("Fertilizer not found");
            }

            var result = new FertilizerDto
            {
                Id = obj.Id,
                Name = obj.Name,
                Quantity = obj.Quantity,
                CropId = obj.CropId,
            };

            return Result.Success<FertilizerDto, string>(result);
        }
    }
}
