using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;
using Newtonsoft.Json;
using MathNet.Numerics.LinearAlgebra;

public class JsonDataReciever : MonoBehaviour
{
    [SerializeField] private string _modelJsonName;
    [SerializeField] private string _spaceJsonName;
    [SerializeField] private bool _isSimpleJson;

    private string _exportFileName = "offsets.json";
    private List<Matrix<double>> _modelMatrix;
    private List<Matrix<double>> _spaceMatrix;

    public List<Matrix<double>> GetModelJsonData()
    {
        if (_isSimpleJson) return _modelMatrix ?? ConvertJsonDataToMatrixList((JSONArray)GetJsonNode(_modelJsonName));
        else return _modelMatrix ?? GetJsonData(_modelJsonName);
    }

    public List<Matrix<double>> GetSpaceJsonData()
    {
        if (_isSimpleJson) return _spaceMatrix ?? ConvertJsonDataToMatrixList((JSONArray)GetJsonNode(_spaceJsonName));
        else return _spaceMatrix ?? GetJsonData(_spaceJsonName);
    }

    private string GetJsonString(string jsonName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, jsonName);
        if (!File.Exists(path))
        {
            Debug.LogError($"Нет файла по указанному пути: {path}");
            return null;
        }
        string jsonString = File.ReadAllText(path);
        return jsonString;
    }

    private JSONNode GetJsonNode(string jsonName) => JSONNode.Parse(GetJsonString(jsonName));

    private MatrixDataModel GetJsonData(string jsonName) => JsonConvert.DeserializeObject<MatrixDataModel>(GetJsonString(jsonName), new MatrixConverter());

    private List<Matrix<double>> ConvertJsonDataToMatrixList(JSONArray node)
    {
        List<Matrix<double>> matrixList = new List<Matrix<double>>();
        foreach (JSONNode matrixData in node)
        {
            var matrix = Matrix<double>.Build.Dense(4, 4);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix[j, i] = matrixData[i * 4 + j].AsDouble;
                }
            }
            matrixList.Add(matrix);
        }
        return matrixList;
    }


    public void WriteResultToFile(List<Matrix<double>> result)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, _exportFileName);
        string jsonString = JsonConvert.SerializeObject(result, Formatting.Indented, new MatrixConverter());
        File.WriteAllText(filePath, jsonString);
        Debug.Log($"Файл {filePath} создан!");
    }
}
