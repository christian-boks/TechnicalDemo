# Install packages
These are the packages that have been installed in the project.

```
dotnet new web
dotnet add package Npgsql
dotnet add package Dapper
dotnet add package Swashbuckle.AspNetCore.Annotations
dotnet add package NUnit --version 4.0.1
dotnet add package Moq --version 4.20.70

dotnet add package Grpc.Net.Client
dotnet add package Google.Protobuf 
dotnet add package Grpc.Tools --version 2.62.0
```


## Adding `swagger.json` export functionality

Install the following packages:

``` bash
dotnet new tool-manifest
dotnet tool install SwashBuckle.AspNetCore.Cli
```

Add the following to the .csproj file:
``` xml
<Target Name="CreateSwaggerJson" AfterTargets="Build" Condition="$(Configuration)=='Debug'">
  <Exec Command="dotnet swagger tofile --output ./resources/swagger.json $(OutputPath)$(AssemblyName).dll v1" WorkingDirectory="$(ProjectDir)" />
</Target>
```

On build the cli will export the `swagger.json` file to the `resources` folder.