using System.DirectoryServices.Protocols;

namespace Enterprice
{
    public class GroupADService
    {
        public static List<ADGroup> GetAllGroups(Action<string>? progressCallback = null)
        {
            // Opret en tom liste til at gemme alle AD grupper
            var groups = new List<ADGroup>();
            var message = "Starter hentning af AD grupper...";
            Console.WriteLine(message);
            progressCallback?.Invoke(message);

            // Opret forbindelse til Active Directory
            using (var connection = ADService.Connect())
            {
                message = "Forbindelse oprettet. Forbereder søgeforespørgsel efter grupper.";
                Console.WriteLine(message);
                progressCallback?.Invoke(message);
                // Definer søgningen:
                // - Hvor skal vi søge: i "mags.local" domænet
                // - Hvad søger vi efter: alle objekter af typen "group"
                // - Hvilke informationer vil vi have: 
                // - navn (cn) og beskrivelse
                var searchRequest = new SearchRequest(
                    "DC=mags,DC=local", // Søg i dette domæne
                    "(objectClass=group)", // Find alle grupper
                    SearchScope.Subtree, // Søg i hele domænet
                    "cn", // Gruppens navn
                    "description" // Gruppens beskrivelse

                );

                try
                {
                    // Udfør søgningen
                    var response = (SearchResponse)connection.SendRequest(searchRequest);
                    message = $"Søgningen returnerede {response.Entries.Count} grupper.";
                    Console.WriteLine(message);
                    progressCallback?.Invoke(message);

                    // For hver gruppe vi finder
                    foreach (SearchResultEntry gruppe in response.Entries)
                    {
                        // Opret et nyt ADGroup objekt med informationerne
                        var nyGruppe = new ADGroup
                        {
                            // Hvis værdien ikke findes, brug "N/A" som standard
                            Name = gruppe.Attributes["cn"]?[0]?.ToString() ?? "N/A",
                            Description = gruppe.Attributes["description"]?[0]?.ToString() ?? "N/A"
                        };

                        // Tilføj gruppen til vores liste
                        groups.Add(nyGruppe);
                        message = $"Tilføjet gruppe: {nyGruppe.Name} - {nyGruppe.Description}";
                        Console.WriteLine(message);
                        progressCallback?.Invoke(message);
                    }

                    message = "Alle grupper er nu hentet og tilføjet til listen.";
                    Console.WriteLine(message);
                    progressCallback?.Invoke(message);
                }
                catch (Exception ex)
                {
                    // Hvis noget går galt, fortæl hvad der skete
                    Console.WriteLine("Der opstod en fejl under hentning af grupper.");
                    throw new Exception($@"Der skete en fejl ved hentning af grupper:
	                    {ex.Message}");
                }
            }

            // Send alle de fundne grupper tilbage
            message = $"Returnerer {groups.Count} grupper til kaldende kode.";
            Console.WriteLine(message);
            progressCallback?.Invoke(message);
            return groups;
        }
    }

    public class ADGroup
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}