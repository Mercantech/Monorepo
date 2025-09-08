using System;
using System.Collections.Generic;

namespace Opgaver
{
    public class Arrays
    {
        public static void Run()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Velkommen til opgaver omkring Arrays, List og Dictionary!");

            // Opgaverne herunder går igennem ting vi skal kunne med arrays, list og dictionary
            // Da I ikke har lært omkring loops og metoder endnu, er det ikke nødvendigt at bruge dem her
            // I må dog gerne bruge loops og metoder i opgaverne herunder

            Array1();
            Array2();
            Array3();
            List1();
            List2();
            List3();
            List4();
            List5();
            Dict1();
            Dict2();
            MiniProjektKlasseliste();
            MiniProjektIndkøbsliste();
        }

        public static void Array1()
        {
            Console.WriteLine("Opgave 1 (Array):");
            Console.WriteLine(
                "Lav et program som gemmer 5 fornavne som brugeren indtaster i et array."
            );
            // Lav opgaven herunder!
            string[] navne = new string[5]; // Array med plads til 5 navne
            
            // Få navne fra brugeren
            for (int i = 0; i < navne.Length; i++)
            {
                Console.Write($"Indtast navn {i + 1}: ");
                navne[i] = Console.ReadLine();
            }
            
            // Udskriv alle navne
            Console.WriteLine("\nDe indtastede navne er:");
            for (int i = 0; i < navne.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {navne[i]}");
            }
        }

        public static void Array2()
        {
            Console.WriteLine("Opgave 2 (Array):");
            Console.WriteLine(
                "Lav et program som gemmer 5 tal i et array og udskriver det største tal."
            );
            // Lav opgaven herunder!
            int[] tal = new int[5]; // Array med plads til 5 tal
            
            // Få tal fra brugeren
            for (int i = 0; i < tal.Length; i++)
            {
                Console.Write($"Indtast tal {i + 1}: ");
                tal[i] = int.Parse(Console.ReadLine());
            }
            
            // Find det største tal
            int størst = tal[0]; // Start med første tal som det største
            for (int i = 1; i < tal.Length; i++)
            {
                if (tal[i] > størst)
                {
                    størst = tal[i];
                }
            }
            
            Console.WriteLine($"Det største tal er: {størst}");
        }

        public static void Array3()
        {
            Console.WriteLine("Opgave 3 (Array):");
            Console.WriteLine(
                @"Lav et program som gemmer 5 bynavne i et array 
                og udskriver dem alle i omvendt rækkefølge."
            );
            // Lav opgaven herunder!
            string[] byer = new string[5]; // Array med plads til 5 bynavne
            
            // Få bynavne fra brugeren
            for (int i = 0; i < byer.Length; i++)
            {
                Console.Write($"Indtast by {i + 1}: ");
                byer[i] = Console.ReadLine();
            }
            
            // Udskriv byerne i omvendt rækkefølge
            Console.WriteLine("\nByerne i omvendt rækkefølge:");
            for (int i = byer.Length - 1; i >= 0; i--)
            {
                Console.WriteLine($"{byer.Length - i}. {byer[i]}");
            }
        }

        public static void List1()
        {
            Console.WriteLine("Opgave 1 (List):");
            Console.WriteLine(
                @"Lav et program som gemmer 5 fornavne 
                som brugeren indtaster i en liste."
            );
            // Lav opgaven herunder!
            List<string> navne = new List<string>(); // Dynamisk liste
            
            // Få 5 navne fra brugeren
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Indtast navn {i + 1}: ");
                string navn = Console.ReadLine();
                navne.Add(navn); // Tilføj navn til listen
            }
            
