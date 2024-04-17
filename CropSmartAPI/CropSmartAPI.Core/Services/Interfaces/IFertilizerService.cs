using CropSmartAPI.Core.Dto;
using CropSmartAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services.Interfaces;

public interface IFertilizerService
{
    Task<int> Create(FertilizerDto obj);
    Task<Fertilizer> Delete(int id);
    Task<Fertilizer> Get(int id);
    Task<List<Fertilizer>> Fertilizers(int userId, int cropId, string searchQuery);
    Task<int> Update(int id, FertilizerDto newObj);
}
