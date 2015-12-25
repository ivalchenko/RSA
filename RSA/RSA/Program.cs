using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger p = BigInteger.Parse("46709458300467424687246424782695622225358816696393333137964351534079003841714485552893464466562695719");
            BigInteger q = BigInteger.Parse("79768913828913280865094151581626658090495579634401750999257378388908476522178827136234166270096152671");

            RSA rsa = new RSA(3, 11, 14);
            //Console.WriteLine("The GCD of {0} and {1} is qeuals {2}", p, q, BigInteger.GreatestCommonDivisor(p, q));

            Console.WriteLine("\n\nTap to continue...");
            Console.ReadKey(true);
        }
    }
}
