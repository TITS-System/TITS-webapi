cd ../
export CONN_STR='Host=localhost;Port=55432;Database=TitsSystem;Username=postgres;Password=root'
echo y | dotnet ef database drop
read -p "Press enter to continue"