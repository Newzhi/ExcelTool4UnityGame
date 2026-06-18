namespace ExcelTool.Config;

/// <summary>
/// 导表工具配置，对应 export-config.json。
/// 路径字段在加载后会解析为绝对路径（相对于配置文件所在目录）。
/// </summary>
public sealed class ExportConfig
{
    /// <summary>Excel 源表目录，如 ../TestExcels。</summary>
    public string ExcelRoot { get; set; } = "";

    /// <summary>生成的 C# Meta 类输出目录，如 ../TargetFonder/Metas。</summary>
    public string CodeOutput { get; set; } = "";

    /// <summary>生成的二进制输出目录，如 ../TargetFonder/Codes。</summary>
    public string DataOutput { get; set; } = "";

    /// <summary>生成代码的命名空间。</summary>
    public string Namespace { get; set; } = "Game.Config";

    /// <summary>
    /// 表发现模式：scanAll = 自动扫描 excelRoot 下所有 xlsx；
    /// explicit = 使用 tables 列表（调试用）。
    /// </summary>
    public string TableDiscovery { get; set; } = "scanAll";

    /// <summary>explicit 模式下手动指定的表名列表。</summary>
    public List<string>? Tables { get; set; }

    /// <summary>scanAll 模式下要排除的表名，如 _Template。</summary>
    public List<string>? ExcludeTables { get; set; }

    /// <summary>非空时只处理这些表（局部导表）。</summary>
    public List<string>? IncludeTables { get; set; }

    /// <summary>配置文件所在目录，用于解析相对路径。</summary>
    public string ConfigDirectory { get; set; } = "";
}
