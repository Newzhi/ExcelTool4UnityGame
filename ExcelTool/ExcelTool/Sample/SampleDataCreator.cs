using MiniExcelLibs;

namespace ExcelTool.Sample;

/// <summary>
/// 创建 MVP 测试用的 Hero.xlsx 样例表（与策划约定的 ## 行格式一致）。
/// 运行方式：dotnet run -- --create-sample
/// </summary>
public static class SampleDataCreator
{
    public static void Create()
    {
        // 与 export-config.json 同级目录下的 ../TestExcels
        var excelToolDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        var testExcelsDir = Path.GetFullPath(Path.Combine(excelToolDir, "..", "TestExcels"));

        Directory.CreateDirectory(testExcelsDir);
        var outputPath = Path.Combine(testExcelsDir, "Hero.xlsx");

        // 每行用 A/B/C/D 列键，与 ExcelReader 读取方式一致
        var rows = new List<Dictionary<string, object>>
        {
            new() { ["A"] = "##var",   ["B"] = "id",     ["C"] = "name",  ["D"] = "hp" },
            new() { ["A"] = "##type",  ["B"] = "uint",   ["C"] = "string", ["D"] = "float" },
            new() { ["A"] = "##Alias", ["B"] = "ID",     ["C"] = "名字",  ["D"] = "生命值" },
            new() { ["A"] = "##desc",  ["B"] = "唯一主键", ["C"] = "名字",  ["D"] = "" },
            new() { ["A"] = "",        ["B"] = "10000",  ["C"] = "nick",  ["D"] = "100" },
            new() { ["A"] = "",        ["B"] = "10001",  ["C"] = "leo",   ["D"] = "150" },
            new() { ["A"] = "",        ["B"] = "10002",  ["C"] = "ally",  ["D"] = "80" },
            new() { ["A"] = "",        ["B"] = "10003",  ["C"] = "faker", ["D"] = "100" },
        };

        MiniExcel.SaveAs(outputPath, rows);
        Console.WriteLine($"已创建样例表: {outputPath}");
    }
}
