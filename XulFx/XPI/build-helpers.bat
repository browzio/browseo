@echo off
SET BASEPATH=%~dp0

echo Generating JS-helpers classes...
XpidlImp.exe -L JS -o "%BASEPATH%Content/components/helpers/generated" -n generated -M "%BASEPATH%Content/components/helpers/auto.manifest" "%BASEPATH%idl/helpers/auto"
echo.

exit /b 0
