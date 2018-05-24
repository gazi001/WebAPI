using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.Common.DotNetCode
{
    public class OrderHelper
    {
        public static object _lock = new object();
        public static int count = 1;

        public static string GetRandom1(string shopcode)
        {
            lock (_lock)
            {
                if (count >= 10000)
                {
                    count = 1;
                }
                var number = shopcode + DateTime.Now.ToString("yyMMddHHmmss") + count.ToString("0000");
                count++;
                return number;
            }
        }


        public static string GetRandom2(string shopcode)
        {
            lock (_lock)
            {
                return shopcode + DateTime.Now.Ticks;

            }
        }

        public static string GetRandom3(string shopcode)
        {
            lock (_lock)
            {
                Random ran = new Random();
                return shopcode + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ran.Next(1000, 9999).ToString();
            }
        }

        public string GetOrderNumber()
        {
            string Number = DateTime.Now.ToString("yyMMddHHmmss");
            return Number + Next(1000, 1).ToString();
        }
        private static int Next(int numSeeds, int length)
        {
            byte[] buffer = new byte[length];
            System.Security.Cryptography.RNGCryptoServiceProvider Gen = new System.Security.Cryptography.RNGCryptoServiceProvider();
            Gen.GetBytes(buffer);
            uint randomResult = 0x0;
            for (int i = 0; i < length; i++)
            {
                randomResult |= ((uint)buffer[i] << ((length - 1 - i) * 8));
            }
            return (int)(randomResult % numSeeds);
        }
    }
}
