using System.DirectoryServices.Protocols;

namespace ActiveDirectoryTesting
{
    public partial class ActiveDirectoryService
    {
        /// <summary>
        /// Henter alle brugere fra Active Directory
        /// </summary>
        /// <returns>Liste af ADUser objekter</returns>
        public List<ADUser> GetAllUsers()
        {
            var users = new List<ADUser>();

            using (var connection = GetConnection())
            {
                var searchRequest = new SearchRequest(
                    GetBaseDN(),
                    "(objectClass=user)",
                    SearchScope.Subtree,
                    "cn",
                    "samAccountName",
                    "mail",
                    "department",
                    "title",
                    "distinguishedName"
                );

                try
                {
                    var response = (SearchResponse)connection.SendRequest(searchRequest);

                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        var user = new ADUser
                        {
                            Name = entry.Attributes["cn"]?[0]?.ToString() ?? "N/A",
                            Username = entry.Attributes["samAccountName"]?[0]?.ToString() ?? "N/A",
                            Email = entry.Attributes["mail"]?[0]?.ToString() ?? "N/A",
                            Department = entry.Attributes["department"]?[0]?.ToString() ?? "N/A",
                            Title = entry.Attributes["title"]?[0]?.ToString() ?? "N/A",
                            DistinguishedName = entry.Attributes["distinguishedName"]?[0]?.ToString() ?? "N/A"
                        };

                        users.Add(user);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Fejl ved hentning af brugere: {ex.Message}");
                }
            }

            return users;
        }

        /// <summary>
        /// Søger efter brugere baseret på søgeterm
        /// </summary>
        /// <param name="searchTerm">Søgeterm til at søge efter</param>
        /// <returns>Liste af matchende ADUser objekter</returns>
        public List<ADUser> SearchUsers(string searchTerm)
        {
            var users = new List<ADUser>();

            using (var connection = GetConnection())
            {
                var filter = $"(|(cn=*{searchTerm}*)(mail=*{searchTerm}*)(department=*{searchTerm}*)(title=*{searchTerm}*))";
                
                var searchRequest = new SearchRequest(
                    GetBaseDN(),
                    filter,
                    SearchScope.Subtree,
                    "cn",
                    "samAccountName",
                    "mail",
                    "department",
                    "title",
                    "distinguishedName"
                );

                try
                {
                    var response = (SearchResponse)connection.SendRequest(searchRequest);

                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        var user = new ADUser
                        {
                            Name = entry.Attributes["cn"]?[0]?.ToString() ?? "N/A",
                            Username = entry.Attributes["samAccountName"]?[0]?.ToString() ?? "N/A",
                            Email = entry.Attributes["mail"]?[0]?.ToString() ?? "N/A",
                            Department = entry.Attributes["department"]?[0]?.ToString() ?? "N/A",
                            Title = entry.Attributes["title"]?[0]?.ToString() ?? "N/A",
                            DistinguishedName = entry.Attributes["distinguishedName"]?[0]?.ToString() ?? "N/A"
                        };

                        users.Add(user);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Fejl ved søgning efter brugere: {ex.Message}");
                }
            }

            return users;
        }

        /// <summary>
        /// Viser alle brugere i konsollen
        /// </summary>
        public void ShowAllUsers()
        {
            Console.WriteLine("=== Alle Brugere ===");
            var users = GetAllUsers();
            
            if (users.Count == 0)
            {
                Console.WriteLine("Ingen brugere fundet.");
                return;
            }

            Console.WriteLine($"Fundet {users.Count} brugere:");
            Console.WriteLine();
            
            foreach (var user in users.OrderBy(u => u.Name))
            {
                Console.WriteLine($"Navn: {user.Name}");
                Console.WriteLine($"Brugernavn: {user.Username}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Afdeling: {user.Department}");
                Console.WriteLine($"Titel: {user.Title}");
                Console.WriteLine(new string('-', 50));
            }
        }

        /// <summary>
        /// Søger og viser brugere baseret på søgeterm
        /// </summary>
        public void SearchUsers()
        {
            Console.WriteLine("=== Søg efter Brugere ===");
            Console.Write("Indtast søgekriterie (navn, email, afdeling, eller titel): ");
            var searchTerm = Console.ReadLine();

            if (string.IsNullOrEmpty(searchTerm))
            {
                Console.WriteLine("Søgeterm kan ikke være tom.");
                return;
            }

            var users = SearchUsers(searchTerm);
            
            if (users.Count == 0)
            {
                Console.WriteLine("Ingen brugere fundet med det søgekriterie.");
                return;
            }

            Console.WriteLine($"Fundet {users.Count} brugere:");
            Console.WriteLine();
            
            foreach (var user in users.OrderBy(u => u.Name))
            {
                Console.WriteLine($"Navn: {user.Name}");
                Console.WriteLine($"Brugernavn: {user.Username}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Afdeling: {user.Department}");
                Console.WriteLine($"Titel: {user.Title}");
                Console.WriteLine(new string('-', 50));
            }
        }
    }
}
