using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.SessionObjects;

public class SessionList: ISessionList
{
    public List<SessionInfo> Sessions { get; set; } = new List<SessionInfo>();
}