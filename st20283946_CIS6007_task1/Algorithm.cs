using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PDS
{
    class Algorithm
    {
        public static void ParallelBubbleSort(int[] arr, int numThreads)
        {
            int chunkSize = arr.Length / numThreads;

            Thread[] threads = new Thread[numThreads];

            // Use a CountdownEvent to signal when all tasks are completed
            CountdownEvent countdownEvent = new CountdownEvent(numThreads);

            for (int i = 0; i < numThreads; i++)
            {   
                int start = i * chunkSize;
                int end = (i == numThreads - 1) ? arr.Length : (i + 1) * chunkSize;
                // Start a task for each partition
                threads[i] = new Thread(() =>
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is sorting elements from index {start} to {end - 1}.");
                    BubbleSort(arr, start, end);
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished sorting.");
                });

                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            // After all threads have sorted their partitions, merge them
            for (int step = 1; step < arr.Length; step *= 2)
            {
                for (int i = 0; i < arr.Length - step; i += 2 * step)
                {
                    int start = i;
                    int middle = i + step;
                    int end = Math.Min(i + 2 * step, arr.Length);
                    Merge(arr, start, middle, end);
                }
            }
        }

        static void BubbleSort(int[] arr, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                for (int j = start; j < end - i - 1; j++)
                {
                    // Swap elements if they are in the wrong order
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }


        // Merge two sorted halves of an array [start, middle) and [middle, end)
        static void Merge(int[] arr, int start, int middle, int end)
        {
            int[] temp = new int[end - start];
            int left = start;
            int right = middle;
            int index = 0;

            // Merge elements in sorted order
            while (left < middle && right < end)
            {
                if (arr[left] <= arr[right])
                {
                    temp[index] = arr[left];
                    left++;
                }
                else
                {
                    temp[index] = arr[right];
                    right++;
                }
                index++;
            }

            // Copy remaining elements from the left half
            while (left < middle)
            {
                temp[index] = arr[left];
                left++;
                index++;
            }

            // Copy remaining elements from the right half
            while (right < end)
            {
                temp[index] = arr[right];
                right++;
                index++;
            }

            // Copy merged elements back to the original array
            Array.Copy(temp, 0, arr, start, temp.Length);
        }
    }

}

