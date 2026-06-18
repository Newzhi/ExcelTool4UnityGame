@echo off
REM 生成 TestExcels/Hero.xlsx 样例表（首次使用或需要重置样例时运行）

chcp 65001 >nul
cd /d "%~dp0"

echo ========================================
echo   ExcelTool 创建样例表
echo ========================================
echo.

dotnet run --project "ExcelTool\ExcelTool.csproj" -c Debug -- --create-sample

if %ERRORLEVEL% neq 0 (
    echo.
    echo [失败] 创建样例失败，错误码: %ERRORLEVEL%
    pause
    exit /b %ERRORLEVEL%
)

echo.
echo [完成] 按任意键关闭...
pause >nul
