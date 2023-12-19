using CropSmartAPI.Core.Dto;
using CropSmartAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services.Interfaces;

public interface ICropService
{
    Task<int> Create(CropDto obj);
    Task<Crop> Delete(int id);
    Task<Crop> Get(int id);
    Task<List<Crop>> GetByField(int fieldId);
    Task<int> Update(int id, CropDto newObj);
}
