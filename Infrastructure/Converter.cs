using BeautifulNumbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautifulNumbers.Infrastructure
{
    public static class Converter
    {
        public static char[] LongToChars(ulong value, ulong radix)
        {
            string result = string.Empty; ;
            string rChars = Constants.characters.Substring(0, Convert.ToInt32(radix));
            do
            {
                result = rChars[Convert.ToInt32(value % radix)] + result;
                value = value / radix;
            }
            while (value > 0);

            result = result.PadLeft(Convert.ToInt32(radix), '0');

            char[] number = new char[result.Length];

            for (int i = 0; i < result.Length; i++)
            {
                number[i] = result[i];
            }

            return number;
        }

        public static ulong CharsToLong(char[] number, int radix)
        {
            double value = 0;

            for (int p = 0; p < radix; p++)
            {
                value += Constants.characters.IndexOf(number[radix - p - 1]) * Math.Pow(radix, p);
            }

            return Convert.ToUInt64(value);
        }

        public static char[] ConcateChars(char[] x, char[] y, char[] z)
        {
            char[] result = new List<char>().Concat(x).Concat(y).Concat(z).ToArray();

            return result;
        }

        public static char[] ConcateChars(char x, int n)
        {
            char[] result = new List<char>().ToArray();
            char[] toAdd = new[] { x };

            for (int i = 0; i < n; i++)
            {
                result = result.Concat(toAdd).ToArray();
            }

            return result;
        }
    }
}
