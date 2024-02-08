﻿// This file was auto-generated by ML.NET Model Builder.

using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.ML.Transforms.TimeSeries;

namespace CropSmart_ML
{
    public partial class CropPredictModel
    {
        /// <summary>
        /// model input class for CropPredictModel.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [LoadColumn(4)]
            [ColumnName(@"Fertility")]
            public float Fertility { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for CropPredictModel.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"Fertility")]
            public float[] Fertility { get; set; }

            [ColumnName(@"Fertility_LB")]
            public float[] Fertility_LB { get; set; }

            [ColumnName(@"Fertility_UB")]
            public float[] Fertility_UB { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath(@"D:\універ\4 курс\diploma\CropSmartAPI\CropSmartAPI\CropSmart.ML\CropPredictModel.mlnet");

        public static readonly Lazy<TimeSeriesPredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<TimeSeriesPredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput? input = null, int? horizon = null)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input, horizon);
        }

        private static TimeSeriesPredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var schema);
            return mlModel.CreateTimeSeriesEngine<ModelInput, ModelOutput>(mlContext);
        }
    }
}

