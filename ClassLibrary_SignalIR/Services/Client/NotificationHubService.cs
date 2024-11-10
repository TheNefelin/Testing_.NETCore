using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;

namespace ClassLibrary_SignalIR.Services.Client
{
    public class NotificationHubService
    {
        private HubConnection _connection;

        public async Task Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7168/notificationHub")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string>("ReceiveNotification", (notification) =>
            {
                Console.WriteLine("Notificación recibida: " + notification);
            });

            _connection.Reconnecting += error =>
            {
                Console.WriteLine("Conexión perdida. Intentando reconectar...");
                Debug.Assert(_connection.State == HubConnectionState.Reconnecting);
                return Task.CompletedTask;
            };

            _connection.Reconnected += connectionId =>
            {
                Console.WriteLine("Reconexion exitosa!");
                Debug.Assert(_connection.State == HubConnectionState.Connected);
                return Task.CompletedTask;
            };

            //_connection.Closed += error =>
            //{
            //    Console.WriteLine("La conexión fue cerrada.");
            //    Debug.Assert(_connection.State == HubConnectionState.Disconnected);
            //    return Task.CompletedTask;
            //};

            _connection.Closed += async error =>
            {
                Console.WriteLine("La conexión fue cerrada. Intentando reconectar en 5 segundos...");
                Debug.Assert(_connection.State == HubConnectionState.Disconnected);
                await Task.Delay(5000);
                await StartConnectionAsync();
            };

            await StartConnectionAsync();
        }

        private async Task StartConnectionAsync()
        {
            try
            {
                Console.WriteLine("Conectado al Hub de SignalR.");
                await _connection.StartAsync();
                Debug.Assert(_connection.State == HubConnectionState.Connected);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar: " + ex.Message);
                //Debug.Assert(_connection.State == HubConnectionState.Disconnected);
            }
        }

        public async Task StopConnectionAsync()
        {
            Console.WriteLine("Conexión detenida y recursos liberados.");
            await _connection.StopAsync();
            await _connection.DisposeAsync();
        }
    }
}
