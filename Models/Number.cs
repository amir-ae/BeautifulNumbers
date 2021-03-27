using BeautifulNumbers.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeautifulNumbers.Models
{
    public class Number
    {
        public char[] NumberText { get; set; }
        public ulong NumberValue { get; set; }
        public int Radix { get; set; }
        public int Extent { get; set; }
        public char[] Left { get; set; }
        public char[] Middle { get; set; }
        public char[] Right { get; set; }

        public Number(char[] numberText, int extent)
        {
            Radix = NumberText.Length;
            NumberText = numberText;
            NumberValue = Converter.CharsToLong(NumberText, Radix);
            Extent = extent;
            Left = NumberText.Take(extent).ToArray();
            Middle = NumberText.Skip(extent).Take(Radix - 2 * extent).ToArray();
            Right = NumberText.Skip(Radix - extent).Take(extent).ToArray();
        }

        public Number(ulong numberValue, int radix, int extent)
        {
            Radix = radix;
            NumberText = Converter.LongToChars(Convert.ToUInt64(numberValue), Convert.ToUInt64(Radix));
            NumberValue = numberValue;
            Extent = extent;
            Left = NumberText.Take(extent).ToArray();
            Middle = NumberText.Skip(extent).Take(Radix - 2 * extent).ToArray();
            Right = NumberText.Skip(Radix - extent).Take(extent).ToArray();
        }

        public Number(char[] left, char[] middle, char[] right)
        {
            NumberText = new List<char>().Concat(left).Concat(middle).Concat(right).ToArray();
            Radix = NumberText.Length;
            Extent = left.Length;
            NumberValue = Converter.CharsToLong(NumberText, Radix);
            Left = left;
            Middle = middle;
            Right = right;
        }

        public void Deconstruct(out char[] l, out char[] m, out char[] r)
        {
            l = Left;
            m = Middle;
            r = Right;
        }
    }
}
