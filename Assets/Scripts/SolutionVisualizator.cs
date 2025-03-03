using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

public class SolutionVisualizator : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObj;
    [SerializeField] private Transform _spawnOrigin;
    [SerializeField] private float _spawnRate = 100;

    private JsonDataReciever _jsonDataReciever;
    private List<MarkableObject> _objectsList = new List<MarkableObject>();
    private void Awake()
    {
        _jsonDataReciever = GetComponent<JsonDataReciever>();
        var solutionDict = MatrixOffsetFinder.FindMatrixOffsets(_jsonDataReciever.GetModelJsonData(), _jsonDataReciever.GetSpaceJsonData());
        _jsonDataReciever.WriteResultToFile(solutionDict["Right"].ToList());
        StartCoroutine(SpawnCoroutine(solutionDict["All"], solutionDict["Right"]));
    }

    private IEnumerator SpawnCoroutine(IEnumerable<Matrix<double>> offsetMatrix, IEnumerable<Matrix<double>> rightOffsetMatrix)
    {
        foreach (var transformMatrix in offsetMatrix)
        {
            SpawnObject(transformMatrix, rightOffsetMatrix.Contains(transformMatrix));
            yield return new WaitForSeconds(1f / _spawnRate);
        }
    }

    private void SpawnObject(Matrix<double> transformMatrix, bool isRight)
    {
        
        var spawnedObj = Instantiate(_spawnObj, _spawnOrigin);
        var markableObj = spawnedObj.GetComponent<MarkableObject>();
        markableObj.SetObjectTransform(transformMatrix);
        markableObj.IsRight = isRight;
        _objectsList.Add(markableObj);
    }
}
