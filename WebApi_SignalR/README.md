# .NET 8 WebApi SignalR Server

### Relaated Projects
* [Console Client App](https://github.com/TheNefelin/Testing_.NETCore/tree/master/Console_SignalR_Client)
* [WebApi Server App](https://github.com/TheNefelin/Testing_.NETCore/tree/master/WebApi_SignalR)
* [ClassLibrary_SignalIR](https://github.com/TheNefelin/Testing_.NETCore/tree/master/ClassLibrary_SignalIR)

### Dependencies
```
System.IdentityModel.Tokens.Jwt
ClassLibrary_SignalIR
```

## Configure Server Program.cs
* Add SignalR service and route HUB
```
// Configure the SignalR services
builder.Services.AddSignalR();

// add Map the SignalR Hub route
app.MapHub<NotificationHub>("/notificationHub");
app.MapHub<ChatHub>("/chatHub");
```
* Add JWT Service and Use Authentication before UseAuthorization()
```
// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
    };

    // add config for HUB endpoint
    jwtOptions.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            if (!string.IsNullOrEmpty(accessToken) && context.HttpContext.WebSockets.IsWebSocketRequest)
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

// add autentication for JWT.
app.UseAuthentication();
```

## Hubs
* Notification Hub
```
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier; // get user id
        if (userId != null)
        {
            // You can store this ID in a list of connected users, a database, etc.
            // Example: Save it in a list of active connections (in memory or in a service).
            Console.WriteLine($"Usuario conectado: {userId}");
        }
        await base.OnConnectedAsync();
    }

    // Method for sending a message to all clients
    public async Task SendMessageToAll(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    // Method for sending a message to a specific client
    public async Task SendNotificationToClient(string clientId, string notification)
    {
        await Clients.Client(clientId).SendAsync("ReceiveNotification", notification);
    }
}
```

* Chat Hub
```
public class ChatHub : Hub
{
    private static ChatConversation _chatConversation = new ChatConversation();

    public async Task SendMessage(string senderId, string senderName, string messageContent)
    {
        var message = new Message(senderId, senderName, messageContent);

        _chatConversation.AddMessage(message);

        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public List<Message> GetChatHistory()
    {
        return _chatConversation.Messages;
    }
}
```

