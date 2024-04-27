using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.DAL.Context;
using CropSmartAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

    public Field? Delete(int userId, int id)
    {
        var obj = _dbContext.Fields.FirstOrDefault(p => p.Id == id && p.Userid == userId);
        if (obj == null)
        {
            return null;
        }

        _dbContext.Fields.Remove(obj);
        _dbContext.SaveChanges();
        return obj;
    }

    public async Task<Field> Get(int userId, int id)
    {
        return await _dbContext.Fields.FirstOrDefaultAsync(p => p.Id == id && p.Userid == userId);
    }

    public async Task<List<Field>> Fields(int userId, string searchQuery)
    {
        if (searchQuery is null)
        {
            searchQuery = "";
        }

        var result = await _dbContext.Fields.Where(p =>
            p.Userid == userId && (
                p.Name.Contains(searchQuery) ||
                p.CadastralNumber.Contains(searchQuery))).ToListAsync();

        return result;
    }

    public async Task<int> Update(int id, FieldDto newObj)
    {
        Field existingObj = await _dbContext.Fields.FirstOrDefaultAsync(p => p.Id == id && p.Userid == newObj.Userid);
        if (existingObj == null)
            throw new ArgumentException("Field not found");

        existingObj.Name = newObj.Name;
        existingObj.CadastralNumber = newObj.CadastralNumber;
        existingObj.Area = newObj.Area;
        existingObj.SoilType = newObj.SoilType;
        existingObj.CoordinateX = newObj.CoordinateX;
        existingObj.CoordinateY = newObj.CoordinateY;
        existingObj.PropertyRight = newObj.PropertyRight;

        await _dbContext.CompleteAsync();
        return existingObj.Id;
    }
}