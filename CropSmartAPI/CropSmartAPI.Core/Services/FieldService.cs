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

public class FieldService : IFieldService
{
    private readonly DataContext _dbContext;
    private readonly ILogger _logger;

    public FieldService(DataContext dataContext, ILogger<FertilizerService> logger)
    {
        _dbContext = dataContext;
        _logger = logger;
    }
    public async Task<int> Create(FieldDto obj)
    {
        var newObj = new Field()
        {
            Name = obj.Name,
            CadastralNumber = obj.CadastralNumber,
            Area = obj.Area,
            PropertyRight = obj.PropertyRight,
            SoilType = obj.SoilType,
            CoordinateX = obj.CoordinateX,
            CoordinateY = obj.CoordinateY,
            Userid = obj.Userid
        };
        var result = _dbContext.Fields.Add(newObj);
        await _dbContext.CompleteAsync();
        return result.Entity.Id;
    }

    public async Task<Field> Delete(int id)
    {
        var obj = await _dbContext.Fields.FirstOrDefaultAsync(p => p.Id == id);
        if (obj == null)
            return null;

        await _dbContext.CompleteAsync();
        return obj;
    }

    public async Task<Field> Get(int id)
    {
        return await _dbContext.Fields.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Field>> GetByUser(int userId)
    {
        return await _dbContext.Fields.Where(p => p.Userid == userId).ToListAsync();
    }

    public async Task<int> Update(int id, FieldDto newObj)
    {
        Field existingObj = await _dbContext.Fields.FirstOrDefaultAsync(p => p.Id == id);
        if (existingObj == null)
            throw new ArgumentException("Field not found");

        existingObj.Name = newObj.Name;
        existingObj.CadastralNumber = newObj.CadastralNumber;
        existingObj.Area = newObj.Area;
        existingObj.Userid = newObj.Userid;
        existingObj.SoilType = newObj.SoilType;
        existingObj.CoordinateX = newObj.CoordinateX;
        existingObj.CoordinateY = newObj.CoordinateY;
        existingObj.PropertyRight = newObj.PropertyRight;

        await _dbContext.CompleteAsync();
        return existingObj.Id;
    }
}
