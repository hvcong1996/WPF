@ECHO OFF
Rem File Execute Path
SET BUILD_DEBUGPATH=%CMDROOT%..\bin\Debug

Rem Media file path
SET MEDIAPATH=%CMDROOT%Media
IF EXIST "%MEDIAPATH%" RMDIR /S /Q  "%MEDIAPATH%"
IF NOT EXIST "%MEDIAPATH%" MD "%MEDIAPATH%"

Rem Copy data to Media folder
Rem Build Debug
COPY /Y "%BUILD_DEBUGPATH%\Common.dll" "%MEDIAPATH%"
COPY /Y "%BUILD_DEBUGPATH%\Service.exe" "%MEDIAPATH%"
COPY /Y "%BUILD_DEBUGPATH%\Uninstall.exe" "%MEDIAPATH%"

REM Media.zip path
SET ZIPPATH=%CMDROOT%Media.zip
REM Create Media.zip
IF EXIST "%ZIPPATH%" DEL "%ZIPPATH%"
PowerShell -Command Compress-Archive -Path \"%MEDIAPATH%\" -DestinationPath \"%ZIPPATH%\";

PAUSE
GOTO :EOF