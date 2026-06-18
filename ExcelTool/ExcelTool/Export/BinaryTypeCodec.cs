using System.Text;
using ExcelTool.Models;

namespace ExcelTool.Export;

/// <summary>
/// 将单个字段值按约定格式写入二进制流。
/// 格式与 Unity 端读取逻辑对称，后续可在 Runtime 中实现对应 Reader。
/// </summary>
public static class BinaryTypeCodec
{
    public static void WriteValue(BinaryWriter writer, FieldType type, object value)
    {
        switch (type)
        {
            case FieldType.UInt:
                writer.Write(Convert.ToUInt32(value));
                break;

            case FieldType.Float:
                writer.Write(Convert.ToSingle(value));
                break;

            case FieldType.String:
                var text = value?.ToString() ?? "";
                var bytes = Encoding.UTF8.GetBytes(text);
                writer.Write(bytes.Length);
                writer.Write(bytes);
                break;

            default:
                throw new NotSupportedException($"不支持的类型: {type}");
        }
    }

    public static byte ToTypeId(FieldType type) =>
        type switch
        {
            FieldType.UInt => 1,
            FieldType.String => 2,
            FieldType.Float => 3,
            _ => throw new NotSupportedException($"不支持的类型: {type}"),
        };
}
