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
* [SmokeTestLogin.Web](./SmokeTestLogin) contains ASP.NET CORE MVC , it only has single `Home` and `Login` view, with simple [MustLoginAttribute](./SmokeTestLogin/Customs/MustLoginAttribute.cs) to hanlde authenticate
* [SmokeTestLogin.Test](./SmokeTestLogin.Test) project contain MSTest code for testing, it contain two test `Test_AuthorizeAtribute_Redirect_When_Not_Login` which test when unauthenticated user try enter `Home` and `Test_Login_Success` which test login flow