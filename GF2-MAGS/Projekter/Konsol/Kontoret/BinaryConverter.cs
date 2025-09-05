namespace Kontoret
{
    public class BinaryConverter
    {
        public void Start()
        {
            bool run = true;
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Binærkodeomformer");
                Console.WriteLine("Vælg en konvertering:");
                Console.WriteLine("1. Decimal til Binær");
                Console.WriteLine("2. Binær til Decimal");
                Console.WriteLine("3. Decimal til Hexadecimal");
                Console.WriteLine("4. Hexadecimal til Decimal");
                Console.WriteLine("5. Binær til Hexadecimal");
                Console.WriteLine("6. Hexadecimal til Binær");
                Console.WriteLine("0. Tilbage til menu");
                Console.Write("Indtast valg: ");
                string valg = Console.ReadLine();
                Console.WriteLine();

                switch (valg)
                {
                    case "1":
                        DecimalToBinary();
                        break;
                    case "2":
                        BinaryToDecimal();
                        break;
                    case "3":
                        DecimalToHex();
                        break;
                    case "4":
                        HexToDecimal();
                        break;
                    case "5":
                        BinaryToHex();
                        break;
                    case "6":
                        HexToBinary();
                        break;
                    case "0":
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg. Prøv igen.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Decimal til Binær (uden Convert)
        private string DecimalToBinaryStr(int dec)
        {
            if (dec == 0) return "0";
            string result = "";
            int n = dec;
            while (n > 0)
            {
                result = (n % 2) + result;
                n /= 2;
            }
            return result;
        }

        // Binær til Decimal (uden Convert)
        private int BinaryToDecimalInt(string bin)
        {
            int result = 0;
            foreach (char c in bin)
            {
                if (c != '0' && c != '1') throw new Exception();
                result = result * 2 + (c - '0');
            }
            return result;
        }

        // Decimal til Hexadecimal (uden ToString("X"))
        private string DecimalToHexStr(int dec)
        {
            if (dec == 0) return "0";
            string hexChars = "0123456789ABCDEF";
            string result = "";
            int n = dec;
            while (n > 0)
            {
                result = hexChars[n % 16] + result;
                n /= 16;
            }
            return result;
        }

        // Hexadecimal til Decimal (uden Convert)
        private  int HexToDecimalInt(string hex)
        {
            int result = 0;
            string hexChars = "0123456789ABCDEF";
            hex = hex.ToUpper();
            foreach (char c in hex)
            {
                int val = hexChars.IndexOf(c);
                if (val == -1) throw new Exception();
                result = result * 16 + val;
            }
            return result;
        }

        // Binær til Hexadecimal (uden Convert)
        private string BinaryToHexStr(string bin)
        {
            int dec = BinaryToDecimalInt(bin);
            return DecimalToHexStr(dec);
        }

        // Hexadecimal til Binær (uden Convert)
        private string HexToBinaryStr(string hex)
        {
            int dec = HexToDecimalInt(hex);
            return DecimalToBinaryStr(dec);
        }

        // Opdaterede menu-metoder:
        private void DecimalToBinary()
        {
            Console.Write("Indtast et decimaltal (fx 42): ");
            if (int.TryParse(Console.ReadLine(), out int dec))
            {
                string bin = DecimalToBinaryStr(dec);
                Console.WriteLine($"Binær: {bin}");
            }
            else
            {
                Console.WriteLine("Ugyldigt decimaltal.");
            }
            Pause();
        }

        private void BinaryToDecimal()
        {
            Console.Write("Indtast et binært tal (fx 101010): ");
            string bin = Console.ReadLine();
            try
            {
                int dec = BinaryToDecimalInt(bin);
                Console.WriteLine($"Decimal: {dec}");
            }
            catch
            {
                Console.WriteLine("Ugyldigt binært tal.");
            }
            Pause();
        }

        private void DecimalToHex()
        {
            Console.Write("Indtast et decimaltal (fx 42): ");
            if (int.TryParse(Console.ReadLine(), out int dec))
            {
                string hex = DecimalToHexStr(dec);
                Console.WriteLine($"Hexadecimal: {hex}");
            }
            else
            {
                Console.WriteLine("Ugyldigt decimaltal.");
            }
            Pause();
        }

        private void HexToDecimal()
        {
            Console.Write("Indtast et hex-tal (fx 2A): ");
            string hex = Console.ReadLine();
            try
            {
                int dec = HexToDecimalInt(hex);
                Console.WriteLine($"Decimal: {dec}");
            }
            catch
            {
                Console.WriteLine("Ugyldigt hex-tal.");
            }
            Pause();
        }

        private void BinaryToHex()
        {
            Console.Write("Indtast et binært tal (fx 101010): ");
            string bin = Console.ReadLine();
            try
            {
                string hex = BinaryToHexStr(bin);
                Console.WriteLine($"Hexadecimal: {hex}");
            }
            catch
            {
                Console.WriteLine("Ugyldigt binært tal.");
            }
            Pause();
        }

        private void HexToBinary()
        {
            Console.Write("Indtast et hex-tal (fx 2A): ");
            string hex = Console.ReadLine();
            try
            {
                string bin = HexToBinaryStr(hex);
                Console.WriteLine($"Binær: {bin}");
            }
            catch
            {
                Console.WriteLine("Ugyldigt hex-tal.");
            }
            Pause();
        }

        // Pausemetode for at vente på brugerinput
        private void Pause()
        {
            Console.WriteLine("\nTryk på en tast for at fortsætte...");
            Console.ReadKey();
        }
    }
}
