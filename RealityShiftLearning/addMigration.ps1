$Env:DbType='SQLite'; dotnet ef migrations add -p ../Database.SQLite "$($args[0])_SQLite"
$Env:DbType='Postgres'; dotnet ef migrations add -p ../Database.Postgres "$($args[0])_Postgres"