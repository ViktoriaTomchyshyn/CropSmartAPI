using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.SessionObjects;

public interface ISessionList
{
    List<SessionInfo> Sessions { get; set; }
}
