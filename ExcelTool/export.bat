@echo off
REM 一键导表：扫描 TestExcels，输出到 TargetFonder/Metas 与 TargetFonder/Codes
REM 双击运行，或在 cmd 中执行本脚本

chcp 65001 >nul
cd /d "%~dp0"

echo ========================================
echo   ExcelTool 导表
echo ========================================
echo.

dotnet run --project "ExcelTool\ExcelTool.csproj" -c Debug -- %*

if %ERRORLEVEL% neq 0 (
    echo.
    echo [失败] 导出未成功，错误码: %ERRORLEVEL%
    pause
    exit /b %ERRORLEVEL%
)

echo.
echo [完成] 按任意键关闭...
pause >nul
