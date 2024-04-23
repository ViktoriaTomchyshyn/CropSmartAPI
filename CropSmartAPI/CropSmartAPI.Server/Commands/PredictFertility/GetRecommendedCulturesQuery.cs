using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.PredictObjects;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.PredictFertility;

public class GetRecommendedCulturesQuery : IRequest<Result<CropRotationResult, string>>
{
    public int FieldId { get; set; }

    public class Handler : IRequestHandler<GetRecommendedCulturesQuery, Result<CropRotationResult, string>>
    {
        private readonly INextCropDefinitionService _nextCropService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(INextCropDefinitionService service, IHttpContextAccessor httpContextAccessor)
        {
            _nextCropService = service;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<CropRotationResult, string>> Handle(GetRecommendedCulturesQuery request,
            CancellationToken cancellationToken)
        {
            var obj = await _nextCropService.AnalyzeCropRotation(request.FieldId);

            return Result.Success<CropRotationResult, string>(obj);
        }
    }
}
