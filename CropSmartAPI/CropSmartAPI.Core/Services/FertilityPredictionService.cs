using CropSmart_ML;
using CropSmartAPI.Core.PredictObjects;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.DAL.Context;
using CropSmartAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services;

public class FertilityPredictionService : IFertilityPredictionService
{
    private readonly DataContext _dbContext;
    private readonly ILogger _logger;

    public FertilityPredictionService(DataContext dataContext, ILogger<FertilizerService> logger)
    {
        _dbContext = dataContext;
        _logger = logger;
    }

    public async Task<PredictResult> PredictFertility(int fieldId)
    {
        var crops = await _dbContext.Crops.Where(p => p.FieldId == fieldId).ToListAsync();
        if (!crops.Any())
        {
            return new PredictResult()
            {
                LowerFertilityValue = 0,
                UpperFertilityValue = 0,
                AverageFertilityValue = 0,
                ResultType = 0
            };
        }
        var modelInput = GetAverageFertility(crops);

        var sampleData = new CropPredictModel.ModelInput()
        {
            Fertility = modelInput
        };

        var result = CropPredictModel.Predict(sampleData);

        var lowerValue = result.Fertility_LB[0];
        var upperValue = result.Fertility_UB[0];
        if (upperValue > 100) upperValue = 100;
        var averageValue = (lowerValue + upperValue) / 2;

        _logger.LogInformation(lowerValue + ", " + upperValue);


        return new PredictResult()
        {
            LowerFertilityValue = lowerValue,
            UpperFertilityValue = upperValue,
            AverageFertilityValue = averageValue,
            ResultType = GetResultType(lowerValue, upperValue)
        };
    }

    private float GetAverageFertility(List<Crop> crops)
    {
        var cropsWithFertility = crops.Where(crop => crop.Fertility != 0 && crop.Fertility != null);

        if (!cropsWithFertility.Any())
        {
            return 0; // or throw exception, return NaN, etc.
        }

        double totalFertility = (double)cropsWithFertility.Sum(crop => crop.Fertility);
        double averageFertility = totalFertility / cropsWithFertility.Count();

        return ((float)averageFertility);
    }

    private ResultType GetResultType(float lowerValue, float upperValue)
    {
        var averageValue = (lowerValue + upperValue) / 2;

        if (averageValue < 50)
        {
            return ResultType.Poor;
        }

        if (averageValue < 80)
        {
            return ResultType.Medium;
        }

        return ResultType.High;
    }
}
