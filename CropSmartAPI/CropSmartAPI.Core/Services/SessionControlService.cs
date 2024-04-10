using CropSmartAPI.Core.Dto;
using CropSmartAPI.Core.Services.Interfaces;
using CropSmartAPI.Core.SessionObjects;
using CropSmartAPI.DAL.Context;
using CropSmartAPI.DAL.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services;

public class SessionControlService : ISessionControlService
{
    private readonly DataContext _dbContext;
    private readonly ILogger _logger;
    private readonly ISessionList _memoryStore;
    public SessionControlService(ISessionList memoryStore, DataContext dataContext, ILogger<FertilizerService> logger)
    {
        _memoryStore = memoryStore;
        _dbContext = dataContext;
        _logger = logger;
    }

    public Task<bool> IsLoggedIn(string key)
    {
        bool isExisting = ifSessionExist(key).Result;
        if (!isExisting)
        {
            throw new Exception("Access denied");
        }
        UpdateLastOperationTime(key);
        return Task.FromResult(isExisting);
    }

    public Task<string> LogIn(string login, string password)
    {
        CheckIfValidData(login, password);
        DeleteOldSessionIfExists(login);
        //var role = _cacheRepository.GetRole(login); ??
        var key = GenerateKey(login, password);
        CreateNewSession(login, key);
        return Task.FromResult(key);
    }

    public Task<bool> LogOut(string key)
    {
        var result = RemoveSession(key);
        return result;
    }








    private Task<bool> CheckIfValidData(string login, string password)
    {
        User thatUser = null;
        foreach (var user in _dbContext.Users)
        {
            if (user.Email == login)
            {
                thatUser = user;
            }

        }

        if (thatUser == null)
        {
            throw new Exception("There is no user with this login");
        }
        else
        {
            if (thatUser.Password == password)
            {
                return Task.FromResult(true);
            }
            else
            {
                throw new Exception("Password is incorrect");
            }
        }
    }

    private Task<bool> DeleteOldSessionIfExists(string login)
    {
        var index = _memoryStore.Sessions.FindIndex(s => s.Email == login);
        if (index == -1) return Task.FromResult(false);
        else
        {
            _memoryStore.Sessions.RemoveAt(index);
            return Task.FromResult(true);
        }
    }

    private Task<int> CreateNewSession(string login, string key)
    {
        var sessionInfo = new SessionInfo() { Key = key, Email = login, LastOperationTime = DateTime.Now };
        var newSession = SessionIDNext(sessionInfo);
        _memoryStore.Sessions.Add(newSession.Result);
        return Task.FromResult(newSession.Result.SessionId);
    }

    private Task<bool> ifSessionExist(string key)
    {
        var session = _memoryStore.Sessions.FirstOrDefault<SessionInfo>(s => s.Key == key);
        if (session != null) return Task.FromResult(true);
        return Task.FromResult(false);
    }

    //returns true if session exists and time was updated, returns false if session does`nt exist
    private Task<bool> UpdateLastOperationTime(string key)
    {
        var session = _memoryStore.Sessions.FirstOrDefault<SessionInfo>(s => s.Key == key);
        if (session != null)
        {
            session.LastOperationTime = DateTime.Now;
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    private Task<bool> RemoveSession(string key)
    {
        var sessionToRemove = _memoryStore.Sessions.FirstOrDefault(session => session.Key == key);

        if (sessionToRemove != null)
        {
            _memoryStore.Sessions.Remove(sessionToRemove);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    private Task<SessionInfo> SessionIDNext(SessionInfo session)
    {
        SessionInfo result = session;

        int nextId;
        if (_memoryStore.Sessions.Count() == 0)
        {
            nextId = 1;
        }
        else
        {
            nextId = _memoryStore.Sessions.Last().SessionId + 1;
        }
        result.SessionId = nextId;
        return Task.FromResult(result);
    }

    private string GenerateKey(string login, string password)
    {
        long timeStamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        var input = login + password + timeStamp;

        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input + "dhfklsidjhd478343");
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
