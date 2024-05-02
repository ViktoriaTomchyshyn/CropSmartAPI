using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.PredictObjects;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.DAL.Context;
using CropSmartAPI.DAL.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Plotly.NET.TraceObjects;
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
    private List<CultureRateDto> _cultureRates;

    public NextCropDefinitionService(DataContext dataContext, ILogger<FertilizerService> logger)
    {
        _dbContext = dataContext;
        _logger = logger;
        _cultureRates = LoadSivozminaDataFromFile("D:\\універ\\4 курс\\diploma\\CropSmartAPI\\CropSmartAPI\\sivozmina.json");
        if (_cultureRates == null)
        {
            throw new Exception("File sivozmina.json didn`t load correctly");
        }
    }

    public Task<CropRotationResult> AnalyzeCropRotation(int fieldId)
    {
        try
        {
            var predecessors = GetPredecessors(fieldId);
            if (!predecessors.Any())
            {
                return Task.FromResult(GetDefaultResult());
            }

            var rateMatrix = new List<List<RateItem>>();

            for (int i = 0; i < predecessors.Count; i++)
            {
                rateMatrix.Add(new List<RateItem>());

                foreach(var rateItem in _cultureRates)
                {
                    if (rateItem.Predecessor.ToLower().Contains(predecessors[i].ToLower()))
                    {
                        rateMatrix[i].Add(new RateItem() { Culture = rateItem.Culture, Rate = rateItem.Rate });
                    }
                }
            }

            var result = NormalizeMatrix(rateMatrix, predecessors);

            return Task.FromResult(result);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while analyzing crop rotation.");
            return Task.FromResult(new CropRotationResult());
        }
    }

    private CropRotationResult NormalizeMatrix(List<List<RateItem>> matrix, List<string> predecessors)
    {
        try
        {
            var normalisedRate = new List<RateItem>();
            var coefs = new List<double>()
            {
                1.0, 0.75, 0.5, 0.25
            };

            for (int i = 0; i < matrix.Count(); i++)
            {
                foreach (var item in matrix[i])
                {
                    var writtenItems = normalisedRate.Where(c => c.Culture.Contains(item.Culture));

                    if (!writtenItems.Any())
                    {
                        normalisedRate.Add(new RateItem() { Culture = item.Culture, Rate = item.Rate * coefs[i]});
                    }
                    else
                    {
                        normalisedRate.FirstOrDefault(c => c.Culture.Contains(item.Culture)).Rate += item.Rate * coefs[i];
                    }
                }
            }

            var summaryResult = new CropRotationResult();

            foreach (var item in normalisedRate)
            {
                item.Rate = (item.Rate / 7.5) * 3;

                //ті ж культури не рекомендовані
                foreach (var pred in predecessors)
                {
                    if (item.Culture.Contains(pred))
                    {
                        item.Rate = 0;
                    }
                }

                //ті ж сімейства рослин, що останнього року. не рекомендовані
                var family = GetSameFamilyCultures(item.Culture);
                foreach (var member in family)
                {
                    if (member.Contains(predecessors[0]))
                    {
                        item.Rate = 0;
                    }
                }


            }

            normalisedRate.OrderByDescending(r => r.Rate);

            foreach (var item in normalisedRate) {

                if (item.Rate < 1)
                {
                    summaryResult.AvoidedCrops.Add(item.Culture);
                } 
                else if(item.Rate < 1.7)
                {
                    summaryResult.AllowedCrops.Add(item.Culture);
                }
                else
                {
                    summaryResult.RecommendedCrops.Add(item.Culture);
                }
            }

            return summaryResult;

        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error while normalizing matrix");
            return new CropRotationResult();
        }
    }

    private List<string> GetSameFamilyCultures(string culture)
    {
        var families = new Dictionary<string, List<string>>()
        {
            { "Злаки", new List<string> { "Пшениця", "Кукурудза", "Ячмінь", "Рис", "Овес", "Ріжок", "Просо"} },
            { "Бобові", new List<string> { "Соя", "Горох", "Фасоля", "Горох", "Льон", "Квасоля", "Фасоля", "Нут" } },
            { "Круп'яні", new List<string> { "Пшениця", "Ячмінь", "Гречка", "Овес", "Рис", "Ріжок" } },
            { "Стрункворцеві", new List<string> { "Соняшник", "Сафлор", "Маслина соняшникова", "Сафлоролійка", "Тундук" } },
            { "Трав'яні", new List<string> { "Трава", "Люцерна", "Клевер", "Тимофеївка", "Суданська трава" } },
            { "Овочі", new List<string> { "Капуста", "Броколі", "Цвітна капуста", "Кольрабі", "Ріпак", "Цибуля", "Часник", "Порей", "Цибуля", "Редис", "Редька", "Картопля" } },
            { "Плодові", new List<string> { "Яблука", "Груші", "Вишні", "Сливи", "Абрикоси", "Виноград", "Смородина", "Кизил", "Шипшина" } }
        };


        foreach (var family in families)
        {
            foreach(var item in family.Value)
            {
                if (item.Contains(culture))
                {
                    return family.Value;
                }
            }
        }

        return new List<string>();
    }


    private List<string> GetPredecessors(int fieldId)
    {
        var predecessors = new List<string>();
        predecessors = _dbContext.Crops.Where(crop => crop.FieldId == fieldId).OrderByDescending(crop => crop.SowingDate).Select(crop => crop.Name).ToList();
        if (predecessors.Count > 4)
        {
            predecessors.RemoveRange(4, predecessors.Count - 4); 
        }

        return predecessors;
    }
    private CropRotationResult GetDefaultResult()
    {
        return new CropRotationResult
        {
            RecommendedCrops = new List<string> { "Горох", "Квасоля", "Люцерна", "Сочевиця", "Люпин", "Конюшина", "Соя", "Картопля", "Гречка" },
            AllowedCrops = new List<string> { "Пшениця", "Овес", "Ячмінь", "Жито", "Просо", "Ріпак", "Гірчиця", "Люпин" },
            AvoidedCrops = new List<string> { "Соняшник" }
        };
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
