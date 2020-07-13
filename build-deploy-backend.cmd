dotnet restore CloudPlayer.Server/CloudPlayerAPI

dotnet publish CloudPlayer.Server/CloudPlayerAPI -c Release

scp -r CloudPlayer.Server/CloudPlayerAPI/bin/ libor@178.128.117.202:/home/libor/CloudPlayer/CloudPlayer.Server/CloudPlayerAPI/

pause