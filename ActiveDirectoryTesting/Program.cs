using System.DirectoryServices.Protocols;
using System.Net;

namespace ActiveDirectoryTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var adTester = new ADTester();
            adTester.Run();
        }
    }

    // Modeller for AD objekter
    public class ADGroup
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Members { get; set; } = new List<string>();
    }

    public class ADUser
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string DistinguishedName { get; set; } = string.Empty;
    }

    // Konfigurationsklasse for runtime indstillinger
    public class ADConfig
    {
        public string Server { get; set; } = "10.133.71.111";
        public string Username { get; set; } = "Admin";
        public string Password { get; set; } = "Cisco1234!";
        public string Domain { get; set; } = "Hotel.local";
    }

    // Hovedklasse for AD tester
    public class ADTester
    {
        private ADConfig _config = new ADConfig();

        public void Run()
        {
            Console.WriteLine("=== Active Directory Tester ===");
            Console.WriteLine();

            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowAllGroups();
                            break;
                        case "2":
                            ShowAllUsers();
                            break;
                        case "3":
                            ShowGroupsWithMembers();
                            break;
                        case "4":
                            SearchUsers();
                            break;
                        case "5":
                            SearchGroups();
                            break;
                        case "6":
                            UpdateConfig();
                            break;
                        case "7":
                            TestConnection();
                            break;
                        case "0":
                            Console.WriteLine("Farvel!");
                            return;
                        default:
                            Console.WriteLine("Ugyldigt valg. Prøv igen.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl: {ex.Message}");
                }

                Console.Clear();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== Hovedmenu ===");
            Console.WriteLine($"Server: {_config.Server}");
            Console.WriteLine($"Bruger: {_config.Username}@{_config.Domain}");
            Console.WriteLine();
            Console.WriteLine("1. Vis alle grupper");
            Console.WriteLine("2. Vis alle brugere");
            Console.WriteLine("3. Vis grupper med medlemmer");
            Console.WriteLine("4. Søg efter brugere");
            Console.WriteLine("5. Søg efter grupper");
            Console.WriteLine("6. Opdater forbindelsesindstillinger");
            Console.WriteLine("7. Test forbindelse");
            Console.WriteLine("0. Afslut");
            Console.WriteLine();
            Console.Write("Vælg en option: ");
        }

        private void UpdateConfig()
        {
            Console.WriteLine("=== Opdater forbindelsesindstillinger ===");
            Console.WriteLine("Tryk Enter for at beholde nuværende værdi");
            Console.WriteLine();

            Console.Write($"Server ({_config.Server}): ");
            var server = Console.ReadLine();
            if (!string.IsNullOrEmpty(server))
                _config.Server = server;

            Console.Write($"Brugernavn ({_config.Username}): ");
            var username = Console.ReadLine();
            if (!string.IsNullOrEmpty(username))
                _config.Username = username;

            Console.Write($"Adgangskode: ");
            var password = Console.ReadLine();
            if (!string.IsNullOrEmpty(password))
                _config.Password = password;

            Console.Write($"Domæne ({_config.Domain}): ");
            var domain = Console.ReadLine();
            if (!string.IsNullOrEmpty(domain))
                _config.Domain = domain;

            Console.WriteLine("Indstillinger opdateret!");
        }

        private void TestConnection()
        {
            Console.WriteLine("=== Test Forbindelse til Active Directory ===");
            Console.WriteLine();
            
            // Vis forbindelsesoplysninger
            Console.WriteLine("Forbindelsesoplysninger:");
            Console.WriteLine($"  Server: {_config.Server}");
            Console.WriteLine($"  Domæne: {_config.Domain}");
            Console.WriteLine($"  Brugernavn: {_config.Username}");
            Console.WriteLine($"  Port: 389 (standard LDAP)");
            Console.WriteLine();
            
            Console.WriteLine("Forsøger at oprette forbindelse...");
            
            try
            {
                // Vis hvad der sker
                Console.WriteLine("  → Opretter NetworkCredential...");
                var credential = new NetworkCredential($"{_config.Username}@{_config.Domain}", _config.Password);
                
                Console.WriteLine("  → Opretter LdapConnection...");
                var connection = new LdapConnection(_config.Server)
                {
                    Credential = credential,
                    AuthType = AuthType.Negotiate
                };
                
                Console.WriteLine("  → Tester autentificering (Bind)...");
                connection.Bind();
                
                Console.WriteLine();
                Console.WriteLine("✅ Forbindelse succesfuld!");
                Console.WriteLine($"   Forbundet til: {_config.Server}");
                Console.WriteLine($"   Autentificeret som: {_config.Username}@{_config.Domain}");
                Console.WriteLine($"   Autentificeringstype: {connection.AuthType}");
                
                // Test en simpel søgning for at verificere at vi kan læse data
                Console.WriteLine();
                Console.WriteLine("Tester dataadgang...");
                var testSearchRequest = new SearchRequest(
                    $"DC={_config.Domain.Split('.')[0]},DC={_config.Domain.Split('.')[1]}",
                    "(objectClass=*)",
                    SearchScope.Base,
                    "objectClass"
                );
                
                var testResponse = (SearchResponse)connection.SendRequest(testSearchRequest);
                Console.WriteLine("  → Dataadgang verificeret!");
                Console.WriteLine($"   Base DN: {testResponse.Entries[0].DistinguishedName}");
                
                connection.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("❌ Forbindelse fejlede!");
                Console.WriteLine($"   Fejltype: {ex.GetType().Name}");
                Console.WriteLine($"   Fejlbesked: {ex.Message}");
                
                // Giv specifikke fejlforslag baseret på fejltypen
                if (ex.Message.Contains("timeout") || ex.Message.Contains("time-out"))
                {
                    Console.WriteLine();
                    Console.WriteLine("💡 Mulige løsninger:");
                    Console.WriteLine("   - Tjek at serveren er tilgængelig");
                    Console.WriteLine("   - Tjek netværksforbindelse");
                    Console.WriteLine("   - Tjek firewall indstillinger");
                }
                else if (ex.Message.Contains("authentication") || ex.Message.Contains("credentials"))
                {
                    Console.WriteLine();
                    Console.WriteLine("💡 Mulige løsninger:");
                    Console.WriteLine("   - Tjek brugernavn og adgangskode");
                    Console.WriteLine("   - Tjek at brugeren har adgang til AD");
                    Console.WriteLine("   - Tjek domænenavn");
                }
                else if (ex.Message.Contains("server") || ex.Message.Contains("host"))
                {
                    Console.WriteLine();
                    Console.WriteLine("💡 Mulige løsninger:");
                    Console.WriteLine("   - Tjek server IP/adresse");
                    Console.WriteLine("   - Tjek at LDAP service kører på serveren");
                    Console.WriteLine("   - Tjek port 389 er åben");
                }
            }
        }

        private LdapConnection GetConnection()
        {
            var credential = new NetworkCredential($"{_config.Username}@{_config.Domain}", _config.Password);
            var connection = new LdapConnection(_config.Server)
            {
                Credential = credential,
                AuthType = AuthType.Negotiate
            };

            connection.Bind(); // Test forbindelse
            return connection;
        }

        private void ShowAllGroups()
        {
            Console.WriteLine("=== Alle Grupper ===");
            var groups = GetAllGroups();
            
            if (groups.Count == 0)
            {
                Console.WriteLine("Ingen grupper fundet.");
                return;
            }

            Console.WriteLine($"Fundet {groups.Count} grupper:");
            Console.WriteLine();
            
            foreach (var group in groups.OrderBy(g => g.Name))
            {
                Console.WriteLine($"Navn: {group.Name}");
                Console.WriteLine($"Beskrivelse: {group.Description}");
                Console.WriteLine(new string('-', 50));
            }
        }

        private void ShowAllUsers()
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

        private void ShowGroupsWithMembers()
        {
            Console.WriteLine("=== Grupper med Medlemmer ===");
            var groups = GetAllGroupsWithMembers();
            
            if (groups.Count == 0)
            {
                Console.WriteLine("Ingen grupper fundet.");
                return;
            }

            Console.WriteLine($"Fundet {groups.Count} grupper:");
            Console.WriteLine();
            
            foreach (var group in groups.OrderBy(g => g.Name))
            {
                Console.WriteLine($"Gruppe: {group.Name}");
                Console.WriteLine($"Beskrivelse: {group.Description}");
                Console.WriteLine($"Medlemmer ({group.Members.Count}):");
                
                if (group.Members.Count > 0)
                {
                    foreach (var member in group.Members.OrderBy(m => m))
                    {
                        Console.WriteLine($"  - {member}");
                    }
                }
                else
                {
                    Console.WriteLine("  Ingen medlemmer");
                }
                
                Console.WriteLine(new string('-', 50));
            }
        }

        private void SearchUsers()
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

        private void SearchGroups()
        {
            Console.WriteLine("=== Søg efter Grupper ===");
            Console.Write("Indtast søgekriterie (navn eller beskrivelse): ");
            var searchTerm = Console.ReadLine();

            if (string.IsNullOrEmpty(searchTerm))
            {
                Console.WriteLine("Søgeterm kan ikke være tom.");
                return;
            }

            var groups = SearchGroups(searchTerm);
            
            if (groups.Count == 0)
            {
                Console.WriteLine("Ingen grupper fundet med det søgekriterie.");
                return;
            }

            Console.WriteLine($"Fundet {groups.Count} grupper:");
            Console.WriteLine();
            
            foreach (var group in groups.OrderBy(g => g.Name))
            {
                Console.WriteLine($"Navn: {group.Name}");
                Console.WriteLine($"Beskrivelse: {group.Description}");
                Console.WriteLine(new string('-', 50));
            }
        }

        // AD funktioner
        public List<ADGroup> GetAllGroups()
        {
            var groups = new List<ADGroup>();

            using (var connection = GetConnection())
            {
                var searchRequest = new SearchRequest(
                    $"DC={_config.Domain.Split('.')[0]},DC={_config.Domain.Split('.')[1]}",
                    "(objectClass=group)",
                    SearchScope.Subtree,
                    "cn",
                    "description"
                );

                try
                {
                    var response = (SearchResponse)connection.SendRequest(searchRequest);

                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        var group = new ADGroup
                        {
                            Name = entry.Attributes["cn"]?[0]?.ToString() ?? "N/A",
                            Description = entry.Attributes["description"]?[0]?.ToString() ?? "N/A"
                        };

                        groups.Add(group);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Fejl ved hentning af grupper: {ex.Message}");
                }
            }

            return groups;
        }

        public List<ADUser> GetAllUsers()
        {
            var users = new List<ADUser>();

            using (var connection = GetConnection())
            {
                var searchRequest = new SearchRequest(
                    $"DC={_config.Domain.Split('.')[0]},DC={_config.Domain.Split('.')[1]}",
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

        public List<ADGroup> GetAllGroupsWithMembers()
        {
            var groups = new List<ADGroup>();

            using (var connection = GetConnection())
            {
                var searchRequest = new SearchRequest(
                    $"DC={_config.Domain.Split('.')[0]},DC={_config.Domain.Split('.')[1]}",
                    "(objectClass=group)",
                    SearchScope.Subtree,
                    "cn",
                    "description",
                    "member"
                );

                try
                {
                    var response = (SearchResponse)connection.SendRequest(searchRequest);

                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        var group = new ADGroup
                        {
                            Name = entry.Attributes["cn"]?[0]?.ToString() ?? "N/A",
                            Description = entry.Attributes["description"]?[0]?.ToString() ?? "N/A"
                        };

                        // Hent medlemmer
                        if (entry.Attributes.Contains("member"))
                        {
                            foreach (string member in entry.Attributes["member"])
                            {
                                group.Members.Add(member);
                            }
                        }

                        groups.Add(group);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Fejl ved hentning af grupper med medlemmer: {ex.Message}");
                }
            }

            return groups;
        }

        public List<ADUser> SearchUsers(string searchTerm)
        {
            var users = new List<ADUser>();

            using (var connection = GetConnection())
            {
                var filter = $"(|(cn=*{searchTerm}*)(mail=*{searchTerm}*)(department=*{searchTerm}*)(title=*{searchTerm}*))";
                
                var searchRequest = new SearchRequest(
                    $"DC={_config.Domain.Split('.')[0]},DC={_config.Domain.Split('.')[1]}",
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

        public List<ADGroup> SearchGroups(string searchTerm)
        {
            var groups = new List<ADGroup>();

            using (var connection = GetConnection())
            {
                var filter = $"(|(cn=*{searchTerm}*)(description=*{searchTerm}*))";
                
                var searchRequest = new SearchRequest(
                    $"DC={_config.Domain.Split('.')[0]},DC={_config.Domain.Split('.')[1]}",
                    filter,
                    SearchScope.Subtree,
                    "cn",
                    "description"
                );

                try
                {
                    var response = (SearchResponse)connection.SendRequest(searchRequest);

                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        var group = new ADGroup
                        {
                            Name = entry.Attributes["cn"]?[0]?.ToString() ?? "N/A",
                            Description = entry.Attributes["description"]?[0]?.ToString() ?? "N/A"
                        };

                        groups.Add(group);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Fejl ved søgning efter grupper: {ex.Message}");
                }
            }

            return groups;
        }
    }
}
