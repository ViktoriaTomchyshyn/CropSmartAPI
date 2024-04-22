using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.PredictObjects;

public class CropRotationResult
{
    public List<string> RecommendedCrops { get; set; }

    public List<string> AllowedCrops { get; set; }

    public List<string> AvoidedCrops { get; set; }
}
