using ConsoleAppTesting.InjeccionDeDependencia.Repositories.Connections;
using ConsoleAppTesting.InjeccionDeDependencia.Repositories;
using ConsoleAppTesting.InjeccionDeDependencia.Services;
using ConsoleAppTesting.InjeccionDeDependencia.Services.imp;
using ConsoleAppTesting.Services;
using System.Security.Cryptography;
using System.Text;
using ConsoleAppTesting.Models.Dtos;
using ConsoleAppTesting.Models;

namespace ConsoleAppTesting.Utils
{
    internal class Menu
    {
        public void StartMenu()
        {
            int option = 0;
            int index;

            Title("Menu Principal");

            List<string> options =
            [
                "1.- Password Hash and Salt",
                "2.- Fake Hacker",
                "3.- Dependency Injection",
                "4.- Encrypt",
                "5.- Key Generator",
                "6.- Database",
                "7.- Encoding and Base64",
                "8.- Salir",
            ];

            do
            {
                MenuOptions(options);
                string input = Console.ReadLine();
                Console.Clear();

                if (!int.TryParse(input, out option))
                    ErrorOpcion("Ingrese una opcion valida");

                index = option - 1;

                if (option == 1)
                    StartHashPassword(options[index]);
                else if (option == 2)
                    StartFakeHacker(options[index]);
                else if(option == 3)
                    StartDependencyInjection(options[index]);
                else if(option == 4)
                    StartEncrypt(options[index]);
                else if(option == 5)
                    StartGenerateKey(options[index]);
                else if(option == 6)
                    StartDatabase(options[index]);
                else if (option == 7)
                    StartBase64(options[index]);
                else if(option == options.Count)
                    StartSalir(options[index]);
                else
                    ErrorOpcion("Ingrese una opcion valida");
            } while (option != options.Count);
        }

        private void StartHashPassword(string txt)
        {
            Title(txt);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter a new password: ");
            string? newPassword = Console.ReadLine();

            (string hashed, string salt) = Password.HashPassword(newPassword);

            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine($"Hashed: {hashed}");
            Console.WriteLine($"Salt: {salt}");
            Console.WriteLine($"----------------------------------------------");

            Console.Write("Enter the password for login: ");
            string verifyPassword = Console.ReadLine();

            Console.WriteLine($"----------------------------------------------");
            if (Password.VerifyPassword(verifyPassword, hashed, salt))
                Console.WriteLine("Login successful!");
            else
                Console.WriteLine("Invalid password. Login failed.");
            Console.WriteLine($"----------------------------------------------");
        }

        private void StartFakeHacker(string txt)
        {
            Title(txt);

            Console.ForegroundColor = ConsoleColor.Cyan;
            EduardoHaker elHaker = new EduardoHaker();
            elHaker.listarIPs();
            elHaker.RealizarAtaque();
        }

        private void StartDependencyInjection(string txt)
        {
            string connectionOption = "";
            string serviceOption = "";
            int index;

            Title(txt);

            List<string> options =
            [
                "1.- MySql",
                "2.- Sql Server",
                "3.- Oracle",
            ];

            do
            {
                MenuOptions(options);
                connectionOption = Console.ReadLine();

                if (int.TryParse(connectionOption, out index))
                    index -= 1;

                if (!connectionOption.Equals("1") && !connectionOption.Equals("2") && !connectionOption.Equals("3"))
                {
                    index = -1;
                    connectionOption = "";
                    ErrorOpcion("Ingrese una opcion valida");
                } 
            } while (connectionOption.Equals(""));

            Title(options[index]);
            options.Clear();

            options =
            [
                "1.- Email",
                "2.- SMS",
            ];

            do
            {
                MenuOptions(options);
                serviceOption = Console.ReadLine();

                if (int.TryParse(serviceOption, out index))
                    index -= 1;

                if (!serviceOption.Equals("1") && !serviceOption.Equals("2"))
                {
                    serviceOption = "";
                    ErrorOpcion("Ingrese una opcion valida");
                }
            } while (serviceOption.Equals(""));

            Title(options[index]);

            Console.ForegroundColor = ConsoleColor.Cyan;
            IDbConnection newConnection;

            if (connectionOption.Equals("1"))
                newConnection = new MySqlConnection();
            else if (connectionOption.Equals("2"))
                newConnection = new SqlServerConnection();
            else
                newConnection = new OracleConnection();

            ISenderService newService;

            if (serviceOption.Equals("1"))
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
            Console.WriteLine($"----------------------------------------------");
        }

        private void StartEncrypt(string txt)
        {
            Title(txt);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter a text for Encrypt: ");
            string? newText = Console.ReadLine();

            string Key = KeyGenerator.Salt32();
            string IV = KeyGenerator.Salt16();

            Console.WriteLine($"----------------------------------------------");
            string encrypt = Encryption.Encrypt(newText, Key, IV);
            Console.WriteLine($"Encrypt: {encrypt}");

            string decrypt = Encryption.Decrypt(encrypt, Key, IV);
            Console.WriteLine($"Decrypt: {decrypt}");
            Console.WriteLine($"----------------------------------------------");
        }

        private void StartGenerateKey(string txt)
        {
            Title(txt);

            Console.ForegroundColor = ConsoleColor.Cyan;
            string key16 = KeyGenerator.Salt16();
            Console.WriteLine($"Key 128 bits (16 bytes): {key16}");

            string key32 = KeyGenerator.Salt32();
            Console.WriteLine($"Key 256 bits (32 bytes): {key32}");

            string key64 = KeyGenerator.Salt64();
            Console.WriteLine($"Key 512 bits (64 bytes): {key64}");
            Console.WriteLine($"----------------------------------------------");
        }

