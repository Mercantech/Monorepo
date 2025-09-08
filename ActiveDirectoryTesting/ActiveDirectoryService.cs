using System.DirectoryServices.Protocols;
using System.Net;

namespace ActiveDirectoryTesting
{
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
        public string Server { get; set; } = "10.133.71.112";
        public string Username { get; set; } = "Admin";
        public string Password { get; set; } = "Cisco1234!";
        public string Domain { get; set; } = "demo.local";
    }

    // Partiel klasse for Active Directory Service
    public partial class ActiveDirectoryService
    {
        private ADConfig _config;

        public ActiveDirectoryService()
        {
            _config = new ADConfig();
        }

        public ActiveDirectoryService(ADConfig config)
        {
            _config = config;
        }

        // Public properties for at få adgang til konfiguration
        public ADConfig Config => _config;
        public string Server => _config.Server;
        public string Username => _config.Username;
        public string Domain => _config.Domain;

        // Metode til at opdatere konfiguration
        public void UpdateConfig(ADConfig newConfig)
        {
            _config = newConfig;
        }

        // Metode til at opdatere individuelle konfigurationsværdier
        public void UpdateConfig(string? server = null, string? username = null, string? password = null, string? domain = null)
        {
            if (!string.IsNullOrEmpty(server))
                _config.Server = server;
            if (!string.IsNullOrEmpty(username))
                _config.Username = username;
            if (!string.IsNullOrEmpty(password))
                _config.Password = password;
            if (!string.IsNullOrEmpty(domain))
                _config.Domain = domain;
        }
    }
}
