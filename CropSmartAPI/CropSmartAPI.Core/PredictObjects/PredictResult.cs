using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.PredictObjects
{
    public class PredictResult
    {
        public float LowerFertilityValue { get; set; }
        public float UpperFertilityValue { get; set; }
        public float AverageFertilityValue { get; set; }
        public ResultType ResultType { get; set; }
    }
}
