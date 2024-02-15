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
       var modelInput = GetAverageFertility(crops);

       var sampleData = new CropPredictModel.ModelInput()
       {
           Fertility = modelInput
       };

       var result = CropPredictModel.Predict(sampleData);

       _logger.LogInformation(result.Fertility_LB[0] + ", " + result.Fertility_UB[0]);

        return new PredictResult();
    }

    private float GetAverageFertility(List<Crop> crops)
    {
        // Filter crops that have field fertility
        var cropsWithFertility = crops.Where(crop => crop.Fertility != 0);

        // If there are no crops with fertility, return 0 or handle accordingly
        if (!cropsWithFertility.Any())
        {
            return 0; // or throw exception, return NaN, etc.
        }

        // Calculate average fertility
        double totalFertility = cropsWithFertility.Sum(crop => crop.Fertility);
        double averageFertility = totalFertility / cropsWithFertility.Count();

        return ((float)averageFertility);
    }
}
