@echo off
SET BASEPATH=%~dp0
SET XPTFILESDIR=%BASEPATH%\interfaces_xpt
SET MOZSDKDIR=%BASEPATH%..\..\PutXulRunnerFolderHere\firefox-sdk
if not exist "%MOZSDKDIR%" (
	echo Firefox SDK not found!
	echo.
	exit /b 3
)

echo Create temporary directory...
if not exist "%XPTFILESDIR%" mkdir "%XPTFILESDIR%"
if exist "C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin\browser\BFXComponents\interfaces.xpt" del /Q /F "C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin\browser\BFXComponents\interfaces.xpt"

SETLOCAL EnableDelayedExpansion
SET XPTFILES=

for %%n in ("%BASEPATH%idl\*.idl") do (
	echo Generating %%~nn.idl...
	"%MOZSDKDIR%\sdk\bin\typelib.py" "%BASEPATH%idl\%%~nn.idl" -o "%XPTFILESDIR%\%%~nn.xpt" -I "%MOZSDKDIR%\idl"

	SET XPTFILES=!XPTFILES! "%XPTFILESDIR%\%%~nn.xpt"
)

echo Merging xpt's to "interfaces.xpt"... 
"%MOZSDKDIR%\sdk\bin\xpt.py" link "C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin\browser\BFXComponents\interfaces.xpt" %XPTFILES%

echo Cleaning...
ENDLOCAL

echo Complete.
exit /b 0
