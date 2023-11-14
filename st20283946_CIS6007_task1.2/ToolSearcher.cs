using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PDS2
{
    class ToolSearcher
    {
        private const int ITEMS_OF_TYPE_1 = 30;
        private const int ITEMS_OF_TYPE_7 = 15;
        private const int ITEMS_OF_TYPE_10 = 8;

        public List<Tool> ParallelSearch(List<Tool> inventory, int numThreads)
        {
            List<Task<List<Tool>>> tasks = new List<Task<List<Tool>>>();

            // Use a CountdownEvent to signal when all tasks are completed
            CountdownEvent countdownEvent = new CountdownEvent(numThreads);

            // Divide the inventory into chunks based on the number of threads
            int chunkSize = inventory.Count / numThreads;

            for (int i = 0; i < numThreads; i++)
            {
                int startIndex = i * chunkSize;
                int endIndex = (i == numThreads - 1) ? inventory.Count : (i + 1) * chunkSize;

                // Print the start time for each thread
                Console.WriteLine($"Thread {i + 1} starting at {DateTime.Now.TimeOfDay}");


                tasks.Add(Task.Factory.StartNew(() => SearchItemsInRange(inventory, startIndex, endIndex, countdownEvent)));
            }

            // Wait for all tasks to complete
            Task.WaitAll(tasks.ToArray());

            // Combine results from all threads
            List<Tool> foundItems = tasks.SelectMany(t => t.Result).ToList();

            return foundItems;
        }

        private List<Tool> SearchItemsInRange(List<Tool> inventory, int startIndex, int endIndex, CountdownEvent countdownEvent)
        {
            List<Tool> localResults = new List<Tool>();

            int type1Count = 0;
            int type7Count = 0;
            int type10Count = 0;

            for (int i = startIndex; i < endIndex; i++)
            {
                var tool = inventory[i];

                // Check the type and count
                if (tool.Type == 1 && type1Count < ITEMS_OF_TYPE_1)
                {
                    localResults.Add(tool);
                    type1Count++;
                }
                else if (tool.Type == 7 && type7Count < ITEMS_OF_TYPE_7)
                {
                    localResults.Add(tool);
                    type7Count++;
                }
                else if (tool.Type == 10 && type10Count < ITEMS_OF_TYPE_10)
                {
                    localResults.Add(tool);
                    type10Count++;
                }

                // Break if all items are found
                if (type1Count == ITEMS_OF_TYPE_1 && type7Count == ITEMS_OF_TYPE_7 && type10Count == ITEMS_OF_TYPE_10)
                    break;
            }


            // Print the end time for each thread
            Console.WriteLine($"Thread ending at {DateTime.Now.TimeOfDay}");

            countdownEvent.Signal();

            return localResults;
        }
    }
}
