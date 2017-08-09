@echo off
SET BASEPATH=%~dp0

echo Generating C# interfaces...
XpidlImp.exe -L C# -o "%BASEPATH%Interfaces" -n Gecko.Interfaces -u SpiderMonkey -u Gecko.CustomMarshalers -m "%BASEPATH%../map.xml" "%BASEPATH%idl"
XpidlImp.exe -L C# -o "%BASEPATH%Interfaces/Helpers" -n Gecko.Interfaces -u SpiderMonkey -u Gecko.CustomMarshalers -m "%BASEPATH%../map.xml" "%BASEPATH%idl/helpers/auto"
echo.

exit /b 0
