using ClassLibrary_SignalIR.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;

namespace ClassLibrary_SignalIR.Services.Client
{
    public class ChatHubService
    {
        private HubConnection _connection;

        public async Task Start(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return;

            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7168/chatHub", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(token);
                })
                .WithAutomaticReconnect()
                .Build();


            _connection.On<List<Message>>("ReceiveChatHistory", messages =>
            {
                Console.WriteLine("Loading History");
                foreach (var message in messages)
                {
                    Console.WriteLine($"{message.IsRead} {message.Name}: {message.Content}");
                }
            });

            _connection.On<Message>("ReceiveMessage", message =>
            {
                Console.WriteLine($"{message.Name}: {message.Content}");
            });

            _connection.Reconnecting += error =>
            {
                Console.WriteLine("Intentando reconectar...");
                return Task.CompletedTask;
            };

            _connection.Reconnected += connectionId =>
            {
                Console.WriteLine("Reconexion exitosa!");
                return Task.CompletedTask;
            };

            _connection.Closed += async error =>
            {
                Console.WriteLine("Conexión cerrada, intentando reconectar...");
                await Task.Delay(TimeSpan.FromSeconds(5)); // Tiempo de espera antes de reconectar
                await StartConnectionAsync(); // Intentar reconectar manualmente si no es automático
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
            }
        }

        public bool IsConnected => _connection?.State == HubConnectionState.Connected;

        public async Task StopConnectionAsync()
        {
            Console.WriteLine("Conexión detenida y recursos liberados.");
            await _connection.StopAsync();
            await _connection.DisposeAsync();
        }
    }
}
