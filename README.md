# MotiWade

[![Build Status](https://dev.azure.com/rtuitlab/RTU%20IT%20Lab/_apis/build/status/RTUITLab.MotiWade?branchName=master)](https://dev.azure.com/rtuitlab/RTU%20IT%20Lab/_build/latest?definitionId=137&branchName=master)

## IOS App repo: https://github.com/RTUITLab/MotiWade-iOS

A mobile web app, which helps students to proceed education tasks without losing concentration, and university representatives to monitor student’s mental health and velocity.

## [Youtube Demo](https://www.youtube.com/watch?v=u0pHURiYnIs&ab_channel=RTUITLab) 

## [Live Demo](https://motiwade.rtuitlab.dev/)
## [Live Demo Admin](https://motiwade.rtuitlab.dev/admin/groupmanagment)

## [IOS App in TestFlight](https://testflight.apple.com/join/2Y2jajhm)

## Development

### Pass secret values

#### Google secret
Fill file `RealityShiftLearning/motiwade-firebase.json` from `A service account JSON file`

#### GitHub auth app
Fill file `RealityShiftLearning/appsettings.Local.json` with ClientId and Secret like
```json
{
  "GitHubOptions": {
    "ClientId": "sime client id",
    "Secret": "some secret"
  }
}
```

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
