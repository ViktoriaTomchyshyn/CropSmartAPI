using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.PredictObjects;
using CropSmartAPI.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace CropSmartAPI.Server.Commands.PredictFertility;

public class PredictFertilityQuery : IRequest<Result<PredictResult, string>>
{
    public string Key { get; set; }
    public int FieldId { get; set; }

    public class Handler : IRequestHandler<PredictFertilityQuery, Result<PredictResult, string>>
    {
        private readonly IFertilityPredictionService _predService;
        private readonly ISessionControlService _sessionControlService;

        public Handler(IFertilityPredictionService service, ISessionControlService sessionControlService)
        {
            _predService = service;
            _sessionControlService = sessionControlService;
        }

        public async Task<Result<PredictResult, string>> Handle(PredictFertilityQuery request,
            CancellationToken cancellationToken)
        {
            _sessionControlService.IsLoggedIn(request.Key);
            var result = await _predService.PredictFertility(request.FieldId);

            if (result == null)
            {
                return Result.Failure<PredictResult, string>("Prediction error, make sure there is any info about this field");
            }

            var finalResult = new PredictResult
            {
                LowerFertilityValue = result.LowerFertilityValue,
                UpperFertilityValue = result.UpperFertilityValue,
                AverageFertilityValue = result.AverageFertilityValue,
                ResultType = result.ResultType,
            };

            return Result.Success<PredictResult, string>(finalResult);
        }
    }
}