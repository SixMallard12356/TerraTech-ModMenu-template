# Build script for TerraTech ModMenu
Set-Location "C:\Programming\TerraTechModMenu"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Building TerraTech ModMenu" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Build Bootstrap.dll
Write-Host "[1/3] Building Bootstrap.dll..." -ForegroundColor Yellow
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "Injector\Bootstrap.vcxproj" /p:Configuration=Release /p:Platform=x64 /t:Build
if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] Bootstrap.dll build failed!" -ForegroundColor Red
    exit 1
}
Write-Host "[SUCCESS] Bootstrap.dll built successfully" -ForegroundColor Green
Write-Host ""

# Build Injector.exe
Write-Host "[2/3] Building Injector.exe..." -ForegroundColor Yellow
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "Injector\Injector.vcxproj" /p:Configuration=Release /p:Platform=x64 /t:Build
if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] Injector.exe build failed!" -ForegroundColor Red
    exit 1
}
Write-Host "[SUCCESS] Injector.exe built successfully" -ForegroundColor Green
Write-Host ""

# Build ModMenu.dll
Write-Host "[3/3] Building ModMenu.dll..." -ForegroundColor Yellow
& "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" "ModMenu\ModMenu.csproj" /p:Configuration=Release /t:Build
if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERROR] ModMenu.dll build failed!" -ForegroundColor Red
    exit 1
}
Write-Host "[SUCCESS] ModMenu.dll built successfully" -ForegroundColor Green
Write-Host ""

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Build completed successfully!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
