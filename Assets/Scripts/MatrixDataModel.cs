using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

[JsonArray]
public class MatrixDataModel : List<Matrix<double>> 
{
}