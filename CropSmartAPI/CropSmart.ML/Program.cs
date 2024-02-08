// Load model and predict the next set values.
// The number of values predicted is equal to the horizon specified while training.
using CropSmart_ML;


var result = CropPredictModel.Predict();

Console.WriteLine(result.Fertility_LB[0] + ", " + result.Fertility_UB[0]);

