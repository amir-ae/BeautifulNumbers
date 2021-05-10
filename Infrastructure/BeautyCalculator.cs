using System;
using System.Collections.Generic;
using BeautifulNumbers.Models;

namespace BeautifulNumbers.Infrastructure
{
    public class BeautyCalculator
    {
        public ulong RadixBeauty(int radix, int extent)
        {
            ulong totalResult = 0;

            // create a dictionary to store calculated result values, so it won't be unnecessarily recalculated
            Dictionary<int, ulong> valResults = new Dictionary<int, ulong>();

            // create a number zero using the selected radix and divide it into left, middle, and right parts
            Number number = new Number(0, radix, extent);
            var (left, middle, right) = number;

            do
            {
                int refValue = Hash(left);              // calculate the left sum value and take it as a reference
                if (valResults.ContainsKey(refValue))   // check dictionary stored values for the reference key
                {
                    totalResult += valResults[refValue];    // add result value to total result
                }
                else
                {
                    ulong result = CalculateResult(refValue, radix, extent);  // calculate result value
                    valResults.Add(refValue, result);       // add result value to the dictionary
                    totalResult += result;                  // add result value to total result
                }
                left = Increment(left, radix);              // increment left part of the number
            }
            while (Hash(left) != 0);

            return totalResult;
        }

        private ulong CalculateResult(int refValue, int radix, int extent, char[] num = null)
        {
            char[] number = num ?? Converter.ConcateChars('0', extent);
            ulong result = 0;
            int currentValue = Hash(number);

            do
            {
                while (refValue > currentValue)
                {
                    number = Increment(number, radix);              // increment the least significant digit
                    currentValue = Hash(number);
                }
                if (currentValue == refValue)
                {
                    result++;                                       // increment result
                }

                for (int position = number.Length - 2; refValue == currentValue && position >= 0; position--)
                {
                    number = Increment(number, radix, position);    // increment the next digit
                    currentValue = Hash(number);

                    if (currentValue == refValue)
                    {
                        result++;                                   // increment result and continue
                    }
                }
            }
            while (refValue > currentValue && currentValue != 0);   // repeat

            return result;
        }

        // calculate the sum of number digits
        private int Hash(char[] number)
        {
            int value = 0;

            foreach (char k in number)
            {
                value += Constants.characters.IndexOf(k);
            }

            return value;
        }

        private char[] Increment(char[] num, int radix, int? pos = null)
        {
            char[] number = new char[num.Length];
            Array.Copy(num, number, num.Length);
            int position = pos ?? number.Length - 1;

            int index = Constants.characters.IndexOf(number[position]);

            if (index == radix - 1)
            {
                if (position > 0)
                {
                    number = Increment(number, radix, position - 1);
                }
                else
                {
                    number = Reset(number);
                }
            }
            else
            {
                number[position] = Constants.characters[++index];
                number = Reset(number, position + 1);
            }

            return number;
        }

        private char[] Reset(char[] num, int position = 0)
        {
            char[] number = new char[num.Length];
            Array.Copy(num, number, num.Length);

            while (position < number.Length)
            {
                number[position] = '0';
                position++;
            }

            return number;
        }

        private Number GenerateRandom(int radix, int extent)
        {
            Number number;

            do
            {
                var generator = new RandomGenerator();
                ulong randomNumber = Convert.ToUInt64(generator.RandomNumber(0, int.MaxValue));
                number = new Number(randomNumber, radix, extent);
            }

            while (NonZeroes(number.Right) < extent / 2);

            return number;
        }

        private int NonZeroes(char[] number)
        {
            int value = 0;

            foreach (char k in number)
            {
                if (k != '0')
                {
                    value++;
                }
            }

            return value;
        }

        public Number GetBeautifulNumber(int radix, int extent)
        {
            Number n1 = GenerateRandom(radix, extent);
            Number n2 = GenerateRandom(radix, extent);
            Number n3 = GenerateRandom(radix, extent);
            Number number = new Number(n1.Right, n2.Middle, n3.Right);

            var (left, middle, right) = number;

            do
            {
                int refValue = Hash(left);
                int currentValue = Hash(right);

                do
                {
                    while (refValue > currentValue)
                    {
                        right = Increment(right, radix);                    // increment the least significant digit
                        currentValue = Hash(right);
                    }
                    if (refValue == currentValue)
                    {
                        return new Number(left, middle, right);
                    }

                    for (int position = right.Length - 2; refValue == currentValue && position >= 0; position--)
                    {
                        right = Increment(right, radix, position);             // increment the next digit
                        currentValue = Hash(right);

                        if (refValue == currentValue)
                        {
                            return new Number(left, middle, right);
                        }
                    }
                }
                while (refValue > currentValue && currentValue != 0);
                left = Increment(left, radix);
                right = Reset(right);
            }
            while (Hash(left) != 0);

            return new Number(0, radix, extent);
        }
    }
}
