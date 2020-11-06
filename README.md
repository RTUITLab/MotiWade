# MotiWake

## Development

### Run
```bash
cd RealityShiftLearning
dotnet watch -p .\RealityShiftLearning.csproj run
```


### Add migration

```bash
cd RealityShiftLearning
./addMigration.ps1 <MigrationName>
# or
./addMigration.sh <MigrationName>
```

### Run in docker-compose environment

```bash
./build
docker-compose up -d
```

Go to http://127.0.0.1:5501
