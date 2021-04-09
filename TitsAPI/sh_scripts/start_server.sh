cd tits_api
export CONN_STR="Host=localhost;Port=5432;Database=TitsSystem;Username=postgres;Password=root"
export ASPNETCORE_URLS="http://+:8080\;https://+:8443"
export ASPNETCORE_ENVIRONMENT="Development"
dotnet TitsAPI.dll &>asp_log.txt