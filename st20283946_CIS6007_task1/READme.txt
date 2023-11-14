1. Program Structure:

The program structure includes the following components:

Algorithm Class: The Algorithm class serves as the central component for sorting operations. Within this class, the implementation includes several key functions:

BubbleSort(arr, start, end):
This function within the Algorithm class is responsible for executing the bubble sort algorithm on a specific segment of the input array. It accepts the array 'arr' and the start and end indices to sort the elements within this specified range.

Merge(arr, start, middle, end):
The Merge function, also residing within the Algorithm class, facilitates the merging of two separately sorted subarrays back into the main array. It takes the array 'arr' along with the indices 'start', 'middle', and 'end', defining the portions to be merged in order for the Bubble Sort to makes sense.

ParallelBubbleSort(arr, numThreads):
Within the Algorithm class, the ParallelBubbleSort function orchestrates the parallel execution of the bubble sort algorithm using threads. This function partitions the input array into segments based on the specified number of threads ('numThreads'). Each thread independently sorts its designated segment utilizing the BubbleSort function. Finally, the sorted partitions are effectively merged back together employing the Merge function, resulting in the complete sorted array.

This structure enables the Algorithm class to manage and control the sorting process. It offers the capability to parallelize the bubble sort algorithm, ensuring efficient execution by dividing the workload among threads while synchronizing and merging the sorted sections to produce the final sorted array.


Main Program (Program.cs): Orchestrates the execution, performs tests, measures time, and writes results to console.


2. Evaluation of Tasks:

Parallelizability: Bubble sort is generally not highly suitable for parallelization due to its nature of comparing adjacent elements. The potential for parallelism is limited as elements need to be compared and swapped in a specific order.

Partitioning: The array is divided into partitions among threads to allow concurrent sorting. Each thread handles a distinct partition.

Communications: Limited communication is needed between threads as they work independently on their partitions.

Data Dependencies: The algorithm relies on comparing adjacent elements, leading to data dependencies that limit the degree of parallelism.

Synchronization: Threads synchronize at the end of sorting partitions using Join() to merge the sorted portions back together so it won't have conflicts between returned chunks and makes the algorithm not be in order.

Load Balancing: Load balancing is a concern as partitions might not always have an equal workload due to the nature of the sorting algorithm.


3. Test Results:


The test was run 5 times for each thread with the following measures

Average Execution Times:
2 Threads:
Average Time = (10319 + 10998 + 10114 + 9959 + 9989) / 5 ≈ 10257.8 ms (rounded to 1 decimal place)
3 Threads:
Average Time = (6224 + 4568 + 4479 + 4578 + 4944) / 5 ≈ 4958.6 ms (rounded to 1 decimal place)
4 Threads:
Average Time = (2853 + 2547 + 2531 + 2918 + 3353) / 5 ≈ 2840.4 ms (rounded to 1 decimal place)
6 Threads:
Average Time = (1547 + 1244 + 1238 + 1294 + 1307) / 5 ≈ 1326 ms (rounded to 1 decimal place)


Observations:


2 Threads:

Average Time: ~10257.8 ms
The average execution time using 2 threads for parallel bubble sort is notably higher compared to higher thread counts.
Limited reduction in execution time suggests potential inefficiency in parallelization due to the algorithm's constraints on parallelism.
3 Threads:

Average Time: ~4958.6 ms
A substantial decrease in execution time is observed when using 3 threads compared to 2 threads.
The execution time continues to decrease with an increase in thread count.
4 Threads:

Average Time: ~2840.4 ms
Further reduction in average execution time is evident with 4 threads, showcasing improved parallelism.
The reduction is less significant compared to the transition from 2 to 3 threads.
6 Threads:

Average Time: ~1326 ms
The most substantial reduction in average execution time occurs when moving from 4 to 6 threads.
With 6 threads, the execution time notably decreases, indicating improved parallel efficiency.


Conclusion:
The results demonstrate that while the nature of the bubble sort algorithm limits the extent of parallelism, an increase in the number of threads generally leads to reduced execution times.
Moving from fewer threads (2 threads) to a moderate number of threads (3 or 4 threads) shows a noticeable improvement in parallel execution and reduced sorting time.
However, as the thread count further increases (from 4 to 6 threads), the diminishing returns on execution time reduction become more evident, indicating a point of diminishing marginal gains due to potential overhead from managing more threads.
Overall, the experiment highlights the trade-off between parallelism and overhead management, suggesting an optimal balance between thread count and efficiency for the bubble sort algorithm in this context.
