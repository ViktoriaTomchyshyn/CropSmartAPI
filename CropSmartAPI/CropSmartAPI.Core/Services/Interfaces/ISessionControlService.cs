using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services.Interfaces;

public interface ISessionControlService
{
    Task<string> LogIn(string login, string password);

    Task<bool> LogOut(string key);

    Task<bool> IsLoggedIn(string key);
}
