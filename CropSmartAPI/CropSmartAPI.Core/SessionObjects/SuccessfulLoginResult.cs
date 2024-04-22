using CropSmartAPI.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.SessionObjects;

public class SuccessfulLoginResult
{
    public string Key { get; set; }
    public UserDto UserInfo { get; set; }
}
