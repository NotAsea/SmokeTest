# SmokeTestLogin

## Description

This project use for example of Smoke test it contain 3 project

```bash
 SmokeTestLogin.sln
 |-- SmokeTestLogin.Data
 |-- SmokeTestLogin.Web
 |-- SmokeTestLogin.Test

```

* [SmokeTestLogin.Data](./SmokeTestLogin.Data) contains code for table definition,
  a [MainContext](./SmokeTestLogin.Data/MainContext.cs) class for Entity Framework Core
* [SmokeTestLogin.Web](./SmokeTestLogin) contains ASP.NET CORE MVC , it only has single `Home` and `Login` view, with
  simple [MustLoginAttribute](./SmokeTestLogin/Customs/MustLoginAttribute.cs) to handle authenticate filter
* [SmokeTestLogin.Test](./SmokeTestLogin.Test) project contain MSTest code for testing, two
  class [LoginTest](./SmokeTestLogin.Test/LoginTest.cs) for test login, logout, or authorization
  checking, [UserFunctionTest](./SmokeTestLogin.Test/UserFunctionTest.cs) for test user create, read, update, delete

SQLite database is used for sake of simple, only Entity Framework Core is required, anything else is native ASP.NET
CORE, Bogus.Faker is used for generating fake data, but can be remove

## Build

- ensure dotnet sdk at least 6 is installed, or Visual Studio 2022 with Web development workload installed
- prefer open terminal or bash in Linux to scaffold database (Note it's already had Seed Data)

 ```cmd
 :: ensure stay in solution Directory
 dotnet restore
 dotnet tool install -- global dotnet-ef --version 7.0.10
 dotnet ef database update --project .\SmokeTestLogin.Data\SmokeTestLogin.Data.csproj --startup-project .\SmokeTestLogin\SmokeTestLogin.Web.csproj
 :: for mac / linux
 :: dotnet ef database update --project ./SmokeTestLogin.Data/SmokeTestLogin.Data.csproj --startup-project ./SmokeTestLogin/SmokeTestLogin.Web.csproj
 ```

- after database has been update, simple build in visual studio or run `dotnet build`

*Password generate can be found at [here](./PasswordGen.md)*


 