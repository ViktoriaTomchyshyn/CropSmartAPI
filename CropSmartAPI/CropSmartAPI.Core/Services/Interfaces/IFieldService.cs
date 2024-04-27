using CropSmartAPI.Core.Dto;
using CropSmartAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services.Interfaces;

public interface IFieldService
{
    Task<int> Create(FieldDto obj);
    public Field? Delete(int userId, int id);
    Task<Field> Get(int userId, int id);
    Task<List<Field>> Fields(int userId, string searchQuery);
    Task<int> Update(int id, FieldDto newObj);
}
