using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Server.Commands.Crop;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.Fertilizer;

public class DeleteFertilizerQuery : IRequest<Result<FertilizerDto, string>>
{
    public int Id { get; set; }

    public class Handler : IRequestHandler<DeleteFertilizerQuery, Result<FertilizerDto, string>>
    {
        private readonly IFertilizerService _fertilizerService;

        public Handler(IFertilizerService service)
        {
            _fertilizerService = service;
        }

        public async Task<Result<FertilizerDto, string>> Handle(DeleteFertilizerQuery request,
            CancellationToken cancellationToken)
        {
            var obj = await _fertilizerService.Delete(request.Id);

            if (obj == null)
            {
                return Result.Failure<FertilizerDto, string>("Fertilizer not found");
            }

            var result = new FertilizerDto
            {
                Id = obj.Id,
                Name = obj.Name,
                CropId = obj.CropId,
                Quantity = obj.Quantity,
            };

            return Result.Success<FertilizerDto, string>(result);
        }
    }
}

