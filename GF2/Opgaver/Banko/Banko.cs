using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgaver
{
    public static class Banko
    {
        public static void Run()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 1000);
            Console.WriteLine(randomNumber);
        }
    }
}