            // Udskriv alle navne
            Console.WriteLine("\nDe indtastede navne er:");
            for (int i = 0; i < navne.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {navne[i]}");
            }
        }

        public static void List2()
        {
            Console.WriteLine("Opgave 2 (List):");
            Console.WriteLine(
                @"Lav et program hvor brugeren kan blive ved med at indtaste 
                navne indtil de skriver 'stop'. Udskriv alle navnene til sidst."
            );
            // Lav opgaven herunder!
            List<string> navne = new List<string>();
            string input = "";
            
            Console.WriteLine("Indtast navne (skriv 'stop' for at afslutte):");
            
            // Fortsæt indtil brugeren skriver 'stop'
            while (input.ToLower() != "stop")
            {
                Console.Write("Indtast navn: ");
                input = Console.ReadLine();
                
                if (input.ToLower() != "stop")
                {
                    navne.Add(input);
                }
            }
            
            // Udskriv alle navne
            Console.WriteLine($"\nDu indtastede {navne.Count} navne:");
            foreach (string navn in navne)
            {
                Console.WriteLine($"- {navn}");
            }
        }

        public static void List3()
        {
            Console.WriteLine("Opgave 3 (List):");
            Console.WriteLine(
                @"Lav et program hvor brugeren indtaster 5 tal i en liste 
                og programmet udskriver gennemsnittet."
            );
            // Lav opgaven herunder!
            List<int> tal = new List<int>();
            
            // Få 5 tal fra brugeren
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"Indtast tal {i + 1}: ");
                int nummer = int.Parse(Console.ReadLine());
                tal.Add(nummer);
            }
            
            // Beregn gennemsnit
            int sum = 0;
            foreach (int nummer in tal)
            {
                sum += nummer;
            }
            
            double gennemsnit = (double)sum / tal.Count;
            Console.WriteLine($"Gennemsnittet af de 5 tal er: {gennemsnit:F2}");
        }

        public static void List4()
        {
            Console.WriteLine("Opgave 4 (List):");
            Console.WriteLine(
                @"Lav et program hvor brugeren indtaster navne på ting de skal købe, 
                og kan fjerne ting fra listen igen. Udskriv listen til sidst."
            );
            // Lav opgaven herunder!
            List<string> indkøbsliste = new List<string>();
            bool fortsæt = true;
            
            while (fortsæt)
            {
                Console.WriteLine("\nVælg en handling:");
                Console.WriteLine("1. Tilføj vare");
                Console.WriteLine("2. Fjern vare");
                Console.WriteLine("3. Vis liste");
                Console.WriteLine("4. Afslut");
                Console.Write("Dit valg: ");
                
                string valg = Console.ReadLine();
                
                switch (valg)
                {
                    case "1":
                        Console.Write("Indtast vare at tilføje: ");
                        string vare = Console.ReadLine();
                        indkøbsliste.Add(vare);
                        Console.WriteLine($"'{vare}' er tilføjet til listen.");
                        break;
                        
                    case "2":
                        if (indkøbsliste.Count > 0)
                        {
                            Console.WriteLine("Nuværende liste:");
                            for (int i = 0; i < indkøbsliste.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {indkøbsliste[i]}");
                            }
                            Console.Write("Indtast nummer på vare at fjerne: ");
                            int index = int.Parse(Console.ReadLine()) - 1;
                            
                            if (index >= 0 && index < indkøbsliste.Count)
                            {
                                string fjernet = indkøbsliste[index];
                                indkøbsliste.RemoveAt(index);
                                Console.WriteLine($"'{fjernet}' er fjernet fra listen.");
                            }
                            else
                            {
                                Console.WriteLine("Ugyldigt nummer!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Listen er tom!");
                        }
                        break;
                        
                    case "3":
                        Console.WriteLine($"\nIndkøbsliste ({indkøbsliste.Count} varer):");
                        foreach (string item in indkøbsliste)
                        {
                            Console.WriteLine($"- {item}");
                        }
                        break;
                        
                    case "4":
                        fortsæt = false;
                        break;
                        
                    default:
                        Console.WriteLine("Ugyldigt valg!");
                        break;
                }
            }
            
            Console.WriteLine("\nFinal indkøbsliste:");
            foreach (string item in indkøbsliste)
            {
                Console.WriteLine($"- {item}");
            }
        }

        public static void List5()
        {
            Console.WriteLine("Opgave 5 (List):");
            Console.WriteLine(
                @"Lav et program hvor brugeren indtaster navne på sine venner 
                i en liste og programmet udskriver hvor mange navne der starter med 'A'."
            );
            // Lav opgaven herunder!
            List<string> venner = new List<string>();
            string input = "";
            
            Console.WriteLine("Indtast navne på dine venner (skriv 'stop' for at afslutte):");
            
            // Få navne fra brugeren
            while (input.ToLower() != "stop")
            {
                Console.Write("Indtast navn: ");
                input = Console.ReadLine();
                
                if (input.ToLower() != "stop")
                {
                    venner.Add(input);
                }
            }
            
            // Tæl navne der starter med 'A'
            int antalMedA = 0;
            foreach (string navn in venner)
            {
                if (navn.ToUpper().StartsWith("A"))
                {
                    antalMedA++;
                }
            }
            
            Console.WriteLine($"\nDu har {venner.Count} venner i alt.");
            Console.WriteLine($"{antalMedA} af dine venner har navne der starter med 'A'.");
            
            // Vis navnene der starter med A
            if (antalMedA > 0)
            {
                Console.WriteLine("Navne der starter med 'A':");
                foreach (string navn in venner)
                {
                    if (navn.ToUpper().StartsWith("A"))
                    {
                        Console.WriteLine($"- {navn}");
                    }
                }
            }
        }

        public static void Dict1()
        {
            Console.WriteLine("Opgave 1 (Dictionary):");
            Console.WriteLine(
                @"Lav et program hvor du gemmer navne og alder på 3 personer 
                i en dictionary og udskriver dem alle."
            );
            // Lav opgaven herunder!
            // Husk syntaxen for Dictionary<type, type> navn = new Dictionary<type, type>();
            Dictionary<string, int> personer = new Dictionary<string, int>();
            
            // Få information om 3 personer
            for (int i = 1; i <= 3; i++)
            {
                Console.Write($"Indtast navn på person {i}: ");
                string navn = Console.ReadLine();
                
                Console.Write($"Indtast alder på {navn}: ");
                int alder = int.Parse(Console.ReadLine());
                
                personer.Add(navn, alder); // Tilføj til dictionary
            }
            
            // Udskriv alle personer
            Console.WriteLine("\nPersoner og deres alder:");
            foreach (KeyValuePair<string, int> person in personer)
            {
                Console.WriteLine($"{person.Key} er {person.Value} år gammel");
            }
        }

        public static void Dict2()
        {
            Console.WriteLine("Opgave 2 (Dictionary):");
            Console.WriteLine(
                @"Lav et program hvor brugeren kan indtaste et navn 
                og få alderen på personen ud fra dictionaryen fra før."
            );
            // Lav opgaven herunder!
            // Opret dictionary med nogle eksempel-personer
            Dictionary<string, int> personer = new Dictionary<string, int>
            {
                {"Anna", 25},
                {"Peter", 30},
                {"Maria", 22},
                {"Lars", 35}
            };
            
            Console.WriteLine("Tilgængelige personer:");
            foreach (string navn in personer.Keys)
            {
                Console.WriteLine($"- {navn}");
            }
            
            Console.Write("\nIndtast et navn for at få alderen: ");
            string søgtNavn = Console.ReadLine();
            
            // Tjek om navnet findes i dictionary
            if (personer.ContainsKey(søgtNavn))
            {
                int alder = personer[søgtNavn];
                Console.WriteLine($"{søgtNavn} er {alder} år gammel.");
            }
            else
            {
                Console.WriteLine($"Personen '{søgtNavn}' findes ikke i listen.");
            }
        }

        public static void MiniProjektKlasseliste()
        {
            Console.WriteLine("\nMini-projekt: Klasseliste (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine(
                "Lav et program, hvor brugeren indtaster navnene på alle elever i en klasse (fx 5 navne)."
            );
            Console.WriteLine(
                @"Gem navnene i en liste og udskriv hele klasselisten 
                  i konsollen."
            );
            // Lav opgaven herunder!
            List<string> klasseliste = new List<string>();
            
            Console.Write("Hvor mange elever er der i klassen? ");
            int antalElever = int.Parse(Console.ReadLine());
            
            // Få navne på alle elever
            for (int i = 1; i <= antalElever; i++)
            {
                Console.Write($"Indtast navn på elev {i}: ");
                string elevNavn = Console.ReadLine();
                klasseliste.Add(elevNavn);
            }
            
            // Sorter listen alfabetisk
            klasseliste.Sort();
            
            // Udskriv klasselisten
            Console.WriteLine($"\n=== KLASSELISTE ({antalElever} elever) ===");
            for (int i = 0; i < klasseliste.Count; i++)
            {
                Console.WriteLine($"{i + 1:D2}. {klasseliste[i]}");
            }
            
            // Ekstra statistik
            Console.WriteLine($"\nStatistik:");
            Console.WriteLine($"- Antal elever: {klasseliste.Count}");
            Console.WriteLine($"- Første elev alfabetisk: {klasseliste[0]}");
            Console.WriteLine($"- Sidste elev alfabetisk: {klasseliste[klasseliste.Count - 1]}");
        }

        public static void MiniProjektIndkøbsliste()
        {
            Console.WriteLine("\nMini-projekt: Indkøbsliste (skabelon)");
            Console.WriteLine("Opgave:");
            Console.WriteLine(
                @"Lav et program, hvor brugeren indtaster navnet på tre ting og deres pris, 
                de skal købe i supermarkedet."
            );
            Console.WriteLine(
                @"Gem tingene i et key-value par med navn og pris, 
                og udskriv en indkøbsliste med total pris til brugeren."
            );
            // Lav opgaven herunder!
            Dictionary<string, double> indkøbsliste = new Dictionary<string, double>();
            
            Console.Write("Hvor mange varer vil du tilføje? ");
            int antalVarer = int.Parse(Console.ReadLine());
            
            // Få information om hver vare
            for (int i = 1; i <= antalVarer; i++)
            {
                Console.Write($"Indtast navn på vare {i}: ");
                string vareNavn = Console.ReadLine();
                
                Console.Write($"Indtast pris på {vareNavn} (kr): ");
                double pris = double.Parse(Console.ReadLine());
                
                indkøbsliste.Add(vareNavn, pris);
            }
            
            // Udskriv indkøbslisten
            Console.WriteLine("\n=== INDKØBSLISTE ===");
            double totalPris = 0;
            int vareNummer = 1;
            
            foreach (KeyValuePair<string, double> vare in indkøbsliste)
            {
                Console.WriteLine($"{vareNummer:D2}. {vare.Key,-20} {vare.Value,8:F2} kr");
                totalPris += vare.Value;
                vareNummer++;
            }
            
            Console.WriteLine(new string('-', 35));
            Console.WriteLine($"{"TOTAL",-20} {totalPris,8:F2} kr");
            
            // Ekstra information
            Console.WriteLine($"\nSammendrag:");
            Console.WriteLine($"- Antal varer: {indkøbsliste.Count}");
            Console.WriteLine($"- Gennemsnitspris: {totalPris / indkøbsliste.Count:F2} kr");
            Console.WriteLine($"- Dyreste vare: {indkøbsliste.OrderByDescending(x => x.Value).First().Key} ({indkøbsliste.Values.Max():F2} kr)");
            Console.WriteLine($"- Billigste vare: {indkøbsliste.OrderBy(x => x.Value).First().Key} ({indkøbsliste.Values.Min():F2} kr)");
        }
    }
}
