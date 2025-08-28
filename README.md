# Budget API
This is the backend of a personal finance application.

The project architecture is an ASP.NET (.NET 8) API running on a Lambda with a PostgreSQL database, deployed using AWS CDK (Cloud Development Kit).


Build Steps:

1. `cd` into the .NET project (./BudgetAPI) and run:
```
dotnet lambda package --configuration Release --framework net8.0 --output-package bin/Release/net8.0/BudgetAPI.zip
```

2. To install dependencies, `cd` into the AWS CDK infrastrucutre (./BudgetAPIInfrastructure) and run

```
npm install
```

3. `cd` into the AWS CDK infrastrucutre (./BudgetAPIInfrastructure) project and run:
```
cdk bootstrap
```

4. In the AWS CDK infrastructure project run:
```
cdk deploy
```


Deleting Stack:

1. `cd` into the AWS CDK infrastructure project and run:
```
cdk destroy BugetAPIInfrastructureStack
```


For all `cdk` commands, if you do not have a default AWS profile, specify the profile as such:
```
cdk bootstrap --profile MyUser-Profile
```
