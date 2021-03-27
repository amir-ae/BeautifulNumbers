using System;

namespace BeautifulNumbers.Infrastructure
{
    public class RandomGenerator
    {
        // Instantiate random number generator.
        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
