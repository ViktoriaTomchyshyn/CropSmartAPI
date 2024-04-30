using CropSmartAPI.Core.Dto;
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
using static Plotly.NET.StyleParam.DrawingStyle;

namespace CropSmartAPI.Core.Services;

public class FertilizerService : IFertilizerService
{
    private readonly DataContext _dbContext;
    private readonly ILogger _logger;

    public FertilizerService(DataContext dataContext, ILogger<FertilizerService> logger)
    {
        _dbContext = dataContext;
        _logger = logger;
    }

    public async Task<int> Create(FertilizerDto obj)
    {
        var newObj = new Fertilizer()
        {
            Name = obj.Name,
            CropId = obj.CropId,
            Quantity = obj.Quantity
        };
        var result = _dbContext.Fertilizers.Add(newObj);
        await _dbContext.CompleteAsync();
        return result.Entity.Id;
    }

    public async Task<Fertilizer> Delete(int id)
    {
        var obj = _dbContext.Fertilizers.FirstOrDefault(p => p.Id == id);
        if (obj == null)
        {
            return null;
        }

        _dbContext.Fertilizers.Remove(obj);
        _dbContext.SaveChanges();
        return obj;
    }

    public async Task<List<Fertilizer>> Fertilizers(int userId, int cropId, string searchQuery)
    {
        if (searchQuery is null)
        {
            searchQuery = "";
        }
        var result = await _dbContext.Fertilizers.Where(p =>
            p.Crop.Field.Userid == userId && p.CropId == cropId && (
                p.Name.Contains(searchQuery) ||
                p.Quantity.ToString().Contains(searchQuery)
                )).ToListAsync();

        return result;
    }

    public async Task<Fertilizer> Get(int id)
    {
        return await _dbContext.Fertilizers.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> Update(int id, FertilizerDto newObj)
    {
        Fertilizer existingObj = await _dbContext.Fertilizers.FirstOrDefaultAsync(p => p.Id == id);
        if (existingObj == null)
            throw new ArgumentException("Fertilizer not found");

        existingObj.Name = newObj.Name;
        existingObj.Quantity = newObj.Quantity;

        await _dbContext.CompleteAsync();
        return existingObj.Id;
    }
}
