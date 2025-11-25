using System.DirectoryServices.Protocols;
using System.Net;

namespace Enterprice
{
    public class ADService
    {
        private static string _server = "10.133.71.100";
        private static string _username = "adReader";
        private static string _password = "Merc1234!";
        private static string _domain = "mags.local";

        public static LdapConnection Connect()
        {
            try
            {
                Console.WriteLine("Defining Credentials");
                var credential = new NetworkCredential($"{_username}@{_domain}", _password);
                Console.WriteLine($"Credentials defined: {credential.UserName}");
                Console.WriteLine("Creating Connection");
                Console.WriteLine($"Server: {_server}");
                var connection = new LdapConnection(_server)
                {
                    Credential = credential, // Credentials to use for the connection
                    AuthType = AuthType.Negotiate // Negotiate is the default auth type for LDAP
                };

                Console.WriteLine("Binding to AD");
                connection.Bind(); // Test connection

                Console.WriteLine("Connection to AD established!");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to AD: {ex.Message}");
                throw;
            }
        }

        public void Start()
        {
            try
            {
                using var connection = Connect();
            }
            catch
            {
                Console.ReadKey();
                return;
            }

            // Show Menu
            ShowMenu();
            // Switch Menu
            switch (Console.ReadLine())
            {
                case "1":
                    UserADService userADService = new UserADService();
                    userADService.GetAllUsers();
                    break;
                case "2":
                    GroupADService.GetAllGroups();
                    Console.ReadKey();
                    break;
            }
        }
        public void ShowMenu()
        {
            Console.WriteLine("1. Get all users");
            Console.WriteLine("2. Get all groups");
            Console.WriteLine("3. Exit");
        }
    }
}
