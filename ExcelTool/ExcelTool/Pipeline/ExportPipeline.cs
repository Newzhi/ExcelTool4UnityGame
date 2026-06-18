using ExcelTool.CodeGen;
using ExcelTool.Config;
using ExcelTool.Excel;
using ExcelTool.Export;
using ExcelTool.Models;
using ExcelTool.Validate;

namespace ExcelTool.Pipeline;

/// <summary>
/// 串联完整导表流程：发现表 → 读取 → 解析 → 校验 → 生成代码 → 导出二进制。
/// </summary>
public sealed class ExportPipeline
{
    public ExportResult Run(ExportConfig config)
    {
        var errors = new List<string>();

        List<string> tableNames;
        try
        {
            tableNames = TableDiscovery.Discover(config);
        }
        catch (Exception ex)
        {
            return ExportResult.Fail([ex.Message]);
        }

        Console.WriteLine($"发现 {tableNames.Count} 张表: {string.Join(", ", tableNames)}");

        foreach (var tableName in tableNames)
        {
            try
            {
                ExportSingleTable(config, tableName);
                Console.WriteLine($"[OK] {tableName}");
            }
            catch (Exception ex)
            {
                errors.Add($"[{tableName}] {ex.Message}");
            }
        }

        return errors.Count == 0 ? ExportResult.Ok() : ExportResult.Fail(errors);
    }

    private static void ExportSingleTable(ExportConfig config, string tableName)
    {
        var excelPath = Path.Combine(config.ExcelRoot, $"{tableName}.xlsx");

        // 1. 读取原始行
        var rawRows = ExcelReader.ReadRows(excelPath);

        // 2. 解析元数据与数据
        var table = SchemaParser.Parse(rawRows, tableName);

        // 3. 校验并转换为强类型
        var errors = TableValidator.ValidateAndConvert(table);
        if (errors.Count > 0)
            throw new InvalidOperationException(string.Join(Environment.NewLine, errors));

        // 4. 生成 C# 代码 → TargetFonder/Metas/{Name}Meta.cs
        MetaCodeGenerator.Generate(table, config.CodeOutput, config.Namespace);

        // 5. 导出二进制 → TargetFonder/Codes/{Name}.bytes
        var bytesPath = Path.Combine(config.DataOutput, $"{tableName}.bytes");
        BinaryExporter.Export(table, bytesPath);
    }
}
