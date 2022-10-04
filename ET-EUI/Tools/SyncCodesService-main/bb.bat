@set WORKSPACE=D:\Study\Test\ET-EUI

@set SERVICE=dotnet %WORKSPACE%\Tools\SyncCodesService-main\SyncCodesService\output\Debug\net6.0\SyncCodesService.dll

@%SERVICE% %WORKSPACE%\Unity
