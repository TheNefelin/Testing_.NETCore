using ConsoleAppTesting.InjeccionDeDependencia.Repositories.Connections;
using ConsoleAppTesting.InjeccionDeDependencia.Repositories;
using ConsoleAppTesting.InjeccionDeDependencia.Services;
using ConsoleAppTesting.InjeccionDeDependencia.Services.imp;
using System.Collections.Generic;

namespace ConsoleAppTesting.Clases
{
    internal class Menu
    {
        public void StartMenu()
        {
            int option;
            List<string> opciones =
            [
                "1.- Password Hash and Salt",
                "2.- Fake Hacker",
                "3.- Dependency Injection",
                "4.- Encrypt",
                "5.- Key Generator",
                "6.- Salir",
            ];

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"----------- Seleccione una Opcion ------------");
                foreach (string opcion in opciones)
                {
                    Console.WriteLine(opcion);
                }
                Console.WriteLine($"----------------------------------------------");

                string line = Console.ReadLine();

                if (int.TryParse(line, out option))
                {
                    if (option >= 1 && option <= opciones.Count)
                    {
                        Console.Clear();

                        if (option == 1)
                            StartHashPassword(opciones[option - 1]);
                        if (option == 2)
                            StartFakeHacker(opciones[option - 1]);
                        if (option == 3)
                            StartDependencyInjection(opciones[option - 1]);
                        if (option == 4)
                            Encrypt(opciones[option - 1]);
                        if (option == 5)
                            GenerateKey(opciones[option - 1]);
                        if (option == opciones.Count)
                            StartSalir(opciones[option - 1]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"----------------------------------------------");
                        Console.WriteLine("Ingrese una opcion valida");
                        Console.WriteLine($"----------------------------------------------");
                        option = 0;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"----------------------------------------------");
                    Console.WriteLine("Ingrese una opcion valida");
                    Console.WriteLine($"----------------------------------------------");
                    option = 0;
                }
            }
            while (option != opciones.Count);
        }

        private void StartHashPassword(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine(txt);
            Console.WriteLine($"----------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter a new password: ");
            string? newPassword = Console.ReadLine();

            Password password = new Password();
            (string hashed, string salt) = password.HashPassword(newPassword);

            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine($"Hashed: {hashed}");
            Console.WriteLine($"Salt: {salt}");
            Console.WriteLine($"----------------------------------------------");

            Console.Write("Enter the password for login: ");
            string verifyPassword = Console.ReadLine();

            Console.WriteLine($"----------------------------------------------");
            if (password.VerifyPassword(verifyPassword, hashed, salt))
                Console.WriteLine("Login successful!");
            else
                Console.WriteLine("Invalid password. Login failed.");
            Console.WriteLine($"----------------------------------------------");
        }

        private void StartFakeHacker(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine(txt);
            Console.WriteLine($"----------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;

            EduardoHaker elHaker = new EduardoHaker();
            elHaker.listarIPs();
            elHaker.RealizarAtaque();
        }

        private void StartDependencyInjection(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine(txt);
            Console.WriteLine($"----------------------------------------------");

            string connectionOption = "";

            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("----------- Seleccione Conexion --------------");
                Console.WriteLine("A.- MySql");
                Console.WriteLine("B.- Sql Server");
                Console.WriteLine("C.- Oracle");
                Console.WriteLine("----------------------------------------------");
                string line = Console.ReadLine();

                if (line.ToLower().Equals("a"))
                    connectionOption = "a";
                else if (line.ToLower().Equals("b"))
                    connectionOption = "b";
                else if (line.ToLower().Equals("c"))
                    connectionOption = "c";
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ingree un opacion valida");
                }

            } while (connectionOption.Equals(""));

            string serviceOption = "";

            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("----------- Seleccione Servicio --------------");
                Console.WriteLine("A.- Email");
                Console.WriteLine("B.- SMS");
                Console.WriteLine("----------------------------------------------");
                string line = Console.ReadLine();

                if (line.ToLower().Equals("a"))
                    serviceOption = "a";
                else if (line.ToLower().Equals("b"))
                    serviceOption = "b";
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ingree un opacion valida");
                }

            } while (serviceOption.Equals(""));
            Console.WriteLine("----------------------------------------------");

            IDbConnection newConnection;

            if (connectionOption.ToLower().Equals("a"))
                newConnection = new MySqlConnection();
            else if (connectionOption.ToLower().Equals("b"))
                newConnection = new SqlServerConnection();
            else
                newConnection = new OracleConnection();

            ISenderService newService;

            if (serviceOption.ToLower().Equals("a"))
                newService = new EmailService();
            else
                newService = new SMSService();

            var customerRepository = new CustomerRepository(newConnection);
            var communicationService = new CommunicationService(newService);

            var customerService = new CustomerService(customerRepository);
            var customers = customerService.GetCustomers();
            var message = "Message to Bbroadcast to all customers";

            foreach (var customer in customers)
            {
                communicationService.SendMessage(customer, message);
            }
        }

        private void Encrypt(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine(txt);
            Console.WriteLine($"----------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter a text for Encrypt: ");
            string? newText = Console.ReadLine();

            Console.WriteLine($"----------------------------------------------");
            string encrypt = Encryption.Encrypt(newText);
            Console.WriteLine($"Encrypt: {encrypt}");
            
            string decrypt = Encryption.Decrypt(encrypt);
            Console.WriteLine($"Decrypt: {decrypt}");
            Console.WriteLine($"----------------------------------------------");
        }

        private void GenerateKey(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine(txt);
            Console.WriteLine($"----------------------------------------------");

            string key16 = KeyGenerator.key16();
            Console.WriteLine($"Key 128 bits (16 bytes): {key16}");

            string key32 = KeyGenerator.key32();
            Console.WriteLine($"Key 256 bits (32 bytes): {key32}");

            string key64 = KeyGenerator.key64();
            Console.WriteLine($"Key 512 bits (64 bytes): {key64}");
            Console.WriteLine($"----------------------------------------------");
        }

        private void StartSalir(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine(txt);
            Console.WriteLine($"----------------------------------------------");
        }
    }
}
