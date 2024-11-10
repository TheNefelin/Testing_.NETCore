using ClassLibrary_SignalIR.DTOs;
using ClassLibrary_SignalIR.Services.Client;
using ClassLibrary_SignalIR.Services.Server;
using Spectre.Console;

namespace Console_SignalR_Client
{
    public class Program
    {
        private static NotificationHubService _hubNotification;
        private static ChatHubService _hubChat;

        static async Task Main(string[] args)
        {
            _hubNotification = new NotificationHubService();
            _hubChat = new ChatHubService();
            await ShowMainMenu();
        }

        static async Task ShowMainMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();

                var prompt = new SelectionPrompt<string>()
                    .Title("Menú Principal")
                    .AddChoices("1. Iniciar Sesión para Chatear", "2. Notificaciones", "3. Salir");

                var choice = AnsiConsole.Prompt(prompt);

                switch (choice)
                {
                    case "1. Iniciar Sesión para Chatear":
                        await LoginChat();
                        break;
                    case "2. Notificaciones":
                        await ShowNotifications();
                        break;
                    case "4. Salir":
                        //await _hub.StopConnectionAsync();
                        ExitMenu();
                        return;
                }
            }
        }

        static async Task LoginChat()
        {
            AnsiConsole.Clear();
            var rule = new Rule("[green]Iniciar Sesión[/]");
            rule.Justification = Justify.Left;
            AnsiConsole.Write(rule);

            var email = AnsiConsole.Ask<string>("Usuario:");
            var password = AnsiConsole.Prompt(
                new TextPrompt<string>("Contraseña:")
                    .Secret());

            var user = new LoginDTO()
            {
                Email = email,
                Password = password
            };

            // Ejecuta el spinner de carga mientras se procesa el inicio de sesión
            var loginResult = await AnsiConsole.Status()
                .StartAsync("Iniciando sesión...", async ctx =>
                {
                    ctx.Spinner(Spinner.Known.BouncingBall);
                    ctx.SpinnerStyle(Style.Parse("green"));

                    var service = new AuthService();
                    var token = await service.Login(user);

                    if (!string.IsNullOrEmpty(token)) {
                        await _hubChat.Start(token);

                        if (_hubChat.IsConnected)
                            return "Conectado exitosamente!";
                        else
                            return "Error al conectar al chat.";
                    };

                    return "Conectando...!";
                });

            // Muestra el resultado y espera a que el usuario vuelva al menú principal
            AnsiConsole.MarkupLine($"[green]{loginResult}[/]");
            AnsiConsole.MarkupLine("[yellow]Presione cualquier tecla para volver al menú principal.[/]");
            Console.ReadKey();

            await _hubChat.StopConnectionAsync();
        }

        static async Task ShowNotifications()
        {
            var rule = new Rule("[green]Notificaciones Activas[/]");
            rule.Justification = Justify.Left;
            AnsiConsole.Write(rule);

            await _hubNotification.Start();

            AnsiConsole.MarkupLine("[yellow]Recibiendo notificaciones en tiempo real. Presione cualquier tecla para volver al menú principal.[/]");
            Console.ReadKey();

            await _hubNotification.StopConnectionAsync();
        }

        static void ExitMenu()
        {
            AnsiConsole.MarkupLine("[bold red]Saliendo...[/]");
        }
    }
}
