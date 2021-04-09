cd dodohackapi
export CONN_STR="Host=localhost;Port=5432;Database=DodoHack;Username=postgres;Password=root"
export ASPNETCORE_URLS="http://+:8400\;https://+:8443"
export ASPNETCORE_ENVIRONMENT="Development"
dotnet DodoHackAPI.dll &>asp_log.txt