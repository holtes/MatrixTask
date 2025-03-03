using Newtonsoft.Json;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

[JsonArray]
public class MatrixDataModel : List<Matrix<double>> 
{
}