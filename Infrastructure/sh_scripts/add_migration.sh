cd ../
export CONN_STR='Host=localhost;Port=5432;Database=TitsSystem;Username=postgres;Password=root'
dotnet ef migrations add improve -o Data/Migrations