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

namespace CropSmartAPI.Core.Services;

public class CropService : ICropService
{
    private readonly DataContext _dbContext;
    private readonly ILogger _logger;

    public CropService(DataContext dataContext, ILogger<FertilizerService> logger)
    {
        _dbContext = dataContext;
        _logger = logger;
    }
    public async Task<int> Create(CropDto obj)
    {
        var newObj = new Crop()
        {
            Name = obj.Name,
            Fertility = obj.Fertility,
            FieldId = obj.FieldId,
            HarverstDate = obj.HarverstDate,
            SowingDate = obj.SowingDate,
            Notes = obj.Notes
        };
        var result = _dbContext.Crops.Add(newObj);
        await _dbContext.CompleteAsync();
        return result.Entity.Id;
    }

    public async Task<Crop> Delete(int id)
    {
        var obj = await _dbContext.Crops.FirstOrDefaultAsync(p => p.Id == id);
        if (obj == null)
            return null;

        await _dbContext.CompleteAsync();
        return obj;
    }

    public async Task<Crop> Get(int id)
    {
        return await _dbContext.Crops.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Crop>> Crops(int userId, int fieldId, string searchQuery)
    {
        if (searchQuery is null)
        {
            searchQuery = "";
        }
        var result = await _dbContext.Crops.Where(p =>
            p.Field.Userid == userId && p.FieldId == fieldId && (
                p.Name.Contains(searchQuery) ||
                p.Notes.Contains(searchQuery) ||
                p.SowingDate.ToString().Contains(searchQuery) ||
                p.HarverstDate.ToString().Contains(searchQuery) ||
                p.Fertility.ToString().Contains(searchQuery)
                )).ToListAsync();

        return result;
    }

    public async Task<int> Update(int id, CropDto newObj)
    {
        Crop existingObj = await _dbContext.Crops.FirstOrDefaultAsync(p => p.Id == id);
        if (existingObj == null)
            throw new ArgumentException("Crop not found");

        existingObj.Name = newObj.Name;
        existingObj.Fertility = newObj.Fertility;
        existingObj.SowingDate = newObj.SowingDate;
        existingObj.HarverstDate = newObj.HarverstDate;
        existingObj.Notes = newObj.Notes;
        existingObj.FieldId = newObj.FieldId;

        await _dbContext.CompleteAsync();
        return existingObj.Id;
    }
}
