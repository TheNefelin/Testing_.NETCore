# .NET 8 Blazor WebApp + Assembly TicTacToe

### Creating project
1. Create a new Blazor WebApp
2. Change the Interactive Render Mode to WebAssembly
3. You got Server and Client projects 

### Projects 
* BlazorApp_TicTacToe
* BlazorApp_TicTacToe.Client
* ClassLibrary_TicTacToe

## BlazorApp_TicTacToe.Client
### Dependencies
```
Microsoft.AspNetCore.SignalR.Client
```
* Add using if necessary in page or in _Imports.razor
```
@using Microsoft.AspNetCore.SignalR.Client
@using ClassLibrary_TicTacToe
@using BlazorApp_TicTacToe.Client.Components
```
* Add Component Room.razor with css
* Add Page Lobby.razor

## BlazorApp_TicTacToe
* Add service and endpint in Program.cs
```
builder.Services.AddSignalR();

app.MapHub<GameHub>("/gameHub");
```
* Add Hubs GameHub.cs
