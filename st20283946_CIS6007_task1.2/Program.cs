using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PDS2
{
    class Program
    {
        static void Main()
        {
            ToolInventory toolInventory = new ToolInventory();
            List<Tool> inventory = toolInventory.GenerateRandomInventory();

            // Get the number of threads from the user
            Console.Write("Enter the number of threads: ");
            int numThreads;
            while (!int.TryParse(Console.ReadLine(), out numThreads) || numThreads <= 0)
            {
                Console.Write("Invalid input. Please enter a positive integer: ");
            }

            Console.WriteLine("\n");
            Console.WriteLine($"Searching with {numThreads} threads...");

            ToolSearcher toolSearcher = new ToolSearcher();
            // Measure the search time
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<Tool> foundItems = toolSearcher.ParallelSearch(inventory, numThreads);
            stopwatch.Stop();

            // Display results
            DisplaySearchResults(foundItems);

            Console.WriteLine("\n");

            // Display search time
            Console.WriteLine($"Search time with {numThreads} threads: {stopwatch.ElapsedMilliseconds} ms\n");

            Console.WriteLine("Search completed.");
        }

        static void DisplaySearchResults(List<Tool> foundItems)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Found items:");
            foreach (var item in foundItems)
            {
                Console.WriteLine($"Barcode: {item.Barcode}, Type: {item.Type}");
            }
        }
    }
}
 

