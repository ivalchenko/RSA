using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Security.Cryptography;

namespace RSA
{
    public class RSA
    {
        private BigInteger p, q, m, n, e, d, fi;
        private BigInteger encrypt, decrypt;
        public BigInteger GetPublicKey {
            get
            {
                return e;
            }
        }
        public BigInteger GetPrivateKey
        {
            get
            {
                return d;
            }
        }
        public BigInteger GetEncryptText
        {
            get
            {
                return encrypt;
            }
        }
        public BigInteger GetDecryptText
        {
            get
            {
                return decrypt;
            }
        }

        public RSA(BigInteger p, BigInteger q, BigInteger m)
        {
            if(m % p != 0 && m % q != 0)
            {
                n = p * q;
                Console.WriteLine("'n' is {0}", n);
                Console.WriteLine("Lenght of 'n' is {0}", n.ToString().Length);

                fi = (p - 1) * (q - 1);
                Console.WriteLine("The 'fi' is {0}", fi);

                e = CreatePublicKey(n.ToString().Length);
                Console.WriteLine("'e' is {0}", e);

                d = Bezu(e, fi)[1];
                Console.WriteLine("'d' is {0}", d);

            } else
            {
                Console.WriteLine("Error! The number 'm' should not be divided by 'p' and 'q'.");
            }
        }

        private BigInteger CreatePublicKey(int lenght)
        {
            /*BigInteger p = 0;
            var rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[lenght / 3];*/

            long l = 0;

            do
            {
                Console.WriteLine("I'm in create random BigInteger loop.");
                /*rng.GetBytes(bytes);
                p = new BigInteger(bytes);*/
                l = LongRandom(1, (long)n, new Random());

                if (p < 0)
                    p = p * (-1);

            } while (BigInteger.GreatestCommonDivisor(p, fi) != 1);

            Console.WriteLine("Create random BigInteger loop is done! Yep!");

            return p;
        }

        private long LongRandom(long min, long max, Random rand)
        {
            long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)rand.Next((Int32)min, (Int32)max);
            return result;
        }

        public BigInteger[] Bezu(BigInteger a, BigInteger b)
        {
            BigInteger aTemp, bTemp, temp;
            List<BigInteger> IL = new List<BigInteger>();

            if(a > b)
            {
                aTemp = a;
                bTemp = b;
            } else
            {
                aTemp = b;
                bTemp = a;
            }
            
            while(aTemp % bTemp != 0)
            {
                // floor
                IL.Add(aTemp / bTemp);
                temp = aTemp % bTemp;
                aTemp = bTemp;
                bTemp = temp;
            }

            BigInteger[] C1 = { 1, 0 };
            BigInteger[] C2 = { 0, 1 };

            for(int i = 0; i < IL.Count; i++)
            {
                BigInteger[] C3 = { C1[0] - (C2[0] * IL[i]), C1[1] - (C2[1] * IL[i]) };

                C1[0] = C2[0];
                C1[1] = C2[1];

                C2[0] = C3[0];
                C2[1] = C3[1];
            }

            return C2;
        }

        public BigInteger Crypt()
        {
            return (m ^ e) % n;
        }

        public BigInteger Decrypt()
        {
            return (Crypt() ^ d) % n;
        }
    }
}
