cd ../
export CONN_STR='Host=localhost;Port=5432;Database=TitsSystem;Username=postgres;Password=root'
dotnet ef migrations add AddOrderAddress -o Data/Migrations
read -p "Press enter to continue"