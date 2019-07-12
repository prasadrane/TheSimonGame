using System;

namespace Simon.Models
{
    public class SimonGame
    {
        private static Random rand = new Random();

        public static int[] GeneratePattern()
        {
            int[] SimonPattern = new int[99];
            for (int i = 0; i < SimonPattern.Length; i++)
            {
                SimonPattern[i] = rand.Next(1, 5);
            }

            return SimonPattern;
        }
    }
}
