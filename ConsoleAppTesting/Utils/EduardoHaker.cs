namespace ConsoleAppTesting.Utils
{
    internal class EduardoHaker
    {
        private int cont = 0;

        private string[,] listaIps =
{
                {"HostGator", "192.168.1.1"},
                {"Bluehost", "192.168.1.2"},
                {"GoDaddy", "192.168.1.3"},
                {"DreamHost", "192.168.1.4"},
                {"Google", "192.168.1.5"},
                {"SiteGround", "192.168.1.6"},
                {"A2 Hosting", "192.168.1.7"},
                {"InMotion Hosting", "192.168.1.8"},
                {"iPage", "192.168.1.9"},
                {"Namecheap", "192.168.1.10"},
                {"1&1 IONOS", "192.168.1.11"},
                {"Hostinger", "192.168.1.12"},
                {"Liquid Web", "192.168.1.13"},
                {"OVH", "192.168.1.14"},
                {"Kinsta", "192.168.1.15"},
                {"WP Engine", "192.168.1.16"},
                {"GoDaddy Pro", "192.168.1.17"},
                {"Bluehost India", "192.168.1.18"},
                {"Cloudways", "192.168.1.19"},
                {"DigitalOcean", "192.168.1.20"},
                {"Linode", "192.168.1.21"},
                {"Rackspace", "192.168.1.22"},
                {"HostPapa", "192.168.1.23"},
                {"Flywheel", "192.168.1.24"},
                {"GreenGeeks", "192.168.1.25"}
            };

        public EduardoHaker()
        {
            Console.WriteLine($"...Scaning IPs");
        }

        public void listarIPs()
        {
            for (int i = 0; i < listaIps.GetLength(0); i++)
            {
                Console.WriteLine($"IP Detected... {listaIps[i, 1]}, Related Hosting: {listaIps[i, 0]}");
                Thread.Sleep(200);
                cont += 200;
            }

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Scan Completed in {cont / listaIps.GetLength(0)}sec, Attack Started");
            Console.WriteLine("----------------------------------------------");
        }

        public void RealizarAtaque()
        {
            for (int i = 0; i < listaIps.GetLength(0); i++)
            {
                Console.WriteLine($"Performing Attack to IP: {listaIps[i, 1]}, ARP Poisoning");
                Thread.Sleep(200);
                cont += 200;
            }

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Successful Attack, Time passed: {cont / listaIps.GetLength(0)}sec, Attack Started");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("------------ Successful Attack --------------");
        }
    }
}
