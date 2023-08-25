# SmokeTestLogin

## Description
This project use for example of Smoke test it contain 3 project
```bash
 SmokeTestLogin.sln
 |-- SmokeTestLogin.Data
 |-- SmokeTestLogin.Web
 |-- SmokeTestLogin.Test

```
* [SmokeTestLogin.Data](./SmokeTestLogin.Data) contains code for table defination, a [MainContext](./SmokeTestLogin.Data/MainContext.cs) class for Entity Framework Core
* [SmokeTestLogin.Web](./SmokeTestLogin) contains ASP.NET CORE MVC , it only has single `Home` and `Login` view, with simple [MustLoginAttribute](./SmokeTestLogin/Customs/MustLoginAttribute.cs) to handle authenticate filter
* [SmokeTestLogin.Test](./SmokeTestLogin.Test) project contain MSTest code for testing, it contain two test `Test_AuthorizeAtribute_Redirect_When_Not_Login` which test when unauthenticated user try enter `Home` and `Test_Login_Success` which test login flow, these two include in [UnitTest](./SmokeTestLogin.Test/UnitTest.cs) class

SQLite database is used for sake of simple, only Entity Framework Core is required, anything else is native ASP.NET CORE 

## Build

 - ensure dotnet sdk at least 6 is installed, or Visual Studio 2022 with Web development workload installed
- prefer open terminal or bash in Linux to scaffold database

 ```cmd
 :: ensure stay in solution Directory
 dotnet restore
 dotnet tool install --global dotnet-ef
 dotnet ef database update --project .\SmokeTestLogin.Data\SmokeTestLogin.Data.csproj --startup-project .\SmokeTestLogin\SmokeTestLogin.Web.csproj
 :: change \ to / if in Linux/Mac
 ```
 - after database has been update, simple build in visual studio or run `dotnet build`
 