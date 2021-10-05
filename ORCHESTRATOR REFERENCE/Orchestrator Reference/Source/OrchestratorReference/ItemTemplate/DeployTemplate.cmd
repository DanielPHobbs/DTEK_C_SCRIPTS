Echo off
"%VS100COMNTOOLS%..\..\..\Microsoft SDKs\Windows\v7.0A\bin\NETFX 4.0 Tools\gacutil.exe" /i C60.OrchestratorReference.dll 
IF NOT EXIST "%USERPROFILE%\Documents" GOTO xp
copy *.zip "%USERPROFILE%\Documents\Visual Studio 2010\Templates\ItemTemplates\Visual C#\"
GOTO end
:xp
copy *.zip "%USERPROFILE%\My Documents\Visual Studio 2010\Templates\ItemTemplates\Visual C#\"
:end
Echo Done.
pause