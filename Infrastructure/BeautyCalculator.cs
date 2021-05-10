using System;
using System.Collections.Generic;
using BeautifulNumbers.Models;

namespace BeautifulNumbers.Infrastructure
{
    public class BeautyCalculator
    {
        public ulong RadixBeauty(int radix, int extent)
        {
            // create a dictionary to store calculated result values
            Dictionary<int, ulong> valResults = new Dictionary<int, ulong>();

            int refValue, maxRefValue = (radix - 1) * extent;

            for (refValue = 0; refValue <= maxRefValue; refValue++)
            {
                valResults.Add(refValue, 0);        // initialize the dictionary             
            }

            // create a number zero using the selected radix and divide it into left, middle, and right parts
            Number number = new Number(0, radix, extent);
            var (num, middle, right) = number;

            do
            {
                refValue = Hash(num);
                valResults[refValue]++;             // populate the dictionary
                num = Increment(num, radix);
            }
            while (refValue < maxRefValue);

            ulong totalResult = 0;                  // initialize total result

            // define additional variants from the middle part
            ulong variants = Convert.ToUInt64(Math.Pow(radix, middle.Length));   
            
            foreach (var kvp in valResults)        
            {
                // define results from the left and right parts
                ulong results = Convert.ToUInt64(Math.Pow(kvp.Value, 2));

                // add beautiful numbers to total result
                totalResult += results * variants;
            }

            return totalResult;
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
            char[] number = new char[num.Length];       // create a new array
            Array.Copy(num, number, num.Length);        // copy content
            int position = pos ?? number.Length - 1;    // define digit position to increment

            int index = Constants.characters.IndexOf(number[position]);     // get digit character at position

            if (index == radix - 1)                 // if it is the last character in number system
            {
                if (position > 0)                       // if it is not the most significant digit
                {
                    number = Increment(number, radix, position - 1);    // increment the next digit
                }
                else
                {
                    number = Reset(number);             // else reset the whole number
                }
            }
            else                                    // if the character can be incremented
            {
                number[position] = Constants.characters[++index];       // incremented the character in position
                number = Reset(number, position + 1);                   // reset the following digits
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
