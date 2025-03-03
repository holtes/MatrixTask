using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public static class MatrixOffsetFinder
{
    public static Dictionary<string, IEnumerable<Matrix<double>>> FindMatrixOffsets(List<Matrix<double>> model, List<Matrix<double>> space)
    {
        var spaceSet = new HashSet<Matrix<double>>(space);
        var offsetsList = new HashSet<Matrix<double>>();
        var rightOffsetsList = new HashSet<Matrix<double>>();

        var modelInverses = model.Select(m => m.Inverse()).ToList();

        foreach (var spaceMatrix in space)
        {
            foreach (var modelInverse in modelInverses)
            {
                var offset = spaceMatrix * modelInverse;
                offsetsList.Add(offset);
                if (model.All(m => spaceSet.Contains(offset * m)))
                {
                    rightOffsetsList.Add(offset);
                }
            }
        }

        Debug.Log($"Общее количество смещений: {offsetsList.Count}");
        Debug.Log($"Количество смещений, удовлетворяющих условию: {rightOffsetsList.Count}");

        return new Dictionary<string, IEnumerable<Matrix<double>>>
        {
            { "All", offsetsList },
            { "Right", rightOffsetsList }
        };
    }
}
