using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.SessionObjects;

public class SessionInfo
{
    public int SessionId { get; set; }
    public string Key { get; set; }

    public string Email { get; set; }

    public DateTime LastOperationTime { get; set; }
}
