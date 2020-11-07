$Env:DbType='SQLite'; dotnet ef migrations add -p ../Database.SQLite -c LearnDbContext "$($args[0])_SQLite";
$Env:DbType='Postgres'; dotnet ef migrations add -p ../Database.Postgres -c LearnDbContext "$($args[0])_Postgres";
$Env:DbType='SQLite';