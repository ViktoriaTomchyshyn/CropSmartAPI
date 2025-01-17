﻿using CropSmartAPI.Core.PredictObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropSmartAPI.Core.Services.Interfaces;

public interface IFertilityPredictionService
{
    Task<PredictResult> PredictFertility(int fieldId);
}
