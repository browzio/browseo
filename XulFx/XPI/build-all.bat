@echo off
echo Generating XPT file...
call build-xpt.bat
echo.
call build-helpers.bat
echo.
call build-interfaces.bat
echo.
echo Complete.
