using System;
using System.Diagnostics;

namespace PDS
{
    class Program
    {
        static void Main(string[] args)
        {
            int lowerBound = 1;
            int upperBound = 100000;

            int[] randomIntegers = GenerateRandomIntegers(lowerBound, upperBound, 100000);

            // Get the number of threads from the user
            Console.Write("Enter the number of threads: ");
            int numThreads;
            while (!int.TryParse(Console.ReadLine(), out numThreads) || numThreads <= 0)
            {
                Console.Write("Invalid input. Please enter a positive integer: ");
            }

            Console.WriteLine($"Searching with {numThreads} threads...");

            Stopwatch stopwatch = Stopwatch.StartNew();
            Algorithm.ParallelBubbleSort(randomIntegers, numThreads);
            stopwatch.Stop();

            PrintArray(randomIntegers);

            Console.WriteLine("\n");
            // Display search time
            Console.WriteLine($"Search time with {numThreads} threads: {stopwatch.ElapsedMilliseconds} ms\n");

            Console.WriteLine("Search completed.");
        }

        static int[] GenerateRandomIntegers(int lowerBound, int upperBound, int arrayLength)
        {
            Random random = new Random();
            int[] randomIntegers = new int[arrayLength];

            for (int i = 0; i < randomIntegers.Length; i++)
            {
                randomIntegers[i] = random.Next(lowerBound, upperBound + 1);
            }

            return randomIntegers;
        }

        static void PrintArray(int[] arr)
        {
            Console.WriteLine("Sorted array:");
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}
