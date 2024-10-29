// See https://aka.ms/new-console-template for more information
using ConsoleFetchAPI.Models;
using ConsoleFetchAPI.Services;

AuthService authService = new AuthService();
CoreService coreService = new CoreService();
CoreRequest<CoreData> coreRequest = new();

Console.WriteLine("-- Register -------------------------------------");
Register register = new() {Email = "user@example.com", Password1 = "string", Password2 = "string",};
Result<object> resultRegister = await authService.Register(register);
Console.WriteLine($"Success: {resultRegister.Success}, StatusCode: {resultRegister.StatusCode}, Message: {resultRegister.Message}");
Console.WriteLine("-------------------------------------------------");
Console.WriteLine();

Console.WriteLine("-- Login ----------------------------------------");
Login login = new() {Email = "user@example.com", Password = "string",};
Result<LoggedIn> resultLogin = await authService.Login(login);
Console.WriteLine($"Success: {resultLogin.Success}, StatusCode: {resultLogin.StatusCode}, Message: {resultLogin.Message}");
Console.WriteLine($"Id: {resultLogin?.Data?.Id_User}");
Console.WriteLine($"SqlToken: {resultLogin?.Data?.Sql_Token}");
Console.WriteLine($"Role: {resultLogin?.Data?.Role}");
Console.WriteLine($"ExpireMin: {resultLogin?.Data?.ExpireMin}");
Console.WriteLine($"ApiToken: {resultLogin?.Data?.ApiToken}");
coreRequest.Id_User = resultLogin.Data.Id_User;
coreRequest.Sql_Token = resultLogin.Data.Sql_Token;
Console.WriteLine("-------------------------------------------------");
Console.WriteLine();

Console.WriteLine("-- Core Register --------------------------------");
coreRequest.Password = "string";
Result<CoreIV> resultCoreRegister = await coreService.CoreRegister(resultLogin.Data.ApiToken, coreRequest);
Console.WriteLine($"Success: {resultCoreRegister.Success}, StatusCode: {resultCoreRegister.StatusCode}, Message: {resultCoreRegister.Message}");
Console.WriteLine($"IV: {resultCoreRegister.Data.IV}");
Console.WriteLine("-------------------------------------------------");
Console.WriteLine();

Console.WriteLine("-- Core Login -----------------------------------");
Result<CoreIV> resultCoreLogin = await coreService.CoreLogin(resultLogin.Data.ApiToken, coreRequest);
Console.WriteLine($"Success: {resultCoreLogin.Success}, StatusCode: {resultCoreLogin.StatusCode}, Message: {resultCoreLogin.Message}");
Console.WriteLine($"IV: {resultCoreLogin.Data.IV}");
Console.WriteLine("-------------------------------------------------");
Console.WriteLine();

Console.WriteLine("-- Core GetAll ----------------------------------");
Result<List<CoreData>> resultGetAll = await coreService.CoreGetAll(resultLogin.Data.ApiToken, coreRequest);
Console.WriteLine($"Success: {resultGetAll.Success}, StatusCode: {resultGetAll.StatusCode}, Message: {resultGetAll.Message}");
Console.WriteLine($"IV: {resultGetAll.Data.Count}");
Console.WriteLine("-------------------------------------------------");
Console.WriteLine();

CoreData coreData = new()
{
    Data01 = "Prueba",
    Data02 = "Prueba",
    Data03 = "Prueba",
    Id_User = resultLogin.Data.Id_User
};

coreRequest.CoreData = coreData;

Console.WriteLine("-- Core Insert ----------------------------------");
Result<CoreData> resultInsert= await coreService.CoreInsert(resultLogin.Data.ApiToken, coreRequest);
Console.WriteLine($"Success: {resultInsert.Success}, StatusCode: {resultInsert.StatusCode}, Message: {resultInsert.Message}");
Console.WriteLine("-------------------------------------------------");
Console.WriteLine();
