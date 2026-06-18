using ExcelTool.Models;

namespace ExcelTool.Validate;

/// <summary>
/// 将 Excel 单元格字符串转换为强类型值。
/// </summary>
public static class TypeConverter
{
    public static bool TryParse(FieldType type, string raw, out object? value, out string error)
    {
        raw = raw.Trim();
        value = null;
        error = "";

        switch (type)
        {
            case FieldType.UInt:
                if (!uint.TryParse(raw, out var u))
                {
                    error = $"无法解析为 uint: \"{raw}\"";
                    return false;
                }

                value = u;
                return true;

            case FieldType.Float:
                if (!float.TryParse(raw, out var f))
                {
                    error = $"无法解析为 float: \"{raw}\"";
                    return false;
                }

                value = f;
                return true;

            case FieldType.String:
                value = raw;
                return true;

            default:
                error = $"未知类型: {type}";
                return false;
        }
    }
}
