using CropSmartAPI.Core.Dto;
using CropSmartAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<int> Create(UserDto obj);
        Task<User> Delete(int id);
        Task<User> Get(int id);
        Task<int> Update(int id, UserDto newObj);
    }
}
