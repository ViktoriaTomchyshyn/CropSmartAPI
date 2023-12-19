﻿using CropSmartAPI.Core.Dto;
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

namespace CropSmartAPI.Core.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dbContext;
        private readonly ILogger _logger;

        public UserService(DataContext dataContext, ILogger<UserService> logger)
        {
            _dbContext = dataContext;
            _logger = logger;
        }

        public async Task<int> Create(UserDto obj)
        {
            var newObj = new User()
            {
                Name = obj.Name,
                Surname = obj.Surname,
                Email = obj.Email,
                Password = obj.Password
            };
            var result = _dbContext.Users.Add(newObj);
            await _dbContext.CompleteAsync();
            return result.Entity.Id;
        }

        public async Task<User> Delete(int id)
        {
            var obj = await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (obj == null)
                return null;

            await _dbContext.CompleteAsync();
            return obj;
        }

        public async Task<User> Get(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> Update(int id, UserDto newObj)
        {
            User existingObj = await _dbContext.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (existingObj == null)
                throw new ArgumentException("User not found");

            existingObj.Name = newObj.Name;
            existingObj.Surname = newObj.Surname;
            existingObj.Email = newObj.Email;
            existingObj.Password = newObj.Password;

            await _dbContext.CompleteAsync();
            return existingObj.Id;
        }
    }
}