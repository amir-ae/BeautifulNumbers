using System.Collections.Generic;

namespace BeautifulNumbers.Models
{
    public static class Constants
    {
        public static readonly Dictionary<int, string> numSystems = new Dictionary<int, string> {
            { 4, "Quaternary" },
            { 5, "Quinary" },
            { 6, "Senary" },
            { 7, "Septenary" },
            { 8, "Octal" },
            { 9, "Nonary" },
            { 10, "Decimal" },
            { 11, "Undecimal" },
            { 12, "Duodecimal" },
            { 13, "Tridecimal" },
            { 14, "Tetradecimal" },
            { 15, "Pentadecimal" },
            { 16, "Hexadecimal" }
        };

        public static readonly string characters = new string(new char[] { '0','1','2','3','4','5','6','7','8','9',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'});
    }
}
