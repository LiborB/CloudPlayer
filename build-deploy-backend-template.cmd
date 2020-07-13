rem IMPORTANT: Change host name and directory and then rename this file as build-deploy-backend.cmd (to gitignore)

dotnet restore CloudPlayer.Server/CloudPlayerAPI

dotnet publish CloudPlayer.Server/CloudPlayerAPI -c Release

scp -r CloudPlayer.Server/CloudPlayerAPI/bin/ <host>:<directory>

pause