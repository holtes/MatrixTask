using MathNet.Numerics.LinearAlgebra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkableObject : MonoBehaviour
{
    [SerializeField] private Color _wrongColor = Color.white;
    [SerializeField] private Color _rightColor = Color.white;

    [HideInInspector] public bool IsRight {
        get 
        {
            return _isRight;
        }
        set
        {
            MarkObject(value);
            _isRight = value;
        }
    }

    private bool _isRight = false;
    private Material _objMat;

    private void Awake() => _objMat = GetComponent<MeshRenderer>().material;

    public void SetObjectTransform(Matrix<double> transformMatrix)
    {
        transform.position = ExtractPosition(transformMatrix);
        transform.rotation = ExtractRotation(transformMatrix);
        transform.localScale = ExtractScale(transformMatrix);
    }

    public void MarkObject(bool isRight) => _objMat.color = isRight ? _rightColor : _wrongColor;

    private Vector3 ExtractPosition(Matrix<double> matrix)
    {
        return new Vector3(
            (float)matrix[0, 3],
            (float)matrix[1, 3],
            (float)matrix[2, 3]
        );
    }

    
    private Quaternion ExtractRotation(Matrix<double> matrix)
    {
        Matrix<double> rotationMatrix = matrix.SubMatrix(0, 3, 0, 3);
        return Quaternion.LookRotation(
            new Vector3((float)rotationMatrix[0, 2], (float)rotationMatrix[1, 2], (float)rotationMatrix[2, 2]),
            new Vector3((float)rotationMatrix[0, 1], (float)rotationMatrix[1, 1], (float)rotationMatrix[2, 1])
        );
    }

    
    private Vector3 ExtractScale(Matrix<double> matrix)
    {
        return new Vector3(
            (float)matrix.Column(0).L2Norm(),
            (float)matrix.Column(1).L2Norm(),
            (float)matrix.Column(2).L2Norm()
        );
    }
}
