namespace Kontoret
{
    public class BinaryConverter
    {
        public void Start()
        {
            Console.WriteLine("Skriv en octet ind - altså et tal fra 0-255");
            Console.ReadLine();
            int bit1 = 0;
            int num = 173;
            if (128 > num)
            {
                bit1 = 0;
            }
            else
            {
                bit1 = 1;
                num = num - 128;
            }
            if (64 > num)
            {
                bit1 = 0;
            }
            else
            {
                bit1 = 1;
                num = num - 64;
            }
            if (128 > num)
            {
                bit1 = 0;
            }
            else
            {
                bit1 = 1;
                num = num - 128;
            }
            Console.WriteLine("Binærkodeomformer er ikke implementeret endnu.");
            Console.ReadKey();
        }
    }
}
