using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.PredictObjects;

public class CropRotationResult
{
    public List<string> RecommendedCrops { get; set; } = new List<string>();

    public List<string> AllowedCrops { get; set; } = new List<string>();

    public List<string> AvoidedCrops { get; set; } = new List<string>();
}
