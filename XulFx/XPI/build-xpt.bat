@echo off
SET BASEPATH=%~dp0
SET XPTFILESDIR=%BASEPATH%\interfaces_temp
SET MOZSDKDIR=%BASEPATH%..\..\PutXulRunnerFolderHere\firefox-sdk
if not exist "%MOZSDKDIR%" (
	echo Firefox SDK not found!
	echo.
	exit /b 3
)

echo Create temporary directory...
if not exist "%XPTFILESDIR%" mkdir "%XPTFILESDIR%"
if exist "%XPTFILESDIR%\*.xpt" del /Q /F "%XPTFILESDIR%\*.xpt"
if exist "%BASEPATH%Content\components\interfaces.xpt" del /Q /F "%BASEPATH%Content\components\interfaces.xpt"

SETLOCAL EnableDelayedExpansion
SET XPTFILES=

for %%n in ("%BASEPATH%idl\*.idl") do (
	echo Generating %%~nn.idl...
	"%MOZSDKDIR%\sdk\bin\typelib.py" "%BASEPATH%idl\%%~nn.idl" -o "%XPTFILESDIR%\%%~nn.xpt" -I "%MOZSDKDIR%\idl"
	for /f %%i in ("%XPTFILESDIR%\%%~nn.xpt") do (
		if %%~zi == 0 (
			echo Failed to generate XPT file for %%~nn.idl.
			rmdir /Q /S "%XPTFILESDIR%"
			exit /b 13
		)
	)
	SET XPTFILES=!XPTFILES! "%XPTFILESDIR%\%%~nn.xpt"
)

for %%n in ("%BASEPATH%idl\helpers\auto\*.idl") do (
	echo Generating %%~nn.idl...
	"%MOZSDKDIR%\sdk\bin\typelib.py" "%BASEPATH%idl\helpers\auto\%%~nn.idl" -o "%XPTFILESDIR%\%%~nn.xpt" -I "%MOZSDKDIR%\idl"
	for /f %%i in ("%XPTFILESDIR%\%%~nn.xpt") do (
		if %%~zi == 0 (
			echo Failed to generate XPT file for %%~nn.idl.
			rmdir /Q /S "%XPTFILESDIR%"
			exit /b 13
		)
	)
	SET XPTFILES=!XPTFILES! "%XPTFILESDIR%\%%~nn.xpt"
)

echo Merging xpt's to "interfaces.xpt"... 
"%MOZSDKDIR%\sdk\bin\xpt.py" link "%BASEPATH%Content\components\interfaces.xpt" %XPTFILES%

echo Cleaning...
rmdir /Q /S "%XPTFILESDIR%"

ENDLOCAL

echo Complete.
exit /b 0
