using Newtonsoft.Json;
using System;
using MathNet.Numerics.LinearAlgebra;
using Newtonsoft.Json.Linq;

public class MatrixConverter : JsonConverter<Matrix<double>>
{
    public override void WriteJson(JsonWriter writer, Matrix<double> value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("m00"); writer.WriteValue(value[0, 0]);
        writer.WritePropertyName("m10"); writer.WriteValue(value[1, 0]);
        writer.WritePropertyName("m20"); writer.WriteValue(value[2, 0]);
        writer.WritePropertyName("m30"); writer.WriteValue(value[3, 0]);
        writer.WritePropertyName("m01"); writer.WriteValue(value[0, 1]);
        writer.WritePropertyName("m11"); writer.WriteValue(value[1, 1]);
        writer.WritePropertyName("m21"); writer.WriteValue(value[2, 1]);
        writer.WritePropertyName("m31"); writer.WriteValue(value[3, 1]);
        writer.WritePropertyName("m02"); writer.WriteValue(value[0, 2]);
        writer.WritePropertyName("m12"); writer.WriteValue(value[1, 2]);
        writer.WritePropertyName("m22"); writer.WriteValue(value[2, 2]);
        writer.WritePropertyName("m32"); writer.WriteValue(value[3, 2]);
        writer.WritePropertyName("m03"); writer.WriteValue(value[0, 3]);
        writer.WritePropertyName("m13"); writer.WriteValue(value[1, 3]);
        writer.WritePropertyName("m23"); writer.WriteValue(value[2, 3]);
        writer.WritePropertyName("m33"); writer.WriteValue(value[3, 3]);
        writer.WriteEndObject();
    }

    public override Matrix<double> ReadJson(JsonReader reader, Type objectType, Matrix<double> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var obj = JObject.Load(reader);
        var matrix = Matrix<double>.Build.Dense(4, 4);

        matrix[0, 0] = obj["m00"].ToObject<double>();
        matrix[1, 0] = obj["m10"].ToObject<double>();
        matrix[2, 0] = obj["m20"].ToObject<double>();
        matrix[3, 0] = obj["m30"].ToObject<double>();
        matrix[0, 1] = obj["m01"].ToObject<double>();
        matrix[1, 1] = obj["m11"].ToObject<double>();
        matrix[2, 1] = obj["m21"].ToObject<double>();
        matrix[3, 1] = obj["m31"].ToObject<double>();
        matrix[0, 2] = obj["m02"].ToObject<double>();
        matrix[1, 2] = obj["m12"].ToObject<double>();
        matrix[2, 2] = obj["m22"].ToObject<double>();
        matrix[3, 2] = obj["m32"].ToObject<double>();
        matrix[0, 3] = obj["m03"].ToObject<double>();
        matrix[1, 3] = obj["m13"].ToObject<double>();
        matrix[2, 3] = obj["m23"].ToObject<double>();
        matrix[3, 3] = obj["m33"].ToObject<double>();

        return matrix;
    }
}
