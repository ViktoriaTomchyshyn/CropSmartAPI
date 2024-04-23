using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.PredictObjects;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.DAL.Context;
using CropSmartAPI.DAL.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services;

public class NextCropDefinitionService: INextCropDefinitionService
{
    private readonly DataContext _dbContext;
    private readonly ILogger _logger;

    public NextCropDefinitionService(DataContext dataContext, ILogger<FertilizerService> logger)
    {
        _dbContext = dataContext;
        _logger = logger;
    }

    public Task<CropRotationResult> AnalyzeCropRotation(int fieldId)
    {
        try
        {
            var crops = _dbContext.Crops.Select(c => c.FieldId == fieldId).ToList();
            if (!crops.Any())
            {
                return Task.FromResult(new CropRotationResult
                {
                    RecommendedCrops = new List<string> { "Горох", "Квасоля", "Люцерна", "Сочевиця", "Люпин", "Конюшина", "Соя", "Картопля", "Гречка" },
                    AllowedCrops = new List<string> { "Пшениця", "Овес", "Ячмінь", "Жито", "Просо", "Ріпак", "Гірчиця", "Люпин" },
                    AvoidedCrops = new List<string> { "Соняшник" }
                });
            }

            var sivozminaData = LoadSivozminaDataFromFile("D:\\універ\\4 курс\\diploma\\CropSmartAPI\\CropSmartAPI\\sivozmina.json");
            if (sivozminaData == null)
            {
                throw new Exception("File sivozmina.json didn`t load correctly");
            }

            // var predecessors = new List<string>();

            //foreach (var crop in crops)
            //{
            //   predecessors.Add(crop.)
            //}
            return Task.FromResult(new CropRotationResult
            {
                RecommendedCrops = new List<string> { "Горох", "Квасоля", "Люцерна", "Сочевиця", "Люпин", "Конюшина", "Соя", "Картопля", "Гречка" },
                AllowedCrops = new List<string> { "Пшениця", "Овес", "Ячмінь", "Жито", "Просо", "Ріпак", "Гірчиця", "Люпин" },
                AvoidedCrops = new List<string> { "Соняшник" }
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while analyzing crop rotation.");
            return Task.FromResult(new CropRotationResult());
        }
    }


    private List<CultureRateDto> LoadSivozminaDataFromFile(string filePath)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string json =  reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<CultureRateDto>>(json);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while reading the sivozmina data file.");
            return null;
        }
    }
}
