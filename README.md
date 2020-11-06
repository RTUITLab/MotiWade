# MotiWake

[![Build Status](https://dev.azure.com/rtuitlab/RTU%20IT%20Lab/_apis/build/status/RTUITLab.MotiWade?branchName=master)](https://dev.azure.com/rtuitlab/RTU%20IT%20Lab/_build/latest?definitionId=137&branchName=master)

[Demo](https://motiwade.rtuitlab.dev/)

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