        private void StartDatabase(string txt)
        {
            string option = "";
            int index;

            Title(txt);

            List<string> options =
            [
                "1.- Listar Usuarios",
                "2.- Crear Usuario",
                "3.- Iniciar Sesion",
                "4.- Encriptar Datos",
                "5.- Desencriptar Datos",
                "6.- Salir",
            ];
            
            do
            {
                MenuOptions(options);

                option = Console.ReadLine();

                if (int.TryParse(option, out index))
                    index -= 1;

                UserService userService = new UserService();

                if (option.Equals("1"))
                {
                    if (!userService.GetAll())
                        ErrorOpcion("Sin Datos");
                }
                else if (option.Equals("2"))
                {
                    Console.Write("Ingrese Correo: ");
                    string email = Console.ReadLine();

                    Console.Write("Ingrese Clave: ");
                    string clave = Console.ReadLine();

                    Console.Write("Ingrese Clave Cifrado: ");
                    string claveC = Console.ReadLine();


                    if (email.Equals("") || clave.Equals("") || claveC.Equals(""))
                        ErrorOpcion("Ingrese Correo y Claves");
                    else
                        userService.Add(email, clave, claveC);
                }
                else if (option.Equals("3"))
                {
                    Console.Write("Ingrese Correo: ");
                    string email = Console.ReadLine();

                    Console.Write("Ingrese Clave: ");
                    string clave = Console.ReadLine();

                    if (email.Equals("") || clave.Equals(""))
                        ErrorOpcion("Ingrese Correo y Clave");
                    else
                        Console.WriteLine(userService.Login(email, clave));                        
                }
                else if (option.Equals("4"))
                {
                    Console.Write("Ingrese Id: ");
                    string id = Console.ReadLine();

                    Console.Write("Ingrese SqlToken: ");
                    string sqlToken = Console.ReadLine();

                    Console.Write("Ingrese Clave: ");
                    string clave = Console.ReadLine();

                    Console.Write("Ingrese Data1: ");
                    string data1 = Console.ReadLine();

                    Console.Write("Ingrese Data2: ");
                    string data2 = Console.ReadLine();

                    Console.Write("Ingrese Data3: ");
                    string data3 = Console.ReadLine();

                    DataDTO data = new()
                    {
                        Data1 = data1,
                        Data2 = data2,
                        Data3 = data3,
                    };

                    ServiceResponse<object> res = userService.SaveData(id, sqlToken, clave, data);

                    if (res.Status)
                        Console.WriteLine(res.Message);
                    else
                        ErrorOpcion(res.Message);
                }
                else if (option.Equals("5"))
                {
                    Console.Write("Ingrese Id: ");
                    string id = Console.ReadLine();

                    Console.Write("Ingrese SqlToken: ");
                    string sqlToken = Console.ReadLine();

                    Console.Write("Ingrese Clave: ");
                    string clave = Console.ReadLine();

                    userService.GetData(id, sqlToken, clave);
                }
                else if (option.Equals("6"))
                    Console.WriteLine("Saliendo");
                else
                {
                    ErrorOpcion("Ingrese una opcion valida");
                }
            } while (option.Equals("") || !option.Equals("6"));
        }

        private void StartBase64(string txt)
        {
            Title(txt);

            Console.ForegroundColor = ConsoleColor.Cyan;
            byte[] byteArray = RandomNumberGenerator.GetBytes(6);
            string toEncoded = Encoding.UTF8.GetString(byteArray);
            string toBase64 = Convert.ToBase64String(byteArray);
            byte[] fromEncoded = Encoding.UTF8.GetBytes(toEncoded);
            byte[] fromBase64 = Convert.FromBase64String(toBase64);

            Console.WriteLine($"-- Byte Array --------------------------------");
            Console.WriteLine($"{byteArray[0]} {byteArray[1]} {byteArray[2]} {byteArray[3]} {byteArray[4]} {byteArray[5]}");
            Console.WriteLine($"-- To Base64 ---------------------------------");
            Console.WriteLine($"{toBase64}");
            Console.WriteLine($"-- From Base64 -------------------------------");
            Console.WriteLine($"{fromBase64[0]} {fromBase64[1]} {fromBase64[2]} {fromBase64[3]} {fromBase64[4]} {fromBase64[5]}");
            Console.WriteLine($"-- To Encoded UTF8 ---------------------------");
            Console.WriteLine($"{toEncoded}");
            Console.WriteLine($"-- From Encoded UTF8 -------------------------");
            Console.WriteLine($"{fromEncoded[0]} {fromEncoded[1]} {fromEncoded[2]} {fromEncoded[3]} {fromEncoded[4]} {fromEncoded[5]}");
            Console.WriteLine($"----------------------------------------------");
        }

        private void StartSalir(string txt)
        {
            Title(txt);
        }

        private void ErrorOpcion(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine(txt);
            Console.WriteLine($"----------------------------------------------");
        }

        private void Title(string txt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----------------------------------------------");
            Console.WriteLine(txt);
            Console.WriteLine($"----------------------------------------------");
        }

        private void MenuOptions(List<string> opciones)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"-- Seleccione una Opcion ---------------------");
            foreach (string opcion in opciones)
            {
                Console.WriteLine(opcion);
            }
            Console.WriteLine($"----------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Ingrese Opcion: ");
        }
    }
}
