using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Konsol.Enterprice
{
    public class ADService
    {
        private readonly string _server;
        private readonly string _username;
        private readonly string _password;
        private readonly string _domain;
        private readonly string _adminUsername;
        private readonly string _adminPassword;

        public ADService(
            string server,
            string username,
            string password,
            string domain,
            string adminUsername,
            string adminPassword
        )
        {
            _server = server;
            _username = username;
            _password = password;
            _domain = domain;
            _adminUsername = adminUsername;
            _adminPassword = adminPassword;
        }

        public static ADService FromConfigFile(string path = "appsettings.json")
        {
            try
            {
                var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path));
                if (config?.AD == null || config.ADAdmin == null)
                    throw new Exception("AD eller ADAdmin konfiguration mangler i filen.");
                return new ADService(
                    config.AD.Server,
                    config.AD.Username,
                    config.AD.Password,
                    config.AD.Domain,
                    config.ADAdmin.Username,
                    config.ADAdmin.Password
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved indlæsning af konfiguration: {ex.Message}");
                throw;
            }
        }

        public void Start()
        {
            var status = GetServerStatus();
            Console.WriteLine(status);
        }

        public LdapConnection Connect(bool useAdmin = false)
        {
            try
            {
                var username = useAdmin ? _adminUsername : _username;
                var password = useAdmin ? _adminPassword : _password;
                var credential = new NetworkCredential($"{username}@{_domain}", password);
                var connection = new LdapConnection(_server)
                {
                    Credential = credential,
                    AuthType = AuthType.Negotiate,
                };
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved oprettelse af forbindelse: {ex.Message}");
                return null;
            }
        }

        public string GetServerStatus()
        {
            using (var connection = Connect())
            {
                if (connection == null)
                    return "Ikke kørende - kunne ikke oprette forbindelse.";
                try
                {
                    connection.Bind();
                    return "Kørende";
                }
                catch (LdapException ex)
                {
                    return $"Ikke kørende - LDAP fejl: {ex.Message}";
                }
                catch (Exception ex)
                {
                    return $"Ikke kørende - {ex.Message}";
                }
            }
        }

        // Hent alle brugere fra AD
        public List<string> GetAllUsers()
        {
            var users = new List<string>();
            using (var connection = Connect())
            {
                if (connection == null)
                    return users;
                try
                {
                    connection.Bind();
                    var searchRequest = new SearchRequest(
                        $"DC={_domain.Replace(".", ",DC=")}",
                        "(objectClass=user)",
                        SearchScope.Subtree,
                        "sAMAccountName",
                        "displayName",
                        "mail"
                    );
                    var response = (SearchResponse)connection.SendRequest(searchRequest);
                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        var name =
                            entry.Attributes["displayName"]?.GetValues(typeof(string))[0] as string
                            ?? "";
                        var user =
                            entry.Attributes["sAMAccountName"]?.GetValues(typeof(string))[0]
                                as string
                            ?? "";
                        var mail =
                            entry.Attributes["mail"]?.GetValues(typeof(string))[0] as string ?? "";
                        users.Add($"{name} ({user}) - {mail}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl ved hentning af brugere: {ex.Message}");
                }
            }
            return users;
        }

        // Hent alle grupper fra AD
        public List<string> GetAllGroups()
        {
            var groups = new List<string>();
            using (var connection = Connect())
            {
                if (connection == null)
                    return groups;
                try
                {
                    connection.Bind();
                    var searchRequest = new SearchRequest(
                        $"DC={_domain.Replace(".", ",DC=")}",
                        "(objectClass=group)",
                        SearchScope.Subtree,
                        "cn"
                    );
                    var response = (SearchResponse)connection.SendRequest(searchRequest);
                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        var group =
                            entry.Attributes["cn"]?.GetValues(typeof(string))[0] as string ?? "";
                        groups.Add(group);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl ved hentning af grupper: {ex.Message}");
                }
            }
            return groups;
        }

        public List<string> GetGroupMembers(string groupName)
        {
            var members = new List<string>();
            using (var connection = Connect())
            {
                if (connection == null)
                    return members;
                try
                {
                    connection.Bind();
                    var searchRequest = new SearchRequest(
                        $"DC={_domain.Replace(".", ",DC=")}",
                        $"(&(objectClass=group)(cn={groupName}))",
                        SearchScope.Subtree,
                        "member"
                    );
                    var response = (SearchResponse)connection.SendRequest(searchRequest);
                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        if (entry.Attributes["member"] != null)
                        {
                            foreach (
                                var member in entry.Attributes["member"].GetValues(typeof(string))
                            )
                            {
                                members.Add(member.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl ved hentning af gruppemedlemmer: {ex.Message}");
                }
            }
            return members;
        }

        public Dictionary<string, List<string>> GetAllGroupsWithMembers()
        {
            var result = new Dictionary<string, List<string>>();
            using (var connection = Connect())
            {
                if (connection == null)
                    return result;
                try
                {
                    connection.Bind();
                    var searchRequest = new SearchRequest(
                        $"DC={_domain.Replace(".", ",DC=")}",
                        "(objectClass=group)",
                        SearchScope.Subtree,
                        "cn",
                        "member"
                    );
                    var response = (SearchResponse)connection.SendRequest(searchRequest);
                    foreach (SearchResultEntry entry in response.Entries)
                    {
                        var group =
                            entry.Attributes["cn"]?.GetValues(typeof(string))[0] as string ?? "";
                        var members = new List<string>();
                        if (entry.Attributes["member"] != null)
                        {
                            foreach (
                                var member in entry.Attributes["member"].GetValues(typeof(string))
                            )
                            {
                                // Vis kun navnet (første CN=)
                                var memberStr = member.ToString();
                                var cn = memberStr.StartsWith("CN=")
                                    ? memberStr.Split(',')[0].Substring(3)
                                    : memberStr;
                                members.Add(cn);
                            }
                        }
                        result[group] = members;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl ved hentning af grupper/medlemmer: {ex.Message}");
                }
            }
            return result;
        }
    }

    public record Config(ADConfig AD, ADAdminConfig ADAdmin);

    public record ADConfig(string Server, string Username, string Password, string Domain);

    public record ADAdminConfig(string Username, string Password);
}
