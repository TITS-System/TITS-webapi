cd webapi
export CONN_STR='Host=localhost;Port=5432;Database=DodoHack;Username=postgres;Password=root'
export ASPNETCORE_URLS='http://*:8080\;https://*:8443'
export ASPNETCORE_ENVIRONMENT='Development'
dotnet WebAPI.dll &>asp_log.txt