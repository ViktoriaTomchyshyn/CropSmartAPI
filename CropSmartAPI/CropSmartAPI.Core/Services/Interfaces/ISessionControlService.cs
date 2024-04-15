using CropSmartAPI.Core.SessionObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services.Interfaces;

public interface ISessionControlService
{
    Task<LoginResult> LogIn(string login, string password);

    Task<bool> LogOut(string key);

    Task<bool> IsLoggedIn(string key);
}
